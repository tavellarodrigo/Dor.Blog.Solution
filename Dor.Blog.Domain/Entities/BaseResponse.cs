namespace Dor.Blog.Domain.Entities
{
    /// <summary>
    /// used for service responses
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseResponse<T>
    {       
        public T? DataResponse { get; set; }
        public bool Successful { get; set; } = true;
        public List<string> errors { get; set; } = new List<string>();
        
        public BaseResponse()
        {
        }        
        public BaseResponse(T? entity)
        {
            DataResponse = entity;            
        }
        public BaseResponse(T? entity, bool result,string errorMessage)
        {
            DataResponse = entity;
            Successful = result;
            errors.Add(errorMessage);
        }

    }
}
