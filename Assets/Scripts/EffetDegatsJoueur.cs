using System.Collections;
using UnityEngine;

// Adapté depuis EffetDegatsJoueur.cs (fourni par le prof) : renommage Robot -> Chevalier.
// Logique identique.
public class EffetDegatsJoueur : MonoBehaviour
{
    [Header("Chevalier")]
    [SerializeField]
    private SpriteRenderer renduChevalier;

    [Header("Interface")]
    [SerializeField]
    private CanvasGroup flashEcran;

    [Header("Animation")]
    [SerializeField]
    private Color couleurDegat = Color.red;

    [SerializeField, Min(0.1f)]
    private float dureeEffet = 0.45f;

    [SerializeField, Min(1)]
    private int nombreClignotements = 3;

    [SerializeField, Min(1f)]
    private float agrandissement = 1.12f;

    private Color couleurInitiale;
    private Vector3 echelleInitiale;
    private Coroutine effetEnCours;

    private void Awake()
    {
        if (renduChevalier == null)
        {
            renduChevalier = GetComponent<SpriteRenderer>();
        }

        if (renduChevalier != null)
        {
            couleurInitiale = renduChevalier.color;
        }

        echelleInitiale = transform.localScale;

        if (flashEcran != null)
        {
            flashEcran.alpha = 0f;
        }
    }

    public void DeclencherEffet()
    {
        if (effetEnCours != null)
        {
            StopCoroutine(effetEnCours);
        }

        effetEnCours = StartCoroutine(JouerEffetDegats());
    }

    private IEnumerator JouerEffetDegats()
    {
        float dureeEtape =
            dureeEffet / (nombreClignotements * 2f);

        transform.localScale =
            echelleInitiale * agrandissement;

        for (int i = 0; i < nombreClignotements; i++)
        {
            if (renduChevalier != null)
            {
                renduChevalier.color = couleurDegat;
            }

            if (flashEcran != null)
            {
                flashEcran.alpha = 0.55f;
            }

            yield return new WaitForSeconds(dureeEtape);

            if (renduChevalier != null)
            {
                renduChevalier.color = couleurInitiale;
            }

            if (flashEcran != null)
            {
                flashEcran.alpha = 0f;
            }

            yield return new WaitForSeconds(dureeEtape);
        }

        if (renduChevalier != null)
        {
            renduChevalier.color = couleurInitiale;
        }

        if (flashEcran != null)
        {
            flashEcran.alpha = 0f;
        }

        transform.localScale = echelleInitiale;
        effetEnCours = null;
    }

    private void OnDisable()
    {
        if (renduChevalier != null)
        {
            renduChevalier.color = couleurInitiale;
        }

        if (flashEcran != null)
        {
            flashEcran.alpha = 0f;
        }

        transform.localScale = echelleInitiale;
    }
}
