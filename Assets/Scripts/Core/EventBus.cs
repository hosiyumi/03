
using System;
using System.Collections.Generic;
using UnityEngine;

    public class EventBus : MonoBehaviour
    {
        // 懒加载单例
        private static EventBus _instance;

        private static bool _isQuittingOrDestroying;


        public static EventBus Instance
        {
            get
            {
                if (_isQuittingOrDestroying) return null;
                if (_instance == null)
                {
                    // 尝试查找已有实例
                    _instance = FindObjectOfType<EventBus>();
                    if (_instance == null)
                    {
                        // 若不存在则自动创建一个
                        var go = new GameObject("[EventBus]");
                        _instance = go.AddComponent<EventBus>();
                        DontDestroyOnLoad(go);
                        Debug.Log("[EventBus] 已自动创建全局实例。");
                    }
                }
                return _instance;
            }
        }

        // 存储事件类型与对应委托
        private readonly Dictionary<Type, Delegate> _eventTable = new();

        private void Awake()
        {
            // 保证单例唯一
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
                return;
            }
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }

        private void OnApplicationQuit()
        {
            // Play 停止/应用退出时会走这里
            _isQuittingOrDestroying = true;
        }

        private void OnDestroy()
        {
            // 场景关闭/域重载时也会走这里
            // 防止 OnDisable/OnDestroy 里有人访问 Instance 触发新建
            _isQuittingOrDestroying = true;

            // 如果销毁的正是当前实例，把引用清掉
            if (_instance == this) _instance = null;
        }
        #region === 发布与订阅核心逻辑 ===

        /// <summary>
        /// 订阅指定类型的事件。
        /// </summary>
        public void Subscribe<T>(Action<T> listener)
        {
            var type = typeof(T);
            if (_eventTable.TryGetValue(type, out var existingDelegate))
                _eventTable[type] = Delegate.Combine(existingDelegate, listener);
            else
                _eventTable[type] = listener;
        }

        /// <summary>
        /// 取消订阅指定类型的事件。
        /// </summary>
        public void Unsubscribe<T>(Action<T> listener)
        {
            var type = typeof(T);
            if (_eventTable.TryGetValue(type, out var existingDelegate))
            {
                var newDelegate = Delegate.Remove(existingDelegate, listener);
                if (newDelegate == null)
                    _eventTable.Remove(type);
                else
                    _eventTable[type] = newDelegate;
            }
        }

        /// <summary>
        /// 发布事件（立即触发）。
        /// </summary>
        public void Publish<T>(T evt)
        {
            if (_eventTable.TryGetValue(typeof(T), out var d))
            {
                if (d is Action<T> callback)
                    callback.Invoke(evt);
            }
        }

        #endregion
    }

   
