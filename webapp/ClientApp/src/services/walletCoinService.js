import axios from "axios";

class WalletCoinService {
    addNewCoin(walletid, coinid, valuewhenbought) {
        const API_URL = `https://localhost:5001/api/wallets/${walletid}/walletcoins`
        const buydate = (new Date()).toLocaleString("en-US")
        return axios.post(API_URL, {
            coinid,
            buydate,
            valuewhenbought
        });
    }
}

export default new WalletCoinService;
