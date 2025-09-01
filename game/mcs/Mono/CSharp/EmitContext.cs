using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using Mono.CompilerServices.SymbolWriter;
using Mono.CSharp.Nullable;

namespace Mono.CSharp
{
	// Token: 0x02000134 RID: 308
	public class EmitContext : BuilderContext
	{
		// Token: 0x06000F6E RID: 3950 RVA: 0x0003F6F0 File Offset: 0x0003D8F0
		public EmitContext(IMemberContext rc, ILGenerator ig, TypeSpec return_type, SourceMethodBuilder methodSymbols)
		{
			this.member_context = rc;
			this.ig = ig;
			this.return_type = return_type;
			if (rc.Module.Compiler.Settings.Checked)
			{
				this.flags |= BuilderContext.Options.CheckedScope;
			}
			if (methodSymbols != null)
			{
				this.methodSymbols = methodSymbols;
				if (!rc.Module.Compiler.Settings.Optimize)
				{
					this.flags |= BuilderContext.Options.AccurateDebugInfo;
					return;
				}
			}
			else
			{
				this.flags |= BuilderContext.Options.OmitDebugInfo;
			}
		}

		// Token: 0x17000448 RID: 1096
		// (get) Token: 0x06000F6F RID: 3951 RVA: 0x0003F77D File Offset: 0x0003D97D
		public AsyncTaskStorey AsyncTaskStorey
		{
			get
			{
				return this.CurrentAnonymousMethod.Storey as AsyncTaskStorey;
			}
		}

		// Token: 0x17000449 RID: 1097
		// (get) Token: 0x06000F70 RID: 3952 RVA: 0x0003F78F File Offset: 0x0003D98F
		public BuiltinTypes BuiltinTypes
		{
			get
			{
				return this.MemberContext.Module.Compiler.BuiltinTypes;
			}
		}

		// Token: 0x1700044A RID: 1098
		// (get) Token: 0x06000F71 RID: 3953 RVA: 0x0003F7A6 File Offset: 0x0003D9A6
		// (set) Token: 0x06000F72 RID: 3954 RVA: 0x0003F7AE File Offset: 0x0003D9AE
		public ConditionalAccessContext ConditionalAccess
		{
			[CompilerGenerated]
			get
			{
				return this.<ConditionalAccess>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<ConditionalAccess>k__BackingField = value;
			}
		}

		// Token: 0x1700044B RID: 1099
		// (get) Token: 0x06000F73 RID: 3955 RVA: 0x0003F7B7 File Offset: 0x0003D9B7
		public TypeSpec CurrentType
		{
			get
			{
				return this.member_context.CurrentType;
			}
		}

		// Token: 0x1700044C RID: 1100
		// (get) Token: 0x06000F74 RID: 3956 RVA: 0x0003F7C4 File Offset: 0x0003D9C4
		public TypeParameters CurrentTypeParameters
		{
			get
			{
				return this.member_context.CurrentTypeParameters;
			}
		}

		// Token: 0x1700044D RID: 1101
		// (get) Token: 0x06000F75 RID: 3957 RVA: 0x0003F7D1 File Offset: 0x0003D9D1
		public MemberCore CurrentTypeDefinition
		{
			get
			{
				return this.member_context.CurrentMemberDefinition;
			}
		}

		// Token: 0x1700044E RID: 1102
		// (get) Token: 0x06000F76 RID: 3958 RVA: 0x0003F7DE File Offset: 0x0003D9DE
		public bool EmitAccurateDebugInfo
		{
			get
			{
				return (this.flags & BuilderContext.Options.AccurateDebugInfo) > (BuilderContext.Options)0;
			}
		}

		// Token: 0x1700044F RID: 1103
		// (get) Token: 0x06000F77 RID: 3959 RVA: 0x0003F7EB File Offset: 0x0003D9EB
		public bool HasMethodSymbolBuilder
		{
			get
			{
				return this.methodSymbols != null;
			}
		}

		// Token: 0x17000450 RID: 1104
		// (get) Token: 0x06000F78 RID: 3960 RVA: 0x0003F7F6 File Offset: 0x0003D9F6
		public bool HasReturnLabel
		{
			get
			{
				return this.return_label != null;
			}
		}

		// Token: 0x17000451 RID: 1105
		// (get) Token: 0x06000F79 RID: 3961 RVA: 0x0003F803 File Offset: 0x0003DA03
		public bool IsStatic
		{
			get
			{
				return this.member_context.IsStatic;
			}
		}

		// Token: 0x17000452 RID: 1106
		// (get) Token: 0x06000F7A RID: 3962 RVA: 0x0003F810 File Offset: 0x0003DA10
		public bool IsStaticConstructor
		{
			get
			{
				return this.member_context.IsStatic && (this.flags & BuilderContext.Options.ConstructorScope) > (BuilderContext.Options)0;
			}
		}

