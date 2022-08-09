import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { Post } from 'src/app/models/post.model';
import { PostsService } from 'src/app/services/posts.service';

@Component({
  selector: 'app-posts',
  templateUrl: './posts.component.html',
  styleUrls: ['./posts.component.scss'],
})
export class PostsComponent implements OnInit, OnDestroy {
  posts: Post[];
  subs: Subscription []=[];

  constructor(private postService: PostsService) {}

  ngOnInit(): void {
    this.subs.push(
      this.postService.getPosts().subscribe((res) => {
        this.posts = res;
        console.log(res)
      })
    )
    
  }

  ngOnDestroy() : void{
    this.subs.forEach(x=>x.unsubscribe());
  }
}
