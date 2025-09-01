using System;
using System.Runtime.InteropServices;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000221 RID: 545
	[UsedByNativeCode]
	[StructLayout(LayoutKind.Sequential)]
	public class TrackedReference
	{
		// Token: 0x0600178D RID: 6029 RVA: 0x00008CBB File Offset: 0x00006EBB
		protected TrackedReference()
		{
		}

		// Token: 0x0600178E RID: 6030 RVA: 0x0002622C File Offset: 0x0002442C
		public static bool operator ==(TrackedReference x, TrackedReference y)
		{
			bool flag = y == null && x == null;
			bool result;
			if (flag)
			{
				result = true;
			}
			else
			{
				bool flag2 = y == null;
				if (flag2)
				{
					result = (x.m_Ptr == IntPtr.Zero);
				}
				else
				{
					bool flag3 = x == null;
					if (flag3)
					{
						result = (y.m_Ptr == IntPtr.Zero);
					}
					else
					{
						result = (x.m_Ptr == y.m_Ptr);
					}
				}
			}
			return result;
		}

		// Token: 0x0600178F RID: 6031 RVA: 0x000262A0 File Offset: 0x000244A0
		public static bool operator !=(TrackedReference x, TrackedReference y)
		{
			return !(x == y);
		}

		// Token: 0x06001790 RID: 6032 RVA: 0x000262BC File Offset: 0x000244BC
		public override bool Equals(object o)
		{
			return o as TrackedReference == this;
		}

		// Token: 0x06001791 RID: 6033 RVA: 0x000262DC File Offset: 0x000244DC
		public override int GetHashCode()
		{
			return (int)this.m_Ptr;
		}

		// Token: 0x06001792 RID: 6034 RVA: 0x000262FC File Offset: 0x000244FC
		public static implicit operator bool(TrackedReference exists)
		{
			return exists != null;
		}

		// Token: 0x04000814 RID: 2068
		internal IntPtr m_Ptr;
	}
}
