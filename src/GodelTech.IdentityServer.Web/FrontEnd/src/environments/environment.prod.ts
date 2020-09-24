export const environment = {
    ResourceServer: "https://localhost:5001/",
    IssuerUri: "https://localhost:5001",
    Uri: "https://localhost:5001",
    production: true,
    RequireHttps: true,
    OAuthClientName: "spa",
    RequiredOAuthScopes: "openid profile",
    OAuthResponseType: "code",
    RedirectPath: "/login-callback",
    SilentRefreshRedirectPath: "/silent-refresh.html",
};
