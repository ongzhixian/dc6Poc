# Security



```
    <div class="security-warning">
        <h2>W A R N I N G !</h2>

        <p>This is a restricted network.</p>

        <p>Use of this network, its equipment, and resources is monitored at all times and requires explicit permission from the network administrator.</p>
        <p>If you do not have this permission in writing, you are violating the regulations of this network and can and will be prosecuted to the full extent of the law.</p>
        <p>By continuing into this system, you are acknowledging that you are aware of and agree to these terms.</p>
    </div>
```


# Authorize and Razor Pages CSS

If using `_Layout.cshtml.css`, you may encouner file errors such as:

```
The stylesheet https://localhost:5001/Login?ReturnUrl=%2FDn6Poc.DocuMgmtPortal.styles.css was not loaded because its MIME type, “text/html”, is not “text/css”.
```

## AntiForgeryToken cookie

```
services.AddAntiforgery(opts => opts.Cookie.Name = "MyAntiforgeryCookie");
```

## Token flow testing

Step 1 − First, the client authenticates with the authorization server by giving the authorization grant.

Step 2 − Next, the authorization server authenticates the client, validates the authorization grant and issues the access token and refresh token to the client, if valid.

Step 3 − Then, the client requests the resource server for protected resource by giving the access token.

Step 4 − The resource server validates the access token and provides the protected resource.

Step 5 − The client makes the protected resource request to the resource server by granting the access token, where the resource server validates it and serves the request, if valid. This step keeps on repeating until the access token expires.

Step 6 − If the access token expires, the client authenticates with the authorization server and requests for new access token by providing refresh token. If the access token is invalid, the resource server sends back the invalid token error response to the client.

Step 7 − The client authenticates with the authorization server by granting the refresh token.

Step 8 − The authorization server then validates the refresh token by authenticating the client and issues a new access token, if it is valid.

See: https://www.tutorialspoint.com/oauth2.0/refresh_token.htm

## References

https://nces.ed.gov/pubs98/safetech/chapter8.asp

https://stackoverflow.com/questions/40511103/using-the-antiforgery-cookie-in-asp-net-core-but-with-a-non-default-cookiename
https://long2know.com/2016/03/asp-net-anti-forgery-configuration/


https://www.tutorialspoint.com/oauth2.0/refresh_token.htm

https://curity.io/product/community/
