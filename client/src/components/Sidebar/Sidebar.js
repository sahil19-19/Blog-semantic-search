import { jsx as _jsx, jsxs as _jsxs, Fragment as _Fragment } from "react/jsx-runtime";
import { NavLink } from "react-router-dom";
import menuList, { menuAuthList } from '../../assets/menuList';
import user from '../../assets/img/icons/user.png';
import s from './Sidebar.module.scss';
import { observer } from "mobx-react-lite";
import { useContext, useEffect, useState } from "react";
import { Context } from "../../main";
import { IoLogOutSharp } from "react-icons/io5";
import PostService from "../../service/PostsService";
import { BiSolidCategory } from "react-icons/bi";
import { IoCreateOutline } from "react-icons/io5";
import { IoMdClose } from "react-icons/io";
const Sidebar = ({ children }) => {
    const [categories, setCategories] = useState([]);
    const [open, setOpen] = useState(false);
    const { store } = useContext(Context);
    useEffect(() => {
        fetchCategory();
    }, []);
    async function fetchCategory() {
        try {
            const response = await PostService.fetchCategory();
            setCategories(response.data.result.topics);
        }
        catch (error) {
            console.log(error);
        }
    }
    return (_jsxs("div", { className: s.sidebar_container, children: [_jsx("div", { className: store.active ? `${s.sidebar__open}` : '', children: _jsxs("div", { className: open ? `${s.sidebar} ${s.sidebar__active}` : `${s.sidebar}`, children: [_jsxs("div", { className: `${s.sidebar__mb_user} ${s.sidebar__pc_hidden}`, children: [_jsx("button", { className: s.sidebar__close_btn, onClick: () => store.setActiveSidebar(false), children: _jsx(IoMdClose, {}) }), store.isAuth ? (_jsxs("div", { className: s.header__user_box, children: [_jsx("div", { className: s.header__user_avatar, children: store.user && typeof store.user.imageUri === 'string' && store.user.imageUri !== '' ?
                                                _jsx("img", { className: s.header__user_img, src: store.user.imageUri, alt: "user" })
                                                : _jsx("img", { className: s.header__user_img, src: user, alt: "user" }) }), store.user ? _jsxs("div", { className: s.header__user_name, children: ["Welcome ~ ", store.user.name] }) : _jsx(_Fragment, {})] })) : ('')] }), _jsxs("nav", { className: s.sidebar__menu, children: [_jsx("ul", { className: s.sidebar__list, children: menuList.listItem.map((item, index) => {
                                        return (_jsx("li", { className: s.sidebar__item, children: _jsxs(NavLink, { to: item.path, className: s.sidebar__item_link, children: [_jsx("span", { className: s.sidebar__icon, children: item.icon }), item.name] }) }, index));
                                    }) }), store.isAuth ? _jsxs(NavLink, { to: '/create-post', className: `${s.sidebar__item_link} ${s.sidebar__item_link_create}`, children: [_jsx("span", { className: s.sidebar__icon, children: _jsx(IoCreateOutline, {}) }), "Create a post"] }) : ''] }), _jsxs("div", { className: s.sidebar__category, children: [_jsx("ul", { className: open ? `${s.sidebar__list} ${s.sidebar__list_categorys} ${s.sidebar__list_categorys_active}` : `${s.sidebar__list} ${s.sidebar__list_categorys}`, children: categories.map((item) => {
                                        return (_jsx("li", { className: `${s.sidebar__item} ${s.sidebar__item_category}`, children: _jsxs(NavLink, { to: '/posts/category/' + item.name, className: s.sidebar__item_btn, children: [_jsx("span", { children: _jsx(BiSolidCategory, {}) }), item.name] }) }, item.id));
                                    }) }), _jsx("button", { onClick: () => setOpen(current => !current), className: s.sidebar__more_btn, children: open ? "See less" : "See more" })] }), store.isAuth ?
                            _jsx("div", { className: "sidebar__authmenu", children: _jsxs("ul", { className: s.sidebar__list, children: [menuAuthList.listItem.map((item, index) => {
                                            return (_jsx("li", { className: s.sidebar__item, children: _jsxs(NavLink, { to: item.path, className: s.sidebar__item_link, children: [_jsx("span", { className: s.sidebar__icon, children: item.icon }), item.name] }) }, index));
                                        }), _jsx("li", { className: s.sidebar__item, children: _jsxs("button", { onClick: () => store.logout(), className: `${s.sidebar__item_link} ${s.sidebar__item_link_logout}`, children: [_jsx("span", { className: s.sidebar__icon, children: _jsx(IoLogOutSharp, {}) }), "Logout"] }) })] }) })
                            : ''] }) }), _jsx("div", { className: "wrapper", children: _jsx("main", { className: "main", children: children }) })] }));
};
export default observer(Sidebar);
