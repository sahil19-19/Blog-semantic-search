import React, { useState, useEffect } from "react";

const API_URL = "http://localhost:5010/api/v1/posts/semantic";

const SemanticSearch = () => {
  const [query, setQuery] = useState("");
  const [results, setResults] = useState<any[]>([]);
  const [page, setPage] = useState(1);
  const [limit] = useState(10);
  const [loading, setLoading] = useState(false);
  const [debounceTimer, setDebounceTimer] = useState<any>(null);

  // Function to call backend
  const fetchResults = async (searchValue: string, pageNum: number) => {
    if (!searchValue.trim()) {
      setResults([]);
      return;
    }

    setLoading(true);

    try {
      const res = await fetch(`${API_URL}?page=${pageNum}&limit=${limit}`, {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({
          search: searchValue,
        }),
      });

      const data = await res.json();

      // ---- FIX STARTS HERE ----
      // Safely extract `result.hits`
      const extracted = Array.isArray(data?.result?.hits)
        ? data.result.hits
        : [];

      setResults(extracted);
      // ---- FIX ENDS HERE ----

    } catch (err) {
      console.error("Semantic Search Error:", err);
      setResults([]);
    } finally {
      setLoading(false);
    }
  };

  // Search-as-you-type with debouncing
  useEffect(() => {
    if (debounceTimer) clearTimeout(debounceTimer);

    const timer = setTimeout(() => {
      fetchResults(query, page);
    }, 350); // user stops typing for 350ms

    setDebounceTimer(timer);
  }, [query, page]);

  return (
    <div style={{ width: "600px", margin: "0 auto", padding: "20px" }}>
      <h2>Semantic Search</h2>

      {/* Search Bar */}
      <input
        type="text"
        value={query}
        placeholder="Search blogs semantically..."
        onChange={(e) => {
          setQuery(e.target.value);
          setPage(1); // reset page when new search begins
        }}
        style={{
          width: "100%",
          padding: "10px",
          fontSize: "16px",
          marginBottom: "20px",
        }}
      />

      {/* Loading */}
      {loading && <p>Searching...</p>}

      {/* Results */}
      <ul style={{ listStyle: "none", padding: 0 }}>
        {Array.isArray(results) &&
          results.map((item: any, index: number) => (
            <li
              key={index}
              style={{
                marginBottom: "20px",
                borderBottom: "1px solid #ccc",
                paddingBottom: "10px",
              }}
            >
              <h3>{item.title}</h3>
              {item.topics && (
                <p>
                  <b>Topics:</b> {item.topics}
                </p>
              )}
              {item.dateCreated && (
                <p>
                  <b>Published:</b> {item.dateCreated}
                </p>
              )}
              <p>{item.description}</p>
            </li>
          ))}

        {results.length === 0 && !loading && query && (
          <p>No results found.</p>
        )}
      </ul>

      {/* Pagination */}
      {Array.isArray(results) && results.length > 0 && (
        <div style={{ display: "flex", gap: "10px" }}>
          <button disabled={page === 1} onClick={() => setPage((p) => p - 1)}>
            Previous
          </button>

          <button onClick={() => setPage((p) => p + 1)}>Next</button>
        </div>
      )}
    </div>
  );
};

export default SemanticSearch;
