// import React, { useContext } from "react";
import { 
  InstantSearch, 
  SearchBox, 
  InfiniteHits, 
  RefinementList, 
  Configure, 
  Pagination, 
  Hits, 
  ClearRefinements, 
  SortBy,
  Highlight,
  Snippet
} from "react-instantsearch";
import { instantMeiliSearch } from "@meilisearch/instant-meilisearch";
import "./SemanticSearch.css";

const { searchClient } = instantMeiliSearch(
    "http://localhost:7700/",
    "d5add78b6f3490a2ba7af30a77ee92f405f7cbc8349743b6aa8a41119b58dc31",
    // "https://ms-adf78ae33284-106.lon.meilisearch.io",
    // "a63da4928426f12639e19d62886f621130f3fa9ff3c7534c5d179f0f51c4f303"
);

const SemanticSearch = () => (
    // <div className="ais-InstantSearch">
    //     <InstantSearch indexName="blogs" searchClient={searchClient}>
    //         <SearchBox className="ais-SearchBox" />
    //         <InfiniteHits
    //             hitComponent={Hit}
    //             className="ais-InfiniteHits-list"
    //         />
    //     </InstantSearch>
    // </div>
    <div className="ais-InstantSearch">
    <h1>MeiliSearch + React InstantSearch</h1>
    <InstantSearch indexName="blogs" searchClient={searchClient}>
      <div className="left-panel">
        {/* <ClearRefinements />
        <SortBy
        //   defaultRefinement="steam-videogames"
          items={[
            { value: "steam-videogames", label: "Relevant" },
            {
              value: "steam-videogames:recommendationCount:desc",
              label: "Most Recommended",
            },
            {
              value: "steam-videogames:recommendationCount:asc",
              label: "Least Recommended",
            },
          ]}
        />
        <h2>Genres</h2>
        <RefinementList attribute="genres" />
        <h2>Categories</h2>
        <RefinementList attribute="categories" />
        <h2>Platforms</h2>
        <RefinementList attribute="platforms" /> */}
        <Configure
          hitsPerPage={6}
          attributesToSnippet={["description:50"]}
          snippetEllipsisText={"..."}
          highlightPreTag="<mark>"
          highlightPostTag="</mark>"
        />
      </div>
      <div className="right-panel">
        <SearchBox className="ais-SearchBox" />
        <Hits hitComponent={Hit} />
        <Pagination showLast={true} />
      </div>
    </InstantSearch>
  </div>
);

const Hit = ({ hit }) => (
    <article key={hit.id} className="ais-Hits-item">
        <h1 className="hit-title">
            <Highlight attribute="title" hit={hit} />
        </h1>

        <h3 className="hit-info">{hit.author}</h3>
        <h3 className="hit-info">{hit.topics}</h3>
        <h3 className="hit-info">{hit.dateCreated}</h3>

        <p className="hit-description">
            <Snippet attribute="description" hit={hit} />
        </p>
    </article>
);

export default SemanticSearch;
