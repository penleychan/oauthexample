import { Component, OnInit } from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {AuthService} from '../services/auth.service';

@Component({
  selector: 'app-call-api',
  templateUrl: './call-api.component.html',
  styleUrls: ['./call-api.component.css']
})
export class CallApiComponent implements OnInit {
  response: string;
  constructor(private http: HttpClient,
              private authService: AuthService) { }

  ngOnInit() {
    const header = new HttpHeaders().set('Authorization', this.authService.getAuthorizationHeaderValue());

    this.http.get('http://localhost:5000/api/values', { headers: header })
      .subscribe(response => this.response = JSON.stringify(response));
  }

}
