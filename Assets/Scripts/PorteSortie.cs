using UnityEngine;
using UnityEngine.SceneManagement;

public class PorteSortie : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D autre)
    {
        if (!autre.CompareTag("Player"))
            return;

        // Sécurité : la porte ne fonctionne que si l'objectif est atteint.
        if (GestionJeu.Instance != null && !GestionJeu.Instance.ObjectifAtteint)
            return;

        // S'il existe une scène suivante dans le build, on la charge (Niveau 1).
        // Sinon, c'est le dernier niveau : on affiche la victoire (Niveau 2).
        if (ExisteSceneSuivante())
            ChargerSceneSuivante();
        else
            GestionJeu.Instance.DeclencherVictoire();
    }

    private bool ExisteSceneSuivante()
    {
        int sceneSuivante = SceneManager.GetActiveScene().buildIndex + 1;
        return sceneSuivante < SceneManager.sceneCountInBuildSettings;
    }

    public void ChargerSceneSuivante()
    {
        int sceneActuelle = SceneManager.GetActiveScene().buildIndex;
        int sceneSuivante = sceneActuelle + 1;

        if (sceneSuivante < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(sceneSuivante);
        }
        else
        {
            Debug.LogWarning("Il n'existe aucune scène suivante.");
        }
    }
}
