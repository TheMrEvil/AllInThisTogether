using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200018B RID: 395
	public class ISteamMatchmakingRulesResponse
	{
		// Token: 0x060008EB RID: 2283 RVA: 0x0000D450 File Offset: 0x0000B650
		public ISteamMatchmakingRulesResponse(ISteamMatchmakingRulesResponse.RulesResponded onRulesResponded, ISteamMatchmakingRulesResponse.RulesFailedToRespond onRulesFailedToRespond, ISteamMatchmakingRulesResponse.RulesRefreshComplete onRulesRefreshComplete)
		{
			if (onRulesResponded == null || onRulesFailedToRespond == null || onRulesRefreshComplete == null)
			{
				throw new ArgumentNullException();
			}
			this.m_RulesResponded = onRulesResponded;
			this.m_RulesFailedToRespond = onRulesFailedToRespond;
			this.m_RulesRefreshComplete = onRulesRefreshComplete;
			this.m_VTable = new ISteamMatchmakingRulesResponse.VTable
			{
				m_VTRulesResponded = new ISteamMatchmakingRulesResponse.InternalRulesResponded(this.InternalOnRulesResponded),
				m_VTRulesFailedToRespond = new ISteamMatchmakingRulesResponse.InternalRulesFailedToRespond(this.InternalOnRulesFailedToRespond),
				m_VTRulesRefreshComplete = new ISteamMatchmakingRulesResponse.InternalRulesRefreshComplete(this.InternalOnRulesRefreshComplete)
			};
			this.m_pVTable = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(ISteamMatchmakingRulesResponse.VTable)));
			Marshal.StructureToPtr<ISteamMatchmakingRulesResponse.VTable>(this.m_VTable, this.m_pVTable, false);
			this.m_pGCHandle = GCHandle.Alloc(this.m_pVTable, GCHandleType.Pinned);
		}

		// Token: 0x060008EC RID: 2284 RVA: 0x0000D50C File Offset: 0x0000B70C
		~ISteamMatchmakingRulesResponse()
		{
			if (this.m_pVTable != IntPtr.Zero)
			{
				Marshal.FreeHGlobal(this.m_pVTable);
			}
			if (this.m_pGCHandle.IsAllocated)
			{
				this.m_pGCHandle.Free();
			}
		}

		// Token: 0x060008ED RID: 2285 RVA: 0x0000D568 File Offset: 0x0000B768
		private void InternalOnRulesResponded(IntPtr thisptr, IntPtr pchRule, IntPtr pchValue)
		{
			this.m_RulesResponded(InteropHelp.PtrToStringUTF8(pchRule), InteropHelp.PtrToStringUTF8(pchValue));
		}

		// Token: 0x060008EE RID: 2286 RVA: 0x0000D581 File Offset: 0x0000B781
		private void InternalOnRulesFailedToRespond(IntPtr thisptr)
		{
			this.m_RulesFailedToRespond();
		}

		// Token: 0x060008EF RID: 2287 RVA: 0x0000D58E File Offset: 0x0000B78E
		private void InternalOnRulesRefreshComplete(IntPtr thisptr)
		{
			this.m_RulesRefreshComplete();
		}

		// Token: 0x060008F0 RID: 2288 RVA: 0x0000D59B File Offset: 0x0000B79B
		public static explicit operator IntPtr(ISteamMatchmakingRulesResponse that)
		{
			return that.m_pGCHandle.AddrOfPinnedObject();
		}

		// Token: 0x04000A52 RID: 2642
		private ISteamMatchmakingRulesResponse.VTable m_VTable;

		// Token: 0x04000A53 RID: 2643
		private IntPtr m_pVTable;

		// Token: 0x04000A54 RID: 2644
		private GCHandle m_pGCHandle;

		// Token: 0x04000A55 RID: 2645
		private ISteamMatchmakingRulesResponse.RulesResponded m_RulesResponded;

		// Token: 0x04000A56 RID: 2646
		private ISteamMatchmakingRulesResponse.RulesFailedToRespond m_RulesFailedToRespond;

		// Token: 0x04000A57 RID: 2647
		private ISteamMatchmakingRulesResponse.RulesRefreshComplete m_RulesRefreshComplete;

		// Token: 0x020001E5 RID: 485
		// (Invoke) Token: 0x06000BD1 RID: 3025
		public delegate void RulesResponded(string pchRule, string pchValue);

		// Token: 0x020001E6 RID: 486
		// (Invoke) Token: 0x06000BD5 RID: 3029
		public delegate void RulesFailedToRespond();

		// Token: 0x020001E7 RID: 487
		// (Invoke) Token: 0x06000BD9 RID: 3033
		public delegate void RulesRefreshComplete();

		// Token: 0x020001E8 RID: 488
		// (Invoke) Token: 0x06000BDD RID: 3037
		[UnmanagedFunctionPointer(CallingConvention.ThisCall)]
		public delegate void InternalRulesResponded(IntPtr thisptr, IntPtr pchRule, IntPtr pchValue);

		// Token: 0x020001E9 RID: 489
		// (Invoke) Token: 0x06000BE1 RID: 3041
		[UnmanagedFunctionPointer(CallingConvention.ThisCall)]
		public delegate void InternalRulesFailedToRespond(IntPtr thisptr);

		// Token: 0x020001EA RID: 490
		// (Invoke) Token: 0x06000BE5 RID: 3045
		[UnmanagedFunctionPointer(CallingConvention.ThisCall)]
		public delegate void InternalRulesRefreshComplete(IntPtr thisptr);

		// Token: 0x020001EB RID: 491
		[StructLayout(LayoutKind.Sequential)]
		private class VTable
		{
			// Token: 0x06000BE8 RID: 3048 RVA: 0x000109EC File Offset: 0x0000EBEC
			public VTable()
			{
			}

			// Token: 0x04000B1A RID: 2842
			[NonSerialized]
			[MarshalAs(UnmanagedType.FunctionPtr)]
			public ISteamMatchmakingRulesResponse.InternalRulesResponded m_VTRulesResponded;

			// Token: 0x04000B1B RID: 2843
			[NonSerialized]
			[MarshalAs(UnmanagedType.FunctionPtr)]
			public ISteamMatchmakingRulesResponse.InternalRulesFailedToRespond m_VTRulesFailedToRespond;

			// Token: 0x04000B1C RID: 2844
			[NonSerialized]
			[MarshalAs(UnmanagedType.FunctionPtr)]
			public ISteamMatchmakingRulesResponse.InternalRulesRefreshComplete m_VTRulesRefreshComplete;
		}
	}
}
