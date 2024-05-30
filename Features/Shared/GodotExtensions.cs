using System.Collections.Generic;
using System.Linq;
using Godot;

public static class GodotExtensions
{
    public static IEnumerable<T> GetChildren<T>(this Node node) where T : Node
    {
        return node.GetChildren()
                .Where(c => c.GetType() == typeof(T))
                .Select(c => (T)c);
    }

    public static T GetClosestChild<T>(this Node node, Node2D other) where T : Node2D
    {
        return node.GetChildren<T>().Closest(other);
    }

    public static T Closest<T>(this IEnumerable<T> collection, Node2D other) where T : Node2D
    {
        return collection.MinBy(t => other.GlobalPosition.DistanceTo(t.GlobalPosition));
    }

    public static T Next<T>(this IList<T> collection, T previous)
    {
        // TODO: CHECK THIS STUPID
        var index = collection.IndexOf(previous);
        return index + 1 >= collection.Count ? collection[0] : collection[index + 1];
    }
}