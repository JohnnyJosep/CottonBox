using System;
using FluentAssertions;
using Xunit;

namespace CottonBox.Monads.Test
{
    public class OptionalTests
    {
        [Theory]
        [InlineData("hi", true)]
        [InlineData(default, false)]
        public void TryGetValueString(string value, bool expectedTryGet)
        {
            // arrange
            var optional = (Optional<string>)value;
            
            // actual
            var actualTryGet = optional.TryGetValue(out var actualValue);

            // assert
            actualTryGet.Should().Be(expectedTryGet);
            actualValue.Should().Be(value);
        }
        
        [Fact]
        public void TryGetValueNone()
        {
            // arrange
            var optional = (Optional<string>)Optional.None;
            
            // actual
            var actualTryGet = optional.TryGetValue(out var actualValue);

            // assert
            actualTryGet.Should().Be(false);
            actualValue.Should().Be(default);
        }
        
        [Fact]
        public void TryGetValueSomeWithValue()
        {
            // arrange
            var optional = Optional.Some("hi");
            
            // actual
            var actualTryGet = optional.TryGetValue(out var actualValue);

            // assert
            actualTryGet.Should().Be(true);
            actualValue.Should().Be("hi");
        }
        
        [Fact]
        public void TryGetValueSomeWithoutValue()
        {
            // arrange
            static string DelNull() => null;
            
            // actual
            Func<object> result = () => Optional.Some(DelNull());

            // assert
            result.Should()
                .Throw<ArgumentNullException>()
                .WithMessage("Value cannot be null. (Parameter '*')");
        }
    }
}
