import { jsx as _jsx, jsxs as _jsxs, Fragment as _Fragment } from "react/jsx-runtime";
import { Link, NavLink } from 'react-router-dom';
import s from './Header.module.scss';
import logo from '../../assets/img/logo.png';
import user from '../../assets/img/icons/user.png';
import { observer } from 'mobx-react-lite';
import { useContext } from 'react';
import { Context } from '../../main';
import { RxHamburgerMenu } from "react-icons/rx";
const Header = () => {
    const { store } = useContext(Context);
    return (_jsx("header", { className: s.header, children: _jsx("div", { className: "container-fluid", children: _jsxs("div", { className: s.header__inner, children: [_jsx(NavLink, { to: '/', children: _jsx("img", { className: s.header__logo, src: logo, alt: "logo" }) }), store.isAuth ? (_jsxs("div", { className: `${s.header__user_box} ${s.header__mb_hidden}`, children: [_jsx("div", { className: s.header__user_avatar, children: store.user && typeof store.user.imageUri === 'string' && store.user.imageUri !== '' ?
                                    _jsx("img", { className: s.header__user_img, src: store.user.imageUri, alt: "user" })
                                    : _jsx("img", { className: s.header__user_img, src: user, alt: "user" }) }), store.user ? _jsxs("div", { className: s.header__user_name, children: ["Welcome ~ ", store.user.name] }) : _jsx(_Fragment, {})] })) : (_jsx(Link, { to: '/login', className: s.header__auth_btn, children: "Login" })), _jsx("div", { className: s.header__pc_hidden, children: _jsx("button", { className: s.header__burger, onClick: () => store.setActiveSidebar(true), children: _jsx(RxHamburgerMenu, {}) }) })] }) }) }));
};
export default observer(Header);
