using System;
using System.Runtime.CompilerServices;

namespace Mono.CSharp
{
	// Token: 0x020002E3 RID: 739
	public abstract class ElementTypeSpec : TypeSpec, ITypeDefinition, IMemberDefinition
	{
		// Token: 0x0600233D RID: 9021 RVA: 0x000ACA04 File Offset: 0x000AAC04
		protected ElementTypeSpec(MemberKind kind, TypeSpec element, Type info) : base(kind, element.DeclaringType, null, info, element.Modifiers)
		{
			this.Element = element;
			this.state &= ~(MemberSpec.StateFlags.Obsolete_Undetected | MemberSpec.StateFlags.Obsolete | MemberSpec.StateFlags.CLSCompliant_Undetected | MemberSpec.StateFlags.CLSCompliant | MemberSpec.StateFlags.MissingDependency_Undetected | MemberSpec.StateFlags.MissingDependency | MemberSpec.StateFlags.HasDynamicElement);
			this.state |= (element.state & (MemberSpec.StateFlags.Obsolete_Undetected | MemberSpec.StateFlags.Obsolete | MemberSpec.StateFlags.CLSCompliant_Undetected | MemberSpec.StateFlags.CLSCompliant | MemberSpec.StateFlags.MissingDependency_Undetected | MemberSpec.StateFlags.MissingDependency | MemberSpec.StateFlags.HasDynamicElement));
			if (element.BuiltinType == BuiltinTypeSpec.Type.Dynamic)
			{
				this.state |= MemberSpec.StateFlags.HasDynamicElement;
			}
			this.definition = this;
			this.cache = MemberCache.Empty;
		}

		// Token: 0x17000824 RID: 2084
		// (get) Token: 0x0600233E RID: 9022 RVA: 0x000ACA7D File Offset: 0x000AAC7D
		// (set) Token: 0x0600233F RID: 9023 RVA: 0x000ACA85 File Offset: 0x000AAC85
		public TypeSpec Element
		{
			[CompilerGenerated]
			get
			{
				return this.<Element>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Element>k__BackingField = value;
			}
		}

		// Token: 0x17000825 RID: 2085
		// (get) Token: 0x06002340 RID: 9024 RVA: 0x000022F4 File Offset: 0x000004F4
		bool ITypeDefinition.IsComImport
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000826 RID: 2086
		// (get) Token: 0x06002341 RID: 9025 RVA: 0x000022F4 File Offset: 0x000004F4
		bool ITypeDefinition.IsPartial
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000827 RID: 2087
		// (get) Token: 0x06002342 RID: 9026 RVA: 0x000022F4 File Offset: 0x000004F4
		bool ITypeDefinition.IsTypeForwarder
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000828 RID: 2088
		// (get) Token: 0x06002343 RID: 9027 RVA: 0x000022F4 File Offset: 0x000004F4
		bool ITypeDefinition.IsCyclicTypeForwarder
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000829 RID: 2089
		// (get) Token: 0x06002344 RID: 9028 RVA: 0x0000225C File Offset: 0x0000045C
		public override string Name
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06002345 RID: 9029 RVA: 0x000ACA8E File Offset: 0x000AAC8E
		public override ObsoleteAttribute GetAttributeObsolete()
		{
			return this.Element.GetAttributeObsolete();
		}

		// Token: 0x06002346 RID: 9030 RVA: 0x000055E7 File Offset: 0x000037E7
		protected virtual string GetPostfixSignature()
		{
			return null;
		}

		// Token: 0x06002347 RID: 9031 RVA: 0x000ACA9B File Offset: 0x000AAC9B
		public override string GetSignatureForDocumentation(bool explicitName)
		{
			return this.Element.GetSignatureForDocumentation(explicitName) + this.GetPostfixSignature();
		}

		// Token: 0x06002348 RID: 9032 RVA: 0x000ACAB4 File Offset: 0x000AACB4
		public override string GetSignatureForError()
		{
			return this.Element.GetSignatureForError() + this.GetPostfixSignature();
		}

