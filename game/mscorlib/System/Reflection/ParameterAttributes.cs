using System;

namespace System.Reflection
{
	/// <summary>Defines the attributes that can be associated with a parameter. These are defined in CorHdr.h.</summary>
	// Token: 0x020008B5 RID: 2229
	[Flags]
	public enum ParameterAttributes
	{
		/// <summary>Specifies that there is no parameter attribute.</summary>
		// Token: 0x04002EFA RID: 12026
		None = 0,
		/// <summary>Specifies that the parameter is an input parameter.</summary>
		// Token: 0x04002EFB RID: 12027
		In = 1,
		/// <summary>Specifies that the parameter is an output parameter.</summary>
		// Token: 0x04002EFC RID: 12028
		Out = 2,
		/// <summary>Specifies that the parameter is a locale identifier (lcid).</summary>
		// Token: 0x04002EFD RID: 12029
		Lcid = 4,
		/// <summary>Specifies that the parameter is a return value.</summary>
		// Token: 0x04002EFE RID: 12030
		Retval = 8,
		/// <summary>Specifies that the parameter is optional.</summary>
		// Token: 0x04002EFF RID: 12031
		Optional = 16,
		/// <summary>Specifies that the parameter has a default value.</summary>
		// Token: 0x04002F00 RID: 12032
		HasDefault = 4096,
		/// <summary>Specifies that the parameter has field marshaling information.</summary>
		// Token: 0x04002F01 RID: 12033
		HasFieldMarshal = 8192,
		/// <summary>Reserved.</summary>
		// Token: 0x04002F02 RID: 12034
		Reserved3 = 16384,
		/// <summary>Reserved.</summary>
		// Token: 0x04002F03 RID: 12035
		Reserved4 = 32768,
		/// <summary>Specifies that the parameter is reserved.</summary>
		// Token: 0x04002F04 RID: 12036
		ReservedMask = 61440
	}
}
