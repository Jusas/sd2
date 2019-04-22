# Update the EF migrations.

$currentDateTime = Date -Format "yyMMdd_HHmm";
dotnet ef migrations add ContextUpdate_$($currentDateTime) --context ApiDbContext --project SD2API.Persistence.csproj -o Migrations
