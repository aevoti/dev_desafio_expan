import { IOrderItem } from "./i.orderItem";

export interface IOrder {
    id: string,
    createdOn: Date,
    status: string,
    items: IOrderItem[]
}