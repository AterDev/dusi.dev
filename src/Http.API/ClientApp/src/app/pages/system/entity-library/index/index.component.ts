import { Component, OnInit, ViewChild } from '@angular/core';
import { EntityLibraryService } from 'src/app/share/services/entity-library.service';
import { Router } from '@angular/router';
import { ConfirmDialogComponent } from 'src/app/components/confirm-dialog/confirm-dialog.component';
import { EntityLibraryItemDto } from 'src/app/share/models/entity-library/entity-library-item-dto.model';
import { EntityLibraryFilterDto } from 'src/app/share/models/entity-library/entity-library-filter-dto.model';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatDialog } from '@angular/material/dialog';

@Component({
  selector: 'app-index',
  templateUrl: './index.component.html',
  styleUrls: ['./index.component.css']
})
export class IndexComponent implements OnInit {
  @ViewChild(MatPaginator, { static: true }) paginator!: MatPaginator;
  isLoading = true;
  total = 0;
  data: EntityLibraryItemDto[] = [];
  columns: string[] = ['name', 'description', 'isPublic', 'createdTime', 'updatedTime', 'actions'];
  dataSource!: MatTableDataSource<EntityLibraryItemDto>;
  filter: EntityLibraryFilterDto;
  pageSizeOption = [12, 20, 50];
  constructor(
    private service: EntityLibraryService,
    private snb: MatSnackBar,
    private dialog: MatDialog,
    private router: Router,
  ) {

    this.filter = {
      pageIndex: 1,
      pageSize: 12
    };
  }

  ngOnInit(): void {
    this.getList();
  }

  getList(event?: PageEvent): void {
    if(event) {
      this.filter.pageIndex = event.pageIndex + 1;
      this.filter.pageSize = event.pageSize;
    }
    this.service.filter(this.filter)
      .subscribe(res => {
        if (res.data) {
          this.data = res.data;
          this.total = res.count;
          this.dataSource = new MatTableDataSource<EntityLibraryItemDto>(this.data);
        }
        this.isLoading = false;
      });
  }

  deleteConfirm(item: EntityLibraryItemDto): void {
    const ref = this.dialog.open(ConfirmDialogComponent, {
      hasBackdrop: true,
      disableClose: false,
      data: {
        title: '??????',
        content: '???????????????????'
      }
    });

    ref.afterClosed().subscribe(res => {
      if (res) {
        this.delete(item);
      }
    });
  }
  delete(item: EntityLibraryItemDto): void {
    this.service.delete(item.id)
      .subscribe(res => {
        if (res) {
          this.data = this.data.filter(_ => _.id !== item.id);
          this.dataSource.data = this.data;
          this.snb.open('????????????');
        } else {
          this.snb.open('????????????');
        }
      });
}

/*
openAddDialog(): void {
  const ref = this.dialog.open(AddComponent, {
    hasBackdrop: true,
    disableClose: false,
    data: {
    }
  });
  ref.afterClosed().subscribe(res => {
    if (res) {
      this.snb.open('????????????');
      this.getList();
    }
  });
}
openDetailDialog(id: string): void {
  const ref = this.dialog.open(DetailComponent, {
    hasBackdrop: true,
    disableClose: false,
    data: { id }
  });
  ref.afterClosed().subscribe(res => {
    if (res) { }
  });
}

openEditDialog(id: string): void {
  const ref = this.dialog.open(EditComponent, {
    hasBackdrop: true,
    disableClose: false,
    data: { id }
  });
  ref.afterClosed().subscribe(res => {
    if (res) {
      this.snb.open('????????????');
      this.getList();
    }
  });
}*/

  /**
   * ??????
   */
  edit(id: string): void {
    console.log(id);
    this.router.navigateByUrl('/admin/entity-library/edit/' + id);
  }

}
