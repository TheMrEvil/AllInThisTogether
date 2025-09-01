using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.ExceptionServices;
using System.Security;

namespace System.Threading
{
	// Token: 0x020002C9 RID: 713
	internal struct ExecutionContextSwitcher
	{
		// Token: 0x06001EE6 RID: 7910 RVA: 0x00072B54 File Offset: 0x00070D54
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[HandleProcessCorruptedStateExceptions]
		internal bool UndoNoThrow()
		{
			try
			{
				this.Undo();
			}
			catch
			{
				return false;
			}
			return true;
		}

		// Token: 0x06001EE7 RID: 7911 RVA: 0x00072B84 File Offset: 0x00070D84
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[SecurityCritical]
		internal void Undo()
		{
			if (this.thread == null)
			{
				return;
			}
			Thread thread = this.thread;
			ExecutionContext.Reader executionContextReader = thread.GetExecutionContextReader();
			thread.SetExecutionContext(this.outerEC, this.outerECBelongsToScope);
			this.thread = null;
			ExecutionContext.OnAsyncLocalContextChanged(executionContextReader.DangerousGetRawExecutionContext(), this.outerEC.DangerousGetRawExecutionContext());
		}

		// Token: 0x04001AEE RID: 6894
		internal ExecutionContext.Reader outerEC;

		// Token: 0x04001AEF RID: 6895
		internal bool outerECBelongsToScope;

		// Token: 0x04001AF0 RID: 6896
		internal object hecsw;

		// Token: 0x04001AF1 RID: 6897
		internal Thread thread;
	}
}
