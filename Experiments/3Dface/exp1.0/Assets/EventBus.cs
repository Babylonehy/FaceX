using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;

public class EventBus
{
    static Dictionary<Type, IList> _topics = new Dictionary<Type, IList>();

    public static void Publish<T>(T published_event)
    {
        Type t = typeof(T);
        if (_topics.ContainsKey(t))
        {
            IList subscriber_list = new List<Subscription<T>>(_topics[t].Cast<Subscription<T>>());
            List<Subscription<T>> orphaned_subscriptions = new List<Subscription<T>>();
            foreach (Subscription<T> s in subscriber_list)
            {
                if (s.callback.Target == null || s.callback.Target.Equals(null))
                {
                    orphaned_subscriptions.Add(s);
                }
                else
                {
                    s.callback(published_event);
                }
            }
            foreach (Subscription<T> orphan_subscription in orphaned_subscriptions)
            {
                EventBus.Unsubscribe<T>(orphan_subscription);
            }
        }
    }

    public static Subscription<T> Subscribe<T>(Action<T> callback)
    {
        Type t = typeof(T);
        Subscription<T> new_subscription = new Subscription<T>(callback);
        if (!_topics.ContainsKey(t))
        {
            _topics[t] = new List<Subscription<T>>();
        }
        _topics[t].Add(new_subscription);
        return new_subscription;
    }

    public static void Unsubscribe<T>(Subscription<T> subscription)
    {
        Type t = typeof(T);
        if (_topics.ContainsKey(t) && _topics[t].Count > 0)
        {
            _topics[t].Remove(subscription);
        }
    }
}

public class Subscription<T>
{
    public Action<T> callback { get; private set; }

    public Subscription(Action<T> _callback)
    {
        callback = _callback;
    }

    ~Subscription()
    {
        EventBus.Unsubscribe<T>(this);
    }
}