		// Token: 0x17000453 RID: 1107
		// (get) Token: 0x06000F7B RID: 3963 RVA: 0x0003F82C File Offset: 0x0003DA2C
		public bool IsAnonymousStoreyMutateRequired
		{
			get
			{
				return this.CurrentAnonymousMethod != null && this.CurrentAnonymousMethod.Storey != null && this.CurrentAnonymousMethod.Storey.Mutator != null;
			}
		}

		// Token: 0x17000454 RID: 1108
		// (get) Token: 0x06000F7C RID: 3964 RVA: 0x0003F858 File Offset: 0x0003DA58
		public IMemberContext MemberContext
		{
			get
			{
				return this.member_context;
			}
		}

		// Token: 0x17000455 RID: 1109
		// (get) Token: 0x06000F7D RID: 3965 RVA: 0x0003F860 File Offset: 0x0003DA60
		public ModuleContainer Module
		{
			get
			{
				return this.member_context.Module;
			}
		}

		// Token: 0x17000456 RID: 1110
		// (get) Token: 0x06000F7E RID: 3966 RVA: 0x0003F86D File Offset: 0x0003DA6D
		public bool NotifyEvaluatorOnStore
		{
			get
			{
				return this.Module.Evaluator != null && this.Module.Evaluator.ModificationListener != null;
			}
		}

		// Token: 0x17000457 RID: 1111
		// (get) Token: 0x06000F7F RID: 3967 RVA: 0x0003F891 File Offset: 0x0003DA91
		public Report Report
		{
			get
			{
				return this.member_context.Module.Compiler.Report;
			}
		}

		// Token: 0x17000458 RID: 1112
		// (get) Token: 0x06000F80 RID: 3968 RVA: 0x0003F8A8 File Offset: 0x0003DAA8
		public TypeSpec ReturnType
		{
			get
			{
				return this.return_type;
			}
		}

		// Token: 0x17000459 RID: 1113
		// (get) Token: 0x06000F81 RID: 3969 RVA: 0x0003F8B0 File Offset: 0x0003DAB0
		public Label ReturnLabel
		{
			get
			{
				return this.return_label.Value;
			}
		}

		// Token: 0x1700045A RID: 1114
		// (get) Token: 0x06000F82 RID: 3970 RVA: 0x0003F8BD File Offset: 0x0003DABD
		public List<IExpressionCleanup> StatementEpilogue
		{
			get
			{
				return this.epilogue_expressions;
			}
		}

		// Token: 0x1700045B RID: 1115
		// (get) Token: 0x06000F83 RID: 3971 RVA: 0x0003F8C5 File Offset: 0x0003DAC5
		// (set) Token: 0x06000F84 RID: 3972 RVA: 0x0003F8CD File Offset: 0x0003DACD
		public LocalVariable AsyncThrowVariable
		{
			[CompilerGenerated]
			get
			{
				return this.<AsyncThrowVariable>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<AsyncThrowVariable>k__BackingField = value;
			}
		}

		// Token: 0x1700045C RID: 1116
		// (get) Token: 0x06000F85 RID: 3973 RVA: 0x0003F8D6 File Offset: 0x0003DAD6
		// (set) Token: 0x06000F86 RID: 3974 RVA: 0x0003F8DE File Offset: 0x0003DADE
		public List<TryFinally> TryFinallyUnwind
		{
			[CompilerGenerated]
			get
			{
				return this.<TryFinallyUnwind>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<TryFinallyUnwind>k__BackingField = value;
			}
		}

		// Token: 0x1700045D RID: 1117
		// (get) Token: 0x06000F87 RID: 3975 RVA: 0x0003F8E7 File Offset: 0x0003DAE7
		// (set) Token: 0x06000F88 RID: 3976 RVA: 0x0003F8EF File Offset: 0x0003DAEF
		public Label RecursivePatternLabel
		{
			[CompilerGenerated]
			get
			{
				return this.<RecursivePatternLabel>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<RecursivePatternLabel>k__BackingField = value;
			}
		}

		// Token: 0x06000F89 RID: 3977 RVA: 0x0003F8F8 File Offset: 0x0003DAF8
		public void AddStatementEpilog(IExpressionCleanup cleanupExpression)
		{
			if (this.epilogue_expressions == null)
			{
				this.epilogue_expressions = new List<IExpressionCleanup>();
			}
			else if (this.epilogue_expressions.Contains(cleanupExpression))
			{
				return;
			}
			this.epilogue_expressions.Add(cleanupExpression);
		}

		// Token: 0x06000F8A RID: 3978 RVA: 0x0000AF70 File Offset: 0x00009170
		public void AssertEmptyStack()
		{
		}

		// Token: 0x06000F8B RID: 3979 RVA: 0x0003F92A File Offset: 0x0003DB2A
		public bool Mark(Location loc)
		{
			return (this.flags & BuilderContext.Options.OmitDebugInfo) == (BuilderContext.Options)0 && !loc.IsNull && this.methodSymbols != null && !loc.SourceFile.IsHiddenLocation(loc);
		}

