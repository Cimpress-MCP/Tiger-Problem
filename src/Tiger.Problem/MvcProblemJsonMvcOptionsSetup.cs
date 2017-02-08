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
