namespace E_Commerce.Services.Exceptions ;

public abstract class NotFoundException ( string Message ) : Exception ( Message ) { }

public sealed class ProductNotFound ( int id ) : NotFoundException ( $"Product With {id} Is Not Found" ) { }

public sealed class BasketNotFound ( string id ) : NotFoundException ( $"Basket With {id} Is Not Found" ) { }