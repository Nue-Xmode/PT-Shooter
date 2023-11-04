using Godot;
using PTShooter.Resources.Scripts.Interfaces;

namespace PTShooter.Assets.Scripts.Component
{
    public partial class HurtBox : Area2D
    {
        [Export] private Node2D _owner;

        public void GetHurt(int damage)
        {
            if (_owner is IHurtBox hurtBox)
                hurtBox.Hurt(damage);
        }
    }
}