		// Token: 0x06000F8C RID: 3980 RVA: 0x0003F95D File Offset: 0x0003DB5D
		public void MarkCallEntry(Location loc)
		{
			if (!this.EmitAccurateDebugInfo)
			{
				return;
			}
			this.Mark(loc);
		}

		// Token: 0x06000F8D RID: 3981 RVA: 0x0003F970 File Offset: 0x0003DB70
		public void DefineLocalVariable(string name, LocalBuilder builder)
		{
			if ((this.flags & BuilderContext.Options.OmitDebugInfo) != (BuilderContext.Options)0)
			{
				return;
			}
			this.methodSymbols.AddLocal(builder.LocalIndex, name);
		}

		// Token: 0x06000F8E RID: 3982 RVA: 0x0003F98F File Offset: 0x0003DB8F
		public void BeginCatchBlock(TypeSpec type)
		{
			if (this.IsAnonymousStoreyMutateRequired)
			{
				type = this.CurrentAnonymousMethod.Storey.Mutator.Mutate(type);
			}
			this.ig.BeginCatchBlock(type.GetMetaInfo());
		}

		// Token: 0x06000F8F RID: 3983 RVA: 0x0003F9C2 File Offset: 0x0003DBC2
		public void BeginFilterHandler()
		{
			this.ig.BeginCatchBlock(null);
		}

		// Token: 0x06000F90 RID: 3984 RVA: 0x0003F9D0 File Offset: 0x0003DBD0
		public void BeginExceptionBlock()
		{
			this.ig.BeginExceptionBlock();
		}

		// Token: 0x06000F91 RID: 3985 RVA: 0x0003F9DE File Offset: 0x0003DBDE
		public void BeginExceptionFilterBlock()
		{
			this.ig.BeginExceptFilterBlock();
		}

		// Token: 0x06000F92 RID: 3986 RVA: 0x0003F9EB File Offset: 0x0003DBEB
		public void BeginFinallyBlock()
		{
			this.ig.BeginFinallyBlock();
		}

		// Token: 0x06000F93 RID: 3987 RVA: 0x0003F9F8 File Offset: 0x0003DBF8
		public void BeginScope()
		{
			BuilderContext.Options options = this.flags & BuilderContext.Options.OmitDebugInfo;
		}

		// Token: 0x06000F94 RID: 3988 RVA: 0x0003F9F8 File Offset: 0x0003DBF8
		public void BeginCompilerScope()
		{
			BuilderContext.Options options = this.flags & BuilderContext.Options.OmitDebugInfo;
		}

		// Token: 0x06000F95 RID: 3989 RVA: 0x0003FA03 File Offset: 0x0003DC03
		public void EndExceptionBlock()
		{
			this.ig.EndExceptionBlock();
		}

		// Token: 0x06000F96 RID: 3990 RVA: 0x0003F9F8 File Offset: 0x0003DBF8
		public void EndScope()
		{
			BuilderContext.Options options = this.flags & BuilderContext.Options.OmitDebugInfo;
		}

		// Token: 0x06000F97 RID: 3991 RVA: 0x0003FA10 File Offset: 0x0003DC10
		public void CloseConditionalAccess(TypeSpec type)
		{
			if (type != null)
			{
				this.Emit(OpCodes.Newobj, NullableInfo.GetConstructor(type));
			}
			this.MarkLabel(this.ConditionalAccess.EndLabel);
			this.ConditionalAccess = null;
		}

		// Token: 0x06000F98 RID: 3992 RVA: 0x0003FA40 File Offset: 0x0003DC40
		public DynamicSiteClass CreateDynamicSite()
		{
			if (this.dynamic_site_container == null)
			{
				MemberBase host = this.member_context.CurrentMemberDefinition as MemberBase;
				this.dynamic_site_container = new DynamicSiteClass(this.CurrentTypeDefinition.Parent.PartialContainer, host, this.member_context.CurrentTypeParameters);
				this.CurrentTypeDefinition.Module.AddCompilerGeneratedClass(this.dynamic_site_container);
				this.dynamic_site_container.CreateContainer();
				this.dynamic_site_container.DefineContainer();
				this.dynamic_site_container.Define();
				TypeParameterInflator inflator = new TypeParameterInflator(this.Module, this.CurrentType, TypeParameterSpec.EmptyTypes, TypeSpec.EmptyTypes);
				MemberSpec ms = this.dynamic_site_container.CurrentType.InflateMember(inflator);
				this.CurrentType.MemberCache.AddMember(ms);
			}
			return this.dynamic_site_container;
		}

		// Token: 0x06000F99 RID: 3993 RVA: 0x0003FB10 File Offset: 0x0003DD10
		public Label CreateReturnLabel()
		{
			if (this.return_label == null)
			{
				this.return_label = new Label?(this.DefineLabel());
			}
			return this.return_label.Value;
		}

		// Token: 0x06000F9A RID: 3994 RVA: 0x0003FB3B File Offset: 0x0003DD3B
		public LocalBuilder DeclareLocal(TypeSpec type, bool pinned)
		{
			if (this.IsAnonymousStoreyMutateRequired)
			{
				type = this.CurrentAnonymousMethod.Storey.Mutator.Mutate(type);
			}
			return this.ig.DeclareLocal(type.GetMetaInfo(), pinned);
		}

