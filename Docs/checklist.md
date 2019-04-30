# Dev checklist

## Tools

- VS2019
- SQL Server
- Storage Explorer

## Environment

- Create empty database
- Create user with full access to db
- Set environment variables to FunctionAPI project
	- SD2API_Auth__AdminCreds
	- SD2API_ConnectionStrings__Db
	- SD2API_ConnectionStrings__ReplayBlobStorage
- In KeyVault, same variables are named ConnectionStrings--Db etc.
- Run Migrations automatically by running the function app

## Deving

- Run migration script when domain models or their configurations change

## Deployment

- Run migrations
- Deploy
