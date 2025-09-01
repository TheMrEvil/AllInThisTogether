using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace UnityEngine.UIElements.StyleSheets
{
	// Token: 0x02000370 RID: 880
	[DebuggerDisplay("id = {id}, keyword = {keyword}, number = {number}, boolean = {boolean}, color = {color}, object = {resource}")]
	[StructLayout(LayoutKind.Explicit)]
	internal struct StyleValue
	{
		// Token: 0x04000E24 RID: 3620
		[FieldOffset(0)]
		public StylePropertyId id;

		// Token: 0x04000E25 RID: 3621
		[FieldOffset(4)]
		public StyleKeyword keyword;

		// Token: 0x04000E26 RID: 3622
		[FieldOffset(8)]
		public float number;

		// Token: 0x04000E27 RID: 3623
		[FieldOffset(8)]
		public Length length;

		// Token: 0x04000E28 RID: 3624
		[FieldOffset(8)]
		public Color color;

		// Token: 0x04000E29 RID: 3625
		[FieldOffset(8)]
		public GCHandle resource;
	}
}
