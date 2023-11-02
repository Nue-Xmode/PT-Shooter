namespace PTShooter.Resources.Scripts.Interfaces
{
    public interface IHealth
    {
        public virtual void HealthChanged() { }
        public void HealthLessThanZero();
    }
}