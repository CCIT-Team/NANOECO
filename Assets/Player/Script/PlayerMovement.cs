using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : Character
{
    public Rigidbody rigid;
    Animator ani;
    public float dash_force;
    bool isdash = false;
   // public Camera maincamera;

    void Start()
    {
        ani = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        is_jump = false;
    }


    void Movement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
       
        if (horizontal > 0 || horizontal < 0 || vertical > 0 || vertical < 0)
        {
            ani.SetBool("Run", true);
        }
        else
        {
            ani.SetBool("Run", false);
        }
        Vector3 move = new Vector3(-horizontal * move_speed, 0f, -vertical * move_speed);
        rigid.velocity = move;
        Jump();
        Dash();
        MouseRotation();
    }
    void Jump()
    {
        float jump = Input.GetAxisRaw("Jump");
        rigid.AddForce(Vector3.up * jump * jump_force);
        if (jump > 0)  { is_jump = true;}
    }
    void Dash()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            rigid.AddForce(Vector3.forward * dash_force);
            isdash = true;
        }
        else { isdash = false; }
    }
    void MouseRotation()
    {
        ////먼저 계산을 위해 마우스와 게임 오브젝트의 현재의 좌표를 임시로 저장합니다.
        //Vector3 mPosition = Input.mousePosition; //마우스 좌표 저장
        //Vector3 oPosition = transform.position; //게임 오브젝트 좌표 저장

        ////카메라가 앞면에서 뒤로 보고 있기 때문에, 마우스 position의 z축 정보에 
        ////게임 오브젝트와 카메라와의 z축의 차이를 입력시켜줘야 합니다.
        //mPosition.y = oPosition.y - maincamera.transform.position.y;

        ////화면의 픽셀별로 변화되는 마우스의 좌표를 유니티의 좌표로 변화해 줘야 합니다.
        ////그래야, 위치를 찾아갈 수 있겠습니다.
        //Vector3 target = maincamera.ScreenToWorldPoint(mPosition);

        ////다음은 아크탄젠트(arctan, 역탄젠트)로 게임 오브젝트의 좌표와 마우스 포인트의 좌표를
        ////이용하여 각도를 구한 후, 오일러(Euler)회전 함수를 사용하여 게임 오브젝트를 회전시키기
        ////위해, 각 축의 거리차를 구한 후 오일러 회전함수에 적용시킵니다.

        ////우선 각 축의 거리를 계산하여, dy, dx에 저장해 둡니다.
        //float dz = target.z - oPosition.z;
        //float dx = target.x - oPosition.x;

        ////오릴러 회전 함수를 0에서 180 또는 0에서 -180의 각도를 입력 받는데 반하여
        ////(물론 270과 같은 값의 입력도 전혀 문제없습니다.) 아크탄젠트 Atan2()함수의 결과 값은 
        ////라디안 값(180도가 파이(3.141592654...)로)으로 출력되므로
        ////라디안 값을 각도로 변화하기 위해 Rad2Deg를 곱해주어야 각도가 됩니다.
        //float rotateDegree = Mathf.Atan2(dz, dx) * Mathf.Rad2Deg;

        ////구해진 각도를 오일러 회전 함수에 적용하여 z축을 기준으로 게임 오브젝트를 회전시킵니다.
        //transform.rotation = Quaternion.Euler(0f, rotateDegree, 0f);
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Monster") { current_hp -= col.gameObject.GetComponent<Character>().damage;}
    }
}

public enum state
{
    IDLE,
    RUN,
    DASH,
    JUMP,




}
public class Wepon : PlayerMovement
{

}
