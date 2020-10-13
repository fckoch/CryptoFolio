import axios from "axios";

const API_URL = "https://localhost:5001/api/coins";

class CoinService {
    getCoinPageData(pageNumber, pageSize) {
        return axios.get(`https://localhost:5001/api/coins?pageNumber=${pageNumber}&pageSize=${pageSize}`)
    }
    getCoinData(coinName) {
        return axios.post(API_URL, {
            coinName
        });
    }
    getCoinList() {
        return axios.get('https://localhost:5001/api/coins/list');
    }
}

export default new CoinService;