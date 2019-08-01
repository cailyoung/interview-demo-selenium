# Selenium Demo for Interview

## Prerequisites

* .NET Core SDK for your platform
* Internet access from the machine you are running this from
* A working Xero credential with 2FA code already set up
  * Reset your demo company before running the test, please.
* Chrome browser (latest version) installed in the default location

## Running the tests

1. Clone the repo to a local folder.
1. Edit `credential.yaml` to have your username and password.
1. Edit `helpers.cs` line 38 to point to your full filepath to the `credential.yaml` file.
1. Have your 2FA authenticator ready!
1. Run `dotnet test` from inside the `selenium-tests` folder.
    * The test runner will attempt to restore all the NuGet dependencies at this point, probably silently, so on first run it will take some time depending on your Internet connection speed.
1. When the browser pauses at the 2FA entry, enter your code but don't click. Selenium should handle the click after 10 seconds.
    * (If you do click, the script doesn't handle this very well and will probably throw an exception.)
1. Results are visible in the console.
1. The test suite will attempt to reset your demo company in teardown.

## TODO

* [ ] Parameterise the credential file location
* [ ] Reporting
* [ ] Extract the Actions clicker to a class
* [ ] Bypass 2FA
* [ ] Randomise bank account name