		// Token: 0x06000F9B RID: 3995 RVA: 0x0003FB6F File Offset: 0x0003DD6F
		public Label DefineLabel()
		{
			return this.ig.DefineLabel();
		}

		// Token: 0x06000F9C RID: 3996 RVA: 0x0003FB7C File Offset: 0x0003DD7C
		public StackFieldExpr GetTemporaryField(TypeSpec type, bool initializedFieldRequired = false)
		{
			return new StackFieldExpr(this.AsyncTaskStorey.AddCapturedLocalVariable(type, initializedFieldRequired))
			{
				InstanceExpression = new CompilerGeneratedThis(this.CurrentType, Location.Null)
			};
		}

		// Token: 0x06000F9D RID: 3997 RVA: 0x0003FBA6 File Offset: 0x0003DDA6
		public void MarkLabel(Label label)
		{
			this.ig.MarkLabel(label);
		}

		// Token: 0x06000F9E RID: 3998 RVA: 0x0003FBB4 File Offset: 0x0003DDB4
		public void Emit(OpCode opcode)
		{
			this.ig.Emit(opcode);
		}

		// Token: 0x06000F9F RID: 3999 RVA: 0x0003FBC2 File Offset: 0x0003DDC2
		public void Emit(OpCode opcode, LocalBuilder local)
		{
			this.ig.Emit(opcode, local);
		}

		// Token: 0x06000FA0 RID: 4000 RVA: 0x0003FBD1 File Offset: 0x0003DDD1
		public void Emit(OpCode opcode, string arg)
		{
			this.ig.Emit(opcode, arg);
		}

		// Token: 0x06000FA1 RID: 4001 RVA: 0x0003FBE0 File Offset: 0x0003DDE0
		public void Emit(OpCode opcode, double arg)
		{
			this.ig.Emit(opcode, arg);
		}

		// Token: 0x06000FA2 RID: 4002 RVA: 0x0003FBEF File Offset: 0x0003DDEF
		public void Emit(OpCode opcode, float arg)
		{
			this.ig.Emit(opcode, arg);
		}

		// Token: 0x06000FA3 RID: 4003 RVA: 0x0003FBFE File Offset: 0x0003DDFE
		public void Emit(OpCode opcode, Label label)
		{
			this.ig.Emit(opcode, label);
		}

		// Token: 0x06000FA4 RID: 4004 RVA: 0x0003FC0D File Offset: 0x0003DE0D
		public void Emit(OpCode opcode, Label[] labels)
		{
			this.ig.Emit(opcode, labels);
		}

		// Token: 0x06000FA5 RID: 4005 RVA: 0x0003FC1C File Offset: 0x0003DE1C
		public void Emit(OpCode opcode, TypeSpec type)
		{
			if (this.IsAnonymousStoreyMutateRequired)
			{
				type = this.CurrentAnonymousMethod.Storey.Mutator.Mutate(type);
			}
			this.ig.Emit(opcode, type.GetMetaInfo());
		}

		// Token: 0x06000FA6 RID: 4006 RVA: 0x0003FC50 File Offset: 0x0003DE50
		public void Emit(OpCode opcode, FieldSpec field)
		{
			if (this.IsAnonymousStoreyMutateRequired)
			{
				field = field.Mutate(this.CurrentAnonymousMethod.Storey.Mutator);
			}
			this.ig.Emit(opcode, field.GetMetaInfo());
		}

		// Token: 0x06000FA7 RID: 4007 RVA: 0x0003FC84 File Offset: 0x0003DE84
		public void Emit(OpCode opcode, MethodSpec method)
		{
			if (this.IsAnonymousStoreyMutateRequired)
			{
				method = method.Mutate(this.CurrentAnonymousMethod.Storey.Mutator);
			}
			if (method.IsConstructor)
			{
				this.ig.Emit(opcode, (ConstructorInfo)method.GetMetaInfo());
				return;
			}
			this.ig.Emit(opcode, (MethodInfo)method.GetMetaInfo());
		}

		// Token: 0x06000FA8 RID: 4008 RVA: 0x0003FCE8 File Offset: 0x0003DEE8
		public void Emit(OpCode opcode, MethodInfo method)
		{
			this.ig.Emit(opcode, method);
		}

		// Token: 0x06000FA9 RID: 4009 RVA: 0x0003FCF7 File Offset: 0x0003DEF7
		public void Emit(OpCode opcode, MethodSpec method, Type[] vargs)
		{
			this.ig.EmitCall(opcode, (MethodInfo)method.GetMetaInfo(), vargs);
		}

