import { jsx as _jsx } from "react/jsx-runtime";
import { createContext } from 'react';
import ReactDOM from 'react-dom/client';
import App from './App';
import Store from './store/store';
import './assets/styles/reset.css';
const store = new Store();
export const Context = createContext({
    store,
});
ReactDOM.createRoot(document.getElementById('root')).render(_jsx(Context.Provider, { value: { store }, children: _jsx(App, {}) }));
