using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T m_ins;
    public static T Ins
    {
        get
        {
            if (m_ins == null)
            {
                m_ins = GameObject.FindObjectOfType<T>();

                if (m_ins == null)
                {
                    GameObject singleton = new GameObject(typeof(T).Name);
                    m_ins = singleton.AddComponent<T>();
                }
            }
            return m_ins;
        }
    }

    protected virtual void Awake()
    {
        MakeSingleton(true);
    }

    public void MakeSingleton(bool destroyOnload)
    {
        if (m_ins == null)
        {
            m_ins = this as T;
            if (!destroyOnload) return;

            var root = transform.root;

            if (root != transform)
            {
                DontDestroyOnLoad(root);
                return;
            }
            DontDestroyOnLoad(this.gameObject);
            return;
        }

        Destroy(gameObject);
    }
}
