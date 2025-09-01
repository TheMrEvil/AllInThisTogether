using System;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x0200000B RID: 11
	[NativeType(CodegenOptions.Custom, "ScriptingJvalue")]
	[StructLayout(LayoutKind.Explicit)]
	public struct jvalue
	{
		// Token: 0x04000015 RID: 21
		[FieldOffset(0)]
		public bool z;

		// Token: 0x04000016 RID: 22
		[FieldOffset(0)]
		public sbyte b;

		// Token: 0x04000017 RID: 23
		[FieldOffset(0)]
		public char c;

		// Token: 0x04000018 RID: 24
		[FieldOffset(0)]
		public short s;

		// Token: 0x04000019 RID: 25
		[FieldOffset(0)]
		public int i;

		// Token: 0x0400001A RID: 26
		[FieldOffset(0)]
		public long j;

		// Token: 0x0400001B RID: 27
		[FieldOffset(0)]
		public float f;

		// Token: 0x0400001C RID: 28
		[FieldOffset(0)]
		public double d;

		// Token: 0x0400001D RID: 29
		[FieldOffset(0)]
		public IntPtr l;
	}
}
