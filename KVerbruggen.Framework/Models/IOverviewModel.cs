namespace KVerbruggen.Framework.Models
{
    /// <summary>
    /// Interface for Overview models
    /// </summary>
    /// <typeparam name="TItem">The type of the items in the overview</typeparam>
    public interface IOverviewModel : IModelBase
    {
        /// <summary>
        /// The allowed page sizes for the overview
        /// </summary>
        IReadOnlyCollection<int> AllowedPageSizes { get; }

        /// <summary>
        /// The number of the currently loaded page
        /// </summary>
        int CurrentPage { get; set; }

        /// <summary>
        /// The currently selected page size
        /// </summary>
        int SelectedPageSize { get; set; }

        /// <summary>
        /// The items currently visible in the overview
        /// </summary>
        IList<IModelBase> Items { get; }

        /// <summary>
        /// The total amount of pages
        /// </summary>
        int PageCount { get; }

        /// <summary>
        /// Refresh the data view
        /// </summary>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>A task representing the action</returns>
        Task Refresh(CancellationToken cancellationToken = default);
    }
}
