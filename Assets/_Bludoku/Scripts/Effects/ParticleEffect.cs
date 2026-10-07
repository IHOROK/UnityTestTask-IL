using _Bludoku.Scripts.Boards;
using UnityEngine;

namespace _Bludoku.Scripts.Effects
{
    public class ParticleEffect
    {
        ParticleSystem _particle;

        public ParticleEffect(ParticleSystem particleSystem)
        {
            _particle = particleSystem;
        }
        
        public void Play(ClearResult result)
        {
            foreach (var pos in result.ClearedPositions)
            {
                var ps = Object.Instantiate(_particle, pos, Quaternion.identity);
                ConfigureMotion(ps, pos);
                ps.Play();
                ps.gameObject.AddComponent<AutoDestroyParticle>();
            }
        }

        private static void ConfigureMotion(ParticleSystem particleSystem, Vector3 origin)
        {
            var main = particleSystem.main;
            main.simulationSpace = ParticleSystemSimulationSpace.World;
            main.gravityModifier = EffectsConstants.ParticlePseudoGravity;
            main.startLifetime = EffectsConstants.ParticleLifetime;
            main.startSpeed = 0f;
            main.startSize = EffectsConstants.ParticleStartSize;

            // The prefab's child system is offset; compensate in world space while preserving the spawn point.
            particleSystem.transform.position = origin;
            Vector3 particleOffset = particleSystem.transform.localRotation * Vector3.up * EffectsConstants.ParticlePrefabVerticalOffset;
            particleSystem.transform.position -= particleOffset;

            var velocity = particleSystem.velocityOverLifetime;
            velocity.enabled = true;
            velocity.space = ParticleSystemSimulationSpace.World;
            // Use the same TwoConstants mode for every axis; Unity rejects mixed curve modes.
            velocity.x = new ParticleSystem.MinMaxCurve(0f, EffectsConstants.ParticleSidewaysSpeed);
            velocity.y = new ParticleSystem.MinMaxCurve(-EffectsConstants.ParticleDownwardSpeed, -EffectsConstants.ParticleDownwardSpeed * 0.5f);
            velocity.z = new ParticleSystem.MinMaxCurve(0f, 0f);
            velocity.orbitalX = new ParticleSystem.MinMaxCurve(0f, 0f);
            velocity.orbitalY = new ParticleSystem.MinMaxCurve(0f, 0f);
            velocity.orbitalZ = new ParticleSystem.MinMaxCurve(0f, 0f);
            velocity.radial = new ParticleSystem.MinMaxCurve(0f, 0f);
            velocity.speedModifier = new ParticleSystem.MinMaxCurve(1f, 1f);

            // Override the prefab's grow-over-lifetime curve with a clear shrink-to-zero curve.
            var size = particleSystem.sizeOverLifetime;
            size.enabled = true;
            size.size = new ParticleSystem.MinMaxCurve(1f, new AnimationCurve(
                new Keyframe(0f, 1f),
                new Keyframe(1f, 0f)));
        }
    }
}
