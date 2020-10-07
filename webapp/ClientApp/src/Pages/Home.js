import React from 'react';
import TopCoinTable from "../components/TopCoinTable/TopCoinTable.js";
import Button from "../components/Button/Button.js";
import { Link } from "react-router-dom";

const Home = () => {
    return (
        <div>
            <TopCoinTable/>
            <div>
                <Link to="/coins">
                    <Button label="View more coins" type="primary"/>
                </Link>
            </div>
        </div>
    );
}

export default Home;