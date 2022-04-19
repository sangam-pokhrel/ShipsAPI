# ShipsAPI
To Run the API with Docker,
1. Have docker running in the system
2. In the ShipsAPI directory, open commandline/terminal
3. Run the command: 'docker build -t <imagename> -f Ships.API/Dockerfile .'
4. Then run the command: 'docker run --name <containername> -p 5106:80 -d <imagename>'
5. The App should then be accessible by the url: 'http://localhost:5106/swagger/index.html'
6. The <imagename> and <containername> are the name of docker image and container respectively, which can be set by the user.

To Run the API with DOT NET SDK, 
1. Install dot net core 6.0 sdk from the given link: https://dotnet.microsoft.com/en-us/download/dotnet/6.0
2. After installation, navigate to Ships.API folder
3. Open the command line/terminal and type 'dotnet run'
4. Open the link 'https://localhost:7106/swagger/index.html' in the browser
