using System.Collections;
using TMPro;
using UnityEngine;
using ViewModels;

namespace Views
{
    public class UIView : ViewBase<IUIViewModel>
    {
        [SerializeField] private TMP_Text label;
        [SerializeField] private FullScreenPassRendererFeature selectRendererFeature;
        [SerializeField] private Material selectMaterial;
        [SerializeField] private float intensityEffect;

        private readonly int intensity = Shader.PropertyToID("_Intensity");
        private Coroutine effectCoroutine;

        private void Start()
        {
            selectRendererFeature.SetActive(false);
        }

        protected override void OnInit()
        {
            base.OnInit();
            ViewModel.SelectionCompleted += ViewModelOnSelectionCompleted;
            ViewModel.ColorLabel.ValueChanged += ColorLabelOnValueChanged;
            
            ColorLabelOnValueChanged(ViewModel.ColorLabel);
        }

        protected override void OnRelease()
        {
            base.OnRelease();
            ViewModel.SelectionCompleted -= ViewModelOnSelectionCompleted;
            ViewModel.ColorLabel.ValueChanged -= ColorLabelOnValueChanged;
        }

        private void ColorLabelOnValueChanged(string text)
        {
            label.text = text;
        }

        private void ViewModelOnSelectionCompleted(bool isCorrect)
        {
            if (effectCoroutine != null)
                StopCoroutine(effectCoroutine);

            selectRendererFeature.SetActive(false);

            if (!isCorrect)
                effectCoroutine = StartCoroutine(StartSelectionEffect());
        }

        private IEnumerator StartSelectionEffect()
        {
            selectRendererFeature.SetActive(true);
            selectMaterial.SetFloat(intensity, intensityEffect);
            var effect = 1f;

            while (effect > 0f)
            {
                selectMaterial.SetFloat(intensity, effect * intensityEffect);
                yield return null;
                effect -= Time.deltaTime;
            }

            selectRendererFeature.SetActive(false);
        }
    }
}