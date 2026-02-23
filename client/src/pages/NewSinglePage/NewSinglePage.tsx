import { useParams } from "react-router-dom";
import { useEffect, useState } from "react";
import s from "./NewSinglePage.module.scss";
import PostsService from "@/service/PostsService";
import { IPost } from "@/models/IPost";

const NewSinglePage = () => {
  const { id: postId } = useParams();

  const [post, setPost] = useState<IPost | null>(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

    useEffect(() => {
    if (!postId) return;

    const fetchPost = async () => {
      try {
        setError(null);
        const res = await PostsService.GetSinglePosts(postId);
        console.log("Fetched post data:", res.data);
        setPost(res.data.result.posts[0]);
      } catch (err) {
        const errorMessage = err instanceof Error ? err.message : "Failed to fetch post";
        console.error("Failed to fetch post", err);
        setError(errorMessage);
      } finally {
        setLoading(false);
      }
    };

    fetchPost();
  }, [postId]);

  if (loading) return <div style={{ padding: 60 }}>Loading...</div>;
  if (error) return <div style={{ padding: 60, color: "red" }}>Error: {error}</div>;
  if (!post) return <div style={{ padding: 60 }}>Post not found</div>;

  return (
    <div className={s.page}>
      <div className={s.container}>
        <h1 className={s.title}>{post.title}</h1>

        {post.author && (
          <div className={s.metaRow}>
            {post.author.imageUri && (
              <img src={post.author.imageUri} alt={post.author.name} />
            )}
            <span>{post.author.name}</span>
            <span>•</span>
            <span>{new Date(post.dateCreated).toDateString()}</span>
          </div>
        )}

        {post.imageUri && (
          <img src={post.imageUri} className={s.heroImage} alt="hero" />
        )}

        <div
          className={s.content}
          dangerouslySetInnerHTML={{ __html: post.description }}
        />
      </div>
    </div>
  );
};

export default NewSinglePage;
