using Godot;
using PTShooter.Resources;

namespace PTShooter.Assets.Scripts.Player
{
	public enum PlayerState { Idle, Walk, Dash, JumpStart, JumpFall }
	
	/// <summary>
	/// 玩家移动控制器
	/// </summary>
	public partial class PlayerMovement : CharacterBody2D
	{
		[Export] private float _normalSpeed = 100.0f;
		[Export] private float _dashSpeed = 200.0f;
		[Export] private float _jumpForce = 400.0f;
		[Export] private bool _canMoveAir = true;
		private int _lastPlayerFaceDir = 1;
		private float _lastMoveDir = 0f;

		public static PlayerState CurrentState = PlayerState.Idle;

		private AnimatedSprite2D _sprite2D;

		public override void _Ready()
		{
			_sprite2D = GetNode<AnimatedSprite2D>("PlayerSprite");
		}

		public override void _PhysicsProcess(double delta)
		{
			SwitchState();
			
			Flip();
			
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
			
			if ((negativeXDir == 0 && positiveXDir == 0))
			{
				targetHorizontalMovement = Vector2.Zero;
				_lastMoveDir = 0f;
			}
			//移动指令后覆盖//
			else
			{
				if (negativeXDir == 0 || positiveXDir == 0)
				{
					_lastMoveDir = negativeXDir == 0 ? positiveXDir : -negativeXDir;
					
					//dash
					if (Input.IsActionPressed("move_dash"))
					{
						targetHorizontalMovement = new Vector2(_lastMoveDir * _dashSpeed, velocity.Y);
						return targetHorizontalMovement;
					}
					targetHorizontalMovement = new Vector2(_lastMoveDir * _normalSpeed, velocity.Y);
				}
				else
				{
					targetHorizontalMovement = new Vector2(-_lastMoveDir * _normalSpeed, velocity.Y);
					
					//dash
					if (Input.IsActionPressed("move_dash"))
					{
						targetHorizontalMovement = new Vector2(-_lastMoveDir * _dashSpeed, velocity.Y);
						return targetHorizontalMovement;
					}
				}
			}

			return targetHorizontalMovement;
		}

		/// <summary>
		/// 获取某一时刻的重力向量
		/// </summary>
		/// <returns></returns>
		private Vector2 GetGravityVector(float delta)
		{
			return new Vector2(0, Settings.Gravity * delta);
		}

		/// <summary>
		/// 获取某一时刻的跳跃力向量
		/// </summary>
		/// <returns></returns>
		private Vector2 GetJumpVector()
		{
			return new Vector2(_lastMoveDir, - _jumpForce);
		}

		/// <summary>
		/// 玩家翻转
		/// </summary>
		private void Flip()
		{
			if (Velocity.X == 0)
				_sprite2D.FlipH = _lastPlayerFaceDir < 0;
			else if (Mathf.Sign(Velocity.X) > 0)
			{
				_lastPlayerFaceDir = 1;
				_sprite2D.FlipH = _lastPlayerFaceDir < 0;
			}
			else if (Mathf.Sign(Velocity.X) < 0)
			{
				_lastPlayerFaceDir = -1;
				_sprite2D.FlipH = _lastPlayerFaceDir < 0;
			}
		}

		/// <summary>
		/// 状态切换
		/// </summary>
		private void SwitchState()
		{
			if (_lastMoveDir == 0 && IsOnFloor())
				CurrentState = PlayerState.Idle;
			else if (_lastMoveDir != 0 && !Input.IsActionPressed("move_dash")
					 && IsOnFloor())
				CurrentState = PlayerState.Walk;
			else if (_lastMoveDir != 0 && Input.IsActionPressed("move_dash")
					 && IsOnFloor())
				CurrentState = PlayerState.Dash;
			else if (!IsOnFloor())
			{
				if (Velocity.Y <= 0)
					CurrentState = PlayerState.JumpStart;
				else if (Velocity.Y > 0)
					CurrentState = PlayerState.JumpFall;
			}
		}
	}
}
