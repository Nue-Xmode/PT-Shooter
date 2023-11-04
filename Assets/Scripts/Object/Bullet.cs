using System;
using Godot;
using PTShooter.Resources.Scripts.Interfaces;
using EventHandler = PTShooter.Resources.Scripts.EventHandler;

namespace PTShooter.Assets.Scripts.Object
{
	public partial class Bullet : RigidBody2D, IHitBox, IVisibleScreenNotifier
	{
		private Vector2 _targetPosition;
		private Vector2 _targetDirection;
		[Export] private float _speed = 50.0f;
		[Export] private int _damage;

		private bool _canMove;
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
			/// 启动子弹
			/// </summary>
			/// <param name="fromPosition">起始位置</param>
			/// <param name="targetPosition">目标位置</param>
			public void ActiveBullet(Vector2 fromPosition, Vector2 targetPosition)
			{
				Visible = true;
				GlobalPosition = fromPosition;
				_targetPosition = targetPosition;
				_targetDirection = (targetPosition - GlobalPosition).Normalized();
				_canMove = true;
			}

			/// <summary>
			/// 暂停子弹运动
			/// </summary>
			public void DisableBullet()
			{
				_canFree = false;
				_canMove = false;
				Visible = false;
			}

			/// <summary>
			/// 状态检测
			/// </summary>
			private void CheckState()
			{
				if (_canFree)
					EventHandler.BulletCanFreeEvent(this);
			}
			
		#endregion

		#region 接口实现
		
			public void GetHit(out int damage)
			{
				damage = _damage;
				_canFree = true;
			}
			
			public void ScreenExited()
			{
				_canFree = true;
			}
		
		#endregion

	}
}
