using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace CodeEvents
{

    public class AbstractEventSystem
    {
        //#if UNITY_EDITOR
        //        protected static UDPEventsCommunicator udpEventsCommunicator = null;
        //        protected StackTrace stackTrace = new StackTrace();
        //        protected string eventName = "";

        //        public AbstractEventSystem()
        //        {
        //            if (udpEventsCommunicator == null) {
        //                udpEventsCommunicator = new UDPEventsCommunicator();
        //                udpEventsCommunicator.Init();
        //            }
        //            this.stackTrace = new StackTrace();
        //            this.eventName = udpEventsCommunicator.EventCreated(this.stackTrace.GetFrame(3).GetMethod());
        //        }
        //#endif
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public class EventSystem : AbstractEventSystem
    {
        //private bool test = false;
        private List<Action> actions = new List<Action>();
        public void AddListener(Action action)
        {
            actions.Add(action);
            //#if UNITY_EDITOR
            //            this.stackTrace = new StackTrace();
            //            udpEventsCommunicator.OnListenerAdded(eventName, action, this.stackTrace.GetFrame(1));
            //#endif
        }

        // TODO: test if that works on scene change too!
        public void AddListenerSingle(Action action)
        {
            if (!actions.Contains(action)) { actions.Add(action); }
        }

        public void RemoveListener(Action action)
        {
            if (actions.Contains(action))
            {
                //#if UNITY_EDITOR
                //                this.stackTrace = new StackTrace();
                //                udpEventsCommunicator.OnListenerRemoved(eventName, action, this.stackTrace.GetFrame(1));
                //#endif
                actions.Remove(action);
            }
        }

        public void RemoveAllListeners()
        {
            actions.Clear();
        }

        public void Invoke()
        {
            for (int i = 0; i < actions.Count; i++)
            {
                actions[i].Invoke();
            }
        }

        public void InvokeSafe()
        {
            foreach (Action a in actions.ToArray())
            {
                a.Invoke();
            }
        }

        public bool HasListeners()
        {
            return this.actions.Count > 0;
        }

        public int GetCountListeners()
        {
            return this.actions.Count;
        }
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public class EventSystem<T> : AbstractEventSystem
    {
        private List<Action<T>> actions = new List<Action<T>>();
        public void AddListener(Action<T> action)
        {
            actions.Add(action);
            //#if UNITY_EDITOR
            //            this.stackTrace = new StackTrace();
            //            udpEventsCommunicator.OnListenerAdded(eventName, action, this.stackTrace.GetFrame(1));
            //#endif
        }

        public void AddListenerSingle(Action<T> action)
        {
            if (!actions.Contains(action)) { actions.Add(action); }
        }

        public void RemoveListener(Action<T> action)
        {
            if (actions.Contains(action))
            {
                //#if UNITY_EDITOR
                //                this.stackTrace = new StackTrace();
                //                udpEventsCommunicator.OnListenerRemoved(eventName, action, this.stackTrace.GetFrame(1));
                //#endif
                actions.Remove(action);
            }
        }

        public void RemoveAllListeners()
        {
            actions.Clear();
        }

        public void Invoke(T param0)
        {
            for (int i = 0; i < actions.Count; i++)
            {
                actions[i].Invoke(param0);
            }
        }
        public void InvokeSafe(T param0)
        {
            foreach (Action<T> a in actions.ToArray())
            {
                a.Invoke(param0);
            }
        }

        public bool HasListeners()
        {
            return this.actions.Count > 0;
        }
        public int GetCountListeners()
        {
            return this.actions.Count;
        }
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public class EventSystem<T1, T2> : AbstractEventSystem
    {
        private List<Action<T1, T2>> actions = new List<Action<T1, T2>>();

        public void AddListener(Action<T1, T2> action)
        {
            actions.Add(action);
            //#if UNITY_EDITOR
            //            this.stackTrace = new StackTrace();
            //            udpEventsCommunicator.OnListenerAdded(eventName, action, this.stackTrace.GetFrame(1));
            //#endif
        }

        public void AddListenerSingle(Action<T1, T2> action)
        {
            if (!actions.Contains(action)) { actions.Add(action); }
        }

        public void RemoveListener(Action<T1, T2> action)
        {
            if (actions.Contains(action))
            {
                //#if UNITY_EDITOR
                //                this.stackTrace = new StackTrace();
                //                udpEventsCommunicator.OnListenerRemoved(eventName, action, this.stackTrace.GetFrame(1));
                //#endif
                actions.Remove(action);
            }
        }

        public void RemoveAllListeners()
        {
            actions.Clear();
        }

        public void Invoke(T1 param0, T2 param1)
        {
            for (int i = 0; i < actions.Count; i++)
            {
                actions[i].Invoke(param0, param1);
            }
        }

        public void InvokeSafe(T1 param1, T2 param2)
        {
            foreach (Action<T1, T2> a in actions.ToArray())
            {
                a.Invoke(param1, param2);
            }
        }

        public bool HasListeners()
        {
            return this.actions.Count > 0;
        }
        public int GetCountListeners()
        {
            return this.actions.Count;
        }
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public class EventSystem<T1, T2, T3> : AbstractEventSystem
    {
        private List<Action<T1, T2, T3>> actions = new List<Action<T1, T2, T3>>();

        public void AddListener(Action<T1, T2, T3> action)
        {
            actions.Add(action);
            //#if UNITY_EDITOR
            //            this.stackTrace = new StackTrace();
            //            udpEventsCommunicator.OnListenerAdded(eventName, action, this.stackTrace.GetFrame(1));
            //#endif
        }

        public void AddListenerSingle(Action<T1, T2, T3> action)
        {
            if (!actions.Contains(action)) { actions.Add(action); }
        }

        public void RemoveListener(Action<T1, T2, T3> action)
        {
            if (actions.Contains(action))
            {
                //#if UNITY_EDITOR
                //                this.stackTrace = new StackTrace();
                //                udpEventsCommunicator.OnListenerRemoved(eventName, action, this.stackTrace.GetFrame(1));
                //#endif
                actions.Remove(action);
            }
        }

        public void RemoveAllListeners()
        {
            actions.Clear();
        }

        public void Invoke(T1 param0, T2 param1, T3 param2)
        {
            for (int i = 0; i < actions.Count; i++)
            {
                actions[i].Invoke(param0, param1, param2);
            }
        }

        public void InvokeSafe(T1 param1, T2 param2, T3 param3)
        {
            foreach (Action<T1, T2, T3> a in actions.ToArray())
            {
                a.Invoke(param1, param2, param3);
            }
        }

        public bool HasListeners()
        {
            return this.actions.Count > 0;
        }
        public int GetCountListeners()
        {
            return this.actions.Count;
        }
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public class EventSystem<T1, T2, T3, T4> : AbstractEventSystem
    {
        private List<Action<T1, T2, T3, T4>> actions = new List<Action<T1, T2, T3, T4>>();

        public void AddListener(Action<T1, T2, T3, T4> action)
        {
            actions.Add(action);
            //#if UNITY_EDITOR
            //            this.stackTrace = new StackTrace();
            //            udpEventsCommunicator.OnListenerAdded(eventName, action, this.stackTrace.GetFrame(1));
            //#endif
        }

        public void AddListenerSingle(Action<T1, T2, T3, T4> action)
        {
            if (!actions.Contains(action)) { actions.Add(action); }
        }

        public void RemoveListener(Action<T1, T2, T3, T4> action)
        {
            if (actions.Contains(action))
            {
                //#if UNITY_EDITOR
                //                this.stackTrace = new StackTrace();
                //                udpEventsCommunicator.OnListenerRemoved(eventName, action, this.stackTrace.GetFrame(1));
                //#endif
                actions.Remove(action);
            }
        }

        public void RemoveAllListeners()
        {
            actions.Clear();
        }

        public void Invoke(T1 param0, T2 param1, T3 param2, T4 param3)
        {
            for (int i = 0; i < actions.Count; i++)
            {
                actions[i].Invoke(param0, param1, param2, param3);
            }
        }

        public void InvokeSafe(T1 param1, T2 param2, T3 param3, T4 param4)
        {
            foreach (Action<T1, T2, T3, T4> a in actions.ToArray())
            {
                a.Invoke(param1, param2, param3, param4);
            }
        }

        public bool HasListeners()
        {
            return this.actions.Count > 0;
        }
        public int GetCountListeners()
        {
            return this.actions.Count;
        }
    }




    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public class EventSystem<T1, T2, T3, T4, T5> : AbstractEventSystem
    {
        private List<Action<T1, T2, T3, T4, T5>> actions = new List<Action<T1, T2, T3, T4, T5>>();

        public void AddListener(Action<T1, T2, T3, T4, T5> action)
        {
            actions.Add(action);
            //#if UNITY_EDITOR
            //            this.stackTrace = new StackTrace();
            //            udpEventsCommunicator.OnListenerAdded(eventName, action, this.stackTrace.GetFrame(1));
            //#endif
        }

        public void AddListenerSingle(Action<T1, T2, T3, T4, T5> action)
        {
            if (!actions.Contains(action)) { actions.Add(action); }
        }

        public void RemoveListener(Action<T1, T2, T3, T4, T5> action)
        {
            if (actions.Contains(action))
            {
                //#if UNITY_EDITOR
                //                this.stackTrace = new StackTrace();
                //                udpEventsCommunicator.OnListenerRemoved(eventName, action, this.stackTrace.GetFrame(1));
                //#endif
                actions.Remove(action);
            }
        }

        public void RemoveAllListeners()
        {
            actions.Clear();
        }

        public void Invoke(T1 param0, T2 param1, T3 param2, T4 param3, T5 param4)
        {
            for (int i = 0; i < actions.Count; i++)
            {
                actions[i].Invoke(param0, param1, param2, param3, param4);
            }
        }

        public void InvokeSafe(T1 param1, T2 param2, T3 param3, T4 param4, T5 param5)
        {
            foreach (Action<T1, T2, T3, T4, T5> a in actions.ToArray())
            {
                a.Invoke(param1, param2, param3, param4, param5);
            }
        }

        public bool HasListeners()
        {
            return this.actions.Count > 0;
        }
        public int GetCountListeners()
        {
            return this.actions.Count;
        }
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}