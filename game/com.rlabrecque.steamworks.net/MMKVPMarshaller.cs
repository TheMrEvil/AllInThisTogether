using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000186 RID: 390
	public class MMKVPMarshaller
	{
		// Token: 0x060008D5 RID: 2261 RVA: 0x0000CF04 File Offset: 0x0000B104
		public MMKVPMarshaller(MatchMakingKeyValuePair_t[] filters)
		{
			if (filters == null)
			{
				return;
			}
			int num = Marshal.SizeOf(typeof(MatchMakingKeyValuePair_t));
			this.m_pNativeArray = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(IntPtr)) * filters.Length);
			this.m_pArrayEntries = Marshal.AllocHGlobal(num * filters.Length);
			for (int i = 0; i < filters.Length; i++)
			{
				Marshal.StructureToPtr<MatchMakingKeyValuePair_t>(filters[i], new IntPtr(this.m_pArrayEntries.ToInt64() + (long)(i * num)), false);
			}
			Marshal.WriteIntPtr(this.m_pNativeArray, this.m_pArrayEntries);
		}

		// Token: 0x060008D6 RID: 2262 RVA: 0x0000CF9C File Offset: 0x0000B19C
		~MMKVPMarshaller()
		{
			if (this.m_pArrayEntries != IntPtr.Zero)
			{
				Marshal.FreeHGlobal(this.m_pArrayEntries);
			}
			if (this.m_pNativeArray != IntPtr.Zero)
			{
				Marshal.FreeHGlobal(this.m_pNativeArray);
			}
		}

		// Token: 0x060008D7 RID: 2263 RVA: 0x0000CFFC File Offset: 0x0000B1FC
		public static implicit operator IntPtr(MMKVPMarshaller that)
		{
			return that.m_pNativeArray;
		}

		// Token: 0x04000A3F RID: 2623
		private IntPtr m_pNativeArray;

		// Token: 0x04000A40 RID: 2624
		private IntPtr m_pArrayEntries;
	}
}
