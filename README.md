# Tiger.Problem

## What It Is

Tiger.Problem is an implementation of [RFC 7807](https://tools.ietf.org/html/rfc7807), which defines a "problem detail" as a way to carry machine-readable details of errors in a HTTP response to avoid the need to define new error response formats for HTTP APIs.

## Why You Want It

From the RFC:

> HTTP status codes are sometimes not sufficient to convey
> enough information about an error to be helpful.  While humans behind
> Web browsers can be informed about the nature of the problem with an
> HTML response body, non-human consumers of
> so-called "HTTP APIs" are usually not.
>
> This specification defines simple JSON and XML
> document formats to suit this purpose.  They
> are designed to be reused by HTTP APIs, which can identify distinct
> "problem types" specific to their needs.
>
> Thus, API clients can be informed of both the high-level error class
> (using the status code) and the finer-grained details of the problem
> (using one of these formats).
>
> For example, consider a response that indicates that the client's
> account doesn't have enough credit.  The 403 Forbidden status code
> might be deemed most appropriate to use, as it will inform HTTP-
> generic software (such as client libraries, caches, and proxies) of
> the general semantics of the response.
>
> However, that doesn't give the API client enough information about
> why the request was forbidden, the applicable account balance, or how
> to correct the problem.  If these details are included in the
> response body in a machine-readable format, the client can treat it
> appropriately; for example, triggering a transfer of more credit into
> the account.

## How You Use It

When a condition arises in implementing an HTTP API that requires the return of an error status code (typically between 400 and 599), construct a `Problem` object with the details of the error, and send it as the body of the response. Documenting that your application returns `application/problem+json` upon error conditions, add the Problem JSON formatter to the available formatters in the startup class of your ASP.NET Core application. This can be done by calling an extension method on values of either `IMvcBuilder` or `IMvcCoreBuilder` (<i lang="la">i.e.</i>, on the result of calling `AddMvc` or `AddMvcCore`).

### What about XML?

XML serialization support is not a feature request that has yet reached critical mass.

## How You Develop It

This project is using the standard [`dotnet`](https://dot.net) build tool. A brief primer:

- Restore NuGet dependencies: `dotnet restore`
- Build the entire solution: `dotnet build`
- Run all unit tests: `dotnet test`
- Pack for publishing: `dotnet pack -o "$(pwd)/artifacts"`

The parameter `--configuration` (shortname `-c`) can be supplied to the `build`, `test`, and `pack` steps with the following meaningful values:

- “Debug” (the default)
- “Release”

This repository is attempting to use the [GitFlow](http://jeffkreeftmeijer.com/2010/why-arent-you-using-git-flow/) branching methodology. Results may be mixed, please be aware.
## Thank You

Seriously, though. Thank you for using this software. The author hopes it performs admirably for you.
