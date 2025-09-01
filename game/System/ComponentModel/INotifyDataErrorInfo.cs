using System;
using System.Collections;

namespace System.ComponentModel
{
	/// <summary>Defines members that data entity classes can implement to provide custom synchronous and asynchronous validation support.</summary>
	// Token: 0x02000400 RID: 1024
	public interface INotifyDataErrorInfo
	{
		/// <summary>Gets a value that indicates whether the entity has validation errors.</summary>
		/// <returns>
		///   <see langword="true" /> if the entity currently has validation errors; otherwise, <see langword="false" />.</returns>
		// Token: 0x170006D0 RID: 1744
		// (get) Token: 0x06002141 RID: 8513
		bool HasErrors { get; }

		/// <summary>Gets the validation errors for a specified property or for the entire entity.</summary>
		/// <param name="propertyName">The name of the property to retrieve validation errors for; or <see langword="null" /> or <see cref="F:System.String.Empty" />, to retrieve entity-level errors.</param>
		/// <returns>The validation errors for the property or entity.</returns>
		// Token: 0x06002142 RID: 8514
		IEnumerable GetErrors(string propertyName);

		/// <summary>Occurs when the validation errors have changed for a property or for the entire entity.</summary>
		// Token: 0x1400002D RID: 45
		// (add) Token: 0x06002143 RID: 8515
		// (remove) Token: 0x06002144 RID: 8516
		event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
	}
}
