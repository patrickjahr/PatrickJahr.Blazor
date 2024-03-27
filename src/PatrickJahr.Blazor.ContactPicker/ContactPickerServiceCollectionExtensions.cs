using Microsoft.Extensions.DependencyInjection;

namespace PatrickJahr.Blazor.ContactPicker;

public static class ContactPickerCollectionExtensions
{
    public static IServiceCollection AddContactPickerService(this IServiceCollection services) => services.AddScoped<ContactPickerService>();
}
