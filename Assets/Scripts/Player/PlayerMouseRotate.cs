using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerMouseRotate : MonoBehaviourPunCallbacks
{
    [SerializeField] private LayerMask groundMask;

    public Camera cam;

    public PhotonView pv;

    private void Start()
    {
        //cam = PhotonTestNaNoPlayer.instance.cam;
        cam = Camera.main;
    }

    private void Update()
    {
        if (pv.IsMine && !NaNoPlayer.instance.is_dead) { Aim();}
    }

    private void Aim()
    {
        var (success, position) = GetMousePosition();
        if (success)
        {
            var direction = position - transform.position;
            direction.y = 0;
            transform.forward = direction;
        }
    }

    private (bool success, Vector3 position) GetMousePosition()
    {
        var ray = cam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out var hitInfo, Mathf.Infinity, groundMask))
        {
            return (success: true, position: hitInfo.point);
        }
        else
        {
            //return (success: false, position: Vector3.zero);
            return (success: false, position: hitInfo.point);
        }
    }
}
