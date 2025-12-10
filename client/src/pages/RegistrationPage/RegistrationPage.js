import { jsx as _jsx, jsxs as _jsxs } from "react/jsx-runtime";
import { Link } from "react-router-dom";
import FormRgister from "../../components/FormRegister/FormRgister";
import s from './RegistrationPage.module.scss';
const RegistrationPage = () => {
    return (_jsx("div", { className: s.registration, children: _jsx("div", { className: "container", children: _jsxs("div", { className: s.registration__inner, children: [_jsxs("h2", { className: s.registration__title, children: ["Welcome!", _jsx("br", {}), "Register your account"] }), _jsx(FormRgister, {}), _jsxs("p", { className: s.registration__small_text, children: ["Already have an account? ", _jsx("span", { children: _jsx(Link, { to: '/login', className: s.registration__link, children: "Login" }) })] })] }) }) }));
};
export default RegistrationPage;
