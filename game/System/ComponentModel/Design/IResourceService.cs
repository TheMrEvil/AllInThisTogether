using System;
using System.Globalization;
using System.Resources;

namespace System.ComponentModel.Design
{
	/// <summary>Provides an interface for designers to access resource readers and writers for specific <see cref="T:System.Globalization.CultureInfo" /> resource types.</summary>
	// Token: 0x02000467 RID: 1127
	public interface IResourceService
	{
		/// <summary>Locates the resource reader for the specified culture and returns it.</summary>
		/// <param name="info">The <see cref="T:System.Globalization.CultureInfo" /> of the resource for which to retrieve a resource reader.</param>
		/// <returns>An <see cref="T:System.Resources.IResourceReader" /> interface that contains the resources for the culture, or <see langword="null" /> if no resources for the culture exist.</returns>
		// Token: 0x06002466 RID: 9318
		IResourceReader GetResourceReader(CultureInfo info);

		/// <summary>Locates the resource writer for the specified culture and returns it.</summary>
		/// <param name="info">The <see cref="T:System.Globalization.CultureInfo" /> of the resource for which to create a resource writer.</param>
		/// <returns>An <see cref="T:System.Resources.IResourceWriter" /> interface for the specified culture.</returns>
		// Token: 0x06002467 RID: 9319
		IResourceWriter GetResourceWriter(CultureInfo info);
	}
}
