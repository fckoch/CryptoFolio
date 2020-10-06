import React, { Component } from 'react';
import Autocomplete from '@material-ui/lab/Autocomplete'; 
import TextField from '@material-ui/core/TextField';
import AppBar from '@material-ui/core/AppBar';  
import Toolbar from '@material-ui/core/Toolbar'; 
import axios from 'axios'; 
import './CoinForm.css';
import Button from '@material-ui/core/Button';

class CoinForm extends Component {
    constructor(props) {
        super()

        this.state = {
            coinList: [],
            loading: true
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
                //loading: false
            })
        })();
    }

    testbutton = () => {
        console.log(this.state.coinList);
    }

    render () {
        return (
            <div className="modal-wrapper">
                <h1>Modal Title</h1>
                <div className="autocomplete-wrapper">
                    <Autocomplete   
                        id="combo-box-demo"
                        options={this.state.coinList}
                        style={{ width: 300 }}
                        renderInput={(params) => <TextField {...params} label="Enter coin" variant="outlined" />}
                        loading={this.state.loading}
                        loadingText='Loading...'
                    />
                    <p>Stuff</p>
                </div>
                <button onClick={this.props.hideModal}>Close modal</button>
                <button onClick={this.testbutton}>test</button>
            </div>
        );
    }
}

export default CoinForm;