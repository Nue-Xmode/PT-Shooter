using Godot;

namespace PTShooter.Assets.Scripts.Ui
{
	public partial class HealthBarUi : ProgressBar
	{
		[Export] private Timer _timer;

		public override void _Ready()
		{
			Value = 0;
			Visible = false;
		}

		public override void _Process(double delta)
		{
			if (_timer != null && _timer.TimeLeft <= 0)
				Visible = false;
		}
		
		/// <summary>
		/// 显示生命值UI
		/// </summary>
		/// <param name="currentHp"></param>
		/// <param name="maxHp"></param>
		public void Show(float currentHp, float maxHp)
		{
			_timer.Start();
			Value = currentHp / maxHp;
			Visible = true;
		}
	}
}
