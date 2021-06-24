namespace MFrameWork
{
    using MFrameWork;
    using System.Collections.Generic;

    public class RegisterInterfaceManager
    {
        private static List<IInit> listInit = new List<IInit>();
        private static List<IUpdate> listUpdate = new List<IUpdate>();
        private static List<ILateUpdate> listLaterUpdate = new List<ILateUpdate>();
        private static List<IFixedUpdate> listFixedUpdate = new List<IFixedUpdate>();


        #region  Update
        public static void RegisteUpdate(IUpdate single)
        {
            if (single == null)
            {
                return;
            }
            if (!listUpdate.Contains(single))
            {
                listUpdate.Add(single);
            }
        }
        public static void UnRegisteUpdate(IUpdate single)
        {
            if (single == null)
            {
                return;
            }
            if (listUpdate.Contains(single))
            {
                listUpdate.Remove(single);
            }
        }
        /// <summary>
        /// 刷新
        /// </summary>
        /// <param name="elapseSeconds">间隔</param>
        /// <param name="realElapseSeconds">真实时间</param>
        public static void Update(float elapseSeconds, float realElapseSeconds)
        {
            for (int i = 0; i < listUpdate.Count; i++)
            {
                IUpdate single = listUpdate[i];
                if (single != null)
                {
                    //UnityEngine.Profiling.Profiler.BeginSample(single.GetHashCode()+ "update");
                    //long time = TimeManager.GetLocalTime();
                    single.On_Update(elapseSeconds, realElapseSeconds);
                    //long time2 = TimeManager.GetLocalTime();
                    //if (time2 - time > 3)
                    //{
                    //    UnityEngine.Debug.LogErrorFormat("{0} {1}", single.GetType().ToString(), time2 - time);
                    //}
                    //UnityEngine.Profiling.Profiler.EndSample();
                }
            }
        }
        #endregion



        #region LateUpdate

        public static void RegisteILateUpdate(ILateUpdate single)
        {
            if (single == null)
            {
                return;
            }
            if (!listLaterUpdate.Contains(single))
            {
                listLaterUpdate.Add(single);
            }
        }

        public static void UnRegisteILateUpdate(ILateUpdate single)
        {
            if (single == null)
            {
                return;
            }
            if (listLaterUpdate.Contains(single))
            {
                listLaterUpdate.Remove(single);
            }
        }
        public static void LateUpdate(float elapseSeconds, float realElapseSeconds)
        {
            for (int i = 0; i < listLaterUpdate.Count; i++)
            {
                ILateUpdate single = listLaterUpdate[i];
                if (single != null)
                {
                    UnityEngine.Profiling.Profiler.BeginSample(single.GetType() + "LateUpdate");
                    single.On_LateUpdate(elapseSeconds, realElapseSeconds);
                    UnityEngine.Profiling.Profiler.EndSample();
                }
            }
        }

        #endregion



        #region  FixedUpdate

        public static void RegisteIFixedUpdate(IFixedUpdate single)
        {
            if (single == null)
            {
                return;
            }
            if (!listFixedUpdate.Contains(single))
            {
                listFixedUpdate.Add(single);
            }
        }

        public static void UnRegisteIFixedUpdate(IFixedUpdate single)
        {
            if (single == null)
            {
                return;
            }
            if (listFixedUpdate.Contains(single))
            {
                listFixedUpdate.Remove(single);
            }
        }

        public static void FixUpdate()
        {
            for (int i = 0; i < listFixedUpdate.Count; i++)
            {
                IFixedUpdate single = listFixedUpdate[i];
                if (single != null)
                {
                    UnityEngine.Profiling.Profiler.BeginSample(single.GetType() + "FixUpdate");
                    single.OnFixedUpdate(UnityEngine.Time.deltaTime);
                    UnityEngine.Profiling.Profiler.EndSample();
                }
            }
        }



        #endregion



        #region  Init
        public static void RegisteIInit(IInit single)
        {
            if (single == null)
            {
                return;
            }
            if (!listInit.Contains(single))
            {
                listInit.Add(single);
                single.OnInit();
            }
        }

        public static void UnRegisteIInit(IInit single)
        {
            if (listInit.Contains(single))
            {
                single.OnRelease();
                listInit.Remove(single);
            }
        }

        /// <summary>
        /// 直接清空了，不取消注册了
        /// </summary>
        public static void OnRelease()
        {
            listLaterUpdate.Clear();
            listUpdate.Clear();
            listFixedUpdate.Clear();
            listInit.Clear();
        }

        #endregion


        //public static void Destroy()
        //{
        //    for (int i = 0; i < _singletonList.Count; i++)
        //    {
        //        ISingleton single = _singletonList[i];
        //        if (single != null)
        //        {
        //            single.On_Destroy();
        //        }
        //    }
        //    _singletonList.Clear();
        //}


        //public static void Reconnect()
        //{
        //    for (int i = 0; i < _singletonList.Count; i++)
        //    {
        //        BaseSingleton single = _singletonList[i];
        //        if (single != null)
        //        {
        //            single.On_Reconnect();
        //        }
        //    }
        //}

        //for (int i = 0; i<listInit.Count; i++)
        //    {
        //        IInit single = listInit[i];
        //        if (single != null)
        //        {
        //            UnityEngine.Profiling.Profiler.BeginSample(single.GetType() + "FixUpdate");
        //            single.OnRelease();
        //            UnityEngine.Profiling.Profiler.EndSample();
        //        }
        //    }
    }
}