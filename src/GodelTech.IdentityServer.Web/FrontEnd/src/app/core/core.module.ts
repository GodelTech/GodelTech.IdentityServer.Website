import { HttpClientModule } from "@angular/common/http";
import { ModuleWithProviders, NgModule, Optional, SkipSelf } from "@angular/core";
import { environment } from "@env/environment";
import { AuthConfig, OAuthModule, OAuthModuleConfig, OAuthStorage } from "angular-oauth2-oidc";
import { AuthGuardAuthenticatedOnly } from "./auth/auth-guard-authenticated-only.service";
import { AuthGuardOnlyAdmin } from "./auth/auth-guard-only-admin.service";
import { AuthService } from "./auth/auth.service";
import { throwIfAlreadyLoaded } from "./module-import-guard";

export function storageFactory(): OAuthStorage {
    return localStorage;
}

@NgModule({
    imports: [HttpClientModule, OAuthModule.forRoot()],
    providers: [AuthService, AuthGuardOnlyAdmin, AuthGuardAuthenticatedOnly],
    declarations: [],
    exports: [],
})
export class CoreModule {
    static forRoot(): ModuleWithProviders<CoreModule> {
        return {
            ngModule: CoreModule,
            providers: [
                {
                    provide: AuthConfig,
                    useValue: {
                        issuer: environment.IssuerUri,
                        clientId: environment.OAuthClientName,
                        requireHttps: environment.RequireHttps,
                        redirectUri: environment.Uri + environment.RedirectPath,
                        scope: environment.RequiredOAuthScopes,
                        responseType: environment.OAuthResponseType,
                        silentRefreshRedirectUri: window.location.origin + environment.SilentRefreshRedirectPath,
                        useSilentRefresh: true, // Needed for Code Flow to suggest using iframe-based refreshes
                        sessionChecksEnabled: true,
                        showDebugInformation: true, // Also requires enabling "Verbose" level in devtools
                        clearHashAfterLogin: false,
                        nonceStateSeparator: "semicolon", // Real semicolon gets mangled by IdentityServer's URI encoding
                    },
                },
                {
                    provide: OAuthModuleConfig,
                    useValue: {
                        resourceServer: {
                            allowedUrls: [environment.ResourceServer],
                            sendAccessToken: true,
                        },
                    },
                },
                { provide: OAuthStorage, useFactory: storageFactory },
            ],
        };
    }

    constructor(@Optional() @SkipSelf() parentModule: CoreModule) {
        throwIfAlreadyLoaded(parentModule, "CoreModule");
    }
}
