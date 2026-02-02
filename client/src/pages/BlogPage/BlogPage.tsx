import s from './BlogPage.module.scss';
import logo from '../../assets/img/logo.png';
import { ChangeEvent, useEffect, useState } from 'react';
import { useInView } from 'react-intersection-observer';
import { Link } from 'react-router-dom';
import $api from '@/assets/utils/axios';
import { clearObserving } from 'mobx/dist/internal';

const BlogPage = () => {
  const [query, setQuery] = useState("");
  const [results, setResults] = useState<any[]>([]);
  const [page, setPage] = useState(1);
  const [limit] = useState(10);
  const [loading, setLoading] = useState(false);
  const [hasMore, setHasMore] = useState(true);

  const [semanticRatio, setSemanticRatio] = useState(0.0);
  const [processingTimeMs, setProcessingTimeMs] = useState<number | null>(null);
  const [estimatedTotalHits, setEstimatedTotalHits] = useState<number | null>(null);

  const { ref, inView } = useInView({
    threshold: 0,
  });

  const resetSearchState = () => {
    setPage(1);
    setResults([]);
    setHasMore(true);
    setProcessingTimeMs(null);
    setEstimatedTotalHits(null);
  };

  const handleSliderChange = (e: ChangeEvent<HTMLInputElement>) => {
    const value = parseFloat(e.target.value);
    const rounded = Math.round(value * 10) / 10;
    setSemanticRatio(rounded);
    resetSearchState();
  };

  const handleSearchChange = (value: string) => {
    setQuery(value);
    resetSearchState();
  };

  const handleHomeTabClick = () => {
    setQuery('');
    resetSearchState();
  };

  const fetchResults = async (searchValue: string, pageNum: number) => {
    if (loading) return;

    setLoading(true);

    try {
      const res = await $api.post(
        `/posts/semantic?page=${pageNum}&limit=${limit}&ratio=${semanticRatio}`,
        { search: searchValue }
      );

      const data = res.data;

      const extracted = Array.isArray(data?.result?.hits)
        ? data.result.hits
        : [];

      setResults(prev =>
        pageNum === 1 ? extracted : [...prev, ...extracted]
      );

      setHasMore(extracted.length === limit);

      setProcessingTimeMs(data?.result?.processingTimeMs ?? null);
      setEstimatedTotalHits(data?.result?.estimatedTotalHits ?? null);
    } catch (err) {
      console.error("Semantic Search Error:", err);
    } finally {
      setLoading(false);
    }
  };

  // 🔹 Debounce ONLY search + slider
  useEffect(() => {
    const timer = setTimeout(() => {
      fetchResults(query, 1);
    }, 350);

    return () => clearTimeout(timer);
  }, [query, semanticRatio]);

  // 🔹 Infinite scroll (NO debounce)
  useEffect(() => {
    if (inView && hasMore && !loading) {
      setPage(p => p + 1);
    }
  }, [inView, hasMore, loading]);

  useEffect(() => {
    if (page > 1) {
      fetchResults(query, page);
    }
  }, [page]);


  return (
    <>
      <header>
        <div className={s.container}>
          <div className={s.nav}>
            <Link className={s.brand} to="/">
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

            <div className={s.navSearch}>
              <div className={s.searchBox}>
                <svg viewBox="0 0 24 24" fill="none" aria-hidden="true">
                  <path
                    d="M10.5 18.5c4.418 0 8-3.582 8-8s-3.582-8-8-8-8 3.582-8 8 3.582 8 8 8Z"
                    stroke="currentColor"
                    strokeWidth="2"
                    opacity="0.8"
                  />
                  <path
                    d="M16.5 16.5 21 21"
                    stroke="currentColor"
                    strokeWidth="2"
                    strokeLinecap="round"
                    opacity="0.8"
                  />
                </svg>
                <input
                  type="text"
                  placeholder="Search"
                  value={query}
                  onChange={(e) => handleSearchChange(e.target.value)}
                />
              </div>
              <div className={s.sliderContainer}>
              <input
                  type="range"
                  min="0"
                  max="1"
                  step="0.1"
                  value={semanticRatio}
                  onChange={handleSliderChange}
                  className={s.slider}
                />
                <label className={s.sliderLabel}>
                  <b>{semanticRatio.toFixed(1)}</b>
                </label>
              </div>
            </div>

            <a className={`${s.btn} ${s.btnPrimary}`} href="#">Login</a>
          </div>
        </div>
      </header>

      <main>
        <div className={s.container}>
          <div className={s.blogLayout}>
            <section>
              <div className={s.tabs}>
                <div className={`${s.tab} ${s.active}`} onClick={handleHomeTabClick}>Home</div>
                {/* <div className={`${s.tab} ${s.active}`}>Popular</div> */}
              </div>

              <div className={s.postList}>
                {
                  results.length > 0 ?
                    results.map((item) => (
                      <article className={s.post} key={item.id}>
                        <div>
                          <div className={s.postMeta}>
                            <span className={s.avatar}>
                              <img
                                src={item.author?.img}
                                alt="Author"
                              />
                            </span>
                            <span>
                              <strong style={{ fontWeight: 500, color: '#6b7280' }}>
                                {item.author?.name}
                              </strong>
                            </span>
                            <span>•</span>
                            <span>4 days ago</span>
                          </div>

                          <h2 className={s.postTitle}>
                            {item.title}
                          </h2>

                          <p className={s.postExcerpt}>
                            {item.description}
                          </p>

                          <div className={s.postFooter}>
                            <div className={s.postTags}>
                              <span className={s.pill}>{item.topics[0]}</span>
                              <span>3 min read</span>
                              <span>·</span>
                              <span>Selected for you</span>
                            </div>

                            {/* <div className="post-actions">
                                <div className="icon-btn">
                                  <svg viewBox="0 0 24 24" fill="none">
                                    <path
                                      d="M6 3h12a1 1 0 0 1 1 1v18l-7-4-7 4V4a1 1 0 0 1 1-1Z"
                                      stroke="currentColor"
                                      strokeWidth="1.8"
                                      strokeLinejoin="round"
                                    />
                                  </svg>
                                </div>
                                <div className="icon-btn">
                                  <svg viewBox="0 0 24 24" fill="none">
                                    <circle cx="6" cy="12" r="1.6" fill="currentColor" />
                                    <circle cx="12" cy="12" r="1.6" fill="currentColor" />
                                    <circle cx="18" cy="12" r="1.6" fill="currentColor" />
                                  </svg>
                                </div>
                              </div> */}
                          </div>
                        </div>

                        <div className={s.thumb}>
                          <img
                            src={item.imageUri}
                            alt="Thumbnail"
                          />
                        </div>
                      </article>
                    ))
                    : !loading ? 'No documents available' : ''
                }
                {!query && results.length > 0 && !loading && (
                  <div ref={ref} className={s.loadingSentinel}></div>
                )}
              </div>
            </section>

            <aside className={s.sidebar}>
              <h3>Blog Categories</h3>

              <ul className={s.categoryList}>
                <li><a href="#">Gaming</a></li>
                <li><a href="#">Sports</a></li>
                <li><a href="#">Business</a></li>
                <li><a href="#">Crypto</a></li>
                <li><a href="#">Television</a></li>
                <li><a href="#">Celebrity</a></li>
                <li><a href="#">Animal and Pets</a></li>
                <li><a href="#">Anime</a></li>
                <li><a href="#">Art</a></li>
                <li><a href="#">Cars and Motor Vehicles</a></li>
                <li><a href="#">Crafts and DIY</a></li>
                <li><a href="#">Culture, Race, and Ethnicity</a></li>
              </ul>

              <a className={s.seeMore} href="#">See more</a>
            </aside>
          </div>

          <div className={s.pageDivider}></div>
        </div>
      </main>

      <footer>
        <div className={s.container}>© levelworks.co</div>
      </footer>
    </>
  )
}

export default BlogPage;
