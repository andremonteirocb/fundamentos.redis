
## Configurações
### Container Redis
```
    docker run -d -p 6379:6379 --name redis -v redis-volume:/data -it redis
```

### Redis Commander
```
    npm install -g redis-commander
    redis-commander
    http://127.0.0.1:8081
```
