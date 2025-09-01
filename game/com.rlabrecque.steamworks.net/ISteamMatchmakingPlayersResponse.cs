using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200018A RID: 394
	public class ISteamMatchmakingPlayersResponse
	{
		// Token: 0x060008E5 RID: 2277 RVA: 0x0000D2F8 File Offset: 0x0000B4F8
		public ISteamMatchmakingPlayersResponse(ISteamMatchmakingPlayersResponse.AddPlayerToList onAddPlayerToList, ISteamMatchmakingPlayersResponse.PlayersFailedToRespond onPlayersFailedToRespond, ISteamMatchmakingPlayersResponse.PlayersRefreshComplete onPlayersRefreshComplete)
		{
			if (onAddPlayerToList == null || onPlayersFailedToRespond == null || onPlayersRefreshComplete == null)
			{
				throw new ArgumentNullException();
			}
			this.m_AddPlayerToList = onAddPlayerToList;
			this.m_PlayersFailedToRespond = onPlayersFailedToRespond;
			this.m_PlayersRefreshComplete = onPlayersRefreshComplete;
			this.m_VTable = new ISteamMatchmakingPlayersResponse.VTable
			{
				m_VTAddPlayerToList = new ISteamMatchmakingPlayersResponse.InternalAddPlayerToList(this.InternalOnAddPlayerToList),
				m_VTPlayersFailedToRespond = new ISteamMatchmakingPlayersResponse.InternalPlayersFailedToRespond(this.InternalOnPlayersFailedToRespond),
				m_VTPlayersRefreshComplete = new ISteamMatchmakingPlayersResponse.InternalPlayersRefreshComplete(this.InternalOnPlayersRefreshComplete)
			};
			this.m_pVTable = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(ISteamMatchmakingPlayersResponse.VTable)));
			Marshal.StructureToPtr<ISteamMatchmakingPlayersResponse.VTable>(this.m_VTable, this.m_pVTable, false);
			this.m_pGCHandle = GCHandle.Alloc(this.m_pVTable, GCHandleType.Pinned);
		}

		// Token: 0x060008E6 RID: 2278 RVA: 0x0000D3B4 File Offset: 0x0000B5B4
		~ISteamMatchmakingPlayersResponse()
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

		// Token: 0x060008E7 RID: 2279 RVA: 0x0000D410 File Offset: 0x0000B610
		private void InternalOnAddPlayerToList(IntPtr thisptr, IntPtr pchName, int nScore, float flTimePlayed)
		{
			this.m_AddPlayerToList(InteropHelp.PtrToStringUTF8(pchName), nScore, flTimePlayed);
		}

		// Token: 0x060008E8 RID: 2280 RVA: 0x0000D426 File Offset: 0x0000B626
		private void InternalOnPlayersFailedToRespond(IntPtr thisptr)
		{
			this.m_PlayersFailedToRespond();
		}

		// Token: 0x060008E9 RID: 2281 RVA: 0x0000D433 File Offset: 0x0000B633
		private void InternalOnPlayersRefreshComplete(IntPtr thisptr)
		{
			this.m_PlayersRefreshComplete();
		}

		// Token: 0x060008EA RID: 2282 RVA: 0x0000D440 File Offset: 0x0000B640
		public static explicit operator IntPtr(ISteamMatchmakingPlayersResponse that)
		{
			return that.m_pGCHandle.AddrOfPinnedObject();
		}

		// Token: 0x04000A4C RID: 2636
		private ISteamMatchmakingPlayersResponse.VTable m_VTable;

		// Token: 0x04000A4D RID: 2637
		private IntPtr m_pVTable;

		// Token: 0x04000A4E RID: 2638
		private GCHandle m_pGCHandle;

		// Token: 0x04000A4F RID: 2639
		private ISteamMatchmakingPlayersResponse.AddPlayerToList m_AddPlayerToList;

		// Token: 0x04000A50 RID: 2640
		private ISteamMatchmakingPlayersResponse.PlayersFailedToRespond m_PlayersFailedToRespond;

		// Token: 0x04000A51 RID: 2641
		private ISteamMatchmakingPlayersResponse.PlayersRefreshComplete m_PlayersRefreshComplete;

		// Token: 0x020001DE RID: 478
		// (Invoke) Token: 0x06000BB8 RID: 3000
		public delegate void AddPlayerToList(string pchName, int nScore, float flTimePlayed);

		// Token: 0x020001DF RID: 479
		// (Invoke) Token: 0x06000BBC RID: 3004
		public delegate void PlayersFailedToRespond();

		// Token: 0x020001E0 RID: 480
		// (Invoke) Token: 0x06000BC0 RID: 3008
		public delegate void PlayersRefreshComplete();

		// Token: 0x020001E1 RID: 481
		// (Invoke) Token: 0x06000BC4 RID: 3012
		[UnmanagedFunctionPointer(CallingConvention.ThisCall)]
		public delegate void InternalAddPlayerToList(IntPtr thisptr, IntPtr pchName, int nScore, float flTimePlayed);

		// Token: 0x020001E2 RID: 482
		// (Invoke) Token: 0x06000BC8 RID: 3016
		[UnmanagedFunctionPointer(CallingConvention.ThisCall)]
		public delegate void InternalPlayersFailedToRespond(IntPtr thisptr);

		// Token: 0x020001E3 RID: 483
		// (Invoke) Token: 0x06000BCC RID: 3020
		[UnmanagedFunctionPointer(CallingConvention.ThisCall)]
		public delegate void InternalPlayersRefreshComplete(IntPtr thisptr);

		// Token: 0x020001E4 RID: 484
		[StructLayout(LayoutKind.Sequential)]
		private class VTable
		{
			// Token: 0x06000BCF RID: 3023 RVA: 0x000109E4 File Offset: 0x0000EBE4
			public VTable()
			{
			}

			// Token: 0x04000B17 RID: 2839
			[NonSerialized]
			[MarshalAs(UnmanagedType.FunctionPtr)]
			public ISteamMatchmakingPlayersResponse.InternalAddPlayerToList m_VTAddPlayerToList;

			// Token: 0x04000B18 RID: 2840
			[NonSerialized]
			[MarshalAs(UnmanagedType.FunctionPtr)]
			public ISteamMatchmakingPlayersResponse.InternalPlayersFailedToRespond m_VTPlayersFailedToRespond;

			// Token: 0x04000B19 RID: 2841
			[NonSerialized]
			[MarshalAs(UnmanagedType.FunctionPtr)]
			public ISteamMatchmakingPlayersResponse.InternalPlayersRefreshComplete m_VTPlayersRefreshComplete;
		}
	}
}
