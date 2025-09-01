using System;
using System.Xml.Schema;
using System.Xml.Serialization.Advanced;

namespace System.Xml.Serialization
{
	// Token: 0x020002BD RID: 701
	internal class TypeDesc
	{
		// Token: 0x06001A71 RID: 6769 RVA: 0x00098F7C File Offset: 0x0009717C
		internal TypeDesc(string name, string fullName, XmlSchemaType dataType, TypeKind kind, TypeDesc baseTypeDesc, TypeFlags flags, string formatterName)
		{
			this.name = name.Replace('+', '.');
			this.fullName = fullName.Replace('+', '.');
			this.kind = kind;
			this.baseTypeDesc = baseTypeDesc;
			this.flags = flags;
			this.isXsdType = (kind == TypeKind.Primitive);
			if (this.isXsdType)
			{
				this.weight = 1;
			}
			else if (kind == TypeKind.Enum)
			{
				this.weight = 2;
			}
			else if (this.kind == TypeKind.Root)
			{
				this.weight = -1;
			}
			else
			{
				this.weight = ((baseTypeDesc == null) ? 0 : (baseTypeDesc.Weight + 1));
			}
			this.dataType = dataType;
			this.formatterName = formatterName;
		}

		// Token: 0x06001A72 RID: 6770 RVA: 0x00099027 File Offset: 0x00097227
		internal TypeDesc(string name, string fullName, XmlSchemaType dataType, TypeKind kind, TypeDesc baseTypeDesc, TypeFlags flags) : this(name, fullName, dataType, kind, baseTypeDesc, flags, null)
		{
		}

		// Token: 0x06001A73 RID: 6771 RVA: 0x00099039 File Offset: 0x00097239
		internal TypeDesc(string name, string fullName, TypeKind kind, TypeDesc baseTypeDesc, TypeFlags flags) : this(name, fullName, null, kind, baseTypeDesc, flags, null)
		{
		}

		// Token: 0x06001A74 RID: 6772 RVA: 0x0009904A File Offset: 0x0009724A
		internal TypeDesc(Type type, bool isXsdType, XmlSchemaType dataType, string formatterName, TypeFlags flags) : this(type.Name, type.FullName, dataType, TypeKind.Primitive, null, flags, formatterName)
		{
			this.isXsdType = isXsdType;
			this.type = type;
		}

		// Token: 0x06001A75 RID: 6773 RVA: 0x00099073 File Offset: 0x00097273
		internal TypeDesc(Type type, string name, string fullName, TypeKind kind, TypeDesc baseTypeDesc, TypeFlags flags, TypeDesc arrayElementTypeDesc) : this(name, fullName, null, kind, baseTypeDesc, flags, null)
		{
			this.arrayElementTypeDesc = arrayElementTypeDesc;
			this.type = type;
		}

		// Token: 0x06001A76 RID: 6774 RVA: 0x00099094 File Offset: 0x00097294
		public override string ToString()
		{
			return this.fullName;
		}

		// Token: 0x17000502 RID: 1282
		// (get) Token: 0x06001A77 RID: 6775 RVA: 0x0009909C File Offset: 0x0009729C
		internal TypeFlags Flags
		{
			get
			{
				return this.flags;
			}
		}

		// Token: 0x17000503 RID: 1283
		// (get) Token: 0x06001A78 RID: 6776 RVA: 0x000990A4 File Offset: 0x000972A4
		internal bool IsXsdType
		{
			get
			{
				return this.isXsdType;
			}
		}

		// Token: 0x17000504 RID: 1284
		// (get) Token: 0x06001A79 RID: 6777 RVA: 0x000990AC File Offset: 0x000972AC
		internal bool IsMappedType
		{
			get
			{
				return this.extendedType != null;
			}
		}

		// Token: 0x17000505 RID: 1285
		// (get) Token: 0x06001A7A RID: 6778 RVA: 0x000990B7 File Offset: 0x000972B7
		internal MappedTypeDesc ExtendedType
		{
			get
			{
				return this.extendedType;
			}
		}

