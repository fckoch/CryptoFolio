import React from 'react';
import SignIn from "../components/LoginForm/LoginForm.js";

const Login = (props) => {
    return (
        <div>
            <SignIn {...props}/>
        </div>
    );
}

export default Login;