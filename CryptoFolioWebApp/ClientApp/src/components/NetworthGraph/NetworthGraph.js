import React, { Component } from 'react';
import './NetworthGraph.css';
import NetworthService from '../../services/networthService.js';
import AuthService from '../../services/authenticationService.js';
import Highcharts from 'highcharts'
import HighchartsReact from 'highcharts-react-official'

const options = {
    title: {
      text: 'My chart'
    },
    series: [{
      data: [1, 2, 3]
    }]
  }

class NetworthGraph extends Component {
    constructor(props) {
        super();
        this.state = {
            walletId: '',
            chartOptions: {
                title: {
                    text: 'Networth'
                  },
                xAxis: {
                    type: 'datetime'
                },
                series: [],
            }
        }
    }

    componentDidMount() {
        const {certserialnumber} = AuthService.getTokenData();

        let seriesData = [];
        let data;

        (async () => { 
            const response = await NetworthService.getNetworthData(certserialnumber);
            for (data of response.data) {
                seriesData.push([Date.parse(data.date.slice(0,10)), data.networthValue]);
            }

            this.setState({
                chartOptions: {
                    series: [{ data: seriesData, name: 'Networth'}]
                }
            })
        })();

    }

    render() {
        return (
            <div>
                <HighchartsReact
                    highcharts={Highcharts}
                    options={this.state.chartOptions}
                />
            </div>
        )
    }
}

export default NetworthGraph;