		// Token: 0x06000FAA RID: 4010 RVA: 0x0003FD14 File Offset: 0x0003DF14
		public void EmitArrayNew(ArrayContainer ac)
		{
			if (ac.Rank == 1)
			{
				TypeSpec typeSpec = this.IsAnonymousStoreyMutateRequired ? this.CurrentAnonymousMethod.Storey.Mutator.Mutate(ac.Element) : ac.Element;
				this.ig.Emit(OpCodes.Newarr, typeSpec.GetMetaInfo());
				return;
			}
			if (this.IsAnonymousStoreyMutateRequired)
			{
				ac = (ArrayContainer)ac.Mutate(this.CurrentAnonymousMethod.Storey.Mutator);
			}
			this.ig.Emit(OpCodes.Newobj, ac.GetConstructor());
		}

		// Token: 0x06000FAB RID: 4011 RVA: 0x0003FDA8 File Offset: 0x0003DFA8
		public void EmitArrayAddress(ArrayContainer ac)
		{
			if (ac.Rank > 1)
			{
				if (this.IsAnonymousStoreyMutateRequired)
				{
					ac = (ArrayContainer)ac.Mutate(this.CurrentAnonymousMethod.Storey.Mutator);
				}
				this.ig.Emit(OpCodes.Call, ac.GetAddressMethod());
				return;
			}
			TypeSpec typeSpec = this.IsAnonymousStoreyMutateRequired ? this.CurrentAnonymousMethod.Storey.Mutator.Mutate(ac.Element) : ac.Element;
			this.ig.Emit(OpCodes.Ldelema, typeSpec.GetMetaInfo());
		}

		// Token: 0x06000FAC RID: 4012 RVA: 0x0003FE3C File Offset: 0x0003E03C
		public void EmitArrayLoad(ArrayContainer ac)
		{
			if (ac.Rank > 1)
			{
				if (this.IsAnonymousStoreyMutateRequired)
				{
					ac = (ArrayContainer)ac.Mutate(this.CurrentAnonymousMethod.Storey.Mutator);
				}
				this.ig.Emit(OpCodes.Call, ac.GetGetMethod());
				return;
			}
			TypeSpec typeSpec = ac.Element;
			if (typeSpec.Kind == MemberKind.Enum)
			{
				typeSpec = EnumSpec.GetUnderlyingType(typeSpec);
			}
			switch (typeSpec.BuiltinType)
			{
			case BuiltinTypeSpec.Type.FirstPrimitive:
			case BuiltinTypeSpec.Type.Byte:
				this.ig.Emit(OpCodes.Ldelem_U1);
				return;
			case BuiltinTypeSpec.Type.SByte:
				this.ig.Emit(OpCodes.Ldelem_I1);
				return;
			case BuiltinTypeSpec.Type.Char:
			case BuiltinTypeSpec.Type.UShort:
				this.ig.Emit(OpCodes.Ldelem_U2);
				return;
			case BuiltinTypeSpec.Type.Short:
				this.ig.Emit(OpCodes.Ldelem_I2);
				return;
			case BuiltinTypeSpec.Type.Int:
				this.ig.Emit(OpCodes.Ldelem_I4);
				return;
			case BuiltinTypeSpec.Type.UInt:
				this.ig.Emit(OpCodes.Ldelem_U4);
				return;
			case BuiltinTypeSpec.Type.Long:
			case BuiltinTypeSpec.Type.ULong:
				this.ig.Emit(OpCodes.Ldelem_I8);
				return;
			case BuiltinTypeSpec.Type.Float:
				this.ig.Emit(OpCodes.Ldelem_R4);
				return;
			case BuiltinTypeSpec.Type.Double:
				this.ig.Emit(OpCodes.Ldelem_R8);
				return;
			case BuiltinTypeSpec.Type.IntPtr:
				this.ig.Emit(OpCodes.Ldelem_I);
				return;
			}
			MemberKind kind = typeSpec.Kind;
			if (kind == MemberKind.Struct)
			{
				if (this.IsAnonymousStoreyMutateRequired)
				{
					typeSpec = this.CurrentAnonymousMethod.Storey.Mutator.Mutate(typeSpec);
				}
				this.ig.Emit(OpCodes.Ldelema, typeSpec.GetMetaInfo());
				this.ig.Emit(OpCodes.Ldobj, typeSpec.GetMetaInfo());
				return;
			}
			if (kind == MemberKind.TypeParameter)
			{
				if (this.IsAnonymousStoreyMutateRequired)
				{
					typeSpec = this.CurrentAnonymousMethod.Storey.Mutator.Mutate(typeSpec);
				}
				this.ig.Emit(OpCodes.Ldelem, typeSpec.GetMetaInfo());
				return;
			}
			if (kind != MemberKind.PointerType)
			{
				this.ig.Emit(OpCodes.Ldelem_Ref);
				return;
			}
			this.ig.Emit(OpCodes.Ldelem_I);
		}

