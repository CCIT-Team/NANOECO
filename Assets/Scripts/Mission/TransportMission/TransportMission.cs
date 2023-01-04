using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransportMission : MissionBase
{
    [Header("미션 세팅")]
    public List<TransportPoint> path;
    public TransportTarget target;
    public float speed;
    public int current_point = 0;
    [Range(0, 1)]
    public float t = 0;
    bool is_active = false;
    public int active_count;
    public int _active_count
    {
        get { return active_count; }
        set
        {
            active_count = value;
            if(!is_active && active_count > 0)
            {
                is_active = true;
                if (!target_ani.GetBool(target_move)) { target_ani.SetBool(target_move, true); }
                Mission_Event();
            }
            else if(is_active && active_count == 0)
            {
                is_active = false;
                if (target_ani.GetBool(target_move)) { target_ani.SetBool(target_move, false); }
                StopAllCoroutines();
            }
        }
    }
    public Animator target_ani;
    int target_move = Animator.StringToHash("move");

    [Space][Header("스폰 세팅")]
    public List<GameObject> monster_groups;

    [Space][Header("헬기")]
    public GameObject heli;
    public GameObject heli_rope;
    public Transform rotor;
    public Transform tail_rotor;
    public float rotor_speed;
    public float tail_rotor_speed;

    void Update()
    {
        rotor.eulerAngles = new Vector3(rotor.eulerAngles.x, rotor.eulerAngles.y + Time.deltaTime * rotor_speed, rotor.eulerAngles.z);
        tail_rotor.eulerAngles = new Vector3(tail_rotor.eulerAngles.x, tail_rotor.eulerAngles.y, tail_rotor.eulerAngles.z + Time.deltaTime * tail_rotor_speed);
    }

    public override void Mission_Event()
    {
        Transfort_Move();
    }

    public void Transfort_Move()
    {
        if(current_point != path.Count -1)
        {
            if (path[current_point].is_curve) { Curve_Movement(path[current_point], path[current_point + 1]); }
            else { Straight_Movement(path[current_point], path[current_point + 1]); }
        }
    }

    void Curve_Movement(TransportPoint point1, TransportPoint point2)
    {
        Vector3 p1 = point1.position;
        Vector3 p2 = point2.position;
        Vector3 p3 = point1.curve_point.transform.position;
        if (t >= 1) { t = 0; }
        StartCoroutine(Curve_Move(point1, point2, p3));
    }

    IEnumerator Curve_Move(TransportPoint p1, TransportPoint p2, Vector3 p3)
    {
        Vector3 p4 = Vector3.Lerp(p1.position, p3, t);
        Vector3 p5 = Vector3.Lerp(p3, p2.position, t);
        target.transform.position = Vector3.Lerp(p4, p5, t);

        Vector3 p1_rotate = new Vector3(p1.transform.eulerAngles.x, p1.transform.eulerAngles.y, p1.transform.eulerAngles.z);
        Vector3 p2_rotate = new Vector3(p2.transform.eulerAngles.x, p2.transform.eulerAngles.y, p2.transform.eulerAngles.z);
        target.transform.eulerAngles = Vector3.Lerp(p1_rotate, p2_rotate, t);

        yield return new WaitForSeconds(speed);
        if(t < 1)
        {
            StartCoroutine(Curve_Move(p1, p2, p3));
            t += speed;
        }
        else
        {
            current_point++;
            if(current_point != path.Count - 1)
                Transfort_Move();
            else { Clear(); }
        }
    }

    void Straight_Movement(TransportPoint point1, TransportPoint point2)
    {
        if (t >= 1) { t = 0; }
        StartCoroutine(Straight_Move(point1, point2));
    }

    IEnumerator Straight_Move(TransportPoint p1, TransportPoint p2)
    {
        target.transform.position = Vector3.Lerp(p1.position, p2.position, t);
        Vector3 p1_rotate = new Vector3(p1.transform.eulerAngles.x, p1.transform.eulerAngles.y, p1.transform.eulerAngles.z);
        Vector3 p2_rotate = new Vector3(p2.transform.eulerAngles.x, p2.transform.eulerAngles.y, p2.transform.eulerAngles.z);
        target.transform.eulerAngles = Vector3.Lerp(p1_rotate, p2_rotate, t * 5);
        yield return new WaitForSeconds(speed);
        if (t < 1)
        {
            StartCoroutine(Straight_Move(p1, p2));
            t += speed;
        }
        else
        {
            current_point++;
            if (current_point != path.Count - 1)
                Transfort_Move();
            else { Clear(); }
        }
    }

    public void Spawn_Monster(List<Transform> col)
    {
        for(int i = 0; i < col.Count; i++)
        {
            int m = Random.Range(0, monster_groups.Count);
            GameObject mg = Instantiate(monster_groups[i], col[i].position, Quaternion.identity);
            mg.transform.parent = transform;
        }
    }

    public override void Clear()
    {
        target.transform.SetParent(heli_rope.transform);
        heli.SetActive(true);
        ms.mission_0_clear = true;
    }

    void OnDrawGizmos()
    {
        foreach(var v in path)
        {
            if(v == path[0]) { Gizmos.color = Color.red; }
            else if(v == path[path.Count - 1]) { Gizmos.color = Color.blue; }
            else { Gizmos.color = Color.gray; }
            Gizmos.DrawCube(v.position, v.transform.localScale * .5f);

            Gizmos.color = Color.yellow;
            if (v.is_curve) { Gizmos.DrawCube(v.curve_point.transform.position, v.transform.localScale * 0.3f); }
        }
    }
}