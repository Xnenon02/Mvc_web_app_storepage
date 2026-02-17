#!/usr/bin/env bash
set -euo pipefail

# ====== SETTINGS (ändra vid behov) ======
APP_NAME="myapp"
DLL_NAME="MyMvcApp.dll"                  # ändra om din dll heter något annat
APP_DIR="/var/www/${APP_NAME}"
PORT="5000"

UPLOAD_USER="azureuser"                  # användaren du ssh:ar in som
RUN_AS_USER="www-data"                   # systemd ska köra appen som

NGINX_SITE="/etc/nginx/sites-available/${APP_NAME}"
SERVICE_FILE="/etc/systemd/system/${APP_NAME}.service"
# =======================================

echo "==> Checking root permissions"
if [[ "${EUID}" -ne 0 ]]; then
  echo "ERROR: Run this script with sudo:"
  echo "  sudo bash scripts/setup_server.sh"
  exit 1
fi

echo "==> Updating packages"
apt-get update -y

echo "==> Installing nginx + ASP.NET Core runtime (8.0)"
apt-get install -y nginx aspnetcore-runtime-8.0

echo "==> Creating app directory: ${APP_DIR}"
mkdir -p "${APP_DIR}"

# Viktigt: så att scp kan ladda upp till /var/www/myapp utan permission denied
# (Du kan låsa tillbaka till www-data efter deploy)
echo "==> Setting temporary ownership for uploads (${UPLOAD_USER})"
chown -R "${UPLOAD_USER}:${UPLOAD_USER}" "${APP_DIR}"
chmod -R 755 "${APP_DIR}"

echo "==> Creating systemd service: ${SERVICE_FILE}"
cat > "${SERVICE_FILE}" <<SERVICE
[Unit]
Description=${APP_NAME} - ASP.NET Core MVC app
After=network.target

[Service]
WorkingDirectory=${APP_DIR}
ExecStart=/usr/bin/dotnet ${APP_DIR}/${DLL_NAME}
Restart=always
RestartSec=10
SyslogIdentifier=${APP_NAME}
User=${RUN_AS_USER}
Environment=ASPNETCORE_ENVIRONMENT=Production
Environment=ASPNETCORE_URLS=http://127.0.0.1:${PORT}

[Install]
WantedBy=multi-user.target
SERVICE

echo "==> Creating nginx reverse proxy config: ${NGINX_SITE}"
cat > "${NGINX_SITE}" <<NGINX
server {
    listen 80;
    server_name _;

    location / {
        proxy_pass http://127.0.0.1:${PORT};
        proxy_http_version 1.1;

        proxy_set_header Host \$host;
        proxy_set_header X-Real-IP \$remote_addr;
        proxy_set_header X-Forwarded-For \$proxy_add_x_forwarded_for;
        proxy_set_header X-Forwarded-Proto \$scheme;
    }
}
NGINX

echo "==> Enabling nginx site"
rm -f /etc/nginx/sites-enabled/default
ln -sf "${NGINX_SITE}" "/etc/nginx/sites-enabled/${APP_NAME}"

echo "==> Testing nginx config"
nginx -t

echo "==> Reloading systemd + enabling services"
systemctl daemon-reload
systemctl enable "${APP_NAME}.service"
systemctl enable nginx

echo "==> Restarting nginx"
systemctl restart nginx

echo ""
echo "✅ Server setup complete."
echo ""
echo "Next steps:"
echo "1) Upload your publish output to: ${APP_DIR}"
echo "2) Then lock permissions + restart service:"
echo "   sudo chown -R ${RUN_AS_USER}:${RUN_AS_USER} ${APP_DIR}"
echo "   sudo chmod -R 755 ${APP_DIR}"
echo "   sudo systemctl restart ${APP_NAME}.service"
echo "   sudo systemctl status ${APP_NAME}.service --no-pager -l"