		// Token: 0x06000FAD RID: 4013 RVA: 0x0004006C File Offset: 0x0003E26C
		public void EmitArrayStore(ArrayContainer ac)
		{
			if (ac.Rank > 1)
			{
				if (this.IsAnonymousStoreyMutateRequired)
				{
					ac = (ArrayContainer)ac.Mutate(this.CurrentAnonymousMethod.Storey.Mutator);
				}
				this.ig.Emit(OpCodes.Call, ac.GetSetMethod());
				return;
			}
			TypeSpec typeSpec = ac.Element;
			if (typeSpec.Kind == MemberKind.Enum)
			{
				typeSpec = EnumSpec.GetUnderlyingType(typeSpec);
			}
			switch (typeSpec.BuiltinType)
			{
			case BuiltinTypeSpec.Type.FirstPrimitive:
			case BuiltinTypeSpec.Type.Byte:
			case BuiltinTypeSpec.Type.SByte:
				this.Emit(OpCodes.Stelem_I1);
				return;
			case BuiltinTypeSpec.Type.Char:
			case BuiltinTypeSpec.Type.Short:
			case BuiltinTypeSpec.Type.UShort:
				this.Emit(OpCodes.Stelem_I2);
				return;
			case BuiltinTypeSpec.Type.Int:
			case BuiltinTypeSpec.Type.UInt:
				this.Emit(OpCodes.Stelem_I4);
				return;
			case BuiltinTypeSpec.Type.Long:
			case BuiltinTypeSpec.Type.ULong:
				this.Emit(OpCodes.Stelem_I8);
				return;
			case BuiltinTypeSpec.Type.Float:
				this.Emit(OpCodes.Stelem_R4);
				return;
			case BuiltinTypeSpec.Type.Double:
				this.Emit(OpCodes.Stelem_R8);
				return;
			default:
			{
				MemberKind kind = typeSpec.Kind;
				if (kind == MemberKind.Struct)
				{
					this.Emit(OpCodes.Stobj, typeSpec);
					return;
				}
				if (kind == MemberKind.TypeParameter)
				{
					this.Emit(OpCodes.Stelem, typeSpec);
					return;
				}
				if (kind != MemberKind.PointerType)
				{
					this.Emit(OpCodes.Stelem_Ref);
					return;
				}
				this.Emit(OpCodes.Stelem_I);
				return;
			}
			}
		}

		// Token: 0x06000FAE RID: 4014 RVA: 0x000401B4 File Offset: 0x0003E3B4
		public void EmitInt(int i)
		{
			this.EmitIntConstant(i);
		}

		// Token: 0x06000FAF RID: 4015 RVA: 0x000401C0 File Offset: 0x0003E3C0
		private void EmitIntConstant(int i)
		{
			switch (i)
			{
			case -1:
				this.ig.Emit(OpCodes.Ldc_I4_M1);
				return;
			case 0:
				this.ig.Emit(OpCodes.Ldc_I4_0);
				return;
			case 1:
				this.ig.Emit(OpCodes.Ldc_I4_1);
				return;
			case 2:
				this.ig.Emit(OpCodes.Ldc_I4_2);
				return;
			case 3:
				this.ig.Emit(OpCodes.Ldc_I4_3);
				return;
			case 4:
				this.ig.Emit(OpCodes.Ldc_I4_4);
				return;
			case 5:
				this.ig.Emit(OpCodes.Ldc_I4_5);
				return;
			case 6:
				this.ig.Emit(OpCodes.Ldc_I4_6);
				return;
			case 7:
				this.ig.Emit(OpCodes.Ldc_I4_7);
				return;
			case 8:
				this.ig.Emit(OpCodes.Ldc_I4_8);
				return;
			default:
				if (i >= -128 && i <= 127)
				{
					this.ig.Emit(OpCodes.Ldc_I4_S, (sbyte)i);
					return;
				}
				this.ig.Emit(OpCodes.Ldc_I4, i);
				return;
			}
		}

		// Token: 0x06000FB0 RID: 4016 RVA: 0x000402DC File Offset: 0x0003E4DC
		public void EmitLong(long l)
		{
			if (l >= -2147483648L && l <= 2147483647L)
			{
				this.EmitIntConstant((int)l);
				this.ig.Emit(OpCodes.Conv_I8);
				return;
			}
			if (l >= 0L && l <= (long)((ulong)-1))
			{
				this.EmitIntConstant((int)l);
				this.ig.Emit(OpCodes.Conv_U8);
				return;
			}
			this.ig.Emit(OpCodes.Ldc_I8, l);
		}

