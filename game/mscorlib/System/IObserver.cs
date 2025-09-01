using System;

namespace System
{
	/// <summary>Provides a mechanism for receiving push-based notifications.</summary>
	/// <typeparam name="T">The object that provides notification information.</typeparam>
	// Token: 0x02000142 RID: 322
	public interface IObserver<in T>
	{
		/// <summary>Provides the observer with new data.</summary>
		/// <param name="value">The current notification information.</param>
		// Token: 0x06000C02 RID: 3074
		void OnNext(T value);

		/// <summary>Notifies the observer that the provider has experienced an error condition.</summary>
		/// <param name="error">An object that provides additional information about the error.</param>
		// Token: 0x06000C03 RID: 3075
		void OnError(Exception error);

		/// <summary>Notifies the observer that the provider has finished sending push-based notifications.</summary>
		// Token: 0x06000C04 RID: 3076
		void OnCompleted();
	}
}
