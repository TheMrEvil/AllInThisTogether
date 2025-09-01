using System;
using System.Runtime.InteropServices;
using UnityEngine.Scripting;

namespace Unity.Profiling.LowLevel.Unsafe
{
	// Token: 0x02000051 RID: 81
	[UsedByNativeCode]
	[StructLayout(LayoutKind.Explicit, Size = 24)]
	public readonly struct ProfilerRecorderDescription
	{
		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600011E RID: 286 RVA: 0x00002FC9 File Offset: 0x000011C9
		public ProfilerCategory Category
		{
			get
			{
				return this.category;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600011F RID: 287 RVA: 0x00002FD1 File Offset: 0x000011D1
		public MarkerFlags Flags
		{
			get
			{
				return this.flags;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000120 RID: 288 RVA: 0x00002FD9 File Offset: 0x000011D9
		public ProfilerMarkerDataType DataType
		{
			get
			{
				return this.dataType;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000121 RID: 289 RVA: 0x00002FE1 File Offset: 0x000011E1
		public ProfilerMarkerDataUnit UnitType
		{
			get
			{
				return this.unitType;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000122 RID: 290 RVA: 0x00002FE9 File Offset: 0x000011E9
		public int NameUtf8Len
		{
			get
			{
				return this.nameUtf8Len;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000123 RID: 291 RVA: 0x00002FF1 File Offset: 0x000011F1
		public unsafe byte* NameUtf8
		{
			get
			{
				return this.nameUtf8;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000124 RID: 292 RVA: 0x00002FF9 File Offset: 0x000011F9
		public string Name
		{
			get
			{
				return ProfilerUnsafeUtility.Utf8ToString(this.nameUtf8, this.nameUtf8Len);
			}
		}

		// Token: 0x0400014E RID: 334
		[FieldOffset(0)]
		private readonly ProfilerCategory category;

		// Token: 0x0400014F RID: 335
		[FieldOffset(2)]
		private readonly MarkerFlags flags;

		// Token: 0x04000150 RID: 336
		[FieldOffset(4)]
		private readonly ProfilerMarkerDataType dataType;

		// Token: 0x04000151 RID: 337
		[FieldOffset(5)]
		private readonly ProfilerMarkerDataUnit unitType;

		// Token: 0x04000152 RID: 338
		[FieldOffset(8)]
		private readonly int reserved0;

		// Token: 0x04000153 RID: 339
		[FieldOffset(12)]
		private readonly int nameUtf8Len;

		// Token: 0x04000154 RID: 340
		[FieldOffset(16)]
		private unsafe readonly byte* nameUtf8;
	}
}
