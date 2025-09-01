using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace System.Data
{
	// Token: 0x020000D1 RID: 209
	internal sealed class DataTablePropertyDescriptor : PropertyDescriptor
	{
		// Token: 0x17000221 RID: 545
		// (get) Token: 0x06000C93 RID: 3219 RVA: 0x00033762 File Offset: 0x00031962
		public DataTable Table
		{
			[CompilerGenerated]
			get
			{
				return this.<Table>k__BackingField;
			}
		}

		// Token: 0x06000C94 RID: 3220 RVA: 0x0003376A File Offset: 0x0003196A
		internal DataTablePropertyDescriptor(DataTable dataTable) : base(dataTable.TableName, null)
		{
			this.Table = dataTable;
		}

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x06000C95 RID: 3221 RVA: 0x0002DD7E File Offset: 0x0002BF7E
		public override Type ComponentType
		{
			get
			{
				return typeof(DataRowView);
			}
		}

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x06000C96 RID: 3222 RVA: 0x00006D64 File Offset: 0x00004F64
		public override bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x06000C97 RID: 3223 RVA: 0x00030768 File Offset: 0x0002E968
		public override Type PropertyType
		{
			get
			{
				return typeof(IBindingList);
			}
		}

		// Token: 0x06000C98 RID: 3224 RVA: 0x00033780 File Offset: 0x00031980
		public override bool Equals(object other)
		{
			return other is DataTablePropertyDescriptor && ((DataTablePropertyDescriptor)other).Table == this.Table;
		}

		// Token: 0x06000C99 RID: 3225 RVA: 0x0003379F File Offset: 0x0003199F
		public override int GetHashCode()
		{
			return this.Table.GetHashCode();
		}

		// Token: 0x06000C9A RID: 3226 RVA: 0x00006D64 File Offset: 0x00004F64
		public override bool CanResetValue(object component)
		{
			return false;
		}

		// Token: 0x06000C9B RID: 3227 RVA: 0x000337AC File Offset: 0x000319AC
		public override object GetValue(object component)
		{
			return ((DataViewManagerListItemTypeDescriptor)component).GetDataView(this.Table);
		}

		// Token: 0x06000C9C RID: 3228 RVA: 0x00007EED File Offset: 0x000060ED
		public override void ResetValue(object component)
		{
		}

		// Token: 0x06000C9D RID: 3229 RVA: 0x00007EED File Offset: 0x000060ED
		public override void SetValue(object component, object value)
		{
		}

		// Token: 0x06000C9E RID: 3230 RVA: 0x00006D64 File Offset: 0x00004F64
		public override bool ShouldSerializeValue(object component)
		{
			return false;
		}

		// Token: 0x0400080B RID: 2059
		[CompilerGenerated]
		private readonly DataTable <Table>k__BackingField;
	}
}