		// Token: 0x17000506 RID: 1286
		// (get) Token: 0x06001A7B RID: 6779 RVA: 0x000990BF File Offset: 0x000972BF
		internal string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000507 RID: 1287
		// (get) Token: 0x06001A7C RID: 6780 RVA: 0x00099094 File Offset: 0x00097294
		internal string FullName
		{
			get
			{
				return this.fullName;
			}
		}

		// Token: 0x17000508 RID: 1288
		// (get) Token: 0x06001A7D RID: 6781 RVA: 0x000990C7 File Offset: 0x000972C7
		internal string CSharpName
		{
			get
			{
				if (this.cSharpName == null)
				{
					this.cSharpName = ((this.type == null) ? CodeIdentifier.GetCSharpName(this.fullName) : CodeIdentifier.GetCSharpName(this.type));
				}
				return this.cSharpName;
			}
		}

		// Token: 0x17000509 RID: 1289
		// (get) Token: 0x06001A7E RID: 6782 RVA: 0x00099103 File Offset: 0x00097303
		internal XmlSchemaType DataType
		{
			get
			{
				return this.dataType;
			}
		}

		// Token: 0x1700050A RID: 1290
		// (get) Token: 0x06001A7F RID: 6783 RVA: 0x0009910B File Offset: 0x0009730B
		internal Type Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x1700050B RID: 1291
		// (get) Token: 0x06001A80 RID: 6784 RVA: 0x00099113 File Offset: 0x00097313
		internal string FormatterName
		{
			get
			{
				return this.formatterName;
			}
		}

		// Token: 0x1700050C RID: 1292
		// (get) Token: 0x06001A81 RID: 6785 RVA: 0x0009911B File Offset: 0x0009731B
		internal TypeKind Kind
		{
			get
			{
				return this.kind;
			}
		}

		// Token: 0x1700050D RID: 1293
		// (get) Token: 0x06001A82 RID: 6786 RVA: 0x00099123 File Offset: 0x00097323
		internal bool IsValueType
		{
			get
			{
				return (this.flags & TypeFlags.Reference) == TypeFlags.None;
			}
		}

		// Token: 0x1700050E RID: 1294
		// (get) Token: 0x06001A83 RID: 6787 RVA: 0x00099130 File Offset: 0x00097330
		internal bool CanBeAttributeValue
		{
			get
			{
				return (this.flags & TypeFlags.CanBeAttributeValue) > TypeFlags.None;
			}
		}

		// Token: 0x1700050F RID: 1295
		// (get) Token: 0x06001A84 RID: 6788 RVA: 0x0009913D File Offset: 0x0009733D
		internal bool XmlEncodingNotRequired
		{
			get
			{
				return (this.flags & TypeFlags.XmlEncodingNotRequired) > TypeFlags.None;
			}
		}

		// Token: 0x17000510 RID: 1296
		// (get) Token: 0x06001A85 RID: 6789 RVA: 0x0009914E File Offset: 0x0009734E
		internal bool CanBeElementValue
		{
			get
			{
				return (this.flags & TypeFlags.CanBeElementValue) > TypeFlags.None;
			}
		}

		// Token: 0x17000511 RID: 1297
		// (get) Token: 0x06001A86 RID: 6790 RVA: 0x0009915C File Offset: 0x0009735C
		internal bool CanBeTextValue
		{
			get
			{
				return (this.flags & TypeFlags.CanBeTextValue) > TypeFlags.None;
			}
		}

		// Token: 0x17000512 RID: 1298
		// (get) Token: 0x06001A87 RID: 6791 RVA: 0x0009916A File Offset: 0x0009736A
		// (set) Token: 0x06001A88 RID: 6792 RVA: 0x0009917C File Offset: 0x0009737C
		internal bool IsMixed
		{
			get
			{
				return this.isMixed || this.CanBeTextValue;
			}
			set
			{
				this.isMixed = value;
			}
		}

