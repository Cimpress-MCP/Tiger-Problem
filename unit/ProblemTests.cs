using System;
using Xunit;
using static Microsoft.AspNetCore.Http.StatusCodes;
using static Newtonsoft.Json.JsonConvert;
using static System.UriKind;
// ReSharper disable All

namespace Tiger.Problem.Test
{
    /// <summary>Tests related to <see cref="Problem"/>.</summary>
    public static class ProblemTests
    {
        public static Uri AboutBlank = new Uri(@"about:blank", Absolute);

        public static readonly TheoryData<Problem> _roundtripProblems = new TheoryData<Problem>
        {
            null,
            new Problem(),
            new Problem(new Uri(@"http://cimpress.invalid/", Absolute)),
            new Problem(new Uri(@"http://cimpress.invalid/problems/invalid-order-id", Absolute))
            {
                Title = "Invalid Order ID",
                Detail = "An Order ID must be non-negative.",
                Status = Status400BadRequest,
                Extensions =
                {
                    ["id"] = -42L
                }
            }
        };

        public static readonly TheoryData<Problem> _aboutBlankProblems = new TheoryData<Problem>
        {
            new Problem(),
            new Problem(AboutBlank)
        };

        [Fact(DisplayName = "Type is implicitly about:blank.")]
        static void AboutBlank_Implicit() => Assert.Equal(AboutBlank, new Problem().Type);

        [Fact(DisplayName = "Type is implicitly about:blank, even after round-tripping.")]
        static void AboutBlank_Serialization()
        {
            var actual = DeserializeObject<Problem>(SerializeObject(new Problem()));

            Assert.NotNull(actual);
            Assert.Equal(AboutBlank, actual.Type);
        }

        [Theory(DisplayName = "about:blank is not written into the serialization.")]
        [MemberData(nameof(_aboutBlankProblems))]
        static void Serialization_Implicit(Problem problem)
        {
            var actual = DeserializeObject<dynamic>(SerializeObject(problem));

            Assert.NotNull(actual);
            Assert.Null(actual.Type);
            Assert.Null(actual.type); // note(cosborn) Belt and suspenders.
        }

        [Theory(DisplayName = "A problem object does not change after serialization and deserialization.")]
        [MemberData(nameof(_roundtripProblems))]
        static void Serialization_RoundTrip(Problem problem) =>
            Assert.Equal(problem, DeserializeObject<Problem>(SerializeObject(problem)), new ProblemTestEqualityComparer());

        [Fact(DisplayName = "Extensions are hoisted to the top-level object.")]
        static void Serialization_Extensions()
        {
            var sut = new Problem
            {
                Extensions =
                {
                    ["id"] = -42L
                }
            };

            var actual = DeserializeObject<dynamic>(SerializeObject(sut));

            Assert.NotNull(actual);
            Assert.NotNull(actual.id);
            var id = Assert.IsType<long>((long?)actual.id);
            Assert.Equal(-42L, id);
        }
    }
}
