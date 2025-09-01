using System;
using System.Runtime.CompilerServices;

namespace Photon.Realtime
{
	// Token: 0x02000038 RID: 56
	public class Region
	{
		// Token: 0x17000046 RID: 70
		// (get) Token: 0x0600016E RID: 366 RVA: 0x00008EB4 File Offset: 0x000070B4
		// (set) Token: 0x0600016F RID: 367 RVA: 0x00008EBC File Offset: 0x000070BC
		public string Code
		{
			[CompilerGenerated]
			get
			{
				return this.<Code>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Code>k__BackingField = value;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000170 RID: 368 RVA: 0x00008EC5 File Offset: 0x000070C5
		// (set) Token: 0x06000171 RID: 369 RVA: 0x00008ECD File Offset: 0x000070CD
		public string Cluster
		{
			[CompilerGenerated]
			get
			{
				return this.<Cluster>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Cluster>k__BackingField = value;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000172 RID: 370 RVA: 0x00008ED6 File Offset: 0x000070D6
		// (set) Token: 0x06000173 RID: 371 RVA: 0x00008EDE File Offset: 0x000070DE
		public string HostAndPort
		{
			[CompilerGenerated]
			get
			{
				return this.<HostAndPort>k__BackingField;
			}
			[CompilerGenerated]
			protected internal set
			{
				this.<HostAndPort>k__BackingField = value;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000174 RID: 372 RVA: 0x00008EE7 File Offset: 0x000070E7
		// (set) Token: 0x06000175 RID: 373 RVA: 0x00008EEF File Offset: 0x000070EF
		public int Ping
		{
			[CompilerGenerated]
			get
			{
				return this.<Ping>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Ping>k__BackingField = value;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000176 RID: 374 RVA: 0x00008EF8 File Offset: 0x000070F8
		public bool WasPinged
		{
			get
			{
				return this.Ping != int.MaxValue;
			}
		}

		// Token: 0x06000177 RID: 375 RVA: 0x00008F0A File Offset: 0x0000710A
		public Region(string code, string address)
		{
			this.SetCodeAndCluster(code);
			this.HostAndPort = address;
			this.Ping = int.MaxValue;
		}

		// Token: 0x06000178 RID: 376 RVA: 0x00008F2B File Offset: 0x0000712B
		public Region(string code, int ping)
		{
			this.SetCodeAndCluster(code);
			this.Ping = ping;
		}

		// Token: 0x06000179 RID: 377 RVA: 0x00008F44 File Offset: 0x00007144
		private void SetCodeAndCluster(string codeAsString)
		{
			if (codeAsString == null)
			{
				this.Code = "";
				this.Cluster = "";
				return;
			}
			codeAsString = codeAsString.ToLower();
			int num = codeAsString.IndexOf('/');
			this.Code = ((num <= 0) ? codeAsString : codeAsString.Substring(0, num));
			this.Cluster = ((num <= 0) ? "" : codeAsString.Substring(num + 1, codeAsString.Length - num - 1));
		}

		// Token: 0x0600017A RID: 378 RVA: 0x00008FB5 File Offset: 0x000071B5
		public override string ToString()
		{
			return this.ToString(false);
		}

		// Token: 0x0600017B RID: 379 RVA: 0x00008FC0 File Offset: 0x000071C0
		public string ToString(bool compact = false)
		{
			string text = this.Code;
			if (!string.IsNullOrEmpty(this.Cluster))
			{
				text = text + "/" + this.Cluster;
			}
			if (compact)
			{
				return string.Format("{0}:{1}", text, this.Ping);
			}
			return string.Format("{0}[{2}]: {1}ms", text, this.Ping, this.HostAndPort);
		}

		// Token: 0x040001CA RID: 458
		[CompilerGenerated]
		private string <Code>k__BackingField;

		// Token: 0x040001CB RID: 459
		[CompilerGenerated]
		private string <Cluster>k__BackingField;

		// Token: 0x040001CC RID: 460
		[CompilerGenerated]
		private string <HostAndPort>k__BackingField;

		// Token: 0x040001CD RID: 461
		[CompilerGenerated]
		private int <Ping>k__BackingField;
	}
}
