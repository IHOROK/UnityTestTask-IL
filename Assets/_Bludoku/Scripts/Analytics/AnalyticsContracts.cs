using System;

namespace _Bludoku.Scripts.Analytics
{
    /// <summary>Provider-neutral contract used by gameplay to report analytics.</summary>
    public interface IAnalytics
    {
        void Track(IAnalyticsEvent analyticsEvent);
    }

    public interface IAnalyticsEvent
    {
        string Name { get; }
        string ToLogString();
    }

    public interface IAnalyticsProvider
    {
        void Track(IAnalyticsEvent analyticsEvent);
    }

    public sealed class AnalyticsBus : IAnalytics
    {
        private readonly System.Collections.Generic.List<IAnalyticsProvider> _providers =
            new System.Collections.Generic.List<IAnalyticsProvider>();

        public void AddProvider(IAnalyticsProvider provider)
        {
            if (provider != null && !_providers.Contains(provider))
                _providers.Add(provider);
        }

        public void RemoveProvider(IAnalyticsProvider provider) => _providers.Remove(provider);

        public void Track(IAnalyticsEvent analyticsEvent)
        {
            if (analyticsEvent == null) return;

            // Iterate a snapshot so providers may safely unregister during dispatch.
            IAnalyticsProvider[] providers = _providers.ToArray();
            foreach (IAnalyticsProvider provider in providers)
                provider.Track(analyticsEvent);
        }
    }
}
