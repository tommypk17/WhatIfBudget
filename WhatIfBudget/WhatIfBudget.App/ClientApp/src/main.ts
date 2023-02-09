import { enableProdMode } from '@angular/core';
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';

import { AppModule } from './app/app.module';
import { environment } from './environments/environment';
import { InjectionToken } from '@angular/core';
export interface AppConfig {
  clientId: string,
  authority: string,
  redirectUri: string,
  knownAuthorities: string
  defaultScope: string
}

if (environment.production) {

  fetch('.info')
    .then((res) => res.json())
    .then((config: AppConfig) => {
      enableProdMode();
      environment.AzureAd.clientId = config.clientId;
      environment.AzureAd.authority = config.authority;
      environment.AzureAd.redirectUri = config.redirectUri;
      environment.AzureAd.knownAuthorities = config.knownAuthorities;
      environment.AzureAd.defaultScope = config.defaultScope;

      platformBrowserDynamic().bootstrapModule(AppModule)
        .catch(err => console.error(err));
    });
} else {
  platformBrowserDynamic().bootstrapModule(AppModule)
    .catch(err => console.error(err));
}
