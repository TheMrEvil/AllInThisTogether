using System;

namespace UnityEngine.Experimental.AI
{
	// Token: 0x0200001E RID: 30
	public struct PolygonId : IEquatable<PolygonId>
	{
		// Token: 0x06000169 RID: 361 RVA: 0x000030D0 File Offset: 0x000012D0
		public bool IsNull()
		{
			return this.polyRef == 0UL;
		}

		// Token: 0x0600016A RID: 362 RVA: 0x000030EC File Offset: 0x000012EC
		public static bool operator ==(PolygonId x, PolygonId y)
		{
			return x.polyRef == y.polyRef;
		}

		// Token: 0x0600016B RID: 363 RVA: 0x0000310C File Offset: 0x0000130C
		public static bool operator !=(PolygonId x, PolygonId y)
		{
			return x.polyRef != y.polyRef;
		}

		// Token: 0x0600016C RID: 364 RVA: 0x00003130 File Offset: 0x00001330
		public override int GetHashCode()
		{
			return this.polyRef.GetHashCode();
		}

		// Token: 0x0600016D RID: 365 RVA: 0x00003150 File Offset: 0x00001350
		public bool Equals(PolygonId rhs)
		{
			return rhs == this;
		}

		// Token: 0x0600016E RID: 366 RVA: 0x00003170 File Offset: 0x00001370
		public override bool Equals(object obj)
		{
			bool flag = obj == null || !(obj is PolygonId);
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				PolygonId x = (PolygonId)obj;
				result = (x == this);
			}
			return result;
		}

		// Token: 0x04000060 RID: 96
		internal ulong polyRef;
	}
}
