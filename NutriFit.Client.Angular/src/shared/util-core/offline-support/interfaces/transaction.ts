export interface Transaction<T> {
  collection: string;
  key: string | null;
  data: T | null;
  method: 'POST' | 'PUT' | 'DELETE';
  endpoint: string;
  payload?: any;
  timestamp: string;
}
