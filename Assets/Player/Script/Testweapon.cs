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
            // 2. 총알 공장에서 총알을 만든다.
            GameObject bullets = Instantiate(bullet);
            // 3. 총알을 발사한다.(총알을 총구 위치로 가져다 둔다.)
            bullet.transform.position = firePosition.transform.position;
        }
    }
}
