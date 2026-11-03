
using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class ZOMBIECONTROLER : MonoBehaviour
{
    private Transform target;
    private NavMeshAgent agent;
    private Animator animator;

    public GameObject zombiePrefab;
    

    private bool morto = false;

    void Start()
    {
        target = Camera.main.transform;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!morto && target != null)
        {
            agent.SetDestination(target.position);
        }
    }

    public void Morrer()
    {
        if (morto) return;

        morto = true;

        agent.isStopped = true;
        agent.enabled = false;

        animator.SetTrigger("BackFall");

        StartCoroutine(Respawn());
    }



    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(2.5f);

        bool nasceu = false;

        while (!nasceu)
        {
            Vector3 randomPosition = transform.position + Random.insideUnitSphere * 15f;

            NavMeshHit hit;

            if (NavMesh.SamplePosition(randomPosition, out hit, 15f, NavMesh.AllAreas))
            {
                float distancia = Vector3.Distance(hit.position, target.position);

                if (distancia >= 8f)
                {
                    Instantiate(zombiePrefab, hit.position, Quaternion.identity);
                    nasceu = true;
                }
            }

            yield return null;
        }

        Destroy(gameObject);
    }


}
