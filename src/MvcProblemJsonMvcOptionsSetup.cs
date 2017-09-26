// <copyright file="MvcProblemJsonMvcOptionsSetup.cs" company="Cimpress, Inc.">
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
using System.Buffers;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using static JetBrains.Annotations.ImplicitUseKindFlags;
using static JetBrains.Annotations.ImplicitUseTargetFlags;

namespace Tiger.Problem
{
    /// <summary>Sets up Problem+JSON formatter options for <see cref="MvcOptions"/>.</summary>
    [UsedImplicitly(InstantiatedNoFixedConstructorSignature, WithMembers)]
    class MvcProblemJsonMvcOptionsSetup
        : ConfigureOptions<MvcOptions>
    {
        /// <summary>Initializes a new instance of the <see cref="MvcProblemJsonMvcOptionsSetup"/> class.</summary>
        /// <param name="jsonOptions">The MVC JSON configuration.</param>
        /// <param name="charPool">An <see cref="ArrayPool{T}"/> of <see cref="char"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="charPool" /> is <see langword="null" />.</exception>
        public MvcProblemJsonMvcOptionsSetup(
            IOptions<MvcJsonOptions> jsonOptions,
            ArrayPool<char> charPool)
            : base(options => ConfigureMvc(
                options,
                jsonOptions.Value.SerializerSettings,
                charPool))
        {
        }

        /// <summary>Configures an application to use a Problem+JSON output formatter.</summary>
        /// <param name="options">The MVC configuration.</param>
        /// <param name="serializerSettings">The settings for JSON serialization.</param>
        /// <param name="charPool">An <see cref="ArrayPool{T}"/> of <see cref="char"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="options" /> is <see langword="null" />.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="serializerSettings" /> is <see langword="null" />.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="charPool" /> is <see langword="null" />.</exception>
        public static void ConfigureMvc(
            [NotNull] MvcOptions options,
            [NotNull] JsonSerializerSettings serializerSettings,
            [NotNull] ArrayPool<char> charPool)
        {
            options.OutputFormatters.Add(new ProblemJsonOutputFormatter(serializerSettings, charPool));
        }
    }
}
