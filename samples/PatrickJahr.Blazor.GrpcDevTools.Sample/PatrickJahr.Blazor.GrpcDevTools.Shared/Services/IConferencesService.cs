using System.ServiceModel;
using PatrickJahr.Blazor.GrpcDevTools.Shared.DTO;

namespace PatrickJahr.Blazor.GrpcDevTools.Shared.Services;

[ServiceContract]
public interface IConferencesService
{
    Task<IEnumerable<ConferenceOverview>> ListConferencesAsync();
    Task<ConferenceDetailModel?> GetConferenceDetailsAsync(ConferenceDetailsRequest request);
    Task<ConferenceDetailModel> AddNewConferenceAsync(ConferenceDetailModel conference);
    Task UpdateConferenceAsync(ConferenceUpdateRequest request);
    Task DeleteConferenceAsync(ConferenceDetailsRequest request);
}