namespace PTShooter.Resources.Scripts.Interfaces
{
    public interface IHealth
    {
        public virtual int GetDamage(int damage) => damage;
        public virtual int GetHeal(int heal) => heal;
        public void HealthLessThanZero();
    }
}