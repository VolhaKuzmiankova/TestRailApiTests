using System.Collections.Generic;
using Bogus;
using Newtonsoft.Json;
using TestRail.Constants;
using TestRail.Factories;

namespace TestRail.Mocks
{
    public class ProjectMocks
    {
        public static IEnumerable<object[]> ProjectIncorrectValues()
        {
            var incorrectName = ProjectFactory.GetProjectModel();
            incorrectName.Name = new Faker().Random.AlphaNumeric(NotesValidationConstants.NotesMaxLength + 1);
            var serializeProject = JsonConvert.SerializeObject(incorrectName);


            return new List<object[]>()
            {
                new object[]
                {
                    serializeProject,
                    ErrorMessageConstants.IncorrectNameMessage
                }
            };
        }

        public static IEnumerable<object[]> ProjectMissingValues()
        {
            var missingName = ProjectFactory.GetProjectModel();
            missingName.Name = null;

            var serializeModel = JsonConvert.SerializeObject(missingName);

            return new List<object[]>()
            {
                new object[]
                {
                    serializeModel
                }
            };
        }

        public static IEnumerable<object[]> IncorrectProjectId()
        {
            var incorrectId = new Faker().Random.Word();

            return new List<object[]>()
            {
                new object[]
                {
                    incorrectId,
                    ErrorMessageConstants.IncorrectProjectIdMessage
                }
            };
        }
    }
}