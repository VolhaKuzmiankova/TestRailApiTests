using Bogus;
using TestRail.Models;

namespace TestRail.Factories
{
    public static class ProjectFactory
    {
        public static CreateProjectModel GetProjectModel()
        {
            return new Faker<CreateProjectModel>()
                .RuleFor(p => p.Name, f => f.Name.FullName())
                .RuleFor(p => p.Announcement, f => f.Lorem.Sentence(5))
                .RuleFor(p => p.ShowAnnouncement, f => f.Random.Bool())
                .Generate();
        }
    }
}