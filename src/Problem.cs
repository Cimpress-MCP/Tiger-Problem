using System;
using System.Collections.Generic;
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
    public sealed class Problem
    {
        static readonly Uri _blank = new Uri(@"about:blank", Absolute);

        /// <summary>Gets an identifier of the problem type</summary>
        [NotNull]
        [JsonProperty(DefaultValueHandling = Ignore)]
        public Uri Type { get; } = _blank;

        /// <summary>Gets or sets a short, human-readable summary of the problem type</summary>
        [JsonProperty(DefaultValueHandling = Ignore)]
        public string Title { get; set; }

        /// <summary>Gets or sets an HTTP status code for this occurrence of the problem</summary>
        /// <remarks>
        /// This value must be the same as the status code in the HTTP response.
        /// </remarks>
        [JsonProperty(DefaultValueHandling = Ignore)]
        public int? Status { get; set; }

        /// <summary>Gets or sets a human-readable explanation specific to this occurrence of the problem</summary>
        [JsonProperty(DefaultValueHandling = Ignore)]
        public string Detail { get; set; }

        /// <summary>Gets or sets an identifier for this occurrence of the problem</summary>
        [JsonProperty(DefaultValueHandling = Ignore)]
        public Uri Instance { get; set; }

        /// <summary>Gets a collection of extension data members for this problem type. </summary>
        /// <remarks>
        /// Essentially, any extension data members are namespaced by <see cref="Type"/>.
        /// Thus, they are "for this problem type", not "for this occurrence of the problem".
        /// </remarks>
        /* note(cosborn)
         * Due to a bug in Newtonsoft.Json, this property's keys will be serialized
         * with the provided capitalization, not as specified in the startup.
         */
        [JsonExtensionData, NotNull]
        public IDictionary<string, dynamic> Extensions { get; } =
            new Dictionary<string, dynamic>(OrdinalIgnoreCase);

        /// <summary>Initializes a new instance of the <see cref="Problem"/> class.</summary>
        public Problem()
        {
        }

        /// <summary>Initializes a new instance of the <see cref="Problem"/> class.</summary>
        /// <param name="type">The identifier  of the problem type.</param>
        [JsonConstructor]
        public Problem([CanBeNull] Uri type)
        {
            Type = type ?? _blank;
        }

        /// <summary>Determines whether <see cref="Type"/> should be serialized.</summary>
        /// <returns>
        /// <see langword="true"/> if <see cref="Type"/> should be serialized;
        /// otherwise, <see langword="false"/>.
        /// </returns>
        public bool ShouldSerializeType() => Type != _blank;
    }
}
