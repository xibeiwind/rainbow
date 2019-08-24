import { Injectable } from '@angular/core';

@Injectable()
export class InputTypeService {
  inputType = {};

  constructor() {
    this.inputType[System.ComponentModel.DataAnnotations.DataType.DateTime] = 'date';
    this.inputType[System.ComponentModel.DataAnnotations.DataType.Date] = 'datetime';
    this.inputType[System.ComponentModel.DataAnnotations.DataType.Time] = 'time';
    this.inputType[System.ComponentModel.DataAnnotations.DataType.Duration] = 'range';
    this.inputType[System.ComponentModel.DataAnnotations.DataType.PhoneNumber] = 'tel';
    this.inputType[System.ComponentModel.DataAnnotations.DataType.Currency] = 'text';
    this.inputType[System.ComponentModel.DataAnnotations.DataType.Text] = 'text';
    this.inputType[System.ComponentModel.DataAnnotations.DataType.Html] = 'textarea';
    this.inputType[System.ComponentModel.DataAnnotations.DataType.MultilineText] = 'textarea';
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

  getInputType(field: Rainbow.ViewModels.FieldDisplayVM): string {
    if (this.inputType.hasOwnProperty(field.DataType)) {
      return this.inputType[field.DataType];
    }
    if (this.inputType.hasOwnProperty(field.FieldType)) {
      return this.inputType[field.FieldType];
    }

    return 'text';
  }
}
