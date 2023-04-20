using System;

namespace UnityEngine.UI
{
    [Serializable]
    public struct AnimationTransition
    {
        public float duration;
        public AnimationCurve curve;

        public AnimationTransition (float duration) : this()
        {
            this.duration = duration;
            curve = AnimationCurve.Linear(0, 0, 1, 1);
        }
    }
}