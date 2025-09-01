using System;
using System.Collections.ObjectModel;

namespace System.Net.Mail
{
	/// <summary>Stores attachments to be sent as part of an email message.</summary>
	// Token: 0x02000827 RID: 2087
	public sealed class AttachmentCollection : Collection<Attachment>, IDisposable
	{
		// Token: 0x0600426B RID: 17003 RVA: 0x000E77EA File Offset: 0x000E59EA
		internal AttachmentCollection()
		{
		}

		/// <summary>Releases all resources used by the <see cref="T:System.Net.Mail.AttachmentCollection" />.</summary>
		// Token: 0x0600426C RID: 17004 RVA: 0x000E77F4 File Offset: 0x000E59F4
		public void Dispose()
		{
			for (int i = 0; i < base.Count; i++)
			{
				base[i].Dispose();
			}
		}

		// Token: 0x0600426D RID: 17005 RVA: 0x000E781E File Offset: 0x000E5A1E
		protected override void ClearItems()
		{
			base.ClearItems();
		}

		// Token: 0x0600426E RID: 17006 RVA: 0x000E7826 File Offset: 0x000E5A26
		protected override void InsertItem(int index, Attachment item)
		{
			base.InsertItem(index, item);
		}

		// Token: 0x0600426F RID: 17007 RVA: 0x000E7830 File Offset: 0x000E5A30
		protected override void RemoveItem(int index)
		{
			base.RemoveItem(index);
		}

		// Token: 0x06004270 RID: 17008 RVA: 0x000E7839 File Offset: 0x000E5A39
		protected override void SetItem(int index, Attachment item)
		{
			base.SetItem(index, item);
		}
	}
}
