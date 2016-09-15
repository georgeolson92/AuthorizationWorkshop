using System.Collections.Generic;

namespace AuthorizationWorkshop
{
    public interface IDocumentRepository
    {
        IEnumerable<Document> Get();

        Document Get(int id);
    }
}