import React, { createContext } from 'react'
import ReactDOM from 'react-dom/client'
import App from './App'
import Store from './store/store';
import './assets/styles/reset.css'
// import './index.css'

interface State {
  store: Store,
}

const store = new Store();

export const Context = createContext<State>({
  store,
})

ReactDOM.createRoot(document.getElementById('root')!).render(
    <Context.Provider value={{ store }}>
      <App />
    </Context.Provider>,
)
