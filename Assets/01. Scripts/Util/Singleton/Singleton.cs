using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] 
    private bool _isDontDestroy;

    private static bool _isQuitting;
    private static T _instance;

    public static T Instance
    {
        get
        {
            if (_isQuitting)
            {
                return null;
            }

            if (ReferenceEquals(_instance, null))
            {
                _instance = FindFirstObjectByType<T>();

                if (ReferenceEquals(_instance, null))
                {
                    GameObject go = new GameObject(typeof(T).Name, typeof(T));
                    _instance = go.GetComponent<T>();
                }
                else
                {
                    CheckForDuplicateInstances();
                }
            }
            return _instance;
        }
    }

    protected virtual void Awake()
    {
        if (!ReferenceEquals(_instance, null) && !ReferenceEquals(_instance, this))
        {
            Destroy(transform.root.gameObject);
            return;
        }

        _instance = this as T;
        SetupDontDestroyOnLoad();
    }

    private void OnApplicationQuit()
    {
        _isQuitting = true;
    }

    protected virtual void OnDestroy()
    {
        if (ReferenceEquals(_instance, this))
        {
            _instance = null;
        }
    }

    private static void CheckForDuplicateInstances()
    {
        T[] instances = FindObjectsByType<T>(FindObjectsSortMode.None);
        if (instances.Length > 1)
        {
            Debug.LogError($"�ߺ��� Singleton {typeof(T)} �ν��Ͻ��� �����Ǿ�, �ߺ� �ν��Ͻ����� �����մϴ�.");
            for (int i = 1; i < instances.Length; i++)
            {
                Destroy(instances[i].gameObject);
            }
        }
    }

    private void SetupDontDestroyOnLoad()
    {
        if (!_isDontDestroy)
        {
            return;
        }

        if (ReferenceEquals(transform.parent, null))
        {
            DontDestroyOnLoad(gameObject);
            return;
        }

        GameObject rootManagerGO = FindAnyObjectByType<T>().gameObject;
        if (!ReferenceEquals(rootManagerGO, null))
        {
            transform.SetParent(rootManagerGO.transform);
            DontDestroyOnLoad(rootManagerGO);
        }
        else
        {
            DontDestroyOnLoad(transform.root.gameObject);
        }
    }
}
