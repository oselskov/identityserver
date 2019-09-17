OnRedirectToIdentityProviderForSignOut = (context) =>
{
    var logoutUri = $"{settings.Authority}/v2/logout?client_id={settings.ClientId}";

    var postLogoutUri = context.Properties.RedirectUri;
    if (!string.IsNullOrEmpty(postLogoutUri))
    {
        if (postLogoutUri.StartsWith("/"))
        {
            // transform to absolute
            var request = context.Request;
            postLogoutUri = request.Scheme + "://" + request.Host + request.PathBase + postLogoutUri;
        }
        logoutUri += $"&returnTo={ Uri.EscapeDataString(postLogoutUri)}";
    }

    context.Response.Redirect(logoutUri);
    context.HandleResponse();

    return Task.CompletedTask;
}