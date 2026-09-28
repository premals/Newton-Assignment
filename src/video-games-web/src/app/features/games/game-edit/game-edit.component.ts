import { Component, inject, input, numberAttribute, OnInit, signal } from '@angular/core';
import { NonNullableFormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { HttpErrorResponse } from '@angular/common/http';
import { Router, RouterLink } from '@angular/router';
import { NgbAlert } from '@ng-bootstrap/ng-bootstrap';
import { VideoGameService } from '../../../core/services/video-game.service';
import { UpdateVideoGame, VideoGame } from '../../../core/models/video-game';
import { HasUnsavedChanges } from '../unsaved-changes.guard';

@Component({
  selector: 'app-game-edit',
  imports: [ReactiveFormsModule, RouterLink, NgbAlert],
  templateUrl: './game-edit.component.html',
})
export class GameEditComponent implements OnInit, HasUnsavedChanges {
  private readonly api = inject(VideoGameService);
  private readonly router = inject(Router);
  private readonly fb = inject(NonNullableFormBuilder);

  /** Bound from the `:id` route param via withComponentInputBinding(). */
  readonly id = input.required({ transform: numberAttribute });

  readonly loading = signal(true);
  readonly saving = signal(false);
  readonly error = signal<string | null>(null);
  private version = '';

  readonly form = this.fb.group({
    title:       ['', [Validators.required, Validators.maxLength(200)]],
    platform:    ['', [Validators.required, Validators.maxLength(50)]],
    genre:       ['', [Validators.required, Validators.maxLength(50)]],
    developer:   ['', Validators.maxLength(100)],
    releaseDate: [''],
    price:       [0, [Validators.required, Validators.min(0), Validators.max(999.99)]],
    rating:      this.fb.control<number | null>(null, [Validators.min(0), Validators.max(100)]),
  });

  ngOnInit(): void {
    this.load();
  }

  load(): void {
    this.loading.set(true);
    this.error.set(null);
    this.api.getById(this.id()).subscribe({
      next: game => this.patch(game),
      error: (e: HttpErrorResponse) => {
        this.error.set(e.status === 404 ? 'Game not found.' : 'Could not load game.');
        this.loading.set(false);
      },
    });
  }

  save(): void {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }
    this.saving.set(true);
    this.error.set(null);

    const v = this.form.getRawValue();
    const payload: UpdateVideoGame = {
      ...v,
      developer: v.developer || null,
      releaseDate: v.releaseDate || null,
      version: this.version,
    };

    this.api.update(this.id(), payload).subscribe({
      next: () => {
        this.form.markAsPristine();
        this.router.navigate(['/games']);
      },
      error: (e: HttpErrorResponse) => {
        this.saving.set(false);
        this.error.set(
          e.status === 409 ? 'Someone else changed this game. Reload to see the latest version.'
          : e.status === 400 ? 'Some values were rejected by the server.'
          : 'Save failed.',
        );
      },
    });
  }

  hasUnsavedChanges(): boolean {
    return this.form.dirty && !this.saving();
  }

  isInvalid(name: keyof typeof this.form.controls): boolean {
    const c = this.form.controls[name];
    return c.invalid && c.touched;
  }

  private patch(game: VideoGame): void {
    this.version = game.version;
    this.form.reset({
      title: game.title,
      platform: game.platform,
      genre: game.genre,
      developer: game.developer ?? '',
      releaseDate: game.releaseDate ?? '',
      price: game.price,
      rating: game.rating,
    });
    this.loading.set(false);
  }
}
