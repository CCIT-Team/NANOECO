using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy : Player
{
    ParticleSystem p;
    // Start is called before the first frame update
    void Start()
    {
        max_hp = 30;
        current_hp = max_hp;
        p = GetComponent<ParticleSystem>();
        StartCoroutine("Destroydummy");
    }

    private void Awake()
    {
        
    }

    void Update()
    {
        if (current_hp <= 0)
            is_dead = true;

        if (!is_dead && current_hp < max_hp )
            StopCoroutine("Destroydummy");
        else if (is_dead)
        { 
            StartCoroutine("Destroydummy");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            var monster = other.GetComponent<NewMonster>();
            monster.data.current_hp -= 5;
        }
    }

    IEnumerator Destroydummy()
    {
        yield return new WaitForSeconds(8f);
        transform.Translate(Vector3.down * 400);
        yield return new WaitForSeconds(2f);
        Destroy(this.gameObject);
    }
}
