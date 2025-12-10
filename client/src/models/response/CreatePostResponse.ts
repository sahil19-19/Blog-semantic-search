import { IPost } from "../IPost";

export interface CreatePostResponse {
    result: {
        post: IPost;
    };
}
