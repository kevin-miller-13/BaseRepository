using System;
using Korbitec.Licensing.Application.Exceptions;
using Korbitec.Licensing.Application.Queries.ValidateKeyDeactivation;
using Korbitec.Licensing.Application.Queries.ValidateKeyDeactivation.InfrastructureContracts;
using Korbitec.Licensing.Application.Queries.ValidateKeyDeactivation.InfrastructureContracts.Models;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using Microsoft.Azure.WebJobs;
using Xunit;


namespace Korbitec.Licensing.Application.Tests
{
    public class ValidateKeyDeactivationTests
    {
        private static readonly Fixture Fixture = new Fixture();

        private static readonly Guid RequestId = Fixture.Create<Guid>();

        private static readonly string FunctionName = Fixture.Create<string>();

        private static readonly Mock<IBinder> Binder = new Mock<IBinder>();

        [Fact]
        public async Task Handle_ActivationKeyNotSupplied_ThrowsMissingActivationKeyException()
        {
            string ACTIVATIONKEY = "";

            var repo = new Mock<IValidateKeyDeactivationRepository>();
            var handler = new ValidateKeyDeactivationHandler(repo.Object);

            repo.Setup(x => x.GetActivationKeyStatus(ACTIVATIONKEY))
                  .Throws(new MissingActivationKeyException())
                  .Verifiable();

            await Assert.ThrowsAsync<MissingActivationKeyException>(() =>
                handler.Handle(new ValidateKeyDeactivationRequest(RequestId, FunctionName, Binder.Object, ACTIVATIONKEY), CancellationToken.None));
            repo.Verify();
        }

        [Fact]
        public async Task Handle_InvalidActivationKeySupplied_ThrowsActivationKeyNotFoundException()
        {
            string ACTIVATIONKEY = "I am an activation key";

            var repo = new Mock<IValidateKeyDeactivationRepository>();
            var handler = new ValidateKeyDeactivationHandler(repo.Object);

            repo.Setup(x => x.GetActivationKeyStatus(ACTIVATIONKEY))
                  .Throws(new ActivationKeyNotFoundException())
                  .Verifiable();

            await Assert.ThrowsAsync<ActivationKeyNotFoundException>(() =>
                handler.Handle(new ValidateKeyDeactivationRequest(RequestId, FunctionName, Binder.Object, ACTIVATIONKEY), CancellationToken.None));
            repo.Verify();
        }

        [Fact]
        public async Task Handle_ValidActivationKeySupplied_ReturnsActivationStatus()
        {
            string ACTIVATIONKEY = "I am an activation key";

            var repo = new Mock<IValidateKeyDeactivationRepository>();
            var handler = new ValidateKeyDeactivationHandler(repo.Object);

            repo.Setup(x => x.GetActivationKeyStatus(ACTIVATIONKEY))
                  .ReturnsAsync(new ActivationKeyStatusModel() { ServerDeleted = false, ServerStatus = 1 })
                  .Verifiable();

            var response = await handler.Handle(new ValidateKeyDeactivationRequest(RequestId, FunctionName, Binder.Object, ACTIVATIONKEY), CancellationToken.None);
            repo.Verify();
            Assert.True(response.ServerDeleted == false && response.ServerStatus == 1);
        }
    }
}
