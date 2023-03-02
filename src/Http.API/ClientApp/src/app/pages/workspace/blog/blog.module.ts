import { NgModule } from '@angular/core';
import { BlogRoutingModule } from './blog-routing.module';
import { ShareModule } from 'src/app/share/share.module';
import { ComponentsModule } from 'src/app/components/components.module';
import { IndexComponent } from './index/index.component';
import { DetailComponent } from './detail/detail.component';
import { AddComponent } from './add/add.component';
import { EditComponent } from './edit/edit.component';
import { SettingComponent } from './setting/setting.component';
import { CatalogModule } from './catalog/catalog.module';
import { TagModule } from './tag/tag.module';

@NgModule({
  declarations: [IndexComponent, DetailComponent, AddComponent, EditComponent, SettingComponent],
  imports: [
    ComponentsModule,
    ShareModule,
    BlogRoutingModule,
    CatalogModule,
    TagModule,
  ]
})
export class BlogModule { }