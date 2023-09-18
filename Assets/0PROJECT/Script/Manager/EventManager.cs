using System;
using System.Collections.Generic;

/// <summary>
/// The class in which all the events used in the game are controlled.
/// All events in the game are defined under the GameEvent enum. Then, in the class where you want these events to be used, definition is made via enum.
/// There are 3 main methods: AddHandler, RemoveHandler and Broadcast.
/// AddHandler: The method in which the event is assigned and added to the dictionary.
/// RemoveHandler: A method that removes the defined event from the dictionary if the object becomes inactive.
/// Broadcast: The method you use where you want the event to run.
/// Different overloads of these are also defined in the manager. 
/// If you want to send an event with a variable, you can use those overloads.
/// </summary>
public enum GameEvent
{
    OnStart,
    OnFinish,
    OnConnectDot,
    OnClosedConnection,
    OnScore,
    OnLineIntersection,
    OnBombExplode,


    //SOUND EVENTS
    OnPlaySound,
    OnPlaySoundVolume,
    OnPlaySoundPitch,
    OnPlaySoundBg,

}
public static class EventManager
{
    private static Dictionary<GameEvent, Action> eventTable =
        new Dictionary<GameEvent, Action>();

    public static void AddHandler(GameEvent gameEvent, Action action)
    {
        if (!eventTable.ContainsKey(gameEvent))
            eventTable[gameEvent] = action;
        else eventTable[gameEvent] += action;
    }

    public static void RemoveHandler(GameEvent gameEvent, Action action)
    {
        if (eventTable[gameEvent] != null)
            eventTable[gameEvent] -= action;

        if (eventTable[gameEvent] == null)
            eventTable.Remove(gameEvent);
    }

    public static void Broadcast(GameEvent gameEvent)
    {
        if (eventTable[gameEvent] != null)
            eventTable[gameEvent]();
    }








    private static Dictionary<GameEvent, Action<object>> eventTableDouble
        = new Dictionary<GameEvent, Action<object>>();

    public static void AddHandler(GameEvent gameEvent, Action<object> action)
    {
        if (!eventTableDouble.ContainsKey(gameEvent))
            eventTableDouble[gameEvent] = action;
        else eventTableDouble[gameEvent] += action;
    }

    public static void RemoveHandler(GameEvent gameEvent, Action<object> action)
    {
        if (eventTableDouble[gameEvent] != null)
            eventTableDouble[gameEvent] -= action;

        if (eventTableDouble[gameEvent] == null)
            eventTableDouble.Remove(gameEvent);
    }

    public static void Broadcast(GameEvent gameEvent, object value)
    {
        if (eventTableDouble[gameEvent] != null)
            eventTableDouble[gameEvent](value);
    }








    private static Dictionary<GameEvent, Action<object, object>> eventTableTriple
    = new Dictionary<GameEvent, Action<object, object>>();

    public static void AddHandler(GameEvent gameEvent, Action<object, object> action)
    {
        if (!eventTableTriple.ContainsKey(gameEvent))
            eventTableTriple[gameEvent] = action;
        else eventTableTriple[gameEvent] += action;
    }

    public static void RemoveHandler(GameEvent gameEvent, Action<object, object> action)
    {
        if (eventTableTriple[gameEvent] != null)
            eventTableTriple[gameEvent] -= action;

        if (eventTableTriple[gameEvent] == null)
            eventTableTriple.Remove(gameEvent);
    }

    public static void Broadcast(GameEvent gameEvent, object value1, object value2)
    {
        if (eventTableTriple[gameEvent] != null)
            eventTableTriple[gameEvent](value1, value2);
    }









    private static Dictionary<GameEvent, Action<object, object, object>> eventTableFourth
    = new Dictionary<GameEvent, Action<object, object, object>>();

    public static void AddHandler(GameEvent gameEvent, Action<object, object, object> action)
    {
        if (!eventTableFourth.ContainsKey(gameEvent))
            eventTableFourth[gameEvent] = action;
        else eventTableFourth[gameEvent] += action;
    }

    public static void RemoveHandler(GameEvent gameEvent, Action<object, object, object> action)
    {
        if (eventTableFourth[gameEvent] != null)
            eventTableFourth[gameEvent] -= action;

        if (eventTableFourth[gameEvent] == null)
            eventTableFourth.Remove(gameEvent);
    }

    public static void Broadcast(GameEvent gameEvent, object value1, object value2, object value3)
    {
        if (eventTableFourth[gameEvent] != null)
            eventTableFourth[gameEvent](value1, value2, value3);
    }


}
