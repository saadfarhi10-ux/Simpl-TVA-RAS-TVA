; ============================================================
; Script Inno Setup - Simpl-TVA
; ============================================================
; Ce script package :
;   - l'application Simpl-TVA (exe + dépendances)
;   - le script de création de base de données
;   - les 3 modèles CSV (Client / Fournisseur / Non-Résident)
;
; INSTRUCTIONS AVANT DE COMPILER :
; 1. Recompile ton projet en mode RELEASE dans Visual Studio
;    (menu déroulant en haut de VS : passer de "Debug" à "Release", puis Rebuild)
; 2. Place ce fichier .iss à la racine de ton dossier projet
;    "JEUILLET 2026_SIMPLTVA_SAAD_FARHI" (au même niveau que Simpl-TVA.sln
;    et les 3 fichiers .csv)
; 3. Vérifie que les chemins ci-dessous (section [Files]) correspondent
;    bien à ton arborescence - ajuste si besoin
; 4. Ouvre ce fichier avec Inno Setup Compiler et clique "Compile" (F9)
; 5. Le fichier final sera dans un dossier "Output" à côté de ce script
; ============================================================

#define MyAppName "Simpl-TVA"
#define MyAppVersion "1.0"
#define MyAppPublisher "Progiciel System"
#define MyAppExeName "Simpl-TVA.exe"

[Setup]
AppId={{8B2E5A1C-9F3D-4B7E-A123-SIMPLTVA0001}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
AppPublisher={#MyAppPublisher}
DefaultDirName={autopf}\{#MyAppName}
DefaultGroupName={#MyAppName}
DisableProgramGroupPage=yes
OutputDir=Output
OutputBaseFilename=SimplTVA_Setup
Compression=lzma
SolidCompression=yes
WizardStyle=modern
; Demande les droits admin (nécessaire pour écrire dans Program Files)
PrivilegesRequired=admin

[Languages]
Name: "french"; MessagesFile: "compiler:Languages\French.isl"

[Tasks]
Name: "desktopicon"; Description: "Créer une icône sur le Bureau"; GroupDescription: "Icônes supplémentaires :"

[Files]
; --- Application : exe + dépendances (dossier bin\Release après compilation) ---
Source: "Simpl-TVA\bin\Release\Simpl-TVA.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "Simpl-TVA\bin\Release\Simpl-TVA.exe.config"; DestDir: "{app}"; Flags: ignoreversion
Source: "Simpl-TVA\bin\Release\Simpl-TVA.xml"; DestDir: "{app}"; Flags: ignoreversion skipifsourcedoesntexist
Source: "Simpl-TVA\bin\Release\Ionic.Zip.dll"; DestDir: "{app}"; Flags: ignoreversion skipifsourcedoesntexist
Source: "Simpl-TVA\bin\Release\MBGlassButton.dll"; DestDir: "{app}"; Flags: ignoreversion skipifsourcedoesntexist
Source: "Simpl-TVA\bin\Release\updater.exe"; DestDir: "{app}"; Flags: ignoreversion skipifsourcedoesntexist
; Ajoute ici toute autre DLL présente dans ton dossier bin\Release si nécessaire :
; Source: "Simpl-TVA\bin\Release\NomDeLaDll.dll"; DestDir: "{app}"; Flags: ignoreversion

; --- Script de création de la base de données ---
Source: "Simpl-TVA\bin\Release\QueryInstallDBContent.txt"; DestDir: "{app}"; Flags: ignoreversion skipifsourcedoesntexist

; --- Les 3 modèles CSV (à la racine du projet) ---
Source: "TVA_NON_RESIDENTS_GRILLE_SIMPLE.csv"; DestDir: "{app}"; Flags: ignoreversion
Source: "TVA_RAS_TEST_06_2026CLIENT.csv"; DestDir: "{app}"; Flags: ignoreversion
Source: "TVA_RAS_TEST_06_2026FOURNISSEUR.csv"; DestDir: "{app}"; Flags: ignoreversion

[Icons]
Name: "{group}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"
Name: "{group}\Désinstaller {#MyAppName}"; Filename: "{uninstallexe}"
Name: "{autodesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: desktopicon

[Run]
Filename: "{app}\{#MyAppExeName}"; Description: "Lancer {#MyAppName}"; Flags: nowait postinstall skipifsilent
