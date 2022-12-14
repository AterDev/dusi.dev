import { Component, Inject, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { UserService } from 'src/app/share/services/user.service';
import { User } from 'src/app/share/models/user/user.model';
import { UserAddDto } from 'src/app/share/models/user/user-add-dto.model';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Location } from '@angular/common';
import { SexType } from 'src/app/share/models/enum/sex-type.model';

@Component({
    selector: 'app-add',
    templateUrl: './add.component.html',
    styleUrls: ['./add.component.css']
})
export class AddComponent implements OnInit {
    SexType = SexType;

    formGroup!: FormGroup;
    data = {} as UserAddDto;
    isLoading = true;
    constructor(
        
        private service: UserService,
        public snb: MatSnackBar,
        private router: Router,
        private route: ActivatedRoute,
        private location: Location
        // public dialogRef: MatDialogRef<AddComponent>,
        // @Inject(MAT_DIALOG_DATA) public dlgData: { id: '' }
    ) {

    }

    get userName() { return this.formGroup.get('userName'); }
    get realName() { return this.formGroup.get('realName'); }
    get email() { return this.formGroup.get('email'); }
    get emailConfirmed() { return this.formGroup.get('emailConfirmed'); }
    get phoneNumber() { return this.formGroup.get('phoneNumber'); }
    get phoneNumberConfirmed() { return this.formGroup.get('phoneNumberConfirmed'); }
    get twoFactorEnabled() { return this.formGroup.get('twoFactorEnabled'); }
    get lockoutEnd() { return this.formGroup.get('lockoutEnd'); }
    get lockoutEnabled() { return this.formGroup.get('lockoutEnabled'); }
    get accessFailedCount() { return this.formGroup.get('accessFailedCount'); }
    get lastLoginTime() { return this.formGroup.get('lastLoginTime'); }
    get retryCount() { return this.formGroup.get('retryCount'); }
    get avatar() { return this.formGroup.get('avatar'); }
    get idNumber() { return this.formGroup.get('idNumber'); }
    get sex() { return this.formGroup.get('sex'); }


  ngOnInit(): void {
    this.initForm();
    
    // TODO:?????????????????????????????????????????????
    this.isLoading = false;
  }
  
