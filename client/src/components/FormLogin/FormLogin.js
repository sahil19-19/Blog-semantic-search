import { jsx as _jsx, jsxs as _jsxs, Fragment as _Fragment } from "react/jsx-runtime";
import s from './FormLogin.module.scss';
import { useState, useContext } from 'react';
import { observer } from 'mobx-react-lite';
import { Context } from '../../main';
import { useNavigate } from 'react-router-dom';
import { SnackbarProvider } from 'notistack';
import { useForm } from 'react-hook-form';
const Form = () => {
    const { register, formState: { errors, isValid }, handleSubmit, } = useForm({
        mode: 'onBlur'
    });
    const navigate = useNavigate();
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const { store } = useContext(Context);
    store.setRedirectCallback((path) => {
        navigate(path);
    });
    const Submit = () => {
        if (isValid) {
            store.login(email, password);
            setEmail('');
            setPassword('');
        }
    };
    return (_jsxs(_Fragment, { children: [_jsx(SnackbarProvider, { autoHideDuration: 3000 }), _jsxs("form", { className: s.form, onSubmit: handleSubmit(Submit), children: [_jsx("input", { ...register('Email', {
                            required: true,
                            pattern: /^((([0-9A-Za-z]{1}[-0-9A-z\.]{1,}[0-9A-Za-z]{1})|([0-9А-Яа-я]{1}[-0-9А-я\.]{1,}[0-9А-Яа-я]{1}))@([-A-Za-z]{1,}\.){1,2}[-A-Za-z]{2,})$/u,
                        }), onChange: (e) => setEmail(e.target.value), value: email, className: s.form__input, type: "email", placeholder: 'Enter email' }), errors.Email && errors.Email.type === "required" ? _jsx("div", { className: s.form__error_message, children: "This field is required" }) : '', errors.Email && errors.Email.type === "pattern" ? _jsx("div", { className: s.form__error_message, children: "Incorrect email" }) : '', _jsx("input", { ...register('Password', {
                            required: true,
                            minLength: 3,
                            maxLength: 21,
                        }), onChange: (e) => setPassword(e.target.value), value: password, className: s.form__input, type: "password", placeholder: 'Enter password' }), errors.Password && errors.Password.type === "required" ? _jsx("div", { className: s.form__error_message, children: "This field is required" }) : '', errors.Password && errors.Password.type === "minLength" ? _jsx("div", { className: s.form__error_message, children: "Minimum password length 3 characters" }) : '', errors.Password && errors.Password.type === "maxLength" ? _jsx("div", { className: s.form__error_message, children: "Maximum password length 21 characters" }) : '', _jsx("button", { className: s.form__btn, type: 'submit', children: "Login" })] })] }));
};
export default observer(Form);
