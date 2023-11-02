using Godot;
using PTShooter.Resources.Scripts.Interfaces;

namespace PTShooter.Assets.Scripts.Component
{
	public partial class HitBox : Area2D
	{
		[Export] private Node2D _owner;
		private int _damage;
		
		public override void _Ready()
		{
			AreaEntered += OnAreaEntered;
		}
		
		private void OnAreaEntered(Area2D other)
		{
			if (_owner is IHitBox hitBox)
				hitBox.GetHit(out _damage);

			if (other is HurtBox hurtBox)
				hurtBox.GetHit(_damage);
		}
	}
}
