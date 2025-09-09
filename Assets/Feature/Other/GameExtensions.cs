using UnityEngine;

public static class GameExtensions
{
    public static void TryGetAndUseComponent<T>(this GameObject container, out T component) where T : Component => container.TryGetComponent(out component);
}