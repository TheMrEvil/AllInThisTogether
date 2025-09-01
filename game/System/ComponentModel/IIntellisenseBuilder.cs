using System;

namespace System.ComponentModel
{
	/// <summary>Provides an interface to facilitate the retrieval of the builder's name and to display the builder.</summary>
	// Token: 0x020003B6 RID: 950
	public interface IIntellisenseBuilder
	{
		/// <summary>Gets a localized name.</summary>
		/// <returns>A localized name.</returns>
		// Token: 0x1700064F RID: 1615
		// (get) Token: 0x06001F13 RID: 7955
		string Name { get; }

		/// <summary>Shows the builder.</summary>
		/// <param name="language">The language service that is calling the builder.</param>
		/// <param name="value">The expression being edited.</param>
		/// <param name="newValue">The new value.</param>
		/// <returns>
		///   <see langword="true" /> if the value should be replaced with <paramref name="newValue" />; otherwise, <see langword="false" /> (if the user cancels, for example).</returns>
		// Token: 0x06001F14 RID: 7956
		bool Show(string language, string value, ref string newValue);
	}
}
