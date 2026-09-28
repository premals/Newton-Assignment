import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { GameQuery, PagedResult, UpdateVideoGame, VideoGame } from '../models/video-game';

@Injectable({ providedIn: 'root' })
export class VideoGameService {
  private readonly http = inject(HttpClient);
  private readonly baseUrl = `${environment.apiUrl}/video-games`;

  getPage(query: GameQuery): Observable<PagedResult<VideoGame>> {
    const params = new HttpParams({ fromObject: { ...query } });
    return this.http.get<PagedResult<VideoGame>>(this.baseUrl, { params });
  }

  getById(id: number): Observable<VideoGame> {
    return this.http.get<VideoGame>(`${this.baseUrl}/${id}`);
  }

  update(id: number, game: UpdateVideoGame): Observable<VideoGame> {
    return this.http.put<VideoGame>(`${this.baseUrl}/${id}`, game);
  }
}
