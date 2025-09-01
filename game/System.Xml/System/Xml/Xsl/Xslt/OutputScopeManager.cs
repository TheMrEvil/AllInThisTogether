using System;

namespace System.Xml.Xsl.Xslt
{
	// Token: 0x020003ED RID: 1005
	internal class OutputScopeManager
	{
		// Token: 0x060027CD RID: 10189 RVA: 0x000ECDDC File Offset: 0x000EAFDC
		public OutputScopeManager()
		{
			this.Reset();
		}

		// Token: 0x060027CE RID: 10190 RVA: 0x000ECDF7 File Offset: 0x000EAFF7
		public void Reset()
		{
			this.records[0].prefix = null;
			this.records[0].nsUri = null;
			this.PushScope();
		}

		// Token: 0x060027CF RID: 10191 RVA: 0x000ECE23 File Offset: 0x000EB023
		public void PushScope()
		{
			this.lastScopes++;
		}

		// Token: 0x060027D0 RID: 10192 RVA: 0x000ECE34 File Offset: 0x000EB034
		public void PopScope()
		{
			if (0 < this.lastScopes)
			{
				this.lastScopes--;
				return;
			}
			OutputScopeManager.ScopeReord[] array;
			int num;
			do
			{
				array = this.records;
				num = this.lastRecord - 1;
				this.lastRecord = num;
			}
			while (array[num].scopeCount == 0);
			this.lastScopes = this.records[this.lastRecord].scopeCount;
			this.lastScopes--;
		}

		// Token: 0x060027D1 RID: 10193 RVA: 0x000ECEA6 File Offset: 0x000EB0A6
		public void AddNamespace(string prefix, string uri)
		{
			this.AddRecord(prefix, uri);
		}

		// Token: 0x060027D2 RID: 10194 RVA: 0x000ECEB0 File Offset: 0x000EB0B0
		private void AddRecord(string prefix, string uri)
		{
			this.records[this.lastRecord].scopeCount = this.lastScopes;
			this.lastRecord++;
			if (this.lastRecord == this.records.Length)
			{
				OutputScopeManager.ScopeReord[] destinationArray = new OutputScopeManager.ScopeReord[this.lastRecord * 2];
				Array.Copy(this.records, 0, destinationArray, 0, this.lastRecord);
				this.records = destinationArray;
			}
			this.lastScopes = 0;
			this.records[this.lastRecord].prefix = prefix;
			this.records[this.lastRecord].nsUri = uri;
		}

		// Token: 0x060027D3 RID: 10195 RVA: 0x000ECF55 File Offset: 0x000EB155
		public void InvalidateAllPrefixes()
		{
			if (this.records[this.lastRecord].prefix == null)
			{
				return;
			}
			this.AddRecord(null, null);
		}

		// Token: 0x060027D4 RID: 10196 RVA: 0x000ECF78 File Offset: 0x000EB178
		public void InvalidateNonDefaultPrefixes()
		{
			string text = this.LookupNamespace(string.Empty);
			if (text == null)
			{
				this.InvalidateAllPrefixes();
				return;
			}
			if (this.records[this.lastRecord].prefix.Length == 0 && this.records[this.lastRecord - 1].prefix == null)
			{
				return;
			}
			this.AddRecord(null, null);
			this.AddRecord(string.Empty, text);
		}

		// Token: 0x060027D5 RID: 10197 RVA: 0x000ECFE8 File Offset: 0x000EB1E8
		public string LookupNamespace(string prefix)
		{
			int num = this.lastRecord;
			while (this.records[num].prefix != null)
			{
				if (this.records[num].prefix == prefix)
				{
					return this.records[num].nsUri;
				}
				num--;
			}
			return null;
		}

		// Token: 0x04001FA8 RID: 8104
		private OutputScopeManager.ScopeReord[] records = new OutputScopeManager.ScopeReord[32];

		// Token: 0x04001FA9 RID: 8105
		private int lastRecord;

		// Token: 0x04001FAA RID: 8106
		private int lastScopes;

		// Token: 0x020003EE RID: 1006
		public struct ScopeReord
		{
			// Token: 0x04001FAB RID: 8107
			public int scopeCount;

			// Token: 0x04001FAC RID: 8108
			public string prefix;

			// Token: 0x04001FAD RID: 8109
			public string nsUri;
		}
	}
}
