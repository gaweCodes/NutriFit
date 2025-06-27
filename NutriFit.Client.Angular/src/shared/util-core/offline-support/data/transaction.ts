export interface Transaction<T> {
  id?: number;
  collection: string;
  key: string | null;
  data: T | null;
  method: 'POST' | 'PUT' | 'DELETE';
  endpoint: string;
  payload?: any;
  // timestamp: string;
}
