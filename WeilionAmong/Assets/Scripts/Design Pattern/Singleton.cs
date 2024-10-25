using UnityEngine;

public abstract class Singleton<T> where T : MonoBehaviour, ISingleton
{
    private static T _instance;
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindAnyObjectByType<T>();
            }

            if (_instance == null)
            {
                _instance = new GameObject().AddComponent<T>();
                _instance.gameObject.name = _instance.GetType().ToString();
            }

            return _instance;
        }
    }
}

public interface ISingleton
{

}