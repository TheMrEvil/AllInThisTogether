using System;
using System.Collections.Generic;

namespace IKVM.Reflection.Reader
{
	// Token: 0x02000097 RID: 151
	internal sealed class GenericTypeParameter : TypeParameterType
	{
		// Token: 0x060007D1 RID: 2001 RVA: 0x0001985D File Offset: 0x00017A5D
		internal GenericTypeParameter(ModuleReader module, int index, byte sigElementType) : base(sigElementType)
		{
			this.module = module;
			this.index = index;
		}

		// Token: 0x060007D2 RID: 2002 RVA: 0x00019874 File Offset: 0x00017A74
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x060007D3 RID: 2003 RVA: 0x0001987D File Offset: 0x00017A7D
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x060007D4 RID: 2004 RVA: 0x00019885 File Offset: 0x00017A85
		public override string Namespace
		{
			get
			{
				return this.DeclaringType.Namespace;
			}
		}

		// Token: 0x170002AE RID: 686
		// (get) Token: 0x060007D5 RID: 2005 RVA: 0x00019892 File Offset: 0x00017A92
		public override string Name
		{
			get
			{
				return this.module.GetString(this.module.GenericParam.records[this.index].Name);
			}
		}

		// Token: 0x170002AF RID: 687
		// (get) Token: 0x060007D6 RID: 2006 RVA: 0x000198BF File Offset: 0x00017ABF
		public override Module Module
		{
			get
			{
				return this.module;
			}
		}

		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x060007D7 RID: 2007 RVA: 0x000198C7 File Offset: 0x00017AC7
		public override int MetadataToken
		{
			get
			{
				return (42 << 24) + this.index + 1;
			}
		}

		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x060007D8 RID: 2008 RVA: 0x000198D7 File Offset: 0x00017AD7
		public override int GenericParameterPosition
		{
			get
			{
				return (int)this.module.GenericParam.records[this.index].Number;
			}
		}

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x060007D9 RID: 2009 RVA: 0x000198FC File Offset: 0x00017AFC
		public override Type DeclaringType
		{
			get
			{
				int owner = this.module.GenericParam.records[this.index].Owner;
				if (owner >> 24 != 2)
				{
					return null;
				}
				return this.module.ResolveType(owner);
			}
		}

		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x060007DA RID: 2010 RVA: 0x00019940 File Offset: 0x00017B40
		public override MethodBase DeclaringMethod
		{
			get
			{
				int owner = this.module.GenericParam.records[this.index].Owner;
				if (owner >> 24 != 6)
				{
					return null;
				}
				return this.module.ResolveMethod(owner);
			}
		}

		// Token: 0x060007DB RID: 2011 RVA: 0x00019984 File Offset: 0x00017B84
		public override Type[] GetGenericParameterConstraints()
		{
			IGenericContext context = (this.DeclaringMethod as IGenericContext) ?? this.DeclaringType;
			List<Type> list = new List<Type>();
			foreach (int num in this.module.GenericParamConstraint.Filter(this.MetadataToken))
			{
				list.Add(this.module.ResolveType(this.module.GenericParamConstraint.records[num].Constraint, context));
			}
			return list.ToArray();
		}

		// Token: 0x060007DC RID: 2012 RVA: 0x00019A14 File Offset: 0x00017C14
		public override CustomModifiers[] __GetGenericParameterConstraintCustomModifiers()
		{
			IGenericContext context = (this.DeclaringMethod as IGenericContext) ?? this.DeclaringType;
			List<CustomModifiers> list = new List<CustomModifiers>();
			foreach (int num in this.module.GenericParamConstraint.Filter(this.MetadataToken))
			{
				CustomModifiers item = default(CustomModifiers);
				int constraint = this.module.GenericParamConstraint.records[num].Constraint;
				if (constraint >> 24 == 27)
				{
					int num2 = (constraint & 16777215) - 1;
					item = CustomModifiers.Read(this.module, this.module.GetBlob(this.module.TypeSpec.records[num2]), context);
				}
				list.Add(item);
			}
			return list.ToArray();
		}

		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x060007DD RID: 2013 RVA: 0x00019AE8 File Offset: 0x00017CE8
		public override GenericParameterAttributes GenericParameterAttributes
		{
			get
			{
				return (GenericParameterAttributes)this.module.GenericParam.records[this.index].Flags;
			}
		}

		// Token: 0x060007DE RID: 2014 RVA: 0x00019B0A File Offset: 0x00017D0A
		internal override Type BindTypeParameters(IGenericBinder binder)
		{
			if (this.module.GenericParam.records[this.index].Owner >> 24 == 6)
			{
				return binder.BindMethodParameter(this);
			}
			return binder.BindTypeParameter(this);
		}

		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x060007DF RID: 2015 RVA: 0x0000212D File Offset: 0x0000032D
		internal override bool IsBaked
		{
			get
			{
				return true;
			}
		}

		// Token: 0x04000314 RID: 788
		private readonly ModuleReader module;

		// Token: 0x04000315 RID: 789
		private readonly int index;
	}
}