		// Token: 0x06000FB1 RID: 4017 RVA: 0x00040348 File Offset: 0x0003E548
		public void EmitLoadFromPtr(TypeSpec type)
		{
			if (type.Kind == MemberKind.Enum)
			{
				type = EnumSpec.GetUnderlyingType(type);
			}
			switch (type.BuiltinType)
			{
			case BuiltinTypeSpec.Type.FirstPrimitive:
			case BuiltinTypeSpec.Type.SByte:
				this.ig.Emit(OpCodes.Ldind_I1);
				return;
			case BuiltinTypeSpec.Type.Byte:
				this.ig.Emit(OpCodes.Ldind_U1);
				return;
			case BuiltinTypeSpec.Type.Char:
			case BuiltinTypeSpec.Type.UShort:
				this.ig.Emit(OpCodes.Ldind_U2);
				return;
			case BuiltinTypeSpec.Type.Short:
				this.ig.Emit(OpCodes.Ldind_I2);
				return;
			case BuiltinTypeSpec.Type.Int:
				this.ig.Emit(OpCodes.Ldind_I4);
				return;
			case BuiltinTypeSpec.Type.UInt:
				this.ig.Emit(OpCodes.Ldind_U4);
				return;
			case BuiltinTypeSpec.Type.Long:
			case BuiltinTypeSpec.Type.ULong:
				this.ig.Emit(OpCodes.Ldind_I8);
				return;
			case BuiltinTypeSpec.Type.Float:
				this.ig.Emit(OpCodes.Ldind_R4);
				return;
			case BuiltinTypeSpec.Type.Double:
				this.ig.Emit(OpCodes.Ldind_R8);
				return;
			case BuiltinTypeSpec.Type.IntPtr:
				this.ig.Emit(OpCodes.Ldind_I);
				return;
			}
			MemberKind kind = type.Kind;
			if (kind == MemberKind.Struct || kind == MemberKind.TypeParameter)
			{
				if (this.IsAnonymousStoreyMutateRequired)
				{
					type = this.CurrentAnonymousMethod.Storey.Mutator.Mutate(type);
				}
				this.ig.Emit(OpCodes.Ldobj, type.GetMetaInfo());
				return;
			}
			if (kind != MemberKind.PointerType)
			{
				this.ig.Emit(OpCodes.Ldind_Ref);
				return;
			}
			this.ig.Emit(OpCodes.Ldind_I);
		}

		// Token: 0x06000FB2 RID: 4018 RVA: 0x000404D9 File Offset: 0x0003E6D9
		public void EmitNull()
		{
			this.ig.Emit(OpCodes.Ldnull);
		}

		// Token: 0x06000FB3 RID: 4019 RVA: 0x000404EB File Offset: 0x0003E6EB
		public void EmitArgumentAddress(int pos)
		{
			if (!this.IsStatic)
			{
				pos++;
			}
			if (pos > 255)
			{
				this.ig.Emit(OpCodes.Ldarga, pos);
				return;
			}
			this.ig.Emit(OpCodes.Ldarga_S, (byte)pos);
		}

		// Token: 0x06000FB4 RID: 4020 RVA: 0x00040528 File Offset: 0x0003E728
		public void EmitArgumentLoad(int pos)
		{
			if (!this.IsStatic)
			{
				pos++;
			}
			switch (pos)
			{
			case 0:
				this.ig.Emit(OpCodes.Ldarg_0);
				return;
			case 1:
				this.ig.Emit(OpCodes.Ldarg_1);
				return;
			case 2:
				this.ig.Emit(OpCodes.Ldarg_2);
				return;
			case 3:
				this.ig.Emit(OpCodes.Ldarg_3);
				return;
			default:
				if (pos > 255)
				{
					this.ig.Emit(OpCodes.Ldarg, pos);
					return;
				}
				this.ig.Emit(OpCodes.Ldarg_S, (byte)pos);
				return;
			}
		}

		// Token: 0x06000FB5 RID: 4021 RVA: 0x000405CA File Offset: 0x0003E7CA
		public void EmitArgumentStore(int pos)
		{
			if (!this.IsStatic)
			{
				pos++;
			}
			if (pos > 255)
			{
				this.ig.Emit(OpCodes.Starg, pos);
				return;
			}
			this.ig.Emit(OpCodes.Starg_S, (byte)pos);
		}

		// Token: 0x06000FB6 RID: 4022 RVA: 0x00040608 File Offset: 0x0003E808
		public void EmitStoreFromPtr(TypeSpec type)
		{
			if (type.IsEnum)
			{
				type = EnumSpec.GetUnderlyingType(type);
			}
			switch (type.BuiltinType)
			{
			case BuiltinTypeSpec.Type.FirstPrimitive:
			case BuiltinTypeSpec.Type.Byte:
			case BuiltinTypeSpec.Type.SByte:
				this.ig.Emit(OpCodes.Stind_I1);
				return;
			case BuiltinTypeSpec.Type.Char:
			case BuiltinTypeSpec.Type.Short:
			case BuiltinTypeSpec.Type.UShort:
				this.ig.Emit(OpCodes.Stind_I2);
				return;
			case BuiltinTypeSpec.Type.Int:
			case BuiltinTypeSpec.Type.UInt:
				this.ig.Emit(OpCodes.Stind_I4);
				return;
			case BuiltinTypeSpec.Type.Long:
			case BuiltinTypeSpec.Type.ULong:
				this.ig.Emit(OpCodes.Stind_I8);
				return;
			case BuiltinTypeSpec.Type.Float:
				this.ig.Emit(OpCodes.Stind_R4);
				return;
			case BuiltinTypeSpec.Type.Double:
				this.ig.Emit(OpCodes.Stind_R8);
				return;
			case BuiltinTypeSpec.Type.IntPtr:
				this.ig.Emit(OpCodes.Stind_I);
				return;
			}
			MemberKind kind = type.Kind;
			if (kind == MemberKind.Struct || kind == MemberKind.TypeParameter)
			{
				if (this.IsAnonymousStoreyMutateRequired)
				{
					type = this.CurrentAnonymousMethod.Storey.Mutator.Mutate(type);
				}
				this.ig.Emit(OpCodes.Stobj, type.GetMetaInfo());
				return;
			}
			if (kind != MemberKind.PointerType)
			{
				this.ig.Emit(OpCodes.Stind_Ref);
				return;
			}
			this.ig.Emit(OpCodes.Stind_I);
		}

