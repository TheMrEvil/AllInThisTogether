using System;

namespace System.Net
{
	/// <summary>Provides the base interface to load and execute scripts for automatic proxy detection.</summary>
	// Token: 0x0200069F RID: 1695
	public interface IWebProxyScript
	{
		/// <summary>Closes a script.</summary>
		// Token: 0x06003648 RID: 13896
		void Close();

		/// <summary>Loads a script.</summary>
		/// <param name="scriptLocation">Internal only.</param>
		/// <param name="script">Internal only.</param>
		/// <param name="helperType">Internal only.</param>
		/// <returns>A <see cref="T:System.Boolean" /> indicating whether the script was successfully loaded.</returns>
		// Token: 0x06003649 RID: 13897
		bool Load(Uri scriptLocation, string script, Type helperType);

		/// <summary>Runs a script.</summary>
		/// <param name="url">Internal only.</param>
		/// <param name="host">Internal only.</param>
		/// <returns>A <see cref="T:System.String" />.  
		///  An internal-only value returned.</returns>
		// Token: 0x0600364A RID: 13898
		string Run(string url, string host);
	}
}
