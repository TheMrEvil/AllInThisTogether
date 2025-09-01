using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.XR.Tango
{
	// Token: 0x02000003 RID: 3
	[NativeHeader("ARScriptingClasses.h")]
	[UsedByNativeCode]
	internal struct PoseData
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public Quaternion rotation
		{
			get
			{
				return new Quaternion((float)this.orientation_x, (float)this.orientation_y, (float)this.orientation_z, (float)this.orientation_w);
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000002 RID: 2 RVA: 0x00002084 File Offset: 0x00000284
		public Vector3 position
		{
			get
			{
				return new Vector3((float)this.translation_x, (float)this.translation_y, (float)this.translation_z);
			}
		}

		// Token: 0x04000006 RID: 6
		public double orientation_x;

		// Token: 0x04000007 RID: 7
		public double orientation_y;

		// Token: 0x04000008 RID: 8
		public double orientation_z;

		// Token: 0x04000009 RID: 9
		public double orientation_w;

		// Token: 0x0400000A RID: 10
		public double translation_x;

		// Token: 0x0400000B RID: 11
		public double translation_y;

		// Token: 0x0400000C RID: 12
		public double translation_z;

		// Token: 0x0400000D RID: 13
		public PoseStatus statusCode;
	}
}
