import React, { Component } from 'react';
import './NetworthGraph.css';
import NetworthService from '../../services/networthService.js';
import AuthService from '../../services/authenticationService.js';

class NetworthGraph extends Component {
    constructor(props) {
        super();
        this.state = {
            XValues: [],
            YValues: [],
            walletId: ''
        }
    }

    componentDidMount() {
        const {certserialnumber} = AuthService.getTokenData();

        let XValues = [];
        let YValues = [];
        let data;

        (async () => { 
            const response = await NetworthService.getNetworthData(certserialnumber);
            for (data of response.data) {
                XValues.push(data.date);
                YValues.push(data.networthValue);
            }

            this.setState({
                XValues: XValues,
                YValues: YValues
            })
        })();

    }

    render() {
        return (
            <div>
                <h1>Graphs</h1>
                <h1>Graphs2</h1>
                <p>{this.state.XValues}</p>
            </div>
        )
    }
}

export default NetworthGraph;