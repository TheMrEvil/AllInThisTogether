using System;
using System.Collections;
using System.ComponentModel;
using System.Data.Common;
using System.Runtime.CompilerServices;

namespace System.Data
{
	// Token: 0x020000B4 RID: 180
	internal sealed class DataColumnPropertyDescriptor : PropertyDescriptor
	{
		// Token: 0x06000AF9 RID: 2809 RVA: 0x0002DD02 File Offset: 0x0002BF02
		internal DataColumnPropertyDescriptor(DataColumn dataColumn) : base(dataColumn.ColumnName, null)
		{
			this.Column = dataColumn;
		}

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x06000AFA RID: 2810 RVA: 0x0002DD18 File Offset: 0x0002BF18
		public override AttributeCollection Attributes
		{
			get
			{
				if (typeof(IList).IsAssignableFrom(this.PropertyType))
				{
					Attribute[] array = new Attribute[base.Attributes.Count + 1];
					base.Attributes.CopyTo(array, 0);
					array[array.Length - 1] = new ListBindableAttribute(false);
					return new AttributeCollection(array);
				}
				return base.Attributes;
			}
		}

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x06000AFB RID: 2811 RVA: 0x0002DD76 File Offset: 0x0002BF76
		internal DataColumn Column
		{
			[CompilerGenerated]
			get
			{
				return this.<Column>k__BackingField;
			}
		}

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x06000AFC RID: 2812 RVA: 0x0002DD7E File Offset: 0x0002BF7E
		public override Type ComponentType
		{
			get
			{
				return typeof(DataRowView);
			}
		}

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x06000AFD RID: 2813 RVA: 0x0002DD8A File Offset: 0x0002BF8A
		public override bool IsReadOnly
		{
			get
			{
				return this.Column.ReadOnly;
			}
		}

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x06000AFE RID: 2814 RVA: 0x0002DD97 File Offset: 0x0002BF97
		public override Type PropertyType
		{
			get
			{
				return this.Column.DataType;
			}
		}

		// Token: 0x06000AFF RID: 2815 RVA: 0x0002DDA4 File Offset: 0x0002BFA4
		public override bool Equals(object other)
		{
			return other is DataColumnPropertyDescriptor && ((DataColumnPropertyDescriptor)other).Column == this.Column;
		}

		// Token: 0x06000B00 RID: 2816 RVA: 0x0002DDC3 File Offset: 0x0002BFC3
		public override int GetHashCode()
		{
			return this.Column.GetHashCode();
		}

		// Token: 0x06000B01 RID: 2817 RVA: 0x0002DDD0 File Offset: 0x0002BFD0
		public override bool CanResetValue(object component)
		{
			DataRowView dataRowView = (DataRowView)component;
			if (!this.Column.IsSqlType)
			{
				return dataRowView.GetColumnValue(this.Column) != DBNull.Value;
			}
			return !DataStorage.IsObjectNull(dataRowView.GetColumnValue(this.Column));
		}

		// Token: 0x06000B02 RID: 2818 RVA: 0x0002DE1C File Offset: 0x0002C01C
		public override object GetValue(object component)
		{
			return ((DataRowView)component).GetColumnValue(this.Column);
		}

		// Token: 0x06000B03 RID: 2819 RVA: 0x0002DE2F File Offset: 0x0002C02F
		public override void ResetValue(object component)
		{
			((DataRowView)component).SetColumnValue(this.Column, DBNull.Value);
		}

		// Token: 0x06000B04 RID: 2820 RVA: 0x0002DE47 File Offset: 0x0002C047
		public override void SetValue(object component, object value)
		{
			((DataRowView)component).SetColumnValue(this.Column, value);
			this.OnValueChanged(component, EventArgs.Empty);
		}

		// Token: 0x06000B05 RID: 2821 RVA: 0x00006D64 File Offset: 0x00004F64
		public override bool ShouldSerializeValue(object component)
		{
			return false;
		}

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x06000B06 RID: 2822 RVA: 0x0002DE67 File Offset: 0x0002C067
		public override bool IsBrowsable
		{
			get
			{
				return this.Column.ColumnMapping != MappingType.Hidden && base.IsBrowsable;
			}
		}

		// Token: 0x0400079D RID: 1949
		[CompilerGenerated]
		private readonly DataColumn <Column>k__BackingField;
	}
}
