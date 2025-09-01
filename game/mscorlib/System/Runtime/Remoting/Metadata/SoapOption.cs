using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata
{
	/// <summary>Specifies the SOAP configuration options for use with the <see cref="T:System.Runtime.Remoting.Metadata.SoapTypeAttribute" /> class.</summary>
	// Token: 0x020005DB RID: 1499
	[ComVisible(true)]
	[Flags]
	[Serializable]
	public enum SoapOption
	{
		/// <summary>The default option indicating that no extra options are selected.</summary>
		// Token: 0x0400260C RID: 9740
		None = 0,
		/// <summary>Indicates that type will always be included on SOAP elements. This option is useful when performing SOAP interop with SOAP implementations that require types on all elements.</summary>
		// Token: 0x0400260D RID: 9741
		AlwaysIncludeTypes = 1,
		/// <summary>Indicates that the output SOAP string type in a SOAP Envelope is using the <see langword="XSD" /> prefix, and that the resulting XML does not have an ID attribute for the string.</summary>
		// Token: 0x0400260E RID: 9742
		XsdString = 2,
		/// <summary>Indicates that SOAP will be generated without references. This option is currently not implemented.</summary>
		// Token: 0x0400260F RID: 9743
		EmbedAll = 4,
		/// <summary>Public reserved option for temporary interop conditions; the use will change.</summary>
		// Token: 0x04002610 RID: 9744
		Option1 = 8,
		/// <summary>Public reserved option for temporary interop conditions; the use will change.</summary>
		// Token: 0x04002611 RID: 9745
		Option2 = 16
	}
}
