using Microsoft.AspNetCore.Components.Forms;
using Newtonsoft.Json.Linq;

namespace FrontEnd.Helpers
{
    public interface IConexionApi<TEntity, TObject> where TEntity : class where TObject : class
    {
        Task<IEnumerable<TEntity>> GetAll(string url);
        Task<HttpResponseMessage> GetResponse(string url);
        Task<TEntity> GetByItem(string url);
        Task<TEntity> Post(string url, TObject model);
        Task<JObject> PostAnToken(string url, TObject model);
        Task<string> PostFile(string url, TObject model, string nombre, string rutaParcial);
        Task<TEntity> PostMultipart(string url, IReadOnlyList<IBrowserFile> files);
        Task<IEnumerable<TEntity>> PostAll(string url, TObject model);
        Task<IEnumerable<TEntity>> PostList(string url, List<TObject> model);
        Task<TEntity> Put(string url, TObject model);
        Task<TEntity> Delete(string url, TObject model);
    }
}
