using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventHub : MonoBehaviour
{
    static Dictionary<string, UnityEvent> eventsList = new Dictionary<string, UnityEvent>();

    /// <summary>
    /// To attach a certain method that woul be invoked when described event triggers 
    /// </summary>
    /// <param name="keyValue"></param>
    /// <param name="actionToAdd"></param>
    public static void AttachListener(string keyValue, UnityAction actionToAdd)
    {
        UnityEvent uEvent;
        eventsList.TryGetValue(keyValue, out uEvent);
        if (uEvent != null)
        {
            uEvent.AddListener(actionToAdd);
        }
        else
        {
            uEvent = new UnityEvent();
            uEvent.AddListener(actionToAdd);
            eventsList.Add(keyValue, uEvent);
        }
    }
    /// <summary>
    /// To remove a certain method that was attached to an event
    /// </summary>
    /// <param name="keyValue"></param>
    /// <param name="actionToRemove"></param>
    public static void RemoveListener(string keyValue, UnityAction actionToRemove)
    {
        UnityEvent uEvent;
        eventsList.TryGetValue(keyValue, out uEvent);
        if (uEvent != null)
        {
            uEvent.RemoveListener(actionToRemove);
        }
    }
    /// <summary>
    /// To call registered method for the described event 
    /// </summary>
    /// <param name="keyValue"></param>
    public static void TriggerEvent(string keyValue)
    {
        UnityEvent eventToExecute;
        eventsList.TryGetValue(keyValue, out eventToExecute);
        eventToExecute.Invoke();
    }
}