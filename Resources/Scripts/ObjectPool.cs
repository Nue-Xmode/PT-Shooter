using System;
using System.Collections.Generic;
using Godot;

namespace PTShooter.Resources.Scripts
{
	public class ObjectPool<T> where T : class, new()
	{
		private Stack<T> _pool;

		private int _maxSize;
		private Action<T> _resetAction;
		private Action<T> _firstInitAction;
		private Action<T> _storeAction;
		private Action<T> _disposeAction;
		

		public ObjectPool(int initSize, int maxSize,
			Action<T> resetAction = null, Action<T> firstInitAction = null,
			Action<T> storeAction = null, Action<T> disposeAction = null)
		{
			_pool = new Stack<T>(initSize);
			_maxSize = maxSize;
			_resetAction = resetAction;
			_firstInitAction = firstInitAction;
			_storeAction = storeAction;
			_disposeAction = disposeAction;
		}

		/// <summary>
		/// 从池中取出一个对象
		/// </summary>
		/// <returns></returns>
		public T Get()
		{
			if (_pool.Count > 0)
			{
				T t = _pool.Pop();
				_resetAction?.Invoke(t);

				return t;
			}
			else
			{
				T t = new T();
				_firstInitAction?.Invoke(t);
			
				return t;
			}
		}

		/// <summary>
		/// 将一个对象存入池中
		/// </summary>
		/// <param name="t"></param>
		public void Store(T t)
		{
			if (_pool.Count <= _maxSize)
			{
				_pool.Push(t);
				_storeAction?.Invoke(t);
			}
			else
				_disposeAction.Invoke(t);
		}
	}
}
