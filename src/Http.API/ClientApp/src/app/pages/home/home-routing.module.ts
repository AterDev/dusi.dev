import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AboutComponent } from './about/about.component';
import { BlogDetailComponent } from './blog-detail/blog-detail.component';
import { BlogComponent } from './blog/blog.component';
import { EntityComponent } from './entity/entity.component';
import { IndexComponent } from './index/index.component';
import { LoginComponent } from './login/login.component';
import { NewsComponent } from './news/news.component';
import { VideoPreviewComponent } from './video-preview/video-preview.component';

const routes: Routes = [
  { path: 'index', component: IndexComponent },
  { path: 'login', component: LoginComponent },
  { path: 'news', component: NewsComponent },
  { path: 'blog', component: BlogComponent },
  { path: 'blog/:id', component: BlogDetailComponent },
  { path: 'entity', component: EntityComponent },
  { path: 'about', component: AboutComponent },
  { path: 'video-preview/:id', component: VideoPreviewComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class HomeRoutingModule { }
