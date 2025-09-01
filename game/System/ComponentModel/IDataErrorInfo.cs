using System;

namespace System.ComponentModel
{
	/// <summary>Provides the functionality to offer custom error information that a user interface can bind to.</summary>
	// Token: 0x020003B4 RID: 948
	public interface IDataErrorInfo
	{
		/// <summary>Gets the error message for the property with the given name.</summary>
		/// <param name="columnName">The name of the property whose error message to get.</param>
		/// <returns>The error message for the property. The default is an empty string ("").</returns>
		// Token: 0x1700064D RID: 1613
		string this[string columnName]
		{
			get;
		}

		/// <summary>Gets an error message indicating what is wrong with this object.</summary>
		/// <returns>An error message indicating what is wrong with this object. The default is an empty string ("").</returns>
		// Token: 0x1700064E RID: 1614
		// (get) Token: 0x06001F11 RID: 7953
		string Error { get; }
	}
}
