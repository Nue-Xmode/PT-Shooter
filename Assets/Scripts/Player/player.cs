using Godot;
using System;

public partial class player : CharacterBody2D
{
   [Export] private float _normalSpeed = 80;
   [Export] private float _jumpForce = 100;
   private Vector2 _moveDir = Vector2.Zero;

   private float _gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle(); //将Variant转换为float
   public override void _Ready()
   {
	  
   }

   public override void _PhysicsProcess(double delta)
   {
	   Vector2 temp = Vector2.Zero;
	   
	   //受重力影响下落
	   if (!IsOnFloor())
		   temp = GetGravityVector(Velocity, (float)delta);

	   Velocity = GetMoveVector(Velocity) + temp;
	   
	   MoveAndSlide();
   }

   /// <summary>
   /// 获取玩家移动向量
   /// </summary>
   /// <param name="velocity">原节点移动向量</param>
   /// <returns></returns>
   private Vector2 GetMoveVector(Vector2 velocity)
   {
	   _moveDir = Input.GetVector( "move_left", "move_right",
		   "move_up", "move_down").Normalized();
	   return _moveDir * _normalSpeed;
   }

   /// <summary>
   /// 获取某一时刻的重力向量
   /// </summary>
   /// <param name="velocity">节点原向量</param>
   /// <param name="delta">变化率</param>
   /// <returns></returns>
   private Vector2 GetGravityVector(Vector2 velocity, float delta)
   {
	   return new Vector2(velocity.X, velocity.Y += _gravity * delta);
   }
   
}
