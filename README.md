# Selenium Demo for Interview

## Prerequisites

* .NET Core SDK for your platform
* A working Xero credential with 2FA code already set up
** Clear out your demo company before running the test
* Chrome browser (latest version) installed in the default location

## Running the tests

1. Edit `credential.yaml` to have your username and password.
1. Edit `helpers.cs` line 38 to point to your full filepath to the `credential.yaml` file.
1. Have your 2FA authenticator ready
1. Run `dotnet test` inside the `selenium-tests` folder.
1. When the browser pauses at the 2FA entry, enter you code but don't click. Selenium should handle the click after 10 seconds.

## TODO

* [ ] Parameterise the credential file location
* [ ] Reporting
* [ ] Extract the Actions clicker to a class
* [ ] Bypass 2FA
* [ ] Randomise account name
