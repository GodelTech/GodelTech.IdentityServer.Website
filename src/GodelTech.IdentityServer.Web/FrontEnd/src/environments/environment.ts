// This file can be replaced during build by using the `fileReplacements` array.
// `ng build ---prod` replaces `environment.ts` with `environment.prod.ts`.
// The list of file replacements can be found in `angular.json`.

export const environment = {
    ResourceServer: "https://localhost:5001/",
    IssuerUri: "https://localhost:5001",
    Uri: "https://localhost:5001",
    production: false,
    RequireHttps: true,
    OAuthClientName: "spa",
    RequiredOAuthScopes: "openid profile",
    OAuthResponseType: "code",
    RedirectPath: "/login-callback",
    SilentRefreshRedirectPath: "/silent-refresh.html",
};

/*
 * In development mode, to ignore zone related error stack frames such as
 * `zone.run`, `zoneDelegate.invokeTask` for easier debugging, you can
 * import the following file, but please comment it out in production mode
 * because it will have performance impact when throw error
 */
// import 'zone.js/dist/zone-error';  // Included with Angular CLI.
