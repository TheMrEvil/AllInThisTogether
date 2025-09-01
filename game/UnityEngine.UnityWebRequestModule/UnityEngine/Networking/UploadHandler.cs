using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;

namespace UnityEngine.Networking
{
	// Token: 0x02000012 RID: 18
	[NativeHeader("Modules/UnityWebRequest/Public/UploadHandler/UploadHandler.h")]
	[StructLayout(LayoutKind.Sequential)]
	public class UploadHandler : IDisposable
	{
		// Token: 0x06000107 RID: 263
		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Release();

		// Token: 0x06000108 RID: 264 RVA: 0x00004A92 File Offset: 0x00002C92
		internal UploadHandler()
		{
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00004FE4 File Offset: 0x000031E4
		~UploadHandler()
		{
			this.Dispose();
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00005014 File Offset: 0x00003214
		public virtual void Dispose()
		{
			bool flag = this.m_Ptr != IntPtr.Zero;
			if (flag)
			{
				this.Release();
				this.m_Ptr = IntPtr.Zero;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600010B RID: 267 RVA: 0x0000504C File Offset: 0x0000324C
		public byte[] data
		{
			get
			{
				return this.GetData();
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600010C RID: 268 RVA: 0x00005064 File Offset: 0x00003264
		// (set) Token: 0x0600010D RID: 269 RVA: 0x0000507C File Offset: 0x0000327C
		public string contentType
		{
			get
			{
				return this.GetContentType();
			}
			set
			{
				this.SetContentType(value);
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600010E RID: 270 RVA: 0x00005088 File Offset: 0x00003288
		public float progress
		{
			get
			{
				return this.GetProgress();
			}
		}

		// Token: 0x0600010F RID: 271 RVA: 0x000050A0 File Offset: 0x000032A0
		internal virtual byte[] GetData()
		{
			return null;
		}

		// Token: 0x06000110 RID: 272 RVA: 0x000050B4 File Offset: 0x000032B4
		internal virtual string GetContentType()
		{
			return this.InternalGetContentType();
		}

		// Token: 0x06000111 RID: 273 RVA: 0x000050CC File Offset: 0x000032CC
		internal virtual void SetContentType(string newContentType)
		{
			this.InternalSetContentType(newContentType);
		}

		// Token: 0x06000112 RID: 274 RVA: 0x000050D8 File Offset: 0x000032D8
		internal virtual float GetProgress()
		{
			return this.InternalGetProgress();
		}

		// Token: 0x06000113 RID: 275
		[NativeMethod("GetContentType")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern string InternalGetContentType();

		// Token: 0x06000114 RID: 276
		[NativeMethod("SetContentType")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void InternalSetContentType(string newContentType);

		// Token: 0x06000115 RID: 277
		[NativeMethod("GetProgress")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern float InternalGetProgress();

		// Token: 0x0400005C RID: 92
		[NonSerialized]
		internal IntPtr m_Ptr;
	}
}
