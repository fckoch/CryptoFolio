import React, { Component } from 'react';
import './CoinTable.css';
import Pagination from "../Pagination/Pagination.js";

class CoinTable extends Component {
    constructor(props) {
        super()
        this.state = {
            totalReactPackages: null,
            errorMessage: null,
            coins: [],
            totalResults: 0,
            currentPage: 0,
            totalPages: 0,
            pageSize: 0
        }
    }

    componentDidMount() {
        fetch('https://localhost:5001/api/coins?pageNumber=1&pageSize=100')
            .then(async response => {
                const data = await response.json();
                this.setState({coins: [...data.coins], pageSize: data.pageSize, currentPage: data.currentPage, totalPages: data.totalPages});
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


    nextPage = (pageNumber) => {
            fetch(`https://localhost:5001/api/coins?pageNumber=${pageNumber}&pageSize=100`)
                .then(async response => {
                    const data = await response.json();
                    this.setState({coins: [...data.coins], currentPage: pageNumber})
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

    renderCoinTableBody() {
        return this.state.coins.map((coin, index) => {
            const { coinName, symbol, currentValue, priceChangePct, marketCap } = coin 
            return (
                <tr>
                    <td className="td-left">{ this.state.currentPage * this.state.pageSize + index + 1 - 100 }</td>
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