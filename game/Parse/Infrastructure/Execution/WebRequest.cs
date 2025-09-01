using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;

namespace Parse.Infrastructure.Execution
{
	// Token: 0x02000063 RID: 99
	public class WebRequest
	{
		// Token: 0x1700015D RID: 349
		// (get) Token: 0x06000476 RID: 1142 RVA: 0x0000FACB File Offset: 0x0000DCCB
		public Uri Target
		{
			get
			{
				return new Uri(new Uri(this.Resource), this.Path);
			}
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x06000477 RID: 1143 RVA: 0x0000FAE3 File Offset: 0x0000DCE3
		// (set) Token: 0x06000478 RID: 1144 RVA: 0x0000FAEB File Offset: 0x0000DCEB
		public string Resource
		{
			[CompilerGenerated]
			get
			{
				return this.<Resource>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Resource>k__BackingField = value;
			}
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x06000479 RID: 1145 RVA: 0x0000FAF4 File Offset: 0x0000DCF4
		// (set) Token: 0x0600047A RID: 1146 RVA: 0x0000FAFC File Offset: 0x0000DCFC
		public string Path
		{
			[CompilerGenerated]
			get
			{
				return this.<Path>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Path>k__BackingField = value;
			}
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x0600047B RID: 1147 RVA: 0x0000FB05 File Offset: 0x0000DD05
		// (set) Token: 0x0600047C RID: 1148 RVA: 0x0000FB0D File Offset: 0x0000DD0D
		public IList<KeyValuePair<string, string>> Headers
		{
			[CompilerGenerated]
			get
			{
				return this.<Headers>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Headers>k__BackingField = value;
			}
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x0600047D RID: 1149 RVA: 0x0000FB16 File Offset: 0x0000DD16
		// (set) Token: 0x0600047E RID: 1150 RVA: 0x0000FB1E File Offset: 0x0000DD1E
		public virtual Stream Data
		{
			[CompilerGenerated]
			get
			{
				return this.<Data>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Data>k__BackingField = value;
			}
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x0600047F RID: 1151 RVA: 0x0000FB27 File Offset: 0x0000DD27
		// (set) Token: 0x06000480 RID: 1152 RVA: 0x0000FB2F File Offset: 0x0000DD2F
		public string Method
		{
			[CompilerGenerated]
			get
			{
				return this.<Method>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Method>k__BackingField = value;
			}
		}

		// Token: 0x06000481 RID: 1153 RVA: 0x0000FB38 File Offset: 0x0000DD38
		public WebRequest()
		{
		}

		// Token: 0x040000E8 RID: 232
		[CompilerGenerated]
		private string <Resource>k__BackingField;

		// Token: 0x040000E9 RID: 233
		[CompilerGenerated]
		private string <Path>k__BackingField;

		// Token: 0x040000EA RID: 234
		[CompilerGenerated]
		private IList<KeyValuePair<string, string>> <Headers>k__BackingField;

		// Token: 0x040000EB RID: 235
		[CompilerGenerated]
		private Stream <Data>k__BackingField;

		// Token: 0x040000EC RID: 236
		[CompilerGenerated]
		private string <Method>k__BackingField;
	}
}
