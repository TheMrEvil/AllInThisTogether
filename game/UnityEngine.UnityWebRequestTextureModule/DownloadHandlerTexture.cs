using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Collections;
using UnityEngine.Bindings;

namespace UnityEngine.Networking
{
	// Token: 0x02000003 RID: 3
	[NativeHeader("Modules/UnityWebRequestTexture/Public/DownloadHandlerTexture.h")]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class DownloadHandlerTexture : DownloadHandler
	{
		// Token: 0x06000005 RID: 5
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr Create(DownloadHandlerTexture obj, bool readable);

		// Token: 0x06000006 RID: 6 RVA: 0x000020D7 File Offset: 0x000002D7
		private void InternalCreateTexture(bool readable)
		{
			this.m_Ptr = DownloadHandlerTexture.Create(this, readable);
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000020E7 File Offset: 0x000002E7
		public DownloadHandlerTexture()
		{
			this.InternalCreateTexture(true);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000020F9 File Offset: 0x000002F9
		public DownloadHandlerTexture(bool readable)
		{
			this.InternalCreateTexture(readable);
			this.mNonReadable = !readable;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002118 File Offset: 0x00000318
		protected override NativeArray<byte> GetNativeData()
		{
			return DownloadHandler.InternalGetNativeArray(this, ref this.m_NativeData);
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002136 File Offset: 0x00000336
		public override void Dispose()
		{
			DownloadHandler.DisposeNativeArray(ref this.m_NativeData);
			base.Dispose();
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000B RID: 11 RVA: 0x0000214C File Offset: 0x0000034C
		public Texture2D texture
		{
			get
			{
				return this.InternalGetTexture();
			}
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002164 File Offset: 0x00000364
		private Texture2D InternalGetTexture()
		{
			bool flag = this.mHasTexture;
			if (flag)
			{
				bool flag2 = this.mTexture == null;
				if (flag2)
				{
					this.mTexture = new Texture2D(2, 2);
					this.mTexture.LoadImage(this.GetData(), this.mNonReadable);
				}
			}
			else
			{
				bool flag3 = this.mTexture == null;
				if (flag3)
				{
					try
					{
						this.mTexture = this.InternalGetTextureNative();
						this.mHasTexture = true;
					}
					finally
					{
						this.ClearNativeTexture();
					}
				}
			}
			return this.mTexture;
		}

		// Token: 0x0600000D RID: 13
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern Texture2D InternalGetTextureNative();

		// Token: 0x0600000E RID: 14
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void ClearNativeTexture();

		// Token: 0x0600000F RID: 15 RVA: 0x00002204 File Offset: 0x00000404
		public static Texture2D GetContent(UnityWebRequest www)
		{
			return DownloadHandler.GetCheckedDownloader<DownloadHandlerTexture>(www).texture;
		}

		// Token: 0x04000001 RID: 1
		private NativeArray<byte> m_NativeData;

		// Token: 0x04000002 RID: 2
		private Texture2D mTexture;

		// Token: 0x04000003 RID: 3
		private bool mHasTexture;

		// Token: 0x04000004 RID: 4
		private bool mNonReadable;
	}
}
