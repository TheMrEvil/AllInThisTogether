using System;

namespace Mono.CSharp
{
	// Token: 0x02000106 RID: 262
	public class HoistedStoreyClass : CompilerGeneratedContainer
	{
		// Token: 0x06000D20 RID: 3360 RVA: 0x0002F800 File Offset: 0x0002DA00
		public HoistedStoreyClass(TypeDefinition parent, MemberName name, TypeParameters tparams, Modifiers mods, MemberKind kind) : base(parent, name, mods | Modifiers.PRIVATE, kind)
		{
			if (tparams != null)
			{
				TypeParameters typeParameters = name.TypeParameters;
				TypeParameterSpec[] array = new TypeParameterSpec[tparams.Count];
				TypeParameterSpec[] array2 = new TypeParameterSpec[tparams.Count];
				for (int i = 0; i < tparams.Count; i++)
				{
					typeParameters[i] = tparams[i].CreateHoistedCopy(this.spec);
					array[i] = tparams[i].Type;
					array2[i] = typeParameters[i].Type;
				}
				TypeParameterInflator inflator = new TypeParameterInflator(this, null, array, array2);
				for (int j = 0; j < tparams.Count; j++)
				{
					array[j].InflateConstraints(inflator, array2[j]);
				}
				this.mutator = new TypeParameterMutator(tparams, typeParameters);
			}
		}

		// Token: 0x170003D2 RID: 978
		// (get) Token: 0x06000D21 RID: 3361 RVA: 0x0002F8CD File Offset: 0x0002DACD
		// (set) Token: 0x06000D22 RID: 3362 RVA: 0x0002F8D5 File Offset: 0x0002DAD5
		public TypeParameterMutator Mutator
		{
			get
			{
				return this.mutator;
			}
			set
			{
				this.mutator = value;
			}
		}

		// Token: 0x06000D23 RID: 3363 RVA: 0x0002F8E0 File Offset: 0x0002DAE0
		public HoistedStoreyClass GetGenericStorey()
		{
			TypeContainer typeContainer = this;
			while (typeContainer != null && typeContainer.CurrentTypeParameters == null)
			{
				typeContainer = typeContainer.Parent;
			}
			return typeContainer as HoistedStoreyClass;
		}

		// Token: 0x04000640 RID: 1600
		protected TypeParameterMutator mutator;

		// Token: 0x02000377 RID: 887
		public sealed class HoistedField : Field
		{
			// Token: 0x0600267E RID: 9854 RVA: 0x000B687C File Offset: 0x000B4A7C
			public HoistedField(HoistedStoreyClass parent, FullNamedExpression type, Modifiers mod, string name, Attributes attrs, Location loc) : base(parent, type, mod, new MemberName(name, loc), attrs)
			{
			}

			// Token: 0x0600267F RID: 9855 RVA: 0x000B6894 File Offset: 0x000B4A94
			protected override bool ResolveMemberType()
			{
				if (!base.ResolveMemberType())
				{
					return false;
				}
				HoistedStoreyClass genericStorey = ((HoistedStoreyClass)this.Parent).GetGenericStorey();
				if (genericStorey != null && genericStorey.Mutator != null)
				{
					this.member_type = genericStorey.Mutator.Mutate(base.MemberType);
				}
				return true;
			}
		}
	}
}
