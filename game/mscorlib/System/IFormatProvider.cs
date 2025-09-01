using System;

namespace System
{
	/// <summary>Provides a mechanism for retrieving an object to control formatting.</summary>
	// Token: 0x0200013F RID: 319
	public interface IFormatProvider
	{
		/// <summary>Returns an object that provides formatting services for the specified type.</summary>
		/// <param name="formatType">An object that specifies the type of format object to return.</param>
		/// <returns>An instance of the object specified by <paramref name="formatType" />, if the <see cref="T:System.IFormatProvider" /> implementation can supply that type of object; otherwise, <see langword="null" />.</returns>
		// Token: 0x06000BFF RID: 3071
		object GetFormat(Type formatType);
	}
}
