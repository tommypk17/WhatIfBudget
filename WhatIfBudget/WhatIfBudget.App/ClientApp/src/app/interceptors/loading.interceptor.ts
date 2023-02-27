import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { finalize, Observable, tap } from 'rxjs';
import { SharedService } from '../services/shared.service';

@Injectable()
export class LoadingInterceptor implements HttpInterceptor {

  constructor(private sharedService: SharedService) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    this.sharedService.queueLoading(request.url);
    return next.handle(request).pipe(
      finalize(() => {
        this.sharedService.dequeueLoading(request.url);
      })
    );
  }
}
