import { jsx as _jsx, jsxs as _jsxs } from "react/jsx-runtime";
import { Link } from 'react-router-dom';
import s from './SearchBox.module.scss';
import { IoSearch } from "react-icons/io5";
import { useState } from 'react';
const SearchBox = () => {
    const [search, setSearch] = useState('');
    return (_jsxs("div", { className: s.home__input_box, children: [_jsx("input", { onChange: (e) => setSearch(e.target.value), value: search, className: s.home__input, type: "text", placeholder: 'Search...' }), _jsx(Link, { to: '/posts/search/' + search, className: s.home__search_btn, children: _jsx(IoSearch, {}) })] }));
};
export default SearchBox;
