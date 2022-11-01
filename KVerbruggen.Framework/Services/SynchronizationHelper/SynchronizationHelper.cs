using KVerbruggen.Framework.Properties;

namespace KVerbruggen.Framework.Services.SynchronizationHelper
{
    /// <summary>
    /// Helper methods to synchronize actions to an app's synchronization context. For example, a UI thread.
    /// </summary>
    public sealed class SynchronizationHelper
    {
        /// <summary>
        /// The singleton instance
        /// </summary>
        private static SynchronizationHelper? _instance = null;

        /// <summary>
        /// The synchronization context 
        /// </summary>
        private SynchronizationContext? _synchronizationContext = null;

        /// <summary>
        /// A lock object used when creating the singleton instance
        /// </summary>
        private static readonly object threadLock = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="SynchronizationHelper"/> class.
        /// The constructor is private, to manage creation of the singleton instance.
        /// </summary>
        private SynchronizationHelper()
        { }

        /// <summary>
        /// Get the singleton instance.
        /// If SetSynchronizationContext has not been called yet, the synchronization context will be set to the current context.
        /// </summary>
        public static SynchronizationHelper Instance
        {
            get
            {
                lock (threadLock)
                {
                    if (_instance == null)
                        _instance = new SynchronizationHelper();

                    return _instance;
                }
            }
        }

        /// <summary>
        /// Set the synchronization context for the helper
        /// </summary>
        /// <param name="synchronizationContext">The synchronization context to use. If null, SynchronizationContext.Current will be used.</param>
        public void SetSynchronizationContext(SynchronizationContext? synchronizationContext = null)
        {
            _synchronizationContext = synchronizationContext ?? SynchronizationContext.Current;
        }

        /// <summary>
        /// Execute the provided action on the app's synchronization context.
        /// Before this method can be called, the synchronization context must have been set via the constructor or SetSynchronizationContext.
        /// </summary>
        /// <param name="actionToSynchronize">The action to execute on the synchronization context</param>
        public void ExecuteSynchronized(Action actionToSynchronize)
        {
            if (_synchronizationContext == null)
                throw new InvalidOperationException(ErrorResources.SynchronizationContextNotFound);

            _synchronizationContext.Post(new SendOrPostCallback((state) => actionToSynchronize?.Invoke()), null);
        }

        /// <summary>
        /// Execute the provided action on the app's synchronization context and wait for the action to complete.
        /// Before this method can be called, the synchronization context must have been set via the constructor or SetSynchronizationContext.
        /// </summary>
        /// <param name="actionToSynchronize">The action to execute on the synchronization context</param>
        public void ExecuteSynchronizedAndWait(Action actionToSynchronize)
        {
            if (_synchronizationContext == null)
                throw new InvalidOperationException(ErrorResources.SynchronizationContextNotFound);

            _synchronizationContext.Send(new SendOrPostCallback((state) => actionToSynchronize?.Invoke()), null);
        }
    }
}
