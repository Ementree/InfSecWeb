import { Component, OnInit } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {environment} from "../../environments/environment";

@Component({
  selector: 'app-dsa',
  templateUrl: './dsa.component.html',
  styleUrls: ['./dsa.component.css']
})
export class DsaComponent implements OnInit {
  P: string;
  Q: string;
  Y: string;
  G: string;
  X: string;
  message: string;
  R: string;
  S: string;
  R_test: string;
  S_test: string;
  baseUrl = `${environment.apiUrl}/dsa`;
  message_hash: string;
  isCheckTrue: boolean;
  checked: boolean;
  changed_message: string;

  constructor(private httpClient: HttpClient) { }

  ngOnInit() {
  }

  generateParams() {
    this.httpClient.get<any>(`${this.baseUrl}/generateParams`).subscribe(x => {
      this.P = x.p;
      this.Q = x.q;
      this.Y = x.y;
      this.G = x.g;
      this.X = x.x;
      this.checked = false;
    });
  }

  changeMessage(event) {
    this.message = event.target.value;
    this.checked = false;
  }

  sign() {
    this.httpClient.post<any>(`${this.baseUrl}/sign`, {
      P: this.P,
      Q: this.Q,
      G: this.G,
      X: this.X,
      message: this.message})
      .subscribe(data => {
        this.checked = false;
        this.R = data.r;
        this.S = data.s;
        this.R_test = data.r;
        this.S_test = data.s;
        this.message_hash = data.messageHash;
        this.changed_message = data.message;
      });
  }

  changeRTest(event) {
    this.R_test = event.target.value;
    this.checked = false;
  }

  changeSTest(event) {
    this.S_test = event.target.value;
    this.checked = false;
  }

  checkSign() {
    this.httpClient.post<boolean>(`${this.baseUrl}/checkSign`, {
      S: this.S_test,
      R: this.R_test,
      messageHash: this.message_hash,
      changedMessage: this.changed_message,
      Q: this.Q,
      Y: this.Y,
      P: this.P,
      G: this.G,})
      .subscribe(data => {
        this.isCheckTrue = data;
        this.checked = true;
      });
  }

  changeMessageHash(event) {
    this.changed_message = event.target.value;
    this.checked = false;
  }
}
