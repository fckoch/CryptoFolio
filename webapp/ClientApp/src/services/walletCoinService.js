import axios from "axios";

class WalletCoinService {
    addNewCoin(walletid, coinid, valuewhenbought, amount) {
        const API_URL = `https://localhost:5001/api/wallets/${walletid}/walletcoins`
        const buydate = (new Date()).toLocaleString("en-US")
        return axios.post(API_URL, {
            coinid,
            buydate,
            valuewhenbought,
            amount
        });
    }
    deleteCoin(walletid, walletcoinid) {
        const API_URL = `https://localhost:5001/api/wallets/${walletid}/walletcoins/${walletcoinid}`
        return axios.delete(API_URL);
    }
}

export default new WalletCoinService;
