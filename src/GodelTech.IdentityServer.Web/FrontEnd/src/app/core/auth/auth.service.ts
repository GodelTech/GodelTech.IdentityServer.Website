import { Injectable } from "@angular/core";
import { Router } from "@angular/router";
import { OAuthErrorEvent, OAuthService, OAuthSuccessEvent } from "angular-oauth2-oidc";
import { BehaviorSubject, combineLatest, defer, from, Observable, of, ReplaySubject } from "rxjs";
import { filter, map, share, tap } from "rxjs/operators";
import { UserProfile } from "@models/user-management-api.models";

@Injectable({ providedIn: "root" })
export class AuthService {
    private userProfileSubject$: BehaviorSubject<UserProfile> = new BehaviorSubject<UserProfile>(null);
    private readonly oAuthUserInfoObservable: Observable<UserProfile>;

    private loadDiscoveryDocumentAndTryLoginObservable: Observable<any>;
    private discoveryDocument: any;

    private isAuthenticatedSubject$ = new BehaviorSubject<boolean>(false);
    public isAuthenticated$ = this.isAuthenticatedSubject$.asObservable();

    private isDoneLoadingSubject$ = new ReplaySubject<boolean>();
    public isDoneLoading$ = this.isDoneLoadingSubject$.asObservable();

    public canActivateProtectedRoutes$: Observable<boolean> = combineLatest([
        this.isAuthenticated$,
        this.isDoneLoading$,
    ]).pipe(map((values) => values.every((b) => b)));

    private navigateToLoginPage() {
        return this.router.navigateByUrl("/login");
    }

    constructor(private oauthService: OAuthService, private router: Router) {
        this.oAuthUserInfoObservable = defer(() => from(this.oauthService.loadUserProfile()))
            .pipe(share())
            .pipe(
                map(
                    (profile) =>
                        ({
                            id: profile["sub"],
                            email: profile["email"],
                            name: profile["name"],
                            userName: profile["preferred_username"],
                        } as UserProfile),
                ),
                tap((profile) => {
                    this.userProfileSubject$.next(profile);
                }),
            );

        this.loadDiscoveryDocumentAndTryLoginObservable = defer(() => from(this.oauthService.loadDiscoveryDocument()))
            .pipe(share())
            .pipe(tap((discoveryDocument) => (this.discoveryDocument = discoveryDocument)))
            .pipe(map(() => this.discoveryDocument));

        this.oauthService.events.subscribe((event) => {
            if (event instanceof OAuthErrorEvent) {
                console.error(event);
            } else {
                console.warn(event);
            }

            if (event instanceof OAuthSuccessEvent) {
                this.isAuthenticatedSubject$.next(this.oauthService.hasValidAccessToken());
            }
        });

        window.addEventListener("storage", (event) => {
            // The `key` is `null` if the event was caused by `.clear()`
            if (event.key !== "access_token" && event.key !== null) {
                return;
            }

            console.warn("Noticed changes to access_token (most likely from another tab), updating isAuthenticated");
            this.isAuthenticatedSubject$.next(this.oauthService.hasValidAccessToken());

            if (!this.oauthService.hasValidAccessToken()) {
                this.navigateToLoginPage();
            }
        });

        this.oauthService.events
            .pipe(filter((e) => ["token_received"].includes(e.type)))
            .subscribe((e) => this.oauthService.loadUserProfile());

        this.oauthService.events
            .pipe(filter((e) => ["session_terminated", "session_error"].includes(e.type)))
            .subscribe((e) => this.navigateToLoginPage());

        this.oauthService.setupAutomaticSilentRefresh();
    }

    public runInitialLoginSequence(): Promise<void> {
        if (location.hash) {
            console.log("Encountered hash fragment, plotting as table...");
            console.table(
                location.hash
                    .substr(1)
                    .split("&")
                    .map((kvp) => kvp.split("=")),
            );
        }

        return this.oauthService
            .loadDiscoveryDocument()
            .then(() => new Promise((resolve) => setTimeout(() => resolve(), 1000)))
            .then(() => this.oauthService.tryLogin())
            .then(() => {
                if (this.oauthService.hasValidAccessToken()) {
                    return Promise.resolve();
                }

                return this.oauthService
                    .silentRefresh()
                    .then(() => Promise.resolve())
                    .catch((result) => {
                        const errorResponsesRequiringUserInteraction = [
                            "interaction_required",
                            "login_required",
                            "account_selection_required",
                            "consent_required",
                        ];

                        if (
                            result &&
                            result.reason &&
                            errorResponsesRequiringUserInteraction.indexOf(result.reason.error) >= 0
                        ) {
                            console.warn(
                                "User interaction is needed to log in, we will wait for the user to manually log in.",
                            );
                            return Promise.resolve();
                        }

                        return Promise.reject(result);
                    });
            })

            .then(() => {
                this.isDoneLoadingSubject$.next(true);

                if (
                    this.oauthService.state &&
                    this.oauthService.state !== "undefined" &&
                    this.oauthService.state !== "null"
                ) {
                    let stateUrl = this.oauthService.state;
                    if (stateUrl.startsWith("/") === false) {
                        stateUrl = decodeURIComponent(stateUrl);
                    }
                    console.log(`There was state of ${this.oauthService.state}, so we are sending you to: ${stateUrl}`);
                    this.router.navigateByUrl(stateUrl);
                }
            })
            .catch(() => this.isDoneLoadingSubject$.next(true));
    }

    public login(targetUrl?: string) {
        this.oauthService.initLoginFlow(targetUrl || this.router.url);
    }

    public logout() {
        this.userProfileSubject$.next(null);
        this.oauthService.logOut();
    }

    public refresh() {
        return this.oauthService.silentRefresh();
    }

    public hasValidToken() {
        return this.oauthService.hasValidAccessToken();
    }

    public get accessToken() {
        return this.oauthService.getAccessToken();
    }

    public get refreshToken() {
        return this.oauthService.getRefreshToken();
    }

    public get identityClaims() {
        return this.oauthService.getIdentityClaims();
    }

    public get idToken() {
        return this.oauthService.getIdToken();
    }

    public get logoutUrl() {
        return this.oauthService.logoutUrl;
    }

    public getUserProfile(): Observable<UserProfile> {
        return this.userProfileSubject$.asObservable();
    }

    public getUserClaims(): object {
        return this.oauthService.getIdentityClaims();
    }
}
