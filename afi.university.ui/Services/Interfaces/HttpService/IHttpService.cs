namespace afi.university.ui.Services.Interfaces.HttpService
{
    public interface IHttpService
    {
        Task<T> Get<T>(string uri);
        Task<T> Post<T>(string uri, object value);
    }
}
