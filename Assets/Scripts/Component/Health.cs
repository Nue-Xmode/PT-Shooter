using Godot;
using PTShooter.Resources.Scripts.Interfaces;

namespace PTShooter.Assets.Scripts.Component
{
	public partial class Health : Node
	{
		[Export] private Node2D _owner;
		[Export] private int _maxHp = 100;
		private int _currentHp;

		public override void _Ready()
		{
			_currentHp = _maxHp;
		}

		public override void _Process(double delta)
		{
			if (_currentHp <= 0 && _owner is IHealth health)
				health.HealthLessThanZero();
				
		}

		/// <summary>
		/// 受到伤害时调用
		/// </summary>
		/// <param name="damage"></param>
		public void GetDamage(int damage)
		{
			_currentHp = _currentHp - damage > 0 ? _currentHp - damage : 0;
		}

		/// <summary>
		/// 手动治疗时调用
		/// </summary>
		/// <param name="heal"></param>
		public void GetHeal(int heal)
		{
			_currentHp = _currentHp + heal < _maxHp ? _currentHp + heal : _maxHp;
		}
	}
}
