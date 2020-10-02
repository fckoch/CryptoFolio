import React, { Component } from 'react';

const Wallet = () => {
    return (
        <div>
            <p>Wallet goes here</p>
            <p>{localStorage.getItem('user')}</p>
        </div>
    )
}

export default Wallet;