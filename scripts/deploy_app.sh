#!/usr/bin/env bash
set -euo pipefail

# ====== SETTINGS (ändra vid behov) ======
APP_NAME="myapp"
APP_DIR="/var/www/${APP_NAME}"
PUBLISH_DIR="${HOME}/publish"     # hit scp:ar du publish/
RUN_AS_USER="www-data"
# =======================================

echo "==> Checking root permissions"
if [[ "${EUID}" -ne 0 ]]; then
  echo "ERROR: Run this script with sudo:"
  echo "  sudo bash scripts/deploy_app.sh"
  exit 1
fi

echo "==> Checking publish folder exists: ${PUBLISH_DIR}"
if [[ ! -d "${PUBLISH_DIR}" ]]; then
  echo "ERROR: ${PUBLISH_DIR} does not exist."
  echo "Upload publish/ to ~/publish first (scp)."
  exit 1
fi

echo "==> Ensuring app directory exists: ${APP_DIR}"
mkdir -p "${APP_DIR}"

echo "==> Clearing old files in ${APP_DIR}"
rm -rf "${APP_DIR:?}/"*

echo "==> Copying new publish files to ${APP_DIR}"
cp -r "${PUBLISH_DIR}/." "${APP_DIR}/"

echo "==> Showing runtimeconfig version (sanity check)"
if [[ -f "${APP_DIR}/MyMvcApp.runtimeconfig.json" ]]; then
  grep -n '"version"' "${APP_DIR}/MyMvcApp.runtimeconfig.json" || true
else
  echo "WARN: MyMvcApp.runtimeconfig.json not found in ${APP_DIR} (check your DLL/app name)."
fi

echo "==> Locking permissions for runtime user (${RUN_AS_USER})"
chown -R "${RUN_AS_USER}:${RUN_AS_USER}" "${APP_DIR}"
chmod -R 755 "${APP_DIR}"

echo "==> Verifying nginx config"
nginx -t

echo "==> Restarting service: ${APP_NAME}.service"
systemctl restart "${APP_NAME}.service"

echo "==> Status:"
systemctl status "${APP_NAME}.service" --no-pager -l || true

echo "==> Local test (should be 200 OK if app is running behind nginx):"
curl -I http://127.0.0.1 || true

echo ""
echo "✅ Deploy finished."
echo "Open in browser: http://<VM_PUBLIC_IP>"
