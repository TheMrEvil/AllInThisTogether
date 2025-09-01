using System;

namespace System.ComponentModel
{
	/// <summary>Provides the ability to retrieve the full nested name of a component.</summary>
	// Token: 0x020003B9 RID: 953
	public interface INestedSite : ISite, IServiceProvider
	{
		/// <summary>Gets the full name of the component in this site.</summary>
		/// <returns>The full name of the component in this site.</returns>
		// Token: 0x17000652 RID: 1618
		// (get) Token: 0x06001F18 RID: 7960
		string FullName { get; }
	}
}
