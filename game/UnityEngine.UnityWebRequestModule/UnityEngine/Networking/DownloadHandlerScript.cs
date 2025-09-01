using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;

namespace UnityEngine.Networking
{
	// Token: 0x02000010 RID: 16
	[NativeHeader("Modules/UnityWebRequest/Public/DownloadHandler/DownloadHandlerScript.h")]
	[StructLayout(LayoutKind.Sequential)]
	public class DownloadHandlerScript : DownloadHandler
	{
		// Token: 0x060000F8 RID: 248
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr Create(DownloadHandlerScript obj);

		// Token: 0x060000F9 RID: 249
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr CreatePreallocated(DownloadHandlerScript obj, [Unmarshalled] byte[] preallocatedBuffer);

		// Token: 0x060000FA RID: 250 RVA: 0x00004EFD File Offset: 0x000030FD
		private void InternalCreateScript()
		{
			this.m_Ptr = DownloadHandlerScript.Create(this);
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00004F0C File Offset: 0x0000310C
		private void InternalCreateScript(byte[] preallocatedBuffer)
		{
			this.m_Ptr = DownloadHandlerScript.CreatePreallocated(this, preallocatedBuffer);
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00004F1C File Offset: 0x0000311C
		public DownloadHandlerScript()
		{
			this.InternalCreateScript();
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00004F30 File Offset: 0x00003130
		public DownloadHandlerScript(byte[] preallocatedBuffer)
		{
			bool flag = preallocatedBuffer == null || preallocatedBuffer.Length < 1;
			if (flag)
			{
				throw new ArgumentException("Cannot create a preallocated-buffer DownloadHandlerScript backed by a null or zero-length array");
			}
			this.InternalCreateScript(preallocatedBuffer);
		}
	}
}
