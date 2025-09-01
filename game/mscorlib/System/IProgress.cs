using System;

namespace System
{
	/// <summary>Defines a provider for progress updates.</summary>
	/// <typeparam name="T">The type of progress update value.</typeparam>
	// Token: 0x02000143 RID: 323
	public interface IProgress<in T>
	{
		/// <summary>Reports a progress update.</summary>
		/// <param name="value">The value of the updated progress.</param>
		// Token: 0x06000C05 RID: 3077
		void Report(T value);
	}
}
