
using System.Collections.Generic;
using System.Linq;

namespace AuthorizationWorkshop
{
    public class DocumentRepository : IDocumentRepository
    {
        static List<Document> _documents = new List<Document> {
            new Document { Id = 1, Author = "Lil' Wayne" },
            new Document { Id = 2, Author = "Drake" }
        };

        public IEnumerable<Document> Get()
        {
            return _documents;
        }

        public Document Get(int id)
        {
            return (_documents.FirstOrDefault(d => d.Id == id));
        }
    }
}