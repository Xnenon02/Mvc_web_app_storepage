# TechStore

TechStore är en ASP.NET Core MVC-applikation som visar laptops och stationära datorer.  
Applikationen använder Azure Cosmos DB for NoSQL för produktdata och Azure Blob Storage för produktbilder.

## Krav

För att köra projektet behöver du:

- .NET 8 SDK
- Visual Studio 2022 eller Visual Studio Code
- Ett Azure Cosmos DB-konto
- Ett Azure Storage Account med Blob Storage
- Git

## Klona projektet

```bash
git clone
cd Mvc_web_app_storepage
Installera beroenden
dotnet restore
Skapa Azure-resurser

Skapa följande i Azure:

Azure Cosmos DB for NoSQL
Database: TechStoreDb
Container: Products
Partition key: /category

Skapa också:

Azure Storage Account
Blob container: product-images
Lägg in produktdata i Cosmos DB

Öppna Data Explorer i Azure Cosmos DB och lägg in dina produkter i containern Products.

Varje produkt ska innehålla fält som till exempel:

id
category
name
brand
price
shortDescription
longDescription
imageUrl
stockQuantity
isFeatured
specs
Ladda upp bilder till Blob Storage

Öppna din blob-container product-images i Azure Portal och ladda upp produktbilder.

Kopiera bildens URL och använd den i fältet imageUrl i Cosmos DB.

Konfigurera hemligheter lokalt

Initiera user secrets:

dotnet user-secrets init

Lägg sedan in dina värden:

dotnet user-secrets set "CosmosDb:AccountEndpoint" "DIN_COSMOS_ENDPOINT"
dotnet user-secrets set "CosmosDb:AccountKey" "DIN_COSMOS_KEY"
dotnet user-secrets set "CosmosDb:DatabaseName" "TechStoreDb"
dotnet user-secrets set "CosmosDb:ContainerName" "Products"
dotnet user-secrets set "BlobStorage:ConnectionString" "DIN_BLOB_CONNECTION_STRING"
dotnet user-secrets set "BlobStorage:ContainerName" "product-images"
Kör applikationen lokalt
dotnet run

Öppna sedan applikationen i webbläsaren via den adress som visas i terminalen.

Sidor i applikationen

Applikationen innehåller bland annat:

/
/Products/Desktop
/Products/Laptops
Deployment i Azure

Projektet kan driftsättas till en Ubuntu-baserad Azure VM.

Lösningen använder:

Nginx som reverse proxy
systemd för att köra appen som tjänst
cloud-init för grundkonfiguration
GitHub Actions för CI/CD
Bash för deployment-script
GitHub Actions

För att GitHub Actions ska fungera behöver följande secrets finnas i GitHub-repot:

VM_HOST
VM_USER
VM_SSH_KEY

Om du använder deployment mot en Azure VM måste servern också ha rätt miljövariabler för Cosmos DB och Blob Storage.

Sammanfattning

Steg för att få igång projektet:

Klona repot
Kör dotnet restore
Skapa Cosmos DB och Blob Storage i Azure
Lägg in produktdata i Cosmos DB
Ladda upp bilder till Blob Storage
Lägg in secrets lokalt med dotnet user-secrets
Kör projektet med dotnet run
Författare

Tom Ekstrand
