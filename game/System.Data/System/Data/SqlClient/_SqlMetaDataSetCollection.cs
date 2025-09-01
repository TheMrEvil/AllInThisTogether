using System;
using System.Collections.Generic;

namespace System.Data.SqlClient
{
	// Token: 0x02000266 RID: 614
	internal sealed class _SqlMetaDataSetCollection
	{
		// Token: 0x06001CD7 RID: 7383 RVA: 0x00089492 File Offset: 0x00087692
		internal _SqlMetaDataSetCollection()
		{
			this._altMetaDataSetArray = new List<_SqlMetaDataSet>();
		}

		// Token: 0x06001CD8 RID: 7384 RVA: 0x000894A8 File Offset: 0x000876A8
		internal void SetAltMetaData(_SqlMetaDataSet altMetaDataSet)
		{
			int id = (int)altMetaDataSet.id;
			for (int i = 0; i < this._altMetaDataSetArray.Count; i++)
			{
				if ((int)this._altMetaDataSetArray[i].id == id)
				{
					this._altMetaDataSetArray[i] = altMetaDataSet;
					return;
				}
			}
			this._altMetaDataSetArray.Add(altMetaDataSet);
		}

		// Token: 0x06001CD9 RID: 7385 RVA: 0x00089500 File Offset: 0x00087700
		internal _SqlMetaDataSet GetAltMetaData(int id)
		{
			foreach (_SqlMetaDataSet sqlMetaDataSet in this._altMetaDataSetArray)
			{
				if ((int)sqlMetaDataSet.id == id)
				{
					return sqlMetaDataSet;
				}
			}
			return null;
		}

		// Token: 0x06001CDA RID: 7386 RVA: 0x0008955C File Offset: 0x0008775C
		public object Clone()
		{
			_SqlMetaDataSetCollection sqlMetaDataSetCollection = new _SqlMetaDataSetCollection();
			sqlMetaDataSetCollection.metaDataSet = ((this.metaDataSet == null) ? null : ((_SqlMetaDataSet)this.metaDataSet.Clone()));
			foreach (_SqlMetaDataSet sqlMetaDataSet in this._altMetaDataSetArray)
			{
				sqlMetaDataSetCollection._altMetaDataSetArray.Add((_SqlMetaDataSet)sqlMetaDataSet.Clone());
			}
			return sqlMetaDataSetCollection;
		}

		// Token: 0x040013F2 RID: 5106
		private readonly List<_SqlMetaDataSet> _altMetaDataSetArray;

		// Token: 0x040013F3 RID: 5107
		internal _SqlMetaDataSet metaDataSet;
	}
}
