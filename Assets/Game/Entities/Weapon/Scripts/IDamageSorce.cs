namespace Weapon
{
    public interface IDamageSorce
    {
        bool CanAttack (IDamageable target);
        void DoDamage (IDamageable target, IDamageMediator mediator);
    }
}