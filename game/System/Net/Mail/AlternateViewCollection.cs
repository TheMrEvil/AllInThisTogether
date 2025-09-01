using System;
using System.Collections.ObjectModel;

namespace System.Net.Mail
{
	/// <summary>Represents a collection of <see cref="T:System.Net.Mail.AlternateView" /> objects.</summary>
	// Token: 0x02000823 RID: 2083
	public sealed class AlternateViewCollection : Collection<AlternateView>, IDisposable
	{
		// Token: 0x06004244 RID: 16964 RVA: 0x000E51E0 File Offset: 0x000E33E0
		internal AlternateViewCollection()
		{
		}

		/// <summary>Releases all resources used by the <see cref="T:System.Net.Mail.AlternateViewCollection" />.</summary>
		// Token: 0x06004245 RID: 16965 RVA: 0x00003917 File Offset: 0x00001B17
		public void Dispose()
		{
		}

		// Token: 0x06004246 RID: 16966 RVA: 0x000E51E8 File Offset: 0x000E33E8
		protected override void ClearItems()
		{
			base.ClearItems();
		}

		// Token: 0x06004247 RID: 16967 RVA: 0x000E51F0 File Offset: 0x000E33F0
		protected override void InsertItem(int index, AlternateView item)
		{
			base.InsertItem(index, item);
		}

		// Token: 0x06004248 RID: 16968 RVA: 0x000E51FA File Offset: 0x000E33FA
		protected override void RemoveItem(int index)
		{
			base.RemoveItem(index);
		}

		// Token: 0x06004249 RID: 16969 RVA: 0x000E5203 File Offset: 0x000E3403
		protected override void SetItem(int index, AlternateView item)
		{
			base.SetItem(index, item);
		}
	}
}
