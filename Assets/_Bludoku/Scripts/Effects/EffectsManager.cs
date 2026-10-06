using _Bludoku.Scripts.Boards;
using _Bludoku.Scripts.Score;
using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

namespace _Bludoku.Scripts.Effects
{
    public class EffectsManager : MonoBehaviour
    {
        [SerializeField] private Board board;
        [SerializeField] private ParticleSystem particles;
        [SerializeField] private ScoreBoosterView boosterView;
        [SerializeField] private GameObject comboFlyFigurePrefab;

        private ParticleEffect _particleEffect;
        private VibrationEffect _vibrationEffect;
        private readonly List<GameObject> _activeComboEffects = new();
        private Tween _cameraShakeTween;
        
        private void Awake()
        {
            _particleEffect = new ParticleEffect(particles);
            _vibrationEffect = new VibrationEffect();
            
            board.OnFigurePlaced += OnFigurePlaced;
        }

        private void OnFigurePlaced(ClearResult result)
        {
            _vibrationEffect.Play(result);
            _particleEffect.Play(result);
            if (boosterView != null)
                StartCoroutine(PlayComboFlight(result));
        }

        private System.Collections.IEnumerator PlayComboFlight(ClearResult result)
        {
            if (result.ClearedShapes == null || result.ClearedShapes.Count == 0)
                yield break;

            Transform target = boosterView.EffectTarget;
            if (target == null)
                yield break;

            Vector3 targetPosition = target.position;
            Camera camera = Camera.main;
            Canvas canvas = target.GetComponentInParent<Canvas>();
            if (camera != null && canvas != null && canvas.renderMode == RenderMode.ScreenSpaceOverlay)
            {
                Vector2 targetScreenPosition = RectTransformUtility.WorldToScreenPoint(null, target.position);
                float boardDepth = Mathf.Abs(board.transform.position.z - camera.transform.position.z);
                targetPosition = camera.ScreenToWorldPoint(new Vector3(
                    targetScreenPosition.x, targetScreenPosition.y, boardDepth));
                targetPosition.z = board.transform.position.z;
            }

            const float startDelay = 0.07f;//0.15f;
            const float flightDuration = 0.175f;//0.35f;
            const float groupStagger = 0.08f;

            foreach (ClearedShape shape in result.ClearedShapes)
            {
                GameObject effect = CreateShapeEffect(shape);
                _activeComboEffects.Add(effect);
                yield return new WaitForSeconds(startDelay + groupStagger);

                effect.transform.DOScale(Vector3.zero, flightDuration).SetEase(Ease.InQuart);
                effect.transform.DOMove(targetPosition, flightDuration)
                    .SetEase(Ease.InQuart)
                    .OnComplete(() =>
                    {
                        if (effect != null)
                        {
                            _activeComboEffects.Remove(effect);
                            Destroy(effect);
                        }
                        boosterView.ApplyClearedGroups(1);
                        ShakeCamera();
                    });
            }
        }

        private void ShakeCamera()
        {
            Camera camera = Camera.main;
            if (camera == null) return;

            _cameraShakeTween?.Kill();
            _cameraShakeTween = camera.transform.DOShakePosition(
                duration: 0.16f,
                strength: new Vector3(0.07f, 0.07f, 0f),
                vibrato: 18,
                randomness: 90f,
                snapping: false,
                fadeOut: true);
        }

        private GameObject CreateShapeEffect(ClearedShape shape)
        {
            GameObject effect = Instantiate(comboFlyFigurePrefab, shape.Center, Quaternion.identity);
            effect.name = $"Combo {shape.Type} Effect";
            effect.transform.position = shape.Center;
            effect.transform.localScale = new Vector3(shape.Size.x, shape.Size.y, 1f);

            SpriteRenderer renderer = effect.GetComponent<SpriteRenderer>();
            renderer.color = Color.white;
            return effect;
        }

        private void OnDestroy()
        {
            if (board != null)
                board.OnFigurePlaced -= OnFigurePlaced;

            _cameraShakeTween?.Kill();

            foreach (GameObject effect in _activeComboEffects)
            {
                if (effect == null) continue;
                effect.transform.DOKill();
                Destroy(effect);
            }
            _activeComboEffects.Clear();
        }
    }
}
