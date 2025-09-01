using System;
using IKVM.Reflection.Reader;

namespace IKVM.Reflection
{
	// Token: 0x02000047 RID: 71
	internal sealed class MissingTypeParameter : TypeParameterType
	{
		// Token: 0x060002F1 RID: 753 RVA: 0x0000A054 File Offset: 0x00008254
		internal MissingTypeParameter(Type owner, int index) : this(owner, index, 19)
		{
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x0000A060 File Offset: 0x00008260
		internal MissingTypeParameter(MethodInfo owner, int index) : this(owner, index, 30)
		{
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x0000A06C File Offset: 0x0000826C
		private MissingTypeParameter(MemberInfo owner, int index, byte sigElementType) : base(sigElementType)
		{
			this.owner = owner;
			this.index = index;
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x060002F4 RID: 756 RVA: 0x0000A083 File Offset: 0x00008283
		public override Module Module
		{
			get
			{
				return this.owner.Module;
			}
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x060002F5 RID: 757 RVA: 0x000055E7 File Offset: 0x000037E7
		public override string Name
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x060002F6 RID: 758 RVA: 0x0000A090 File Offset: 0x00008290
		public override int GenericParameterPosition
		{
			get
			{
				return this.index;
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x060002F7 RID: 759 RVA: 0x0000A098 File Offset: 0x00008298
		public override MethodBase DeclaringMethod
		{
			get
			{
				return this.owner as MethodBase;
			}
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x060002F8 RID: 760 RVA: 0x0000A0A5 File Offset: 0x000082A5
		public override Type DeclaringType
		{
			get
			{
				return this.owner as Type;
			}
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x0000A0B2 File Offset: 0x000082B2
		internal override Type BindTypeParameters(IGenericBinder binder)
		{
			if (this.owner is MethodBase)
			{
				return binder.BindMethodParameter(this);
			}
			return binder.BindTypeParameter(this);
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x060002FA RID: 762 RVA: 0x0000A0D0 File Offset: 0x000082D0
		internal override bool IsBaked
		{
			get
			{
				return this.owner.IsBaked;
			}
		}

		// Token: 0x0400017B RID: 379
		private readonly MemberInfo owner;

		// Token: 0x0400017C RID: 380
		private readonly int index;
	}
}
