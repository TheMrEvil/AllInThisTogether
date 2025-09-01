using System;

namespace System.Runtime.CompilerServices
{
	/// <summary>Represents an operation that schedules continuations when it completes.</summary>
	// Token: 0x020007F6 RID: 2038
	public interface INotifyCompletion
	{
		/// <summary>Schedules the continuation action that's invoked when the instance completes.</summary>
		/// <param name="continuation">The action to invoke when the operation completes.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="continuation" /> argument is null (Nothing in Visual Basic).</exception>
		// Token: 0x06004608 RID: 17928
		void OnCompleted(Action continuation);
	}
}
