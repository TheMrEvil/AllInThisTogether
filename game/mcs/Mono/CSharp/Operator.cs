using System;
using System.Reflection;
using System.Text;
using Mono.CSharp.Nullable;

namespace Mono.CSharp
{
	// Token: 0x02000256 RID: 598
	public class Operator : MethodOrOperator
	{
		// Token: 0x06001DAF RID: 7599 RVA: 0x00090AA4 File Offset: 0x0008ECA4
		static Operator()
		{
			Operator.names[0] = new string[]
			{
				"!",
				"op_LogicalNot"
			};
			Operator.names[1] = new string[]
			{
				"~",
				"op_OnesComplement"
			};
			Operator.names[2] = new string[]
			{
				"++",
				"op_Increment"
			};
			Operator.names[3] = new string[]
			{
				"--",
				"op_Decrement"
			};
			Operator.names[4] = new string[]
			{
				"true",
				"op_True"
			};
			Operator.names[5] = new string[]
			{
				"false",
				"op_False"
			};
			Operator.names[6] = new string[]
			{
				"+",
				"op_Addition"
			};
			Operator.names[7] = new string[]
			{
				"-",
				"op_Subtraction"
			};
			Operator.names[8] = new string[]
			{
				"+",
				"op_UnaryPlus"
			};
			Operator.names[9] = new string[]
			{
				"-",
				"op_UnaryNegation"
			};
			Operator.names[10] = new string[]
			{
				"*",
				"op_Multiply"
			};
			Operator.names[11] = new string[]
			{
				"/",
				"op_Division"
			};
			Operator.names[12] = new string[]
			{
				"%",
				"op_Modulus"
			};
			Operator.names[13] = new string[]
			{
				"&",
				"op_BitwiseAnd"
			};
			Operator.names[14] = new string[]
			{
				"|",
				"op_BitwiseOr"
			};
			Operator.names[15] = new string[]
			{
				"^",
				"op_ExclusiveOr"
			};
			Operator.names[16] = new string[]
			{
				"<<",
				"op_LeftShift"
			};
			Operator.names[17] = new string[]
			{
				">>",
				"op_RightShift"
			};
			Operator.names[18] = new string[]
			{
				"==",
				"op_Equality"
			};
			Operator.names[19] = new string[]
			{
				"!=",
				"op_Inequality"
			};
			Operator.names[20] = new string[]
			{
				">",
				"op_GreaterThan"
			};
			Operator.names[21] = new string[]
			{
				"<",
				"op_LessThan"
			};
			Operator.names[22] = new string[]
			{
				">=",
				"op_GreaterThanOrEqual"
			};
			Operator.names[23] = new string[]
			{
				"<=",
				"op_LessThanOrEqual"
			};
			Operator.names[24] = new string[]
			{
				"implicit",
				"op_Implicit"
			};
			Operator.names[25] = new string[]
			{
				"explicit",
				"op_Explicit"
			};
			Operator.names[26] = new string[]
			{
				"is",
				"op_Is"
			};
		}

		// Token: 0x06001DB0 RID: 7600 RVA: 0x00090DDE File Offset: 0x0008EFDE
		public Operator(TypeDefinition parent, Operator.OpType type, FullNamedExpression ret_type, Modifiers mod_flags, ParametersCompiled parameters, ToplevelBlock block, Attributes attrs, Location loc) : base(parent, ret_type, mod_flags, Modifiers.PUBLIC | Modifiers.STATIC | Modifiers.EXTERN | Modifiers.UNSAFE, new MemberName(Operator.GetMetadataName(type), loc), attrs, parameters)
		{
			this.OperatorType = type;
			base.Block = block;
		}

