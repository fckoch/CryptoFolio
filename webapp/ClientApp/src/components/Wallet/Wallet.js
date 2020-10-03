import React, { Component } from 'react';
import { render } from 'react-dom';
import AuthService from '../../services/authenticationService.js';

class Wallet extends Component {
    constructor(props) {
        super()

        this.state = {
            email: '',
            role: '',
            nameid: '',
            unique_name: ''
        }
    }

    render() {

    const {email, role, nameid, unique_name} = AuthService.getTokenData()
        return (
            <div>
                <p>Wallet goes here</p>
                <p>{email}</p>
            </div>
        )
    }
}

export default Wallet;