using UnityEngine;
using DG.Tweening;

namespace Unit.Skin
{
    public class MoveAction : MonoBehaviour
    {
        [SerializeField] private Vector3 _offset = Vector3.back;
        [SerializeField] private float _duration = .5f;
        [SerializeField] private AnimationCurve _curve = AnimationCurve.Linear(0, 0, 1, 1);
        private Vector3 _ancore;
        private Tween _process;

        private void Awake ()
        {
            _ancore = transform.localPosition;
        }

        public void Move ()
        {
            transform.localPosition = _ancore;

            _process?.Kill();
            _process = null;

            _process = transform.DOLocalMove(_ancore + _offset, _duration).
                SetEase(_curve).
                SetLink(gameObject);
        }
    }
}