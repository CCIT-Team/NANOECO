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
    }

    void CameraEvent()
    {
        switch (Player.instance.camera_shaking_num)
        {
            case 0:
                StopCoroutine(CameraShaking(0, 0));
                break;
            case 1://무기 사격시 근거리
                StartCoroutine(CameraShaking(0.25f, 0.5f));
                break;
            case 2://무기 사격시 원거리
                StartCoroutine(CameraShaking(0.3f, 1f));
                break;
            case 3://몬스터에게 피격 
                StartCoroutine(CameraShaking(0.2f, 0.175f));
                break;
            case 4://폭탄
                StartCoroutine(CameraShaking(0.5f, 4f));
                break;
            case 5://이벤트 스폰
                StartCoroutine(CameraShaking(0.5f, 0.3f));
                break;
            case 6://이벤트 보스 출현
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
}
