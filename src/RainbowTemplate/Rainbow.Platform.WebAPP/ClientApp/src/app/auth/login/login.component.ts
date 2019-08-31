import { Component, OnInit } from '@angular/core';
import { SiteService } from 'src/app/services/site.service';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { ViewModelDisplayService } from '../../services/ViewModelDisplayService';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { AccountService } from '../../services/AccountService';
import { InputTypeService } from '../../services/InputTypeService';

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

  constructor(site: SiteService,
    private service: AccountService,
    private inputTypeService: InputTypeService,
    private displayService: ViewModelDisplayService,
    private router: Router,
    private toastr: ToastrService,
    private formBuilder: FormBuilder) {
    this.site = site.getSite();
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
      return 'html';
    } else {
      return 'input';
    }
  }

  getInputType(field: Rainbow.ViewModels.FieldDisplayVM): string {
    return this.inputTypeService.getInputType(field);
  }

  login() {
    this.service.Login({ ...this.loginForm.value }).subscribe(res => {
      if (res.IsSuccess) {
        localStorage.token = res.Message;
        // goto dashboard
        this.toastr.success('登陆成功');
        this.router.navigate(['/dashboard']);
      } else {
        this.toastr.error(res.Message);
      }

    }, err => {
      localStorage.removeItem('token');
      this.toastr.error(err.error.ErrorMessage);
    });
  }
}
