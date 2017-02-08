using System;
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

            const long id = -42L;
            model.Example = new Problem(new Uri(@"http://cimpress.invalid/problems/invalid-order-id", Absolute))
            {
                Title = "Invalid Order ID",
                Detail = "An Order ID must be non-negative.",
                Status = Status400BadRequest,
                Extensions =
                {
                    [nameof(id)] = id
                }
            };
        }
    }
}
