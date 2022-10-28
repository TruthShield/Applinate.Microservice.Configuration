// Copyright (c) TruthShield, LLC. All rights reserved.
namespace Tests
{
    public class ConfigurationTest
    {
        private AppSetting configurations;

        public ConfigurationTest()
        {
            InitializationProvider.Initialize();

            // setting the environment variables for testing configuration to read from environment variables.
            Environment.SetEnvironmentVariable("EnvironemtKey", "environment variable app key");
            Environment.SetEnvironmentVariable("EnvironemtKeyDouble", "2.014");
            Environment.SetEnvironmentVariable("EnvironemtKeyInt", "-2014");
            Environment.SetEnvironmentVariable("EnvironemtKeyLong", "1232014");
            Environment.SetEnvironmentVariable("DuplicateKey", "this is duplicate key in environment.");
            Environment.SetEnvironmentVariable("additional-files", "appsettings.json, bootstrap.json");

            configurations = ConfigurationProvider.GetConfiguration<AppSetting>();
        }

        [Fact]
        public void IsValid_DuplicateKeyInDifferentConfigStore_DuplicateKeyFromEnvironmentReturnsTrue()
        {
            Xunit.Assert.True(configurations.DuplicateKey.Equals("this is duplicate key in environment."));
        }

        [Fact]
        public void IsValid_FromEnvironmentVariable_EnvironmentValueReturnsTrue()
        {
            configurations.EnvironemtKey.Should().NotBeNullOrEmpty();
            int.TryParse(configurations.EnvironemtKeyInt.ToString(), out _).Should().BeTrue(); ;
            double.TryParse(configurations.EnvironemtKeyDouble.ToString(), out _).Should().BeTrue();
            long.TryParse(configurations.EnvironemtKeyLong.ToString(), out _).Should().BeTrue(); ;
        }

        [Fact]
        public void IsValid_NotInLocalAndNotInGlobal_NullReturnsTrue()
        {
            Xunit.Assert.Throws<ConfigurationErrorsException>(() => ConfigurationProvider.GetConfiguration<ExceptionSetting>());
        }

        [Fact]
        public void IsValid_NotInLocalButInGlobal_GlobalKeyReturnsTrue()
        {
            configurations.GlobalKey.Should().NotBeNull();
        }

        [Fact]
        public void IsValid_ValidAge_ReturnsTrue()
        {
            int.TryParse(configurations.Age.ToString(), out _).Should().BeTrue();
        }

        [Fact]
        public void IsValid_ValidGUID_ReturnsTrue()
        {
            Xunit.Assert.True(Guid.TryParse(configurations.Identity.ToString(), out _));
        }

        [Fact]
        public void IsValid_ValidIncomeTax_ReturnsTrue()
        {
            long.TryParse(configurations.IncomeTax.ToString(), out _).Should().BeTrue();
        }

        [Fact]
        public void IsValid_ValidMarriedStatus_ReturnsTrue()
        {
            bool.TryParse(configurations.IsMarried.ToString(), out _).Should().BeTrue();
        }

        [Fact]
        public void IsValid_ValidName_ReturnsTrue()
        {
            configurations.Name.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public void IsValid_ValidProfileURI_ReturnsTrue()
        {
            Uri.IsWellFormedUriString(configurations.ProfileUri.ToString(), UriKind.RelativeOrAbsolute).Should().BeTrue();
        }

        [Fact]
        public void IsValid_ValidSalary_ReturnsTrue()
        {
            double.TryParse(configurations.Salary.ToString(), out _).Should().BeTrue();
        }

        [Fact]
        public void IsValid_ValidWorkingHours_ReturnsTrue()
        {
            TimeSpan.TryParse(configurations.WorkingHours.ToString(), out _).Should().BeTrue();
        }

        [Fact]
        public void TemporaryTest()
        {
            configurations.StripeKey.Should().NotBeNullOrEmpty();
        }
    }
}