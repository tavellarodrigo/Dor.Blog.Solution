namespace Dor.Blog.Application.Behaviors
{
    public record LogMessages
    {        
        public static string Started { get; } =  "*************** Service Started ***************";
        public static string Finished { get; } = "*************** Service Finished ***************";
        public static string NotFound { get; } = "*************** Not Found ***************";
        public static string Error { get; } = "*************** Error check the exception ***************";

    }
}
