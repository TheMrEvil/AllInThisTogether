using System;
using System.Collections.ObjectModel;

namespace System.Net.Mail
{
	/// <summary>Stores linked resources to be sent as part of an email message.</summary>
	// Token: 0x0200082A RID: 2090
	public sealed class LinkedResourceCollection : Collection<LinkedResource>, IDisposable
	{
		// Token: 0x0600427C RID: 17020 RVA: 0x000E7937 File Offset: 0x000E5B37
		internal LinkedResourceCollection()
		{
		}

		/// <summary>Releases all resources used by the <see cref="T:System.Net.Mail.LinkedResourceCollection" />.</summary>
		// Token: 0x0600427D RID: 17021 RVA: 0x000E793F File Offset: 0x000E5B3F
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600427E RID: 17022 RVA: 0x00003917 File Offset: 0x00001B17
		private void Dispose(bool disposing)
		{
		}

		// Token: 0x0600427F RID: 17023 RVA: 0x000E794E File Offset: 0x000E5B4E
		protected override void ClearItems()
		{
			base.ClearItems();
		}

		// Token: 0x06004280 RID: 17024 RVA: 0x000E7956 File Offset: 0x000E5B56
		protected override void InsertItem(int index, LinkedResource item)
		{
			base.InsertItem(index, item);
		}

		// Token: 0x06004281 RID: 17025 RVA: 0x000E7960 File Offset: 0x000E5B60
		protected override void RemoveItem(int index)
		{
			base.RemoveItem(index);
		}

		// Token: 0x06004282 RID: 17026 RVA: 0x000E7969 File Offset: 0x000E5B69
		protected override void SetItem(int index, LinkedResource item)
		{
			base.SetItem(index, item);
		}
	}
}
