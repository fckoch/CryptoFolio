import React, { Component } from 'react';
import Pagination from '../Pagination/Pagination.js';
import CoinService from '../../services/coinService.js';
import './CoinTable.css';

class CoinTable extends Component {
    constructor(props) {
        super()
        this.state = {
            coins: [],
            totalResults: 0,
            currentPage: 0,
            totalPages: 0,
            pageSize: 0
        }
    }

    componentDidMount() {
        (async () => {
            try {
                const response = await CoinService.getCoinPageData(1,100);
                this.setState({
                    coins: response.data.coins,
                    totalResults: response.data.TotalResults,
                    currentPage: response.data.currentPage,
                    totalPages: response.data.totalPages,
                    pageSize: response.data.pageSize
                })
            }
            catch (error) {
                console.log('There was an error!', error)
            }
        })();
    } 

    nextPage = (pageNumber) => {
        (async () => {
            try {
                const response = await CoinService.getCoinPageData(pageNumber, 100);
                this.setState({
                    coins: response.data.coins,
                    totalResults: response.data.TotalResults,
                    currentPage: response.data.currentPage,
                    totalPages: response.data.totalPages,
                    pageSize: response.data.pageSize
                })
            } 
            catch (error) {
                console.log('There was an error!', error)
            }
        })();
    }

    renderCoinTableHeader() {
        return (
            <tr>
                <th className="th-left">Rank</th>
                <th className="th-left">Symbol</th>
                <th className="th-left">Cryptocurrency</th>
                <th className="th-right">Market Cap</th>
                <th className="th-right">Price</th>
                <th className="th-right">24h change</th>
            </tr>  
        )
    }

    applyUSDFormat = (data) => {
        return Intl.NumberFormat(
            'en-US',
            {style: 'currency',
            currency: 'USD',})
            .format(data);
    }

    renderCoinTableBody() {
        return this.state.coins.map((coin, index) => {
            const { coinName, symbol, currentValue, priceChangePct, marketCap } = coin 
            return (
                <tr>
                    <td className="td-left">{ this.state.currentPage * this.state.pageSize + index + 1 - 100 }</td>
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
            <div className="container">
                <table className="coin-table">
                    <thead>
                        {this.renderCoinTableHeader()}
                    </thead>
                    <tbody>
                        {this.renderCoinTableBody()}
                    </tbody>
                </table>
                <Pagination currentPage={this.state.currentPage} totalPages={this.state.totalPages} nextPage={this.nextPage}/>
            </div>
        )
    }
}

export default CoinTable;