# RoadStatus

RoadStatus requires dotnet core version 2.2

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

## Notes

The TflApiPresentationEntitiesRoadCorridor DTO class was generated from the Swagger document.

The tests were aimed at the api as the display is easily verified by inspection.

Validation of input road id was determined by the results returned by the api rather than by the format of the string, but this could be easily added if required.


