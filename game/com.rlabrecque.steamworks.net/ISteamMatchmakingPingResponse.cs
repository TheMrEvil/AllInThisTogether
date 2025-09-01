using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000189 RID: 393
	public class ISteamMatchmakingPingResponse
	{
		// Token: 0x060008E0 RID: 2272 RVA: 0x0000D1D4 File Offset: 0x0000B3D4
		public ISteamMatchmakingPingResponse(ISteamMatchmakingPingResponse.ServerResponded onServerResponded, ISteamMatchmakingPingResponse.ServerFailedToRespond onServerFailedToRespond)
		{
			if (onServerResponded == null || onServerFailedToRespond == null)
			{
				throw new ArgumentNullException();
			}
			this.m_ServerResponded = onServerResponded;
			this.m_ServerFailedToRespond = onServerFailedToRespond;
			this.m_VTable = new ISteamMatchmakingPingResponse.VTable
			{
				m_VTServerResponded = new ISteamMatchmakingPingResponse.InternalServerResponded(this.InternalOnServerResponded),
				m_VTServerFailedToRespond = new ISteamMatchmakingPingResponse.InternalServerFailedToRespond(this.InternalOnServerFailedToRespond)
			};
			this.m_pVTable = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(ISteamMatchmakingPingResponse.VTable)));
			Marshal.StructureToPtr<ISteamMatchmakingPingResponse.VTable>(this.m_VTable, this.m_pVTable, false);
			this.m_pGCHandle = GCHandle.Alloc(this.m_pVTable, GCHandleType.Pinned);
		}

		// Token: 0x060008E1 RID: 2273 RVA: 0x0000D274 File Offset: 0x0000B474
		~ISteamMatchmakingPingResponse()
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

		// Token: 0x060008E2 RID: 2274 RVA: 0x0000D2D0 File Offset: 0x0000B4D0
		private void InternalOnServerResponded(IntPtr thisptr, gameserveritem_t server)
		{
			this.m_ServerResponded(server);
		}

		// Token: 0x060008E3 RID: 2275 RVA: 0x0000D2DE File Offset: 0x0000B4DE
		private void InternalOnServerFailedToRespond(IntPtr thisptr)
		{
			this.m_ServerFailedToRespond();
		}

		// Token: 0x060008E4 RID: 2276 RVA: 0x0000D2EB File Offset: 0x0000B4EB
		public static explicit operator IntPtr(ISteamMatchmakingPingResponse that)
		{
			return that.m_pGCHandle.AddrOfPinnedObject();
		}

		// Token: 0x04000A47 RID: 2631
		private ISteamMatchmakingPingResponse.VTable m_VTable;

		// Token: 0x04000A48 RID: 2632
		private IntPtr m_pVTable;

		// Token: 0x04000A49 RID: 2633
		private GCHandle m_pGCHandle;

		// Token: 0x04000A4A RID: 2634
		private ISteamMatchmakingPingResponse.ServerResponded m_ServerResponded;

		// Token: 0x04000A4B RID: 2635
		private ISteamMatchmakingPingResponse.ServerFailedToRespond m_ServerFailedToRespond;

		// Token: 0x020001D9 RID: 473
		// (Invoke) Token: 0x06000BA7 RID: 2983
		public delegate void ServerResponded(gameserveritem_t server);

		// Token: 0x020001DA RID: 474
		// (Invoke) Token: 0x06000BAB RID: 2987
		public delegate void ServerFailedToRespond();

		// Token: 0x020001DB RID: 475
		// (Invoke) Token: 0x06000BAF RID: 2991
		[UnmanagedFunctionPointer(CallingConvention.ThisCall)]
		private delegate void InternalServerResponded(IntPtr thisptr, gameserveritem_t server);

		// Token: 0x020001DC RID: 476
		// (Invoke) Token: 0x06000BB3 RID: 2995
		[UnmanagedFunctionPointer(CallingConvention.ThisCall)]
		private delegate void InternalServerFailedToRespond(IntPtr thisptr);

		// Token: 0x020001DD RID: 477
		[StructLayout(LayoutKind.Sequential)]
		private class VTable
		{
			// Token: 0x06000BB6 RID: 2998 RVA: 0x000109DC File Offset: 0x0000EBDC
			public VTable()
			{
			}

			// Token: 0x04000B15 RID: 2837
			[NonSerialized]
			[MarshalAs(UnmanagedType.FunctionPtr)]
			public ISteamMatchmakingPingResponse.InternalServerResponded m_VTServerResponded;

			// Token: 0x04000B16 RID: 2838
			[NonSerialized]
			[MarshalAs(UnmanagedType.FunctionPtr)]
			public ISteamMatchmakingPingResponse.InternalServerFailedToRespond m_VTServerFailedToRespond;
		}
	}
}
