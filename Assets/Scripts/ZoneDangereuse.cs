using UnityEngine;

public class ZoneDangereuse : MonoBehaviour
{
    [SerializeField] private Transform pointDepart;

    private void OnTriggerEnter2D(Collider2D autre)
    {
        if (!autre.CompareTag("Player"))
            return;

        if (pointDepart == null)
        {
            Debug.LogError("Le point de départ n'est pas assigné.");
            return;
        }

        // Retire une vie. PerdreVie() joue aussi le son d'impact, le flash rouge
        // du chevalier et met à jour l'interface (et déclenche la défaite à 0 vie).
        GestionJeu.Instance?.PerdreVie();

        // Replace le joueur au point de départ.
        autre.transform.position = pointDepart.position;

        // Annule son élan pour qu'il ne reparte pas avec sa vitesse.
        Rigidbody2D corps = autre.GetComponent<Rigidbody2D>();
        if (corps != null)
            corps.linearVelocity = Vector2.zero;

        Debug.Log("Le chevalier retourne au point de départ.");
    }
}
