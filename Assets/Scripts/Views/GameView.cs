using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using ViewModels;

namespace Views
{
    public class GameView : ViewBase<IGameViewModel>
    {
        [SerializeField] private Camera mainCamera;
        [SerializeField] private LayerMask itemLayer;
        [SerializeField] private ItemView prefab;
        [SerializeField] private Transform[] anchors;

        private readonly List<ItemView> items = new();

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                var ray = mainCamera.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out var hit, 10, itemLayer))
                {
                    var itemView = hit.collider.GetComponent<ItemView>();

                    if (itemView != null)
                        itemView.SelectItem();
                }
            }
        }

        protected override void OnInit()
        {
            base.OnInit();
            Assert.IsTrue(ViewModel.Items.Count == anchors.Length, "Anchors count not equals Items count");
            ViewModel.SelectionCompleted += ViewModelOnSelectionCompleted;
            
            for (var i = 0; i < ViewModel.Items.Count; i++)
            {
                var item = Instantiate(prefab, anchors[i]);
                item.Init(ViewModel.Items[i]);
                items.Add(item);
            }
        }

        protected override void OnRelease()
        {
            base.OnRelease();
            ViewModel.SelectionCompleted -= ViewModelOnSelectionCompleted;
            
            for (var i = 0; i < items.Count; i++)
            {
                var item = items[i];
                if (item != null)
                {
                    
                    item.Release();
                }
                Destroy(item.gameObject);
            }
            
            items.Clear();
        }

        private void ViewModelOnSelectionCompleted(bool isCorrect)
        {
            if (isCorrect)
                ViewModel.StartRound();
        }
    }
}
