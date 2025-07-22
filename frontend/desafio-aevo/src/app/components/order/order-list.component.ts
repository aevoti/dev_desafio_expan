import { Component, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { OrderDispatcherService } from '../../services/orders-dispatcher-service';
import { IOrder } from '../../interfaces/i.order';
import { CommonModule } from '@angular/common';
import { MaterialModule } from '../../material/material.module';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { interval, Subscription } from 'rxjs';

@Component({
  standalone: true,
  imports: [CommonModule, MaterialModule],
  selector: 'app-order-list',
  templateUrl: './order-list.component.html',
  styleUrls: ['./order-list.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class OrderListComponent implements OnInit {
  public dataSource = new MatTableDataSource<IOrder>();
  public columnsToDisplay = ['id', 'createdOn', 'status'];
  public columnsToDisplayWithExpand = [...this.columnsToDisplay, 'expand'];
  public expandedElement: IOrder | null = null;
  private refreshSubscription!: Subscription;

  @ViewChild(MatPaginator) paginator!: MatPaginator;

  constructor(private orderService: OrderDispatcherService) { }

  ngOnInit(): void {
    this.loadOrders();

    this.refreshSubscription = interval(30000).subscribe(() => {
      this.loadOrders();
    });
  }

  private statusTranslation: { [key: string]: string } = {
    Received: 'Recebido',
    Processing: 'Processando',
    Completed: 'Concluído',
    Failed: 'Falhou'
  };

  public getStatusClass(status: string): string {
    switch (status?.toLowerCase()) {
      case 'received': return 'received';
      case 'processing': return 'processing';
      case 'completed': return 'completed';
      case 'failed': return 'failed';
      default: return '';
    }
  }

  private loadOrders(): void {
    this.orderService.getAll().subscribe({
      next: (orders) => {
        this.dataSource.data = orders.map((order: IOrder) => ({
          ...order,
          originalStatus: order.status,
          status: this.statusTranslation[order.status] || order.status
        }));
        this.dataSource.paginator = this.paginator;
      },
      error: (err) => console.error(err)
    });
  }
  
  public toggle(element: IOrder): void {
    this.expandedElement = this.expandedElement === element ? null : element;
  }

  public isExpanded(element: IOrder): boolean {
    return this.expandedElement === element;
  }
}