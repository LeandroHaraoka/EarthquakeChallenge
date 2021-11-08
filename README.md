# Earthquake Challenge

This project aims to provide earthquakes information, so that users can identify if a location has suffered an earthquake effects during a specific period of time. 

## Operation System Requirements
- .Net Core Runtime 3.1
- AspnetCore Runtime 3.1
- It's not necessary to implement any database technology.

## How to run

### Build

```bash
$ dotnet build
```

### Run

```bash
$ dotnet run --project <cs proj path>
```

### Test

To run tests commands, you need **dotnet core**, **xunit** and the **coverlet library**.

1. Dependencies

```bash
$ dotnet add <TEST_PROJECT_PATH> package Microsoft.NET.Test.Sdk
```

```bash
$  dotnet tool install --global coverlet.console
```

2. Checking coverage with coverlet

When executing coverlet coverage command, a `coverage.json` file will be generated
in the project folder. It's important to add it to `gitignore` file.

```bash
$  dotnet test <TEST_PROJECT_PATH> /p:CollectCoverage=true /p:CoverletOutputFormat=opencover /p:Exclude=[xunit.*]*
```

## Improvements

- Each request for getting earthquakes requires calling the USGS API. Due to unavailability, latency or overload issues, the response time can get degraded. So, an interesting option should be caching the USGS API information. When an user searchs for a earthquake occurrence, we can first try to get it from cache. If cache doesn't provide any information within the time range between start_date/end_date, so then we can call the USGS API. Also, we can implement a scheduled routine to update cache with latest records, from n in n minutes for example. To get a best improvement, the elements can be cached already in order from newest to oldest, without need to order them when user requests the endpoint.

- For each earthquake returned by USGS API, our application calculates the distance between the earthquake and the provided lat/long values, using Haversine formula. This calculus can be be improved for a huge volume of earthquake data. We can proccess these calculus using C# features for paralelism. A bunch of calculus can be performed simultaneously if we consider they're tasks that run in different threads.

- Not for performance improvements, but for monitoring purpose we should add some logs with information related to errors (like exceptions), elapsed time, trace execution. In a professional project it's fundamental, we decided to omit in this challenge in order to keep it simple and because the implementation depends on the monitoring technology available.

- The project architecture was made simple, in a real project it would contains some additional layers, for repositories, infrastructure, but we decided to avoid single file layers.
