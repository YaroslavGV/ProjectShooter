using System.Collections.Generic;
using UnityEngine.Events;

namespace UnityEngine.UI
{
    public class ChoiseDots : MonoBehaviour
    {
        public UnityEvent<int> OnSelect;
        [SerializeField] private ChoiseDot _template;
        private List<ChoiseDot> _dots;

        public int Count => _dots != null ? _dots.Count : 0;
        
        private void OnEnable ()
        {
            if (Count == 0)
                SetCount(6);
        }

        public void SetCount (int count)
        {
            if (_dots != null && _dots.Count == count)
                return;
            if (_dots == null)
                _dots = new List<ChoiseDot>();
            ClearDots();
            for (int i = 0; i < count; i++) 
            {
                ChoiseDot dot = Instantiate(_template, transform);
                dot.SetIndex(i);
                dot.OnClick += OnClickDot;
                _dots.Add(dot);
            }
        }

        public void Select (int index)
        {
            for (int i = 0; i < _dots.Count; i++) 
                if (i != index)
                    _dots[i].IsSelect = false;
            _dots[index].IsSelect = true;
        }

        private void OnClickDot (int index)
        {
            OnSelect?.Invoke(index);
        }

        private void ClearDots ()
        {
            foreach (ChoiseDot dot in _dots)
            {
                dot.OnClick -= OnClickDot;
                Destroy(dot.gameObject);
            }
            _dots.Clear();
        }
    }
}