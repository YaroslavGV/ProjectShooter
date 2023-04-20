using System;
using UnityEngine.Events;

namespace UnityEngine.UI
{
    public class ChoiseDot : MonoBehaviour
    {
        public UnityEvent OnSelect;
        public UnityEvent OnDiselect;
        public Action<int> OnClick;
        private int _index;
        private bool _isSelect;

        public bool IsSelect
        {
            get => _isSelect;
            set
            {
                if (_isSelect != value)
                {
                    _isSelect = value;
                    if (_isSelect)
                        OnSelect?.Invoke();
                    else
                        OnDiselect?.Invoke();
                }
            }
        }

        public int SetIndex (int index) => _index = index;
        
        public void Click ()
        {
            OnClick?.Invoke(_index);
        }
    }
}
