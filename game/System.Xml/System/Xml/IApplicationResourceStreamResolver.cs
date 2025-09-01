using System;
using System.ComponentModel;
using System.IO;

namespace System.Xml
{
	/// <summary>Represents an application resource stream resolver.</summary>
	// Token: 0x020001E0 RID: 480
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Obsolete("This API supports the .NET Framework infrastructure and is not intended to be used directly from your code.", true)]
	public interface IApplicationResourceStreamResolver
	{
		/// <summary>Returns an application resource stream from the specified URI.</summary>
		/// <param name="relativeUri">The relative URI.</param>
		/// <returns>An application resource stream.</returns>
		// Token: 0x06001319 RID: 4889
		[Obsolete("This API supports the .NET Framework infrastructure and is not intended to be used directly from your code.", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		Stream GetApplicationResourceStream(Uri relativeUri);
	}
}
