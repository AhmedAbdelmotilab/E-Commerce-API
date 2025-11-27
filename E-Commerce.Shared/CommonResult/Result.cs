namespace E_Commerce.Shared.CommonResult ;

public class Result
{
    // Errors
    protected readonly List < Error > _errors = [ ] ;

    // OK -> Success
    protected Result ( ) { }

    // Fail With One Error
    protected Result ( Error error )
    {
        _errors.Add ( error ) ;
    }

    // Fail With Errors
    protected Result ( List < Error > errors )
    {
        _errors.AddRange ( errors ) ;
    }

    public IReadOnlyList < Error > Errors => _errors ;

    // IsSuccess
    public bool IsSuccess => _errors.Count == 0 ;

    // IsFailure
    public bool IsFailure => ! IsSuccess ;

    public static Result Ok ( ) => new Result ( ) ;
    public static Result Fail ( Error error ) => new Result ( error ) ;
    public static Result Fail ( List < Error > errors ) => new Result ( errors ) ;
}

public class Result < TValue > : Result
{
    private readonly TValue _value ;

    // Ok Success With Value
    private Result ( TValue value )
    {
        _value = value ;
    }

    // Fail With One Error
    private Result ( Error error ) : base ( error )
    {
        _value = default ! ;
    }

    // Fail With Errors
    private Result ( List < Error > errors ) : base ( errors )
    {
        _value = default ! ;
    }

    public TValue Value => IsSuccess ? _value : throw new Exception ( "Can Not Access The Value" ) ;

    public static Result < TValue > Ok ( TValue value ) => new ( value ) ;
    public static new Result < TValue > Fail ( Error error ) => new ( error ) ;
    public static new Result < TValue > Fail ( List < Error > errors ) => new ( errors ) ;
    public static implicit operator Result < TValue > ( TValue value ) => Ok ( value ) ;
    public static implicit operator Result < TValue > ( Error error ) => Fail ( error ) ;
    public static implicit operator Result < TValue > ( List < Error > errors ) => Fail ( errors ) ;
}