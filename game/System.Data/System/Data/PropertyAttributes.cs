using System;
using System.ComponentModel;

namespace System.Data
{
	/// <summary>Specifies the attributes of a property.</summary>
	// Token: 0x02000158 RID: 344
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Flags]
	[Obsolete("PropertyAttributes has been deprecated.  http://go.microsoft.com/fwlink/?linkid=14202")]
	public enum PropertyAttributes
	{
		/// <summary>The property is not supported by the provider.</summary>
		// Token: 0x04000BA3 RID: 2979
		NotSupported = 0,
		/// <summary>The user must specify a value for this property before the data source is initialized.</summary>
		// Token: 0x04000BA4 RID: 2980
		Required = 1,
		/// <summary>The user does not need to specify a value for this property before the data source is initialized.</summary>
		// Token: 0x04000BA5 RID: 2981
		Optional = 2,
		/// <summary>The user can read the property.</summary>
		// Token: 0x04000BA6 RID: 2982
		Read = 512,
		/// <summary>The user can write to the property.</summary>
		// Token: 0x04000BA7 RID: 2983
		Write = 1024
	}
}
