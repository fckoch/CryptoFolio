import React, { Component } from 'react';
import './NetworthGraph.css';
import NetworthService from '../../services/networthService.js';
import AuthService from '../../services/authenticationService.js';
import Highcharts from 'highcharts'
import HighchartsReact from 'highcharts-react-official'
import NoDataToDisplay from 'highcharts/modules/no-data-to-display';

NoDataToDisplay(Highcharts);

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
            networth: props.networth,
            chartOptions: {
                title: {
                    text: 'Wallet Networth graph'
                  },
                xAxis: {
                    type: 'datetime'
                },
                series: [{
                    data : [],
                    type: 'line',
                    name: 'Networth'
                }],
                yAxis: {
                    labels: {
                      formatter: function() {
                        if (this.value >= 0) {
                          return '$' + this.value
                        } else {
                          return '-$' + (-this.value)
                        }
                      }
                    },
                    title: {
                        text: 'Networth'
                    }
                },
                tooltip: {
                    pointFormatter: function() {
                      var value;
                      if (this.y >= 0) {
                        value = '$ ' + this.y
                      } else {
                        value = '-$ ' + (-this.y)
                      }
                      return '<span style="color:' + this.series.color + '">' + this.series.name + '</span>: <b>' + value + '</b><br />'
                    },
                    shared: true
                },
                lang: {
                    noData: "No data to display yet, need at least two sets of data."
                },
                noData: {
                    style: {
                        fontWeight: 'bold',
                        fontSize: '15px',
                        color: '#303030'
                    }
                },
                credits: {
                    enabled: true,
                    text: "CryptoFolio"
                }
            }
        }   
    }

    updateGraphic = () => {
        //Get wallet id by JWT token - certserialnumber = walletId
        const {certserialnumber} = AuthService.getTokenData();
      
        let seriesData = [];
        let data;

        (async () => { 

            try {
                const response = await NetworthService.getNetworthData(certserialnumber);
                if (response.data.length > 0) {
                    for (data of response.data) {
                        seriesData.push([Date.parse(data.date.slice(0,10)), data.networthValue]);
                    }
                    var lastItem = seriesData[seriesData.length-1];
                    //Adds current networth to last point on graphic
                    seriesData[seriesData.length-1] = [lastItem[0], this.props.networth];
                    this.setState({
                        chartOptions: {
                            series: [{ data: seriesData, name: 'Networth'}]
                        }
                    })
                }
            } 
            catch (error) {
                console.log('There was an error!', error)
            }
        })();
    }

    componentDidMount() {
        this.updateGraphic();
    }

    componentDidUpdate(prevProps, prevState) {
        if (this.props.networth !== prevProps.networth)
        {
            this.updateGraphic();
        }
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