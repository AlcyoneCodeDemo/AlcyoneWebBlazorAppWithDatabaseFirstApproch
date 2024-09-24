using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;

public class CustomAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly IJSRuntime _jsRuntime;
    //private readonly ILocalStorageService _localStorage;

    public CustomAuthenticationStateProvider(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
 
    }
    public void MarkUserAsAuthenticated(string userName)
    {
        // Store the user's authentication status and username
        var authState = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.Name, userName)
        }, "apiauth")));

        NotifyAuthenticationStateChanged(Task.FromResult(authState));
    }
    public void MarkUserAsLoggedOut()
    {
        var identity = new ClaimsIdentity();
        var user = new ClaimsPrincipal(identity);
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
    }
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        // Retrieve user information from local storage (if any)
        var authUser = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "authUser");

        if (!string.IsNullOrEmpty(authUser))
        {
            // Deserialize the claims data
            var claimsData = JsonSerializer.Deserialize<List<ClaimData>>(authUser);

            // Create ClaimsIdentity from claims data
            var identity = new ClaimsIdentity(claimsData.Select(c => new Claim(c.Type, c.Value)), "authType");
            var user = new ClaimsPrincipal(identity);

            return new AuthenticationState(user);
        }

        // Return an empty ClaimsPrincipal if no user information is found
        return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
    }

    // Helper class for deserializing claims data
    private class ClaimData
    {
        public string Type { get; set; }
        public string Value { get; set; }
    }

}
