using Godot;
using PTShooter.Assets.Scripts.Ui;
using PTShooter.Resources.Scripts.Interfaces;

namespace PTShooter.Assets.Scripts.Component
{
	public partial class Health : Node
	{
		[Export] private Node2D _owner;
		[Export] private int _maxHp = 100;
		[Export] private HealthBarUi _healthBarUi;

		private int _currentHp;

		public override void _Ready() => _currentHp = _maxHp;

		public override void _Process(double delta)
		{
			//生命值归零时
			if (_currentHp <= 0 && _owner is IHealth health)
				health.HealthLessThanZero();
		}

		#region 组件功能

			/// <summary>
			/// 受到伤害时调用
			/// </summary>
			/// <param name="damage"></param>
			public void GetDamage(int damage)
			{
				_currentHp = _currentHp - damage > 0 ? _currentHp - damage : 0;
				UpdateHealthBarUi(_currentHp, _maxHp);
				
				//触发接口
				if (_owner is IHealth health)
					health.GetDamage(damage);
			}

			/// <summary>
			/// 受到治疗时调用
			/// </summary>
			/// <param name="heal"></param>
			public void GetHeal(int heal)
			{
				_currentHp = _currentHp + heal < _maxHp ? _currentHp + heal : _maxHp;
				UpdateHealthBarUi(_currentHp, _maxHp);
				
				//触发接口
				if (_owner is IHealth health)
					health.GetHeal(heal);
			}

		#endregion

		#region UI组件调用
		
			/// <summary>
			/// 显示对应的生命值UI
			/// </summary>
			/// <param name="currentHp">当前生命值</param>
			/// <param name="maxHp">最大生命值</param>
			private void UpdateHealthBarUi(float currentHp, float maxHp)
			{
				if (_healthBarUi != null)
					_healthBarUi.Show(currentHp, maxHp);
			}
		
		#endregion
	}
}
