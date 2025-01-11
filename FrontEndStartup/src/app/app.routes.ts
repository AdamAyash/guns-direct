import { Routes } from '@angular/router';
import { HomePage } from './shared/pages/home-page/home.page';
import { ProductsListPage } from './shared/pages/products-list-page/products-list.page';
import { ProductDetailsPage } from './shared/pages/product-details-page/product-details.page';
import { LoginPage } from './shared/pages/login-page/login-page';
import { AutenticationhGuard } from './core/route-guards/authentication-guard';
import { RegisterPage } from './shared/pages/register-page/register.page';

export const routes: Routes = [
   
    {  path: 'home',     component: HomePage, },
    { path: 'products-list', component: ProductsListPage, canActivate: [AutenticationhGuard] 
        ,children: [
            { path: 'product-details', component: ProductDetailsPage},
        ]
    },
    { path: 'product-details/:id', component: ProductDetailsPage},
    {path: 'login', component: LoginPage},
    {path: 'register', component: RegisterPage},
    { path: '',   redirectTo: '/home', pathMatch: 'full' },
];
