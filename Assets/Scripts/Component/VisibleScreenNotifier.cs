using Godot;
using PTShooter.Resources.Scripts.Interfaces;

namespace PTShooter.Assets.Scripts.Component
{
	public partial class VisibleScreenNotifier : VisibleOnScreenNotifier2D
	{
		[Export] private Node2D _owner;
		
		public override void _Ready()
		{
			ScreenExited += OnScreenExited;
		}

		private void OnScreenExited()
		{
			if (_owner is IVisibleScreenNotifier visibleScreenNotifier)
				visibleScreenNotifier.ScreenExited();
		}
	}
}
