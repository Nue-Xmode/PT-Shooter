using System;

namespace PTShooter.Resources.Scripts
{
    public class SingleTonProperty<T> where T : class
    {
        private static T _mInstance;
        
        private static readonly object mLock = new object();

        public static T Instance
        {
            get
            {
                lock (mLock)
                {
                    if (_mInstance == null)
                    {
                        _mInstance = (T)Activator.CreateInstance(typeof(T));
                    }
                }

                return _mInstance;
            }
        }

        public static void Dispose()
        {
            _mInstance = null;
        }
    }
}

