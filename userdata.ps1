# Install Docker
Invoke-WebRequest -UseBasicParsing "https://desktop.docker.com/win/stable/Docker%20Desktop%20Installer.exe" -OutFile "DockerInstaller.exe"
Start-Process -Wait -FilePath ".\DockerInstaller.exe" -ArgumentList "/quiet"

# Start Docker
Start-Service com.docker.service

# Download Kong config from S3
aws s3 cp s3://kong-config-bucket/kong-config.yml C:\kong\kong-config.yml

# Run Kong container
docker run -d --name kong `
  -p 8000:8000 -p 8001:8001 `
  -v C:\kong:/kong `
  -e KONG_DATABASE=off `
  -e KONG_DECLARATIVE_CONFIG=/kong/kong-config.yml `
  -e KONG_ADMIN_LISTEN=0.0.0.0:8001 `
  kong:3.6