using Bogus;
using TestRail.Models;

namespace TestRail.Factories
{
    public static class ProjectFactory
    {
        public static CreateProjectModel GetProjectModel()
        {
            return new CreateProjectModel
            {
                Name = new Faker().Name.Random.Word(),
                Announcement = new Faker().Lorem.Sentence(5),
                ShowAnnouncement = true
            };
        }
    }
}