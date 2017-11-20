// <copyright file="Problem.cs" company="Cimpress, Inc.">
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
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.SwaggerGen;
using static Newtonsoft.Json.DefaultValueHandling;
using static System.StringComparer;
using static System.UriKind;

namespace Tiger.Problem
{
    /// <summary>Represents an error in an HTTP response.</summary>
    [SwaggerSchemaFilter(typeof(ProblemSchemaFilter))]
    [PublicAPI]
    [SuppressMessage("Microsoft.Guidelines", "CA1724", Justification = "No.")]
    public sealed class Problem
    {
        static readonly Uri s_blank = new Uri(@"about:blank", Absolute);

        readonly Dictionary<string, dynamic> _extensions = new Dictionary<string, dynamic>(Ordinal);

        /// <summary>Initializes a new instance of the <see cref="Problem"/> class.</summary>
        /// <param name="extensions">A collection of extension data members for this problem type.</param>
        public Problem([CanBeNull] IDictionary<string, dynamic> extensions = default)
        {
            if (extensions != null)
            {
                _extensions = new Dictionary<string, dynamic>(extensions, Ordinal);
            }
        }

        /// <summary>Initializes a new instance of the <see cref="Problem"/> class.</summary>
        /// <param name="type">The identifier  of the problem type.</param>
        /// <param name="extensions">A collection of extension data members for this problem type.</param>
        [JsonConstructor]
        public Problem([CanBeNull] Uri type, [CanBeNull] IDictionary<string, dynamic> extensions = default)
            : this(extensions)
        {
            Type = type ?? s_blank;
        }

        /// <summary>Gets an identifier of the problem type</summary>
        [NotNull]
        [JsonProperty(DefaultValueHandling = Ignore)]
        [SuppressMessage("Microsoft:Guidelines", "CA1721", Justification = "That's what it's called.")]
        public Uri Type { get; } = s_blank;

        /// <summary>Gets or sets a short, human-readable summary of the problem type.</summary>
        [JsonProperty(DefaultValueHandling = Ignore)]
        public string Title { get; set; }

        /// <summary>Gets or sets an HTTP status code for this occurrence of the problem.</summary>
        /// <remarks>
        /// This value must be the same as the status code in the HTTP response.
        /// </remarks>
        [JsonProperty(DefaultValueHandling = Ignore)]
        public int? Status { get; set; }

        /// <summary>Gets or sets a human-readable explanation specific to this occurrence of the problem.</summary>
        [JsonProperty(DefaultValueHandling = Ignore)]
        public string Detail { get; set; }

        /// <summary>Gets or sets an identifier for this occurrence of the problem</summary>
        [JsonProperty(DefaultValueHandling = Ignore)]
        public Uri Instance { get; set; }

        /// <summary>Gets a collection of extension data members for this problem type.</summary>
        /// <remarks>
        /// Essentially, any extension data members are namespaced by <see cref="Type"/>.
        /// Thus, they are "for this problem type", not "for this occurrence of the problem".
        /// </remarks>
        [JsonExtensionData, NotNull]
        public IDictionary<string, dynamic> Extensions => _extensions;

        /// <summary>Determines whether <see cref="Type"/> should be serialized.</summary>
        /// <returns>
        /// <see langword="true"/> if <see cref="Type"/> should be serialized;
        /// otherwise, <see langword="false"/>.
        /// </returns>
        public bool ShouldSerializeType() => Type != s_blank;
    }
}
