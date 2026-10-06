using UnityEngine;

namespace _Bludoku.Scripts.Analytics
{
    /// <summary>Scene entry point that wires the neutral bus to configured providers.</summary>
    public sealed class AnalyticsService : MonoBehaviour
    {
        private static AnalyticsService _instance;
        private readonly AnalyticsBus _bus = new AnalyticsBus();

        public static IAnalytics Instance => _instance != null ? _instance._bus : null;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void InitializeBeforeSceneLoad()
        {
            if (_instance != null) return;

            var serviceObject = new GameObject("AnalyticsService");
            DontDestroyOnLoad(serviceObject);
            serviceObject.AddComponent<AnalyticsService>();
        }

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
                return;
            }

            _instance = this;
            _bus.AddProvider(new ConsoleAnalyticsProvider());
        }

        private void OnDestroy()
        {
            if (_instance == this)
                _instance = null;
        }
    }
}
