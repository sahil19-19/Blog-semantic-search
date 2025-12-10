import { jsx as _jsx, jsxs as _jsxs } from "react/jsx-runtime";
import { useContext, useEffect, useState } from 'react';
import s from './SearchPage.module.scss';
import { Context } from '../../main';
import { observer } from 'mobx-react-lite';
import Post from '../../components/Post/Post';
import { useParams } from 'react-router-dom';
import { useInView } from 'react-intersection-observer';
const SearchPage = ({ type }) => {
    const { store } = useContext(Context);
    const { id } = useParams();
    const [page, setPage] = useState(0);
    const [ref, inView] = useInView({
        threshold: 0.6,
        triggerOnce: true,
    });
    useEffect(() => {
        store.ClearPosts();
    }, [id]);
    useEffect(() => {
        if (inView) {
            setPage((current) => current + 1);
        }
    }, [inView]);
    useEffect(() => {
        store.GetSearchPosts(id, type, page);
    }, [id, type, page]);
    return (_jsx("section", { className: s.search, children: _jsx("div", { className: "container", children: _jsxs("div", { className: s.search__inner, children: [_jsx("h2", { className: s.search__title, children: store.posts.length <= 0 ? "No results" : `Search: ${id}` }), _jsxs("div", { className: s.search__items, children: [Array.isArray(store.posts) && store.posts.length > 0 ?
                                store.posts.map((item) => {
                                    return (_jsx(Post, { title: item.title, imageUri: item.author.imageUri, name: item.author.name, dateCreated: item.dateCreated, topics: item.topics, description: item.description, postImg: item.imageUri, postId: item.id }, item.id));
                                })
                                : '', store.end ? '' : store.loading ? '' : _jsx("div", { ref: ref, className: s.search__loading })] })] }) }) }));
};
export default observer(SearchPage);
