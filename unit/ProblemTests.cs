// ReSharper disable All

using System;
using Xunit;
using static Microsoft.AspNetCore.Http.StatusCodes;
using static Newtonsoft.Json.JsonConvert;
using static System.UriKind;

namespace Tiger.Problem.Test
{
    /// <summary>Tests related to <see cref="Problem"/>.</summary>
    public class ProblemTests
    {
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
            new Problem(new Uri(@"about:blank", Absolute))
        };

        [Fact(DisplayName = "Type is implicitly about:blank.")]
        public static void AboutBlank()
        {
            // arrange
            var blank = new Uri(@"about:blank", Absolute);
            var sut = new Problem();

            // act
            var actual = sut.Type;

            // assert
            Assert.Equal(blank, actual);
        }

        [Fact(DisplayName = "Type is implicitly about:blank, even after round-tripping.")]
        public static void AboutBlank_Serialization()
        {
            // arrange
            var blank = new Uri(@"about:blank", Absolute);
            var sut = new Problem();

            // act
            var actual = DeserializeObject<Problem>(SerializeObject(sut));

            // assert
            Assert.NotNull(actual);
            Assert.Equal(blank, actual.Type);
        }

        [Theory(DisplayName = "about:blank is not written into the serialization.")]
        [MemberData(nameof(_aboutBlankProblems))]
        public static void AboutBlank_Implicit(Problem problem)
        {
            // act
            var actual = DeserializeObject<dynamic>(SerializeObject(problem));

            // assert
            Assert.NotNull(actual);
            Assert.Null(actual.Type);
            Assert.Null(actual.type); // note(cosborn) Belt and suspenders.
        }

        [Theory(DisplayName = "A problem object does not change after serialization and deserialization.")]
        [MemberData(nameof(_roundtripProblems))]
        public static void Serialization_RoundTrip(Problem problem)
        {
            // act
            var actual = DeserializeObject<Problem>(SerializeObject(problem));

            // assert
            Assert.Equal(problem, actual, new ProblemTestEqualityComparer());
        }

        [Fact(DisplayName = "Extensions are hoisted to the top-level object.")]
        public static void Serialization_Extensions()
        {
            // arrange
            var sut = new Problem
            {
                Extensions =
                {
                    ["id"] = -42L
                }
            };

            // act
            var actual = DeserializeObject<dynamic>(SerializeObject(sut));

            // assert
            Assert.NotNull(actual);
            Assert.NotNull(actual.id);
            var id = Assert.IsType<long>((long?)actual.id);
            Assert.Equal(-42L, id);
        }
    }
}
