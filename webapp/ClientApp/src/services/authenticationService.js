import axios from "axios";
import https from 'https';

const API_URL = "http://localhost:5001/api/users";
const httpsAgent = new https.Agent({ rejectUnauthorized: false });

class AuthService {
    register(firstname, lastname, email, password) {
        return axios.post(API_URL, {
            firstname,
            lastname,
            email,
            password
        }, httpsAgent);
    }
}

export default new AuthService;