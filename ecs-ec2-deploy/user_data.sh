#!/bin/bash
yum update -y
yum install -y docker
service docker start
usermod -a -G docker ec2-user

mkdir -p /ecs/kong-config
curl -o /ecs/kong-config/kong-config.yml https://your-s3-bucket/kong-config.yml

docker run -d --name kong \
  -p 8000:8000 -p 8001:8001 \
  -v /ecs/kong-config:/kong \
  -e KONG_DATABASE=off \
  -e KONG_DECLARATIVE_CONFIG=/kong/kong-config.yml \
  -e KONG_ADMIN_LISTEN=0.0.0.0:8001 \
  kong:3.6