using System;
using System.Collections.Generic;

namespace PTShooter.Resources
{
    public class ObjectPool<T> where T : class, new()
    {
        private Stack<T> _objectPool;

        private Action<T> _resetAction;
        private Action<T> _firstInitAction;
        

        public ObjectPool(int initSize,
            Action<T> _resetAction = null, Action<T> _firstInitAction = null)
        {
            _objectPool = new Stack<T>(initSize);
            this._resetAction = _resetAction;
            this._firstInitAction = _firstInitAction;
        }

        /// <summary>
        /// 从池中取出一个对象
        /// </summary>
        /// <returns></returns>
        public T Get()
        {
            if (_objectPool.Count > 0)
            {
                T t = _objectPool.Pop();
                
                if (_resetAction != null)
                    _resetAction.Invoke(t);

                return t;
            }
            else
            {
                T t = new T();
                
                if (_firstInitAction != null)
                    _firstInitAction.Invoke(t);

                return t;
            }
        }

        /// <summary>
        /// 将一个对象存入池中
        /// </summary>
        /// <param name="t"></param>
        public void Store(T t)
        {
            _objectPool.Push(t);
        }
    }
}