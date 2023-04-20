using UnityEngine;

namespace Unit.Skin
{
    public abstract class InputAgent : MonoBehaviour
    {
        public abstract void UpdateValues (InputValues value);
    }
}