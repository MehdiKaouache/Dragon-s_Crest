using System.Collections;
using UnityEngine;

// Nouveau script (n'existait pas dans le zip du prof) : effet de secousse de caméra,
// utilisé par EnnemiMobile.cs lors d'un contact avec le joueur.
// Optionnel : si ce composant n'est pas ajouté à la caméra, rien ne se passe
// (EnnemiMobile.cs l'appelle avec l'opérateur ?., donc pas d'erreur si absent).
public class SecousseCamera : MonoBehaviour
{
    [SerializeField, Min(0f)] private float duree = 0.15f;
    [SerializeField, Min(0f)] private float intensite = 0.15f;

    private Vector3 positionInitiale;
    private Coroutine secousseEnCours;

    private void Awake()
    {
        positionInitiale = transform.localPosition;
    }

    public void Declencher()
    {
        if (secousseEnCours != null)
            StopCoroutine(secousseEnCours);

        secousseEnCours = StartCoroutine(Secouer());
    }

    private IEnumerator Secouer()
    {
        float tempsEcoule = 0f;

        while (tempsEcoule < duree)
        {
            Vector2 decalage = Random.insideUnitCircle * intensite;
            transform.localPosition = positionInitiale + new Vector3(decalage.x, decalage.y, 0f);

            tempsEcoule += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = positionInitiale;
        secousseEnCours = null;
    }

    private void OnDisable()
    {
        transform.localPosition = positionInitiale;
    }
}
