using Godot;
using PTShooter.Resources.Scripts.Interfaces;

namespace PTShooter.Assets.Scripts.Component
{
    public partial class HurtBox : Area2D
    {
        [Export] private Node2D _owner;
		
        public override void _Ready()
        {
            AreaEntered += OnAreaEntered;
        }
		
        private void OnAreaEntered(Area2D other)
        {
            if (_owner is IHurtBox hurtBox)
                hurtBox.GetHurt();
        }
    }
}