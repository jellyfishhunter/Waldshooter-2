using UnityEngine;
using System.Collections;

public class Loot : MonoBehaviour
{

    public int value = 1;
    public float timeToLive = 10;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (timeToLive <= 0)
        {
            Destroy(gameObject);
        }
        timeToLive -= Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        GameObject target = other.gameObject;
        if (target.tag == "Player")
        {
            target.GetComponent<Player>().collectLoot(this);
            Destroy(gameObject);
        }
    }
}
