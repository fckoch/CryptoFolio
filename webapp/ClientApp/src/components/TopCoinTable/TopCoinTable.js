import React, { Component } from 'react';
import './TopCoinTable.css';

class TopCoinTable extends Component {
    constructor(props) {
        super();
        this.state = {
            totalReactPackages: null,
            errorMessage: null,
            coins: []
        }
    }

    componentDidMount() {
        fetch('https://localhost:5001/api/coins?pageNumber=1&pageSize=10')
            .then(async response => {
                const data = await response.json();
                this.setState({coins: [...data.coins]});
                console.log(data);
            if (!response.ok) {
                const error = (data && data.message) || response.statusText;
                return Promise.reject(error);
            }

            this.setState({ totalReactPackages: data.total })
            })
            .catch(error => {
                this.setState({ errorMessage: error.toString() });
                console.error('There was an error!', error);
            });
    }

    renderTopCoinTableHeader() {
        return (
            <tr>
                <th className="th-left">Rank</th>
                <th colspan="2" className="th-center">Symbol</th>
                <th className="th-left">Cryptocurrency</th>
                <th className="th-right">Market Cap</th>
                <th className="th-right">Price</th>
                <th className="th-right">24h change</th>
            </tr>  
        )
    }

    renderTopCoinTable() {
        return this.state.coins.map((coin, index) => {
            const { coinName, symbol, currentValue, priceChangePct, marketCap } = coin 
            return (
                <tr>
                    <td className="td-left">{ index + 1}</td>
                    <td className="td-coin-icon"><img className="coin-icon-img" src={require(`./Icons/${symbol}.png`)}/></td>
                    <td className="td-left">{ symbol }</td>
                    <td className="td-left">{ coinName }</td>
                    <td className="td-right">{ Intl.NumberFormat(
                    'en-US',
                    {style: 'currency',
                    currency: 'USD',})
                    .format(marketCap) }</td>
                    <td className="td-right">{ Intl.NumberFormat(
                    'en-US', 
                    {style: 'currency',
                    currency: 'USD',})
                    .format(currentValue) }</td>
                    <td className={priceChangePct > 0 ? "green-pct-change" : "red-pct-change"}>{ (Math.round(priceChangePct*100000)/1000).toFixed(2) + " %"}</td>
                </tr>
            )
        })
    }

    render () {
        return (
            <div className="container-top-coin">
                <table className="coin-table">
                    <thead>
                        {this.renderTopCoinTableHeader()}
                    </thead>
                    <tbody>
                        {this.renderTopCoinTable()}
                    </tbody>
                </table>
            </div>
        )
    }
}

export default TopCoinTable;