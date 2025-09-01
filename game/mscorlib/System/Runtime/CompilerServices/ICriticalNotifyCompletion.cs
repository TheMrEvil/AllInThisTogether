using System;

namespace System.Runtime.CompilerServices
{
	/// <summary>Represents an awaiter that schedules continuations when an await operation completes.</summary>
	// Token: 0x020007F7 RID: 2039
	public interface ICriticalNotifyCompletion : INotifyCompletion
	{
		/// <summary>Schedules the continuation action that's invoked when the instance completes.</summary>
		/// <param name="continuation">The action to invoke when the operation completes.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="continuation" /> argument is null (Nothing in Visual Basic).</exception>
		// Token: 0x06004609 RID: 17929
		void UnsafeOnCompleted(Action continuation);
	}
}
