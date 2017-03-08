using System;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using static Microsoft.Extensions.DependencyInjection.ServiceDescriptor;

namespace Tiger.Problem
{
    /// <summary>Extensions to the functionality of <see cref="IMvcBuilder"/>.</summary>
    [PublicAPI]
    public static class MvcBuilderExtensions
    {
        /// <summary>Adds a formatter for Problem+JSON content to the application.</summary>
        /// <param name="builder">An MVC service configurator.</param>
        /// <returns>The modified MVC service configurator.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="builder"/> is <see langword="null"/>.</exception>
        [NotNull]
        public static IMvcBuilder AddProblemJsonFormatter([NotNull] this IMvcBuilder builder)
        {
            if (builder == null) { throw new ArgumentNullException(nameof(builder)); }

            AddProblemJsonFormatterServices(builder.Services);
            return builder;
        }

        static void AddProblemJsonFormatterServices([NotNull] IServiceCollection services)
        {
            services.TryAddEnumerable(Transient<IConfigureOptions<MvcOptions>, MvcProblemJsonMvcOptionsSetup>());
        }
    }
}
