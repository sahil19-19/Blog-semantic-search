import { jsx as _jsx, jsxs as _jsxs, Fragment as _Fragment } from "react/jsx-runtime";
import { useForm } from 'react-hook-form';
import s from './CreatePostForm.module.scss';
const CreatePostForm = ({ postTitle, setPostTitle, postDescr, setPostDescr, Submit }) => {
    const { register, formState: { errors, isValid }, handleSubmit, } = useForm({
        mode: 'onBlur'
    });
    return (_jsx(_Fragment, { children: _jsxs("form", { className: s.postform__form, onSubmit: handleSubmit(() => Submit(isValid)), children: [_jsxs("label", { className: s.postform__input_box, children: [_jsx("span", { children: "Post title" }), _jsx("input", { ...register('PostTitle', {
                                required: true,
                                minLength: 3,
                            }), onChange: (e) => setPostTitle(e.target.value), value: postTitle, className: s.postform__input, type: "text", placeholder: 'Enter title' }), errors.PostTitle && errors.PostTitle.type === "required" ? _jsx("div", { className: s.postform__error_message, children: "This field is required" }) : '', errors.PostTitle && errors.PostTitle.type === "minLength" ? _jsx("div", { className: s.postform__error_message, children: "Minimum password length 3 characters" }) : ''] }), _jsxs("label", { className: s.postform__input_box, children: [_jsx("span", { children: "Post description" }), _jsx("textarea", { ...register('PostDescription', {
                                required: true,
                                minLength: 10,
                            }), onChange: (e) => setPostDescr(e.target.value), value: postDescr, className: s.postform__textarea, placeholder: 'Enter description' }), errors.PostDescription && errors.PostDescription.type === "required" ? _jsx("div", { className: s.postform__error_message, children: "This field is required" }) : '', errors.PostDescription && errors.PostDescription.type === "minLength" ? _jsx("div", { className: s.postform__error_message, children: "Minimum password length 10 characters" }) : ''] }), _jsx("button", { type: 'submit', className: s.postform__btn, children: "Submit" })] }) }));
};
export default CreatePostForm;
