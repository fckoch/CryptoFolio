import React, { Component } from 'react';
import './App.css';
import {Route, BrowserRouter as Router, Switch} from "react-router-dom";
import Home from "./Pages/Home.js";
import Coins from "./Pages/Coins.js";
import Navbar from "./components/Navbar/Navbar.js";


class App extends Component {
    render() {
        return (
            <Router>
                <div>
                    <Navbar/>
                    <Switch>
                        <Route path="/" exact component={Home}/>
                        <Route path="/coins" component={Coins}/>
                    </Switch>
                </div>
            </Router>
        );
    }
}

export default App;