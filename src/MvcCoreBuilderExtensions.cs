// <copyright file="MvcCoreBuilderExtensions.cs" company="Cimpress, Inc.">
//   Copyright 2017 Cimpress, Inc.
//
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
//   Unless required by applicable law or agreed to in writing, software
//   distributed under the License is distributed on an "AS IS" BASIS,
//   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//   See the License for the specific language governing permissions and
//   limitations under the License.
// </copyright>

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
    public static class MvcCoreBuilderExtensions
    {
        /// <summary>Adds a formatter for Problem+JSON content to the application.</summary>
        /// <param name="builder">An MVC core service configurator.</param>
        /// <returns>The modified MVC core service configurator.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="builder"/> is <see langword="null"/>.</exception>
        [NotNull]
        public static IMvcCoreBuilder AddProblemJsonFormatter([NotNull] this IMvcCoreBuilder builder)
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
