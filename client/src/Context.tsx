// src/Context.ts
import { createContext } from 'react';
import Store from './store/store';

export const store = new Store();

export interface State {
  store: Store;
}

export const Context = createContext<State>({
  store,
});
