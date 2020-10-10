import React from 'react';
import Wallet from "../components/Wallet/Wallet.js";
import NetworthGraph from "../components/NetworthGraph/NetworthGraph.js";

const UserPage = () => {
    return (
        <div>
            <Wallet/>
            <NetworthGraph/>
        </div>
    )
}

export default UserPage;