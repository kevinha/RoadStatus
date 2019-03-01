# RoadStatus

RoadStatus requires dotnet version 2.2

To build RoadStatus for Windows:

` dotnet publish -o build -c RELEASE -r win-x64 .\RoadStatus\`

Then to run RoadStatus:

`.\RoadStatus\build\RoadStatus.exe <road id> <app id> <app key>`

where
* <road id\> is the id of the road, e.g. A2
* <app id\> is your Tfl application id
* <app key\> is your Tfl application key

for example

`.\RoadStatus\build\RoadStatus.exe A2 myappid myappkey`

To show logs add the verbose option

`.\RoadStatus\build\RoadStatus.exe A2 myappid myappkey -v`

To run the tests

`dotnet test .\RoadStatusTests`