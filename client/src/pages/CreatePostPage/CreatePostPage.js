import { jsx as _jsx, jsxs as _jsxs, Fragment as _Fragment } from "react/jsx-runtime";
import { useContext, useState } from 'react';
import s from './CreatePostPage.module.scss';
import { observer } from 'mobx-react-lite';
import { IoCreate } from "react-icons/io5";
import { Context } from '../../main';
import CreatePostFileInput from '../../components/CreatePostFileInput/CreatePostFileInput';
import CreatePostForm from '../../components/CreatePostForm/CreatePostForm';
import CreatePostSelectCategorys from '../../components/CreatePostSelectCategorys/CreatePostSelectCategorys';
import { SnackbarProvider, enqueueSnackbar } from 'notistack';
const CreatePostPage = () => {
    const [imgUrl, setImgUrl] = useState('');
    const [postTitle, setPostTitle] = useState('');
    const [postDescr, setPostDescr] = useState('');
    const [selectedCategories, setSelectedCategories] = useState([]);
    const [open, setOpen] = useState(false);
    const { store } = useContext(Context);
    const Submit = (isValid) => {
        if (isValid) {
            enqueueSnackbar("Post successfully created!");
            setPostTitle('');
            setPostDescr('');
            setImgUrl('');
            return store.CreatePost(postTitle, postDescr, selectedCategories, imgUrl);
        }
        else {
            enqueueSnackbar("Fill out the form correctly");
        }
    };
    return (_jsxs(_Fragment, { children: [_jsx(SnackbarProvider, { autoHideDuration: 3000 }), _jsx("section", { className: s.postform, children: _jsx("div", { className: "container", children: store.isAuth ?
                        _jsxs("div", { className: s.postform__inner, children: [_jsxs("h2", { className: s.postform__title, children: [_jsx("span", { children: _jsx(IoCreate, {}) }), "Create a post"] }), _jsxs("div", { className: s.postform__row, children: [_jsx("div", { className: s.postform__columns, children: _jsx(CreatePostFileInput, { imgUrl: imgUrl, setImgUrl: setImgUrl }) }), _jsxs("div", { className: s.postform__columns, children: [_jsx(CreatePostForm, { postTitle: postTitle, setPostTitle: setPostTitle, postDescr: postDescr, setPostDescr: setPostDescr, Submit: Submit }), _jsx("button", { onClick: () => setOpen(current => !current), className: s.postform__category_all, children: "Select post category" })] })] }), _jsx(CreatePostSelectCategorys, { selectedCategories: selectedCategories, setSelectedCategories: setSelectedCategories, open: open, setOpen: setOpen })] })
                        :
                            _jsx("div", { className: s.postform__inner, children: "Log in..." }) }) })] }));
};
export default observer(CreatePostPage);
