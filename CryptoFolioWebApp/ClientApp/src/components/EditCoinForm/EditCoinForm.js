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
import './EditCoinForm.css';

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

class EditCoinForm extends Component {
    constructor(props) {
        super()

        this.state = {
            coinList: [],
            walletId: props.walletid,
            editTargetCoinId: props.editTargetCoinId,
            editTargetCoinName: props.editTargetCoinName,
            editTargetBuyDate: props.editTargetBuyDate,
            editTargetValueWhenBought: props.editTargetValueWhenBought,
            editTargetCurrentValue: props.editTargetCurrentValue,
            editTargetAmount: props.editTargetAmount,
            editTargetWalletCoinId: props.editTargetWalletCoinId,
            editTargetAmountError: '',
            editTargetAmountErrorMessage: ''
        }
    }

    componentDidMount() {
        (async () => {
            try {
                let coinList = [];
                let coin;
                const response = await CoinService.getCoinList();
                for (coin of response.data) {
                        coinList.push(coin.coinName);
                    }
                this.setState({
                    coinList: coinList,
                })
            } 
            catch (error) {
                console.log('There was an error!', error)
            }
        })();
    }

    onChangeCoinName = (event, values) => {
        this.setState({
            editTargetCoinName: values
        })
    }

    onChangePrice = (e) => {
        this.setState({
            editTargetValueWhenBought: e.target.value
        })
    }

    onChangeAmount = (e) => {
        this.setState({
            editTargetAmount: e.target.value
        })
    }

    validate = () => {
        let isError = false;
        if (this.state.editTargetAmount.length < 1 ){
            isError = true
            this.setState({
                editTargetAmountError: true,
                editTargetAmountErrorMessage: 'Please enter a valid number'
            })
        }
        return isError;
    }  

    onSubmit = () => {

        this.setState({
            editTargetAmountError: false,
            editTargetAmountErrorMessage: '',
        });

        const err = this.validate();

        if (!err) {
            (async () => {
                try {
                    const response = await WalletCoinService.editCoin(
                        this.state.editTargetCoinId,
                        this.state.editTargetCoinName,
                        this.state.editTargetBuyDate,
                        this.state.editTargetValueWhenBought,
                        this.state.editTargetCurrentValue,
                        this.state.editTargetAmount,
                        this.state.walletId,
                        this.state.editTargetWalletCoinId
                    );
                    //Clean form and close
                    if (response.status === 200) {
                        this.setState({
                            editTargetCoinId: '',
                            editTargetCoinName: '',
                            editTargetBuyDate: '',
                            editTargetValueWhenBought: '',
                            editTargetCurrentValue: '',
                            editTargetAmount: ''
                        })
                        this.props.hideEditCoinModal();
                        this.props.refreshWalletCoins();
                    }
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
                const response = await CoinService.getCoinData(this.state.editTargetCoinName);
                this.setState({
                    editTargetValueWhenBought: response.data.currentValue,
                    editTargetCoinId: response.data.coinId
                })
            } catch (error) {
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
                        value={this.state.editTargetCoinName}
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
                        value={this.state.editTargetValueWhenBought}
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
                    value={this.state.editTargetAmount}
                    error={this.state.editTargetAmountError}
                    helperText={this.state.editTargetAmountErrorMessage}
                    />
                </div>
                <div className="buttons-wrap">
                    <Button handleClick={this.props.hideEditCoinModal} label="Cancel" type="secundary"/>
                    <Button handleClick={this.onSubmit} label="Submit changes" type="secundary"/>
                </div>
            </div>
        );
    }
}

export default withStyles(styles)(EditCoinForm);