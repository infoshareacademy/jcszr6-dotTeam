# You can use following command to run script from CLI: powershell -executionpolicy bypass -File .\publish-cmd\publish.ps1
dotnet publish -c Release -r win10-x64 --self-contained true;
dotnet publish -c Release -r linux-x64 --self-contained true;
