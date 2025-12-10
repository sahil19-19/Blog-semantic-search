import $api from "../assets/utils/axios/index";
export default class SettingService {
    static async ChangeImg(imageUri) {
        return $api.post('/accounts/my/image', imageUri);
    }
    static async ChangePassword(password) {
        return $api.put('/accounts/change-password', { password });
    }
    static async ChangeName(username) {
        return $api.put('/accounts/my', { username });
    }
}
