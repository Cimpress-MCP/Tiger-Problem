// <copyright file="ProblemJsonOutputFormatter.cs" company="Cimpress, Inc.">
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
using Microsoft.AspNetCore.Mvc.Formatters;
using Newtonsoft.Json;

namespace Tiger.Problem
{
    /// <summary>A <see cref="JsonOutputFormatter"/> for Problem+JSON content.</summary>
    sealed class ProblemJsonOutputFormatter
        : JsonOutputFormatter
    {
        /// <summary>Initializes a new instance of the <see cref="ProblemJsonOutputFormatter"/> class.</summary>
        /// <param name="serializerSettings">The <see cref="JsonSerializerSettings"/>.</param>
        /// <param name="charPool">The <see cref="ArrayPool{T}"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="serializerSettings"/> is <see langword="null" />.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="charPool"/> is <see langword="null" />.</exception>
        public ProblemJsonOutputFormatter(
            [NotNull] JsonSerializerSettings serializerSettings,
            [NotNull] ArrayPool<char> charPool)
            : base(serializerSettings, charPool)
        {
            if (serializerSettings == null) { throw new ArgumentNullException(nameof(serializerSettings)); }
            if (charPool == null) { throw new ArgumentNullException(nameof(charPool)); }

            SupportedMediaTypes.Add("application/problem+json");
        }

        /// <summary>
        /// Returns a value indicating whether or not the given type can be written by this serializer.
        /// </summary>
        /// <param name="type">The object type.</param>
        /// <returns>
        /// <see langword="true"/> if the type can be written;
        /// otherwise, <see langword="false"/>.
        /// </returns>
        protected override bool CanWriteType(Type type) => type == typeof(Problem);
    }
}
