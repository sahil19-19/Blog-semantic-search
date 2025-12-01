// import React, { useState, useEffect } from "react";

// const API_URL = "http://localhost:5010/api/v1/posts/semantic";

// const SemanticSearch = () => {
//   const [query, setQuery] = useState("");
//   const [results, setResults] = useState<any[]>([]);
//   const [page, setPage] = useState(1);
//   const [limit] = useState(10);
//   const [loading, setLoading] = useState(false);
//   const [debounceTimer, setDebounceTimer] = useState<any>(null);

//   // NEW — slider state
//   const [semanticRatio, setSemanticRatio] = useState(0.0);

//   // Ensure slider always keeps only ONE decimal place
//   const handleSliderChange = (e: React.ChangeEvent<HTMLInputElement>) => {
//     const value = parseFloat(e.target.value);

//     // Round to 1 decimal place
//     const rounded = Math.round(value * 10) / 10;

//     setSemanticRatio(rounded);
//   };

//   // Function to call backend
//   const fetchResults = async (searchValue: string, pageNum: number) => {
//     if (!searchValue.trim()) {
//       setResults([]);
//       return;
//     }

//     setLoading(true);

//     try {
//       const res = await fetch(
//         `${API_URL}?page=${pageNum}&limit=${limit}&ratio=${semanticRatio}`,
//         {
//           method: "POST",
//           headers: {
//             "Content-Type": "application/json",
//           },
//           body: JSON.stringify({
//             search: searchValue,
//           }),
//         }
//       );

//       const data = await res.json();

//       const extracted = Array.isArray(data?.result?.hits)
//         ? data.result.hits
//         : [];

//       setResults(extracted);
//     } catch (err) {
//       console.error("Semantic Search Error:", err);
//       setResults([]);
//     } finally {
//       setLoading(false);
//     }
//   };

//   // Search-as-you-type with debouncing
//   useEffect(() => {
//     if (debounceTimer) clearTimeout(debounceTimer);

//     const timer = setTimeout(() => {
//       fetchResults(query, page);
//     }, 350);

//     setDebounceTimer(timer);
//   }, [query, page, semanticRatio]); // also refetch if slider changes

//   return (
//     <div style={{ width: "600px", margin: "0 auto", padding: "20px" }}>
//       <h2>Semantic Search</h2>

//       {/* Search Bar */}
//       <input
//         type="text"
//         value={query}
//         placeholder="Search blogs semantically..."
//         onChange={(e) => {
//           setQuery(e.target.value);
//           setPage(1);
//         }}
//         style={{
//           width: "100%",
//           padding: "10px",
//           fontSize: "16px",
//           marginBottom: "20px",
//         }}
//       />

//       {/* SLIDER COMPONENT */}
//       <div style={{ marginBottom: "20px" }}>
//         <label style={{ display: "block", marginBottom: "8px" }}>
//           Semantic Ratio: <b>{semanticRatio.toFixed(1)}</b>
//         </label>

//         <input
//           type="range"
//           min="0"
//           max="1"
//           step="0.1"
//           value={semanticRatio}
//           onChange={handleSliderChange}
//           style={{ width: "50%" }}
//         />
//       </div>

//       {/* Loading */}
//       {loading && <p>Searching...</p>}

//       {/* Results */}
//       <ul style={{ listStyle: "none", padding: 0 }}>
//         {Array.isArray(results) &&
//           results.map((item: any, index: number) => (
//             <li
//               key={index}
//               style={{
//                 marginBottom: "20px",
//                 borderBottom: "1px solid #ccc",
//                 paddingBottom: "10px",
//               }}
//             >
//               <h3>{item.title}</h3>
//               {item.topics && (
//                 <p>
//                   <b>Topics:</b> {item.topics}
//                 </p>
//               )}
//               {item.dateCreated && (
//                 <p>
//                   <b>Published:</b> {item.dateCreated}
//                 </p>
//               )}
//               <p>{item.description}</p>
//             </li>
//           ))}

//         {results.length === 0 && !loading && query && <p>No results found.</p>}
//       </ul>

//       {/* Pagination */}
//       {Array.isArray(results) && results.length > 0 && (
//         <div style={{ display: "flex", gap: "10px" }}>
//           <button disabled={page === 1} onClick={() => setPage((p) => p - 1)}>
//             Previous
//           </button>

//           <button onClick={() => setPage((p) => p + 1)}>Next</button>
//         </div>
//       )}
//     </div>
//   );
// };

// export default SemanticSearch;


import { useState, useEffect, ChangeEvent } from "react";
import "./SemanticSearch.css"; // ← IMPORT CSS

const API_URL = "http://localhost:5010/api/v1/posts/semantic";

