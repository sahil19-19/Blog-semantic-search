import { jsx as _jsx, jsxs as _jsxs } from "react/jsx-runtime";
import SearchBox from '../../components/SearchBox/SearchBox';
import s from './HomePage.module.scss';
import { observer } from 'mobx-react-lite';
import Post from '../../components/Post/Post';
import { useContext, useEffect, useState } from 'react';
import { Context } from '../../main';
import { useInView } from 'react-intersection-observer';
import { useNavigate } from 'react-router-dom';
const HomePage = () => {
    const { store } = useContext(Context);
    const [page, setPage] = useState(0);
    const navigate = useNavigate();
    const [ref, inView] = useInView({
        threshold: 0.6,
        triggerOnce: true,
    });
    useEffect(() => {
        store.ClearPosts();
    }, []);
    useEffect(() => {
        if (inView) {
            setPage((current) => current + 1);
        }
    }, [inView]);
    useEffect(() => {
        store.GetPosts(page);
    }, [page]);
    return (_jsx("section", { className: s.home, children: _jsxs("div", { className: "container", children: [_jsxs("div", { className: s.home__top, children: [_jsx("button", { className: s.semanticButton, onClick: () => navigate("/posts/semantic-search"), children: "Try Semantic Search" }), _jsx(SearchBox, {})] }), _jsxs("div", { className: s.home__items, children: [Array.isArray(store.posts) && store.posts.length > 0 ?
                            store.posts.map((item) => {
                                return (_jsx(Post, { title: item.title, imageUri: item.author.imageUri, name: item.author.name, dateCreated: item.dateCreated, topics: item.topics, description: item.description, postImg: item.imageUri, postId: item.id }, item.id));
                            })
                            : '', store.end ? '' : store.loading ? '' : _jsx("div", { ref: ref, className: s.home__loading })] })] }) }));
};
export default observer(HomePage);
