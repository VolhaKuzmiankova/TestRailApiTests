namespace TestRail.Constants
{
    public static class ErrorMessageConstants
    {
        public const string IncorrectSuiteIdMessage = "Field :suite_id is not a valid test suite.";
        public const string NotAValidProjectIdMessage = "Field :project_id is not a valid or accessible project.";
        public const string AuthenticationFailedMessage ="Authentication failed: invalid or missing user/password or session cookie.";
        public const string MissingNameMessage = "Field :name is a required field.";
        public const string IncorrectNameMessage = "Field :name is too long (250 characters at most).";
        public const string IncorrectProjectIdMessage = "Field :project_id is not a valid ID.";
        public const string NotAValidSuiteMessage = "Field :suite_id is not a valid ID.";
    }
}