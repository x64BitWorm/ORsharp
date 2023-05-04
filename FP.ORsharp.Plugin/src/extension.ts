import * as vscode from 'vscode';
import { runTriggered } from './runCommand';
import { updateTriggered } from './updateCommand';

export function activate(context: vscode.ExtensionContext) {
	let command1 = vscode.commands.registerCommand('orsharper.run', () => runTriggered(context));
	let command2 = vscode.commands.registerCommand('orsharper.update', updateTriggered);
	context.subscriptions.push(command1);
	context.subscriptions.push(command2);
}

export function deactivate() {}
