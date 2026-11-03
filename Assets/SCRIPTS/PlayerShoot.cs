
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public float distanciaTiro = 100f;

    private Animation animacaoArma;

    void Start()
    {
        animacaoArma = GetComponentInChildren<Animation>();

        if (animacaoArma == null)
            Debug.LogError("Animation da arma não encontrada!");
        else
            Debug.Log("Animation da arma encontrada!");
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("ATIROU!");

            if (animacaoArma != null)
            {
                if (animacaoArma.GetClip("ARMARICOCHET") != null)
                {
                    animacaoArma.Play("ARMARICOCHET");
                }
                else
                {
                    Debug.LogError("Animação ARMARICOCHET não encontrada!");
                }
            }

            Atirar();
        }
    }

    void Atirar()
    {
        Ray ray = new Ray(transform.position, transform.forward);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, distanciaTiro))
        {
            Debug.Log("Acertou: " + hit.collider.name);

            if (hit.collider.CompareTag("zombie"))
            {
                ZOMBIECONTROLER zombie =
                    hit.collider.GetComponentInParent<ZOMBIECONTROLER>();

                if (zombie != null)
                {
                    Debug.Log("ZUMBI MORREU!");
                    zombie.Morrer();
                }
            }
        }
        else
        {
            Debug.Log("Não acertou nada!");
        }
    }
}
