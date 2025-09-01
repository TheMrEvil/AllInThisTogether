using System;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Runtime.ConstrainedExecution;
using System.Security;

namespace System.Runtime.CompilerServices
{
	/// <summary>Provides methods that the binary rewriter uses to handle contract failures.</summary>
	// Token: 0x02000819 RID: 2073
	public static class ContractHelper
	{
		/// <summary>Used by the binary rewriter to activate the default failure behavior.</summary>
		/// <param name="failureKind">One of the enumeration values that specifies the type of failure.</param>
		/// <param name="userMessage">Additional user information.</param>
		/// <param name="conditionText">The description of the condition that caused the failure.</param>
		/// <param name="innerException">The inner exception that caused the current exception.</param>
		/// <returns>A null reference (<see langword="Nothing" /> in Visual Basic) if the event was handled and should not trigger a failure; otherwise, returns the localized failure message.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="failureKind" /> is not a valid <see cref="T:System.Diagnostics.Contracts.ContractFailureKind" /> value.</exception>
		// Token: 0x06004659 RID: 18009 RVA: 0x000E5ED0 File Offset: 0x000E40D0
		[DebuggerNonUserCode]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public static string RaiseContractFailedEvent(ContractFailureKind failureKind, string userMessage, string conditionText, Exception innerException)
		{
			string result = "Contract failed";
			ContractHelper.RaiseContractFailedEventImplementation(failureKind, userMessage, conditionText, innerException, ref result);
			return result;
		}

		/// <summary>Triggers the default failure behavior.</summary>
		/// <param name="kind">One of the enumeration values that specifies the type of failure.</param>
		/// <param name="displayMessage">The message to display.</param>
		/// <param name="userMessage">Additional user information.</param>
		/// <param name="conditionText">The description of the condition that caused the failure.</param>
		/// <param name="innerException">The inner exception that caused the current exception.</param>
		// Token: 0x0600465A RID: 18010 RVA: 0x000E5EEF File Offset: 0x000E40EF
		[DebuggerNonUserCode]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public static void TriggerFailure(ContractFailureKind kind, string displayMessage, string userMessage, string conditionText, Exception innerException)
		{
			ContractHelper.TriggerFailureImplementation(kind, displayMessage, userMessage, conditionText, innerException);
		}

		// Token: 0x0600465B RID: 18011 RVA: 0x000E5EFC File Offset: 0x000E40FC
		[DebuggerNonUserCode]
		[SecuritySafeCritical]
		private static void RaiseContractFailedEventImplementation(ContractFailureKind failureKind, string userMessage, string conditionText, Exception innerException, ref string resultFailureMessage)
		{
			if (failureKind < ContractFailureKind.Precondition || failureKind > ContractFailureKind.Assume)
			{
				throw new ArgumentException(Environment.GetResourceString("Illegal enum value: {0}.", new object[]
				{
					failureKind
				}), "failureKind");
			}
			string text = "contract failed.";
			ContractFailedEventArgs contractFailedEventArgs = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			string text2;
			try
			{
				text = ContractHelper.GetDisplayMessage(failureKind, userMessage, conditionText);
				EventHandler<ContractFailedEventArgs> eventHandler = ContractHelper.contractFailedEvent;
				if (eventHandler != null)
				{
					contractFailedEventArgs = new ContractFailedEventArgs(failureKind, text, conditionText, innerException);
					foreach (EventHandler<ContractFailedEventArgs> eventHandler2 in eventHandler.GetInvocationList())
					{
						try
						{
							eventHandler2(null, contractFailedEventArgs);
						}
						catch (Exception thrownDuringHandler)
						{
							contractFailedEventArgs.thrownDuringHandler = thrownDuringHandler;
							contractFailedEventArgs.SetUnwind();
						}
					}
					if (contractFailedEventArgs.Unwind)
					{
						if (Environment.IsCLRHosted)
						{
							ContractHelper.TriggerCodeContractEscalationPolicy(failureKind, text, conditionText, innerException);
						}
						if (innerException == null)
						{
							innerException = contractFailedEventArgs.thrownDuringHandler;
						}
						throw new ContractException(failureKind, text, userMessage, conditionText, innerException);
					}
				}
			}
			finally
			{
				if (contractFailedEventArgs != null && contractFailedEventArgs.Handled)
				{
					text2 = null;
				}
				else
				{
					text2 = text;
				}
			}
			resultFailureMessage = text2;
		}

