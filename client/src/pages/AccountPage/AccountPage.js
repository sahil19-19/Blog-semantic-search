import { jsx as _jsx, jsxs as _jsxs } from "react/jsx-runtime";
import s from './AccountPage.module.scss';
import { IoSettingsSharp } from "react-icons/io5";
import FileInputImg from '../../components/FileInputImg/FileInputImg';
import ChangePassword from '../../components/ChangePassword/ChangePassword';
import ChangeName from '../../components/ChangeName/ChangeName';
import { useContext } from 'react';
import { Context } from '../../main';
const AccountPage = () => {
    const { store } = useContext(Context);
    return (_jsx("section", { className: s.account, children: _jsx("div", { className: "container", children: store.isAuth ?
                _jsxs("div", { className: s.account__inner, children: [_jsxs("h2", { className: s.account__title, children: [_jsx("span", { children: _jsx(IoSettingsSharp, {}) }), "Your account settings"] }), _jsxs("div", { className: s.account__box, children: [_jsxs("div", { className: s.account__columns, children: [_jsx("h3", { className: s.account__sub_title, children: "Edit profile photo" }), _jsx(FileInputImg, {})] }), _jsxs("div", { className: s.account__columns, children: [_jsx(ChangeName, {}), _jsx(ChangePassword, {})] })] })] })
                :
                    _jsx("div", { className: s.account__inner, children: "Log in..." }) }) }));
};
export default AccountPage;
