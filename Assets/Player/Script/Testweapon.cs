using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testweapon : MonoBehaviour
{
    public GameObject bullet;
    public GameObject firePosition;
    void Start()
    {
        
    }

    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // 2. �Ѿ� ���忡�� �Ѿ��� �����.
            GameObject bullets = Instantiate(bullet);
            // 3. �Ѿ��� �߻��Ѵ�.(�Ѿ��� �ѱ� ��ġ�� ������ �д�.)
            bullet.transform.position = firePosition.transform.position;
        }
    }
}
