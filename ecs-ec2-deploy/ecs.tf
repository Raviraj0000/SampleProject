resource "aws_ecs_cluster" "main" {
  name = "MicroServices-Cluster"
}

resource "aws_ecs_task_definition" "app" {
  family                   = "my-app-task"
  network_mode             = "bridge"
  requires_compatibilities = ["EC2"]
  container_definitions    = jsonencode([
    {
      name      = "my-app"
      image     = "${aws_ecr_repository.app_repo.repository_url}:latest"
      cpu       = 256
      memory    = 512
      essential = true
      portMappings = [{ containerPort = 80, hostPort = 80 }]
    }
  ])
  execution_role_arn = aws_iam_role.ecs_task_execution.arn
}

resource "aws_ecs_service" "app_service" {
  name            = "my-app-service"
  cluster         = aws_ecs_cluster.main.id
  task_definition = aws_ecs_task_definition.app.arn
  desired_count   = 1
  launch_type     = "EC2"
}