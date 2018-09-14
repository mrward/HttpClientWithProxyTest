**Windows**

```
Url: https://go.microsoft.com/fwlink/?LinkID=288859
GetCredential. AuthType=digest Uri=http://localhost:8888/
GetCredential. AuthType=basic Uri=http://localhost:8888/
GetCredential. AuthType=basic Uri=http://localhost:8888/
OK

Url: http://go.microsoft.com/fwlink/?LinkID=288859
GetCredential. AuthType=digest Uri=http://localhost:8888/
GetCredential. AuthType=basic Uri=http://localhost:8888/
GetCredential. AuthType=basic Uri=http://localhost:8888/
```

**Mac**

```
Url: https://go.microsoft.com/fwlink/?LinkID=288859
GetCredential. AuthType=basic Uri=http://localhost:8888/
GetCredential. AuthType=basic Uri=http://localhost:8888/
OK

Url: http://go.microsoft.com/fwlink/?LinkID=288859
GetCredential. AuthType=basic Uri=http://go.microsoft.com/fwlink/?LinkID=288859
Failed: ProxyAuthenticationRequired
```

**Mono Tests**

Mono 5.12.0.301 (2018-02/4fe3280bba1 Fri Jul 20 08:25:42 EDT 2018) - Fail
Mono 5.14.0.177 (2018-04/f3a2216b65a Fri Aug  3 09:28:16 EDT 2018) - Fail
Mono 5.16.0.151 (2018-06/5e81afa90df Fri Sep  7 12:05:47 EDT 2018) - Fail


