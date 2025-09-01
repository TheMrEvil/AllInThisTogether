using System;

namespace System.Diagnostics.Tracing
{
	/// <summary>Specifies how to format the value of a user-defined type and can be used to override the default formatting for a field.</summary>
	// Token: 0x020009F2 RID: 2546
	public enum EventFieldFormat
	{
		/// <summary>Boolean</summary>
		// Token: 0x0400381E RID: 14366
		Boolean = 3,
		/// <summary>Default.</summary>
		// Token: 0x0400381F RID: 14367
		Default = 0,
		/// <summary>Hexadecimal.</summary>
		// Token: 0x04003820 RID: 14368
		Hexadecimal = 4,
		/// <summary>HResult.</summary>
		// Token: 0x04003821 RID: 14369
		HResult = 15,
		/// <summary>JSON.</summary>
		// Token: 0x04003822 RID: 14370
		Json = 12,
		/// <summary>String.</summary>
		// Token: 0x04003823 RID: 14371
		String = 2,
		/// <summary>XML.</summary>
		// Token: 0x04003824 RID: 14372
		Xml = 11
	}
}
