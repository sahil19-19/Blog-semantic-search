## Purpose

This file gives concise, actionable guidance to AI coding agents working on this repository so they can be productive right away.

**Project Overview**
- **Stack:** React + TypeScript + Vite (see `package.json`).
- **State:** MobX store in `src/store/store.ts` (single `Store` instance provided via `Context` in `src/main.tsx`).
- **Routing:** React Router configured in `src/App.tsx`.
- **API:** Axios instance at `src/assets/utils/axios/index.ts` uses a hard-coded `API_URL` (`http://localhost:5010/api/v1`) and attaches `Authorization: Bearer <token>` from `localStorage`.

**Key Patterns & Conventions**
- **State access:** Components use `useContext(Context)` to get `{ store }` from `src/main.tsx` and are wrapped with `observer` from `mobx-react-lite` when they read/store observable data (e.g., `src/components/Header/Header.tsx`).
- **Services:** API logic lives under `src/service/*.ts`. Services are exported as default classes and return typed `AxiosResponse<T>` from the shared `$api` instance. Example: `src/service/PostsService.ts`.
- **Method naming:** Service methods use PascalCase (e.g., `GetPosts`, `CreatePost`) — mirror that style when adding similar service methods to keep consistency.
- **Forms & file uploads:** File uploads use `FormData` and endpoints expect multipart bodies — see `CreatePost` flow in `src/store/store.ts` and `src/pages/CreatePostPage/CreatePostPage.tsx`.
- **Styling:** SCSS modules are used per-component: `ComponentName.module.scss` next to the component TSX file.

**Auth & Tokens**
- Token storage: `localStorage.setItem('token', ...)` under the key `token`.
- `src/assets/utils/axios/index.ts` automatically attaches the token on requests and attempts a refresh when a `401` is received (it calls `AuthService.refresh()` and retries the original request once).
- On app start `App.tsx` calls `store.checkAuth()` if a token exists.

**Dev / Build / Debugging**
- Start dev server: `npm install` then `npm run dev` (runs `vite`).
- Build: `npm run build` (runs `tsc && vite build`).
- Preview a build: `npm run preview`.
- Backend requirement: the frontend expects the API at `http://localhost:5010/api/v1`. If the backend runs elsewhere, update `API_URL` in `src/assets/utils/axios/index.ts`.

**Files to inspect when making changes**
- `src/store/store.ts` — central place for app state/actions (MobX).
- `src/assets/utils/axios/index.ts` — API base, interceptors (auth refresh) and `withCredentials` setting.
- `src/service/*` — add/modify service methods here (follow pattern returning `AxiosResponse<T>`).
- `src/main.tsx` — `Context` provider and store instantiation.
- `src/App.tsx` — app routes and auth check on startup.

**Examples**
- Adding a new API method:

```ts
// src/service/ExampleService.ts
import $api from '../assets/utils/axios/index';
import { AxiosResponse } from 'axios';

export default class ExampleService {
  static async GetSomething(id: string): Promise<AxiosResponse<any>> {
    return $api.get(`/something/${id}`);
  }
}
```

- Using store in a component:

```tsx
import { useContext } from 'react';
import { Context } from '../../main';

const Comp = () => {
  const { store } = useContext(Context);
  // call store actions like store.GetPosts(0)
}
```

**Project quirks / things to watch for**
- `src/service/UserServer.ts` exports a class named `AuthService` — file name vs class name mismatch exists; search by exported class name if an import fails.
- There are dependencies in `package.json` (like `redux` / `react-redux`) that are not currently used — check before introducing related code.
- Service and store method names use inconsistent casing (PascalCase for service methods). Follow the existing casing when modifying or adding.
- API base is hard-coded in the axios wrapper — update it instead of sprinkling environment changes across services.

If anything in this doc is unclear or you'd like additional examples (testing instructions, CI steps, or more file-level guidance), tell me which area to expand and I'll iterate.

