import { jsx as _jsx, jsxs as _jsxs, Fragment as _Fragment } from "react/jsx-runtime";
import './assets/styles/global.scss';
import { BrowserRouter as Router, Routes, Route, Navigate } from "react-router-dom";
import ScrollToTop from './assets/utils/ScrollToTop';
import HomePage from './pages/HomePage/HomePage';
import LoginPage from './pages/LoginPage/LoginPage';
import Sidebar from './components/Sidebar/Sidebar';
import Header from './components/Header/Header';
import { observer } from 'mobx-react-lite';
import RegistrationPage from './pages/RegistrationPage/RegistrationPage';
import { useContext, useEffect } from 'react';
import { Context } from './main';
import AccountPage from './pages/AccountPage/AccountPage';
import CreatePostPage from './pages/CreatePostPage/CreatePostPage';
import SinglePage from './pages/SinglePage/SinglePage';
import SearchPage from './pages/SearchPage/SearchPage';
import SemanticSearch from './pages/SemanticSearch/SemanticSearch';
const App = () => {
    const { store } = useContext(Context);
    useEffect(() => {
        if (localStorage.getItem('token')) {
            store.checkAuth();
        }
    }, []);
    return (_jsx(_Fragment, { children: _jsxs(Router, { children: [_jsx(ScrollToTop, {}), _jsx(Header, {}), _jsx(Sidebar, { children: _jsxs(Routes, { children: [_jsx(Route, { path: "/posts/search/:id", element: _jsx(SearchPage, { type: 'search' }) }), _jsx(Route, { path: "/posts/semantic-search", element: _jsx(SemanticSearch, {}) }), _jsx(Route, { path: "/posts/search/:id", element: _jsx(SearchPage, { type: 'search' }) }), _jsx(Route, { path: "/posts/category/:id", element: _jsx(SearchPage, { type: 'category' }) }), _jsx(Route, { path: "/post/:id", element: _jsx(SinglePage, {}) }), _jsx(Route, { path: "/create-post", element: _jsx(CreatePostPage, {}) }), _jsx(Route, { path: "/account", element: _jsx(AccountPage, {}) }), _jsx(Route, { path: "/login", element: _jsx(LoginPage, {}) }), _jsx(Route, { path: "/registration", element: _jsx(RegistrationPage, {}) }), _jsx(Route, { path: "/", element: _jsx(HomePage, {}) }), _jsx(Route, { path: "*", element: _jsx(Navigate, { to: "/" }) })] }) })] }) }));
};
export default observer(App);
