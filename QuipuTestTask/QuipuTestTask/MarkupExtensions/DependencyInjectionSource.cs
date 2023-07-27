using System;
using System.Windows.Markup;

namespace QuipuTestTask.MarkupExtensions;

public class DependencyInjectionSource : MarkupExtension
{
    public Type Type { get; set; }

    public override object ProvideValue(IServiceProvider serviceProvider) =>
        serviceProvider.GetService(Type) ?? throw new Exception($"Dependency of type {Type} is not registered");
}