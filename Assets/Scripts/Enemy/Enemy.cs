using Godot;
using PTShooter.Assets.Scripts.Component;
using PTShooter.Resources;
using PTShooter.Resources.Scripts.Interfaces;

namespace PTShooter.Assets.Scripts.Enemy
{
	public partial class Enemy : CharacterBody2D, IHurtBox, IHealth
	{
		[Export] private float _normalSpeed = 60.0f;
		[Export] private bool _canFly = false;

		[Export] private Health _health;

		public override void _PhysicsProcess(double delta)
		{
			if (IsOnFloor())
				Velocity = GetMoveDir(Velocity) * _normalSpeed;
			//重力
			else
			{
				var velocity = Velocity;
				velocity.Y += Settings.Gravity * (float)delta;
				Velocity = velocity;	
			}

			MoveAndSlide();
		}

		/// <summary>
		/// 获取移动方向
		/// </summary>
		/// <returns></returns>
		private Vector2 GetMoveDir(Vector2 velocity)
		{
			Node2D playerNode = GetTree().GetFirstNodeInGroup("player") as Node2D;

			if (playerNode != null)
			{
				var playerPos = playerNode.GlobalPosition;
				
				if (!_canFly)
					return new Vector2(playerPos.X - GlobalPosition.X, velocity.Y).Normalized();
				else
					return new Vector2(playerPos.X - GlobalPosition.X, playerPos.Y - GlobalPosition.Y).Normalized();	
			}
			
			return Vector2.Zero;
		}

		#region 接口方法
		
			public void Hurt(int damage)
			{
				_health?.GetDamage(damage);
			}

			public void HealthLessThanZero()
			{
				QueueFree();
			}
		
		#endregion
	}
}
