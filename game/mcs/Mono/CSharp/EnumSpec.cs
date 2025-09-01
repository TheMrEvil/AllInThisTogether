using System;

namespace Mono.CSharp
{
	// Token: 0x020001CA RID: 458
	internal class EnumSpec : TypeSpec
	{
		// Token: 0x0600181A RID: 6170 RVA: 0x000740FF File Offset: 0x000722FF
		public EnumSpec(TypeSpec declaringType, ITypeDefinition definition, TypeSpec underlyingType, Type info, Modifiers modifiers) : base(MemberKind.Enum, declaringType, definition, info, modifiers | Modifiers.SEALED)
		{
			this.underlying = underlyingType;
		}

		// Token: 0x170005B7 RID: 1463
		// (get) Token: 0x0600181B RID: 6171 RVA: 0x0007411C File Offset: 0x0007231C
		// (set) Token: 0x0600181C RID: 6172 RVA: 0x00074124 File Offset: 0x00072324
		public TypeSpec UnderlyingType
		{
			get
			{
				return this.underlying;
			}
			set
			{
				if (this.underlying != null)
				{
					throw new InternalErrorException("UnderlyingType reset");
				}
				this.underlying = value;
			}
		}

		// Token: 0x0600181D RID: 6173 RVA: 0x00074140 File Offset: 0x00072340
		public static TypeSpec GetUnderlyingType(TypeSpec t)
		{
			return ((EnumSpec)t.GetDefinition()).UnderlyingType;
		}

		// Token: 0x0600181E RID: 6174 RVA: 0x00074154 File Offset: 0x00072354
		public static bool IsValidUnderlyingType(TypeSpec type)
		{
			switch (type.BuiltinType)
			{
			case BuiltinTypeSpec.Type.Byte:
			case BuiltinTypeSpec.Type.SByte:
			case BuiltinTypeSpec.Type.Short:
			case BuiltinTypeSpec.Type.UShort:
			case BuiltinTypeSpec.Type.Int:
			case BuiltinTypeSpec.Type.UInt:
			case BuiltinTypeSpec.Type.Long:
			case BuiltinTypeSpec.Type.ULong:
				return true;
			}
			return false;
		}

		// Token: 0x04000991 RID: 2449
		private TypeSpec underlying;
	}
}
