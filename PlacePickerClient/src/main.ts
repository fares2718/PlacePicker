import { bootstrapApplication } from '@angular/platform-browser';

import { AppComponent } from './app/app.component';
import {
  HttpEventType,
  HttpHandlerFn,
  HttpRequest,
  provideHttpClient,
  withInterceptors,
} from '@angular/common/http';
import { tap } from 'rxjs';

function logginIterseptor(request: HttpRequest<unknown>, next: HttpHandlerFn) {
  console.log('[Outgoing Request]', request);
  return next(request).pipe(
    tap((event) => {
      if (event.type === HttpEventType.Response) {
        console.log('[Incomming Request]', event.status, event.body);
      }
    })
  );
}

bootstrapApplication(AppComponent, {
  providers: [provideHttpClient(withInterceptors([logginIterseptor]))],
}).catch((err) => console.error(err));
