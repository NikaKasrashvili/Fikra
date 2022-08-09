import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';
import { Post } from 'src/app/models/post.model';
import { PostCommentService } from 'src/app/services/post-comment.service';
import { PostsService } from 'src/app/services/posts.service';

@Component({
  selector: 'app-post-details',
  templateUrl: './post-details.component.html',
  styleUrls: ['./post-details.component.scss'],
})
export class PostDetailsComponent implements OnInit, OnDestroy {
  post: Post;
  postID: number;
  comments: Comment[];

  subs: Subscription[] = [];

  constructor(
    private postService: PostsService,
    private route: ActivatedRoute,
    private commentService: PostCommentService
  ) {
    this.postID = this.route.snapshot.params['id'];
  }

  ngOnInit(): void {
    this.subs.push(
      this.postService.getPostByID(this.postID).subscribe((res) => {
        this.post = res;
      })
    );
    this.subs.push(
      this.commentService.getByPostId(this.postID).subscribe((res) => {
        this.comments = res;
      })
    );
  }
  
  ngOnDestroy(): void {
    this.subs.forEach((x) => x.unsubscribe());
  }
}
