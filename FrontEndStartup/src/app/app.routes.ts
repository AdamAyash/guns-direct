import { Routes } from '@angular/router';
import { HomePage } from './pages/home/home.page';
import { ProductsListPage } from './pages/products-list/products-list.page';
import { ProductDetailsPage } from './pages/product-details/product-details.page';
import { LoginPage } from './pages/login-page/login-page.page';

export const routes: Routes = [
   
    {  path: 'home',     component: HomePage, },
    { path: 'products-list', component: ProductsListPage 
        ,children: [
            { path: 'product-details', component: ProductDetailsPage},
        ]
    },
    { path: 'product-details/:id', component: ProductDetailsPage},
    {path: 'login', component: LoginPage},
    { path: '',   redirectTo: '/home', pathMatch: 'full' },
];
