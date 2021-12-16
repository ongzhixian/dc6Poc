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


## References

https://nces.ed.gov/pubs98/safetech/chapter8.asp

https://stackoverflow.com/questions/40511103/using-the-antiforgery-cookie-in-asp-net-core-but-with-a-non-default-cookiename
https://long2know.com/2016/03/asp-net-anti-forgery-configuration/
