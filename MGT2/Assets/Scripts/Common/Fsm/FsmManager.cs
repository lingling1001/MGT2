
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MFrameWork
{
    public class FsmManager : IInit
    {
        private Dictionary<string, IFsm> _mapStates = new Dictionary<string, IFsm>();
        private Dictionary<string, bool> _mapStatesLoad = new Dictionary<string, bool>();

        private IFsm _currentState;
        public IFsm CurrentState
        {
            get
            {
                return _currentState;
            }
        }

        public virtual int Priority { get { return 100; } }



        public virtual void OnInit()
        {
        }
        public virtual void OnRelease()
        {
            if (CurrentState != null)
            {
                CurrentState.OnLeave();
            }
            foreach (var item in _mapStates)
            {
                item.Value.OnRelease();
            }
            _currentState = null;
            _mapStates.Clear();
            _mapStatesLoad.Clear();
        }
        /// <summary>
        /// 有限状态机状态进入时调用。
        /// </summary>
        public void ChangeState(string fsmName)
        {
            IFsm next = GetState(fsmName);
            if (next == null)
            {
                new GameFrameworkException(fsmName + " Is Null ");
                return;
            }
            if (CurrentState != null)
            {
                if (!CurrentState.CanChange(fsmName))
                {
                    return;
                }
                CurrentState.OnLeave();
            }
            if (!_mapStatesLoad.ContainsKey(fsmName))
            {
                _mapStatesLoad.Add(fsmName, true);
                next.OnLoad();
            }
            _currentState = next;
            next.OnEnter();
        }


        /// <summary>
        /// 获取状态
        /// </summary>
        public IFsm GetState(string strName)
        {
            if (_mapStates.ContainsKey(strName))
            {
                return _mapStates[strName];
            }
            return null;
        }
        /// <summary>
        /// 添加状态机。
        /// </summary>
        public bool AddFsm(IFsm fsm)
        {
            if (fsm == null)
            {
                return false;
            }
            if (_mapStates.ContainsKey(fsm.Name))
            {
                return false;
            }
            fsm.OnInit();
            _mapStates.Add(fsm.Name, fsm);
            return true;
        }
    }

}
