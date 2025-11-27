namespace E_Commerce.Shared.CommonResult ;

public class Error
{
    private Error ( string code , string description , ErrorType errorType )
    {
        Code = code ;
        Description = description ;
        ErrorType = errorType ;
    }

    public string Code { get ; }
    public string Description { get ; }
    public ErrorType ErrorType { get ; }

    // Static Factory Methods To Create Error
    public static Error Failure ( string code = "General.Failure" , string description = "General Failure Has Occurred" )
    {
        return new Error ( code , description , ErrorType.Failure ) ;
    }

    public static Error Validation ( string code = "General.Validation" , string description = "Validation Error Has Occurred" )
    {
        return new Error ( code , description , ErrorType.Validation ) ;
    }

    public static Error NotFound ( string code = "General.NotFound" , string description = "The Requested Resource Has Not Found" )
    {
        return new Error ( code , description , ErrorType.NotFound ) ;
    }

    public static Error Unauthorized ( string code = "General.Unauthorized" , string description = "You Are Not Authorized" )
    {
        return new Error ( code , description , ErrorType.Unauthorized ) ;
    }

    public static Error Forbidden ( string code = "General.Forbidden" , string description = "You Do Not Have Permission" )
    {
        return new Error ( code , description , ErrorType.Forbidden ) ;
    }

    public static Error InvalidCredentials (
        string code = "General.InvalidCredentials" , string description = "The Credentials Are Not Valid" )
    {
        return new Error ( code , description , ErrorType.InvalidCredentials ) ;
    }
}