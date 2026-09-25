using TMPro;                       // Textes TextMeshPro.
using UnityEngine;                 // Composants et fonctionnalités Unity.
using UnityEngine.SceneManagement; // Chargement et rechargement des scènes.
using UnityEngine.UI;              // Composant Image de la barre de progression.

// Adapté depuis GestionJeu.cs (fourni par le prof) pour le thème Dragon's Crest.
// Changements : Batteries -> Blasons (les cristaux du scénario), MouvementRobot -> MouvementChevalier.
// Le reste de la logique est identique à l'original.
public class GestionJeu : MonoBehaviour
{
    public static GestionJeu Instance { get; private set; }

    [Header("Progression")]

    // Nombre de blasons nécessaires pour activer la sortie.
    [SerializeField] private int objectifBlasons = 3;

    // Nombre de vies au début de la partie.
    [SerializeField] private int viesInitiales = 3;

    [Header("Interface")]

    // Textes affichant la progression et les vies restantes.
    [SerializeField] private TMP_Text texteBlasons;
    [SerializeField] private TMP_Text texteVies;

    // Image dont le remplissage représente la progression.
    [SerializeField] private Image barreProgression;

    // Panneaux affichés à la fin de la partie.
    [SerializeField] private GameObject panneauVictoire;
    [SerializeField] private GameObject panneauDefaite;

    [Header("Niveau")]

    // Porte activée lorsque l'objectif de blasons est atteint.
    [SerializeField] private GameObject porteSortie;

    // Script du joueur permettant de désactiver ses commandes.
    [SerializeField] private MouvementChevalier joueur;

    // Script responsable des effets sonores.
    [SerializeField] private AudioJeu audioJeu;

    // Nombre de blasons actuellement collectés.
    private int blasonsCollectes;

    // Nombre de vies restantes.
    private int vies;

    // Empêche de modifier la partie après une victoire ou une défaite.
    private bool partieTerminee;

    public bool PartieTerminee => partieTerminee;

    // Retourne true si le nombre de blasons est suffisant.
    public bool ObjectifAtteint => blasonsCollectes >= objectifBlasons;

    // Script déclenchant l'effet visuel lorsque le joueur perd une vie.
    [SerializeField] private EffetDegatsJoueur effetDegatsJoueur;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        vies = viesInitiales;
        blasonsCollectes = 0;
        partieTerminee = false;

        panneauVictoire.SetActive(false);
        panneauDefaite.SetActive(false);

        porteSortie.SetActive(false);

        ActualiserInterface();

        audioJeu?.JouerLancement();
    }

    // Ajoute des blasons au compteur.
    public void AjouterBlason(int valeur = 1)
    {
        if (partieTerminee) return;

        blasonsCollectes += valeur;

        audioJeu?.JouerCollecte();

        ActualiserInterface();

        if (ObjectifAtteint)
        {
            porteSortie.SetActive(true);
            audioJeu?.JouerObjectif();
        }
    }

    // Retire une vie, par exemple après une collision avec un ennemi.
    public void PerdreVie()
    {
        if (partieTerminee)
            return;

        vies = Mathf.Max(vies - 1, 0);

        audioJeu?.JouerImpact();

        if (effetDegatsJoueur != null)
            effetDegatsJoueur.DeclencherEffet();

        ActualiserInterface();

        if (vies == 0)
            DeclencherDefaite();
    }

    // Peut être appelée lorsque le joueur atteint la sortie.
    public void DeclencherVictoire()
    {
        if (partieTerminee || !ObjectifAtteint) return;

        partieTerminee = true;

        panneauVictoire.SetActive(true);

        joueur.DesactiverCommandes();

        audioJeu?.JouerVictoire();
    }

    // Termine la partie par une défaite.
    public void DeclencherDefaite()
    {
        if (partieTerminee) return;

        partieTerminee = true;

        panneauDefaite.SetActive(true);

        joueur.DesactiverCommandes();

        audioJeu?.JouerDefaite();
    }

    // Méthode appelée par MinuterieJeu lorsque le temps atteint zéro.
    public void TempsEcoule() => DeclencherDefaite();

    // Synchronise les éléments de l'interface avec l'état du jeu.
    private void ActualiserInterface()
    {
        // Exemple : "Blasons : 2/3".
        texteBlasons.text =
            $"Blasons : {blasonsCollectes}/{objectifBlasons}";

        texteVies.text = $"Vies : {vies}";

        if (barreProgression != null)
            barreProgression.fillAmount = objectifBlasons > 0
                ? (float)blasonsCollectes / objectifBlasons
                : 0f;
    }

    // Peut être associée au OnClick d'un bouton "Recommencer".
    public void RecommencerPartie()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
