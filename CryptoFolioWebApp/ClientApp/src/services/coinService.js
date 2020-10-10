import axios from "axios";

const API_URL = "https://localhost:5001/api/coins";

class CoinService {
    getCoinData(coinName) {
        return axios.post(API_URL, {
            coinName
        });
    }
}

export default new CoinService;