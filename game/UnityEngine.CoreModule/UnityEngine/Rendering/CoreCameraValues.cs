using System;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	// Token: 0x020003F0 RID: 1008
	[UsedByNativeCode]
	internal struct CoreCameraValues : IEquatable<CoreCameraValues>
	{
		// Token: 0x0600223E RID: 8766 RVA: 0x00038BDC File Offset: 0x00036DDC
		public bool Equals(CoreCameraValues other)
		{
			return this.filterMode == other.filterMode && this.cullingMask == other.cullingMask && this.instanceID == other.instanceID;
		}

		// Token: 0x0600223F RID: 8767 RVA: 0x00038C1C File Offset: 0x00036E1C
		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is CoreCameraValues && this.Equals((CoreCameraValues)obj);
		}

		// Token: 0x06002240 RID: 8768 RVA: 0x00038C54 File Offset: 0x00036E54
		public override int GetHashCode()
		{
			int num = this.filterMode;
			num = (num * 397 ^ (int)this.cullingMask);
			return num * 397 ^ this.instanceID;
		}

		// Token: 0x06002241 RID: 8769 RVA: 0x00038C90 File Offset: 0x00036E90
		public static bool operator ==(CoreCameraValues left, CoreCameraValues right)
		{
			return left.Equals(right);
		}

		// Token: 0x06002242 RID: 8770 RVA: 0x00038CAC File Offset: 0x00036EAC
		public static bool operator !=(CoreCameraValues left, CoreCameraValues right)
		{
			return !left.Equals(right);
		}

		// Token: 0x04000C7A RID: 3194
		private int filterMode;

		// Token: 0x04000C7B RID: 3195
		private uint cullingMask;

		// Token: 0x04000C7C RID: 3196
		private int instanceID;
	}
}
