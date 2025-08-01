resource "aws_instance" "ecs_host" {
  ami           = "ami-0dc2d3e4c0f9ebd18" # ECS-optimized AMI
  instance_type = "t3.micro"
  subnet_id     = aws_subnet.public[0].id
  user_data     = file("user_data.sh")
  iam_instance_profile = aws_iam_instance_profile.ecs_instance_profile.name
}