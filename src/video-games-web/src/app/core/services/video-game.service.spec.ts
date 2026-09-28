import { TestBed } from '@angular/core/testing';
import { provideHttpClient } from '@angular/common/http';
import { HttpTestingController, provideHttpClientTesting } from '@angular/common/http/testing';
import { VideoGameService } from './video-game.service';

describe('VideoGameService', () => {
  let service: VideoGameService;
  let http: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [provideHttpClient(), provideHttpClientTesting()],
    });
    service = TestBed.inject(VideoGameService);
    http = TestBed.inject(HttpTestingController);
  });

  afterEach(() => http.verify());

  it('sends query params when fetching a page', () => {
    service.getPage({ search: 'zelda', sortBy: 'title', descending: false, page: 2, pageSize: 10 }).subscribe();

    const req = http.expectOne(r => r.url.endsWith('/video-games'));
    expect(req.request.params.get('search')).toBe('zelda');
    expect(req.request.params.get('page')).toBe('2');
    req.flush({ items: [], totalCount: 0, page: 2, pageSize: 10 });
  });

  it('PUTs the update to the item url', () => {
    service.update(5, {
      title: 'Halo', platform: 'Xbox', genre: 'Shooter', developer: null,
      releaseDate: null, price: 10, rating: 80, version: 'abc',
    }).subscribe();

    const req = http.expectOne(r => r.url.endsWith('/video-games/5'));
    expect(req.request.method).toBe('PUT');
    expect(req.request.body.version).toBe('abc');
    req.flush({});
  });
});
