using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Unity.Profiling.LowLevel.Unsafe
{
	// Token: 0x02000054 RID: 84
	[StructLayout(LayoutKind.Explicit, Size = 24)]
	public readonly struct ProfilerCategoryDescription
	{
		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000134 RID: 308 RVA: 0x00003147 File Offset: 0x00001347
		public string Name
		{
			get
			{
				return ProfilerUnsafeUtility.Utf8ToString(this.NameUtf8, this.NameUtf8Len);
			}
		}

		// Token: 0x0400015C RID: 348
		[FieldOffset(0)]
		public readonly ushort Id;

		// Token: 0x0400015D RID: 349
		[FieldOffset(2)]
		public readonly ushort Flags;

		// Token: 0x0400015E RID: 350
		[FieldOffset(4)]
		public readonly Color32 Color;

		// Token: 0x0400015F RID: 351
		[FieldOffset(8)]
		private readonly int reserved0;

		// Token: 0x04000160 RID: 352
		[FieldOffset(12)]
		public readonly int NameUtf8Len;

		// Token: 0x04000161 RID: 353
		[FieldOffset(16)]
		public unsafe readonly byte* NameUtf8;
	}
}
