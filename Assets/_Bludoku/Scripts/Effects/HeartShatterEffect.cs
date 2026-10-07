using UnityEngine;
using UnityEngine.UI;

namespace _Bludoku.Scripts.Effects
{
    public static class HeartShatterEffect
    {
        public static void Play(Image heart, int comboCount)
        {
            if (heart == null || heart.sprite == null)
                return;

            Canvas canvas = heart.GetComponentInParent<Canvas>();
            if (canvas == null)
                return;

            int shardCount = Mathf.Max(0, comboCount);
            if (shardCount == 0)
                return;

            RectTransform canvasRect = canvas.transform as RectTransform;
            heart.enabled = false;

            RectTransform heartRect = heart.rectTransform;
            Rect sourceRect = heart.sprite.textureRect;
            float sliceWidth = sourceRect.width / shardCount;

            for (int i = 0; i < shardCount; i++)
            {
                GameObject shard = new GameObject("Heart Glass Shard", typeof(RectTransform), typeof(CanvasRenderer), typeof(Image), typeof(HeartShardMotion));
                RectTransform rect = (RectTransform)shard.transform;
                rect.SetParent(canvasRect, false);
                rect.anchorMin = rect.anchorMax = new Vector2(0.5f, 0.5f);
                rect.pivot = new Vector2(0.5f, 0.5f);
                float sliceCenterX = (i + 0.5f) / shardCount * heartRect.rect.width - heartRect.rect.width * 0.5f;
                Vector3 sliceWorldPosition = heartRect.TransformPoint(new Vector3(sliceCenterX, 0f, 0f));
                rect.anchoredPosition = canvasRect.InverseTransformPoint(sliceWorldPosition);

                Rect sliceRect = new Rect(sourceRect.x + i * sliceWidth, sourceRect.y, sliceWidth, sourceRect.height);
                Sprite sliceSprite = Sprite.Create(heart.sprite.texture, sliceRect, new Vector2(0.5f, 0.5f), heart.sprite.pixelsPerUnit);
                sliceSprite.name = $"Heart Shard {i}";
                rect.sizeDelta = new Vector2(heartRect.rect.width / shardCount, heartRect.rect.height);
                rect.localRotation = Quaternion.identity;

                Image image = shard.GetComponent<Image>();
                image.sprite = sliceSprite;
                image.raycastTarget = false;
                image.color = new Color(1f, 1f, 1f, EffectsConstants.HeartShatterAlpha);

                float normalizedX = shardCount == 1
                    ? (Random.value < 0.5f ? -1f : 1f)
                    : sliceCenterX / (heartRect.rect.width * 0.5f);
                float horizontalSpeed = normalizedX * EffectsConstants.HeartShatterHorizontalSpeed;
                float spin = normalizedX * EffectsConstants.HeartShatterSpinSpeed;

                shard.GetComponent<HeartShardMotion>().Initialize(
                    new Vector2(horizontalSpeed, -EffectsConstants.HeartShatterInitialDownwardSpeed),
                    EffectsConstants.HeartShatterGravity,
                    spin,
                    EffectsConstants.HeartShatterLifetime);
            }
        }
    }
}
