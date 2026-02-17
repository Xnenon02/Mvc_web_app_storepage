MYMvcApp

My first ASP.NET Core MVC web application deployed to an Ubuntu Virtual Machine in Azure using Nginx and systemd.

📦 Projektöversikt

Detta projekt visar hur man:

Bygger en ASP.NET Core MVC-applikation (.NET 8)

Provisionerar en Ubuntu VM i Azure

Installerar .NET Runtime och Nginx

Driftsätter applikationen som en systemd-tjänst

Exponerar den via en reverse proxy

🛠 Scripts i repot

I mappen scripts/ finns två script som används för att återskapa servermiljön.

setup_server.sh

Installerar och konfigurerar:

Nginx

ASP.NET Core Runtime 8.0

Applikationsmapp (/var/www/myapp)

systemd-service

Nginx reverse proxy

deploy_app.sh

Verifierar:

Att filer finns på rätt plats

Att runtimeconfig har rätt version

Att Nginx-konfigurationen är korrekt

Scriptsen körs på servern, men ligger i GitHub så att en annan student kan återskapa hela miljön.

🚀 Driftsättning – steg för steg

Fungerar för:

Windows (Git Bash)

macOS

Linux

1️⃣ Publicera applikationen lokalt

Gå till projektmappen där .csproj ligger och kör:

dotnet publish -c Release -o publish


Verifiera att rätt .NET-version används:

cat publish/MyMvcApp.runtimeconfig.json | grep -n '"version"'


Du ska se:

"version": "8.0.0"

2️⃣ Ladda upp scripts och publish till servern

Stå i repo-root (där scripts/ och publish/ finns).

Windows (Git Bash)
scp -i "/c/Users/<USER>/Downloads/<KEY>.pem" -r scripts azureuser@<VM_PUBLIC_IP>:~/
scp -i "/c/Users/<USER>/Downloads/<KEY>.pem" -r publish azureuser@<VM_PUBLIC_IP>:~/

macOS / Linux
scp -i "~/<path>/<KEY>.pem" -r scripts azureuser@<VM_PUBLIC_IP>:~/
scp -i "~/<path>/<KEY>.pem" -r publish azureuser@<VM_PUBLIC_IP>:~/

3️⃣ Logga in på servern
Windows (Git Bash)
ssh -i "/c/Users/<USER>/Downloads/<KEY>.pem" azureuser@<VM_PUBLIC_IP>

macOS / Linux
ssh -i "~/<path>/<KEY>.pem" azureuser@<VM_PUBLIC_IP>

4️⃣ Kör setup-scriptet på servern

Gör scripts körbara:

chmod +x ~/scripts/setup_server.sh
chmod +x ~/scripts/deploy_app.sh


Kör setup:

sudo bash ~/scripts/setup_server.sh


Detta installerar runtime, nginx och skapar systemd-service.

5️⃣ Kopiera publish-filer till applikationsmappen
sudo rm -rf /var/www/myapp/*
sudo cp -r ~/publish/* /var/www/myapp/


Sätt säkra rättigheter:

sudo chown -R www-data:www-data /var/www/myapp
sudo chmod -R 755 /var/www/myapp

6️⃣ Starta tjänsten
sudo systemctl restart myapp.service
sudo systemctl status myapp.service --no-pager -l


Om allt fungerar ska du se:

Active: active (running)

7️⃣ Verifiera lokalt på servern
curl -I http://127.0.0.1


Du ska få:

HTTP/1.1 200 OK

8️⃣ Verifiera från internet

Öppna i webbläsare:

http://<VM_PUBLIC_IP>


Applikationen ska nu vara åtkomlig publikt.

🔐 Säkerhet
Serveråtkomst

SSH med privat nyckel (.pem)

Lösenordsinloggning används inte

Nätverk

Port 22 (SSH)

Port 80 (HTTP)

Övriga portar är stängda

Applikation

Körs som www-data

Exponeras via Nginx reverse proxy

Kestrel lyssnar endast på 127.0.0.1

📌 Teknisk sammanfattning
Komponent	Teknik
Framework	.NET 8 MVC
OS	Ubuntu 24.04 LTS
Hostmodell	IaaS (Azure VM)
Reverse proxy	Nginx
Service manager	systemd
📎 Slutsats

Denna lösning visar en fullständig manuell driftsättning av en .NET MVC-applikation i Azure med:

Infrastruktur via Azure Portal

Konfiguration via shell-scripts

Produktionsexponering via Nginx

Processhantering via systemd

Hela miljön kan återskapas genom att följa denna README och använda scriptsen i detta repository.