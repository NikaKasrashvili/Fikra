import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Post } from '../models/post.model';

@Injectable({
  providedIn: 'root',
})
export class PostsService {
  constructor(private http: HttpClient) {}

  getPosts() {
    return this.http.get<Post[]>(`${environment.webApi}posts/get-all`);
  }

  getPostByID(id: number) {
    return this.http.get<Post>(`${environment.webApi}posts/${id}`)
  }

  deletePost(id: number){
    return this.http.delete(`${environment.webApi}posts/${id}`)
  }
}
