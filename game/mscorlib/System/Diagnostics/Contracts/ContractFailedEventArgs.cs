using System;
using System.Runtime.ConstrainedExecution;
using System.Security;

namespace System.Diagnostics.Contracts
{
	/// <summary>Provides methods and data for the <see cref="E:System.Diagnostics.Contracts.Contract.ContractFailed" /> event.</summary>
	// Token: 0x020009D1 RID: 2513
	public sealed class ContractFailedEventArgs : EventArgs
	{
		/// <summary>Provides data for the <see cref="E:System.Diagnostics.Contracts.Contract.ContractFailed" /> event.</summary>
		/// <param name="failureKind">One of the enumeration values that specifies the contract that failed.</param>
		/// <param name="message">The message for the event.</param>
		/// <param name="condition">The condition for the event.</param>
		/// <param name="originalException">The exception that caused the event.</param>
		// Token: 0x06005A29 RID: 23081 RVA: 0x00134100 File Offset: 0x00132300
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public ContractFailedEventArgs(ContractFailureKind failureKind, string message, string condition, Exception originalException)
		{
			this._failureKind = failureKind;
			this._message = message;
			this._condition = condition;
			this._originalException = originalException;
		}

		/// <summary>Gets the message that describes the <see cref="E:System.Diagnostics.Contracts.Contract.ContractFailed" /> event.</summary>
		/// <returns>The message that describes the event.</returns>
		// Token: 0x17000F57 RID: 3927
		// (get) Token: 0x06005A2A RID: 23082 RVA: 0x00134125 File Offset: 0x00132325
		public string Message
		{
			get
			{
				return this._message;
			}
		}

		/// <summary>Gets the condition for the failure of the contract.</summary>
		/// <returns>The condition for the failure.</returns>
		// Token: 0x17000F58 RID: 3928
		// (get) Token: 0x06005A2B RID: 23083 RVA: 0x0013412D File Offset: 0x0013232D
		public string Condition
		{
			get
			{
				return this._condition;
			}
		}

		/// <summary>Gets the type of contract that failed.</summary>
		/// <returns>One of the enumeration values that specifies the type of contract that failed.</returns>
		// Token: 0x17000F59 RID: 3929
		// (get) Token: 0x06005A2C RID: 23084 RVA: 0x00134135 File Offset: 0x00132335
		public ContractFailureKind FailureKind
		{
			get
			{
				return this._failureKind;
			}
		}

		/// <summary>Gets the original exception that caused the <see cref="E:System.Diagnostics.Contracts.Contract.ContractFailed" /> event.</summary>
		/// <returns>The exception that caused the event.</returns>
		// Token: 0x17000F5A RID: 3930
		// (get) Token: 0x06005A2D RID: 23085 RVA: 0x0013413D File Offset: 0x0013233D
		public Exception OriginalException
		{
			get
			{
				return this._originalException;
			}
		}

		/// <summary>Indicates whether the <see cref="E:System.Diagnostics.Contracts.Contract.ContractFailed" /> event has been handled.</summary>
		/// <returns>
		///   <see langword="true" /> if the event has been handled; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000F5B RID: 3931
		// (get) Token: 0x06005A2E RID: 23086 RVA: 0x00134145 File Offset: 0x00132345
		public bool Handled
		{
			get
			{
				return this._handled;
			}
		}

		/// <summary>Sets the <see cref="P:System.Diagnostics.Contracts.ContractFailedEventArgs.Handled" /> property to <see langword="true" />.</summary>
		// Token: 0x06005A2F RID: 23087 RVA: 0x0013414D File Offset: 0x0013234D
		[SecurityCritical]
		public void SetHandled()
		{
			this._handled = true;
		}

		/// <summary>Indicates whether the code contract escalation policy should be applied.</summary>
		/// <returns>
		///   <see langword="true" /> to apply the escalation policy; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17000F5C RID: 3932
		// (get) Token: 0x06005A30 RID: 23088 RVA: 0x00134156 File Offset: 0x00132356
		public bool Unwind
		{
			get
			{
				return this._unwind;
			}
		}

		/// <summary>Sets the <see cref="P:System.Diagnostics.Contracts.ContractFailedEventArgs.Unwind" /> property to <see langword="true" />.</summary>
		// Token: 0x06005A31 RID: 23089 RVA: 0x0013415E File Offset: 0x0013235E
		[SecurityCritical]
		public void SetUnwind()
		{
			this._unwind = true;
		}

		// Token: 0x040037B3 RID: 14259
		private ContractFailureKind _failureKind;

		// Token: 0x040037B4 RID: 14260
		private string _message;

		// Token: 0x040037B5 RID: 14261
		private string _condition;

		// Token: 0x040037B6 RID: 14262
		private Exception _originalException;

		// Token: 0x040037B7 RID: 14263
		private bool _handled;

		// Token: 0x040037B8 RID: 14264
		private bool _unwind;

		// Token: 0x040037B9 RID: 14265
		internal Exception thrownDuringHandler;
	}
}
