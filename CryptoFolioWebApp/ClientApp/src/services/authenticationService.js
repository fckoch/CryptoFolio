import axios from "axios";

const API_URL = "https://localhost:5001/api/users";

class AuthService {
    register(firstname, lastname, email, password) {
        return axios.post(API_URL, {
            firstname,
            lastname,
            email,
            password
        });
    }
    login(email, password) {
        return axios.post(API_URL + '/authenticate', {
            email,
            password
        });
    }
    getTokenData() {
        let token = localStorage.getItem('user');
        if (token) {
            let tokenPayload = token.split('.')[1];
            return JSON.parse(window.atob(tokenPayload));
        }
    }
    logout() {
        localStorage.removeItem("user");
    }
}

export default new AuthService;