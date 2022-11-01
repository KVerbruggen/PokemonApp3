using KVerbruggen.Framework.Maui.Commands.Generic;
using KVerbruggen.Framework.Models;

namespace KVerbruggen.Framework.Maui.ViewModels
{
    public interface IOverviewViewModel : IViewModel<IOverviewModel>
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
        /// The command to load any other page
        /// </summary>
        RelayCommand RefreshCommand { get; }

        /// <summary>
        /// Refresh the data view
        /// </summary>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>A task representing the action</returns>
        Task Refresh(CancellationToken cancellationToken = default);
    }
}
