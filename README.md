# AspNetCoreWebAPI-JWT-Authorize
Asp.Net Core WebAPI with JWT Authorization.

初始化 Asp.Net Core WebAPI 项目

### 集成JWT权限验证
### 集成Swagger接口控制台

## JWT 密钥配置在appsettings.json中
```
{
  "Logging": {
    "LogLevel": {
      "Default": "Warning"
    }
  },
  "AllowedHosts": "*",
  "SecurityKey": "B8E01701-5125-4B69-AB00-7730F5BFB3CF", //JWT SecurityKey
  "Domain": "yourdomain.com" //iss and aud
}
```

# clone项目，修改密钥后可直接使用

在需要权限验证的Controller 或者 Action 上添加[Authorize]标签或者指定角色的[Authorize(Roles = "Client,Admin")]标签即可
```
  [HttpGet]
  [Authorize]//[Authorize(Roles = "Client,Admin")]
  public ActionResult<IEnumerable<string>> Get()
  {
      return new string[] { "value1", "value2" };
  }
```
