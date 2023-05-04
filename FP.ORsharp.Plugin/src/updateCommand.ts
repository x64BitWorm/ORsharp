import * as vscode from 'vscode';

export function updateTriggered() {
	vscode.window.showInformationMessage('Updating...');
    const terminal = vscode.window.createTerminal('ORsharp - Updater');
    terminal.show();
    [
        "# Removing old version",
        "remove-item -Recurse -Force -ErrorAction Ignore 'c:\\orsharp'",
        "# Preparing folder for new version",
        "New-Item -Path 'c:\\' -Name 'orsharp' -ItemType 'directory'",
        "# Downloading new version",
        "Invoke-WebRequest -Uri 'https://github.com/x64BitWorm/ORsharp/raw/main/bin.zip' -OutFile 'c:\\orsharp\\bin.zip'",
        "# Expanding archive",
        "Expand-Archive -LiteralPath 'c:\\orsharp\\bin.zip' -DestinationPath 'c:\\orsharp'",
        "# Installed",
        "sleep 3",
        "exit"
    ].forEach(line => terminal.sendText(line));
}
