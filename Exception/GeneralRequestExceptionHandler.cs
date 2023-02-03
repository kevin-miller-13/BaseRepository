using System;
using System.Threading;
using System.Threading.Tasks;
using Korbitec.Licensing.Application;
using Korbitec.Licensing.Common;
using Korbitec.Licensing.Common.Persistence.TableStorage;
using Korbitec.Licensing.Common.Persistence.TableStorage.DTOs;
using MediatR.Pipeline;

namespace Korbitec.Licensing.FunctionApplication
{
    public class GeneralRequestExceptionHandler<TRequest, TResponse, TException> : IRequestExceptionHandler<TRequest, TResponse, TException>
        where TException : System.Exception
    {
        public async Task Handle(TRequest request, TException exception, RequestExceptionHandlerState<TResponse> state,
            CancellationToken cancellationToken)
        {
            await StoreExceptionInformationAsync(request, exception);
        }

        public async Task StoreExceptionInformationAsync(TRequest request, TException e)
        {
            System.Exception ex = e;

            var baseRequest = request as BaseRequest;

            var fullRequestType = request.GetType();

            var fullRequest = Convert.ChangeType(request, fullRequestType);

            var fullRequestSerialized = GetRequestString(fullRequest);

            var entity = new ExceptionEntity(
                id: baseRequest.RequestId.ToString(),
                time: TableOperations.GetUtcTimeKey(DateTime.UtcNow),
                title: $"{baseRequest.FunctionName}|{fullRequestType.Name}|{ex.Message}",
                type: e.GetType().Name,
                request: fullRequestSerialized,
                stackTrace: ex.BuildExceptionString()
            );

            await TableOperations.Operation_InsertAsync(baseRequest.Binder, entity, SettingKeys.LicensingLogException_Table_Name);
        }

        private string GetRequestString(object request)
        {
            try
            {
                return System.Text.Json.JsonSerializer.Serialize(request);
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}
