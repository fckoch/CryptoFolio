import React, { Component } from 'react';
import './Button.css';

const Button = (props) => {
    return (
        <div className={props.type}>
            <button className="button" onClick={props.handleClick}>{props.label}</button>
        </div>
    );
}

export default Button;