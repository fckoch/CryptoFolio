import React, { Component } from 'react';
import { NavLink } from "react-router-dom";
import { logedOut, logedIn  } from "./MenuItems";
import AuthService from '../../services/authenticationService.js'
import './Navbar.css';

class Navbar extends Component {
    constructor(props) {
        super();
        this.state = {
            clicked: false
        }
    }

    handleClick = () => {
        this.setState({ 
            clicked: !this.state.clicked 
        });
    }

    componentDidMount() {
        const user = AuthService.getTokenData();
        if (user) {
            this.props.onUserChange('signin');
        }
    }

    onSignOutClick = (e) => {
        AuthService.logout();
        this.props.onUserChange('signout');
    }

    render() {
        let menutype;
        this.props.isSignedIn === true ? menutype = logedIn : menutype = logedOut;

        return (
            <nav className="NavbarItems">
                <h1 className="navbar-logo">CryptoFolio</h1>
                <div className="menu-icon" onClick={this.handleClick}>
                    <i className={this.state.clicked ? 'fas fa-times' : 'fas fa-bars'}></i>
                </div>
                <ul className={this.state.clicked ? 'nav-menu active' : 'nav-menu'}>
                    {menutype.map((item, index) => {
                        return (
                            <li key={index}>
                                {item.title === 'Sign out' ?

                                <NavLink className={item.cName} to={item.url} onClick={this.onSignOutClick}>
                                {item.title}
                                </NavLink>

                                : 

                                <NavLink className={item.cName} to={item.url}>
                                {item.title}
                                </NavLink>}
                            </li>
                        )
                    })}
                </ul>
            </nav>
        )
    }
}

export default Navbar;