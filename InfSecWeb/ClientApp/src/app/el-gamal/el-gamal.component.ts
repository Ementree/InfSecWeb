import { Component, OnInit } from '@angular/core';
import {RsaParameters} from "../rsa/RsaParameters";
import {environment} from "../../environments/environment";
import {HttpClient} from "@angular/common/http";

@Component({
  selector: 'app-el-gamal',
  templateUrl: './el-gamal.component.html',
  styleUrls: ['./el-gamal.component.css']
})
export class ElGamalComponent implements OnInit {

  P: number;
  A: number;
  X: number;
  Y: number;
  message: string;
  encryptedMessage: string;
  decryptedMessage: string;
  public parameters: any;
  baseUrl = `${environment.apiUrl}/elgamal`;

  constructor(private httpClient: HttpClient) {
  }

  ngOnInit() {
  }

  generateParams() {
    this.httpClient.get<any>(`${this.baseUrl}/generateParams`).subscribe(x =>{
      this.parameters = x;
      this.P = x.p;
      this.A = x.a;
      this.X = x.x;
      this.Y = x.y;
    });
  }

  setParams() {
    this.httpClient.get<any>(`${this.baseUrl}/setParams/${this.P}`, {}).subscribe(x =>{
      this.parameters = x;
      this.A = x.a;
      this.X = x.x;
      this.Y = x.y;
    });
  }

  encrypt() {
    this.httpClient.post<string>(`${this.baseUrl}/encrypt`, {
      P: this.P,
      A: this.A,
      Y: this.Y,
      message: this.message})
      .subscribe(data => {
        this.encryptedMessage = data;
      });
  }

  decrypt() {
    this.httpClient.post<string>(`${this.baseUrl}/decrypt`, {
      P: this.P,
      X: this.X,
      message: this.encryptedMessage})
      .subscribe(data => {
        this.decryptedMessage = data;
      });
  }

  changeP(event) {
    this.P = event.target.value;
  }

  changeMessage(event) {
    this.message = event.target.value;
  }
}

