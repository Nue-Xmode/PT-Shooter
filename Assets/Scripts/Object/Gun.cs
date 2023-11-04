using Godot;
using PTShooter.Resources.Scripts;

namespace PTShooter.Assets.Scripts.Object
{
	public partial class Gun : CharacterBody2D
	{
		[Export] private PackedScene _bulletScene;
		private Timer _shootInterval;
		private bool _canShoot = true;

		[Export] private Node _bulletParent;
		private ObjectPool<Node2D> _bulletPool;
		
		public override void _Ready()
		{
			_shootInterval = GetNode<Timer>("ShootInterval");
			_bulletPool = new ObjectPool<Node2D>(10, 20, ResetBullet, FirstInitBullet, StoreBullet, node => node.QueueFree());

			_shootInterval.Timeout += OnTimeOut;
			EventHandler.BulletCanFree += OnBulletCanFree;
		}

		public override void _PhysicsProcess(double delta)
		{
			Shoot();
		}
		

		/// <summary>
		/// 触发射击
		/// </summary>
		private void Shoot()
		{
			if (Input.IsActionPressed("act_shoot") && _canShoot)
			{
				_canShoot = false;
				var bulletInstance = _bulletPool.Get();
			}
		}

		#region 对象池相关行为
		
			/// <summary>
			/// 从对象池初始化子弹实例时调用
			/// </summary>
			/// <param name="bulletInstance"></param>
			private void FirstInitBullet(Node2D bulletInstance)
			{
				bulletInstance = _bulletScene.Instantiate() as Node2D;
				if (bulletInstance is Bullet bullet)
					bullet.ActiveBullet(GlobalPosition, GetGlobalMousePosition());
				
				_bulletParent.AddChild(bulletInstance);
				_shootInterval.Start();	
			}
			
			/// <summary>
			/// 从对象池取出子弹实例时调用
			/// </summary>
			/// <param name="bulletInstance"></param>
			private void ResetBullet(Node2D bulletInstance)
			{
				if (bulletInstance is Bullet bullet)
					bullet.ActiveBullet(GlobalPosition, GetGlobalMousePosition());
				
				_shootInterval.Start();	
			}

			/// <summary>
			/// 将子弹实例存入对象池时调用
			/// </summary>
			/// <param name="bulletInstance"></param>
			public void StoreBullet(Node2D bulletInstance)
			{
				if (bulletInstance is Bullet bullet)
					bullet.DisableBullet();
			}
		
		#endregion

		#region 事件
		
			private void OnTimeOut()
			{
				_canShoot = true;
			}
			
			private void OnBulletCanFree(Bullet bullet)
			{
				_bulletPool.Store(bullet);
			}

		#endregion
	}
}
