namespace Dor.Blog.Application.Interfaces
{
    public interface IMapperUtil
    {
        TDestination Map<TOrigin, TDestination>(TOrigin value);
    }
}
