namespace Tests
{
    using Applinate;
    using Applinate.Test;
    using FluentAssertions;

    public class EmulatorTests:ApplinateTestBase
    {
        [Fact]
        public void LocalTest()
        {
            var key = "key";
            var value = Guid.NewGuid().ToString();  

            EmulateConfiguration.SetValue(key, value);

            ConfigurationProvider.GetConfiguration<MyConfig>().Key.Should().Be(value);
        }

        [Fact]
        public void GlobalTest()
        {
            var key = "key";
            var value = Guid.NewGuid().ToString();

            EmulateConfiguration.SetValue(key, value, true);

            ConfigurationProvider.GetConfiguration<MyConfig>().Key.Should().Be(value);
        }
    }
}