  initForm(): void {
    this.formGroup = new FormGroup({
      userName: new FormControl(null, []),
      realName: new FormControl(null, [Validators.maxLength(30)]),
      email: new FormControl(null, [Validators.maxLength(100)]),
      emailConfirmed: new FormControl(null, []),
      phoneNumber: new FormControl(null, [Validators.maxLength(20)]),
      phoneNumberConfirmed: new FormControl(null, []),
      twoFactorEnabled: new FormControl(null, []),
      lockoutEnd: new FormControl(null, []),
      lockoutEnabled: new FormControl(null, []),
      accessFailedCount: new FormControl(null, []),
      lastLoginTime: new FormControl(null, []),
      retryCount: new FormControl(null, []),
      avatar: new FormControl(null, [Validators.maxLength(200)]),
      idNumber: new FormControl(null, [Validators.maxLength(18)]),
      sex: new FormControl(null, []),

    });
  }
  getValidatorMessage(type: string): string {
    switch (type) {
      case 'userName':
        return this.userName?.errors?.['required'] ? 'UserName??????' :
          this.userName?.errors?.['minlength'] ? 'UserName???????????????' :
            this.userName?.errors?.['maxlength'] ? 'UserName???????????????' : '';
      case 'realName':
        return this.realName?.errors?.['required'] ? 'RealName??????' :
          this.realName?.errors?.['minlength'] ? 'RealName???????????????' :
            this.realName?.errors?.['maxlength'] ? 'RealName????????????30???' : '';
      case 'email':
        return this.email?.errors?.['required'] ? 'Email??????' :
          this.email?.errors?.['minlength'] ? 'Email???????????????' :
            this.email?.errors?.['maxlength'] ? 'Email????????????100???' : '';
      case 'emailConfirmed':
        return this.emailConfirmed?.errors?.['required'] ? 'EmailConfirmed??????' :
          this.emailConfirmed?.errors?.['minlength'] ? 'EmailConfirmed???????????????' :
            this.emailConfirmed?.errors?.['maxlength'] ? 'EmailConfirmed???????????????' : '';
      case 'phoneNumber':
        return this.phoneNumber?.errors?.['required'] ? 'PhoneNumber??????' :
          this.phoneNumber?.errors?.['minlength'] ? 'PhoneNumber???????????????' :
            this.phoneNumber?.errors?.['maxlength'] ? 'PhoneNumber????????????20???' : '';
      case 'phoneNumberConfirmed':
        return this.phoneNumberConfirmed?.errors?.['required'] ? 'PhoneNumberConfirmed??????' :
          this.phoneNumberConfirmed?.errors?.['minlength'] ? 'PhoneNumberConfirmed???????????????' :
            this.phoneNumberConfirmed?.errors?.['maxlength'] ? 'PhoneNumberConfirmed???????????????' : '';
      case 'twoFactorEnabled':
        return this.twoFactorEnabled?.errors?.['required'] ? 'TwoFactorEnabled??????' :
          this.twoFactorEnabled?.errors?.['minlength'] ? 'TwoFactorEnabled???????????????' :
            this.twoFactorEnabled?.errors?.['maxlength'] ? 'TwoFactorEnabled???????????????' : '';
      case 'lockoutEnd':
        return this.lockoutEnd?.errors?.['required'] ? 'LockoutEnd??????' :
          this.lockoutEnd?.errors?.['minlength'] ? 'LockoutEnd???????????????' :
            this.lockoutEnd?.errors?.['maxlength'] ? 'LockoutEnd???????????????' : '';
      case 'lockoutEnabled':
        return this.lockoutEnabled?.errors?.['required'] ? 'LockoutEnabled??????' :
          this.lockoutEnabled?.errors?.['minlength'] ? 'LockoutEnabled???????????????' :
            this.lockoutEnabled?.errors?.['maxlength'] ? 'LockoutEnabled???????????????' : '';
      case 'accessFailedCount':
        return this.accessFailedCount?.errors?.['required'] ? 'AccessFailedCount??????' :
          this.accessFailedCount?.errors?.['minlength'] ? 'AccessFailedCount???????????????' :
            this.accessFailedCount?.errors?.['maxlength'] ? 'AccessFailedCount???????????????' : '';
      case 'lastLoginTime':
        return this.lastLoginTime?.errors?.['required'] ? 'LastLoginTime??????' :
          this.lastLoginTime?.errors?.['minlength'] ? 'LastLoginTime???????????????' :
            this.lastLoginTime?.errors?.['maxlength'] ? 'LastLoginTime???????????????' : '';
      case 'retryCount':
        return this.retryCount?.errors?.['required'] ? 'RetryCount??????' :
          this.retryCount?.errors?.['minlength'] ? 'RetryCount???????????????' :
            this.retryCount?.errors?.['maxlength'] ? 'RetryCount???????????????' : '';
      case 'avatar':
        return this.avatar?.errors?.['required'] ? 'Avatar??????' :
          this.avatar?.errors?.['minlength'] ? 'Avatar???????????????' :
            this.avatar?.errors?.['maxlength'] ? 'Avatar????????????200???' : '';
      case 'idNumber':
        return this.idNumber?.errors?.['required'] ? 'IdNumber??????' :
          this.idNumber?.errors?.['minlength'] ? 'IdNumber???????????????' :
            this.idNumber?.errors?.['maxlength'] ? 'IdNumber????????????18???' : '';
      case 'sex':
        return this.sex?.errors?.['required'] ? 'Sex??????' :
          this.sex?.errors?.['minlength'] ? 'Sex???????????????' :
            this.sex?.errors?.['maxlength'] ? 'Sex???????????????' : '';

      default:
    return '';
    }
  }

  add(): void {
    if(this.formGroup.valid) {
    const data = this.formGroup.value as UserAddDto;
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
