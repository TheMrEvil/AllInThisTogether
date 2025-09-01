using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x020001CE RID: 462
	[NativeHeader("Runtime/Export/Networking/Ping.bindings.h")]
	public sealed class Ping
	{
		// Token: 0x0600159C RID: 5532 RVA: 0x00022EDD File Offset: 0x000210DD
		public Ping(string address)
		{
			this.m_Ptr = Ping.Internal_Create(address);
		}

		// Token: 0x0600159D RID: 5533 RVA: 0x00022EF4 File Offset: 0x000210F4
		~Ping()
		{
			this.DestroyPing();
		}

		// Token: 0x0600159E RID: 5534 RVA: 0x00022F24 File Offset: 0x00021124
		[ThreadAndSerializationSafe]
		public void DestroyPing()
		{
			bool flag = this.m_Ptr == IntPtr.Zero;
			if (!flag)
			{
				Ping.Internal_Destroy(this.m_Ptr);
				this.m_Ptr = IntPtr.Zero;
			}
		}

		// Token: 0x0600159F RID: 5535
		[FreeFunction("DestroyPing", IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_Destroy(IntPtr ptr);

		// Token: 0x060015A0 RID: 5536
		[FreeFunction("CreatePing")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr Internal_Create(string address);

		// Token: 0x17000455 RID: 1109
		// (get) Token: 0x060015A1 RID: 5537 RVA: 0x00022F60 File Offset: 0x00021160
		public bool isDone
		{
			get
			{
				bool flag = this.m_Ptr == IntPtr.Zero;
				return !flag && this.Internal_IsDone();
			}
		}

		// Token: 0x060015A2 RID: 5538
		[NativeName("GetIsDone")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool Internal_IsDone();

		// Token: 0x17000456 RID: 1110
		// (get) Token: 0x060015A3 RID: 5539
		public extern int time { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000457 RID: 1111
		// (get) Token: 0x060015A4 RID: 5540
		public extern string ip { [NativeName("GetIP")] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x040007A8 RID: 1960
		internal IntPtr m_Ptr;
	}
}
