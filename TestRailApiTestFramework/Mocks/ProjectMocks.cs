using System.Collections.Generic;
using Newtonsoft.Json;
using TestRail.Constants;
using TestRail.Factories;

namespace TestRail.Mocks
{
    public class ProjectMocks
    {
        public static IEnumerable<object[]> ProjectIncorrectValues()
        {
            var incorrectId = 1;
            var message = ErrorMessageConstants.IncorrectProjectIdMessage;

            return new List<object[]>()
            {
                new object[]
                {
                    incorrectId,
                    message
                }
            };
        }

        public static IEnumerable<object[]> IncorrectFieldName()
        {
            var incorrectName = ProjectFactory.GetProjectModel();
            incorrectName.Name = new string('a', ConstantsForNotes.NotesMaxLength + 1);
            var serializeIncorrectName = JsonConvert.SerializeObject(incorrectName);

            return new List<object[]>()
            {
                new object[]
                {
                    serializeIncorrectName
                }
            };
        }
        
        public static IEnumerable<object[]> ProjectMissingValues()
        {
            var missingName = ProjectFactory.GetProjectModel();
            missingName.Name = null;
            var missingNameMessage = ErrorMessageConstants.MissingNameMessage;
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