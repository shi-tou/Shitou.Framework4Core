@echo off
echo ----------begin pack-----------

nuget.exe pack -Prop Configuration=Release

echo ----------end-----------
pause