import { jsx as _jsx, jsxs as _jsxs, Fragment as _Fragment } from "react/jsx-runtime";
import { useContext, useState } from "react";
import s from './ChangeName.module.scss';
import { Context } from "../../main";
import { observer } from "mobx-react-lite";
import { SnackbarProvider, enqueueSnackbar } from "notistack";
import { useForm } from "react-hook-form";
const ChangeName = () => {
    const { register, formState: { errors, isValid }, handleSubmit, } = useForm({
        mode: 'onBlur'
    });
    const [newName, setNewName] = useState('');
    const { store } = useContext(Context);
    const Submit = () => {
        if (isValid) {
            store.ChangeName(newName);
            enqueueSnackbar("Name changed successfully");
            setNewName('');
        }
        else {
            enqueueSnackbar("Fill out the form correctly");
        }
    };
    return (_jsxs(_Fragment, { children: [_jsx(SnackbarProvider, { autoHideDuration: 3000 }), _jsxs("form", { className: s.account__name_form, onSubmit: handleSubmit(Submit), children: [_jsx("h3", { className: s.account__sub_title, children: "Change user password" }), _jsx("input", { ...register('FirstName', {
                            required: true
                        }), onChange: (e) => setNewName(e.target.value), value: newName, className: s.account__input, type: "text", placeholder: 'Enter a new name' }), errors.FirstName && errors.FirstName.type === "required" ? _jsx("div", { className: s.account__error_message, children: "This field is required" }) : '', _jsx("button", { className: s.account__btn, children: "Submit" })] })] }));
};
export default observer(ChangeName);
