import { Component, OnInit, ViewChild } from '@angular/core';
import { EntityModelService } from 'src/app/share/admin/services/entity-model.service';
import { Router } from '@angular/router';
import { ConfirmDialogComponent } from 'src/app/components/confirm-dialog/confirm-dialog.component';
import { EntityModelItemDto } from 'src/app/share/admin/models/entity-model/entity-model-item-dto.model';
import { EntityModelFilterDto } from 'src/app/share/admin/models/entity-model/entity-model-filter-dto.model';
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
  data: EntityModelItemDto[] = [];
  columns: string[] = ['name', 'comment', 'accessModifier', 'codeLanguage', 'languageVersion', 'actions'];
  dataSource!: MatTableDataSource<EntityModelItemDto>;
  filter: EntityModelFilterDto;
  pageSizeOption = [12, 20, 50];
  constructor(
    private service: EntityModelService,
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
          this.dataSource = new MatTableDataSource<EntityModelItemDto>(this.data);
        }
        this.isLoading = false;
      });
  }

  deleteConfirm(item: EntityModelItemDto): void {
    const ref = this.dialog.open(ConfirmDialogComponent, {
      hasBackdrop: true,
      disableClose: false,
      data: {
        title: '删除',
        content: '是否确定删除?'
      }
    });

    ref.afterClosed().subscribe(res => {
      if (res) {
        this.delete(item);
      }
    });
  }
  delete(item: EntityModelItemDto): void {
    this.service.delete(item.id)
      .subscribe(res => {
        if (res) {
          this.data = this.data.filter(_ => _.id !== item.id);
          this.dataSource.data = this.data;
          this.snb.open('删除成功');
        } else {
          this.snb.open('删除失败');
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
      this.snb.open('添加成功');
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
      this.snb.open('修改成功');
      this.getList();
    }
  });
}*/

  /**
   * 编辑
   */
  edit(id: string): void {
    console.log(id);
    this.router.navigateByUrl('/admin/entity-model/edit/' + id);
  }

}
