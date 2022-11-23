using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Photon.Pun;
using Photon.Realtime;
public enum MONSTER_TYPE
{
    ENomalMonster,
    ENomalFarMonster,
    EScoutMonster,

    ETankerMonster,
    EWideCloseMonster,
    EWideFarMonster,
    ESelfDestructMonster
}

public struct Data
{
    public float max_hp;
    public float current_hp;
    public float damage;
    public float defense;
    public float patrol_speed;
    public float chase_speed;

    public float move_range;
    public float patrol_dist;
    public float chase_dist;
    public float attack_dist;
    public float skill_dist;

    public float idle_cool_time;
    public float chase_cool_time;
    public float attack_cool_time;
    public float skill_cool_time;

    public float wait_time;
    public float currnet_state_cool;
    public float current_time;
}

public abstract class NewMonster : MonoBehaviour
{
    public Data data;
    [SerializeField]
    protected Collider[] targets;
    protected PhotonTestPlayer player;
    protected MONSTER_TYPE monster_type;
    [SerializeField]
    protected NavMeshAgent nav;
    [SerializeField]
    protected GameObject[] Particles;
    protected GameObject lock_target;
    protected Vector3 lock_target_pos;
    [SerializeField]
    protected LayerMask target_mask;

    protected bool is_dead = false;

    [SerializeField]
    protected CurrentState current_state = new CurrentState();

    public virtual void Idle()
    {
        
    }

    public virtual void Patrol()
    {

    }

    public virtual void Chase()
    {

    }

    public virtual void Attack()
    {

    }

    public virtual void Skill() { }
    public virtual void Init() { }
    public virtual void Is_Dead() { }

    bool Random_Point(Vector3 center, float range, out Vector3 result)
    {

        for (int i = 0; i < 30; i++)
        {
            Vector3 randomPoint = center + UnityEngine.Random.insideUnitSphere * range;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
            {
                result = hit.position;
                return true;
            }
        }

        result = Vector3.zero;

        return false;
    }

    private Vector3 Get_Random_Point(Transform point = null, float radius = 0)
    {
        Vector3 _point;

        if (Random_Point(point == null ? transform.position : point.position, radius == 0 ? data.move_range : radius, out _point))
        {
            Debug.DrawRay(_point, Vector3.up, Color.black, 1);

            return _point;
        }

        return point == null ? Vector3.zero : point.position;
    }

}
