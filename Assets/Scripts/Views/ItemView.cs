using UnityEngine;
using ViewModels;

namespace Views
{
    public class ItemView : ViewBase<IItemViewModel>
    {
        [SerializeField] private MeshRenderer meshRenderer;
        [SerializeField] private Animator animator;
        
        private readonly int select = Animator.StringToHash("Select");
        private readonly int correct = Animator.StringToHash("Correct");
        private readonly int incorrect = Animator.StringToHash("Incorrect");

        public void SelectItem()
        {
            animator.SetTrigger(select);
            ViewModel.OnSelect();
        }

        protected override void OnInit()
        {
            base.OnInit();
            ViewModel.Color.ValueChanged += ColorOnValueChanged;
            ViewModel.SelectionCompleted += ViewModelOnSelectionCompleted;

            ColorOnValueChanged(ViewModel.Color);
        }

        protected override void OnRelease()
        {
            base.OnRelease();
            ViewModel.Color.ValueChanged -= ColorOnValueChanged;
            ViewModel.SelectionCompleted -= ViewModelOnSelectionCompleted;
        }

        private void ColorOnValueChanged(Color color)
        {
            meshRenderer.material.color = ViewModel.Color;
        }
        
        private void ViewModelOnSelectionCompleted(bool isCorrect)
        {
            animator.SetTrigger(isCorrect ? correct : incorrect);
        }
    }
}
