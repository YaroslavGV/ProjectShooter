using System;

public interface IDamageable
{
    public bool IsAlive { get; }

    void ApplyDamage (Damage damage);
}

public interface IDamageReportReceiver
{
    void AcceptDealDamageReport (DamageReport report);
    void AcceptTakeDamageReport (DamageReport report);
}

public struct Damage
{
    public object sorce;
    public float value;
    public bool isCrit;

    public Damage (object sorce, float value, bool isCrit = false)
    {
        this.sorce = sorce;
        this.value = value;
        this.isCrit = isCrit;
    }

    public override string ToString ()
    {
        string[] rows =
        {
            "sorce: " + sorce.ToString(),
            "value: " + value,
            "isCrit: " + isCrit
        };
        return string.Join(Environment.NewLine, rows);
    }

    public static Damage one => new Damage(null, 1);
}

public struct DamageReport
{
    public Damage damage;
    public IDamageable target;
    public float takenValue;

    public DamageReport (Damage damage, IDamageable target, float takenValue)
    {
        this.damage = damage;
        this.target = target;
        this.takenValue = takenValue;
    }

    public override string ToString ()
    {
        string[] rows =
        {
            damage.ToString(),
            "target: " + target.ToString(),
            "takenValue: " + takenValue
        };
        return string.Join(Environment.NewLine, rows);
    }

    public void SendToReceivers ()
    {
        if (damage.sorce != null && damage.sorce is IDamageReportReceiver dReceiver)
            dReceiver.AcceptDealDamageReport(this);
        if (target is IDamageReportReceiver tReceiver)
            tReceiver.AcceptTakeDamageReport(this);
    }
}
