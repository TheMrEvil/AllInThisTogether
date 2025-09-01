using System;
using System.Collections.Generic;

namespace Mono.CSharp
{
	// Token: 0x0200028A RID: 650
	internal class SessionReportPrinter : ReportPrinter
	{
		// Token: 0x06001FAA RID: 8106 RVA: 0x0009B5E1 File Offset: 0x000997E1
		public void ClearSession()
		{
			this.session_messages = null;
		}

		// Token: 0x06001FAB RID: 8107 RVA: 0x0009B5EA File Offset: 0x000997EA
		public override void Print(AbstractMessage msg, bool showFullPath)
		{
			if (this.session_messages == null)
			{
				this.session_messages = new List<AbstractMessage>();
			}
			this.session_messages.Add(msg);
			this.showFullPaths = showFullPath;
			base.Print(msg, showFullPath);
		}

		// Token: 0x06001FAC RID: 8108 RVA: 0x0009B61C File Offset: 0x0009981C
		public void EndSession()
		{
			if (this.session_messages == null)
			{
				return;
			}
			if (this.common_messages == null)
			{
				this.common_messages = new List<AbstractMessage>(this.session_messages);
				this.merged_messages = this.session_messages;
				this.session_messages = null;
				return;
			}
			for (int i = 0; i < this.common_messages.Count; i++)
			{
				AbstractMessage abstractMessage = this.common_messages[i];
				bool flag = false;
				foreach (AbstractMessage obj in this.session_messages)
				{
					if (abstractMessage.Equals(obj))
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					this.common_messages.RemoveAt(i);
				}
			}
			for (int j = 0; j < this.session_messages.Count; j++)
			{
				AbstractMessage abstractMessage2 = this.session_messages[j];
				bool flag2 = false;
				for (int k = 0; k < this.merged_messages.Count; k++)
				{
					if (abstractMessage2.Equals(this.merged_messages[k]))
					{
						flag2 = true;
						break;
					}
				}
				if (!flag2)
				{
					this.merged_messages.Add(abstractMessage2);
				}
			}
		}

		// Token: 0x1700074A RID: 1866
		// (get) Token: 0x06001FAD RID: 8109 RVA: 0x0009B754 File Offset: 0x00099954
		public bool IsEmpty
		{
			get
			{
				return this.merged_messages == null && this.common_messages == null;
			}
		}

		// Token: 0x06001FAE RID: 8110 RVA: 0x0009B76C File Offset: 0x0009996C
		public bool Merge(ReportPrinter dest)
		{
			List<AbstractMessage> list = this.merged_messages;
			if (this.common_messages != null && this.common_messages.Count > 0)
			{
				list = this.common_messages;
			}
			if (list == null)
			{
				return false;
			}
			bool flag = false;
			foreach (AbstractMessage abstractMessage in list)
			{
				dest.Print(abstractMessage, this.showFullPaths);
				flag |= !abstractMessage.IsWarning;
			}
			if (this.reported_missing_definitions != null)
			{
				foreach (ITypeDefinition typeDefinition in this.reported_missing_definitions)
				{
					dest.MissingTypeReported(typeDefinition);
				}
			}
			return flag;
		}

		// Token: 0x06001FAF RID: 8111 RVA: 0x0009B5D9 File Offset: 0x000997D9
		public SessionReportPrinter()
		{
		}

		// Token: 0x04000B96 RID: 2966
		private List<AbstractMessage> session_messages;

		// Token: 0x04000B97 RID: 2967
		private List<AbstractMessage> common_messages;

		// Token: 0x04000B98 RID: 2968
		private List<AbstractMessage> merged_messages;

		// Token: 0x04000B99 RID: 2969
		private bool showFullPaths;
	}
}
