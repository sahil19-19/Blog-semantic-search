import { jsx as _jsx, jsxs as _jsxs } from "react/jsx-runtime";
import { useParams } from 'react-router-dom';
import s from './SinglePage.module.scss';
import { useEffect, useState } from 'react';
import PostService from '../../service/PostsService';
const SinglePage = () => {
    const { id } = useParams();
    const [post, setPost] = useState(null);
    async function GetSinglePost(postId) {
        try {
            const response = await PostService.GetSinglePosts(postId);
            setPost(response.data.result.posts[0]);
        }
        catch (error) {
            console.error(error);
        }
    }
    useEffect(() => {
        if (id) {
            GetSinglePost(id);
        }
    }, [id]);
    return (_jsx("section", { className: s.single_post, children: _jsx("div", { className: "container", children: _jsxs("div", { className: s.single_post__inner, children: [post?.imageUri !== '' ?
                        _jsx("img", { className: s.single_post__img, src: post?.imageUri, alt: "post_img" })
                        : '', _jsx("h2", { className: s.single_post__title, children: post?.title }), _jsx("div", { className: s.single_post__categorys, children: post?.topics.map(item => {
                            return _jsx("div", { className: s.single_post__category, children: item.name }, item.id);
                        }) }), _jsx("div", { className: s.single_post__content, children: post?.description })] }) }) }));
};
export default SinglePage;
