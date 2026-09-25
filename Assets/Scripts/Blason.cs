using UnityEngine;

// Nouveau script (n'existait pas dans le zip du prof) : l'objet à ramasser.
// Suit le même principe que "Batterie.cs" vu en exemple en semaine 4, relié
// ici à GestionJeu.AjouterBlason() plutôt qu'AjouterBatterie().
public class Blason : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GestionJeu.Instance.AjouterBlason();
            Destroy(gameObject);
        }
    }
}
