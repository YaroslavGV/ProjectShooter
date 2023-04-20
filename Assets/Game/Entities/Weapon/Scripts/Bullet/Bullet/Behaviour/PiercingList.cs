using System;
using System.Collections.Generic;
using UnityEngine;

namespace Weapon
{
    public class PiercingList
    {
        private List<Collider> _hittedPerRun;

        public void ResetHitList () => _hittedPerRun = new List<Collider>();
        public bool IsAlreadyHit (Collider target) => _hittedPerRun.Contains(target);
        public void AddHitTarget (Collider target) => _hittedPerRun.Add(target);
    }
}