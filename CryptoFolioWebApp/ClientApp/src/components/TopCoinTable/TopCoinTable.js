import React, { Component } from 'react';
import CoinService from '../../services/coinService.js';
import './TopCoinTable.css';

class TopCoinTable extends Component {
    constructor(props) {
        super();
        this.state = {
            coins: []
        }
    }

    componentDidMount() {
        (async () => {
            try {
                const response = await CoinService.getCoinPageData(1,10);
                this.setState({
                    coins: response.data.coins,
                })
            }
            catch (error) {
                console.log('There was an error!', error)
            }
        })();
    }

    applyUSDFormat = (data) => {
        return Intl.NumberFormat(
            'en-US',
            {style: 'currency',
            currency: 'USD',})
            .format(data);
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
                    <td className="td-right">{ this.applyUSDFormat(marketCap) }</td>
                    <td className="td-right">{ this.applyUSDFormat(currentValue) }</td>
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