using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : Component
{
    private static T _instance;
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType(typeof(T)) as T;
                if (_instance == null)
                {
                    GameObject go = GameObject.Find("MonoSingletonHandler");
                    if (go == null)
                    {
                        go = new GameObject("MonoSingletonHandler");
                        DontDestroyOnLoad(go);
                    }
                    _instance = go.AddComponent<T>();
                }
            }
            return _instance;
        }
    }
}