using UnityEngine;

// Adapté depuis ObstacleDangereux.cs (fourni par le prof) : message générique au lieu
// de "robot", et suppression du bloc de code commenté (ancienne version, inutile).
// Logique identique.
public class ObstacleDangereux : MonoBehaviour
{
    [SerializeField, Min(0.1f)]
    private float delaiEntreDegats = 1f;

    [SerializeField]
    private Transform pointDepart;

    private float prochainDegat;

    private void OnCollisionStay2D(Collision2D collision)
    {
        InfligerDegat(collision.gameObject);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        InfligerDegat(collision.gameObject);
    }

    private void InfligerDegat(GameObject objet)
    {
        // Vérifier qu'il s'agit du joueur
        if (!objet.CompareTag("Player"))
            return;

        // Respecter le délai entre deux dégâts
        if (Time.time < prochainDegat)
            return;

        prochainDegat = Time.time + delaiEntreDegats;

        // Retirer une vie
        if (GestionJeu.Instance != null)
        {
            GestionJeu.Instance.PerdreVie();
        }

        // Déclencher l'effet visuel
        EffetDegatsJoueur effet = objet.GetComponent<EffetDegatsJoueur>();

        if (effet != null)
        {
            effet.DeclencherEffet();
        }
        else
        {
            Debug.LogWarning(
                "Le composant EffetDegatsJoueur est absent du joueur."
            );
        }

        // Replacer le joueur au point de départ
        if (pointDepart != null)
        {
            objet.transform.position = pointDepart.position;
            Debug.Log("Le chevalier retourne au point de départ.");
        }
        else
        {
            Debug.LogWarning(
                "Le PointDepart n'est pas assigné à l'obstacle."
            );
        }
    }
}
