import React, { Component } from 'react';
import './ViewMoreButton.css';
import { Link } from "react-router-dom";

const ViewMoreButton = () => {
    return (
        <div className="container-2">
            <Link to="/coins">
                <button className="view-more-button">View more coins</button>
            </Link>
        </div>
    );
}

export default ViewMoreButton;