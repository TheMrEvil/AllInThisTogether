using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x0200003A RID: 58
	[NativeHeader("Modules/IMGUI/GUIState.h")]
	internal class ObjectGUIState : IDisposable
	{
		// Token: 0x0600040D RID: 1037 RVA: 0x0000F25B File Offset: 0x0000D45B
		public ObjectGUIState()
		{
			this.m_Ptr = ObjectGUIState.Internal_Create();
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x0000F270 File Offset: 0x0000D470
		public void Dispose()
		{
			this.Destroy();
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x0000F284 File Offset: 0x0000D484
		~ObjectGUIState()
		{
			this.Destroy();
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x0000F2B4 File Offset: 0x0000D4B4
		private void Destroy()
		{
			bool flag = this.m_Ptr != IntPtr.Zero;
			if (flag)
			{
				ObjectGUIState.Internal_Destroy(this.m_Ptr);
				this.m_Ptr = IntPtr.Zero;
			}
		}

		// Token: 0x06000411 RID: 1041
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr Internal_Create();

		// Token: 0x06000412 RID: 1042
		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_Destroy(IntPtr ptr);

		// Token: 0x04000125 RID: 293
		internal IntPtr m_Ptr;
	}
}
