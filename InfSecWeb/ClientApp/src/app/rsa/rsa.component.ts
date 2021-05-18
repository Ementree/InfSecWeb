import { Component, OnInit } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {environment} from "../../environments/environment";
import {RsaParameters} from "./RsaParameters";

@Component({
  selector: 'app-rsa',
  templateUrl: './rsa.component.html',
  styleUrls: ['./rsa.component.css']
})
export class RsaComponent implements OnInit {

  constructor(private httpClient: HttpClient) {
  }


  ngOnInit() {

  }
  P: number;
  Q: number;
  E: number;
  D: number;

  public parameters: RsaParameters;
  public message: number;
  public encryptedMessage: string;
  public decryptedMessage: string;

  baseUrl = `${environment.apiUrl}/rsa`;
  generateParams(){
    this.httpClient.get<RsaParameters>(`${this.baseUrl}/generateParams`).subscribe(x =>{
      this.parameters = x;
      this.P = x.p;
      this.Q = x.q;
      this.E = x.e;
      if (!x.error)
        this.D = x.d;
    });
  }
  setParams(){
    this.httpClient.post<RsaParameters>(`${this.baseUrl}/setParams`, {
      P: this.P,
      Q: this.Q,
      E: this.E
    }).subscribe(data => {
      this.parameters = data;
      this.D = data.d;
    });
  }

  encrypt(){
    this.httpClient.post<string>(`${this.baseUrl}/encrypt`, {
      P: this.P,
      Q: this.Q,
      E: this.E,
      message: this.message})
      .subscribe(data => {
        this.encryptedMessage = data;
      });
  }

  decrypt(){
    this.httpClient.post<string>(`${this.baseUrl}/decrypt`, {
      P: this.P,
      Q: this.Q,
      D: this.D,
      message: this.encryptedMessage})
      .subscribe(data => {
        this.decryptedMessage = data;
      });
  }

  changeP(event){
    this.P = event.target.value;
  }

  changeQ(event){
    this.Q = event.target.value;
  }

  changeE(event){
    this.E = event.target.value;
  }

  changeMessage(event){
    this.message = event.target.value;
  }

  clear(){
    this.P = null;
    this.E = null;
    this.Q = null;
    this.D = null;
    this.message = null;
    this.encryptedMessage = null;
    this.decryptedMessage = null;
  }
}
