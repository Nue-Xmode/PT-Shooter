using Godot;
using PTShooter.Resources.Scripts;

namespace PTShooter.Assets.Scripts.Object
{
	public partial class Bullet : RigidBody2D, IHitBox, IVisibleScreenNotifier
	{
		private Vector2 _targetPosition;
		private Vector2 _targetDirection;
		private bool _canMove;
		[Export] private float _speed = 50.0f;

		private bool _canFree;

		public override void _PhysicsProcess(double delta)
		{
			CheckState();
			MoveToward();
		}

		#region 普通函数方法
		
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

			/// <summary>
			/// 状态检测
			/// </summary>
			private void CheckState()
			{
				if (_canFree)
					QueueFree();
			}
			
		#endregion

		#region 接口实现
		
			public void TriggerHitBox()
			{
				_canFree = true;
			}
			public void TriggerVisibleScreenNotifier()
			{
				_canFree = true;
			}
		
		#endregion

	}
}
