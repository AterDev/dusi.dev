import { Component, Inject, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { EntityModel } from 'src/app/share/models/entity-model/entity-model.model';
import { EntityModelService } from 'src/app/share/services/entity-model.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { EntityModelUpdateDto } from 'src/app/share/models/entity-model/entity-model-update-dto.model';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Location } from '@angular/common';
import * as ClassicEditor from 'ng-ckeditor5-classic';
import { environment } from 'src/environments/environment';
import { CKEditor5 } from '@ckeditor/ckeditor5-angular';
// import { OidcSecurityService } from 'angular-auth-oidc-client';
import { AccessModifier } from 'src/app/share/models/enum/access-modifier.model';
import { CodeLanguage } from 'src/app/share/models/enum/code-language.model';

@Component({
  selector: 'app-edit',
  templateUrl: './edit.component.html',
  styleUrls: ['./edit.component.css']
})
export class EditComponent implements OnInit {
  public editorConfig!: CKEditor5.Config;
  public editor: CKEditor5.EditorConstructor = ClassicEditor;
  AccessModifier = AccessModifier;
CodeLanguage = CodeLanguage;

  id!: string;
  isLoading = true;
  data = {} as EntityModel;
  updateData = {} as EntityModelUpdateDto;
  formGroup!: FormGroup;
    constructor(
    
    // private authService: OidcSecurityService,
    private service: EntityModelService,
    private snb: MatSnackBar,
    private router: Router,
    private route: ActivatedRoute,
    private location: Location
    // public dialogRef: MatDialogRef<EditComponent>,
    // @Inject(MAT_DIALOG_DATA) public dlgData: { id: '' }
  ) {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.id = id;
    } else {
      // TODO: id为空
    }
  }

    get name() { return this.formGroup.get('name'); }
    get comment() { return this.formGroup.get('comment'); }
    get accessModifier() { return this.formGroup.get('accessModifier'); }
    get codeExample() { return this.formGroup.get('codeExample'); }
    get codeLanguage() { return this.formGroup.get('codeLanguage'); }


  ngOnInit(): void {
    this.getDetail();
    this.initEditor();
    // TODO:等待数据加载完成
    // this.isLoading = false;
  }
    initEditor(): void {
    this.editorConfig = {
      // placeholder: '请添加图文信息提供证据，也可以直接从Word文档中复制',
      simpleUpload: {
        uploadUrl: environment.uploadEditorFileUrl,
        headers: {
          Authorization: 'Bearer ' + localStorage.getItem("accessToken")
        }
      },
      language: 'zh-cn'
    };
  }
  onReady(editor: any) {
    editor.ui.getEditableElement().parentElement.insertBefore(
      editor.ui.view.toolbar.element,
      editor.ui.getEditableElement()
    );
  }
  getDetail(): void {
    this.service.getDetail(this.id)
      .subscribe(res => {
        this.data = res;
        this.initForm();
        this.isLoading = false;
      }, error => {
        this.snb.open(error);
      })
  }

  initForm(): void {
    this.formGroup = new FormGroup({
      name: new FormControl(this.data.name, [Validators.maxLength(60)]),
      comment: new FormControl(this.data.comment, [Validators.maxLength(300)]),
      accessModifier: new FormControl(this.data.accessModifier, []),
      codeExample: new FormControl(this.data.codeExample, [Validators.maxLength(2000)]),
      codeLanguage: new FormControl(this.data.codeLanguage, []),

    });
  }
  getValidatorMessage(type: string): string {
    switch (type) {
      case 'name':
        return this.name?.errors?.['required'] ? 'Name必填' :
          this.name?.errors?.['minlength'] ? 'Name长度最少位' :
            this.name?.errors?.['maxlength'] ? 'Name长度最多60位' : '';
      case 'comment':
        return this.comment?.errors?.['required'] ? 'Comment必填' :
          this.comment?.errors?.['minlength'] ? 'Comment长度最少位' :
            this.comment?.errors?.['maxlength'] ? 'Comment长度最多300位' : '';
      case 'accessModifier':
        return this.accessModifier?.errors?.['required'] ? 'AccessModifier必填' :
          this.accessModifier?.errors?.['minlength'] ? 'AccessModifier长度最少位' :
            this.accessModifier?.errors?.['maxlength'] ? 'AccessModifier长度最多位' : '';
      case 'codeExample':
        return this.codeExample?.errors?.['required'] ? 'CodeExample必填' :
          this.codeExample?.errors?.['minlength'] ? 'CodeExample长度最少位' :
            this.codeExample?.errors?.['maxlength'] ? 'CodeExample长度最多2000位' : '';
      case 'codeLanguage':
        return this.codeLanguage?.errors?.['required'] ? 'CodeLanguage必填' :
          this.codeLanguage?.errors?.['minlength'] ? 'CodeLanguage长度最少位' :
            this.codeLanguage?.errors?.['maxlength'] ? 'CodeLanguage长度最多位' : '';

      default:
        return '';
    }
  }
  edit(): void {
    if(this.formGroup.valid) {
      this.updateData = this.formGroup.value as EntityModelUpdateDto;
      this.service.update(this.id, this.updateData)
        .subscribe(res => {
          this.snb.open('修改成功');
           // this.dialogRef.close(res);
          this.router.navigate(['../../index'],{relativeTo: this.route});
        });
    }
  }

  back(): void {
    this.location.back();
  }

}
