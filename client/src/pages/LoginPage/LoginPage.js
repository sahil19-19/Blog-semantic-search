import { jsx as _jsx, jsxs as _jsxs } from "react/jsx-runtime";
import s from './LoginPage.module.scss';
import logo from '../../assets/img/logo.png';
import FormLogin from '../../components/FormLogin/FormLogin';
import { Link } from 'react-router-dom';
const LoginPage = () => {
    return (_jsx("section", { className: s.login, children: _jsx("div", { className: "container", children: _jsxs("div", { className: s.login__inner, children: [_jsx("img", { className: s.login__logo, src: logo, alt: "logo" }), _jsx("h2", { className: s.login__title, children: "Welcome back" }), _jsx(FormLogin, {}), _jsxs("p", { className: s.login__small_text, children: ["Don't have an account yet? ", _jsx("span", { children: _jsx(Link, { to: '/registration', className: s.login__link, children: "Register" }) })] })] }) }) }));
};
export default LoginPage;
