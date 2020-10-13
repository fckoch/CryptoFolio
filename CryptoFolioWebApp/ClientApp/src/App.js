import React, { Component } from 'react';
import {Route, BrowserRouter as Router, Switch, Redirect} from "react-router-dom";
import Home from "./Pages/Home.js";
import Coins from "./Pages/Coins.js";
import Login from "./Pages/Login.js";
import Register from "./Pages/Register.js";
import Navbar from "./components/Navbar/Navbar.js";
import Wallet from "./components/Wallet/Wallet.js";
import './App.css';

class App extends Component {
    constructor() {
        super();
        this.state = {
            isSignedIn: false
        }
    }

    onUserChange = (action) => {
        if (action === 'signout') {
          this.setState({isSignedIn: false})
        } else if (action === 'signin') {
          this.setState({isSignedIn: true})
        }
    }

    SecuredRoute = (props) => {
        return (
            <Route path={props.path} render={data => this.state.isSignedIn ? 
            (<props.component {...data}></props.component>) :
            (<Redirect to={{pathname:'/login'}}></Redirect>)}></Route>
        )
    }

    render() {
        return (
            <Router>
                <div>
                    <Navbar isSignedIn={this.state.isSignedIn} onUserChange={this.onUserChange}/>
                    <Switch>
                        <Route path="/" exact component={Home}/>
                        <Route path="/coins" component={Coins}/>
                        <Route path="/login" render={props => <Login onUserChange={this.onUserChange}/>}/>
                        <Route path="/register" component={Register}/>
                        <this.SecuredRoute path="/wallet" component={Wallet}></this.SecuredRoute>
                    </Switch>
                </div>
            </Router>
        );
    }
}

export default App; 