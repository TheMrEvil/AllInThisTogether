using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;

namespace UnityEngine.Networking
{
	// Token: 0x02000003 RID: 3
	[NativeHeader("Modules/UnityWebRequestAssetBundle/Public/DownloadHandlerAssetBundle.h")]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class DownloadHandlerAssetBundle : DownloadHandler
	{
		// Token: 0x0600000B RID: 11
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr Create(DownloadHandlerAssetBundle obj, string url, uint crc);

		// Token: 0x0600000C RID: 12 RVA: 0x000021E1 File Offset: 0x000003E1
		private static IntPtr CreateCached(DownloadHandlerAssetBundle obj, string url, string name, Hash128 hash, uint crc)
		{
			return DownloadHandlerAssetBundle.CreateCached_Injected(obj, url, name, ref hash, crc);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000021EF File Offset: 0x000003EF
		private void InternalCreateAssetBundle(string url, uint crc)
		{
			this.m_Ptr = DownloadHandlerAssetBundle.Create(this, url, crc);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002200 File Offset: 0x00000400
		private void InternalCreateAssetBundleCached(string url, string name, Hash128 hash, uint crc)
		{
			this.m_Ptr = DownloadHandlerAssetBundle.CreateCached(this, url, name, hash, crc);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002214 File Offset: 0x00000414
		public DownloadHandlerAssetBundle(string url, uint crc)
		{
			this.InternalCreateAssetBundle(url, crc);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002227 File Offset: 0x00000427
		public DownloadHandlerAssetBundle(string url, uint version, uint crc)
		{
			this.InternalCreateAssetBundleCached(url, "", new Hash128(0U, 0U, 0U, version), crc);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002248 File Offset: 0x00000448
		public DownloadHandlerAssetBundle(string url, Hash128 hash, uint crc)
		{
			this.InternalCreateAssetBundleCached(url, "", hash, crc);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002261 File Offset: 0x00000461
		public DownloadHandlerAssetBundle(string url, string name, Hash128 hash, uint crc)
		{
			this.InternalCreateAssetBundleCached(url, name, hash, crc);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002277 File Offset: 0x00000477
		public DownloadHandlerAssetBundle(string url, CachedAssetBundle cachedBundle, uint crc)
		{
			this.InternalCreateAssetBundleCached(url, cachedBundle.name, cachedBundle.hash, crc);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002298 File Offset: 0x00000498
		protected override byte[] GetData()
		{
			throw new NotSupportedException("Raw data access is not supported for asset bundles");
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000022A5 File Offset: 0x000004A5
		protected override string GetText()
		{
			throw new NotSupportedException("String access is not supported for asset bundles");
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000016 RID: 22
		public extern AssetBundle assetBundle { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000017 RID: 23
		// (set) Token: 0x06000018 RID: 24
		public extern bool autoLoadAssetBundle { [MethodImpl(MethodImplOptions.InternalCall)] get; [NativeThrows] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000019 RID: 25
		public extern bool isDownloadComplete { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x0600001A RID: 26 RVA: 0x000022B4 File Offset: 0x000004B4
		public static AssetBundle GetContent(UnityWebRequest www)
		{
			return DownloadHandler.GetCheckedDownloader<DownloadHandlerAssetBundle>(www).assetBundle;
		}

		// Token: 0x0600001B RID: 27
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr CreateCached_Injected(DownloadHandlerAssetBundle obj, string url, string name, ref Hash128 hash, uint crc);
	}
}
