using Godot;
using PTShooter.Resources.Scripts.Interfaces;

namespace PTShooter.Assets.Scripts.Component
{
    public partial class HurtBox : Area2D
    {
        [Export] private Node2D _owner;

        /// <summary>
        /// 受到攻击时调用，触发对应接口行为
        /// </summary>
        /// <param name="damage"></param>
        public void GetHurt(int damage)
        {
            if (_owner is IHurtBox hurtBox)
                hurtBox.Hurt(damage);
        }
    }
}