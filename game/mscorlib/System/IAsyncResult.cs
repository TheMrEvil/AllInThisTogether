using System;
using System.Threading;

namespace System
{
	/// <summary>Represents the status of an asynchronous operation.</summary>
	// Token: 0x02000137 RID: 311
	public interface IAsyncResult
	{
		/// <summary>Gets a value that indicates whether the asynchronous operation has completed.</summary>
		/// <returns>
		///   <see langword="true" /> if the operation is complete; otherwise, <see langword="false" />.</returns>
		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x06000BE4 RID: 3044
		bool IsCompleted { get; }

		/// <summary>Gets a <see cref="T:System.Threading.WaitHandle" /> that is used to wait for an asynchronous operation to complete.</summary>
		/// <returns>A <see cref="T:System.Threading.WaitHandle" /> that is used to wait for an asynchronous operation to complete.</returns>
		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x06000BE5 RID: 3045
		WaitHandle AsyncWaitHandle { get; }

		/// <summary>Gets a user-defined object that qualifies or contains information about an asynchronous operation.</summary>
		/// <returns>A user-defined object that qualifies or contains information about an asynchronous operation.</returns>
		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000BE6 RID: 3046
		object AsyncState { get; }

		/// <summary>Gets a value that indicates whether the asynchronous operation completed synchronously.</summary>
		/// <returns>
		///   <see langword="true" /> if the asynchronous operation completed synchronously; otherwise, <see langword="false" />.</returns>
		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x06000BE7 RID: 3047
		bool CompletedSynchronously { get; }
	}
}
