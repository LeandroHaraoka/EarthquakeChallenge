namespace EarthquakeChallenge.UnitTests.Application
{
    internal static class EarthquakeResponseMock
    {
        internal const string USGSClient = @"
            ""{
                ""features"": [
                    {
                        ""properties"": {
                            ""mag"": 0.85
                        },
                        ""geometry"": {
                            ""type"": ""Point"",
                            ""coordinates"": [
                                -122.8083344,
                                38.8274994,
                                2.23
                            ]
                        },
                        ""id"": ""nc73650190""
                    },
                    {
                        ""properties"": {
                            ""mag"": 0.7
                        },
                        ""geometry"": {
                            ""type"": ""Point"",
                            ""coordinates"": [
                                -120.6065,
                                39.5111,
                                9.9
                            ]
                        },
                        ""id"": ""nn00827614""
                    }
                ],
                ""bbox"": [
                    -173.4713,
                    -57.8709,
                    -2.63,
                    179.7812,
                    69.6639,
                    454.1
                ]
            }""";
    }
}
