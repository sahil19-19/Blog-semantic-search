import s from './BlogPage.module.scss';
import logo from '../../assets/img/logo.png';
import { ChangeEvent, useCallback, useEffect, useState } from 'react';
import { useInView } from 'react-intersection-observer';
import { Link } from 'react-router-dom';
import $api from '@/assets/utils/axios';
import PostService from '../../service/PostsService';

interface ICategory {
  id: number;
  name: string;
  postCount: number;
}

interface IPostResult {
  id: string;
  title: string;
  description: string;
  imageUri: string;
  topics: string[];
  author?: {
    name: string;
    img: string;
  };
}

const BlogPage = () => {
  const [query, setQuery] = useState("");
  const [results, setResults] = useState<IPostResult[]>([]);
  const [page, setPage] = useState(1);
  const limit = 10;
  const [loading, setLoading] = useState(false);
  const [hasMore, setHasMore] = useState(true);

  const [semanticRatio, setSemanticRatio] = useState(0.0);
  const [processingTimeMs, setProcessingTimeMs] = useState<number | null>(null);
  const [estimatedTotalHits, setEstimatedTotalHits] = useState<number | null>(null);

  const [categories, setCategories] = useState<ICategory[]>([]);
  const [selectedCategory, setSelectedCategory] = useState<string>("");
  const [categoriesOpen, setCategoriesOpen] = useState<boolean>(false);

  const [hasSearched, setHasSearched] = useState(false);// here

  const { ref, inView } = useInView({
    threshold: 0,
  });

  const resetSearchState = () => {
    setPage(1);
    setResults([]);
    setHasMore(true);
    setProcessingTimeMs(null);
    setEstimatedTotalHits(null);

    setHasSearched(false); //here
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

  const handleAllTabClick = () => {
    if (query || selectedCategory) {
      setQuery('');
      setSelectedCategory('');
      resetSearchState();
    }
  };

  const handleCategoryClick = (categoryName: string) => {
    // If the same category is clicked again, do nothing
    if (selectedCategory === categoryName) {
      return;
    }
    setSelectedCategory(categoryName);
    resetSearchState();
  };

  const fetchResults = useCallback(async (searchValue: string, pageNum: number) => {
    if (loading) return;

    setLoading(true);

    try {
      const requestBody: Record<string, string> = { search: searchValue };
      if (selectedCategory) {
        requestBody.FilterTopic = selectedCategory;
      }

      const res = await $api.post(
        `/posts/semantic?page=${pageNum}&limit=${limit}&ratio=${semanticRatio}`,
        requestBody
      );

      const data = res.data;

      const extracted: IPostResult[] = Array.isArray(data?.result?.hits)
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
      setHasSearched(true); //here
    }
  }, [loading, selectedCategory, limit, semanticRatio]);

  // 🔹 Debounce ONLY search + slider + category
  useEffect(() => {
    const timer = setTimeout(() => {
      fetchResults(query, 1);
    }, 350);

    return () => clearTimeout(timer);
  }, [query, semanticRatio, selectedCategory]);

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
  }, [page, query]);

  useEffect(() => {
    fetchCategories();
  }, []);

  const fetchCategories = async () => {
    try {
      const response = await PostService.fetchCategory();
      setCategories(response.data.result.topics);
    } catch (error) {
      console.error('Error fetching categories:', error);
    }
  };


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

            <a className={`${s.btn} ${s.btnPrimary}`} href="#">Login</a>
          </div>
        </div>
      </header>

      <main>
        <div className={s.container}>
          <div className={s.blogLayout}>
            <section>
              <div className={s.tabs}>
                {/* <div className={`${s.tab} ${s.active}`} onClick={handleHomeTabClick}>Home</div> */}
                {/* <div className={`${s.tab} ${s.active}`}>Popular</div> */}
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
              </div>

              {/* <div className={s.navSearch}>
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
              </div> */}

              {results.length > 0 && (processingTimeMs !== null || estimatedTotalHits !== null) && (
                <div className={s.searchMeta}>
                  {processingTimeMs !== null && (
                    <span className={s.searchMetaItem}>
                      Search took {processingTimeMs}ms
                    </span>
                  )}
                  {estimatedTotalHits !== null && (
                    <span className={s.searchMetaItem}>
                      ~{estimatedTotalHits.toLocaleString()} results
                    </span>
                  )}
                </div>
              )}

          		<div className={s.mobileCategoryRow}>
          		  <span>Category :</span>
          		  <b>All</b>
          		  <a href="#">Change</a>
          		</div>

              <div className={s.postList}>
                {
                  (loading && page === 1) ? (
                    <div className={s.searchingMessage}>Searching...</div>
                  ) : results.length > 0 ? (
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
                  ) : hasSearched ? (
                    <div className={s.noResultsMessage}>No results found</div>
                  ) : null
                }
                {results.length > 0 && (
                  <div ref={ref} className={s.loadingSentinel}></div>
                )}
              </div>
            </section>

            <aside className={s.sidebar}>
              <h3>Blog Categories</h3>
              <div className={`${s.tab} ${s.active}`} onClick={handleAllTabClick}>All</div>
              <ul className={s.categoryList}>
                {categories.slice(0, categoriesOpen ? categories.length : 12).map((category) => (
                  <li key={category.id}>
                    <a
                      href="#"
                      onClick={(e) => {
                        e.preventDefault();
                        handleCategoryClick(category.name);
                      }}
                      className={selectedCategory === category.name ? s.active : ''}
                    >
                      {category.name} [{category.postCount}]
                    </a>
                  </li>
                ))}
              </ul>

              {categories.length > 12 && (
                <button
                  className={s.seeMore}
                  onClick={() => setCategoriesOpen(current => !current)}
                >
                  {categoriesOpen ? "See less" : "See more"}
                </button>
              )}
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
