resource "aws_vpc" "main" {
  cidr_block = "10.0.0.0/16"
  enable_dns_hostnames = true
}

resource "aws_subnet" "public" {
  count = 2
  vpc_id = aws_vpc.main.id
  cidr_block = cidrsubnet(aws_vpc.main.cidr_block, 8, count.index)
  map_public_ip_on_launch = true
  availability_zone = data.aws_availability_zones.available.names[count.index]
}

provider "aws" {
  region = "eu-north-1"
}

resource "aws_s3_bucket" "kong_config" {
  bucket = "kong-config-bucket"
  acl    = "private"
}

resource "aws_instance" "kong_ec2" {
  ami           = "ami-0abcdef1234567890" # Use latest Amazon Linux 2
  instance_type = "t3.micro"
  user_data     = file("userdata.sh")
  tags = {
    Name = "KongGatewayEC2"
  }
}

resource "aws_security_group" "kong_sg" {
  name        = "kong-sg"
  description = "Allow Kong ports"
  ingress {
    from_port   = 8000
    to_port     = 8001
    protocol    = "tcp"
    cidr_blocks = ["0.0.0.0/0"]
  }
  egress {
    from_port   = 0
    to_port     = 0
    protocol    = "-1"
    cidr_blocks = ["0.0.0.0/0"]
  }
}