# Publish/Subscribe Homework Solution

This is a small C# console application which makes use of the publish/subscribe pattern. It uses publish/subscribe to provide means of tracking weather conditions (temperature and humidity) in multiple cities.

The PubSub project contains the classes and console application, while the PubSub.Tests projects contains the unit tests.

## Running

You can run the program from the command line using the following commands from the root directory:

```
cd PubSub
dotnet restore
dotnet run
```

The output of application is:

```
Temperature in Damascus is 10
Humidity in Homs is 51.5
```

## Testing

You can run unit tests by running the following commands from the root directory:

```
cd PubSub.Tests
dotnet restore
dotnet test
```
