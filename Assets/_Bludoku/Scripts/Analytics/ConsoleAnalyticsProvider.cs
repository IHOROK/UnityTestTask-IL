using UnityEngine;

namespace _Bludoku.Scripts.Analytics
{
    /// <summary>Simple development provider; replace or supplement without changing gameplay.</summary>
    public sealed class ConsoleAnalyticsProvider : IAnalyticsProvider
    {
        public void Track(IAnalyticsEvent analyticsEvent)
        {
            if (analyticsEvent == null) return;

            Debug.Log($"[Analytics] {analyticsEvent.Name} ({analyticsEvent.ToLogString()})");
        }
    }
}
