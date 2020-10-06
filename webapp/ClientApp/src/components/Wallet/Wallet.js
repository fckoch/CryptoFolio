import React, { Component } from 'react';
import Modal from 'react-modal';
import AuthService from '../../services/authenticationService.js';
import './Wallet.css';
import CoinForm from '../CoinForm/CoinForm.js';

class Wallet extends Component {
    constructor(props) {
        super()
        this.showModal = this.showModal.bind(this);

        this.state = {
            email: '',
            role: '',
            nameid: '',
            unique_name: '',
            modalIsOpen: false,
        }
    }

    showModal = () => {
        this.setState({
            modalIsOpen: true
        })
    }

    hideModal = () => {
        this.setState({
            modalIsOpen: false
        })
    }

    render() {

    const {email, role, nameid, unique_name, certserialnumber} = AuthService.getTokenData()
        return (
            <div className="wrapper">
                <div className="main">
                    <header className="header">
                        <div className="header-wrapper">
                            <div className="button-wrapper">
                                <button className="add-coin-button" onClick={this.showModal}>Add new coin</button>
                            </div>
                            <div className="gains-wrapper">
                                <table className="gains-table">
                                    <tr>
                                        <th>Ytd</th>
                                        <th>30d</th>
                                        <th>7d</th>
                                        <th>24h</th>
                                    </tr>
                                    <tr>
                                        <td>+150%</td>
                                        <td>+80%</td>
                                        <td>+28%</td>
                                        <td>+7%</td>
                                    </tr>
                                </table>
                            </div>
                            <div className="networth-wrapper">
                                <h1>$8.107,00</h1>
                            </div>
                        </div>
                    </header>
                    <div className="wallet">
                        <table className="wallet-table">
                            <thead className="wallet-table-header">
                                <tr>
                                    <th>Coin</th>
                                    <th>Quantity</th>
                                    <th>Buy value</th>
                                    <th>Current value</th>
                                    <th>Change</th>
                                    <th>Gain</th>
                                    <th>Total worth</th>
                                </tr>
                            </thead>
                            <tbody className="wallet-table-body">
                                <tr>
                                    <td>BTC</td>
                                    <td>0.28</td>
                                    <td>$11.453,00</td>
                                    <td>$22.423,25</td>
                                    <td>+80%</td>
                                    <td>$5.403,00</td>
                                    <td>$6.280,40</td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
                <Modal className="modal" isOpen={this.state.modalIsOpen}>
                    <CoinForm showModal={this.showModal} hideModal={this.hideModal}/>
                </Modal>
            </div>
        )
    }
}

export default Wallet;