const SemanticSearch = () => {
  const [query, setQuery] = useState("");
  const [results, setResults] = useState<any[]>([]);
  const [page, setPage] = useState(1);
  const [limit] = useState(10);
  const [loading, setLoading] = useState(false);
  const [debounceTimer, setDebounceTimer] = useState<any>(null);

  const [semanticRatio, setSemanticRatio] = useState(0.0);
  const [processingTimeMs, setProcessingTimeMs] = useState<number | null>(null);
  const [estimatedTotalHits, setEstimatedTotalHits] = useState<number | null>(null);

  const handleSliderChange = (e: ChangeEvent<HTMLInputElement>) => {
    const value = parseFloat(e.target.value);
    const rounded = Math.round(value * 10) / 10;
    setSemanticRatio(rounded);
  };

  const fetchResults = async (searchValue: string, pageNum: number) => {
    if (!searchValue.trim()) {
      setResults([]);
      setProcessingTimeMs(null);
      setEstimatedTotalHits(null);
      return;
    }

    setLoading(true);

    try {
      const res = await fetch(
        `${API_URL}?page=${pageNum}&limit=${limit}&ratio=${semanticRatio}`,
        {
          method: "POST",
          headers: {
            "Content-Type": "application/json",
          },
          body: JSON.stringify({ search: searchValue }),
        }
      );

      const data = await res.json();

      const extracted = Array.isArray(data?.result?.hits)
        ? data.result.hits
        : [];

      setResults(extracted);
      setProcessingTimeMs(
        typeof data?.result?.processingTimeMs === "number"
          ? data.result.processingTimeMs
          : null
      );
      setEstimatedTotalHits(
        typeof data?.result?.estimatedTotalHits === "number"
          ? data.result.estimatedTotalHits
          : null
      );
    } catch (err) {
      console.error("Semantic Search Error:", err);
      setResults([]);
      setProcessingTimeMs(null);
      setEstimatedTotalHits(null);
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    if (debounceTimer) clearTimeout(debounceTimer);

    const timer = setTimeout(() => {
      fetchResults(query, page);
    }, 350);

    setDebounceTimer(timer);
  }, [query, page, semanticRatio]);

  return (
    <div className="ss-container">
      <h2 className="ss-title">Semantic Search</h2>

      <input
        type="text"
        value={query}
        placeholder="Search blogs semantically..."
        onChange={(e) => {
          setQuery(e.target.value);
          setPage(1);
        }}
        className="ss-input"
      />

      {/* SLIDER */}
      <div className="ss-slider-container">
        <label className="ss-slider-label">
          Semantic Ratio: <b>{semanticRatio.toFixed(1)}</b>
        </label>

        <input
          type="range"
          min="0"
          max="1"
          step="0.1"
          value={semanticRatio}
          onChange={handleSliderChange}
          className="ss-slider"
        />
      </div>

      {loading && <p className="ss-loading">Searching...</p>}

      {(processingTimeMs !== null || estimatedTotalHits !== null) && (
        <div className="ss-meta">
          {estimatedTotalHits !== null && (
            <p className="ss-meta-item">
              <b>Hits:</b> {estimatedTotalHits}
            </p>
          )}
          {processingTimeMs !== null && (
            <p className="ss-meta-item">
              <b>Time spent:</b> {processingTimeMs} ms
            </p>
          )}
        </div>
      )}

      {/* Results */}
      <ul className="ss-results-list">
        {Array.isArray(results) &&
          results.map((item: any, index: number) => (
            <li key={index} className="ss-result-item">
              <h3
                className="ss-result-title"
                dangerouslySetInnerHTML={{ __html: item._formatted?.title || item.title }}
              />
              {item.topics && (
                <p className="ss-result-text">
                  <b>Topics:</b> {item.topics}
                </p>
              )}
              {item.dateCreated && (
                <p className="ss-result-text">
                  <b>Published:</b> {item.dateCreated}
                </p>
              )}
              <p 
                className="ss-result-text description-preview" 
                dangerouslySetInnerHTML={{ 
                  __html: item._formatted?.description || item.description 
                  }} 
              />
            </li>
          ))}

        {results.length === 0 && !loading && query && (
          <p className="ss-no-results">No results found.</p>
        )}
      </ul>

      {Array.isArray(results) && results.length > 0 && (
        <div className="ss-pagination">
          <button
            disabled={page === 1}
            onClick={() => setPage((p) => p - 1)}
            className="ss-button"
          >
            Previous
          </button>

          <button onClick={() => setPage((p) => p + 1)} className="ss-button">
            Next
          </button>
        </div>
      )}
    </div>
  );
};

export default SemanticSearch;
