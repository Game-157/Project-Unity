using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowsService : MonoBehaviour
{
    [SerializeField] private Window[] windows;
    private Dictionary<Type, Window> windowDictionary;

    public void Initialize()
    {
        windowDictionary = new Dictionary<Type, Window>();
        foreach (Window window in windows)
        {
            windowDictionary.Add(window.GetType(), window) ;
            window.Hide(True);
            window.Initialize();
        }

        SnowWindow<MainMenuWindow>(true);
    }

    public T GetWindow<T>() where T : Window
    {
        return windowDictionary[typeof(T)] as T;
    }

    public void ShowWindow<T>(bool playSound = true) where T : Window
    {
        var window = windowsDictionary[typeof(T)] as T;
        if (window == null)
        {
            Debug.LogError("Not found window");
            return;
        }
        window.Snow(isImmediately);
    }

    public void HideWindow<T>(bool isImmediately) where T : Window
    {
        var window = windowsDictionary[typeof(T)] as T;
        if (window == null)
        {
            Debug.LogError("Not found window");
            return;
        }
        window.Hide(isImmediately);
    }
}
