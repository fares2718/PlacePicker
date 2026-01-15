import { bootstrapApplication } from '@angular/platform-browser';

import { AppComponent } from './app/app.component';
import {
  HttpHandlerFn,
  HttpRequest,
  provideHttpClient,
  withInterceptors,
} from '@angular/common/http';

function logginIterseptor(request: HttpRequest<unknown>, next: HttpHandlerFn) {
  console.log('[OutGoinng Request]', request);
  return next(request);
}

bootstrapApplication(AppComponent, {
  providers: [provideHttpClient(withInterceptors([logginIterseptor]))],
}).catch((err) => console.error(err));
