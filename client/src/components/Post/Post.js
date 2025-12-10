import { jsx as _jsx, jsxs as _jsxs } from "react/jsx-runtime";
import { Link } from 'react-router-dom';
import s from './Post.module.scss';
import user from '../../assets/img/icons/user.png';
import { IoMdArrowDropright } from "react-icons/io";
import { observer } from 'mobx-react-lite';
const Post = ({ title, imageUri, name, dateCreated, topics, description, postImg, postId }) => {
    return (_jsxs("div", { className: s.home__item, children: [_jsxs("div", { className: s.home__item_top, children: [_jsxs("div", { className: s.home__item_user, children: [imageUri && typeof imageUri === 'string' && imageUri !== '' ?
                                _jsx("img", { className: s.home__item_user_img, src: imageUri, alt: "user" })
                                : _jsx("img", { className: s.home__item_user_img, src: user, alt: "user" }), _jsx("p", { className: s.home__item_user_name, children: name })] }), _jsx("div", { className: s.home__item_data, children: _jsx("p", { className: s.home__item_data_time, children: dateCreated }) })] }), _jsx("h3", { className: s.home__item_title, children: title }), postImg && typeof postImg === 'string' && postImg !== '' ?
                _jsx("img", { className: s.home__item_img, src: postImg, alt: "post-img" })
                : '', _jsx("div", { className: s.home__item_data_categorys, children: topics.map(topic => {
                    return _jsx("div", { className: s.home__item_data_category, children: topic.name }, topic.id);
                }) }), _jsx("p", { className: s.home__item_text, children: description }), _jsxs(Link, { to: '/post/' + postId, className: s.home__item_btn, children: ["\u041F\u043E\u0434\u0440\u043E\u0431\u043D\u0435\u0435 ", _jsx("span", { children: _jsx(IoMdArrowDropright, {}) })] })] }));
};
export default observer(Post);
