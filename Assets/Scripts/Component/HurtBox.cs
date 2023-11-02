using Godot;
using PTShooter.Resources.Scripts.Interfaces;

namespace PTShooter.Assets.Scripts.Component
{
    public partial class HurtBox : Area2D
    {
        [Export] private Node2D _owner;
        private int _currentHitDamage;

        public void GetHit(int damage)
        {
            _currentHitDamage = damage;
            
            if (_owner is IHurtBox hurtBox)
                hurtBox.GetHurt(_currentHitDamage);
        }
    }
}