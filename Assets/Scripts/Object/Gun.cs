using Godot;
using PTShooter.Resources;

namespace PTShooter.Assets.Scripts.Object
{
	public partial class Gun : CharacterBody2D
	{
		[Export] private PackedScene _bulletScene;
		private Timer _shootInterval;
		private bool _canShoot = true;

		public override void _Ready()
		{
			_shootInterval = GetNode<Timer>("ShootInterval");
			_shootInterval.Timeout += OnTimeOut;
		}

		public override void _PhysicsProcess(double delta)
		{
			FollowPlayer();
			Shoot();
		}

		/// <summary>
		/// 跟随玩家
		/// </summary>
		private void FollowPlayer()
		{
			var playerNode = GetTree().GetFirstNodeInGroup("player") as Node2D;
			if (playerNode != null)
				GlobalPosition = playerNode.GlobalPosition;
		}

		/// <summary>
		/// 触发射击
		/// </summary>
		private void Shoot()
		{
			if (Input.IsActionPressed("act_shoot") && _canShoot)
			{
				_canShoot = false;
				var bulletInstance = _bulletScene.Instantiate() as Bullet;
				if (bulletInstance != null)
				{
					bulletInstance.GlobalPosition = GlobalPosition;
					bulletInstance.Init(GetGlobalMousePosition());
					GetTree().Root.AddChild(bulletInstance);
					_shootInterval.Start();	
				}
			}
		}

		/// <summary>
		/// 计时器
		/// </summary>
		private void OnTimeOut()
		{
			_canShoot = true;
		}
	}
}
