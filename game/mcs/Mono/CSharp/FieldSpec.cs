using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

namespace Mono.CSharp
{
	// Token: 0x020002D9 RID: 729
	public class FieldSpec : MemberSpec, IInterfaceMemberSpec
	{
		// Token: 0x060022AE RID: 8878 RVA: 0x000AAC18 File Offset: 0x000A8E18
		public FieldSpec(TypeSpec declaringType, IMemberDefinition definition, TypeSpec memberType, FieldInfo info, Modifiers modifiers) : base(MemberKind.Field, declaringType, definition, modifiers)
		{
			this.metaInfo = info;
			this.memberType = memberType;
		}

		// Token: 0x170007E8 RID: 2024
		// (get) Token: 0x060022AF RID: 8879 RVA: 0x000AAC34 File Offset: 0x000A8E34
		public bool IsReadOnly
		{
			get
			{
				return (base.Modifiers & Modifiers.READONLY) > (Modifiers)0;
			}
		}

		// Token: 0x170007E9 RID: 2025
		// (get) Token: 0x060022B0 RID: 8880 RVA: 0x000AAC45 File Offset: 0x000A8E45
		public TypeSpec MemberType
		{
			get
			{
				return this.memberType;
			}
		}

		// Token: 0x060022B1 RID: 8881 RVA: 0x000AAC50 File Offset: 0x000A8E50
		public FieldInfo GetMetaInfo()
		{
			if ((this.state & MemberSpec.StateFlags.PendingMetaInflate) != (MemberSpec.StateFlags)0)
			{
				Type type = base.DeclaringType.GetMetaInfo();
				if (base.DeclaringType.IsTypeBuilder)
				{
					this.metaInfo = TypeBuilder.GetField(type, this.metaInfo);
				}
				else
				{
					int metadataToken = this.metaInfo.MetadataToken;
					this.metaInfo = type.GetField(this.Name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
					if (this.metaInfo.MetadataToken != metadataToken)
					{
						throw new NotImplementedException("Resolved to wrong meta token");
					}
				}
				this.state &= ~MemberSpec.StateFlags.PendingMetaInflate;
			}
			return this.metaInfo;
		}

		// Token: 0x060022B2 RID: 8882 RVA: 0x000AACE9 File Offset: 0x000A8EE9
		public override MemberSpec InflateMember(TypeParameterInflator inflator)
		{
			FieldSpec fieldSpec = (FieldSpec)base.InflateMember(inflator);
			fieldSpec.memberType = inflator.Inflate(this.memberType);
			return fieldSpec;
		}

		// Token: 0x060022B3 RID: 8883 RVA: 0x000AAD0C File Offset: 0x000A8F0C
		public FieldSpec Mutate(TypeParameterMutator mutator)
		{
			TypeSpec typeSpec = base.DeclaringType;
			if (base.DeclaringType.IsGenericOrParentIsGeneric)
			{
				typeSpec = mutator.Mutate(typeSpec);
			}
			if (typeSpec == base.DeclaringType)
			{
				return this;
			}
			FieldSpec fieldSpec = (FieldSpec)base.MemberwiseClone();
			fieldSpec.declaringType = typeSpec;
			fieldSpec.state |= MemberSpec.StateFlags.PendingMetaInflate;
			fieldSpec.metaInfo = MemberCache.GetMember<FieldSpec>(TypeParameterMutator.GetMemberDeclaringType(base.DeclaringType), this).metaInfo;
			return fieldSpec;
		}

		// Token: 0x060022B4 RID: 8884 RVA: 0x000AAD80 File Offset: 0x000A8F80
		public override List<MissingTypeSpecReference> ResolveMissingDependencies(MemberSpec caller)
		{
			return this.memberType.ResolveMissingDependencies(this);
		}

		// Token: 0x04000D5C RID: 3420
		private FieldInfo metaInfo;

		// Token: 0x04000D5D RID: 3421
		private TypeSpec memberType;
	}
}
