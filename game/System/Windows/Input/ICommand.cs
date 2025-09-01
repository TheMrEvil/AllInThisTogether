using System;
using System.Runtime.CompilerServices;

namespace System.Windows.Input
{
	/// <summary>Defines a command.</summary>
	// Token: 0x0200017A RID: 378
	[TypeForwardedFrom("PresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35")]
	public interface ICommand
	{
		/// <summary>Defines the method that determines whether the command can execute in its current state.</summary>
		/// <param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to <see langword="null" />.</param>
		/// <returns>
		///   <see langword="true" /> if this command can be executed; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000A18 RID: 2584
		bool CanExecute(object parameter);

		/// <summary>Defines the method to be called when the command is invoked.</summary>
		/// <param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to <see langword="null" />.</param>
		// Token: 0x06000A19 RID: 2585
		void Execute(object parameter);

		/// <summary>Occurs when changes occur that affect whether or not the command should execute.</summary>
		// Token: 0x14000013 RID: 19
		// (add) Token: 0x06000A1A RID: 2586
		// (remove) Token: 0x06000A1B RID: 2587
		event EventHandler CanExecuteChanged;
	}
}
