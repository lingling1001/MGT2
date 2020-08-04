using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MFrameWork
{
    public class Singleton<T> : ISingleton<T> where T : class, ISingleton<T>, new()
    {
        //单例模式
        private static T _instance;
        //线程安全使用
        private static readonly object _lockObj = new object();

        public static T Instance
        {
            get
            {
                if (null == _instance)
                {
                    lock (_lockObj)
                    {
                        if (null == _instance)
                        {
                            _instance = new T();
                        }
                    }
                }
                return _instance;
            }
        }

        public T GetInstance()
        {
            return Instance;
        }
        
    }
}


