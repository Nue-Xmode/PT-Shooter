using Godot;

namespace PTShooter.Assets.Scripts.Object
{
	public partial class Bullet : RigidBody2D
	{
		private Vector2 _targetPosition;
		private Vector2 _targetDirection;
		private bool _canMove;
		[Export] private float _speed = 50.0f;
		
		public override void _PhysicsProcess(double delta)
		{
			MoveToward();
		}

		/// <summary>
		/// 实例化后的飞行轨迹
		/// </summary>
		private void MoveToward()
		{
			if (_canMove)
			{
				LinearVelocity = _targetDirection * _speed;
			}
		}

		/// <summary>
		/// 初始化相关数据
		/// </summary>
		/// <param name="targetPosition"></param>
		public void Init(Vector2 targetPosition)
		{
			_targetPosition = targetPosition;
			_targetDirection = (targetPosition - GlobalPosition).Normalized();
			_canMove = true;
		}
	}
}
