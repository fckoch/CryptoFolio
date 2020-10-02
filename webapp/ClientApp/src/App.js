import React, { Component } from 'react';
import './App.css';
import {Route, BrowserRouter as Router, Switch} from "react-router-dom";
import Home from "./Pages/Home.js";
import Coins from "./Pages/Coins.js";
import Login from "./Pages/Login.js";
import Register from "./Pages/Register.js";
import Navbar from "./components/Navbar/Navbar.js";
import Wallet from "./components/Wallet/Wallet.js";


class App extends Component {
    render() {
        return (
            <Router>
                <div>
                    <Navbar/>
                    <Switch>
                        <Route path="/" exact component={Home}/>
                        <Route path="/coins" component={Coins}/>
                        <Route path="/login" component={Login}/>
                        <Route path="/register" component={Register}/>
                        <Route path="/wallet" component={Wallet}/>
                    </Switch>
                </div>
            </Router>
        );
    }
}

export default App; 