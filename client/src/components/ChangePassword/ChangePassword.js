import { jsx as _jsx, jsxs as _jsxs, Fragment as _Fragment } from "react/jsx-runtime";
import { useContext, useState } from 'react';
import s from './ChangePassword.module.scss';
import { Context } from '../../main';
import { observer } from 'mobx-react-lite';
import { useForm } from 'react-hook-form';
import { SnackbarProvider, enqueueSnackbar } from 'notistack';
const ChangePassword = () => {
    const { register, formState: { errors, isValid }, handleSubmit, } = useForm({
        mode: 'onBlur'
    });
    const [newPassword, setNewPassword] = useState('');
    const [repeatPassword, setRepeatPassword] = useState('');
    const { store } = useContext(Context);
    const Submit = () => {
        if (newPassword && repeatPassword && newPassword !== repeatPassword) {
            return enqueueSnackbar("Password mismatch!");
        }
        if (isValid) {
            enqueueSnackbar("Password changed successfully");
            setNewPassword('');
            setRepeatPassword('');
            return store.ChangePassword(newPassword);
        }
        else {
            enqueueSnackbar("Fill out the form correctly");
        }
    };
    return (_jsxs(_Fragment, { children: [_jsx(SnackbarProvider, { autoHideDuration: 3000 }), _jsxs("form", { className: s.account__password_form, onSubmit: handleSubmit(Submit), children: [_jsx("h3", { className: s.account__sub_title, children: "Change user password" }), _jsx("input", { ...register('NewPassword', {
                            required: true,
                            minLength: 3,
                            maxLength: 21,
                        }), onChange: (e) => setNewPassword(e.target.value), value: newPassword, className: s.account__input, type: "password", placeholder: 'Enter a new password' }), errors.NewPassword && errors.NewPassword.type === "required" ? _jsx("div", { className: s.account__error_message, children: "This field is required" }) : '', errors.NewPassword && errors.NewPassword.type === "minLength" ? _jsx("div", { className: s.account__error_message, children: "Minimum password length 3 characters" }) : '', errors.NewPassword && errors.NewPassword.type === "maxLength" ? _jsx("div", { className: s.account__error_message, children: "Maximum password length 21 characters" }) : '', _jsx("input", { ...register('RepeatPassword', {
                            required: true,
                            minLength: 3,
                            maxLength: 21,
                        }), onChange: (e) => setRepeatPassword(e.target.value), value: repeatPassword, className: s.account__input, type: "password", placeholder: 'Repeat new password' }), errors.RepeatPassword && errors.RepeatPassword.type === "required" ? _jsx("div", { className: s.account__error_message, children: "This field is required" }) : '', errors.RepeatPassword && errors.RepeatPassword.type === "minLength" ? _jsx("div", { className: s.account__error_message, children: "Minimum password length 3 characters" }) : '', errors.RepeatPassword && errors.RepeatPassword.type === "maxLength" ? _jsx("div", { className: s.account__error_message, children: "Maximum password length 21 characters" }) : '', _jsx("button", { className: s.account__btn, children: "Submit" })] })] }));
};
export default observer(ChangePassword);
