# Dragon's Crest

Mini-projet 2D — Développement de jeux vidéo (420-511-MV), Cégep Marie-Victorin, automne 2026

| | |
|---|---|
| **Enseignant** | Sofiane Faidi |
| **Étudiant** | Mehdi Kaouache |
| **Dépôt GitHub** | https://github.com/MehdiKaouache/Dragon-s_Crest.git |
| **Moteur** | Unity 6000.2.5f1 (projet 2D) |

## Description

Dragon's Crest est un jeu 2D vu de dessus, sans saut ni gravité. Le royaume de Veyra était protégé par des dragons ancestraux, dont le pouvoir était scellé dans des cristaux appelés les **Blasons**. Depuis la disparition mystérieuse des dragons, les Blasons sont dispersés dans leur repaire abandonné. Le joueur incarne un chevalier parti les récupérer.

Le projet est un « reskin » thématique du **Robot Collecteur**, le projet fil rouge du cours : même structure et même logique de jeu (déplacement en 4 directions, collecte d'objets, vies, porte de sortie conditionnelle), avec mon propre thème, mes propres niveaux et mes propres ennemis.

## Comment jouer

1. Ouvrir le projet avec **Unity 6000.2.5f1**.
2. Ouvrir la scène `Assets/Scenes/Niveau1.unity`, puis appuyer sur **Play**.
3. Les scènes sont déjà dans la liste du build (File > Build Profiles) : `Niveau1` (index 0), `Niveau2` (index 1).

**Contrôles** : flèches directionnelles ou WASD.

## Les niveaux

### Niveau 1
Une salle fermée. Le chevalier doit collecter **3 Blasons**. Quand le dernier est ramassé, un son retentit et la porte de sortie apparaît. La toucher charge automatiquement le Niveau 2.

### Niveau 2
Une carte en forme de L : une salle principale, un **couloir étroit** et une salle en hauteur. Le joueur doit collecter **6 Blasons**, en évitant :
- un **dragon de patrouille** (aller-retour dans la salle principale, il ignore le joueur) ;
- un **dragon de poursuite** (immobile tant que le joueur est loin, il le chasse dès qu'il entre dans son rayon de détection et s'arrête s'il le perd) ;
- **2 zones de piques** (une dans la salle principale, une à l'entrée du couloir).

La porte de sortie, dans la salle du haut, affiche l'écran de **victoire**. Perdre les 3 vies affiche l'écran de **défaite**.

## Exigences de la Partie 2 et leur réalisation

| Exigence | Réalisation |
|---|---|
| 4.1 Environnement différent mais cohérent | Nouvelle carte en L (Tilemaps `Mur` et `Obstacle`), même tileset et même fond de grotte que le Niveau 1. Nouvelle position des Blasons, des obstacles, des zones dangereuses et de la porte. |
| 4.2 Deux comportements d'ennemis | Deux ennemis basés sur `EnnemiMobile`, configurés différemment : patrouille entre deux points (rayon de détection minimal, ignore le joueur) et poursuite (aucun point de patrouille, rayon de détection de 3,5). Collider, dégâts, effet visuel (flash orange) et sonore, délai de 1,2 s entre deux dégâts. |
| 4.3 Zone dangereuse avec réapparition | `ZoneDangereuse` : le joueur perd une vie, le flash rouge et le son d'impact se déclenchent, il revient au `PointDepart`, le HUD est mis à jour. |
| 4.4 Objectif de collecte | `Blason` : l'objet disparaît, le compteur augmente, un son est joué, la progression s'affiche (texte et barre). La porte reste invisible jusqu'au dernier Blason. |
| 4.5 Porte conditionnelle | `PorteSortie` : charge la scène suivante du build (Niveau 1) ou affiche la victoire s'il n'y en a pas (Niveau 2). Elle vérifie que l'objectif est atteint. |
| 4.6 Interface adaptée | Niveau actuel, vies, Blasons collectés / objectif, barre de progression, panneaux de victoire et de défaite. |
| 4.7 Ambiance et effets sonores | Ambiance en boucle, sons de collecte, de dégâts, d'ouverture de porte, de victoire et de défaite (générés par le code, voir `AudioJeu`). |
| 4.8 Difficulté supérieure | Parcours en L avec couloir étroit, deux types d'ennemis, zones de piques, 6 Blasons au lieu de 3. |

## Structure du projet

```
Assets/
  Animations/     Animator et clips Idle / Move du chevalier
  Audio/
  Palettes/       Tile Palette
  Prefabs/
  Scenes/         Niveau1, Niveau2
  Scripts/
  Sprites/        Knight, Dragon, Items, Traps, Background, Terrain (tileset)
```

### Scripts

| Script | Rôle | Origine |
|---|---|---|
| `MouvementChevalier` | Déplacement en 4 directions, animation Idle/Move | Fourni (`MouvementRobot`), adapté |
| `GestionJeu` | Vies, compteur de Blasons, victoire, défaite, interface | Fourni, adapté (Blasons) |
| `AudioJeu` | Sons générés par le code | Fourni, adapté (ambiance réglable) |
| `EnnemiMobile` | Patrouille, détection et poursuite, dégâts | Fourni, modifié |
| `EffetAttaqueEnnemi` | Flash orange et pulsation de l'ennemi | Fourni |
| `EffetDegatsJoueur` | Clignotement et flash rouge du joueur | Fourni, adapté |
| `ZoneDangereuse` | Zone de piques : perte de vie et réapparition | Fourni, modifié |
| `PorteSortie` | Porte de sortie : transition de niveau ou victoire | Créé |
| `Blason` | Objet à collecter | Créé |
| `CameraSuivi` | La caméra suit le joueur avec un lissage | Créé |

Scripts présents dans le projet mais non utilisés dans les scènes actuelles : `MinuterieJeu`, `ObstacleDangereux`, `SecousseCamera`.

### Modifications apportées aux scripts fournis

- **`MouvementChevalier`** : la méthode `LimiterPosition()` (limite de zone avec `Mathf.Clamp`) n'est plus appelée. Elle entrait en conflit avec la vraie collision des murs (tremblement ou éjection du joueur).
- **`EnnemiMobile`** : sans point de patrouille, l'ennemi s'arrête au lieu de conserver sa dernière vitesse (sinon il glissait tout droit après avoir perdu le joueur).
- **`ZoneDangereuse`** : ajout de l'appel à `GestionJeu.PerdreVie()`. Le script ne faisait que replacer le joueur, sans perte de vie ni effet.
- **`AudioJeu`** : la fréquence et la pulsation de l'ambiance sont maintenant des champs de l'Inspector. Le Niveau 1 utilise 110 Hz (pulsation 0,5) et le Niveau 2 utilise 147 Hz (pulsation 1), pour une ambiance plus tendue. Les fréquences graves d'origine (55 Hz) étaient presque inaudibles sur des haut-parleurs d'ordinateur portable.
- **`PorteSortie`** : utilise la fonction `ChargerSceneSuivante()` de l'énoncé (`SceneManager` et `buildIndex + 1`). Si aucune scène ne suit dans le build, elle déclenche la victoire.

## Détails techniques

- **Décor** : une `Grid` avec des Tilemaps séparés. Les couches qui bloquent (`Mur`, `Obstacle`) ont un `Tilemap Collider 2D`, un `Rigidbody 2D` (Static) et un `Composite Collider 2D` (Composite Operation = Merge). Les tuiles d'`Obstacle` ont une forme de collision personnalisée (Custom Physics Shape) pour suivre leur dessin.
- **Colliders des personnages** : dimensionnés sur la silhouette visible du sprite et non sur son canevas, qui contient une grande marge transparente. Sans cela, le joueur s'arrêtait à distance des murs.
- **Ennemis** : chacun a deux colliders, un solide (respecte les murs) et un Trigger (inflige les dégâts). Les deux sont centrés sur le pivot, car le sprite est retourné avec `flipX`.
- **Transition de niveau** : basée sur l'ordre des scènes dans le build. Les vies et le compteur repartent à zéro dans chaque niveau.
- **Animations** : Animator à deux états (Idle, Move) piloté par le paramètre booléen `EnMouvement`, sans temps de sortie (`Has Exit Time` désactivé).
- **Audio** : tous les sons sont générés par le code (ondes sinusoïdales), aucun fichier audio n'est importé.

## Sources des assets

| Asset | Source | Licence |
|---|---|---|
| Tileset du décor (`Terrain (16x16)`), fruits (Blasons) et piques (`Traps`) | *Pixel Adventure 1* par **Pixel Frog** — https://pixelfrog-assets.itch.io/pixel-adventure-1 | CC0 selon la page de l'auteur. Une copie sur OpenGameArt indique CC-BY 4.0 : l'auteur est donc crédité ici par précaution. |
| Fond d'arrière-plan (grotte aux cristaux) | *Crystal Cave Background* par **fellfeline** — https://www.deviantart.com/fellfeline/art/Crystal-Cave-Background-881500860 | CC BY-NC-ND 3.0 (usage non commercial, attribution, sans modification). Le fichier image n'a pas été modifié, il est seulement placé et mis à l'échelle dans Unity. |
| Sprites du chevalier (« Knight Files ») et des dragons (« Dragon Warrior ») | Présents dans le dépôt du projet d'inspiration (voir plus bas) | Auteur et licence d'origine non identifiés |
| Sons | Générés par le code (`AudioJeu.cs`) | — |

## Inspiration

Ce projet s'inspire du projet Unity-Platformer-Episode-15 (https://github.com/nickbota/Unity-Platformer-Episode-15.git), un jeu de plateforme. J'en ai réutilisé uniquement des sprites (chevalier et dragons). Son code (saut, système de salles, ennemis) n'a **pas** été réutilisé : la logique du jeu est celle du Robot Collecteur, adaptée à mon thème.

## Aide reçue

- **Claude (Anthropic)**, pour la structure du jeu, la rédaction de ce README et le débogage de certaines parties du jeu.
- **Scripts de base fournis par l'enseignant** (adaptés à mon thème) et **documents de cours** : Cours 2 (sprites, animations, Tilemap), Cours 3-4 (mécaniques et boucle de jeu, Robot Collecteur), Module 5 (interface, audio, finition), énoncés du mini-projet.

J'ai testé, compris et adapté tout le code remis.

## Tests effectués

Parcours complet depuis le Niveau 1 : déplacement et collisions, animations, collecte et HUD, apparition de la porte, chargement du Niveau 2, comportements des deux dragons, délai entre dégâts, zones de piques, victoire, défaite, sons, et Console sans erreur.

## Limites connues et améliorations possibles

- L'icône des Blasons est un placeholder (des cerises du pack Pixel Adventure), faute d'un sprite de cristal libre de droits.
- Les dragons utilisent un sprite fixe (les animations d'idle et de marche sont importées mais pas encore branchées).
- La barre de progression n'a pas de visuel adapté au thème.
- Il n'y a pas de menu principal ni de bouton pour rejouer.

## Git

Une branche par étape : `niveau1`, `niveau2`, fusionnées dans `main`. Les commits suivent l'avancement (décor Tilemap, animations, HUD, ennemis, zones dangereuses, transition entre niveaux, audio, documentation).