		// Token: 0x06002349 RID: 9033 RVA: 0x000ACACC File Offset: 0x000AACCC
		public override TypeSpec Mutate(TypeParameterMutator mutator)
		{
			TypeSpec typeSpec = this.Element.Mutate(mutator);
			if (typeSpec == this.Element)
			{
				return this;
			}
			ElementTypeSpec elementTypeSpec = (ElementTypeSpec)base.MemberwiseClone();
			elementTypeSpec.Element = typeSpec;
			elementTypeSpec.info = null;
			return elementTypeSpec;
		}

		// Token: 0x1700082A RID: 2090
		// (get) Token: 0x0600234A RID: 9034 RVA: 0x000ACB0A File Offset: 0x000AAD0A
		IAssemblyDefinition ITypeDefinition.DeclaringAssembly
		{
			get
			{
				return this.Element.MemberDefinition.DeclaringAssembly;
			}
		}

		// Token: 0x0600234B RID: 9035 RVA: 0x000ACB1C File Offset: 0x000AAD1C
		bool ITypeDefinition.IsInternalAsPublic(IAssemblyDefinition assembly)
		{
			return this.Element.MemberDefinition.IsInternalAsPublic(assembly);
		}

		// Token: 0x1700082B RID: 2091
		// (get) Token: 0x0600234C RID: 9036 RVA: 0x00023DF4 File Offset: 0x00021FF4
		public string Namespace
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700082C RID: 2092
		// (get) Token: 0x0600234D RID: 9037 RVA: 0x000022F4 File Offset: 0x000004F4
		public int TypeParametersCount
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x1700082D RID: 2093
		// (get) Token: 0x0600234E RID: 9038 RVA: 0x0000225C File Offset: 0x0000045C
		public TypeParameterSpec[] TypeParameters
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x0600234F RID: 9039 RVA: 0x000ACB2F File Offset: 0x000AAD2F
		public TypeSpec GetAttributeCoClass()
		{
			return this.Element.MemberDefinition.GetAttributeCoClass();
		}

		// Token: 0x06002350 RID: 9040 RVA: 0x000ACB41 File Offset: 0x000AAD41
		public string GetAttributeDefaultMember()
		{
			return this.Element.MemberDefinition.GetAttributeDefaultMember();
		}

		// Token: 0x06002351 RID: 9041 RVA: 0x000ACB53 File Offset: 0x000AAD53
		public void LoadMembers(TypeSpec declaringType, bool onlyTypes, ref MemberCache cache)
		{
			this.Element.MemberDefinition.LoadMembers(declaringType, onlyTypes, ref cache);
		}

		// Token: 0x1700082E RID: 2094
		// (get) Token: 0x06002352 RID: 9042 RVA: 0x000ACB68 File Offset: 0x000AAD68
		public bool IsImported
		{
			get
			{
				return this.Element.MemberDefinition.IsImported;
			}
		}

		// Token: 0x06002353 RID: 9043 RVA: 0x000ACB7A File Offset: 0x000AAD7A
		public string[] ConditionalConditions()
		{
			return this.Element.MemberDefinition.ConditionalConditions();
		}

		// Token: 0x1700082F RID: 2095
		// (get) Token: 0x06002354 RID: 9044 RVA: 0x000ACB8C File Offset: 0x000AAD8C
		bool? IMemberDefinition.CLSAttributeValue
		{
			get
			{
				return this.Element.MemberDefinition.CLSAttributeValue;
			}
		}

		// Token: 0x06002355 RID: 9045 RVA: 0x000ACB9E File Offset: 0x000AAD9E
		public void SetIsAssigned()
		{
			this.Element.MemberDefinition.SetIsAssigned();
		}

		// Token: 0x06002356 RID: 9046 RVA: 0x000ACBB0 File Offset: 0x000AADB0
		public void SetIsUsed()
		{
			this.Element.MemberDefinition.SetIsUsed();
		}

		// Token: 0x04000D7A RID: 3450
		[CompilerGenerated]
		private TypeSpec <Element>k__BackingField;
	}
}
