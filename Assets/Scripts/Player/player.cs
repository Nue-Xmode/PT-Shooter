using Godot;

namespace PTShooter.Assets.Scripts.Player
{
	public partial class Player : CharacterBody2D
	{
		[Export] private float _normalSpeed = 80.0f;
		[Export] private float _jumpForce = 100.0f;
		[Export] private bool _canMoveAir = false;
		private float _lastMoveDir = 0f;

		private float _gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle(); //将Variant转换为float

		public override void _PhysicsProcess(double delta)
		{
			//重力
			if (!IsOnFloor())
			{
				Velocity += GetGravityVector((float)delta);

				//处理空中水平移动
				if (_canMoveAir &&
					(Input.IsActionPressed("move_left") || Input.IsActionPressed("move_right")))
					Velocity = GetHorizontalMoveVector(Velocity);
			}
			else
				Velocity = GetHorizontalMoveVector(Velocity);
			
			//跳跃
			if (IsOnFloor() && Input.IsActionPressed("move_jump"))
				Velocity += GetJumpVector();
			
			MoveAndSlide();
		}

		/// <summary>
		/// 获取某一时刻玩家的水平移动向量
		/// </summary>
		/// <param name="velocity">原节点移动向量</param>
		/// <returns></returns>
		private Vector2 GetHorizontalMoveVector(Vector2 velocity)
		{
			Vector2 targetHorizontalMovement = Vector2.Zero;
			float negativeXDir = Input.GetActionStrength("move_left");
			float positiveXDir = Input.GetActionStrength("move_right");

			//移动指令后覆盖//
			if ((negativeXDir == 0 && positiveXDir == 0))
			{
				targetHorizontalMovement = Vector2.Zero;
				_lastMoveDir = 0f;
			}
			else
			{
				if (negativeXDir == 0 || positiveXDir == 0)
				{
					targetHorizontalMovement = new Vector2((negativeXDir == 0 ? positiveXDir : -negativeXDir) * _normalSpeed, velocity.Y);
					_lastMoveDir = negativeXDir == 0 ? positiveXDir : -negativeXDir;
				}
				else
					targetHorizontalMovement = new Vector2(-_lastMoveDir * _normalSpeed, velocity.Y);
			}

			return targetHorizontalMovement;
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
		private Vector2 GetJumpVector()
		{
			return new Vector2(_lastMoveDir, - _jumpForce);
		}
	}
}
