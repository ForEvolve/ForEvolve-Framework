using ForEvolve.EntityFrameworkCore.Seeders.TestData;
using ForEvolve.Testing;
using System;
using System.Linq;
using Xunit;

namespace ForEvolve.EntityFrameworkCore.ValueConversion
{
    [Collection(SeederDbContextCollection.Name)]
    [Trait(DependencyTrait.Name, DependencyTrait.Values.SqlServer)]
    public class IntegrationConvertersTest
    {
        private readonly SeederDbContext sut;

        public IntegrationConvertersTest(LocalDbSeederDbContextFactory factory)
        {
            sut = factory.Create(nameof(IntegrationConvertersTest));
        }

        [Fact]
        public void Should_convert_Object_property()
        {
            // Arrange
            var entity = new TestEntity
            {
                Name = "Should_serialize_Object_property",
                Object = new { SomeProp = "SomeValue" }
            };
            sut.SaveChangesShouldSave = true;
            sut.Add(entity);
            sut.SaveChanges();

            // Act
            var result = sut.TestEntities.Single(x => x.Id == entity.Id);

            // Assert
            Assert.NotNull(result.Object);
            result.Object
                .Should()
                .OwnProperty("SomeProp")
                .That().Equal("SomeValue");
        }

        [Fact]
        public void Should_convert_Dictionary_property()
        {
            // Arrange
            var entity = new TestEntity
            {
                Name = "Should_serialize_Object_property",
                Dictionary = new System.Collections.Generic.Dictionary<string, object>
                {
                    { "SomeProp", "SomeValue" }
                }
            };
            sut.SaveChangesShouldSave = true;
            sut.Add(entity);
            sut.SaveChanges();

            // Act
            var result = sut.TestEntities.Single(x => x.Id == entity.Id);

            // Assert
            Assert.NotNull(result.Dictionary);
            Assert.True(result.Dictionary.ContainsKey("SomeProp"));
            Assert.Equal("SomeValue", result.Dictionary["SomeProp"]);
        }
    }
}