		// Token: 0x17000513 RID: 1299
		// (get) Token: 0x06001A89 RID: 6793 RVA: 0x00099185 File Offset: 0x00097385
		internal bool IsSpecial
		{
			get
			{
				return (this.flags & TypeFlags.Special) > TypeFlags.None;
			}
		}

		// Token: 0x17000514 RID: 1300
		// (get) Token: 0x06001A8A RID: 6794 RVA: 0x00099192 File Offset: 0x00097392
		internal bool IsAmbiguousDataType
		{
			get
			{
				return (this.flags & TypeFlags.AmbiguousDataType) > TypeFlags.None;
			}
		}

		// Token: 0x17000515 RID: 1301
		// (get) Token: 0x06001A8B RID: 6795 RVA: 0x000991A3 File Offset: 0x000973A3
		internal bool HasCustomFormatter
		{
			get
			{
				return (this.flags & TypeFlags.HasCustomFormatter) > TypeFlags.None;
			}
		}

		// Token: 0x17000516 RID: 1302
		// (get) Token: 0x06001A8C RID: 6796 RVA: 0x000991B1 File Offset: 0x000973B1
		internal bool HasDefaultSupport
		{
			get
			{
				return (this.flags & TypeFlags.IgnoreDefault) == TypeFlags.None;
			}
		}

		// Token: 0x17000517 RID: 1303
		// (get) Token: 0x06001A8D RID: 6797 RVA: 0x000991C2 File Offset: 0x000973C2
		internal bool HasIsEmpty
		{
			get
			{
				return (this.flags & TypeFlags.HasIsEmpty) > TypeFlags.None;
			}
		}

		// Token: 0x17000518 RID: 1304
		// (get) Token: 0x06001A8E RID: 6798 RVA: 0x000991D3 File Offset: 0x000973D3
		internal bool CollapseWhitespace
		{
			get
			{
				return (this.flags & TypeFlags.CollapseWhitespace) > TypeFlags.None;
			}
		}

		// Token: 0x17000519 RID: 1305
		// (get) Token: 0x06001A8F RID: 6799 RVA: 0x000991E4 File Offset: 0x000973E4
		internal bool HasDefaultConstructor
		{
			get
			{
				return (this.flags & TypeFlags.HasDefaultConstructor) > TypeFlags.None;
			}
		}

		// Token: 0x1700051A RID: 1306
		// (get) Token: 0x06001A90 RID: 6800 RVA: 0x000991F5 File Offset: 0x000973F5
		internal bool IsUnsupported
		{
			get
			{
				return (this.flags & TypeFlags.Unsupported) > TypeFlags.None;
			}
		}

		// Token: 0x1700051B RID: 1307
		// (get) Token: 0x06001A91 RID: 6801 RVA: 0x00099206 File Offset: 0x00097406
		internal bool IsGenericInterface
		{
			get
			{
				return (this.flags & TypeFlags.GenericInterface) > TypeFlags.None;
			}
		}

		// Token: 0x1700051C RID: 1308
		// (get) Token: 0x06001A92 RID: 6802 RVA: 0x00099217 File Offset: 0x00097417
		internal bool IsPrivateImplementation
		{
			get
			{
				return (this.flags & TypeFlags.UsePrivateImplementation) > TypeFlags.None;
			}
		}

		// Token: 0x1700051D RID: 1309
		// (get) Token: 0x06001A93 RID: 6803 RVA: 0x00099228 File Offset: 0x00097428
		internal bool CannotNew
		{
			get
			{
				return !this.HasDefaultConstructor || this.ConstructorInaccessible;
			}
		}

		// Token: 0x1700051E RID: 1310
		// (get) Token: 0x06001A94 RID: 6804 RVA: 0x0009923A File Offset: 0x0009743A
		internal bool IsAbstract
		{
			get
			{
				return (this.flags & TypeFlags.Abstract) > TypeFlags.None;
			}
		}

