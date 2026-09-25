using UnityEngine;

// Adapté depuis ZoneDangereuse.cs (fourni par le prof) : message générique au lieu de "robot".
// Logique identique.
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

        autre.transform.position = pointDepart.position;
        Debug.Log("Le chevalier retourne au point de départ.");
    }
}
