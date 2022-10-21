using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public class GenericClass<T>
    {
        public T m_Value;

        public GenericClass(T value)
        {
            m_Value = value;
        }

        public void Method1()
        {
            Debug.Log(m_Value);
        }
    }

    public static GameManager gm;

    public Time t;

    private void Awake()
    {
        gm = this;
    }

    void Start()
    {
        
    }

   
    void Update()
    {
        
    }


}
