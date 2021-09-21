# How to start

## How to set up Heroku for the first run
1. Create Heroku account:
https://signup.heroku.com/login

2. Install Heroku CLI:
https://devcenter.heroku.com/articles/heroku-cli#download-and-install

3. Login to Heroku CLI by console\terminal:
```
heroku login
```

## Heroku setups per project
1. Create Heroku app with the name `drawgo-test`:
https://dashboard.heroku.com/new-app

2. Heroku doesn't support .net from the box, so you need to push docker container for the first time by console\terminal:
* login to container
```
heroku container:login
```
* push your docker image to the container
```
heroku container:push -a drawgo-test web
```
* release the container
```
heroku container:release -a drawgo-test web
```
3. Add HEROKU_API_KEY to GitLab repository setting -> CI/CD -> Variables
4. Push init commit to your GitLab repository in the `main` branch
5. Check your app
https://drawgo.herokuapp.com/
6. Enjoy


 docker build -t drawgo .
 docker tag drawgo registry.heroku.com/drawgo/web
 heroku container:push web -a drawgo
 heroku container:release web -a drawgo