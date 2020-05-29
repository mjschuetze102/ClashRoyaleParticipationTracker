namespace ClashRoyaleApiQuery.Database
{
    /// <summary>
    /// Provides a contract for all classes which store information to the database should follow.
    /// </summary>
    internal interface IDataStore
    {
        /// <summary>
        /// Calls a set of steps to be performed to store information to the database.
        /// </summary>
        public void StoreAll();
    }
}
