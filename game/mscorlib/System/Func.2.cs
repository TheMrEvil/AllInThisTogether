using System;

namespace System
{
	/// <summary>Encapsulates a method that has one parameter and returns a value of the type specified by the <typeparamref name="TResult" /> parameter.</summary>
	/// <param name="arg">The parameter of the method that this delegate encapsulates.</param>
	/// <typeparam name="T">The type of the parameter of the method that this delegate encapsulates.</typeparam>
	/// <typeparam name="TResult">The type of the return value of the method that this delegate encapsulates.</typeparam>
	/// <returns>The return value of the method that this delegate encapsulates.</returns>
	// Token: 0x020000E6 RID: 230
	// (Invoke) Token: 0x060006BB RID: 1723
	public delegate TResult Func<in T, out TResult>(T arg);
}
