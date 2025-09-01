using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000188 RID: 392
	public class ISteamMatchmakingServerListResponse
	{
		// Token: 0x060008DA RID: 2266 RVA: 0x0000D010 File Offset: 0x0000B210
		public ISteamMatchmakingServerListResponse(ISteamMatchmakingServerListResponse.ServerResponded onServerResponded, ISteamMatchmakingServerListResponse.ServerFailedToRespond onServerFailedToRespond, ISteamMatchmakingServerListResponse.RefreshComplete onRefreshComplete)
		{
			if (onServerResponded == null || onServerFailedToRespond == null || onRefreshComplete == null)
			{
				throw new ArgumentNullException();
			}
			this.m_ServerResponded = onServerResponded;
			this.m_ServerFailedToRespond = onServerFailedToRespond;
			this.m_RefreshComplete = onRefreshComplete;
			this.m_VTable = new ISteamMatchmakingServerListResponse.VTable
			{
				m_VTServerResponded = new ISteamMatchmakingServerListResponse.InternalServerResponded(this.InternalOnServerResponded),
				m_VTServerFailedToRespond = new ISteamMatchmakingServerListResponse.InternalServerFailedToRespond(this.InternalOnServerFailedToRespond),
				m_VTRefreshComplete = new ISteamMatchmakingServerListResponse.InternalRefreshComplete(this.InternalOnRefreshComplete)
			};
			this.m_pVTable = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(ISteamMatchmakingServerListResponse.VTable)));
			Marshal.StructureToPtr<ISteamMatchmakingServerListResponse.VTable>(this.m_VTable, this.m_pVTable, false);
			this.m_pGCHandle = GCHandle.Alloc(this.m_pVTable, GCHandleType.Pinned);
		}

		// Token: 0x060008DB RID: 2267 RVA: 0x0000D0CC File Offset: 0x0000B2CC
		~ISteamMatchmakingServerListResponse()
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

		// Token: 0x060008DC RID: 2268 RVA: 0x0000D128 File Offset: 0x0000B328
		private void InternalOnServerResponded(IntPtr thisptr, HServerListRequest hRequest, int iServer)
		{
			try
			{
				this.m_ServerResponded(hRequest, iServer);
			}
			catch (Exception e)
			{
				CallbackDispatcher.ExceptionHandler(e);
			}
		}

		// Token: 0x060008DD RID: 2269 RVA: 0x0000D15C File Offset: 0x0000B35C
		private void InternalOnServerFailedToRespond(IntPtr thisptr, HServerListRequest hRequest, int iServer)
		{
			try
			{
				this.m_ServerFailedToRespond(hRequest, iServer);
			}
			catch (Exception e)
			{
				CallbackDispatcher.ExceptionHandler(e);
			}
		}

		// Token: 0x060008DE RID: 2270 RVA: 0x0000D190 File Offset: 0x0000B390
		private void InternalOnRefreshComplete(IntPtr thisptr, HServerListRequest hRequest, EMatchMakingServerResponse response)
		{
			try
			{
				this.m_RefreshComplete(hRequest, response);
			}
			catch (Exception e)
			{
				CallbackDispatcher.ExceptionHandler(e);
			}
		}

		// Token: 0x060008DF RID: 2271 RVA: 0x0000D1C4 File Offset: 0x0000B3C4
		public static explicit operator IntPtr(ISteamMatchmakingServerListResponse that)
		{
			return that.m_pGCHandle.AddrOfPinnedObject();
		}

		// Token: 0x04000A41 RID: 2625
		private ISteamMatchmakingServerListResponse.VTable m_VTable;

		// Token: 0x04000A42 RID: 2626
		private IntPtr m_pVTable;

		// Token: 0x04000A43 RID: 2627
		private GCHandle m_pGCHandle;

		// Token: 0x04000A44 RID: 2628
		private ISteamMatchmakingServerListResponse.ServerResponded m_ServerResponded;

		// Token: 0x04000A45 RID: 2629
		private ISteamMatchmakingServerListResponse.ServerFailedToRespond m_ServerFailedToRespond;

		// Token: 0x04000A46 RID: 2630
		private ISteamMatchmakingServerListResponse.RefreshComplete m_RefreshComplete;

		// Token: 0x020001D2 RID: 466
		// (Invoke) Token: 0x06000B8E RID: 2958
		public delegate void ServerResponded(HServerListRequest hRequest, int iServer);

		// Token: 0x020001D3 RID: 467
		// (Invoke) Token: 0x06000B92 RID: 2962
		public delegate void ServerFailedToRespond(HServerListRequest hRequest, int iServer);

		// Token: 0x020001D4 RID: 468
		// (Invoke) Token: 0x06000B96 RID: 2966
		public delegate void RefreshComplete(HServerListRequest hRequest, EMatchMakingServerResponse response);

		// Token: 0x020001D5 RID: 469
		// (Invoke) Token: 0x06000B9A RID: 2970
		[UnmanagedFunctionPointer(CallingConvention.ThisCall)]
		private delegate void InternalServerResponded(IntPtr thisptr, HServerListRequest hRequest, int iServer);

		// Token: 0x020001D6 RID: 470
		// (Invoke) Token: 0x06000B9E RID: 2974
		[UnmanagedFunctionPointer(CallingConvention.ThisCall)]
		private delegate void InternalServerFailedToRespond(IntPtr thisptr, HServerListRequest hRequest, int iServer);

		// Token: 0x020001D7 RID: 471
		// (Invoke) Token: 0x06000BA2 RID: 2978
		[UnmanagedFunctionPointer(CallingConvention.ThisCall)]
		private delegate void InternalRefreshComplete(IntPtr thisptr, HServerListRequest hRequest, EMatchMakingServerResponse response);

		// Token: 0x020001D8 RID: 472
		[StructLayout(LayoutKind.Sequential)]
		private class VTable
		{
			// Token: 0x06000BA5 RID: 2981 RVA: 0x000109D4 File Offset: 0x0000EBD4
			public VTable()
			{
			}

			// Token: 0x04000B12 RID: 2834
			[NonSerialized]
			[MarshalAs(UnmanagedType.FunctionPtr)]
			public ISteamMatchmakingServerListResponse.InternalServerResponded m_VTServerResponded;

			// Token: 0x04000B13 RID: 2835
			[NonSerialized]
			[MarshalAs(UnmanagedType.FunctionPtr)]
			public ISteamMatchmakingServerListResponse.InternalServerFailedToRespond m_VTServerFailedToRespond;

			// Token: 0x04000B14 RID: 2836
			[NonSerialized]
			[MarshalAs(UnmanagedType.FunctionPtr)]
			public ISteamMatchmakingServerListResponse.InternalRefreshComplete m_VTRefreshComplete;
		}
	}
}
