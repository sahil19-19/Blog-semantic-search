import { jsx as _jsx, jsxs as _jsxs } from "react/jsx-runtime";
import { useEffect, useState } from 'react';
import s from './CreatePostSelectCategorys.module.scss';
import { IoIosCloseCircle } from "react-icons/io";
import PostService from '../../service/PostsService';
const CreatePostSelectCategorys = ({ selectedCategories, setSelectedCategories, open, setOpen }) => {
    const [categorys, setCategorys] = useState([]);
    useEffect(() => {
        fetchCategory();
    }, []);
    async function fetchCategory() {
        try {
            const response = await PostService.fetchCategory();
            setCategorys(response.data.result.topics);
        }
        catch (error) {
            console.log(error);
        }
    }
    const toggleCategory = (categoryId) => {
        const index = selectedCategories.indexOf(categoryId);
        if (index === -1) {
            setSelectedCategories([...selectedCategories, categoryId]);
        }
        else {
            const updatedCategories = [...selectedCategories];
            updatedCategories.splice(index, 1);
            setSelectedCategories(updatedCategories);
        }
    };
    return (_jsxs("div", { className: `${s.postform__popup} ${open ? s.postform__popup_active : ''}`, children: [_jsxs("div", { className: s.postform__popup_top, children: [_jsx("h3", { className: s.postform__popup_title, children: "Select a category" }), _jsx("button", { onClick: () => setOpen(false), className: s.postform__popup_close, children: _jsx(IoIosCloseCircle, {}) })] }), _jsx("div", { className: s.postform__popup_items, children: categorys.map((item) => {
                    const isSelected = selectedCategories.includes(item.id);
                    return (_jsx("div", { className: s.postform__popup_item, children: _jsx("button", { className: `${s.postform__item_btn} ${isSelected ? s.postform__item_btn_active : ''}`, onClick: () => toggleCategory(item.id), children: item.name }) }, item.id));
                }) })] }));
};
export default CreatePostSelectCategorys;
