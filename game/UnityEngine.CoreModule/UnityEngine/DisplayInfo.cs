using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000128 RID: 296
	[UsedByNativeCode]
	[NativeType("Runtime/Graphics/DisplayInfo.h")]
	public struct DisplayInfo : IEquatable<DisplayInfo>
	{
		// Token: 0x06000831 RID: 2097 RVA: 0x0000C514 File Offset: 0x0000A714
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(DisplayInfo other)
		{
			return this.handle == other.handle && this.width == other.width && this.height == other.height && this.refreshRate.Equals(other.refreshRate) && this.workArea.Equals(other.workArea) && this.name == other.name;
		}

		// Token: 0x040003BB RID: 955
		[RequiredMember]
		internal ulong handle;

		// Token: 0x040003BC RID: 956
		[RequiredMember]
		public int width;

		// Token: 0x040003BD RID: 957
		[RequiredMember]
		public int height;

		// Token: 0x040003BE RID: 958
		[RequiredMember]
		public RefreshRate refreshRate;

		// Token: 0x040003BF RID: 959
		[RequiredMember]
		public RectInt workArea;

		// Token: 0x040003C0 RID: 960
		[RequiredMember]
		public string name;
	}
}
