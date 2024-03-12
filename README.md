# Dev Local Authentication

Package that reads account data from appsettings.json and automatically performs login for convenient local development.

## Features

-	When the Dev.Local section is configured in appsettings.json, the authentication will be enabled.
-   Read the Claim data from the `Dev.Local` section of `appsettings.json` and set it into the ClaimsIdentity.

## Installation

You can install the Molecular Mass Calculator library via NuGet Package Manager Console:

```bash
NuGet\Install-Package DevLocalAuthentication
```

## Usage

Here's how you can use the library to auto login:

##### appsettings.json
```json
"Dev.Local": {
    "sub": "bbf6ea7e-c072-4e74-b359-4c51b8eb5442",
    "NameIdentifier": "bbf6ea7e-c072-4e74-b359-4c51b8eb5442",
    "Name": "x1@gss.com.tw",
    "preferred_username": "x1@gss.com.tw",
    "Email": "x1@gss.com.tw"
}
```

##### ConfigureAuthentication
```csharp
// check Dev.Local section exists, add AddScheme
builder.Services.AddDevLocalAuthentication(builder.Configuration);
```

## ChangeLog

## Contributing

Contributions are welcome!
If you find any issues or have suggestions for improvements, feel free to open an issue or submit a pull request.

## License

[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)
