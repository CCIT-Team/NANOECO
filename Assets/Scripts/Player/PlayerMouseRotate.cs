using UnityEngine;


public class PlayerMouseRotate : MonoBehaviour
{
    [SerializeField] private LayerMask groundMask;

    public Camera cam;

    private void Start()
    {
        //cam = PhotonTestPlayer.instance.cam;
        cam = Camera.main;
    }

    private void Update()
    {
        if(Player.instance.pv.IsMine && Player.instance.is_dead == false)
        {
            Aim();
        }
    }

    private void Aim()
    {
        var (success, position) = GetMousePosition();
        if (success && Player.instance.is_dead == false)
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
