[Setup]
AppName=BeautyManager
AppVersion=1.0
AppPublisher=Clever
DefaultDirName={autopf}\BeautyManager
DefaultGroupName=BeautyManager
OutputDir=C:\Users\Clever\Desktop\BeautyManager\installer
OutputBaseFilename=BeautyManager_Setup_v1.0
SetupIconFile=C:\Users\Clever\Desktop\BeautyManager\icone.ico
Compression=lzma2
SolidCompression=yes
PrivilegesRequired=admin

[Languages]
Name: "french"; MessagesFile: "compiler:Languages\French.isl"

[Tasks]
Name: "desktopicon"; Description: "Créer un raccourci sur le Bureau"; GroupDescription: "Raccourcis"
Name: "startmenuicon"; Description: "Créer un raccourci dans le Menu Démarrer"; GroupDescription: "Raccourcis"

[Files]
Source: "C:\Users\Clever\Desktop\BeautyManager\output\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs

[Icons]
Name: "{autodesktop}\BeautyManager"; Filename: "{app}\BeautyManager.exe"; IconFilename: "{app}\BeautyManager.exe"; Tasks: desktopicon
Name: "{group}\BeautyManager"; Filename: "{app}\BeautyManager.exe"; Tasks: startmenuicon
Name: "{group}\Désinstaller BeautyManager"; Filename: "{uninstallexe}"

[Run]
Filename: "{app}\BeautyManager.exe"; Description: "Lancer BeautyManager"; Flags: nowait postinstall skipifsilent