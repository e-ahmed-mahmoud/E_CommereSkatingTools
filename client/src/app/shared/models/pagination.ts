
export interface Pagination<T> {
    pageNumber: number;
    pageSize: number;
    count: number;
    items: T;
}
