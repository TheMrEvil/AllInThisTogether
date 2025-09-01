using System;

namespace System
{
	/// <summary>Encapsulates a method that has no parameters and returns a value of the type specified by the <typeparamref name="TResult" /> parameter.</summary>
	/// <typeparam name="TResult">The type of the return value of the method that this delegate encapsulates.</typeparam>
	/// <returns>The return value of the method that this delegate encapsulates.</returns>
	// Token: 0x020000E5 RID: 229
	// (Invoke) Token: 0x060006B7 RID: 1719
	public delegate TResult Func<out TResult>();
}
