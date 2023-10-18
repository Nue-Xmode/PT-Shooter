using Godot;

namespace PTShooter.Assets.Scripts.Player
{
	public partial class Player : CharacterBody2D
	{
		[Export] private float _normalSpeed = 80.0f;
		[Export] private float _jumpForce = 100.0f;
		private float _lastMoveDir = 0f;

		private float _gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle(); //将Variant转换为float

		public override void _Ready()
		{

		}

		public override void _PhysicsProcess(double delta)
		{
			//重力
			if (!IsOnFloor())
				Velocity += GetGravityVector((float)delta);
			else
				Velocity = GetMoveVector(Velocity);
			
			//跳跃
			if (IsOnFloor() && Input.IsActionPressed("move_jump"))
				Velocity += GetJumpVector(Velocity);
			
			MoveAndSlide();
		}

		/// <summary>
		/// 获取某一时刻玩家的移动向量
		/// </summary>
		/// <param name="velocity">原节点移动向量</param>
		/// <returns></returns>
		private Vector2 GetMoveVector(Vector2 velocity)
		{
			Vector2 targetMoveDir = Vector2.Zero;
			float negativeXDir = Input.GetActionStrength("move_left");
			float positiveXDir = Input.GetActionStrength("move_right");

			//移动指令后覆盖//
			if ((negativeXDir == 0 && positiveXDir == 0))
			{
				targetMoveDir = Vector2.Zero;
				_lastMoveDir = 0f;
			}
			else
			{
				if (negativeXDir == 0 || positiveXDir == 0)
				{
					targetMoveDir = new Vector2(negativeXDir == 0 ? positiveXDir : -negativeXDir, velocity.Y);
					_lastMoveDir = negativeXDir == 0 ? positiveXDir : -negativeXDir;
				}
				else
					targetMoveDir = new Vector2(-_lastMoveDir, velocity.Y);
			}

			return targetMoveDir * _normalSpeed;
		}

		/// <summary>
		/// 获取某一时刻的重力向量
		/// </summary>
		/// <returns></returns>
		private Vector2 GetGravityVector(float delta)
		{
			return new Vector2(0, _gravity * delta);
		}

		/// <summary>
		/// 获取某一时刻的跳跃力向量
		/// </summary>
		/// <param name="velocity">节点原移动向量</param>
		/// <returns></returns>
		private Vector2 GetJumpVector(Vector2 velocity)
		{
			return new Vector2(_lastMoveDir, - _jumpForce);
		}
	}
}
