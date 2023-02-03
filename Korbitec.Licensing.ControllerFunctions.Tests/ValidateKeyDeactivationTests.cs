using Korbitec.Licensing.Application.Queries.ValidateKeyDeactivation;
using Korbitec.Licensing.Common;
using Korbitec.Licensing.ControllerFunctions.Tests.Helpers;
using Korbitec.Licensing.ControllerFunctions.Tests.Models;
using Korbitec.Licensing.FunctionApplication.Dtos;
using Korbitec.Licensing.FunctionApplication.Exception;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using static Korbitec.Licensing.ControllerFunctions.Tests.TestBase;

namespace Korbitec.Licensing.ControllerFunctions.Tests
{
    public class ValidateKeyDeactivationTests : TestBase, IClassFixture<HostFixture>
    {
        private const string ActivationKeyParameterName = "activationkey";

        private readonly HostFixture _hostFixture;

        private readonly Binder Binder = new Mock<Binder>().Object;

        private readonly ExecutionContext ExecutionContext = new Mock<ExecutionContext>().Object;

        public ValidateKeyDeactivationTests(HostFixture hostFixture)
        {
            _hostFixture = hostFixture;
        }

        [Fact]
        public async Task RunValidateKeyDeactivation_NoActivationCodeInRequest_ReturnsBadRequestObjectResult()
        {
            // Arrange
            var request = new DefaultHttpRequest(new DefaultHttpContext());

            // Act
            var response = (ObjectResult)await _hostFixture.ValidateKeyDeactivation.RunValidateKeyDeactivationAsync(request, Binder, ExecutionContext);

            // Assert

            Assert.IsType<ExceptionCommon.BadRequestObjectResult>(response);

            var baseMessageResponse = (BaseMessageResponse)response.Value;
            Assert.True(baseMessageResponse.HasError);
            Assert.True(baseMessageResponse.ErrorCode != 0);
            Assert.False(string.IsNullOrEmpty(baseMessageResponse.ErrorMessage));
        }

        [Fact]
        public async Task RunValidateKeyDeactivation_InvalidActivationCodeInRequest_ReturnsNotFoundResult()
        {
            const string InvalidActivationCode = "INVALID CODE";

            // Arrange
            Tuple<string, string> requestParams = RequestHelpers.MakeHttpRequestParameter(ActivationKeyParameterName, InvalidActivationCode);
            DefaultHttpRequest request = RequestHelpers.BuildHttpRequest(requestParams);

            // Act
            var response = (ObjectResult)await _hostFixture.ValidateKeyDeactivation.RunValidateKeyDeactivationAsync(request, Binder, ExecutionContext);

            // Assert

            Assert.IsType<ExceptionCommon.NotFoundResult>(response);

            var baseMessageResponse = (BaseMessageResponse)response.Value;
            Assert.True(baseMessageResponse.HasError);
            Assert.True(baseMessageResponse.ErrorCode != 0);
            Assert.False(string.IsNullOrEmpty(baseMessageResponse.ErrorMessage));
        }

        [Theory]
        [InlineData(1, false)]
        [InlineData(0, true)]
        public async Task RunValidateKeyDeactivation_ValidActivationCodeInRequest_ReturnsKeyValidAndDeactivated(byte licensingServerStatus, bool expectedResult)
        {
            LicensingServer licensingServer = null;
            LicensingServerActivationCode licensingServerActivationCode = null;

            try
            {
                // Arrange

                ActivationCode activationCode = (await DbHelpers.GenerateAndInsertActivationCodeRecordsAsync(_hostFixture)).FirstOrDefault();

                licensingServer = (await DbHelpers.GenerateAndInsertLicensingServerRecordsAsync(_hostFixture, licensingServerStatus)).FirstOrDefault();

                licensingServerActivationCode = new LicensingServerActivationCode(activationCode.ActivationCodeId, licensingServer.ServerId);
                await DbHelpers.InsertLicensingServerActivationCodeRecordsAsync(_hostFixture, licensingServerActivationCode);

                Tuple<string, string> requestParams = RequestHelpers.MakeHttpRequestParameter(ActivationKeyParameterName, activationCode.Code);
                DefaultHttpRequest request = RequestHelpers.BuildHttpRequest(requestParams);

                // Act
                var response = (ObjectResult)await _hostFixture.ValidateKeyDeactivation.RunValidateKeyDeactivationAsync(request, Binder, ExecutionContext);

                // Assert

                Assert.IsType<OkObjectResult>(response);

                var validateKeyDeactivationResponseDto = (ValidateKeyDeactivationResponseDto)response.Value;
                Assert.IsType<ValidateKeyDeactivationResponseDto>(validateKeyDeactivationResponseDto);
                Assert.Equal(expectedResult, validateKeyDeactivationResponseDto.KeyValidAndDeactivated);
            }
            finally
            {
                // Cleanup
                await DbHelpers.DeleteLicensingServerActivationCodeRecordsAsync(_hostFixture, licensingServerActivationCode);
                await DbHelpers.DeleteLicensingServerRecordsAsync(_hostFixture, licensingServer);
            }
        }
    }
}
