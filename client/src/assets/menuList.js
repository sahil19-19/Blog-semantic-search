import { jsx as _jsx } from "react/jsx-runtime";
import { MdHome } from "react-icons/md";
import { MdAccountCircle } from "react-icons/md";
import { MdLocalFireDepartment } from "react-icons/md";
const menuList = {
    listItem: [
        {
            path: '/',
            name: 'Home',
            icon: _jsx(MdHome, {})
        },
        {
            path: '/posts/search/Popular',
            name: 'Popular',
            icon: _jsx(MdLocalFireDepartment, {})
        }
    ]
};
export const menuAuthList = {
    listItem: [
        {
            path: '/account',
            name: 'Account',
            icon: _jsx(MdAccountCircle, {})
        }
    ]
};
export default menuList;
