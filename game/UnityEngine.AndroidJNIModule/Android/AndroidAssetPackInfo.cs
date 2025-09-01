using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace UnityEngine.Android
{
	// Token: 0x02000011 RID: 17
	public class AndroidAssetPackInfo
	{
		// Token: 0x06000189 RID: 393 RVA: 0x00007D3C File Offset: 0x00005F3C
		internal AndroidAssetPackInfo(string name, AndroidAssetPackStatus status, ulong size, ulong bytesDownloaded, float transferProgress, AndroidAssetPackError error)
		{
			this.name = name;
			this.status = status;
			this.size = size;
			this.bytesDownloaded = bytesDownloaded;
			this.transferProgress = transferProgress;
			this.error = error;
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600018A RID: 394 RVA: 0x00007D73 File Offset: 0x00005F73
		public string name
		{
			[CompilerGenerated]
			get
			{
				return this.<name>k__BackingField;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600018B RID: 395 RVA: 0x00007D7B File Offset: 0x00005F7B
		public AndroidAssetPackStatus status
		{
			[CompilerGenerated]
			get
			{
				return this.<status>k__BackingField;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600018C RID: 396 RVA: 0x00007D83 File Offset: 0x00005F83
		public ulong size
		{
			[CompilerGenerated]
			get
			{
				return this.<size>k__BackingField;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600018D RID: 397 RVA: 0x00007D8B File Offset: 0x00005F8B
		public ulong bytesDownloaded
		{
			[CompilerGenerated]
			get
			{
				return this.<bytesDownloaded>k__BackingField;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600018E RID: 398 RVA: 0x00007D93 File Offset: 0x00005F93
		public float transferProgress
		{
			[CompilerGenerated]
			get
			{
				return this.<transferProgress>k__BackingField;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600018F RID: 399 RVA: 0x00007D9B File Offset: 0x00005F9B
		public AndroidAssetPackError error
		{
			[CompilerGenerated]
			get
			{
				return this.<error>k__BackingField;
			}
		}

		// Token: 0x04000036 RID: 54
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private readonly string <name>k__BackingField;

		// Token: 0x04000037 RID: 55
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private readonly AndroidAssetPackStatus <status>k__BackingField;

		// Token: 0x04000038 RID: 56
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private readonly ulong <size>k__BackingField;

		// Token: 0x04000039 RID: 57
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private readonly ulong <bytesDownloaded>k__BackingField;

		// Token: 0x0400003A RID: 58
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private readonly float <transferProgress>k__BackingField;

		// Token: 0x0400003B RID: 59
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private readonly AndroidAssetPackError <error>k__BackingField;
	}
}