		// Token: 0x1700051F RID: 1311
		// (get) Token: 0x06001A95 RID: 6805 RVA: 0x00099247 File Offset: 0x00097447
		internal bool IsOptionalValue
		{
			get
			{
				return (this.flags & TypeFlags.OptionalValue) > TypeFlags.None;
			}
		}

		// Token: 0x17000520 RID: 1312
		// (get) Token: 0x06001A96 RID: 6806 RVA: 0x00099258 File Offset: 0x00097458
		internal bool UseReflection
		{
			get
			{
				return (this.flags & TypeFlags.UseReflection) > TypeFlags.None;
			}
		}

		// Token: 0x17000521 RID: 1313
		// (get) Token: 0x06001A97 RID: 6807 RVA: 0x00099269 File Offset: 0x00097469
		internal bool IsVoid
		{
			get
			{
				return this.kind == TypeKind.Void;
			}
		}

		// Token: 0x17000522 RID: 1314
		// (get) Token: 0x06001A98 RID: 6808 RVA: 0x00099274 File Offset: 0x00097474
		internal bool IsClass
		{
			get
			{
				return this.kind == TypeKind.Class;
			}
		}

		// Token: 0x17000523 RID: 1315
		// (get) Token: 0x06001A99 RID: 6809 RVA: 0x0009927F File Offset: 0x0009747F
		internal bool IsStructLike
		{
			get
			{
				return this.kind == TypeKind.Struct || this.kind == TypeKind.Class;
			}
		}

		// Token: 0x17000524 RID: 1316
		// (get) Token: 0x06001A9A RID: 6810 RVA: 0x00099295 File Offset: 0x00097495
		internal bool IsArrayLike
		{
			get
			{
				return this.kind == TypeKind.Array || this.kind == TypeKind.Collection || this.kind == TypeKind.Enumerable;
			}
		}

		// Token: 0x17000525 RID: 1317
		// (get) Token: 0x06001A9B RID: 6811 RVA: 0x000992B4 File Offset: 0x000974B4
		internal bool IsCollection
		{
			get
			{
				return this.kind == TypeKind.Collection;
			}
		}

		// Token: 0x17000526 RID: 1318
		// (get) Token: 0x06001A9C RID: 6812 RVA: 0x000992BF File Offset: 0x000974BF
		internal bool IsEnumerable
		{
			get
			{
				return this.kind == TypeKind.Enumerable;
			}
		}

		// Token: 0x17000527 RID: 1319
		// (get) Token: 0x06001A9D RID: 6813 RVA: 0x000992CA File Offset: 0x000974CA
		internal bool IsArray
		{
			get
			{
				return this.kind == TypeKind.Array;
			}
		}

		// Token: 0x17000528 RID: 1320
		// (get) Token: 0x06001A9E RID: 6814 RVA: 0x000992D5 File Offset: 0x000974D5
		internal bool IsPrimitive
		{
			get
			{
				return this.kind == TypeKind.Primitive;
			}
		}

		// Token: 0x17000529 RID: 1321
		// (get) Token: 0x06001A9F RID: 6815 RVA: 0x000992E0 File Offset: 0x000974E0
		internal bool IsEnum
		{
			get
			{
				return this.kind == TypeKind.Enum;
			}
		}

		// Token: 0x1700052A RID: 1322
		// (get) Token: 0x06001AA0 RID: 6816 RVA: 0x000992EB File Offset: 0x000974EB
		internal bool IsNullable
		{
			get
			{
				return !this.IsValueType;
			}
		}

		// Token: 0x1700052B RID: 1323
		// (get) Token: 0x06001AA1 RID: 6817 RVA: 0x000992F6 File Offset: 0x000974F6
		internal bool IsRoot
		{
			get
			{
				return this.kind == TypeKind.Root;
			}
		}

