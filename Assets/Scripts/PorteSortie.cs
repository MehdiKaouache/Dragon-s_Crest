using UnityEngine;

public class PorteSortie : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D autre)
    {
        if (autre.CompareTag("Player"))
        {
            GestionJeu.Instance.DeclencherVictoire();
        }
    }
}
