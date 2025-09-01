using System;
using System.Collections.Generic;
using System.Data.Common;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x02000030 RID: 48
	internal class SmiMetaDataPropertyCollection
	{
		// Token: 0x060001CA RID: 458 RVA: 0x00007DDC File Offset: 0x00005FDC
		private static SmiMetaDataPropertyCollection CreateEmptyInstance()
		{
			SmiMetaDataPropertyCollection smiMetaDataPropertyCollection = new SmiMetaDataPropertyCollection();
			smiMetaDataPropertyCollection.SetReadOnly();
			return smiMetaDataPropertyCollection;
		}

		// Token: 0x060001CB RID: 459 RVA: 0x00007DEC File Offset: 0x00005FEC
		internal SmiMetaDataPropertyCollection()
		{
			this._properties = new SmiMetaDataProperty[3];
			this._isReadOnly = false;
			this._properties[0] = SmiMetaDataPropertyCollection.s_emptyDefaultFields;
			this._properties[1] = SmiMetaDataPropertyCollection.s_emptySortOrder;
			this._properties[2] = SmiMetaDataPropertyCollection.s_emptyUniqueKey;
		}

		// Token: 0x17000075 RID: 117
		internal SmiMetaDataProperty this[SmiPropertySelector key]
		{
			get
			{
				return this._properties[(int)key];
			}
			set
			{
				if (value == null)
				{
					throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
				}
				this.EnsureWritable();
				this._properties[(int)key] = value;
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060001CE RID: 462 RVA: 0x00007E5F File Offset: 0x0000605F
		internal bool IsReadOnly
		{
			get
			{
				return this._isReadOnly;
			}
		}

		// Token: 0x060001CF RID: 463 RVA: 0x00007E67 File Offset: 0x00006067
		internal void SetReadOnly()
		{
			this._isReadOnly = true;
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x00007E70 File Offset: 0x00006070
		private void EnsureWritable()
		{
			if (this.IsReadOnly)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x00007E82 File Offset: 0x00006082
		// Note: this type is marked as 'beforefieldinit'.
		static SmiMetaDataPropertyCollection()
		{
		}

		// Token: 0x040004A8 RID: 1192
		private const int SelectorCount = 3;

		// Token: 0x040004A9 RID: 1193
		private SmiMetaDataProperty[] _properties;

		// Token: 0x040004AA RID: 1194
		private bool _isReadOnly;

		// Token: 0x040004AB RID: 1195
		private static readonly SmiDefaultFieldsProperty s_emptyDefaultFields = new SmiDefaultFieldsProperty(new List<bool>());

		// Token: 0x040004AC RID: 1196
		private static readonly SmiOrderProperty s_emptySortOrder = new SmiOrderProperty(new List<SmiOrderProperty.SmiColumnOrder>());

		// Token: 0x040004AD RID: 1197
		private static readonly SmiUniqueKeyProperty s_emptyUniqueKey = new SmiUniqueKeyProperty(new List<bool>());

		// Token: 0x040004AE RID: 1198
		internal static readonly SmiMetaDataPropertyCollection EmptyInstance = SmiMetaDataPropertyCollection.CreateEmptyInstance();
	}
}
