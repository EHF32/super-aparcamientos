using AutoMapper;
using SuperAparcamiento.Logic.MappingProfiles;
using System.Reflection;

namespace SuperAparcamiento.IntegrationTests;

public class AutoMapperTests
{
    [SetUp]
    public void Setup() { }

    [Test]
    public void AssertConfigurationIsValid()
    {
        // Arrange
        var config = new MapperConfiguration(configuration =>
        {
            configuration.AddMaps(typeof(IMappingProfilesMarker).GetTypeInfo().Assembly);
        });

        // Assert
        config.AssertConfigurationIsValid();
    }
}