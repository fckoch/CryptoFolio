import axios from "axios";

const API_URL = "https://localhost:5001/api/users";

class UserService {
    getUserData(nameid) {
        return axios.get(API_URL + `/${nameid}`);
    }
    getUserWalletcoins(userId) {
        return axios.get(API_URL + `/${userId}`)
    }
}

export default new UserService;