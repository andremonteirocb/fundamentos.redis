
## Configurações
### Execute o comando para a criação do container abaixo:
```
    docker run -d -p 6379:6379 --name redis -v redis-volume:/data -it redis
```

### Para visualizar as informações utilizando o redis-commander siga os passos abaixo:
```
    npm install -g redis-commander
    redis-commander
    http://127.0.0.1:8081
```
