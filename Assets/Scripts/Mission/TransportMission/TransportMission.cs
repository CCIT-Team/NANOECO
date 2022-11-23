using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransportMission : MissionBase
{
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
                Mission_Event();
            }
            else if(is_active && active_count == 0)
            {
                is_active = false;
                StopAllCoroutines();
            }
        }
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
        StartCoroutine(Curve_Move(p1, p2, p3));
    }

    IEnumerator Curve_Move(Vector3 p1, Vector3 p2, Vector3 p3)
    {
        Vector3 p4 = Vector3.Lerp(p1, p3, t);
        Vector3 p5 = Vector3.Lerp(p3, p2, t);
        target.transform.position = Vector3.Lerp(p4, p5, t);
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
        StartCoroutine(Straight_Move(point1.position, point2.position));
    }

    IEnumerator Straight_Move(Vector3 p1, Vector3 p2)
    {
        target.transform.position = Vector3.Lerp(p1, p2, t);
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

    public override void Clear()
    {
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