		// Token: 0x0600465C RID: 18012 RVA: 0x000E6008 File Offset: 0x000E4208
		[DebuggerNonUserCode]
		[SecuritySafeCritical]
		private static void TriggerFailureImplementation(ContractFailureKind kind, string displayMessage, string userMessage, string conditionText, Exception innerException)
		{
			if (Environment.IsCLRHosted)
			{
				ContractHelper.TriggerCodeContractEscalationPolicy(kind, displayMessage, conditionText, innerException);
				throw new ContractException(kind, displayMessage, userMessage, conditionText, innerException);
			}
			if (!Environment.UserInteractive)
			{
				throw new ContractException(kind, displayMessage, userMessage, conditionText, innerException);
			}
			string resourceString = Environment.GetResourceString(ContractHelper.GetResourceNameForFailure(kind, false));
			Assert.Fail(conditionText, displayMessage, resourceString, -2146233022, StackTrace.TraceFormat.Normal, 2);
		}

		// Token: 0x1400001B RID: 27
		// (add) Token: 0x0600465D RID: 18013 RVA: 0x000E6064 File Offset: 0x000E4264
		// (remove) Token: 0x0600465E RID: 18014 RVA: 0x000E60BC File Offset: 0x000E42BC
		internal static event EventHandler<ContractFailedEventArgs> InternalContractFailed
		{
			[SecurityCritical]
			add
			{
				RuntimeHelpers.PrepareContractedDelegate(value);
				object obj = ContractHelper.lockObject;
				lock (obj)
				{
					ContractHelper.contractFailedEvent = (EventHandler<ContractFailedEventArgs>)Delegate.Combine(ContractHelper.contractFailedEvent, value);
				}
			}
			[SecurityCritical]
			remove
			{
				object obj = ContractHelper.lockObject;
				lock (obj)
				{
					ContractHelper.contractFailedEvent = (EventHandler<ContractFailedEventArgs>)Delegate.Remove(ContractHelper.contractFailedEvent, value);
				}
			}
		}

		// Token: 0x0600465F RID: 18015 RVA: 0x000E6110 File Offset: 0x000E4310
		private static string GetResourceNameForFailure(ContractFailureKind failureKind, bool withCondition = false)
		{
			string result;
			switch (failureKind)
			{
			case ContractFailureKind.Precondition:
				result = (withCondition ? "Precondition failed: {0}" : "Precondition failed.");
				break;
			case ContractFailureKind.Postcondition:
				result = (withCondition ? "Postcondition failed: {0}" : "Postcondition failed.");
				break;
			case ContractFailureKind.PostconditionOnException:
				result = (withCondition ? "Postcondition failed after throwing an exception: {0}" : "Postcondition failed after throwing an exception.");
				break;
			case ContractFailureKind.Invariant:
				result = (withCondition ? "Invariant failed: {0}" : "Invariant failed.");
				break;
			case ContractFailureKind.Assert:
				result = (withCondition ? "Assertion failed: {0}" : "Assertion failed.");
				break;
			case ContractFailureKind.Assume:
				result = (withCondition ? "Assumption failed: {0}" : "Assumption failed.");
				break;
			default:
				Contract.Assume(false, "Unreachable code");
				result = "Assumption failed.";
				break;
			}
			return result;
		}

		// Token: 0x06004660 RID: 18016 RVA: 0x000E61C0 File Offset: 0x000E43C0
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		private static string GetDisplayMessage(ContractFailureKind failureKind, string userMessage, string conditionText)
		{
			string resourceNameForFailure = ContractHelper.GetResourceNameForFailure(failureKind, !string.IsNullOrEmpty(conditionText));
			string resourceString;
			if (!string.IsNullOrEmpty(conditionText))
			{
				resourceString = Environment.GetResourceString(resourceNameForFailure, new object[]
				{
					conditionText
				});
			}
			else
			{
				resourceString = Environment.GetResourceString(resourceNameForFailure);
			}
			if (!string.IsNullOrEmpty(userMessage))
			{
				return resourceString + "  " + userMessage;
			}
			return resourceString;
		}

		// Token: 0x06004661 RID: 18017 RVA: 0x000E6218 File Offset: 0x000E4418
		[SecuritySafeCritical]
		[DebuggerNonUserCode]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		private static void TriggerCodeContractEscalationPolicy(ContractFailureKind failureKind, string message, string conditionText, Exception innerException)
		{
			string exceptionAsString = null;
			if (innerException != null)
			{
				exceptionAsString = innerException.ToString();
			}
			Environment.TriggerCodeContractFailure(failureKind, message, conditionText, exceptionAsString);
		}

		// Token: 0x06004662 RID: 18018 RVA: 0x000E623A File Offset: 0x000E443A
		// Note: this type is marked as 'beforefieldinit'.
		static ContractHelper()
		{
		}

		// Token: 0x04002D53 RID: 11603
		private static volatile EventHandler<ContractFailedEventArgs> contractFailedEvent;

		// Token: 0x04002D54 RID: 11604
		private static readonly object lockObject = new object();

		// Token: 0x04002D55 RID: 11605
		internal const int COR_E_CODECONTRACTFAILED = -2146233022;
	}
}
