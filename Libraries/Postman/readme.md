# Getting started with the M-Files Postman Collection and Environment

1.	Make sure that M-Files Web is enabled in the development environment.
1.	Install [Postman](https://www.getpostman.com/) from their website.
1.	Open Postman.
1.	Import the two json files (`M-Files.postman_collection.json` and `localhost.postman_environment.json`).
1.	Edit the environment to change the `MFWSUrl` to the REST API url of your environment (M-Files Web Url + /REST).
1.	Under `1. Connection group` Select the `Login to Vault` request.
1.	Modify the body of the request to contain credentials and vault guid for the vault in your environment.
1.	Click Send. The response is the authentication token to the vault.
1.	Edit the environment to change the `MFAuthenticationToken` to be the value from previous response.
1.	Test other requests. Some of them will require you to change some values in the environment or in the request url and body.

