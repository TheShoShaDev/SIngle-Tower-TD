public interface IAttackable
{
	void Attack();
}

public interface IDamagable
{
	void GetDamage(float value);
}

public interface IHealable
{
	void GetHealed(float value);
}