import React, { Component } from 'react';
import Modal from 'react-modal';
import AuthService from '../../services/authenticationService.js';
import WalletCoinService from '../../services/walletCoinService.js';
import './Wallet.css';
import AddCoinForm from '../AddCoinForm/AddCoinForm.js';
import EditCoinForm from '../EditCoinForm/EditCoinForm.js';
import Button from "../Button/Button.js";
import axios from "axios";
import EditIcon from '@material-ui/icons/Edit';
import DeleteOutlineIcon from '@material-ui/icons/DeleteOutline';

import { withStyles } from '@material-ui/core/styles';

const styles = theme => ({
    DeleteOutlineIcon: {
        '&:hover': {
            cursor: 'pointer'
        }
    },
    EditIcon: {
        '&:hover': {
            cursor: 'pointer'
        }
    }
});

class Wallet extends Component {
    constructor(props) {
        super()

        this.state = {
            userId: '',
            walletId: '',
            addCoinModalIsOpen: false,
            editCoinModalIsOpen: false,
            walletCoins: [],
            networth: ''
        }
    }

    componentDidMount() {
        const {nameid} = AuthService.getTokenData();

        (async () => { 
            const response = await axios.get(`https://localhost:5001/api/users/${nameid}`)
            this.setState({
                userId: response.data.userId,
                walletId: response.data.wallet.walletId,
                walletCoins: response.data.wallet.walletcoins
            })
            this.calculateNetWorth();
        })();

    }

    refreshWalletCoins = () => {
        (async () => { 
            const response = await axios.get(`https://localhost:5001/api/users/${this.state.userId}`)
            this.setState({
                walletCoins: response.data.wallet.walletcoins
            })
            this.calculateNetWorth();
        })();
    }

    showAddCoinModal = () => {
        this.setState({
            addCoinModalIsOpen: true
        })
    }

    hideAddCoinModal = () => {
        this.setState({
            addCoinModalIsOpen: false
        })
    }

    showEditCoinModal = () => {
        this.setState({
            editCoinModalIsOpen: true
        })
    }

    hideEditCoinModal = () => {
        this.setState({
            editCoinModalIsOpen: false
        })
    }

    calculateNetWorth = () => {
        const networth = this.state.walletCoins.reduce((sum, coin) => {
            return sum + (coin.amount * coin.currentValue);
        }, 0);
        this.setState({
            networth: networth
        })
    }

    deleteCoin = (walletId, walletCoinId) => {
        (async () => {
            await WalletCoinService.deleteCoin(walletId, walletCoinId);
        this.refreshWalletCoins();
        })();
    }

    /*editCoin = () => {
        (async () => {
            await WalletCoinService.editCoin(coinId, coinName, buyDate, valueWhenBought, currentValue, amount, walletCoinId);
        this.refreshWalletCoins();
        })();
    }*/

    applyUSDFormat = (data) => {
        return Intl.NumberFormat(
            'en-US',
            {style: 'currency',
            currency: 'USD',})
            .format(data);
    }
    

    renderUserCoinsTable() {
        const { classes } = this.props;
        return this.state.walletCoins.map((coin, index) => {
            const { coinId, coinName, buyDate, valueWhenBought, currentValue, amount, walletCoinId } = coin
            return (
                <tr>
                    <td className="td-left">{coinName}</td>
                    <td className="td-center">{amount}</td>
                    <td className="td-right">{this.applyUSDFormat(valueWhenBought)}</td>
                    <td className="td-right">{this.applyUSDFormat(currentValue)}</td>
                    <td className="td-right">{(Math.round(((currentValue/valueWhenBought)-1)*100000)/1000).toFixed(2) + " %"}</td>
                    <td className="td-right">{this.applyUSDFormat(((amount * currentValue) - (amount * valueWhenBought)).toFixed(2))}</td>
                    <td className="td-right">{this.applyUSDFormat((amount * currentValue).toFixed(2))}</td>
                    <td><EditIcon className={classes.EditIcon} color="action" onClick={() => {this.showEditCoinModal()}}/></td>
                    <td><DeleteOutlineIcon className={classes.DeleteOutlineIcon} color="action" onClick={() => {this.deleteCoin(this.state.walletId, walletCoinId)}}/></td>
                </tr>
            )
        })
    }

    render() {

        return (
            <div className="wrapper">
                <div className="main">
                    <header className="header">
                        <div className="header-wrapper">
                            <div className="button-wrapper">
                                <Button className="add-coin-button" handleClick={this.showAddCoinModal} label="Add new coin" type="secundary"/>
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
                                <h1>{ this.applyUSDFormat(this.state.networth)}</h1>
                            </div>
                        </div>
                    </header>
                    <div className="wallet">
                        <table className="wallet-table">
                            <thead className="wallet-table-header">
                                <tr>
                                    <th className="th-left">Coin</th>
                                    <th className="th-center">Quantity</th>
                                    <th className="th-right">Buy value</th>
                                    <th className="th-right">Current value</th>
                                    <th className="th-right">Change</th>
                                    <th className="th-right">Gain</th>
                                    <th className="th-right">Total worth</th>
                                    <th colspan="2">Actions</th>
                                </tr>
                            </thead>
                            <tbody className="wallet-table-body">
                                {this.renderUserCoinsTable()}
                            </tbody>
                        </table>
                    </div>
                </div>
                <Modal className="modal" isOpen={this.state.addCoinModalIsOpen}>
                    <AddCoinForm walletid={this.state.walletId} showAddCoinModal={this.showAddCoinModal} hideAddCoinModal={this.hideAddCoinModal} refreshWalletCoins={this.refreshWalletCoins}/>
                </Modal>
                <Modal className="modal" isOpen={this.state.editCoinModalIsOpen}>
                    <EditCoinForm walletid={this.state.walletId} showEditCoinModal={this.showEditCoinModal} hideEditCoinModal={this.hideEditCoinModal} refreshWalletCoins={this.refreshWalletCoins}/>
                </Modal>
            </div>
        )
    }
}

export default withStyles(styles)(Wallet);