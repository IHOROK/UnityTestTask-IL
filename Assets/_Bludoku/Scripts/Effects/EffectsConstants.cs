using UnityEngine;

namespace _Bludoku.Scripts.Effects
{
    public static class EffectsConstants
    {
        public const float ParticleSidewaysSpeed = 0.8f;
        public const float ParticleDownwardSpeed = 5f;
        public const float ParticlePseudoGravity = 0.35f;
        public const float ParticleLifetime = 1.25f;
        public const float ParticleStartSize = 1.5f;
        public const float ParticlePrefabVerticalOffset = 1.5f;

        public const float ComboFlightStartDelay = 0.07f;
        public const float ComboFlightDuration = 0.175f;
        public const float ComboFlightGroupStagger = 0.08f;

        public const float CameraShakeDuration = 0.32f;
        public static readonly Vector3 CameraShakeStrength = new Vector3(0.07f, 0.07f, 0f);
        public const int CameraShakeVibrato = 18;
        public const float CameraShakeRandomness = 90f;

        public const float BoosterEntranceDuration = 0.8f;
        public const float BoosterExitDuration = 0.2f;
        public const float BoosterArrivalScale = 1.5f;
        public const float BoosterArrivalScaleUpDuration = 0.25f;
        public const float BoosterArrivalScaleDownDuration = 0.25f;
        public const float ComboPulseInterval = 1f;
        public const float ComboPulseScaleStep = 0.08f;
        public const int ActiveComboPulsesPerBurst = 1;
        public const int WarningComboPulsesPerBurst = 1;
        public const int CriticalComboPulsesPerBurst = 2;
        public const float ComboPulseScalePortion = 0.5f;

        public static readonly Color ActiveComboColor = Color.green;
        public static readonly Color WarningComboColor = Color.yellow;
        public static readonly Color CriticalComboColor = Color.red;
        public const float ComboTextVerticalOffset = -68f;
        public const float ComboTextWidth = 140f;
        public const float ComboTextHeight = 48f;
        public const float ComboTextFontSize = 30f;

        public const float HeartShatterShardMinSize = 15f;
        public const float HeartShatterShardMaxSize = 33f;
        public const float HeartShatterHorizontalSpeed = 240f;
        public const float HeartShatterInitialVerticalSpeed = 100f;
        public const float HeartShatterGravity = 800f;
        public const float HeartShatterMinSpinSpeed = 180f;
        public const float HeartShatterMaxSpinSpeed = 540f;
        public const float HeartShatterLifetime = 1.2f;
        public const float HeartShatterFadeDuration = 0.35f;
    }
}