		// Token: 0x06001DB1 RID: 7601 RVA: 0x00090E0F File Offset: 0x0008F00F
		public override void Accept(StructuralVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x06001DB2 RID: 7602 RVA: 0x000904DC File Offset: 0x0008E6DC
		public override void ApplyAttributeBuilder(Attribute a, MethodSpec ctor, byte[] cdata, PredefinedAttributes pa)
		{
			if (a.Type == pa.Conditional)
			{
				base.Error_ConditionalAttributeIsNotValid();
				return;
			}
			base.ApplyAttributeBuilder(a, ctor, cdata, pa);
		}

		// Token: 0x06001DB3 RID: 7603 RVA: 0x00090E18 File Offset: 0x0008F018
		public override bool Define()
		{
			if ((base.ModFlags & (Modifiers.PUBLIC | Modifiers.STATIC)) != (Modifiers.PUBLIC | Modifiers.STATIC))
			{
				base.Report.Error(558, base.Location, "User-defined operator `{0}' must be declared static and public", this.GetSignatureForError());
			}
			if (!base.Define())
			{
				return false;
			}
			if (this.block != null)
			{
				if (this.block.IsIterator)
				{
					Iterator.CreateIterator(this, this.Parent.PartialContainer, base.ModFlags);
					base.ModFlags |= Modifiers.DEBUGGER_HIDDEN;
				}
				if (this.Compiler.Settings.WriteMetadataOnly)
				{
					this.block = null;
				}
			}
			if (this.OperatorType == Operator.OpType.Explicit)
			{
				this.Parent.MemberCache.CheckExistingMembersOverloads(this, Operator.GetMetadataName(Operator.OpType.Implicit), this.parameters);
			}
			else if (this.OperatorType == Operator.OpType.Implicit)
			{
				this.Parent.MemberCache.CheckExistingMembersOverloads(this, Operator.GetMetadataName(Operator.OpType.Explicit), this.parameters);
			}
			TypeSpec currentType = this.Parent.PartialContainer.CurrentType;
			TypeSpec memberType = base.MemberType;
			TypeSpec typeSpec = base.ParameterTypes[0];
			TypeSpec typeSpec2 = typeSpec;
			if (typeSpec.IsNullableType)
			{
				typeSpec2 = NullableInfo.GetUnderlyingType(typeSpec);
			}
			TypeSpec typeSpec3 = memberType;
			if (memberType.IsNullableType)
			{
				typeSpec3 = NullableInfo.GetUnderlyingType(memberType);
			}
			if (this.OperatorType == Operator.OpType.Implicit || this.OperatorType == Operator.OpType.Explicit)
			{
				if (typeSpec2 == typeSpec3 && typeSpec2 == currentType)
				{
					base.Report.Error(555, base.Location, "User-defined operator cannot take an object of the enclosing type and convert to an object of the enclosing type");
					return false;
				}
				TypeSpec typeSpec4;
				if (currentType == memberType || currentType == typeSpec3)
				{
					typeSpec4 = typeSpec;
				}
				else
				{
					if (currentType != typeSpec && currentType != typeSpec2)
					{
						base.Report.Error(556, base.Location, "User-defined conversion must convert to or from the enclosing type");
						return false;
					}
					typeSpec4 = memberType;
				}
				if (typeSpec4.BuiltinType == BuiltinTypeSpec.Type.Dynamic)
				{
					base.Report.Error(1964, base.Location, "User-defined conversion `{0}' cannot convert to or from the dynamic type", this.GetSignatureForError());
					return false;
				}
				if (typeSpec4.IsInterface)
				{
					base.Report.Error(552, base.Location, "User-defined conversion `{0}' cannot convert to or from an interface type", this.GetSignatureForError());
					return false;
				}
				if (typeSpec4.IsClass)
				{
					if (TypeSpec.IsBaseClass(currentType, typeSpec4, true))
					{
						base.Report.Error(553, base.Location, "User-defined conversion `{0}' cannot convert to or from a base class", this.GetSignatureForError());
						return false;
					}
					if (TypeSpec.IsBaseClass(typeSpec4, currentType, false))
					{
						base.Report.Error(554, base.Location, "User-defined conversion `{0}' cannot convert to or from a derived class", this.GetSignatureForError());
						return false;
					}
				}
			}
			else if (this.OperatorType == Operator.OpType.LeftShift || this.OperatorType == Operator.OpType.RightShift)
			{
				if (typeSpec != currentType || this.parameters.Types[1].BuiltinType != BuiltinTypeSpec.Type.Int)
				{
					base.Report.Error(564, base.Location, "Overloaded shift operator must have the type of the first operand be the containing type, and the type of the second operand must be int");
					return false;
				}
			}
			else if (this.parameters.Count == 1)
			{
				if (this.OperatorType == Operator.OpType.Increment || this.OperatorType == Operator.OpType.Decrement)
				{
					if (memberType != currentType && !TypeSpec.IsBaseClass(memberType, currentType, false))
					{
						base.Report.Error(448, base.Location, "The return type for ++ or -- operator must be the containing type or derived from the containing type");
						return false;
					}
					if (typeSpec != currentType)
					{
						base.Report.Error(559, base.Location, "The parameter type for ++ or -- operator must be the containing type");
						return false;
					}
				}
				if (typeSpec2 != currentType)
				{
					base.Report.Error(562, base.Location, "The parameter type of a unary operator must be the containing type");
					return false;
				}
				if ((this.OperatorType == Operator.OpType.True || this.OperatorType == Operator.OpType.False) && memberType.BuiltinType != BuiltinTypeSpec.Type.FirstPrimitive)
				{
					base.Report.Error(215, base.Location, "The return type of operator True or False must be bool");
					return false;
				}
			}
			else if (typeSpec2 != currentType)
			{
				TypeSpec typeSpec5 = base.ParameterTypes[1];
				if (typeSpec5.IsNullableType)
				{
					typeSpec5 = NullableInfo.GetUnderlyingType(typeSpec5);
				}
				if (typeSpec5 != currentType)
				{
					base.Report.Error(563, base.Location, "One of the parameters of a binary operator must be the containing type");
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001DB4 RID: 7604 RVA: 0x000911E5 File Offset: 0x0008F3E5
		protected override bool ResolveMemberType()
		{
			if (!base.ResolveMemberType())
			{
				return false;
			}
			this.flags |= (MethodAttributes.HideBySig | MethodAttributes.SpecialName);
			return true;
		}

		// Token: 0x06001DB5 RID: 7605 RVA: 0x0008FC24 File Offset: 0x0008DE24
		protected override MemberSpec FindBaseMember(out MemberSpec bestCandidate, ref bool overrides)
		{
			bestCandidate = null;
			return null;
		}

		// Token: 0x06001DB6 RID: 7606 RVA: 0x00091204 File Offset: 0x0008F404
		public static string GetName(Operator.OpType ot)
		{
			return Operator.names[(int)ot][0];
		}

		// Token: 0x06001DB7 RID: 7607 RVA: 0x00091210 File Offset: 0x0008F410
		public static string GetName(string metadata_name)
		{
			for (int i = 0; i < Operator.names.Length; i++)
			{
				if (Operator.names[i][1] == metadata_name)
				{
					return Operator.names[i][0];
				}
			}
			return null;
		}

		// Token: 0x06001DB8 RID: 7608 RVA: 0x0009124B File Offset: 0x0008F44B
		public static string GetMetadataName(Operator.OpType ot)
		{
			return Operator.names[(int)ot][1];
		}

		// Token: 0x06001DB9 RID: 7609 RVA: 0x00091258 File Offset: 0x0008F458
		public static string GetMetadataName(string name)
		{
			for (int i = 0; i < Operator.names.Length; i++)
			{
				if (Operator.names[i][0] == name)
				{
					return Operator.names[i][1];
				}
			}
			return null;
		}

		// Token: 0x06001DBA RID: 7610 RVA: 0x00091294 File Offset: 0x0008F494
		public static Operator.OpType? GetType(string metadata_name)
		{
			for (int i = 0; i < Operator.names.Length; i++)
			{
				if (Operator.names[i][1] == metadata_name)
				{
					return new Operator.OpType?((Operator.OpType)i);
				}
			}
			return null;
		}

		// Token: 0x06001DBB RID: 7611 RVA: 0x000912D8 File Offset: 0x0008F4D8
		public Operator.OpType GetMatchingOperator()
		{
			Operator.OpType operatorType = this.OperatorType;
			if (operatorType == Operator.OpType.True)
			{
				return Operator.OpType.False;
			}
			if (operatorType == Operator.OpType.False)
			{
				return Operator.OpType.True;
			}
			switch (operatorType)
			{
			case Operator.OpType.Equality:
				return Operator.OpType.Inequality;
			case Operator.OpType.Inequality:
				return Operator.OpType.Equality;
			case Operator.OpType.GreaterThan:
				return Operator.OpType.LessThan;
			case Operator.OpType.LessThan:
				return Operator.OpType.GreaterThan;
			case Operator.OpType.GreaterThanOrEqual:
				return Operator.OpType.LessThanOrEqual;
			case Operator.OpType.LessThanOrEqual:
				return Operator.OpType.GreaterThanOrEqual;
			default:
				return Operator.OpType.TOP;
			}
		}

		// Token: 0x06001DBC RID: 7612 RVA: 0x00091330 File Offset: 0x0008F530
		public override string GetSignatureForDocumentation()
		{
			string text = base.GetSignatureForDocumentation();
			if (this.OperatorType == Operator.OpType.Implicit || this.OperatorType == Operator.OpType.Explicit)
			{
				text = text + "~" + base.ReturnType.GetSignatureForDocumentation();
			}
			return text;
		}

		// Token: 0x06001DBD RID: 7613 RVA: 0x00091370 File Offset: 0x0008F570
		public override string GetSignatureForError()
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (this.OperatorType == Operator.OpType.Implicit || this.OperatorType == Operator.OpType.Explicit)
			{
				stringBuilder.AppendFormat("{0}.{1} operator {2}", this.Parent.GetSignatureForError(), Operator.GetName(this.OperatorType), (this.member_type == null) ? this.type_expr.GetSignatureForError() : this.member_type.GetSignatureForError());
			}
			else
			{
				stringBuilder.AppendFormat("{0}.operator {1}", this.Parent.GetSignatureForError(), Operator.GetName(this.OperatorType));
			}
			stringBuilder.Append(this.parameters.GetSignatureForError());
			return stringBuilder.ToString();
		}

		// Token: 0x04000AF5 RID: 2805
		private const Modifiers AllowedModifiers = Modifiers.PUBLIC | Modifiers.STATIC | Modifiers.EXTERN | Modifiers.UNSAFE;

		// Token: 0x04000AF6 RID: 2806
		public readonly Operator.OpType OperatorType;

		// Token: 0x04000AF7 RID: 2807
		private static readonly string[][] names = new string[27][];

		// Token: 0x020003D1 RID: 977
		public enum OpType : byte
		{
			// Token: 0x040010CF RID: 4303
			LogicalNot,
			// Token: 0x040010D0 RID: 4304
			OnesComplement,
			// Token: 0x040010D1 RID: 4305
			Increment,
			// Token: 0x040010D2 RID: 4306
			Decrement,
			// Token: 0x040010D3 RID: 4307
			True,
			// Token: 0x040010D4 RID: 4308
			False,
			// Token: 0x040010D5 RID: 4309
			Addition,
			// Token: 0x040010D6 RID: 4310
			Subtraction,
			// Token: 0x040010D7 RID: 4311
			UnaryPlus,
			// Token: 0x040010D8 RID: 4312
			UnaryNegation,
			// Token: 0x040010D9 RID: 4313
			Multiply,
			// Token: 0x040010DA RID: 4314
			Division,
			// Token: 0x040010DB RID: 4315
			Modulus,
			// Token: 0x040010DC RID: 4316
			BitwiseAnd,
			// Token: 0x040010DD RID: 4317
			BitwiseOr,
			// Token: 0x040010DE RID: 4318
			ExclusiveOr,
			// Token: 0x040010DF RID: 4319
			LeftShift,
			// Token: 0x040010E0 RID: 4320
			RightShift,
			// Token: 0x040010E1 RID: 4321
			Equality,
			// Token: 0x040010E2 RID: 4322
			Inequality,
			// Token: 0x040010E3 RID: 4323
			GreaterThan,
			// Token: 0x040010E4 RID: 4324
			LessThan,
			// Token: 0x040010E5 RID: 4325
			GreaterThanOrEqual,
			// Token: 0x040010E6 RID: 4326
			LessThanOrEqual,
			// Token: 0x040010E7 RID: 4327
			Implicit,
			// Token: 0x040010E8 RID: 4328
			Explicit,
			// Token: 0x040010E9 RID: 4329
			Is,
			// Token: 0x040010EA RID: 4330
			TOP
		}
	}
}
