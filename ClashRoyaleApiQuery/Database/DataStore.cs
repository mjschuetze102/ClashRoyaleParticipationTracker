using ClashRoyaleDataModel.DatabaseContexts;
using System.Collections.Generic;

namespace ClashRoyaleApiQuery.Database
{
    /// <summary>
    /// Stores information gathered from an API to a database.
    /// </summary>
    /// <typeparam name="T">Object retrieved from the API and stored within the database.</typeparam>
    abstract class DataStore<T> : IDataStore where T : class
    {
        /// <summary>
        /// Reference to the database to store the information to.
        /// </summary>
        protected readonly ClanParticipationContext _context;

        /// <summary>
        /// Endpoint used to retrieve information from the API.
        /// </summary>
        protected readonly string _endpointUrl;

        /// <summary>
        /// Initializes store with reference to the database and the endpoint url.
        /// </summary>
        /// <param name="context">Reference to the database the information will be stored to.</param>
        /// <param name="endpointUrl">Endpoint url with which to retrieve information from the API.</param>
        public DataStore(ClanParticipationContext context, string endpointUrl)
        {
            _context = context;
            _endpointUrl = endpointUrl;
        }

        /// <see cref="IDataStore.StoreAll"/>
        public void StoreAll()
        {
            IEnumerable<T> data = GetDataFromApi();
            SaveAll(data);
        }

        /// <summary>
        /// Retrieve the information from the API.
        /// </summary>
        /// <returns>Enumerable list of objects received from the API.</returns>
        protected abstract IEnumerable<T> GetDataFromApi();

        /// <summary>
        /// Saves the collection of data to the database.
        /// </summary>
        /// <param name="data">Items to be modified/ saved to the database.</param>
        protected abstract void SaveAll(IEnumerable<T> data);

    }
}
