using UnityEngine;

namespace Weapon
{
    public abstract class CastBullet : Bullet
    {
        [Space]
        [SerializeField] private LayerMask _mask;
        private LifeDistance _life;

        protected LifeDistance Life => _life;
        protected LayerMask Mask => _mask;
        protected Ray ForwardRay => new Ray(transform.position, transform.forward);

        private void Awake ()
        {
            _life = new LifeDistance();
            _life.LifeOver += Exhaust;
        }

        private void OnDestroy ()
        {
            _life.LifeOver -= Exhaust;
        }

        private void Update ()
        {
            if (IsRunning)
            {
                float distance = Settings.speed*Time.deltaTime;
                distance = Mathf.Min(distance, _life.Remainder);
                HandleRay(distance);
            }
        }

        public override void Fire (ShotData data, IDamageSorce sorce = null, IDamageMediator mediator = null)
        {
            base.Fire(data, sorce, mediator);
            _life.SetDistance(Settings.maxDistance);
            _life.Reset();
        }

        private void HandleRay (float deltaDistance)
        {
            Vector3 point = transform.position;
            RaycastHit[] hitTargets = GetHits(deltaDistance);
            foreach (RaycastHit hitTarget in hitTargets)
            {
                transform.position = hitTarget.point;
                Collider collider = hitTarget.collider;
                IDamageable damagebleTarget = GetDamageTarget(collider);
                if (damagebleTarget != null)
                {
                    bool isFinished = HandleTargetCollision(damagebleTarget, collider, hitTarget);
                    if (isFinished)
                    {
                        Exhaust();
                        return;
                    }
                }
                else
                {
                    HandleEnvironmentCollision(hitTarget.collider, hitTarget, out bool isRicocheted);
                    if (isRicocheted)
                    {
                        float travaledDistance = GetTravaledDistance(point, hitTarget);
                        float remainderDistance = deltaDistance-travaledDistance;
                        _life.SpendDistance(travaledDistance);
                        HandleRay(remainderDistance);
                    }
                    return;
                }
            }
            transform.position += transform.forward*deltaDistance;
            _life.SpendDistance(deltaDistance);
        }

        protected abstract RaycastHit[] GetHits (float deltaDistance);
        protected abstract float GetTravaledDistance (Vector3 point, RaycastHit hit);
    }
}