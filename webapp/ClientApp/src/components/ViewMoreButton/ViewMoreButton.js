import React, { Component } from 'react';
import './ViewMoreButton.css';
import { Link } from "react-router-dom";

class ViewMoreButton extends Component {
    render () {
        return (
            <Link to="/coins">
                <button className="view-more-button">View more coins</button>
            </Link>
        );
    }
}

export default ViewMoreButton;