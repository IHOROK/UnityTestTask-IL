using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace _Bludoku.Scripts.Score
{
    public class ScoreBoosterView : MonoBehaviour
    {
        [SerializeField] private Transform booster;
        [SerializeField] private Image heart;
        [SerializeField] private TMP_Text comboText;
        [SerializeField] private Color activeColor = Color.green;
        [SerializeField] private Color warningColor = Color.yellow;
        [SerializeField] private Color criticalColor = Color.red;
        
        private bool _isBoosterEnabled;
        private Tween _pulseTween;
        private Sequence _comboAnimation;
        private int _comboCount;
        private bool _comboActive;
        private ComboState _comboState;
        private int _visualComboCount;
        private Sequence _arrivalTween;
        private ComboState _arrivalComboState;

        public Transform EffectTarget => comboText != null ? comboText.transform : booster;

        private void Awake()
        {
            booster.localScale = Vector3.zero;
            _visualComboCount = _comboCount;
            if (comboText == null)
                comboText = CreateComboText();
        }

        private TMP_Text CreateComboText()
        {
            GameObject textObject = new GameObject("Combo Multiplier", typeof(RectTransform));
            textObject.transform.SetParent(booster, false);

            RectTransform rect = textObject.GetComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.anchoredPosition = new Vector2(0f, -68f);
            rect.sizeDelta = new Vector2(140f, 48f);

            TMP_Text text = textObject.AddComponent<TextMeshProUGUI>();
            text.font = TMP_Settings.defaultFontAsset;
            text.fontSize = 30f;
            text.alignment = TextAlignmentOptions.Center;
            text.color = Color.white;
            text.raycastTarget = false;
            textObject.SetActive(false);
            return text;
        }

        public void SetBoosterEnabled(bool boosterEnabled)
        {
            if (_isBoosterEnabled == boosterEnabled)
                return;

            booster.DOKill();
            _pulseTween?.Kill();
            _pulseTween = null;

            if (boosterEnabled)
            {
                booster.transform.DOScale(Vector3.one, 0.8f)
                    .SetEase(Ease.OutElastic)
                    .OnComplete(StartPulse);
            }
            else
            {
                booster.transform.DOScale(Vector3.zero, 0.2f).SetEase(Ease.InBack);
            }

            _isBoosterEnabled = boosterEnabled;
        }

        public void SetComboState(ComboState state)
        {
            if (heart == null) return;

            if (state == ComboState.Inactive)
            {
                heart.enabled = false;
                if (comboText != null)
                    comboText.gameObject.SetActive(false);
                _comboActive = false;
                StopComboAnimation();
                return;
            }

            heart.enabled = true;
            heart.color = state switch
            {
                ComboState.Active => activeColor,
                ComboState.Warning => warningColor,
                _ => criticalColor
            };
        }

        public void SetCombo(int comboCount, ComboState state)
        {
            SetComboState(state);
            _comboCount = comboCount;
            _comboActive = state != ComboState.Inactive;
            _comboState = state;
            _visualComboCount = comboCount;

            if (comboText != null)
            {
                comboText.gameObject.SetActive(comboCount > 1);
                comboText.text = comboCount > 1 ? $"X{comboCount}" : string.Empty;
            }

            StartComboAnimation();
        }

        public void SetArrivalComboState(ComboState state)
        {
            _arrivalComboState = state;
        }

        public void ApplyClearedGroups(int groupsCount)
        {
            for (int i = 0; i < groupsCount; i++)
            {
                _visualComboCount++;
                _comboCount = _visualComboCount;
                _comboState = _arrivalComboState;
                _comboActive = _arrivalComboState != ComboState.Inactive;
                SetComboState(_arrivalComboState);
                if (comboText != null && _visualComboCount > 1)
                {
                    comboText.gameObject.SetActive(true);
                    comboText.text = $"X{_visualComboCount}";
                }

                StartComboAnimation();

                if (EffectTarget != null)
                {
                    _arrivalTween?.Kill();
                    EffectTarget.DOKill();
                    EffectTarget.localScale = Vector3.one;
                    _arrivalTween = DOTween.Sequence()
                        .Append(EffectTarget.DOScale(1.5f, 0.25f).SetEase(Ease.OutBack))
                        .Append(EffectTarget.DOScale(1f, 0.25f).SetEase(Ease.InOutSine));
                }
            }
        }

        private void StartPulse()
        {
            StartComboAnimation();
        }

        private void StartComboAnimation()
        {
            StopComboAnimation();
            _arrivalTween?.Kill();

            if (!_comboActive)
                return;

            int pulsesPerBurst = _comboState switch
            {
                ComboState.Active => 1,
                ComboState.Warning => 1,
                ComboState.Critical => 2,
                _ => 0
            };
            if (pulsesPerBurst == 0)
                return;

            float pulseDuration = 1f / pulsesPerBurst;
            float scaleStep = 0.08f;
            Transform pulseTarget = booster;
            pulseTarget.DOKill();
            pulseTarget.localScale = Vector3.one;
            _comboAnimation = DOTween.Sequence()
                .AppendInterval(1f)
                .Append(pulseTarget.DOScale(1f + scaleStep, pulseDuration * 0.5f).SetEase(Ease.InOutSine))
                .Append(pulseTarget.DOScale(1f, pulseDuration * 0.5f).SetEase(Ease.InOutSine));

            for (int pulse = 1; pulse < pulsesPerBurst; pulse++)
            {
                _comboAnimation
                    .Append(pulseTarget.DOScale(1f + scaleStep, pulseDuration * 0.5f).SetEase(Ease.InOutSine))
                    .Append(pulseTarget.DOScale(1f, pulseDuration * 0.5f).SetEase(Ease.InOutSine));
            }

            _comboAnimation.SetLoops(-1, LoopType.Restart);
        }

        private void StopComboAnimation()
        {
            _comboAnimation?.Kill();
            _comboAnimation = null;
        }

        private void OnDisable()
        {
            booster.DOKill();
            EffectTarget?.DOKill();
            _pulseTween?.Kill();
            _pulseTween = null;
            StopComboAnimation();
            _arrivalTween?.Kill();
            _arrivalTween = null;
        }
    }
}