		// Token: 0x1700052C RID: 1324
		// (get) Token: 0x06001AA2 RID: 6818 RVA: 0x00099301 File Offset: 0x00097501
		internal bool ConstructorInaccessible
		{
			get
			{
				return (this.flags & TypeFlags.CtorInaccessible) > TypeFlags.None;
			}
		}

		// Token: 0x1700052D RID: 1325
		// (get) Token: 0x06001AA3 RID: 6819 RVA: 0x00099312 File Offset: 0x00097512
		// (set) Token: 0x06001AA4 RID: 6820 RVA: 0x0009931A File Offset: 0x0009751A
		internal Exception Exception
		{
			get
			{
				return this.exception;
			}
			set
			{
				this.exception = value;
			}
		}

		// Token: 0x06001AA5 RID: 6821 RVA: 0x00099324 File Offset: 0x00097524
		internal TypeDesc GetNullableTypeDesc(Type type)
		{
			if (this.IsOptionalValue)
			{
				return this;
			}
			if (this.nullableTypeDesc == null)
			{
				this.nullableTypeDesc = new TypeDesc("NullableOf" + this.name, "System.Nullable`1[" + this.fullName + "]", null, TypeKind.Struct, this, this.flags | TypeFlags.OptionalValue, this.formatterName);
				this.nullableTypeDesc.type = type;
			}
			return this.nullableTypeDesc;
		}

		// Token: 0x06001AA6 RID: 6822 RVA: 0x0009939C File Offset: 0x0009759C
		internal void CheckSupported()
		{
			if (!this.IsUnsupported)
			{
				if (this.baseTypeDesc != null)
				{
					this.baseTypeDesc.CheckSupported();
				}
				if (this.arrayElementTypeDesc != null)
				{
					this.arrayElementTypeDesc.CheckSupported();
				}
				return;
			}
			if (this.Exception != null)
			{
				throw this.Exception;
			}
			throw new NotSupportedException(Res.GetString("{0} is an unsupported type. Please use [XmlIgnore] attribute to exclude members of this type from serialization graph.", new object[]
			{
				this.FullName
			}));
		}

		// Token: 0x06001AA7 RID: 6823 RVA: 0x00099408 File Offset: 0x00097608
		internal void CheckNeedConstructor()
		{
			if (!this.IsValueType && !this.IsAbstract && !this.HasDefaultConstructor)
			{
				this.flags |= TypeFlags.Unsupported;
				this.exception = new InvalidOperationException(Res.GetString("{0} cannot be serialized because it does not have a parameterless constructor.", new object[]
				{
					this.FullName
				}));
			}
		}

		// Token: 0x1700052E RID: 1326
		// (get) Token: 0x06001AA8 RID: 6824 RVA: 0x00099463 File Offset: 0x00097663
		internal string ArrayLengthName
		{
			get
			{
				if (this.kind != TypeKind.Array)
				{
					return "Count";
				}
				return "Length";
			}
		}

		// Token: 0x1700052F RID: 1327
		// (get) Token: 0x06001AA9 RID: 6825 RVA: 0x00099479 File Offset: 0x00097679
		// (set) Token: 0x06001AAA RID: 6826 RVA: 0x00099481 File Offset: 0x00097681
		internal TypeDesc ArrayElementTypeDesc
		{
			get
			{
				return this.arrayElementTypeDesc;
			}
			set
			{
				this.arrayElementTypeDesc = value;
			}
		}

		// Token: 0x17000530 RID: 1328
		// (get) Token: 0x06001AAB RID: 6827 RVA: 0x0009948A File Offset: 0x0009768A
		internal int Weight
		{
			get
			{
				return this.weight;
			}
		}

		// Token: 0x06001AAC RID: 6828 RVA: 0x00099494 File Offset: 0x00097694
		internal TypeDesc CreateArrayTypeDesc()
		{
			if (this.arrayTypeDesc == null)
			{
				this.arrayTypeDesc = new TypeDesc(null, this.name + "[]", this.fullName + "[]", TypeKind.Array, null, TypeFlags.Reference | (this.flags & TypeFlags.UseReflection), this);
			}
			return this.arrayTypeDesc;
		}

