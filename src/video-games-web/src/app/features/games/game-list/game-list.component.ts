import { Component, computed, inject, signal } from '@angular/core';
import { toObservable, toSignal } from '@angular/core/rxjs-interop';
import { CurrencyPipe, DatePipe } from '@angular/common';
import { RouterLink } from '@angular/router';
import { NgbPagination } from '@ng-bootstrap/ng-bootstrap';
import { catchError, debounceTime, map, of, startWith, switchMap } from 'rxjs';
import { VideoGameService } from '../../../core/services/video-game.service';
import { GameQuery, SortField } from '../../../core/models/video-game';

@Component({
  selector: 'app-game-list',
  imports: [RouterLink, NgbPagination, CurrencyPipe, DatePipe],
  templateUrl: './game-list.component.html',
})
export class GameListComponent {
  private readonly api = inject(VideoGameService);

  readonly search = signal('');
  readonly sortBy = signal<SortField>('title');
  readonly descending = signal(false);
  readonly page = signal(1);
  readonly pageSize = 10;

  private readonly query = computed<GameQuery>(() => ({
    search: this.search(),
    sortBy: this.sortBy(),
    descending: this.descending(),
    page: this.page(),
    pageSize: this.pageSize,
  }));

  readonly state = toSignal(
    toObservable(this.query).pipe(
      debounceTime(250),
      switchMap(q =>
        this.api.getPage(q).pipe(
          map(result => ({ loading: false, error: false, result })),
          startWith({ loading: true, error: false, result: null }),
          catchError(() => of({ loading: false, error: true, result: null })),
        ),
      ),
    ),
    { initialValue: { loading: true, error: false, result: null } },
  );

  onSearch(value: string): void {
    this.search.set(value);
    this.page.set(1);
  }

  sort(field: SortField): void {
    if (this.sortBy() === field) {
      this.descending.update(d => !d);
    } else {
      this.sortBy.set(field);
      this.descending.set(false);
    }
  }
}
