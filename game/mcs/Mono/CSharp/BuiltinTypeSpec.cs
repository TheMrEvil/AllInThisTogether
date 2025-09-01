using System;

namespace Mono.CSharp
{
	// Token: 0x020002DF RID: 735
	public sealed class BuiltinTypeSpec : TypeSpec
	{
		// Token: 0x06002308 RID: 8968 RVA: 0x000AC3A4 File Offset: 0x000AA5A4
		public BuiltinTypeSpec(MemberKind kind, string ns, string name, BuiltinTypeSpec.Type builtinKind) : base(kind, null, null, null, Modifiers.PUBLIC)
		{
			this.type = builtinKind;
			this.ns = ns;
			this.name = name;
		}

		// Token: 0x06002309 RID: 8969 RVA: 0x000AC3C7 File Offset: 0x000AA5C7
		public BuiltinTypeSpec(string name, BuiltinTypeSpec.Type builtinKind) : this(MemberKind.InternalCompilerType, "", name, builtinKind)
		{
			this.state = ((this.state & ~(MemberSpec.StateFlags.Obsolete_Undetected | MemberSpec.StateFlags.CLSCompliant_Undetected | MemberSpec.StateFlags.MissingDependency_Undetected)) | MemberSpec.StateFlags.CLSCompliant);
		}

		// Token: 0x1700080B RID: 2059
		// (get) Token: 0x0600230A RID: 8970 RVA: 0x000022F4 File Offset: 0x000004F4
		public override int Arity
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x1700080C RID: 2060
		// (get) Token: 0x0600230B RID: 8971 RVA: 0x000AC3EC File Offset: 0x000AA5EC
		public override BuiltinTypeSpec.Type BuiltinType
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x1700080D RID: 2061
		// (get) Token: 0x0600230C RID: 8972 RVA: 0x000AC3F4 File Offset: 0x000AA5F4
		public string FullName
		{
			get
			{
				return this.ns + "." + this.name;
			}
		}

		// Token: 0x1700080E RID: 2062
		// (get) Token: 0x0600230D RID: 8973 RVA: 0x000AC40C File Offset: 0x000AA60C
		public override string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x1700080F RID: 2063
		// (get) Token: 0x0600230E RID: 8974 RVA: 0x000AC414 File Offset: 0x000AA614
		public string Namespace
		{
			get
			{
				return this.ns;
			}
		}

		// Token: 0x0600230F RID: 8975 RVA: 0x000AC41C File Offset: 0x000AA61C
		public static bool IsPrimitiveType(TypeSpec type)
		{
			return type.BuiltinType >= BuiltinTypeSpec.Type.FirstPrimitive && type.BuiltinType <= BuiltinTypeSpec.Type.Double;
		}

		// Token: 0x06002310 RID: 8976 RVA: 0x000AC436 File Offset: 0x000AA636
		public static bool IsPrimitiveTypeOrDecimal(TypeSpec type)
		{
			return type.BuiltinType >= BuiltinTypeSpec.Type.FirstPrimitive && type.BuiltinType <= BuiltinTypeSpec.Type.Decimal;
		}

		// Token: 0x06002311 RID: 8977 RVA: 0x000AC450 File Offset: 0x000AA650
		public override string GetSignatureForError()
		{
			string text = this.Name;
			uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
			if (num <= 2386971688U)
			{
				if (num <= 765439473U)
				{
					if (num <= 679076413U)
					{
						if (num != 423635464U)
						{
							if (num == 679076413U)
							{
								if (text == "Char")
								{
									return "char";
								}
							}
						}
						else if (text == "SByte")
						{
							return "sbyte";
						}
					}
					else if (num != 697196164U)
					{
						if (num == 765439473U)
						{
							if (text == "Int16")
							{
								return "short";
							}
						}
					}
					else if (text == "Int64")
					{
						return "long";
					}
				}
				else if (num <= 1324880019U)
				{
					if (num != 1323747186U)
					{
						if (num == 1324880019U)
						{
							if (text == "UInt64")
							{
								return "ulong";
							}
						}
					}
					else if (text == "UInt16")
					{
						return "ushort";
					}
				}
				else if (num != 1615808600U)
				{
					if (num == 2386971688U)
					{
						if (text == "Double")
						{
							return "double";
						}
					}
				}
				else if (text == "String")
				{
					return "string";
				}
			}
			else if (num <= 3409549631U)
			{
				if (num <= 2779444460U)
				{
					if (num != 2711245919U)
					{
						if (num == 2779444460U)
						{
							if (text == "Decimal")
							{
								return "decimal";
							}
						}
					}
					else if (text == "Int32")
					{
						return "int";
					}
				}
				else if (num != 3370340735U)
				{
					if (num == 3409549631U)
					{
						if (text == "Byte")
						{
							return "byte";
						}
					}
				}
				else if (text == "Void")
				{
					return "void";
				}
			}
			else if (num <= 3851314394U)
			{
				if (num != 3538687084U)
				{
					if (num == 3851314394U)
					{
						if (text == "Object")
						{
							return "object";
						}
					}
				}
				else if (text == "UInt32")
				{
					return "uint";
				}
			}
			else if (num != 3969205087U)
			{
				if (num == 4051133705U)
				{
					if (text == "Single")
					{
						return "float";
					}
				}
			}
			else if (text == "Boolean")
			{
				return "bool";
			}
			if (this.ns.Length == 0)
			{
				return this.name;
			}
			return this.FullName;
		}

