using NetArchTest.Rules;
using Xunit;
 

namespace CustomerRegisterationFlow.Application.UnitTest
{
    public class ArchitectureIntegrationTest
    {
        private const string DomainNamesapce = "CustomerRegisterationFlow.Domain";
        private const string DomainEntitiesNamesapce = "CustomerRegisterationFlow.Domain.Entities";
        private const string ApplicationNamesapce = "CustomerRegisterationFlow.Application";
        private const string InfrastructureNamesapce = "CustomerRegisterationFlow. Infrastructure";
        private const string PresistenceNamesapce = "CustomerRegisterationFlow.Presistence";
        private const string PresentationNamesapce = "CustomerRegisterationFlow.Presentation";
        private const string LoggerNamesapce = "CustomerRegisterationFlow.LoggerService";
        public ArchitectureIntegrationTest()
        {
         
        }
        [Fact]
        public void Domain_Should_Not_HaveDependencyOnOtherProjects()
        {
            var assembly = typeof(CustomerRegisterationFlow.Domain.AssemblyReference).Assembly;
            var otherProjects = new[]
            {
                ApplicationNamesapce,
                InfrastructureNamesapce,
                PresistenceNamesapce,
                PresentationNamesapce,
                LoggerNamesapce

            };
            var result = Types.InAssembly(assembly)
               .Should()
               .NotHaveDependencyOnAll(otherProjects)
               .GetResult();
            //Assert
            Assert.True(result.IsSuccessful);
        }
        [Fact]
        public void Application_Should_Not_HaveDependencyOnOtherProjects()
        {
            var assembly = typeof(CustomerRegisterationFlow.Application.AssemblyReference).Assembly;
            var otherProjects = new[]
            {
                InfrastructureNamesapce,
                PresistenceNamesapce,
                PresentationNamesapce,
                LoggerNamesapce

            };
            var result = Types.InAssembly(assembly)
               .Should()
               .NotHaveDependencyOnAll(otherProjects)
               .GetResult();
            //Assert
            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public void Presistence_Should_Not_HaveDependencyOnOtherProjects()
        {
            var assembly = typeof(CustomerRegisterationFlow.Presistence.AssemblyReference).Assembly;
            var otherProjects = new[]
            {
                PresentationNamesapce,
                LoggerNamesapce

            };
            var result = Types.InAssembly(assembly)
               .Should()
               .NotHaveDependencyOnAll(otherProjects)
               .GetResult();
            //Assert
            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public void Infrastructure_Should_Not_HaveDependencyOnOtherProjects()
        {
            var assembly = typeof(CustomerRegisterationFlow.Infrastructure.AssemblyReference).Assembly;
            var otherProjects = new[]
            {
                PresentationNamesapce,
                LoggerNamesapce

            };
            var result = Types.InAssembly(assembly)
               .Should()
               .NotHaveDependencyOnAll(otherProjects)
               .GetResult();
            //Assert
            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public void Application_Should_HaveDependencyOnDomain()
        {
            var assembly = typeof(CustomerRegisterationFlow.Application.AssemblyReference).Assembly;
            var result = Types.InAssembly(assembly).That().HaveName("AddCustomerBasicInfoCommandHandler")
               .Should()
               .HaveDependencyOn(DomainEntitiesNamesapce)
               .GetResult();
            //Assert
            Assert.True(result.IsSuccessful);
        }

 
    }
}
