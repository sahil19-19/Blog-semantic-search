export interface CategoryResponse {
    result: {
        totalCount,
        topics: {
            id: number;
            name: string;
            postCount: number;
        }[];
    };
}
