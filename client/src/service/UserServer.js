import $api from "../assets/utils/axios/index";
export default class AuthService {
    static async login(email, password) {
        return $api.post('/oauth/login', { email, password });
    }
    static async registration(username, email, password) {
        return $api.post("/oauth/register", { username, email, password });
    }
    static async logout() {
        return $api.post('/oauth/logout');
    }
    static async refresh() {
        return $api.post("/oauth/refresh");
    }
}
