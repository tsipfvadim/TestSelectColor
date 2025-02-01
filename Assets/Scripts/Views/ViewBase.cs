using UnityEngine;

namespace Views
{
    public abstract class ViewBase<TViewModel> : MonoBehaviour
    {
        protected TViewModel ViewModel { get; private set; }

        private void OnDestroy()
        {
            Release();
        }

        public void Init(TViewModel viewModel)
        {
            ViewModel = viewModel;
            OnInit();
        }
        
        public void Release()
        {
            if (ViewModel != null)
            {
                OnRelease();
                ViewModel = default;
            }
        }
        
        protected virtual void OnRelease()
        {
        }

        protected virtual void OnInit()
        {
        }
    }
}
