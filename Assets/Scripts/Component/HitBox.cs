using Godot;
using PTShooter.Resources.Scripts.Interfaces;

namespace PTShooter.Assets.Scripts.Component
{
	public partial class HitBox : Area2D
	{
		[Export] private Node2D _owner;
		[Export] private int _damage;
		
		public override void _Ready()
		{
			AreaEntered += OnAreaEntered;
		}
		
		
		/// <summary>
		/// 攻击触发时调用，触发对应接口行为
		/// </summary>
		/// <param name="other"></param>
		private void OnAreaEntered(Area2D other)
		{
			if (_owner is IHitBox hitBox)
				hitBox.Hit();

			if (other is HurtBox hurtBox)
				hurtBox.GetHurt(_damage);
		}
	}
}
