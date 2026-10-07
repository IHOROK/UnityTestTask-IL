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
        public const float ComboTextVerticalOffset = -150f;
        public const float ComboTextWidth = 400f;
        public const float ComboTextHeight = 100f;
        public const float ComboTextFontSize = 72f;

        public const float HeartShatterAlpha = 0.65f;
        public const float HeartShatterHorizontalSpeed = 180f;
        public const float HeartShatterInitialDownwardSpeed = 40f;
        public const float HeartShatterGravity = 1500f;
        public const float HeartShatterSpinSpeed = 45f;
        public const float HeartShatterLifetime = 1.2f;
        public const float HeartShatterFadeDuration = 0.35f;
    }
}
