import { Routes } from '@angular/router';
import { unsavedChangesGuard } from './features/games/unsaved-changes.guard';

export const routes: Routes = [
  { path: '', pathMatch: 'full', redirectTo: 'games' },
  {
    path: 'games',
    title: 'Video Games',
    loadComponent: () => import('./features/games/game-list/game-list.component').then(m => m.GameListComponent),
  },
  {
    path: 'games/:id',
    title: 'Edit Game',
    loadComponent: () => import('./features/games/game-edit/game-edit.component').then(m => m.GameEditComponent),
    canDeactivate: [unsavedChangesGuard],
  },
  { path: '**', redirectTo: 'games' },
];
