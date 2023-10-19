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
			if (PlayerMovement.LastMoveDir != 0)
				Play("walk");
			else
				Play("idle");
		}
	}
}
