using System;
using System.Threading;

namespace System.ComponentModel
{
	/// <summary>Provides concurrency management for classes that support asynchronous method calls. This class cannot be inherited.</summary>
	// Token: 0x02000362 RID: 866
	public static class AsyncOperationManager
	{
		/// <summary>Returns an <see cref="T:System.ComponentModel.AsyncOperation" /> for tracking the duration of a particular asynchronous operation.</summary>
		/// <param name="userSuppliedState">An object used to associate a piece of client state, such as a task ID, with a particular asynchronous operation.</param>
		/// <returns>An <see cref="T:System.ComponentModel.AsyncOperation" /> that you can use to track the duration of an asynchronous method invocation.</returns>
		// Token: 0x06001CBA RID: 7354 RVA: 0x00067B07 File Offset: 0x00065D07
		public static AsyncOperation CreateOperation(object userSuppliedState)
		{
			return AsyncOperation.CreateOperation(userSuppliedState, AsyncOperationManager.SynchronizationContext);
		}

		/// <summary>Gets or sets the synchronization context for the asynchronous operation.</summary>
		/// <returns>The synchronization context for the asynchronous operation.</returns>
		// Token: 0x170005CA RID: 1482
		// (get) Token: 0x06001CBB RID: 7355 RVA: 0x00067B14 File Offset: 0x00065D14
		// (set) Token: 0x06001CBC RID: 7356 RVA: 0x00067B2C File Offset: 0x00065D2C
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static SynchronizationContext SynchronizationContext
		{
			get
			{
				if (SynchronizationContext.Current == null)
				{
					SynchronizationContext.SetSynchronizationContext(new SynchronizationContext());
				}
				return SynchronizationContext.Current;
			}
			set
			{
				SynchronizationContext.SetSynchronizationContext(value);
			}
		}
	}
}
