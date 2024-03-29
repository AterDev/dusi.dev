import { Component, Inject, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { ThirdNewsService } from 'src/app/share/admin/services/third-news.service';
import { ThirdNewsAddDto } from 'src/app/share/admin/models/third-news/third-news-add-dto.model';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { Location } from '@angular/common';
import { NewsSource } from 'src/app/share/admin/models/enum/news-source.model';
import { NewsType } from 'src/app/share/admin/models/enum/news-type.model';
import { TechType } from 'src/app/share/admin/models/enum/tech-type.model';

@Component({
  selector: 'app-add',
  templateUrl: './add.component.html',
  styleUrls: ['./add.component.css']
})
export class AddComponent implements OnInit {
  NewsSource = NewsSource;
  NewsType = NewsType;
  TechType = TechType;

  formGroup!: FormGroup;
  data = {} as ThirdNewsAddDto;
  isLoading = true;
  constructor(

    // private authService: OidcSecurityService,
    private service: ThirdNewsService,
    public snb: MatSnackBar,
    private router: Router,
    private route: ActivatedRoute,
    private location: Location
    // public dialogRef: MatDialogRef<AddComponent>,
    // @Inject(MAT_DIALOG_DATA) public dlgData: { id: '' }
  ) {

  }
  get title() { return this.formGroup.get('title'); }
  get description() { return this.formGroup.get('description'); }
  get datePublished() { return this.formGroup.get('datePublished'); }
  get content() { return this.formGroup.get('content'); }
  get type() { return this.formGroup.get('type'); }
  get newsType() { return this.formGroup.get('newsType'); }
  get techType() { return this.formGroup.get('techType'); }


  ngOnInit(): void {
    this.initForm();
    // TODO:获取其他相关数据后设置加载状态
    this.isLoading = false;
  }

  onReady(editor: any) {
    editor.ui.getEditableElement().parentElement.insertBefore(
      editor.ui.view.toolbar.element,
      editor.ui.getEditableElement()
    );
  }
  initForm(): void {
    this.formGroup = new FormGroup({
      title: new FormControl(null, [Validators.maxLength(200)]),
      description: new FormControl(null, [Validators.maxLength(5000)]),
      content: new FormControl(null, [Validators.maxLength(8000)]),
      type: new FormControl(null, []),
      newsType: new FormControl(null, []),
      techType: new FormControl(null, []),
    });
  }
  getValidatorMessage(type: string): string {
    switch (type) {
      case 'title':
        return this.title?.errors?.['required'] ? 'Title必填' :
          this.title?.errors?.['minlength'] ? 'Title长度最少位' :
            this.title?.errors?.['maxlength'] ? 'Title长度最多200位' : '';
      case 'description':
        return this.description?.errors?.['required'] ? 'Description必填' :
          this.description?.errors?.['minlength'] ? 'Description长度最少位' :
            this.description?.errors?.['maxlength'] ? 'Description长度最多5000位' : '';
      case 'datePublished':
        return this.datePublished?.errors?.['required'] ? 'DatePublished必填' :
          this.datePublished?.errors?.['minlength'] ? 'DatePublished长度最少位' :
            this.datePublished?.errors?.['maxlength'] ? 'DatePublished长度最多位' : '';
      case 'content':
        return this.content?.errors?.['required'] ? 'Content必填' :
          this.content?.errors?.['minlength'] ? 'Content长度最少位' :
            this.content?.errors?.['maxlength'] ? 'Content长度最多8000位' : '';
      case 'type':
        return this.type?.errors?.['required'] ? 'Type必填' :
          this.type?.errors?.['minlength'] ? 'Type长度最少位' :
            this.type?.errors?.['maxlength'] ? 'Type长度最多位' : '';
      case 'newsType':
        return this.newsType?.errors?.['required'] ? 'NewsType必填' :
          this.newsType?.errors?.['minlength'] ? 'NewsType长度最少位' :
            this.newsType?.errors?.['maxlength'] ? 'NewsType长度最多位' : '';
      case 'techType':
        return this.techType?.errors?.['required'] ? 'TechType必填' :
          this.techType?.errors?.['minlength'] ? 'TechType长度最少位' :
            this.techType?.errors?.['maxlength'] ? 'TechType长度最多位' : '';

      default:
        return '';
    }
  }

  add(): void {
    if (this.formGroup.valid) {
      const data = this.formGroup.value as ThirdNewsAddDto;
      this.service.add(data)
        .subscribe({
          next: (res) => {
            if (res) {
              this.snb.open('添加成功');
              // this.dialogRef.close(res);
              this.router.navigate(['../index'], { relativeTo: this.route });
            }

          },
          error: () => {
          }
        });
    }
  }
  back(): void {
    this.location.back();
  }
}
