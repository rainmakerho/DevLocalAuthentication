# Dev Local Authentication

Package that reads account data from appsettings.json and automatically performs login for convenient local development.

## Features

-	When the Dev.Local section is configured in appsettings.json, the authentication will be enabled.
-   Read the Claim data from the `Dev.Local` section of `appsettings.json` and set it into the ClaimsIdentity.

## Installation

You can install the Dev Local Authentication library via NuGet Package Manager Console:

```bash
NuGet\Install-Package DevLocalAuthentication
```

## Usage

Here's how you can use the library to auto login:

##### appsettings.json
```json
"Dev.Local": {
    "Claims": {
      "sub": "bbf6ea7e-c072-4e74-b359-4c51b8eb5442",
      "NameIdentifier": "bbf6ea7e-c072-4e74-b359-4c51b8eb5442",
      "Name": "x1@gss.com.tw",
      "preferred_username": "x1@gss.com.tw",
      "Email": "x1@gss.com.tw"
    },
    "Enable":"true"
    //  ,
    //"AuthenticationType": "Dev.Local"
    //"AuthenticationType": "Identity.Application"
    //"AuthenticationType": "whateveryouwant"
  }
```

##### Middleware
```csharp

// before app.UseAuthorization();
app.UseDevLocalAuthentication();


```

## ChangeLog
### v1.0.1
1. [breaking] Change appsettings.json `Dev.Local` section
2. Use `app.UseDevLocalAuthentication()` instead of `context.Services.AddDevLocalAuthentication();`

Use a instead of b

## Contributing

Contributions are welcome!
If you find any issues or have suggestions for improvements, feel free to open an issue or submit a pull request.

## License

[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)
