using Godot;

namespace PTShooter.Assets.Scripts.Object
{
	public partial class Bullet : RigidBody2D
	{
		private VisibleOnScreenNotifier2D _notifier;

		private Vector2 _targetPosition;
		private Vector2 _targetDirection;
		private bool _canMove;
		[Export] private float _speed = 50.0f;

		public override void _Ready()
		{
			_notifier = GetNode<VisibleOnScreenNotifier2D>("VisibleOnScreenNotifier2D");
			_notifier.VisibilityChanged += OnBulletVisibilityChanged;
		}

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
				GlobalPosition += _targetDirection * _speed;
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
		
		private void OnBulletVisibilityChanged()
		{
			QueueFree();
		}
	}
}
