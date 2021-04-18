using System.Collections.Generic;
using Newtonsoft.Json;
using TestRail.Factories;

namespace TestRail.Mocks
{
    public class ProjectMocks
    {
        public static IEnumerable<object[]> ProjectIncorrectValues()
        {
            var incorrectId = 1;
            var message = "Field :project_id is not a valid or accessible project.";

            return new List<object[]>()
            {
                new object[]
                {
                    incorrectId,
                    message
                }
            };
        }

        public static IEnumerable<object[]> ProjectMissingValues()
        {
            var missingName = ProjectFactory.GetProjectModel();
            missingName.Name = null;
            var missingNameMessage = "Field :name is a required field.";
            var serializeMissingName = JsonConvert.SerializeObject(missingName);


            return new List<object[]>()
            {
                new object[]
                {
                    serializeMissingName,
                    missingNameMessage
                }
            };
        }
    }
}