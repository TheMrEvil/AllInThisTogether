using System;
using System.Collections.ObjectModel;
using System.Globalization;

namespace System.Data.ProviderBase
{
	// Token: 0x02000344 RID: 836
	internal sealed class FieldNameLookup : BasicFieldNameLookup
	{
		// Token: 0x06002650 RID: 9808 RVA: 0x000AA745 File Offset: 0x000A8945
		public FieldNameLookup(string[] fieldNames, int defaultLocaleID) : base(fieldNames)
		{
			this._defaultLocaleID = defaultLocaleID;
		}

		// Token: 0x06002651 RID: 9809 RVA: 0x000AA755 File Offset: 0x000A8955
		public FieldNameLookup(ReadOnlyCollection<string> columnNames, int defaultLocaleID) : base(columnNames)
		{
			this._defaultLocaleID = defaultLocaleID;
		}

		// Token: 0x06002652 RID: 9810 RVA: 0x000AA765 File Offset: 0x000A8965
		public FieldNameLookup(IDataReader reader, int defaultLocaleID) : base(reader)
		{
			this._defaultLocaleID = defaultLocaleID;
		}

		// Token: 0x06002653 RID: 9811 RVA: 0x000AA778 File Offset: 0x000A8978
		protected override CompareInfo GetCompareInfo()
		{
			CompareInfo compareInfo = null;
			if (-1 != this._defaultLocaleID)
			{
				compareInfo = CompareInfo.GetCompareInfo(this._defaultLocaleID);
			}
			if (compareInfo == null)
			{
				compareInfo = base.GetCompareInfo();
			}
			return compareInfo;
		}

		// Token: 0x0400192C RID: 6444
		private readonly int _defaultLocaleID;
	}
}
