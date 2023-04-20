using UnityEngine.Events;
using DG.Tweening;

namespace UnityEngine.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class CanvasGroupRepresent : MonoBehaviour
    {
        [SerializeField] private AnimationTransition _showTransition = new AnimationTransition(0.5f);
        [SerializeField] private Vector2 _showOffset = new Vector2(0, -64);
        public UnityEvent BeforeShow;
        [Space]
        [SerializeField] private AnimationTransition _hideTransition = new AnimationTransition(0.5f);
        [SerializeField] private Vector2 _hideOffset = new Vector2(0, 64);
        public UnityEvent AfterHide;
        private CanvasGroup _group;
        private RectTransform _rect;
        private Vector2 _ancore;
        private Tween _move, _fade;

        private void OnEnable () => Initialize();

        public void Show ()
        {
            if (gameObject.activeSelf)
                return;
            
            gameObject.SetActive(true);
            _group.interactable = false;
            _group.alpha = 0;
            _rect.anchoredPosition = _ancore + _showOffset;

            BeforeShow?.Invoke();
            DoTransition(_showTransition, _ancore, 1, OnShowComplete);
        }

        public void Hide ()
        {
            if (gameObject.activeSelf == false)
                return;
            
            gameObject.SetActive(true);
            _group.interactable = false;
            
            DoTransition(_hideTransition, _ancore + _hideOffset, 0, OnHideComplete);
        }

        private void Initialize ()
        {
            if (_group != null)
                return;
            _group = GetComponent<CanvasGroup>();
            _rect = transform as RectTransform;
            _ancore = _rect.anchoredPosition;
            AfterHide?.Invoke();
        }

        private void OnShowComplete () => _group.interactable = true;

        private void OnHideComplete () => gameObject.SetActive(false);

        private void DoTransition (AnimationTransition transition, Vector2 position, float alpha, TweenCallback onComplete)
        {
            _move?.Kill();
            _fade?.Kill();

            TweenParams tParams = new TweenParams().
                SetEase(transition.curve).
                SetUpdate(UpdateType.Normal, true);
            _move = _rect.DOAnchorPos(position, transition.duration).
                SetAs(tParams).
                SetLink(gameObject);
            _fade = _group.DOFade(alpha, transition.duration).
                SetAs(tParams).
                SetLink(gameObject).
                OnComplete(onComplete);
        }
    }
}