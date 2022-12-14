import { Component, Inject, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { EntityModelService } from 'src/app/share/services/entity-model.service';
import { EntityModel } from 'src/app/share/models/entity-model/entity-model.model';
import { EntityModelAddDto } from 'src/app/share/models/entity-model/entity-model-add-dto.model';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Location } from '@angular/common';
import * as ClassicEditor from 'ng-ckeditor5-classic';
import { environment } from 'src/environments/environment';
import { CKEditor5 } from '@ckeditor/ckeditor5-angular';
// import { OidcSecurityService } from 'angular-auth-oidc-client';
import { AccessModifier } from 'src/app/share/models/enum/access-modifier.model';
import { CodeLanguage } from 'src/app/share/models/enum/code-language.model';

@Component({
    selector: 'app-add',
    templateUrl: './add.component.html',
    styleUrls: ['./add.component.css']
})
export class AddComponent implements OnInit {
    public editorConfig!: CKEditor5.Config;
  public editor: CKEditor5.EditorConstructor = ClassicEditor;
  AccessModifier = AccessModifier;
CodeLanguage = CodeLanguage;

    formGroup!: FormGroup;
    data = {} as EntityModelAddDto;
    isLoading = true;
    constructor(
        
    // private authService: OidcSecurityService,
        private service: EntityModelService,
        public snb: MatSnackBar,
        private router: Router,
        private route: ActivatedRoute,
        private location: Location
        // public dialogRef: MatDialogRef<AddComponent>,
        // @Inject(MAT_DIALOG_DATA) public dlgData: { id: '' }
    ) {

    }

    get name() { return this.formGroup.get('name'); }
    get comment() { return this.formGroup.get('comment'); }
    get accessModifier() { return this.formGroup.get('accessModifier'); }
    get codeExample() { return this.formGroup.get('codeExample'); }
    get codeLanguage() { return this.formGroup.get('codeLanguage'); }


  ngOnInit(): void {
    this.initForm();
    this.initEditor();
    // TODO:?????????????????????????????????????????????
    this.isLoading = false;
  }
    initEditor(): void {
    this.editorConfig = {
      // placeholder: '??????????????????????????????????????????????????????Word???????????????',
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
  initForm(): void {
    this.formGroup = new FormGroup({
      name: new FormControl(null, [Validators.maxLength(60)]),
      comment: new FormControl(null, [Validators.maxLength(300)]),
      accessModifier: new FormControl(null, []),
      codeExample: new FormControl(null, [Validators.maxLength(2000)]),
      codeLanguage: new FormControl(null, []),

    });
  }
  getValidatorMessage(type: string): string {
    switch (type) {
      case 'name':
        return this.name?.errors?.['required'] ? 'Name??????' :
          this.name?.errors?.['minlength'] ? 'Name???????????????' :
            this.name?.errors?.['maxlength'] ? 'Name????????????60???' : '';
      case 'comment':
        return this.comment?.errors?.['required'] ? 'Comment??????' :
          this.comment?.errors?.['minlength'] ? 'Comment???????????????' :
            this.comment?.errors?.['maxlength'] ? 'Comment????????????300???' : '';
      case 'accessModifier':
        return this.accessModifier?.errors?.['required'] ? 'AccessModifier??????' :
          this.accessModifier?.errors?.['minlength'] ? 'AccessModifier???????????????' :
            this.accessModifier?.errors?.['maxlength'] ? 'AccessModifier???????????????' : '';
      case 'codeExample':
        return this.codeExample?.errors?.['required'] ? 'CodeExample??????' :
          this.codeExample?.errors?.['minlength'] ? 'CodeExample???????????????' :
            this.codeExample?.errors?.['maxlength'] ? 'CodeExample????????????2000???' : '';
      case 'codeLanguage':
        return this.codeLanguage?.errors?.['required'] ? 'CodeLanguage??????' :
          this.codeLanguage?.errors?.['minlength'] ? 'CodeLanguage???????????????' :
            this.codeLanguage?.errors?.['maxlength'] ? 'CodeLanguage???????????????' : '';

      default:
    return '';
    }
  }

  add(): void {
    if(this.formGroup.valid) {
    const data = this.formGroup.value as EntityModelAddDto;
    this.data = { ...data, ...this.data };
    this.service.add(this.data)
        .subscribe(res => {
            this.snb.open('????????????');
            // this.dialogRef.close(res);
            this.router.navigate(['../index'],{relativeTo: this.route});
        });
    }
  }
  back(): void {
    this.location.back();
  }
}
