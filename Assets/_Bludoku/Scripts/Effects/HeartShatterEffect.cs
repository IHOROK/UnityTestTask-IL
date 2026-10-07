using UnityEngine;
using UnityEngine.UI;

namespace _Bludoku.Scripts.Effects
{
    public static class HeartShatterEffect
    {
        private static Sprite _shardSprite;

        public static void Play(Image heart, int comboCount)
        {
            if (heart == null || heart.sprite == null)
                return;

            Canvas canvas = heart.GetComponentInParent<Canvas>();
            if (canvas == null)
                return;

            RectTransform canvasRect = canvas.transform as RectTransform;
            Camera eventCamera = canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : canvas.worldCamera;
            Vector2 screenPosition = RectTransformUtility.WorldToScreenPoint(eventCamera, heart.rectTransform.position);
            if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, screenPosition, eventCamera, out Vector2 localPosition))
                return;

            Color sourceColor = heart.color;
            heart.enabled = false;
            Sprite shardSprite = GetShardSprite();

            int shardCount = Mathf.Max(0, comboCount);
            for (int i = 0; i < shardCount; i++)
            {
                GameObject shard = new GameObject("Heart Glass Shard", typeof(RectTransform), typeof(CanvasRenderer), typeof(Image), typeof(HeartShardMotion));
                RectTransform rect = (RectTransform)shard.transform;
                rect.SetParent(canvasRect, false);
                rect.anchorMin = rect.anchorMax = new Vector2(0.5f, 0.5f);
                rect.pivot = new Vector2(0.5f, 0.5f);
                rect.anchoredPosition = localPosition + Random.insideUnitCircle * 8f;

                float size = Random.Range(EffectsConstants.HeartShatterShardMinSize, EffectsConstants.HeartShatterShardMaxSize);
                rect.sizeDelta = new Vector2(size * Random.Range(0.65f, 1f), size);
                rect.localRotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));

                Image image = shard.GetComponent<Image>();
                image.sprite = shardSprite;
                image.raycastTarget = false;
                image.color = Color.Lerp(sourceColor, Color.white, Random.Range(0.25f, 0.75f));

                float horizontalSpeed = Random.Range(-EffectsConstants.HeartShatterHorizontalSpeed, EffectsConstants.HeartShatterHorizontalSpeed);
                float verticalSpeed = Random.Range(-EffectsConstants.HeartShatterInitialVerticalSpeed * 0.25f, EffectsConstants.HeartShatterInitialVerticalSpeed);
                float spin = Random.Range(EffectsConstants.HeartShatterMinSpinSpeed, EffectsConstants.HeartShatterMaxSpinSpeed);
                if (Random.value < 0.5f)
                    spin = -spin;

                shard.GetComponent<HeartShardMotion>().Initialize(
                    new Vector2(horizontalSpeed, verticalSpeed),
                    EffectsConstants.HeartShatterGravity,
                    spin,
                    EffectsConstants.HeartShatterLifetime);
            }
        }

        private static Sprite GetShardSprite()
        {
            if (_shardSprite != null)
                return _shardSprite;

            const int textureSize = 16;
            Texture2D texture = new Texture2D(textureSize, textureSize, TextureFormat.RGBA32, false)
            {
                filterMode = FilterMode.Bilinear,
                wrapMode = TextureWrapMode.Clamp,
                name = "Heart Glass Shard Texture"
            };

            for (int y = 0; y < textureSize; y++)
            {
                for (int x = 0; x < textureSize; x++)
                {
                    float edge = Mathf.Lerp(0f, textureSize - 1, (float)y / (textureSize - 1));
                    bool inside = x >= textureSize * 0.5f - edge * 0.5f && x <= textureSize * 0.5f + edge * 0.5f;
                    texture.SetPixel(x, y, inside ? Color.white : Color.clear);
                }
            }

            texture.Apply();
            _shardSprite = Sprite.Create(texture, new Rect(0f, 0f, textureSize, textureSize), new Vector2(0.5f, 0.5f), textureSize);
            _shardSprite.name = "Heart Glass Shard";
            return _shardSprite;
        }
    }
}
