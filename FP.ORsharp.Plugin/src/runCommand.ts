import * as vscode from 'vscode';

export function runTriggered(context: vscode.ExtensionContext) {
    var source = vscode.window.activeTextEditor?.document.fileName;
    if(!source) {
	    vscode.window.showInformationMessage('File not opened');
        return;
    }
    if(!source.endsWith('.osp')) {
        vscode.window.showInformationMessage('It works only with OSP files');
        return;
    }
	vscode.window.showInformationMessage('Running...');
    const terminal = vscode.window.createTerminal('ORsharp - Executor');
    terminal.show();
    [
        `c:\\orsharp\\FP.ORsharp.Worker.exe '${source}'`,
    ].forEach(line => terminal.sendText(line));
}
