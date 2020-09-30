import axios from "axios";

const API_URL = "http://localhost:5001/api/users";

class AuthService {
    register(firstname, lastname, email, password) {
        return axios.post(API_URL, {
            firstname,
            lastname,
            email,
            password
        });
    }
}

export default AuthService;