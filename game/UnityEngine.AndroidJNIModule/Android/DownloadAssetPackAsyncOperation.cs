using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace UnityEngine.Android
{
	// Token: 0x02000014 RID: 20
	public class DownloadAssetPackAsyncOperation : CustomYieldInstruction
	{
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000196 RID: 406 RVA: 0x00007DF4 File Offset: 0x00005FF4
		public override bool keepWaiting
		{
			get
			{
				Dictionary<string, AndroidAssetPackInfo> assetPackInfos = this.m_AssetPackInfos;
				bool result;
				lock (assetPackInfos)
				{
					foreach (AndroidAssetPackInfo androidAssetPackInfo in this.m_AssetPackInfos.Values)
					{
						bool flag = androidAssetPackInfo == null;
						if (flag)
						{
							return true;
						}
						bool flag2 = androidAssetPackInfo.status != AndroidAssetPackStatus.Canceled && androidAssetPackInfo.status != AndroidAssetPackStatus.Completed && androidAssetPackInfo.status != AndroidAssetPackStatus.Failed && androidAssetPackInfo.status > AndroidAssetPackStatus.Unknown;
						if (flag2)
						{
							return true;
						}
					}
					result = false;
				}
				return result;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000197 RID: 407 RVA: 0x00007EB4 File Offset: 0x000060B4
		public bool isDone
		{
			get
			{
				return !this.keepWaiting;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000198 RID: 408 RVA: 0x00007EC0 File Offset: 0x000060C0
		public float progress
		{
			get
			{
				Dictionary<string, AndroidAssetPackInfo> assetPackInfos = this.m_AssetPackInfos;
				float result;
				lock (assetPackInfos)
				{
					float num = 0f;
					float num2 = 0f;
					foreach (AndroidAssetPackInfo androidAssetPackInfo in this.m_AssetPackInfos.Values)
					{
						bool flag = androidAssetPackInfo == null;
						if (!flag)
						{
							bool flag2 = androidAssetPackInfo.status == AndroidAssetPackStatus.Canceled || androidAssetPackInfo.status == AndroidAssetPackStatus.Completed || androidAssetPackInfo.status == AndroidAssetPackStatus.Failed || androidAssetPackInfo.status == AndroidAssetPackStatus.Unknown;
							if (flag2)
							{
								num += 1f;
								num2 += 1f;
							}
							else
							{
								double num3 = androidAssetPackInfo.bytesDownloaded / androidAssetPackInfo.size;
								num += (float)num3;
								num2 += androidAssetPackInfo.transferProgress;
							}
						}
					}
					result = Mathf.Clamp((num * 0.8f + num2 * 0.2f) / (float)this.m_AssetPackInfos.Count, 0f, 1f);
				}
				return result;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000199 RID: 409 RVA: 0x00007FF8 File Offset: 0x000061F8
		public string[] downloadedAssetPacks
		{
			get
			{
				Dictionary<string, AndroidAssetPackInfo> assetPackInfos = this.m_AssetPackInfos;
				string[] result;
				lock (assetPackInfos)
				{
					List<string> list = new List<string>();
					foreach (AndroidAssetPackInfo androidAssetPackInfo in this.m_AssetPackInfos.Values)
					{
						bool flag = androidAssetPackInfo == null;
						if (!flag)
						{
							bool flag2 = androidAssetPackInfo.status == AndroidAssetPackStatus.Completed;
							if (flag2)
							{
								list.Add(androidAssetPackInfo.name);
							}
						}
					}
					result = list.ToArray();
				}
				return result;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600019A RID: 410 RVA: 0x000080B0 File Offset: 0x000062B0
		public string[] downloadFailedAssetPacks
		{
			get
			{
				Dictionary<string, AndroidAssetPackInfo> assetPackInfos = this.m_AssetPackInfos;
				string[] result;
				lock (assetPackInfos)
				{
					List<string> list = new List<string>();
					foreach (KeyValuePair<string, AndroidAssetPackInfo> keyValuePair in this.m_AssetPackInfos)
					{
						AndroidAssetPackInfo value = keyValuePair.Value;
						bool flag = value == null;
						if (flag)
						{
							list.Add(keyValuePair.Key);
						}
						else
						{
							bool flag2 = value.status == AndroidAssetPackStatus.Canceled || value.status == AndroidAssetPackStatus.Failed || value.status == AndroidAssetPackStatus.Unknown;
							if (flag2)
							{
								list.Add(value.name);
							}
						}
					}
					result = list.ToArray();
				}
				return result;
			}
		}

		// Token: 0x0600019B RID: 411 RVA: 0x00008194 File Offset: 0x00006394
		internal DownloadAssetPackAsyncOperation(string[] assetPackNames)
		{
			this.m_AssetPackInfos = assetPackNames.ToDictionary((string name) => name, (string name) => null);
		}

		// Token: 0x0600019C RID: 412 RVA: 0x000081F4 File Offset: 0x000063F4
		internal void OnUpdate(AndroidAssetPackInfo info)
		{
			Dictionary<string, AndroidAssetPackInfo> assetPackInfos = this.m_AssetPackInfos;
			lock (assetPackInfos)
			{
				this.m_AssetPackInfos[info.name] = info;
			}
		}

		// Token: 0x04000040 RID: 64
		private Dictionary<string, AndroidAssetPackInfo> m_AssetPackInfos;

		// Token: 0x02000015 RID: 21
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x0600019D RID: 413 RVA: 0x00008240 File Offset: 0x00006440
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x0600019E RID: 414 RVA: 0x000049C2 File Offset: 0x00002BC2
			public <>c()
			{
			}

			// Token: 0x0600019F RID: 415 RVA: 0x0000824C File Offset: 0x0000644C
			internal string <.ctor>b__11_0(string name)
			{
				return name;
			}

			// Token: 0x060001A0 RID: 416 RVA: 0x0000824F File Offset: 0x0000644F
			internal AndroidAssetPackInfo <.ctor>b__11_1(string name)
			{
				return null;
			}

			// Token: 0x04000041 RID: 65
			public static readonly DownloadAssetPackAsyncOperation.<>c <>9 = new DownloadAssetPackAsyncOperation.<>c();

			// Token: 0x04000042 RID: 66
			public static Func<string, string> <>9__11_0;

			// Token: 0x04000043 RID: 67
			public static Func<string, AndroidAssetPackInfo> <>9__11_1;
		}
	}
}
