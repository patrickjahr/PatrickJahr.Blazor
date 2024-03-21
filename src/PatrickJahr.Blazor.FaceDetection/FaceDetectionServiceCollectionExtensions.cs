using Microsoft.Extensions.DependencyInjection;

namespace PatrickJahr.Blazor.FaceDetection
{
    public static class FaceDetectionServiceCollectionExtensions
    {
        public static IServiceCollection AddFaceDetectionService(this IServiceCollection services)
        {
            return services.AddScoped<FaceDetectionService>();
        }
    }
}