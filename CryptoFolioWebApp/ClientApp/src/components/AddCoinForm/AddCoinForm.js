import React, { Component } from 'react';
import Autocomplete from '@material-ui/lab/Autocomplete'; 
import TextField from '@material-ui/core/TextField';
import Button from "../Button/Button.js";
import FormControl from '@material-ui/core/FormControl';
import InputLabel from '@material-ui/core/InputLabel';
import OutlinedInput from '@material-ui/core/OutlinedInput';
import InputAdornment from '@material-ui/core/InputAdornment';
import AutorenewIcon from '@material-ui/icons/Autorenew';
import CoinService from '../../services/coinService.js'
import WalletCoinService from '../../services/walletCoinService.js'
import { withStyles } from '@material-ui/core/styles';
import './AddCoinForm.css';

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

class AddCoinForm extends Component {
    constructor(props) {
        super()

        this.state = {
            coinList: [],
            walletId: props.walletid,
            coinName: '',
            coinId: '',
            buyPrice: '',
            amount: '',
            amountError: '',
            amountErrorMessage: ''
        }
    }

    componentDidMount() {
        let coinList = [];
        let coin;

        (async () => {
            try {
                const response = await CoinService.getCoinList();
                for (coin of response.data) {
                        coinList.push(coin.coinName);
                    }
                this.setState({
                    coinList: coinList
                })
            } 
            catch (error) {
                console.log('There was an error!', error)
            }
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

    onChangeAmount = (e) => {
        this.setState({
            amount: e.target.value
        })
    }

    validate = () => {
        let isError = false;
        if (this.state.amount.length < 1) {
            isError = true
            this.setState({
                amountError: true,
                amountErrorMessage: 'Please enter a valid number'
            })
        return isError;
        }
    }   

    onSubmit = () => {

        //Clear errors
        this.setState({
            amountError: false,
            amountErrorMessage: '',
        });

        const err = this.validate();

        if (!err) {

            (async () => {
                try {
                    const response = await WalletCoinService.addNewCoin(
                        this.state.walletId,
                        this.state.coinId,
                        this.state.buyPrice,
                        this.state.amount
                    );
                    //Clean form and close
                    if (response.status === 201) {
                        this.setState({
                            coinName: '',
                            coinId: '',
                            buyPrice: '',
                            quantity: '',
                        });
                    }
                    this.props.hideAddCoinModal();
                    this.props.refreshWalletCoins(); 
                } 
                catch (error) {
                    console.log('There was an error!', error)
                }
            })();
        }
    }

    updateCoinData = () => {
        (async () => {
            try {
                const response = await CoinService.getCoinData(this.state.coinName);
                this.setState({
                    buyPrice: response.data.currentValue,
                    coinId: response.data.coinId
                })
            } 
            catch (error) {
                console.log('There was an error!', error)
            }
        })();
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
                    onChange={this.onChangeAmount}
                    value={this.state.amount}
                    error={this.state.amountError}
                    helperText={this.state.amountErrorMessage}
                    />
                </div>
                <div className="buttons-wrap">
                    <Button handleClick={this.props.hideAddCoinModal} label="Cancel" type="secundary"/>
                    <Button handleClick={this.onSubmit} label="Submit" type="secundary"/>
                </div>
            </div>
        );
    }
}

export default withStyles(styles)(AddCoinForm);