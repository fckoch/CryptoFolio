import React, { Component } from 'react';
import TopCoinTable from "../components/TopCoinTable/TopCoinTable.js";
import ViewMoreButton from "../components/ViewMoreButton/ViewMoreButton.js";

const Home = () => {
    return (
        <div>
            <TopCoinTable/>
            <ViewMoreButton/>
        </div>
    );
}

export default Home;