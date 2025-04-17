using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Code
{
    public sealed class CurrencyView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _currencyText;
        [SerializeField] private float _fillRate;
        [SerializeField] private ScaleTweenParams _startScale;
        [SerializeField] private ScaleTweenParams _endScale;
        private Sequence _sequence;
        private long _lastCurrency;

        public void SetupCurrency(long currency)
        {
            Setter(currency);
            _lastCurrency = currency;
        }

        public void UpdateCurrency(long currency)
        {
            AnimateText(_lastCurrency, currency);
            _lastCurrency = currency;
        }

        private void AnimateText(long currentValue, long nextValue)
        {
            _sequence?.Kill();
            var tweenerCore = DOTween.To(() => currentValue, Setter, nextValue, _fillRate);
            _sequence = DOTween
                .Sequence()
                .Append(_currencyText.transform.DOScale(_startScale.Scale, _startScale.RillRate))
                .Append(tweenerCore)
                .Append(_currencyText.transform.DOScale(_endScale.Scale, _endScale.RillRate));
        }

        private void Setter(long value)
        {
            _currencyText.text = value.ToString();
        }

        private void OnDisable()
        {
            _sequence?.Kill();
            _sequence = null;
        }
    }
}
