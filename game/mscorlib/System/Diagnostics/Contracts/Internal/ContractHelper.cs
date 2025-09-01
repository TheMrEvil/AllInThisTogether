using System;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;

namespace System.Diagnostics.Contracts.Internal
{
	/// <summary>Provides methods that the binary rewriter uses to handle contract failures.</summary>
	// Token: 0x020009D3 RID: 2515
	[Obsolete("Use the ContractHelper class in the System.Runtime.CompilerServices namespace instead.")]
	public static class ContractHelper
	{
		/// <summary>Used by the binary rewriter to activate the default failure behavior.</summary>
		/// <param name="failureKind">The type of failure.</param>
		/// <param name="userMessage">Additional user information.</param>
		/// <param name="conditionText">The description of the condition that caused the failure.</param>
		/// <param name="innerException">The inner exception that caused the current exception.</param>
		/// <returns>A null reference (<see langword="Nothing" /> in Visual Basic) if the event was handled and should not trigger a failure; otherwise, returns the localized failure message.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="failureKind" /> is not a valid <see cref="T:System.Diagnostics.Contracts.ContractFailureKind" /> value.</exception>
		// Token: 0x06005A3A RID: 23098 RVA: 0x00134251 File Offset: 0x00132451
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[DebuggerNonUserCode]
		public static string RaiseContractFailedEvent(ContractFailureKind failureKind, string userMessage, string conditionText, Exception innerException)
		{
			return ContractHelper.RaiseContractFailedEvent(failureKind, userMessage, conditionText, innerException);
		}

		/// <summary>Triggers the default failure behavior.</summary>
		/// <param name="kind">The type of failure.</param>
		/// <param name="displayMessage">The message to display.</param>
		/// <param name="userMessage">Additional user information.</param>
		/// <param name="conditionText">The description of the condition that caused the failure.</param>
		/// <param name="innerException">The inner exception that caused the current exception.</param>
		// Token: 0x06005A3B RID: 23099 RVA: 0x0013425C File Offset: 0x0013245C
		[DebuggerNonUserCode]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public static void TriggerFailure(ContractFailureKind kind, string displayMessage, string userMessage, string conditionText, Exception innerException)
		{
			ContractHelper.TriggerFailure(kind, displayMessage, userMessage, conditionText, innerException);
		}
	}
}
