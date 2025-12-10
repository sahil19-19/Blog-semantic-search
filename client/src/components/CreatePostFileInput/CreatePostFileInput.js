import { jsx as _jsx, jsxs as _jsxs } from "react/jsx-runtime";
import { MdFileDownload } from "react-icons/md";
import { useRef } from 'react';
import s from './CreatePostFileInput.module.scss';
const CreatePostFileInput = ({ imgUrl, setImgUrl }) => {
    const refImg = useRef(null);
    const readURL = (event) => {
        const input = event.target;
        if (input.files && input.files[0]) {
            const reader = new FileReader();
            setImgUrl(input.files[0]);
            reader.onload = function (e) {
                if (refImg.current) {
                    refImg.current.src = e.target?.result;
                }
            };
            reader.readAsDataURL(input.files[0]);
        }
    };
    return (_jsxs("div", { className: s.postform__input_file, children: [imgUrl !== '' ?
                _jsx("img", { ref: refImg, id: "file_upload", src: '', alt: "post img", className: s.postform__upload_img })
                :
                    _jsx("div", { className: s.postform__null, children: "Post image" }), _jsxs("div", { className: s.postform__file_box, children: [_jsxs("p", { className: s.postform__file_text, children: ["Attach an image in JPG, PNG format.", _jsx("br", {}), "Maximum size 800 KB."] }), _jsx("div", { className: s.postform__input_file_box, children: _jsxs("label", { className: s.postform__input_file_upload, children: [_jsx(MdFileDownload, { className: s.postform__input_file_icon }), _jsx("span", { className: s.postform__upload_label, children: "Upload a photo" }), _jsx("input", { className: s.postform__upload_input, type: "file", onChange: readURL, accept: "image/png, image/jpeg" })] }) })] })] }));
};
export default CreatePostFileInput;
