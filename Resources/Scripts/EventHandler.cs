using System;
using PTShooter.Assets.Scripts.Object;

namespace PTShooter.Resources.Scripts
{
    public class EventHandler
    {
        public static event Action<Bullet> BulletCanFree;

        public static void BulletCanFreeEvent(Bullet bullet)
        {
            BulletCanFree?.Invoke(bullet);
        }
    }
}