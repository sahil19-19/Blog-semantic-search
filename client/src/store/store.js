import { makeAutoObservable } from 'mobx';
import AuthService from "../service/UserServer";
import SettingService from "../service/SettingService";
import PostService from "../service/PostsService";
export default class Store {
    constructor() {
        Object.defineProperty(this, "user", {
            enumerable: true,
            configurable: true,
            writable: true,
            value: {}
        });
        Object.defineProperty(this, "posts", {
            enumerable: true,
            configurable: true,
            writable: true,
            value: []
        });
        Object.defineProperty(this, "isAuth", {
            enumerable: true,
            configurable: true,
            writable: true,
            value: false
        });
        Object.defineProperty(this, "loading", {
            enumerable: true,
            configurable: true,
            writable: true,
            value: false
        });
        Object.defineProperty(this, "end", {
            enumerable: true,
            configurable: true,
            writable: true,
            value: false
        });
        Object.defineProperty(this, "active", {
            enumerable: true,
            configurable: true,
            writable: true,
            value: false
        });
        Object.defineProperty(this, "redirectCallback", {
            enumerable: true,
            configurable: true,
            writable: true,
            value: null
        });
        makeAutoObservable(this);
    }
    setAuth(bool) {
        this.isAuth = bool;
    }
    setUser(user) {
        this.user = user;
    }
    setPost(posts) {
        this.posts = posts;
    }
    setEndPosts(end) {
        this.end = end;
    }
    setLoading(loading) {
        this.loading = loading;
    }
    setActiveSidebar(active) {
        this.active = active;
    }
    setRedirectCallback(callback) {
        this.redirectCallback = callback;
    }
    async login(email, password) {
        try {
            const response = await AuthService.login(email, password);
            localStorage.setItem('token', response.data.result.accessToken);
            this.setAuth(true);
            this.setUser(response.data.result.user);
            if (this.redirectCallback) {
                this.redirectCallback('/');
            }
        }
        catch (error) {
            console.log(error);
        }
    }
    async registration(username, email, password) {
        try {
            const response = await AuthService.registration(username, email, password);
            localStorage.setItem('token', response.data.result.accessToken);
            this.setAuth(true);
            this.setUser(response.data.result.user);
            if (this.redirectCallback) {
                this.redirectCallback('/');
            }
        }
        catch (error) {
            console.log(error);
        }
    }
    async logout() {
        try {
            const response = await AuthService.logout();
            localStorage.removeItem('token');
            this.setAuth(false);
            this.setUser({});
        }
        catch (error) {
            console.log(error);
        }
    }
    async checkAuth() {
        try {
            const response = await AuthService.refresh();
            localStorage.setItem('token', response.data.result.accessToken);
            this.setAuth(true);
            this.setUser(response.data.result.user);
        }
        catch (error) {
            console.log(error);
        }
    }
    async ChangeImg(imageUri) {
        try {
            const formData = new FormData();
            formData.append('image', imageUri);
            const response = await SettingService.ChangeImg(formData);
            this.setUser(response.data.result.user);
        }
        catch (error) {
            console.log(error);
        }
    }
    async ChangePassword(password) {
        try {
            const response = await SettingService.ChangePassword(password);
            this.setUser(response.data.result.user);
        }
        catch (error) {
            console.log(error);
        }
    }
    async ChangeName(username) {
        try {
            const response = await SettingService.ChangeName(username);
            this.setUser(response.data.result.user);
        }
        catch (error) {
            console.log(error);
        }
    }
    async CreatePost(title, description, topics, imageUri) {
        try {
            const responsePost = await PostService.CreatePost(title, description, topics);
            const formData = new FormData();
            formData.append('image', imageUri);
            const responsePostImg = await PostService.CreatePostImage(responsePost.data.result.post.id, formData);
            this.setPost([...this.posts, responsePostImg.data.result.post]);
        }
        catch (error) {
            console.log(error);
        }
    }
    CheckLastReq(posts) {
        if (posts.length == 0 || posts.length % 3 !== 0) {
            this.setEndPosts(true);
        }
        else {
            this.setEndPosts(false);
        }
    }
    async GetPosts(number) {
        try {
            this.setLoading(this.loading = true);
            const response = await PostService.GetPosts(number);
            const newPosts = [...this.posts, ...response.data.result.posts];
            this.setPost(newPosts);
            this.setLoading(this.loading = false);
            this.CheckLastReq(response.data.result.posts);
        }
        catch (error) {
            console.log(error);
        }
    }
    async GetSearchPosts(id, type, number) {
        try {
            this.setLoading(this.loading = true);
            const response = await PostService.SearchCategoryPosts(id, type, number);
            const newPosts = [...this.posts, ...response.data.result.posts];
            this.setPost(newPosts);
            this.setLoading(this.loading = false);
            this.CheckLastReq(response.data.result.posts);
        }
        catch (error) {
            console.log(error);
        }
    }
    async ClearPosts() {
        try {
            this.setPost([]);
        }
        catch (error) {
            console.log(error);
        }
    }
}
