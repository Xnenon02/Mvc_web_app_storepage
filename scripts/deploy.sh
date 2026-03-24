#!/bin/bash
set -e

APP_ROOT="/var/www/techstore"
RELEASES_DIR="$APP_ROOT/releases"
CURRENT_DIR="$APP_ROOT/current"
PACKAGE_PATH="/tmp/techstore-release.tar.gz"
TIMESTAMP=$(date +%Y%m%d%H%M%S)
NEW_RELEASE="$RELEASES_DIR/$TIMESTAMP"

mkdir -p "$NEW_RELEASE"

tar -xzf "$PACKAGE_PATH" -C "$NEW_RELEASE"

ln -sfn "$NEW_RELEASE" "$CURRENT_DIR"

chown -R deployer:deployer "$APP_ROOT"

sudo systemctl restart techstore.service
sudo systemctl status techstore.service --no-pager