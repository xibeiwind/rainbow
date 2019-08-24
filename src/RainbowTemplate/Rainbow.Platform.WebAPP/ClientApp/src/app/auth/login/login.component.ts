import { Component, OnInit } from '@angular/core';
import { SiteService } from 'src/app/services/site.service';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { ViewModelDisplayService } from '../../services/ViewModelDisplayService';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { AccountService } from '../../services/AccountService';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  private loginInfo = {};
  site = { title: '管理彩虹' };
  fields: Rainbow.ViewModels.FieldDisplayVM[];


  loginForm: FormGroup;

  inputIcon = {};

  inputType = {};

  constructor(site: SiteService,
    private service: AccountService,
    private displayService: ViewModelDisplayService,
    private router: Router,
    private toastr: ToastrService,
    private formBuilder: FormBuilder) {
    this.site = site.getSite();

    this.inputType[System.ComponentModel.DataAnnotations.DataType.DateTime] = 'date';
    this.inputType[System.ComponentModel.DataAnnotations.DataType.Date] = 'datetime';
    this.inputType[System.ComponentModel.DataAnnotations.DataType.Time] = 'time';
    this.inputType[System.ComponentModel.DataAnnotations.DataType.Duration] = 'range';
    this.inputType[System.ComponentModel.DataAnnotations.DataType.PhoneNumber] = 'tel';
    this.inputType[System.ComponentModel.DataAnnotations.DataType.Currency] = 'text';
    this.inputType[System.ComponentModel.DataAnnotations.DataType.Text] = 'text';
    this.inputType[System.ComponentModel.DataAnnotations.DataType.Html] = 'text';
    this.inputType[System.ComponentModel.DataAnnotations.DataType.MultilineText] = 'text';
    this.inputType[System.ComponentModel.DataAnnotations.DataType.EmailAddress] = 'email';
    this.inputType[System.ComponentModel.DataAnnotations.DataType.Password] = 'password';
    this.inputType[System.ComponentModel.DataAnnotations.DataType.Url] = 'url';
    this.inputType[System.ComponentModel.DataAnnotations.DataType.ImageUrl] = 'image';
    this.inputType[System.ComponentModel.DataAnnotations.DataType.CreditCard] = 'text';
    this.inputType[System.ComponentModel.DataAnnotations.DataType.PostalCode] = 'text';
    this.inputType[System.ComponentModel.DataAnnotations.DataType.Upload] = 'file';

    this.inputType['text'] = 'text';
    this.inputType['number'] = 'number';
    this.inputType['checkbox'] = 'checkbox';
  }

  ngOnInit() {

    this.displayService.GetVMDisplay({ Name: 'LoginVM' }).subscribe(res => {
      this.fields = res.Data.Fields;
      const formFields = {};
      this.fields.forEach((field: Rainbow.ViewModels.FieldDisplayVM) => {
        formFields[field.Name] = ['', Validators.required];
      });

      this.loginForm = this.formBuilder.group(formFields);

      this.inputIcon['Phone'] = 'glyphicon-phone';
      this.inputIcon['Password'] = 'glyphicon-lock';
    });
  }

  getInputControlType(field: Rainbow.ViewModels.FieldDisplayVM): string {
    if (field.IsEnum) {
      return 'select';
    }
    if (field.DataType === System.ComponentModel.DataAnnotations.DataType.MultilineText) {
      return 'textarea';
    } else {
      return 'input';
    }
  }

  getInputType(field: Rainbow.ViewModels.FieldDisplayVM): string {
    if (this.inputType.hasOwnProperty(field.DataType)) {
      return this.inputType[field.DataType];
    }
    if (this.inputType.hasOwnProperty(field.FieldType)) {
      return this.inputType[field.FieldType];
    }

    return 'text';
  }

  login() {
    this.service.Login({ ...this.loginForm.value }).subscribe(res => {
      localStorage['token'] = res.Message;
      // goto dashboard
      this.toastr.success('登陆成功');
      this.router.navigate(['/dashboard']);
    }, err => {
      localStorage.removeItem('token');
      this.toastr.error(err.error.ErrorMessage);
    });
  }
}
