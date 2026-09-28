export interface VideoGame {
  id: number;
  title: string;
  platform: string;
  genre: string;
  developer: string | null;
  releaseDate: string | null;   // 'yyyy-MM-dd' (DateOnly)
  price: number;
  rating: number | null;
  version: string;              // concurrency token
}

export type UpdateVideoGame = Omit<VideoGame, 'id'>;

export interface PagedResult<T> {
  items: T[];
  totalCount: number;
  page: number;
  pageSize: number;
}

export type SortField = 'title' | 'releaseDate' | 'rating' | 'price';

export interface GameQuery {
  search: string;
  sortBy: SortField;
  descending: boolean;
  page: number;
  pageSize: number;
}
