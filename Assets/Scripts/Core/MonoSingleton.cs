using UnityEngine;

namespace Core
{
    public class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
    {
        protected bool Initialized;
        private static volatile T _instance;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType(typeof(T)) as T;

                    if (!_instance.Initialized)
                    {
                        _instance.Initialize();
                        _instance.Initialized = true;
                    }
                }
                return _instance;
            }
        }

        protected virtual void Initialize()
        { }


    }
}