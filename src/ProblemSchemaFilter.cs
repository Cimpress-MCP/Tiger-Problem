using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using static Microsoft.AspNetCore.Http.StatusCodes;
using static System.UriKind;

namespace Tiger.Problem
{
    /// <summary>Describes the schema of <see cref="Problem"/>.</summary>
    sealed class ProblemSchemaFilter
        : ISchemaFilter
    {
        /// <inheritdoc/>
        void ISchemaFilter.Apply([NotNull] Schema model, [NotNull] SchemaFilterContext context)
        {
            if (model == null) { throw new ArgumentNullException(nameof(model)); }
            if (context == null) { throw new ArgumentNullException(nameof(context)); }

            model.Description = "Represents an error in an HTTP response.";

            model.Properties["type"].Description = "An identifier of the problem type.";
            model.Properties["title"].Description = "A short, human-readable summary of the problem type.";
            model.Properties["status"].Description = "An HTTP status code for this occurrence of the problem.";
            model.Properties["detail"].Description = "A human-readable explanation specific to this occurrence of the problem.";
            model.Properties["instance"].Description = "An identifier for this occurrence of the problem.";

            model.Example = new Problem(new Uri(@"http://cimpress.invalid/problems/invalid-order-id", Absolute))
            {
                Title = "Invalid Order ID",
                Detail = $"The provided Order ID was a negative number. An Order ID must be non-negative.",
                Status = Status400BadRequest,
                Extensions =
                {
                    ["id"] = -42L
                }
            };
        }
    }
}
