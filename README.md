# MinecraftServerAPI
Easy Minecraft-server decorator to provide API for remote control and manipulate with blocks!

## About
This app run server.jar and provide all [God Commands](http://minecraft.gamepedia.com/Commands) to server inputStream;

You can send HTTP POST request to web server URL, 
like 
#### {"command": "/fill 0 0 0 10 10 10 sand"}
etc..

And MinecraftServerAPI redirect this command to server inputStream 

## How to use

1. Download /example/[server](https://github.com/VasenevEA/MinecraftServerAPI/tree/master/example/server) 
2. Edit server.properties (write your ip and port );
3. Edit config.txt -> "url": "http://localhost:81" (you will send HTTP POST command to this port)
4. Run as admin MinecraftServerApi.exe

Try to send HTTP POST with body: {"command": "/setblock 0 0 0 stone"} to yourURL/setcommand

## Why?
You can write scripts for creating some structures. Python, c#, js... nothing matters what use.
All you need -> send HTTP POST request.

1. Download /example/python-client/[test.py](https://github.com/VasenevEA/MinecraftServerAPI/tree/master/example/python-client/test.py) 
2. Check exist Python3
3. Run test.py (check destination url ;-)

![alt text](https://github.com/VasenevEA/MinecraftServerAPI/blob/master/example/train.gif)
![alt text](https://github.com/VasenevEA/MinecraftServerAPI/blob/master/example/walls.gif)
