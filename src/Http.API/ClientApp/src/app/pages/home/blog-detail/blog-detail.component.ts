import { AfterViewInit, Component } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { Blog } from 'src/app/share/client/models/blog/blog.model';
import { BlogService } from 'src/app/share/client/services/blog.service';

import 'prismjs/plugins/line-numbers/prism-line-numbers.js';
import 'prismjs/components/prism-typescript.min.js';
import 'prismjs/components/prism-powershell.min.js';
import 'prismjs/components/prism-csharp.min.js';
import 'prismjs/components/prism-markup.min.js';
import { MatTooltip } from '@angular/material/tooltip';

@Component({
  selector: 'app-blog-detail',
  templateUrl: './blog-detail.component.html',
  styleUrls: ['./blog-detail.component.css'],
})
export class BlogDetailComponent implements AfterViewInit {
  id!: string;
  isLoading = false;
  data = {} as Blog;
  testContent: string = '';
  isCopied = false;
  constructor(
    private service: BlogService,
    private snb: MatSnackBar,
    private route: ActivatedRoute,
    private router: Router,
  ) {

    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.id = id;
    } else {
      // TODO: id为空

    }
  }
  ngOnInit(): void {
    this.getDetail();
  }
  ngAfterViewInit(): void {
  }
  getDetail(): void {
    this.service.getDetail(this.id)
      .subscribe({
        next: (res) => {
          if (res) {
            this.data = res;
            this.isLoading = false;
          }
        },
        error: (error) => {
          this.snb.open(error.detail);
        }
      })
  }

  copyCode(): void {
    this.isCopied = true;
    setTimeout(() => {
      this.isCopied = false;
    }, 1500);
  }
}