		// Token: 0x06002312 RID: 8978 RVA: 0x000AC730 File Offset: 0x000AA930
		public static int GetSize(TypeSpec type)
		{
			switch (type.BuiltinType)
			{
			case BuiltinTypeSpec.Type.FirstPrimitive:
			case BuiltinTypeSpec.Type.Byte:
			case BuiltinTypeSpec.Type.SByte:
				return 1;
			case BuiltinTypeSpec.Type.Char:
			case BuiltinTypeSpec.Type.Short:
			case BuiltinTypeSpec.Type.UShort:
				return 2;
			case BuiltinTypeSpec.Type.Int:
			case BuiltinTypeSpec.Type.UInt:
			case BuiltinTypeSpec.Type.Float:
				return 4;
			case BuiltinTypeSpec.Type.Long:
			case BuiltinTypeSpec.Type.ULong:
			case BuiltinTypeSpec.Type.Double:
				return 8;
			case BuiltinTypeSpec.Type.Decimal:
				return 16;
			default:
				return 0;
			}
		}

		// Token: 0x06002313 RID: 8979 RVA: 0x000AC78E File Offset: 0x000AA98E
		public void SetDefinition(ITypeDefinition td, System.Type type, Modifiers mod)
		{
			this.definition = td;
			this.info = type;
			this.modifiers |= (mod & ~(Modifiers.PROTECTED | Modifiers.PUBLIC | Modifiers.PRIVATE | Modifiers.INTERNAL));
		}

		// Token: 0x06002314 RID: 8980 RVA: 0x000AC7AF File Offset: 0x000AA9AF
		public void SetDefinition(TypeSpec ts)
		{
			this.definition = ts.MemberDefinition;
			this.info = ts.GetMetaInfo();
			this.BaseType = ts.BaseType;
			this.Interfaces = ts.Interfaces;
			this.modifiers = ts.Modifiers;
		}

		// Token: 0x04000D6D RID: 3437
		private readonly BuiltinTypeSpec.Type type;

		// Token: 0x04000D6E RID: 3438
		private readonly string ns;

		// Token: 0x04000D6F RID: 3439
		private readonly string name;

		// Token: 0x02000405 RID: 1029
		public enum Type
		{
			// Token: 0x0400116C RID: 4460
			None,
			// Token: 0x0400116D RID: 4461
			FirstPrimitive,
			// Token: 0x0400116E RID: 4462
			Bool = 1,
			// Token: 0x0400116F RID: 4463
			Byte,
			// Token: 0x04001170 RID: 4464
			SByte,
			// Token: 0x04001171 RID: 4465
			Char,
			// Token: 0x04001172 RID: 4466
			Short,
			// Token: 0x04001173 RID: 4467
			UShort,
			// Token: 0x04001174 RID: 4468
			Int,
			// Token: 0x04001175 RID: 4469
			UInt,
			// Token: 0x04001176 RID: 4470
			Long,
			// Token: 0x04001177 RID: 4471
			ULong,
			// Token: 0x04001178 RID: 4472
			Float,
			// Token: 0x04001179 RID: 4473
			Double,
			// Token: 0x0400117A RID: 4474
			LastPrimitive = 12,
			// Token: 0x0400117B RID: 4475
			Decimal,
			// Token: 0x0400117C RID: 4476
			IntPtr,
			// Token: 0x0400117D RID: 4477
			UIntPtr,
			// Token: 0x0400117E RID: 4478
			Object,
			// Token: 0x0400117F RID: 4479
			Dynamic,
			// Token: 0x04001180 RID: 4480
			String,
			// Token: 0x04001181 RID: 4481
			Type,
			// Token: 0x04001182 RID: 4482
			ValueType,
			// Token: 0x04001183 RID: 4483
			Enum,
			// Token: 0x04001184 RID: 4484
			Delegate,
			// Token: 0x04001185 RID: 4485
			MulticastDelegate,
			// Token: 0x04001186 RID: 4486
			Array,
			// Token: 0x04001187 RID: 4487
			IEnumerator,
			// Token: 0x04001188 RID: 4488
			IEnumerable,
			// Token: 0x04001189 RID: 4489
			IDisposable,
			// Token: 0x0400118A RID: 4490
			Exception,
			// Token: 0x0400118B RID: 4491
			Attribute,
			// Token: 0x0400118C RID: 4492
			Other
		}
	}
}
