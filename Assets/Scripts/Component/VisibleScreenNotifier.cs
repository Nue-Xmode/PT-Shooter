using Godot;
using PTShooter.Resources.Scripts;

namespace PTShooter.Assets.Scripts.Component
{
	public partial class VisibleScreenNotifier : VisibleOnScreenNotifier2D
	{
		[Export] private Node2D _owner;
		
		public override void _Ready()
		{
			VisibilityChanged += OnVisibilityChanged;
			
			
		}

		private void OnVisibilityChanged()
		{
			if (_owner is IVisibleScreenNotifier visibleScreenNotifier)
				visibleScreenNotifier.TriggerVisibleScreenNotifier();
		}
	}
}
