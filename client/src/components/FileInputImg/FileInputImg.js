import { jsx as _jsx, jsxs as _jsxs } from "react/jsx-runtime";
import s from './FileInputImg.module.scss';
import { useContext, useRef, useState } from 'react';
import { Context } from '../../main';
import user from '../../assets/img/icons/user.png';
import { MdFileDownload } from "react-icons/md";
import { observer } from 'mobx-react-lite';
const FileInputImg = () => {
    const [imgUrl, setImgUrl] = useState('');
    const refImg = useRef(null);
    const { store } = useContext(Context);
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
    return (_jsxs("div", { className: s.account__form, children: [_jsxs("div", { className: s.account__input_file, children: [store.user.imageUri ?
                        _jsx("img", { ref: refImg, id: "file_upload", src: store.user.imageUri, alt: "your avatar", className: s.account__upload_img })
                        :
                            _jsx("img", { ref: refImg, id: "file_upload", src: user, alt: "your avatar", className: s.account__upload_img }), _jsxs("div", { className: s.account__file_box, children: [_jsxs("p", { className: s.account__file_text, children: ["Attach an image in JPG, PNG format.", _jsx("br", {}), "Maximum size 800 KB."] }), _jsx("div", { className: s.account__input_file_box, children: _jsxs("label", { className: s.account__input_file_upload, children: [_jsx(MdFileDownload, { className: s.account__input_file_icon }), _jsx("span", { className: s.account__upload_label, children: "Upload a photo" }), _jsx("input", { className: s.account__upload_input, type: "file", onChange: readURL, accept: "image/png, image/jpeg" })] }) })] })] }), _jsx("button", { className: s.account__form_btn, onClick: () => store.ChangeImg(imgUrl), children: "Submit" })] }));
};
export default observer(FileInputImg);
