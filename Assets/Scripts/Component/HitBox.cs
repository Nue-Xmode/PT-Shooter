using Godot;
using PTShooter.Resources.Scripts;

namespace PTShooter.Assets.Scripts.Component
{
	public partial class HitBox : Area2D
	{
		[Export] private Node2D _owner;
		
		public override void _Ready()
		{
			AreaEntered += OnAreaEntered;
		}
		
		private void OnAreaEntered(Area2D other)
		{
			GD.Print(other.Owner.Name);

			if (_owner is IHitBox hitBox)
				hitBox.TriggerHitBox();
		}
	}
}
