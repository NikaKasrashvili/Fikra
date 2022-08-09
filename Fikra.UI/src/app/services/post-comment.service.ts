import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class PostCommentService {

  constructor(private http: HttpClient) { }

  getByPostId(postId: number){
    return this.http.get<Comment[]>(`${environment.webApi}comments/get-comments/${postId}`);
  }
}
