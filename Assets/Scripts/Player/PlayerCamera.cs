using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerCamera : MonoBehaviourPunCallbacks
{
    public Transform player;
    public float smooth_speed = 0.125f;
    public Vector3 offset;
    public float rotate_speed = 5;
    
    Vector3 originpos;
    Vector3 mouseCurserpos;
    float v;
    
    bool inputMode = false;
    private void Start()
    {
        //if(PhotonTestPlayer.instance.pv.IsMine)
        //{
        //    player = PhotonTestPlayer.instance.gameObject.transform;
        //} 
        originpos = offset;
    }
    void FixedUpdate()
    {
        if (player != null && inputMode == false)
        {
            Vector3 desired_position = player.position + offset;
            Vector3 smoothed_position = Vector3.Lerp(transform.position, desired_position, smooth_speed);
            transform.position = smoothed_position;
        }
    }

    private void Update()
    {
        CameraEvent();
        CheckWall();
    }

    void CameraEvent()
    {
        switch (Player.instance.camera_shaking_num)
        {
            case 0:
                StopCoroutine(CameraShaking(0, 0));
                break;
            case 1://���� ��ݽ� �ٰŸ�
                StartCoroutine(CameraShaking(0.25f, 0.5f));
                break;
            case 2://���� ��ݽ� ���Ÿ�
                StartCoroutine(CameraShaking(0.3f, 1f));
                break;
            case 3://���Ϳ��� �ǰ� 
                StartCoroutine(CameraShaking(0.2f, 0.175f));
                break;
            case 4://��ź
                StartCoroutine(CameraShaking(0.5f, 4f));
                break;
            case 5://�̺�Ʈ ����
                StartCoroutine(CameraShaking(0.5f, 0.3f));
                break;
            case 6://�̺�Ʈ ���� ����
                StartCoroutine(CameraShaking(0.35f, 1f));
                break;
        }
    }


    IEnumerator CameraShaking(float duration, float manitude)
    {
        float timer = 0;

        while (timer <= duration)
        {
            Camera.main.transform.localPosition = Random.insideUnitSphere * manitude + originpos;

            timer += Time.deltaTime;
            yield return null;

            Camera.main.transform.localPosition = originpos;
            Player.instance.camera_shaking_num = 0;
        }
    }

    void CheckWall()
    {
        RaycastHit playerRay;
        RaycastHit backRay;
        Debug.DrawRay(transform.position, player.position - transform.position, Color.red);
        if(Physics.Raycast(transform.position, player.position, out playerRay, player.position.z - transform.position.z))
        {
           if (!playerRay.transform.CompareTag("player"))
            {
                offset.y -= playerRay.distance;
            }
           else
            {
                offset = originpos;
            }

        }
        Debug.DrawRay(transform.position, player.position - transform.position, Color.blue);
        if (Physics.Raycast(transform.position, Vector3.back, out backRay, 3))
        {
            offset.z = backRay.transform.position.z;
        }
        else
        {
            offset = originpos;
        }
    }
}
