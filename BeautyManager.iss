[Setup]
AppName=BeautyManager
AppVersion=1.0
AppPublisher=Clever
DefaultDirName={autopf}\BeautyManager
DefaultGroupName=BeautyManager
OutputDir=C:\Users\Clever\Desktop\BeautyManager\installer
OutputBaseFilename=BeautyManager_Setup_v1.0
SetupIconFile=C:\Users\Clever\Desktop\BeautyManager\icone.ico
UninstallDisplayIcon={app}\BeautyManager.exe
Compression=lzma2
SolidCompression=yes
PrivilegesRequired=admin
ArchitecturesAllowed=x64
ArchitecturesInstallIn64BitMode=x64

[Languages]
Name: "french"; MessagesFile: "compiler:Languages\French.isl"

[Tasks]
Name: "desktopicon"; Description: "Créer un raccourci sur le Bureau"; GroupDescription: "Raccourcis"
Name: "startmenuicon"; Description: "Créer un raccourci dans le Menu Démarrer"; GroupDescription: "Raccourcis"

[Dirs]
Name: "{commonappdata}\BeautyManager"

[Files]
Source: "C:\Users\Clever\Desktop\BeautyManager\publish\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs
Source: "C:\mariadb-test\mariadb-10.11.19-winx64\*"; DestDir: "{app}\mariadb"; Excludes: "data\*"; Flags: ignoreversion recursesubdirs createallsubdirs
Source: "C:\BeautyManagerTest\seed.sql"; DestDir: "{app}"; Flags: ignoreversion

[Icons]
Name: "{autodesktop}\BeautyManager"; Filename: "{app}\BeautyManager.exe"; IconFilename: "{app}\BeautyManager.exe"; Tasks: desktopicon
Name: "{group}\BeautyManager"; Filename: "{app}\BeautyManager.exe"; Tasks: startmenuicon
Name: "{group}\Désinstaller BeautyManager"; Filename: "{uninstallexe}"

[Run]
Filename: "{app}\BeautyManager.exe"; Description: "Lancer BeautyManager"; Flags: nowait postinstall skipifsilent

[UninstallRun]
Filename: "{sys}\net.exe"; Parameters: "stop BeautyManagerDB"; Flags: runhidden; RunOnceId: "StopDb"
Filename: "{sys}\sc.exe"; Parameters: "delete BeautyManagerDB"; Flags: runhidden; RunOnceId: "DelDb"

[UninstallDelete]
Type: filesandordirs; Name: "{commonappdata}\BeautyManager"
Type: files; Name: "{app}\config.properties"

[Code]
const
  SvcName = 'BeautyManagerDB';
  DbPort  = '3309';
  RootPwd = 'BM_root_2026';
  AppUser = 'beautymanager';
  AppPwd  = 'BM_demo_2026';

function RunHidden(const Exe, Params: String): Integer;
var
  Code: Integer;
begin
  if not Exec(Exe, Params, '', SW_HIDE, ewWaitUntilTerminated, Code) then
    Code := -1;
  Result := Code;
end;

function DbReady: Boolean;
var
  I: Integer;
begin
  Result := False;
  for I := 1 to 30 do
  begin
    if RunHidden(ExpandConstant('{app}\mariadb\bin\mysqladmin.exe'),
         '--protocol=tcp -h 127.0.0.1 -P ' + DbPort + ' -u root -p' + RootPwd + ' ping') = 0 then
    begin
      Result := True;
      Exit;
    end;
    Sleep(1000);
  end;
end;

procedure ImportSql(const SqlFile: String);
begin
  RunHidden(ExpandConstant('{cmd}'),
    '/C ""' + ExpandConstant('{app}\mariadb\bin\mysql.exe') +
    '" --protocol=tcp -h 127.0.0.1 -P ' + DbPort + ' -u root -p' + RootPwd +
    ' --default-character-set=utf8mb4 < "' + SqlFile + '""');
end;

function SqlUser(const Host: String): String;
var
  Q: String;
begin
  Q := '''';
  Result := Q + AppUser + Q + '@' + Q + Host + Q;
end;

procedure CurStepChanged(CurStep: TSetupStep);
var
  DataDir, Cfg, Sql, InitFile, SeedFile, Q: String;
  Fresh: Boolean;
begin
  if CurStep <> ssPostInstall then Exit;

  Q := '''';
  DataDir := ExpandConstant('{commonappdata}\BeautyManager\data');
  InitFile := ExpandConstant('{app}\init.sql');
  SeedFile := ExpandConstant('{app}\seed.sql');

  { Fichier lu par l'application }
  Cfg := 'db.host=127.0.0.1' + #13#10 +
         'db.port=' + DbPort + #13#10 +
         'db.name=salonbeaute' + #13#10 +
         'db.user=' + AppUser + #13#10 +
         'db.password=' + AppPwd + #13#10;
  SaveStringToFile(ExpandConstant('{app}\config.properties'), Cfg, False);

  Fresh := not DirExists(DataDir + '\mysql');
  if Fresh then
  begin
    WizardForm.StatusLabel.Caption := 'Initialisation de la base de données...';
    RunHidden(ExpandConstant('{app}\mariadb\bin\mysql_install_db.exe'),
      '--datadir="' + DataDir + '" --service=' + SvcName +
      ' --port=' + DbPort + ' --password=' + RootPwd);
    { Plan B si le service n'a pas été créé }
    if RunHidden(ExpandConstant('{sys}\sc.exe'), 'query ' + SvcName) <> 0 then
      RunHidden(ExpandConstant('{app}\mariadb\bin\mysqld.exe'),
        '--install ' + SvcName + ' --defaults-file="' + DataDir + '\my.ini"');
  end;

  RunHidden(ExpandConstant('{sys}\net.exe'), 'start ' + SvcName);

  if not DbReady then
  begin
    MsgBox('La base de données n''a pas démarré. Relancez l''installateur ou consultez le service ' + SvcName + '.',
           mbError, MB_OK);
    Exit;
  end;

  if Fresh then
  begin
    WizardForm.StatusLabel.Caption := 'Importation des données de démonstration...';
    ImportSql(SeedFile);
  end;

  Sql := 'CREATE USER IF NOT EXISTS ' + SqlUser('localhost') + ' IDENTIFIED BY ' + Q + AppPwd + Q + ';' + #13#10 +
         'CREATE USER IF NOT EXISTS ' + SqlUser('127.0.0.1') + ' IDENTIFIED BY ' + Q + AppPwd + Q + ';' + #13#10 +
         'GRANT ALL PRIVILEGES ON salonbeaute.* TO ' + SqlUser('localhost') + ';' + #13#10 +
         'GRANT ALL PRIVILEGES ON salonbeaute.* TO ' + SqlUser('127.0.0.1') + ';' + #13#10 +
         'FLUSH PRIVILEGES;' + #13#10;
  SaveStringToFile(InitFile, Sql, False);
  ImportSql(InitFile);

  DeleteFile(InitFile);
  DeleteFile(SeedFile);
end;