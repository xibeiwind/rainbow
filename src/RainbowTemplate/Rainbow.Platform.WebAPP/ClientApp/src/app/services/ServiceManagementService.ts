import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { stringify } from 'qs';
import { getHttpOptions } from './httpOptions';

@Injectable()
export class ServiceManagementService {
  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }


}