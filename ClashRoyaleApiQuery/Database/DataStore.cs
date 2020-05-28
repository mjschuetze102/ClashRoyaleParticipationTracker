using ClashRoyaleDataModel.DatabaseContexts;
using System.Collections.Generic;

namespace ClashRoyaleApiQuery.Database
{
    abstract class DataStore<T> : IDataStore where T : class
    {
        /// <summary>
        /// Reference to the database to store the information to
        /// </summary>
        protected readonly ClanParticipationContext _context;

        /// <summary>
        /// Endpoint used to retrieve information from the API
        /// </summary>
        protected readonly string _endpointUrl;

        public DataStore(ClanParticipationContext context, string endpointUrl)
        {
            _context = context;
            _endpointUrl = endpointUrl;
        }

        public void StoreAll()
        {
            IEnumerable<T> data = GetDataFromApi();
            SaveAll(data);
        }

        protected abstract IEnumerable<T> GetDataFromApi();

        protected abstract void SaveAll(IEnumerable<T> data);

    }
}
