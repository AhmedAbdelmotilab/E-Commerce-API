namespace E_Commerce.Domain.Entities.SharedModule ;

public abstract class BaseEntity < TKey >
{
    public TKey Id { get ; set ; } = default! ;
}