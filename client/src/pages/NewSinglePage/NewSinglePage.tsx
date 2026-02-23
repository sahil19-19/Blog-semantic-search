import { Link, useParams } from "react-router-dom";
import logo from '../../assets/img/logo.png';
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
        setPost(res.data.result.post);
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
    <>
      <header>
        <div className={s.container}>
          <div className={s.nav}>
            <Link className={s.brand} to="/demos">
              <img
                src={logo}
                alt="Levelworks Logo"
                className={s.logoImg}
                onError={(e) => {
                  (e.currentTarget.style.display = 'none')
                  const fallback = document.getElementById('fallbackLogo')
                  if (fallback) fallback.style.display = 'block'
                }}
              />

              <span
                className={s.brandMark}
                id="fallbackLogo"
                aria-hidden="true"
                style={{ display: 'none' }}
              >
                <svg viewBox="0 0 24 24" fill="none">
                  <rect x="2.5" y="7" width="3.5" height="12" rx="1.4" fill="#111827" />
                  <rect x="8.0" y="4" width="3.5" height="15" rx="1.4" fill="#111827" opacity="0.55" />
                  <rect x="13.5" y="10" width="3.5" height="9" rx="1.4" fill="#111827" opacity="0.35" />
                  <circle cx="20" cy="18" r="2" fill="#F58B2C" />
                </svg>
              </span>
            </Link>

            <a className={`${s.btn} ${s.btnPrimary}`} href="/">Sign In</a>
          </div>
        </div>
      </header>
      <main className={s.main}>
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
                  <span>4 days ago</span>
                  <span>•</span>
                  <span>3 min read</span>
                  <span>•</span>
                  <span className={s.pill}>{post.topics[0].name}</span>
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
      </main>
    </>
  );
};

export default NewSinglePage;
