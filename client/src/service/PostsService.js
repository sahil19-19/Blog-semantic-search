import $api from "../assets/utils/axios/index";
export default class PostService {
    static async fetchCategory() {
        return $api.get('/topics');
    }
    static async GetPosts(number = 0) {
        return $api.get(`/posts/?page=${number}`);
    }
    static async GetSinglePosts(postId) {
        return $api.get(`/posts/${postId}`);
    }
    static async CreatePost(title, description, topics) {
        return $api.post('/posts', { title, description, topics });
    }
    static async CreatePostImage(id, imageUri) {
        return $api.post(`/posts/${id}/image`, imageUri);
    }
    static async SearchCategoryPosts(id, type, number) {
        return $api.get(`/posts/?${type}=${id}&page=${number}`);
    }
}
