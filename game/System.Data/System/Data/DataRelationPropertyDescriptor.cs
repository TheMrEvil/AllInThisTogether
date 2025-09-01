using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace System.Data
{
	// Token: 0x020000BC RID: 188
	internal sealed class DataRelationPropertyDescriptor : PropertyDescriptor
	{
		// Token: 0x06000B90 RID: 2960 RVA: 0x0003074A File Offset: 0x0002E94A
		internal DataRelationPropertyDescriptor(DataRelation dataRelation) : base(dataRelation.RelationName, null)
		{
			this.Relation = dataRelation;
		}

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x06000B91 RID: 2961 RVA: 0x00030760 File Offset: 0x0002E960
		internal DataRelation Relation
		{
			[CompilerGenerated]
			get
			{
				return this.<Relation>k__BackingField;
			}
		}

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x06000B92 RID: 2962 RVA: 0x0002DD7E File Offset: 0x0002BF7E
		public override Type ComponentType
		{
			get
			{
				return typeof(DataRowView);
			}
		}

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x06000B93 RID: 2963 RVA: 0x00006D64 File Offset: 0x00004F64
		public override bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x06000B94 RID: 2964 RVA: 0x00030768 File Offset: 0x0002E968
		public override Type PropertyType
		{
			get
			{
				return typeof(IBindingList);
			}
		}

		// Token: 0x06000B95 RID: 2965 RVA: 0x00030774 File Offset: 0x0002E974
		public override bool Equals(object other)
		{
			return other is DataRelationPropertyDescriptor && ((DataRelationPropertyDescriptor)other).Relation == this.Relation;
		}

		// Token: 0x06000B96 RID: 2966 RVA: 0x00030793 File Offset: 0x0002E993
		public override int GetHashCode()
		{
			return this.Relation.GetHashCode();
		}

		// Token: 0x06000B97 RID: 2967 RVA: 0x00006D64 File Offset: 0x00004F64
		public override bool CanResetValue(object component)
		{
			return false;
		}

		// Token: 0x06000B98 RID: 2968 RVA: 0x000307A0 File Offset: 0x0002E9A0
		public override object GetValue(object component)
		{
			return ((DataRowView)component).CreateChildView(this.Relation);
		}

		// Token: 0x06000B99 RID: 2969 RVA: 0x00007EED File Offset: 0x000060ED
		public override void ResetValue(object component)
		{
		}

		// Token: 0x06000B9A RID: 2970 RVA: 0x00007EED File Offset: 0x000060ED
		public override void SetValue(object component, object value)
		{
		}

		// Token: 0x06000B9B RID: 2971 RVA: 0x00006D64 File Offset: 0x00004F64
		public override bool ShouldSerializeValue(object component)
		{
			return false;
		}

		// Token: 0x040007C6 RID: 1990
		[CompilerGenerated]
		private readonly DataRelation <Relation>k__BackingField;
	}
}
