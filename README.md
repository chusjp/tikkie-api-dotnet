# Tikkie API .NET

[![Build Status](https://dev.azure.com/chusjp/TikkieAPI/_apis/build/status/chusjp.tikkie-api-dotnet?branchName=master)](https://dev.azure.com/chusjp/TikkieAPI/_build/latest?definitionId=1&branchName=master)
![Nuget](https://img.shields.io/nuget/v/TikkieAPI.svg)
[![GitHub license](https://img.shields.io/github/license/Naereen/StrapDown.js.svg)](https://github.com/Naereen/StrapDown.js/blob/master/LICENSE)

Unofficial client implementation for communicating with the **Tikkie API**.

## Prerequisites

**Tikkie API .NET** is a _.NET Standard 2.0_ library that implements the [Tikkie Payment Request API](https://developer.abnamro.com/content/tikkie-payment-request). It follows the technical details described in the [official documentation](https://developer.abnamro.com/api/tikkie-v1/technical-details).

The authentication for this API is implemented through signed _JSON Web Token_ (**JWT**). Every HTTP request contains a header with a token generated after making an authentication request to the API. This client automatically handles the authentication for requests in case they expired. 

The API consumer should follow this steps to be able to generate a token:
1. Sign up on the [Developer ABN-AMRO website](https://developer.abnamro.com/user/register).
1. Add a new App using the API Product **Tikkie**.
1. Create a public/private key pair using _OpenSSL_.
    ```
    #generates RSA private key of 2048 bit size
    openssl genrsa -out private_rsa.pem 2048

    #generates public key from the private key
    openssl rsa -in private_rsa.pem -outform PEM -pubout -out public_rsa.pem
    ```
1. Share the public key created along with your app name and developer email to `api.support@nl.abnamro.com`.

Reference: [Authentication with Signed JSON Web Token](https://developer.abnamro.com/get-started#headingFive)

## Getting started

The package can be installed via NuGet:

```
Install-Package TikkieApi
```

## Usage

### Creating the Tikkie API Client

To start using the client there should be created a new instance of `TikkieClient` class. The API key (a.k.a. Consumer Key) should be provided together with the path of the private RSA Pem file previously created.

```c#
TikkieClient tikkieClient = new TikkieClient(
    apiKey: "MyApIKeY", 
    privateKeyPath: "private_rsa.pem");
```

By default it is connecting to the Tikkie Production environment. But it is possible to use the Sandbox environment on the client creation by setting the constructor's `useTestEnvironment` parameter to `true` (it is `false` by default):

```c#
TikkieClient tikkieClient = new TikkieClient(
    apiKey: "MyApIKeY", 
    privateKeyPath: "private_rsa.pem",
    useTestEnvironment: true);
```

### Platforms

Create a new Platform:

```c#
PlaformResponse response = await tikkieClient.CreatePlatformAsync(
    request: new PlatformRequest
    {
        Name = "New Platform",
        Email = "user@emailaddress.com",                // Optional
        NotificationUrl = "notifyme@emailaddress.com"   // Optional
        PhoneNumber = "06xxxxxxxx",
        Usage = PlatformUsage.PaymentRequestForMyself
    });
```

Retrieve all existing Platforms:

```c#
PlaformResponse[] response = await tikkieClient.GetPlatformsAsync();
```

### Users

Create a new User:

```c#
UserResponse response = await tikkieClient.CreateUserAsync(
    request: new UserRequest
    {
        PlatformToken = "platformToken",
        Name = "Arya Stark",
        PhoneNumber = "06xxxxxxxx",
        IBAN = "NLXXXXXXXXXXXXXXXX",
        BankAccountLabel = "Personal account"
    });
```

Retrieve all Users from an existing Platform:

```c#
UserResponse[] response = await tikkieClient.GetUsersAsync(platformToken: "platformToken");
```

### Payments

Create a new Payment Request for an existing User:

```c#
PaymentResponse response = await tikkieClient.CreatePaymentRequestAsync(
    request: new PaymentRequest
    {
        PlatformToken = "platformToken",
        UserToken = "userToken",
        BankAccountToken = "bankAccountToken",
        AmountInCents = 100,                    // Optional
        Currency = "EUR",
        Description = "Demo payment request",
        ExternalId = "Demo"                     // Mandatory when PlatformUsage is set to PaymentRequestForMyself
    });
```

It is possible to retrieve all the Payment Requests from an existing User. The results are paginated and the desired offset and limit must be specified as parameters. Filtering is possible based on the date. Example:

```c#
UserPaymentResponse response = await tikkieClient.GetUserPaymentRequestsAsync(
    request: new UserPaymentRequest
    {
        PlatformToken = "platformToken",
        UserToken = "userToken",
        Offset = 0,
        Limit = 20,
        FromDate = new DateTime(2017, 05, 23),  // Optional
        ToDate = new DateTime(2017, 05, 24)     // Optional
    });
```

Retrieve a single Payment Request:

```c#
SinglePaymentRequestResponse response = await tikkieClient.GetPaymentRequestAsync(
    request: new SinglePaymentRequest
    {
        PlatformToken = "platformToken",
        UserToken = "userToken",
        PaymentRequestToken = "paymentRequestToken"
    });
```

### Extra information

Access to configuration parameters on the created client can be accessed via the property `TikkieClient.Configuration`.

Access to the current authorization token parameters used for the requests can be accessed via the property `TikkieClient.AuthorizationTokenInfo`.

### Error handling

Error responses from the Tikkie API are thrown as a `TikkieErrorResponseException`, where the errors are gathered.

Example:

```c#
try
{
    // ...
    var response = tikkieClient.GetPlatformsAsync();
    // ...
}
catch (TikkieErrorResponseException ex)
{
    ErrorResponse[] errorResponses = ex.ErrorResponses;
    // Handle exception
}
```
