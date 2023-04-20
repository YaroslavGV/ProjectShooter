using UnityEngine;

namespace PointGenerator
{
    public abstract class PointLocation : MonoBehaviour
    {
        protected static Color _gizmosColor = new Color(0.5f, 1, 0.5f, 1);
        protected static bool _drawGizmos = true;

        private void OnDrawGizmos ()
        {
            if (_drawGizmos == false)
                return;
            Gizmos.color = _gizmosColor;
            DrawGizmos();
        }

        public abstract Vector3 GetPoint ();

        protected virtual void DrawGizmos () { }

        [ContextMenu("Gizmos On")]
        private void GizmosOn () => _drawGizmos = true;

        [ContextMenu("Gizmos Off")]
        private void GizmosOff () => _drawGizmos = false;
    }
}