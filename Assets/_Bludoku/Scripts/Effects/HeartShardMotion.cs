using UnityEngine;
using UnityEngine.UI;

namespace _Bludoku.Scripts.Effects
{
    public class HeartShardMotion : MonoBehaviour
    {
        private RectTransform _rectTransform;
        private Image _image;
        private Vector2 _velocity;
        private float _gravity;
        private float _spinSpeed;
        private float _lifetime;
        private float _age;
        private float _initialAlpha;

        public void Initialize(Vector2 velocity, float gravity, float spinSpeed, float lifetime)
        {
            _rectTransform = (RectTransform)transform;
            _image = GetComponent<Image>();
            _velocity = velocity;
            _gravity = gravity;
            _spinSpeed = spinSpeed;
            _lifetime = lifetime;
            _initialAlpha = _image.color.a;
        }

        private void Update()
        {
            float deltaTime = Time.deltaTime;
            _age += deltaTime;
            _rectTransform.anchoredPosition += _velocity * deltaTime + Vector2.down * (0.5f * _gravity * deltaTime * deltaTime);
            _velocity.y -= _gravity * deltaTime;
            _rectTransform.Rotate(0f, 0f, _spinSpeed * deltaTime);

            float fadeStart = Mathf.Max(0f, _lifetime - EffectsConstants.HeartShatterFadeDuration);
            if (_age >= fadeStart)
            {
                float fadeProgress = Mathf.InverseLerp(fadeStart, _lifetime, _age);
                Color color = _image.color;
                color.a = _initialAlpha * (1f - fadeProgress);
                _image.color = color;
            }

            if (_age >= _lifetime)
                Destroy(gameObject);
        }

        private void OnDestroy()
        {
            if (_image != null && _image.sprite != null)
                Destroy(_image.sprite);
        }
    }
}
