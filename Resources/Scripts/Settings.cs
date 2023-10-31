using Godot;

namespace PTShooter.Resources
{
	public static class Settings
	{
		public static float Gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle(); //将Variant转换为float
	}
}
