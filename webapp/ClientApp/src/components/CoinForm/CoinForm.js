import React, { Component } from 'react';
import Autocomplete from '@material-ui/lab/Autocomplete'; 
import TextField from '@material-ui/core/TextField';
import AppBar from '@material-ui/core/AppBar';  
import Toolbar from '@material-ui/core/Toolbar'; 
import axios from 'axios'; 
import './CoinForm.css';
import Button from "../Button/Button.js";
import FormControl from '@material-ui/core/FormControl';
import InputLabel from '@material-ui/core/InputLabel';
import OutlinedInput from '@material-ui/core/OutlinedInput';
import InputAdornment from '@material-ui/core/InputAdornment';
import AutorenewIcon from '@material-ui/icons/Autorenew';
import CoinService from '../../services/coinService.js'
import WalletCoinService from '../../services/walletCoinService.js'

import { withStyles } from '@material-ui/core/styles';

const styles = theme => ({
    root: {
      display: 'flex',
      flexWrap: 'wrap',
    },
    margin: {
      margin: theme.spacing(1),
    },
    withoutLabel: {
      marginTop: theme.spacing(3),
    },
    textField: {
      width: '25ch',
    },
    AutorenewIcon: {
        '&:hover': {
            cursor: 'pointer'
        }
    }
});

class CoinForm extends Component {
    constructor(props) {
        super()

        this.state = {
            coinList: [],
            loading: true,
            walletId: '',
            coinName: '',
            coinId: '',
            buyPrice: '',
            quantity: '',
        }
    }

    componentDidMount() {
        let coinList = [];
        let coin;

        (async () => {
            const response = await axios.get('https://localhost:5001/api/coins/list')
            for (coin of response.data) {
                coinList.push(coin.coinName);
                    //console.log(response.data[1].coinName)
                }
            this.setState({
                coinList: coinList,
                walletId: this.props.walletid,
            })
        })();
    }

    onChangeCoinName = (event, values) => {
        this.setState({
          coinName: values
        })
    }

    onChangePrice = (e) => {
        this.setState({
            buyPrice: e.target.value
        })
    }

    onChangeQuantity = (e) => {
        this.setState({
            quantity: e.target.value
        })
    }

    onSubmit = () => {
        WalletCoinService.addNewCoin(
            this.props.walletid,
            this.state.coinId,
            this.state.buyPrice
        ).then(response => {
            console.log(response)
            this.setState({
                coinName: '',
                coinId: '',
                buyPrice: '',
                quantity: '',
            })
            this.props.hideModal();
        }).catch(error => {
            console.log(error);
        })
    }

    updateCoinData = () => {
        CoinService.getCoinData(this.state.coinName).then(response => {
            this.setState({
                buyPrice: response.data.currentValue,
                coinId: response.data.coinId
            });
        })
    }

    render () {

        const { classes } = this.props;

        return (
            <div className="modal-wrapper">
                <div className="autocomplete-wrapper">
                    <Autocomplete   
                        id="combo-box-demo"
                        options={this.state.coinList}
                        style={{ width: 300 }}
                        renderInput={(params) => <TextField {...params} label="Enter coin" variant="outlined" />}
                        loading={this.state.loading}
                        loadingText='Loading...'
                        onChange={this.onChangeCoinName}
                        value={this.state.coinName}
                        getOptionSelected= {(
                            option,
                            value,
                         ) => value.value === option.value
                        }
                    />
                </div>
                <div className="price-wrapper">
                    <FormControl fullWidth className={classes.margin} variant="outlined">
                    <InputLabel htmlFor="outlined-adornment-amount">Price</InputLabel>
                    <OutlinedInput
                        id="outlined-adornment-amount"
                        startAdornment={<InputAdornment position="start">$</InputAdornment>}
                        labelWidth={60}
                        style={{ width: 300 }}
                        onChange={this.onChangePrice}
                        value={this.state.buyPrice}
                    />
                    </FormControl>
                    <div>
                        <AutorenewIcon className={classes.AutorenewIcon} color="primary" onClick={() => {this.updateCoinData()}}/> 
                    </div>
                </div>
                <div className="quantity-wrapper">
                    <TextField
                    id="filled-number"
                    label="Quantity"
                    type="number"
                    style={{ width: 300 }}
                    InputLabelProps={{
                    shrink: true,
                    }}
                    variant="outlined"
                    onChange={this.onChangeQuantity}
                    value={this.state.quantity}
                    />
                </div>
                <div className="buttons-wrap">
                    <Button handleClick={this.props.hideModal} label="Cancel" type="secundary"/>
                    <Button handleClick={this.onSubmit} label="Submit" type="secundary"/>
                </div>
            </div>
        );
    }
}

export default withStyles(styles)(CoinForm);