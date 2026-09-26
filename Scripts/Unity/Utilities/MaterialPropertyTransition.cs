using UnityEngine;
using DG.Tweening;
using System;

namespace CardGame.Unity.Utilities
{
    public class MaterialPropertyTransition
    {
        // References
        private readonly MeshRenderer _renderer;
        private readonly MaterialPropertyBlock _propertyBlock;
        private readonly int _propertyId;
        private readonly float _duration;
        private readonly Ease _easeType;
    
        // Internal Variables
        private Tweener _currentTween;

        public MaterialPropertyTransition(
            MeshRenderer renderer, 
            string propertyName, 
            float duration = 0.3f, 
            Ease easeType = Ease.InOutSine)
        {
            _renderer = renderer;
            _propertyBlock = new MaterialPropertyBlock();
            _propertyId = Shader.PropertyToID(propertyName);
            _duration = duration;
            _easeType = easeType;
        }

        public void SetValue(float value)
        {
            _renderer.GetPropertyBlock(_propertyBlock);
            _propertyBlock.SetFloat(_propertyId, value);
            _renderer.SetPropertyBlock(_propertyBlock);
        }

        public Tweener TransitionToFloat(float targetValue)
        {
            if (_renderer == null)
                return null;

            // Kill Existing Tween
            _currentTween?.Kill();

            _renderer.GetPropertyBlock(_propertyBlock);
            float currentValue = _propertyBlock.GetFloat(_propertyId);

            _currentTween = DOTween.To(
                () => currentValue,
                value =>
                {
                    _propertyBlock.SetFloat(_propertyId, value);
                    _renderer.SetPropertyBlock(_propertyBlock);
                },
                targetValue,
                _duration
            ).SetEase(_easeType);

            return _currentTween;
        }
    }
}