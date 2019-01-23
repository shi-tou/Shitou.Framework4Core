@echo off
echo ----------begin push-----------

nuget.exe push Fxt.Framework.RabbitMQ.1.0.0.4.nupkg 790095a1-555a-3d89-aad8-164954fc3e22 -Source http://192.168.2.45:8081/repository/nuget4net/

echo ----------end-----------
pause