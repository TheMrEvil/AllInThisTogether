using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Mono
{
	// Token: 0x02000054 RID: 84
	internal struct MonoAssemblyName
	{
		// Token: 0x04000DF1 RID: 3569
		private const int MONO_PUBLIC_KEY_TOKEN_LENGTH = 17;

		// Token: 0x04000DF2 RID: 3570
		internal IntPtr name;

		// Token: 0x04000DF3 RID: 3571
		internal IntPtr culture;

		// Token: 0x04000DF4 RID: 3572
		internal IntPtr hash_value;

		// Token: 0x04000DF5 RID: 3573
		internal IntPtr public_key;

		// Token: 0x04000DF6 RID: 3574
		[FixedBuffer(typeof(byte), 17)]
		internal MonoAssemblyName.<public_key_token>e__FixedBuffer public_key_token;

		// Token: 0x04000DF7 RID: 3575
		internal uint hash_alg;

		// Token: 0x04000DF8 RID: 3576
		internal uint hash_len;

		// Token: 0x04000DF9 RID: 3577
		internal uint flags;

		// Token: 0x04000DFA RID: 3578
		internal ushort major;

		// Token: 0x04000DFB RID: 3579
		internal ushort minor;

		// Token: 0x04000DFC RID: 3580
		internal ushort build;

		// Token: 0x04000DFD RID: 3581
		internal ushort revision;

		// Token: 0x04000DFE RID: 3582
		internal ushort arch;

		// Token: 0x02000055 RID: 85
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, Size = 17)]
		public struct <public_key_token>e__FixedBuffer
		{
			// Token: 0x04000DFF RID: 3583
			public byte FixedElementField;
		}
	}
}
