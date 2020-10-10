import axios from "axios";

class NetworthService {
    getNetworthData(walletId) {
        const API_URL = `https://localhost:5001/api/networth/${walletId}`
        return axios.get(API_URL);
    }
}

export default new NetworthService;