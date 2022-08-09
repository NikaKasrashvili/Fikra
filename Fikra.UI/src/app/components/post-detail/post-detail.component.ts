import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { PostsService } from 'src/app/services/posts.service';
import { Post } from 'src/app/models/post.model';
import { AuthService } from 'src/app/services/auth.service';
@Component({
  selector: 'app-post-detail',
  templateUrl: './post-detail.component.html',
  styleUrls: ['./post-detail.component.scss'],
})
export class PostDetailComponent implements OnInit {
  postId: number;

  detailedPost: Post;

  constructor(
    private route: ActivatedRoute,
    private postsService: PostsService,
    public authService: AuthService
  ) {
    this.postId = route.snapshot.params['id'];
  }

  ngOnInit(): void {
    this.postsService.getPostByID(this.postId).subscribe((res) => {
      this.detailedPost = res;
    });
  }

  deletePost() {
    this.postsService
      .deletePost(this.postId)
      .subscribe((res) => console.log(res));
  }
}
