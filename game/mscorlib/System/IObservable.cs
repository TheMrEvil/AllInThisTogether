using System;

namespace System
{
	/// <summary>Defines a provider for push-based notification.</summary>
	/// <typeparam name="T">The object that provides notification information.</typeparam>
	// Token: 0x02000141 RID: 321
	public interface IObservable<out T>
	{
		/// <summary>Notifies the provider that an observer is to receive notifications.</summary>
		/// <param name="observer">The object that is to receive notifications.</param>
		/// <returns>A reference to an interface that allows observers to stop receiving notifications before the provider has finished sending them.</returns>
		// Token: 0x06000C01 RID: 3073
		IDisposable Subscribe(IObserver<T> observer);
	}
}
