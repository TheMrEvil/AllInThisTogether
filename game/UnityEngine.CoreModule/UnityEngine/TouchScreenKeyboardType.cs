using System;

namespace UnityEngine
{
	// Token: 0x02000255 RID: 597
	public enum TouchScreenKeyboardType
	{
		// Token: 0x04000886 RID: 2182
		Default,
		// Token: 0x04000887 RID: 2183
		ASCIICapable,
		// Token: 0x04000888 RID: 2184
		NumbersAndPunctuation,
		// Token: 0x04000889 RID: 2185
		URL,
		// Token: 0x0400088A RID: 2186
		NumberPad,
		// Token: 0x0400088B RID: 2187
		PhonePad,
		// Token: 0x0400088C RID: 2188
		NamePhonePad,
		// Token: 0x0400088D RID: 2189
		EmailAddress,
		// Token: 0x0400088E RID: 2190
		[Obsolete("Wii U is no longer supported as of Unity 2018.1.")]
		NintendoNetworkAccount,
		// Token: 0x0400088F RID: 2191
		Social,
		// Token: 0x04000890 RID: 2192
		Search,
		// Token: 0x04000891 RID: 2193
		DecimalPad,
		// Token: 0x04000892 RID: 2194
		OneTimeCode
	}
}
