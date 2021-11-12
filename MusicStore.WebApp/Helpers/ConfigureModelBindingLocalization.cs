using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;

namespace MusicStore.WebApp.Helpers
{
    public class ConfigureModelBindingLocalization : IConfigureOptions<MvcOptions>
    {
         private readonly IServiceScopeFactory _serviceFactory;
         private readonly IServiceCollection _serviceCollection;
    public ConfigureModelBindingLocalization(IServiceScopeFactory serviceFactory)
    {
        _serviceFactory = serviceFactory;
    }

    public void Configure(MvcOptions options)
    {
        using(var scope = _serviceFactory.CreateScope())
        {
            var provider = scope.ServiceProvider;
            var F = _serviceCollection.BuildServiceProvider().GetService<IStringLocalizerFactory>();
            var L = F.Create("ModelBindingMessages", "AspNetCoreLocalizationSample");
            options.ModelBindingMessageProvider.SetValueIsInvalidAccessor(
                (x) => L["The value '{0}' is invalid.", x]);
            options.ModelBindingMessageProvider.SetValueMustBeANumberAccessor(
                (x) => L["The field {0} must be a number.", x]);
            options.ModelBindingMessageProvider.SetMissingBindRequiredValueAccessor(
                (x) => L["A value for the '{0}' property was not provided.", x]);
            options.ModelBindingMessageProvider.SetAttemptedValueIsInvalidAccessor(
                (x, y) => L["The value '{0}' is not valid for {1}.", x, y]);
            options.ModelBindingMessageProvider.SetMissingKeyOrValueAccessor( 
                () => L["A value is required."]);
            options.ModelBindingMessageProvider.SetUnknownValueIsInvalidAccessor(
                (x) => L["The supplied value is invalid for {0}.", x]);
            options.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor(
                (x) => L["Null value is invalid.", x]);
        }
    }
    }
}