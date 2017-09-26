// <copyright file="ProblemSchemaFilter.cs" company="Cimpress, Inc.">
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
