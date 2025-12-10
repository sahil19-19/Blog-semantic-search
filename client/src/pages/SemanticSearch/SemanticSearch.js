import { jsx as _jsx, jsxs as _jsxs } from "react/jsx-runtime";
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
import { useState, useEffect } from "react";
import "./SemanticSearch.css"; // ← IMPORT CSS
import $api from "../../assets/utils/axios";
const SemanticSearch = () => {
    const [query, setQuery] = useState("");
    const [results, setResults] = useState([]);
    const [page, setPage] = useState(1);
    const [limit] = useState(10);
    const [loading, setLoading] = useState(false);
    const [debounceTimer, setDebounceTimer] = useState(null);
    const [semanticRatio, setSemanticRatio] = useState(0.0);
    const [processingTimeMs, setProcessingTimeMs] = useState(null);
    const [estimatedTotalHits, setEstimatedTotalHits] = useState(null);
    const handleSliderChange = (e) => {
        const value = parseFloat(e.target.value);
        const rounded = Math.round(value * 10) / 10;
        setSemanticRatio(rounded);
    };
    const fetchResults = async (searchValue, pageNum) => {
        if (!searchValue.trim()) {
            setResults([]);
            setProcessingTimeMs(null);
            setEstimatedTotalHits(null);
            return;
        }
        setLoading(true);
        try {
            const res = await $api.post(`/posts/semantic?page=${pageNum}&limit=${limit}&ratio=${semanticRatio}`, { search: searchValue });
            const data = res.data;
            const extracted = Array.isArray(data?.result?.hits)
                ? data.result.hits
                : [];
            setResults(extracted);
            setProcessingTimeMs(typeof data?.result?.processingTimeMs === "number"
                ? data.result.processingTimeMs
                : null);
            setEstimatedTotalHits(typeof data?.result?.estimatedTotalHits === "number"
                ? data.result.estimatedTotalHits
                : null);
        }
        catch (err) {
            console.error("Semantic Search Error:", err);
            setResults([]);
            setProcessingTimeMs(null);
            setEstimatedTotalHits(null);
        }
        finally {
            setLoading(false);
        }
    };
    useEffect(() => {
        if (debounceTimer)
            clearTimeout(debounceTimer);
        const timer = setTimeout(() => {
            fetchResults(query, page);
        }, 350);
        setDebounceTimer(timer);
    }, [query, page, semanticRatio]);
    return (_jsxs("div", { className: "ss-container", children: [_jsx("h2", { className: "ss-title", children: "Semantic Search" }), _jsx("input", { type: "text", value: query, placeholder: "Search blogs semantically...", onChange: (e) => {
                    setQuery(e.target.value);
                    setPage(1);
                }, className: "ss-input" }), _jsxs("div", { className: "ss-slider-container", children: [_jsxs("label", { className: "ss-slider-label", children: ["Semantic Ratio: ", _jsx("b", { children: semanticRatio.toFixed(1) })] }), _jsx("input", { type: "range", min: "0", max: "1", step: "0.1", value: semanticRatio, onChange: handleSliderChange, className: "ss-slider" })] }), loading && _jsx("p", { className: "ss-loading", children: "Searching..." }), (processingTimeMs !== null || estimatedTotalHits !== null) && (_jsxs("div", { className: "ss-meta", children: [estimatedTotalHits !== null && (_jsxs("p", { className: "ss-meta-item", children: [_jsx("b", { children: "Hits:" }), " ", estimatedTotalHits] })), processingTimeMs !== null && (_jsxs("p", { className: "ss-meta-item", children: [_jsx("b", { children: "Time spent:" }), " ", processingTimeMs, " ms"] }))] })), _jsxs("ul", { className: "ss-results-list", children: [Array.isArray(results) &&
                        results.map((item, index) => (_jsxs("li", { className: "ss-result-item", children: [_jsx("h3", { className: "ss-result-title", dangerouslySetInnerHTML: { __html: item._formatted?.title || item.title } }), item.topics && (_jsxs("p", { className: "ss-result-text", children: [_jsx("b", { children: "Topics:" }), " ", item.topics] })), item.dateCreated && (_jsxs("p", { className: "ss-result-text", children: [_jsx("b", { children: "Published:" }), " ", item.dateCreated] })), _jsx("p", { className: "ss-result-text description-preview", dangerouslySetInnerHTML: {
                                        __html: item._formatted?.description || item.description
                                    } })] }, index))), results.length === 0 && !loading && query && (_jsx("p", { className: "ss-no-results", children: "No results found." }))] }), Array.isArray(results) && results.length > 0 && (_jsxs("div", { className: "ss-pagination", children: [_jsx("button", { disabled: page === 1, onClick: () => setPage((p) => p - 1), className: "ss-button", children: "Previous" }), _jsx("button", { onClick: () => setPage((p) => p + 1), className: "ss-button", children: "Next" })] }))] }));
};
export default SemanticSearch;