		// Token: 0x06000FB7 RID: 4023 RVA: 0x0004075E File Offset: 0x0003E95E
		public void EmitThis()
		{
			this.ig.Emit(OpCodes.Ldarg_0);
		}

		// Token: 0x06000FB8 RID: 4024 RVA: 0x00040770 File Offset: 0x0003E970
		public void EmitEpilogue()
		{
			if (this.epilogue_expressions == null)
			{
				return;
			}
			foreach (IExpressionCleanup expressionCleanup in this.epilogue_expressions)
			{
				expressionCleanup.EmitCleanup(this);
			}
			this.epilogue_expressions = null;
		}

		// Token: 0x06000FB9 RID: 4025 RVA: 0x000407D4 File Offset: 0x0003E9D4
		public LocalBuilder GetTemporaryLocal(TypeSpec t)
		{
			if (this.temporary_storage != null)
			{
				object obj;
				if (this.temporary_storage.TryGetValue(t, out obj))
				{
					if (obj is Stack<LocalBuilder>)
					{
						Stack<LocalBuilder> stack = (Stack<LocalBuilder>)obj;
						obj = ((stack.Count == 0) ? null : stack.Pop());
					}
					else
					{
						this.temporary_storage.Remove(t);
					}
				}
				if (obj != null)
				{
					return (LocalBuilder)obj;
				}
			}
			return this.DeclareLocal(t, false);
		}

		// Token: 0x06000FBA RID: 4026 RVA: 0x0004083C File Offset: 0x0003EA3C
		public void FreeTemporaryLocal(LocalBuilder b, TypeSpec t)
		{
			if (this.temporary_storage == null)
			{
				this.temporary_storage = new Dictionary<TypeSpec, object>(ReferenceEquality<TypeSpec>.Default);
				this.temporary_storage.Add(t, b);
				return;
			}
			object obj;
			if (!this.temporary_storage.TryGetValue(t, out obj))
			{
				this.temporary_storage.Add(t, b);
				return;
			}
			Stack<LocalBuilder> stack = obj as Stack<LocalBuilder>;
			if (stack == null)
			{
				stack = new Stack<LocalBuilder>();
				stack.Push((LocalBuilder)obj);
				this.temporary_storage[t] = stack;
			}
			stack.Push(b);
		}

		// Token: 0x06000FBB RID: 4027 RVA: 0x000408BD File Offset: 0x0003EABD
		public LocalBuilder TemporaryReturn()
		{
			if (this.return_value == null)
			{
				this.return_value = this.DeclareLocal(this.return_type, false);
			}
			return this.return_value;
		}

		// Token: 0x0400070E RID: 1806
		public readonly ILGenerator ig;

		// Token: 0x0400070F RID: 1807
		private readonly TypeSpec return_type;

		// Token: 0x04000710 RID: 1808
		private Dictionary<TypeSpec, object> temporary_storage;

		// Token: 0x04000711 RID: 1809
		public LocalBuilder return_value;

		// Token: 0x04000712 RID: 1810
		public Label LoopBegin;

		// Token: 0x04000713 RID: 1811
		public Label LoopEnd;

		// Token: 0x04000714 RID: 1812
		public Label DefaultTarget;

		// Token: 0x04000715 RID: 1813
		public Switch Switch;

		// Token: 0x04000716 RID: 1814
		public AnonymousExpression CurrentAnonymousMethod;

		// Token: 0x04000717 RID: 1815
		private readonly IMemberContext member_context;

		// Token: 0x04000718 RID: 1816
		private readonly SourceMethodBuilder methodSymbols;

		// Token: 0x04000719 RID: 1817
		private DynamicSiteClass dynamic_site_container;

		// Token: 0x0400071A RID: 1818
		private Label? return_label;

		// Token: 0x0400071B RID: 1819
		private List<IExpressionCleanup> epilogue_expressions;

		// Token: 0x0400071C RID: 1820
		[CompilerGenerated]
		private ConditionalAccessContext <ConditionalAccess>k__BackingField;

		// Token: 0x0400071D RID: 1821
		[CompilerGenerated]
		private LocalVariable <AsyncThrowVariable>k__BackingField;

		// Token: 0x0400071E RID: 1822
		[CompilerGenerated]
		private List<TryFinally> <TryFinallyUnwind>k__BackingField;

		// Token: 0x0400071F RID: 1823
		[CompilerGenerated]
		private Label <RecursivePatternLabel>k__BackingField;
	}
}
