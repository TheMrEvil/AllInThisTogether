using System;

namespace System.Data.Odbc
{
	// Token: 0x020002A9 RID: 681
	internal sealed class DbCache
	{
		// Token: 0x06001F42 RID: 8002 RVA: 0x00093560 File Offset: 0x00091760
		internal DbCache(OdbcDataReader record, int count)
		{
			this._count = count;
			this._record = record;
			this._randomaccess = !record.IsBehavior(CommandBehavior.SequentialAccess);
			this._values = new object[count];
			this._isBadValue = new bool[count];
		}

		// Token: 0x1700058A RID: 1418
		internal object this[int i]
		{
			get
			{
				if (this._isBadValue[i])
				{
					OverflowException ex = (OverflowException)this.Values[i];
					throw new OverflowException(ex.Message, ex);
				}
				return this.Values[i];
			}
			set
			{
				this.Values[i] = value;
				this._isBadValue[i] = false;
			}
		}

		// Token: 0x1700058B RID: 1419
		// (get) Token: 0x06001F45 RID: 8005 RVA: 0x00093602 File Offset: 0x00091802
		internal int Count
		{
			get
			{
				return this._count;
			}
		}

		// Token: 0x06001F46 RID: 8006 RVA: 0x0009360A File Offset: 0x0009180A
		internal void InvalidateValue(int i)
		{
			this._isBadValue[i] = true;
		}

		// Token: 0x1700058C RID: 1420
		// (get) Token: 0x06001F47 RID: 8007 RVA: 0x00093615 File Offset: 0x00091815
		internal object[] Values
		{
			get
			{
				return this._values;
			}
		}

		// Token: 0x06001F48 RID: 8008 RVA: 0x00093620 File Offset: 0x00091820
		internal object AccessIndex(int i)
		{
			object[] values = this.Values;
			if (this._randomaccess)
			{
				for (int j = 0; j < i; j++)
				{
					if (values[j] == null)
					{
						values[j] = this._record.GetValue(j);
					}
				}
			}
			return values[i];
		}

		// Token: 0x06001F49 RID: 8009 RVA: 0x0009365F File Offset: 0x0009185F
		internal DbSchemaInfo GetSchema(int i)
		{
			if (this._schema == null)
			{
				this._schema = new DbSchemaInfo[this.Count];
			}
			if (this._schema[i] == null)
			{
				this._schema[i] = new DbSchemaInfo();
			}
			return this._schema[i];
		}

		// Token: 0x06001F4A RID: 8010 RVA: 0x0009369C File Offset: 0x0009189C
		internal void FlushValues()
		{
			int num = this._values.Length;
			for (int i = 0; i < num; i++)
			{
				this._values[i] = null;
			}
		}

		// Token: 0x040015AB RID: 5547
		private bool[] _isBadValue;

		// Token: 0x040015AC RID: 5548
		private DbSchemaInfo[] _schema;

		// Token: 0x040015AD RID: 5549
		private object[] _values;

		// Token: 0x040015AE RID: 5550
		private OdbcDataReader _record;

		// Token: 0x040015AF RID: 5551
		internal int _count;

		// Token: 0x040015B0 RID: 5552
		internal bool _randomaccess = true;
	}
}