		// Token: 0x06001AAD RID: 6829 RVA: 0x000994EC File Offset: 0x000976EC
		internal TypeDesc CreateMappedTypeDesc(MappedTypeDesc extension)
		{
			return new TypeDesc(extension.Name, extension.Name, null, this.kind, this.baseTypeDesc, this.flags, null)
			{
				isXsdType = this.isXsdType,
				isMixed = this.isMixed,
				extendedType = extension,
				dataType = this.dataType
			};
		}

		// Token: 0x17000531 RID: 1329
		// (get) Token: 0x06001AAE RID: 6830 RVA: 0x00099549 File Offset: 0x00097749
		// (set) Token: 0x06001AAF RID: 6831 RVA: 0x00099551 File Offset: 0x00097751
		internal TypeDesc BaseTypeDesc
		{
			get
			{
				return this.baseTypeDesc;
			}
			set
			{
				this.baseTypeDesc = value;
				this.weight = ((this.baseTypeDesc == null) ? 0 : (this.baseTypeDesc.Weight + 1));
			}
		}

		// Token: 0x06001AB0 RID: 6832 RVA: 0x00099578 File Offset: 0x00097778
		internal bool IsDerivedFrom(TypeDesc baseTypeDesc)
		{
			for (TypeDesc typeDesc = this; typeDesc != null; typeDesc = typeDesc.BaseTypeDesc)
			{
				if (typeDesc == baseTypeDesc)
				{
					return true;
				}
			}
			return baseTypeDesc.IsRoot;
		}

		// Token: 0x06001AB1 RID: 6833 RVA: 0x000995A0 File Offset: 0x000977A0
		internal static TypeDesc FindCommonBaseTypeDesc(TypeDesc[] typeDescs)
		{
			if (typeDescs.Length == 0)
			{
				return null;
			}
			TypeDesc typeDesc = null;
			int num = int.MaxValue;
			for (int i = 0; i < typeDescs.Length; i++)
			{
				int num2 = typeDescs[i].Weight;
				if (num2 < num)
				{
					num = num2;
					typeDesc = typeDescs[i];
				}
			}
			while (typeDesc != null)
			{
				int num3 = 0;
				while (num3 < typeDescs.Length && typeDescs[num3].IsDerivedFrom(typeDesc))
				{
					num3++;
				}
				if (num3 == typeDescs.Length)
				{
					break;
				}
				typeDesc = typeDesc.BaseTypeDesc;
			}
			return typeDesc;
		}

		// Token: 0x04001992 RID: 6546
		private string name;

		// Token: 0x04001993 RID: 6547
		private string fullName;

		// Token: 0x04001994 RID: 6548
		private string cSharpName;

		// Token: 0x04001995 RID: 6549
		private TypeDesc arrayElementTypeDesc;

		// Token: 0x04001996 RID: 6550
		private TypeDesc arrayTypeDesc;

		// Token: 0x04001997 RID: 6551
		private TypeDesc nullableTypeDesc;

		// Token: 0x04001998 RID: 6552
		private TypeKind kind;

		// Token: 0x04001999 RID: 6553
		private XmlSchemaType dataType;

		// Token: 0x0400199A RID: 6554
		private Type type;

		// Token: 0x0400199B RID: 6555
		private TypeDesc baseTypeDesc;

		// Token: 0x0400199C RID: 6556
		private TypeFlags flags;

		// Token: 0x0400199D RID: 6557
		private string formatterName;

		// Token: 0x0400199E RID: 6558
		private bool isXsdType;

		// Token: 0x0400199F RID: 6559
		private bool isMixed;

		// Token: 0x040019A0 RID: 6560
		private MappedTypeDesc extendedType;

		// Token: 0x040019A1 RID: 6561
		private int weight;

		// Token: 0x040019A2 RID: 6562
		private Exception exception;
	}
}
