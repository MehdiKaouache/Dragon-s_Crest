# Dragon's Crest

Mini-projet 2D — Cégep Marie-Victorin, 420-511-MV, automne 2026
Enseignant : Sofiane Faidi

## Description
Dragon's Crest est un jeu 2D vu de dessus dans lequel un chevalier explore le repaire
abandonné d'anciens dragons à la recherche de cristaux appelés les Blasons. Le joueur
se déplace en 4 directions, collecte les 3 Blasons dispersés dans la salle, puis
atteint la porte de sortie pour terminer le niveau 1.

## Inspiration
Ce projet est un mix de deux sources :
- La structure et la logique de jeu (déplacement top-down sans saut/gravité, gestion
  des blasons/vies, porte de sortie) suivent le "Robot Collecteur", le projet fil rouge
  officiel du cours.
- L'inspiration visuelle et thématique (chevalier, donjon) vient du projet
  Unity-Platformer-Episode-15 (https://github.com/nickbota/Unity-Platformer-Episode-15.git),
  dont j'ai réutilisé certains sprites (voir section Sources des assets ci-dessous).
  Le code de ce projet (mouvement avec saut, système de salles/portes, ennemis) n'a
  PAS été réutilisé, seulement des sprites libres de droits qu'il contenait — la logique
  du jeu reste celle du Robot Collecteur, adaptée à mon thème.

## Contrôles
- Déplacement : flèches directionnelles ou WASD

## Objectif
Collecter les 3 Blasons pour activer la porte de sortie, puis l'atteindre pour gagner.

## Sources des assets
- Sprites du chevalier : pack "Knight Files" (via Unity-Platformer-Episode-15)
- Tileset du décor (sol, murs, porte) : "Pixel Adventure 1" par Pixel Frog, licence CC0
  (via Unity-Platformer-Episode-15)
- Sprite des Blasons : "Cherries" du pack "Pixel Adventure 1" (icône temporaire/placeholder,
  faute d'un sprite de cristal disponible dans les assets à disposition)
- Audio : généré procéduralement en code (aucun fichier audio externe), voir AudioJeu.cs
- Fond d'arrière-plan (grotte aux cristaux) : "Crystal Cave Background" par fellfeline
  (https://www.deviantart.com/fellfeline/art/Crystal-Cave-Background-881500860),
  licence CC BY-NC-ND 3.0

## Aide reçue
- Assistance de Claude (Anthropic) pour la structure du jeu, la rédaction de ce README et le débogage de certaines parties du jeu
- Scripts de base fournis par l'enseignant (adaptés au thème Dragon's Crest)
- Tutoriels du cours : Cours 2 (Sprites/Animations/Tilemap), Cours 3-4 (mécaniques,
  Robot Collecteur), Module 5 (Interface/Audio/Finition)