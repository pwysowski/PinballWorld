# PinballWorld
Simple pinball game made as university assignment.

Made with Unity 2019.3.14f1

# Requirements (in Polish)

- oczekiwany rodzaj gry to pinball, tzn. gra ma stanowić symulację fizyczną klasycznej gry arcade;
- gra może być wykonana w grafice 2D lub 3D;
- gra musi wykorzystywać dotyk jako formę sterowania grą, np. panning, pinch to zoom;
- gra musi wykorzystywać akcelerometr (np. jako symulacja nudging-u);
- gra musi wykorzystywać logowanie do Google Play Services, w skrócie GPS (prowadzący dostarczy wymagane pęki kluczy do autoryzacji, proszę o maila w tej sprawie);
- gra musi wykorzystywać mechanizm osiągnięć GPS (proszę zaprogramować min. 10  5 osiągnięć),
- gra musi wykorzystywać mechanizm tablicy wyników GPS,
- gra musi wykorzystywać mechanizm zapisu metadanych użytkownika (np. do przechowywania postępu osiągnięć czy ustawień),
- gra powinna zawierać autorski schemat stołu - proszę o uwzględnienie elementów charakterystycznych dla gry typu pinball (ten artykuł zawiera ich listę);
- gra powinna zawierać efekty dźwiękowe;
- gra powinna zawierać min. 3 sceny: menu, właściwa gra oraz scena z listą autorów wraz z wykazem wykorzystanych elementów, które nie są pracą własną autorów;
- gra powinna być umieszczona na platformie GitLab lub GitHub (może być w formie prywatnego repozytorium z udostępnieniem repozytorium konkretnemu użytkownikowi, w tym celu załączam poniżej linki do moich profilów do wspomnianych serwisów);
proszę wykorzystać plik .gitignore przeznaczony dla Unity;
- w pliku README powinny znaleźć się wszystkie wymagania projektu (np. wersja Unity) oraz sposób uruchomienia gry (gdyby różnił się on od standardowego);

# Progress

- Added android configuration
- Added panning
- Added zoom in and zoom out
- Added paddle tap controls
- Added nudging
- Added GPGS support
- Added GPGS sign in
- Added leaderboards and achievements
- Added final graphics
- Added sounds
- Updated Readme

# How to build

- Before building remember to add keystore file
- Default keystore folder is Assets/Keystore
- You will have to enter password for keystore

# How to play

- To shoot plunger you have to swipe down in right corner of your screen
- Flippers will only move when the ball was shot with plunger
- To move flippers yopu have to touch left/right sides of screen
- To zoom in or zoom out use pinch gesture
- You can move camera only in PRE-GAME phase (when the ball is on plunger)
- To use nudging shake your phone

- Bumpers will give you points when hit
- Board bumpers will give you additional boost and even more points
- Floating wave area will give you points on exit (calculated with time)
