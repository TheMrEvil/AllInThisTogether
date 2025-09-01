using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace UnityEngine.Experimental.AI
{
	// Token: 0x0200001F RID: 31
	public struct NavMeshLocation
	{
		// Token: 0x17000073 RID: 115
		// (get) Token: 0x0600016F RID: 367 RVA: 0x000031AF File Offset: 0x000013AF
		public readonly PolygonId polygon
		{
			[CompilerGenerated]
			get
			{
				return this.<polygon>k__BackingField;
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000170 RID: 368 RVA: 0x000031B7 File Offset: 0x000013B7
		public readonly Vector3 position
		{
			[CompilerGenerated]
			get
			{
				return this.<position>k__BackingField;
			}
		}

		// Token: 0x06000171 RID: 369 RVA: 0x000031BF File Offset: 0x000013BF
		internal NavMeshLocation(Vector3 position, PolygonId polygon)
		{
			this.position = position;
			this.polygon = polygon;
		}

		// Token: 0x04000061 RID: 97
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private readonly PolygonId <polygon>k__BackingField;

		// Token: 0x04000062 RID: 98
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private readonly Vector3 <position>k__BackingField;
	}
}
