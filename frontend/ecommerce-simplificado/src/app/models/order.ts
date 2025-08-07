export interface Order {
    id: number;
    products: { id: number; quantity: number }[];
    status: string;
}
