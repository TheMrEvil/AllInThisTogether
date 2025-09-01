using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace Mono.CSharp
{
	// Token: 0x02000288 RID: 648
	public abstract class ReportPrinter
	{
		// Token: 0x17000747 RID: 1863
		// (get) Token: 0x06001F9E RID: 8094 RVA: 0x0009B443 File Offset: 0x00099643
		// (set) Token: 0x06001F9F RID: 8095 RVA: 0x0009B44B File Offset: 0x0009964B
		public int ErrorsCount
		{
			[CompilerGenerated]
			get
			{
				return this.<ErrorsCount>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<ErrorsCount>k__BackingField = value;
			}
		}

		// Token: 0x17000748 RID: 1864
		// (get) Token: 0x06001FA0 RID: 8096 RVA: 0x0009B454 File Offset: 0x00099654
		// (set) Token: 0x06001FA1 RID: 8097 RVA: 0x0009B45C File Offset: 0x0009965C
		public int WarningsCount
		{
			[CompilerGenerated]
			get
			{
				return this.<WarningsCount>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<WarningsCount>k__BackingField = value;
			}
		}

		// Token: 0x17000749 RID: 1865
		// (get) Token: 0x06001FA2 RID: 8098 RVA: 0x0000212D File Offset: 0x0000032D
		public virtual bool HasRelatedSymbolSupport
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06001FA3 RID: 8099 RVA: 0x0004D50E File Offset: 0x0004B70E
		protected virtual string FormatText(string txt)
		{
			return txt;
		}

		// Token: 0x06001FA4 RID: 8100 RVA: 0x0009B468 File Offset: 0x00099668
		public virtual void Print(AbstractMessage msg, bool showFullPath)
		{
			int num;
			if (msg.IsWarning)
			{
				num = this.WarningsCount + 1;
				this.WarningsCount = num;
				return;
			}
			num = this.ErrorsCount + 1;
			this.ErrorsCount = num;
		}

		// Token: 0x06001FA5 RID: 8101 RVA: 0x0009B4A0 File Offset: 0x000996A0
		protected void Print(AbstractMessage msg, TextWriter output, bool showFullPath)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (!msg.Location.IsNull)
			{
				if (showFullPath)
				{
					stringBuilder.Append(msg.Location.ToStringFullName());
				}
				else
				{
					stringBuilder.Append(msg.Location.ToString());
				}
				stringBuilder.Append(" ");
			}
			stringBuilder.AppendFormat("{0} CS{1:0000}: {2}", msg.MessageType, msg.Code, msg.Text);
			if (!msg.IsWarning)
			{
				output.WriteLine(this.FormatText(stringBuilder.ToString()));
			}
			else
			{
				output.WriteLine(stringBuilder.ToString());
			}
			if (msg.RelatedSymbols != null)
			{
				foreach (string str in msg.RelatedSymbols)
				{
					output.WriteLine(str + msg.MessageType + ")");
				}
			}
		}

		// Token: 0x06001FA6 RID: 8102 RVA: 0x0009B589 File Offset: 0x00099789
		public bool MissingTypeReported(ITypeDefinition typeDefinition)
		{
			if (this.reported_missing_definitions == null)
			{
				this.reported_missing_definitions = new HashSet<ITypeDefinition>();
			}
			if (this.reported_missing_definitions.Contains(typeDefinition))
			{
				return true;
			}
			this.reported_missing_definitions.Add(typeDefinition);
			return false;
		}

		// Token: 0x06001FA7 RID: 8103 RVA: 0x0009B5BC File Offset: 0x000997BC
		public void Reset()
		{
			this.ErrorsCount = (this.WarningsCount = 0);
		}

		// Token: 0x06001FA8 RID: 8104 RVA: 0x00002CCC File Offset: 0x00000ECC
		protected ReportPrinter()
		{
		}

		// Token: 0x04000B93 RID: 2963
		protected HashSet<ITypeDefinition> reported_missing_definitions;

		// Token: 0x04000B94 RID: 2964
		[CompilerGenerated]
		private int <ErrorsCount>k__BackingField;

		// Token: 0x04000B95 RID: 2965
		[CompilerGenerated]
		private int <WarningsCount>k__BackingField;
	}
}
