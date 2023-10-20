using Godot;
using System;

namespace PTShooter.Assets.Scripts.Player
{
	/// <summary>
	/// 玩家动画控制器 
	/// </summary>
	public partial class PlayerAnimation : AnimatedSprite2D
	{
		public override void _Process(double delta)
		{
			SwitchAnimation();
		}

		/// <summary>
		/// 动画切换
		/// </summary>
		private void SwitchAnimation()
		{
			if (PlayerMovement.CurrentState == PlayerState.JumpStart)
				Play("jump_start");
			else if (PlayerMovement.CurrentState == PlayerState.JumpFall)
				Play("jump_end");
			else if (PlayerMovement.CurrentState == PlayerState.Dash)
				Play("dash");
			else if (PlayerMovement.CurrentState == PlayerState.Walk)
				Play("walk");
			else if (PlayerMovement.CurrentState == PlayerState.Idle)
				Play("idle");
		}
	}
}
