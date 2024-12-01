import { Routes } from '@angular/router';
import { HomePage } from './pages/home/home.page';
import { ProductsListPage } from './pages/products-list/products-list.page';

export const routes: Routes = [
    { 
        path: 'home', 
        component: HomePage,
        children: [
            { path: 'products-list', component: ProductsListPage },
        ]
    },
    { path: 'products-list', component: ProductsListPage },
    { path: '',   redirectTo: '/home', pathMatch: 'full' },
];
