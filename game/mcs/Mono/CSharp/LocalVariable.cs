using System;
using System.Reflection.Emit;

namespace Mono.CSharp
{
	// Token: 0x020002B3 RID: 691
	public sealed class LocalVariable : INamedBlockVariable, ILocalVariable
	{
		// Token: 0x06002105 RID: 8453 RVA: 0x000A1D27 File Offset: 0x0009FF27
		public LocalVariable(Block block, string name, Location loc)
		{
			this.block = block;
			this.name = name;
			this.loc = loc;
		}

		// Token: 0x06002106 RID: 8454 RVA: 0x000A1D44 File Offset: 0x0009FF44
		public LocalVariable(Block block, string name, LocalVariable.Flags flags, Location loc) : this(block, name, loc)
		{
			this.flags = flags;
		}

		// Token: 0x06002107 RID: 8455 RVA: 0x000A1D57 File Offset: 0x0009FF57
		public LocalVariable(LocalVariable li, string name, Location loc) : this(li.block, name, li.flags, loc)
		{
		}

		// Token: 0x17000787 RID: 1927
		// (get) Token: 0x06002108 RID: 8456 RVA: 0x000A1D6D File Offset: 0x0009FF6D
		public bool AddressTaken
		{
			get
			{
				return (this.flags & LocalVariable.Flags.AddressTaken) > (LocalVariable.Flags)0;
			}
		}

		// Token: 0x17000788 RID: 1928
		// (get) Token: 0x06002109 RID: 8457 RVA: 0x000A1D7A File Offset: 0x0009FF7A
		public Block Block
		{
			get
			{
				return this.block;
			}
		}

		// Token: 0x17000789 RID: 1929
		// (get) Token: 0x0600210A RID: 8458 RVA: 0x000A1D82 File Offset: 0x0009FF82
		// (set) Token: 0x0600210B RID: 8459 RVA: 0x000A1D8A File Offset: 0x0009FF8A
		public Constant ConstantValue
		{
			get
			{
				return this.const_value;
			}
			set
			{
				this.const_value = value;
			}
		}

		// Token: 0x1700078A RID: 1930
		// (get) Token: 0x0600210C RID: 8460 RVA: 0x000A1D93 File Offset: 0x0009FF93
		// (set) Token: 0x0600210D RID: 8461 RVA: 0x000A1D9B File Offset: 0x0009FF9B
		public HoistedVariable HoistedVariant
		{
			get
			{
				return this.hoisted_variant;
			}
			set
			{
				this.hoisted_variant = value;
			}
		}

		// Token: 0x1700078B RID: 1931
		// (get) Token: 0x0600210E RID: 8462 RVA: 0x000A1DA4 File Offset: 0x0009FFA4
		public bool IsDeclared
		{
			get
			{
				return this.type != null;
			}
		}

		// Token: 0x1700078C RID: 1932
		// (get) Token: 0x0600210F RID: 8463 RVA: 0x000A1DAF File Offset: 0x0009FFAF
		public bool IsCompilerGenerated
		{
			get
			{
				return (this.flags & LocalVariable.Flags.CompilerGenerated) > (LocalVariable.Flags)0;
			}
		}

		// Token: 0x1700078D RID: 1933
		// (get) Token: 0x06002110 RID: 8464 RVA: 0x000A1DBC File Offset: 0x0009FFBC
		public bool IsConstant
		{
			get
			{
				return (this.flags & LocalVariable.Flags.Constant) > (LocalVariable.Flags)0;
			}
		}

		// Token: 0x1700078E RID: 1934
		// (get) Token: 0x06002111 RID: 8465 RVA: 0x000A1DCA File Offset: 0x0009FFCA
		// (set) Token: 0x06002112 RID: 8466 RVA: 0x000A1DDB File Offset: 0x0009FFDB
		public bool IsLocked
		{
			get
			{
				return (this.flags & LocalVariable.Flags.IsLocked) > (LocalVariable.Flags)0;
			}
			set
			{
				this.flags = (value ? (this.flags | LocalVariable.Flags.IsLocked) : (this.flags & ~LocalVariable.Flags.IsLocked));
			}
		}

		// Token: 0x1700078F RID: 1935
		// (get) Token: 0x06002113 RID: 8467 RVA: 0x000A1E00 File Offset: 0x000A0000
		public bool IsThis
		{
			get
			{
				return (this.flags & LocalVariable.Flags.IsThis) > (LocalVariable.Flags)0;
			}
		}

		// Token: 0x17000790 RID: 1936
		// (get) Token: 0x06002114 RID: 8468 RVA: 0x000A1E0D File Offset: 0x000A000D
		public bool IsFixed
		{
			get
			{
				return (this.flags & LocalVariable.Flags.FixedVariable) > (LocalVariable.Flags)0;
			}
		}

		// Token: 0x17000791 RID: 1937
		// (get) Token: 0x06002115 RID: 8469 RVA: 0x000022F4 File Offset: 0x000004F4
		bool INamedBlockVariable.IsParameter
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000792 RID: 1938
		// (get) Token: 0x06002116 RID: 8470 RVA: 0x000A1E1B File Offset: 0x000A001B
		public bool IsReadonly
		{
			get
			{
				return (this.flags & LocalVariable.Flags.ReadonlyMask) > (LocalVariable.Flags)0;
			}
		}

		// Token: 0x17000793 RID: 1939
		// (get) Token: 0x06002117 RID: 8471 RVA: 0x000A1E2C File Offset: 0x000A002C
		public Location Location
		{
			get
			{
				return this.loc;
			}
		}

		// Token: 0x17000794 RID: 1940
		// (get) Token: 0x06002118 RID: 8472 RVA: 0x000A1E34 File Offset: 0x000A0034
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000795 RID: 1941
		// (get) Token: 0x06002119 RID: 8473 RVA: 0x000A1E3C File Offset: 0x000A003C
		// (set) Token: 0x0600211A RID: 8474 RVA: 0x000A1E44 File Offset: 0x000A0044
		public TypeSpec Type
		{
			get
			{
				return this.type;
			}
			set
			{
				this.type = value;
			}
		}

		// Token: 0x0600211B RID: 8475 RVA: 0x000A1E50 File Offset: 0x000A0050
		public void CreateBuilder(EmitContext ec)
		{
			if ((this.flags & LocalVariable.Flags.Used) == (LocalVariable.Flags)0)
			{
				if (this.VariableInfo == null)
				{
					throw new InternalErrorException("VariableInfo is null and the variable `{0}' is not used", new object[]
					{
						this.name
					});
				}
				if (this.VariableInfo.IsEverAssigned)
				{
					ec.Report.Warning(219, 3, this.Location, "The variable `{0}' is assigned but its value is never used", this.Name);
				}
				else
				{
					ec.Report.Warning(168, 3, this.Location, "The variable `{0}' is declared but never used", this.Name);
				}
			}
			if (this.HoistedVariant != null)
			{
				return;
			}
			if (this.builder == null)
			{
				this.builder = ec.DeclareLocal(this.Type, this.IsFixed);
				if (!ec.HasSet(BuilderContext.Options.OmitDebugInfo) && (this.flags & LocalVariable.Flags.CompilerGenerated) == (LocalVariable.Flags)0)
				{
					ec.DefineLocalVariable(this.name, this.builder);
				}
				return;
			}
			if ((this.flags & LocalVariable.Flags.CompilerGenerated) != (LocalVariable.Flags)0)
			{
				return;
			}
			throw new InternalErrorException("Already created variable `{0}'", new object[]
			{
				this.name
			});
		}

		// Token: 0x0600211C RID: 8476 RVA: 0x000A1F4F File Offset: 0x000A014F
		public static LocalVariable CreateCompilerGenerated(TypeSpec type, Block block, Location loc)
		{
			return new LocalVariable(block, LocalVariable.GetCompilerGeneratedName(block), LocalVariable.Flags.Used | LocalVariable.Flags.CompilerGenerated, loc)
			{
				Type = type
			};
		}

		// Token: 0x0600211D RID: 8477 RVA: 0x000A1F67 File Offset: 0x000A0167
		public Expression CreateReferenceExpression(ResolveContext rc, Location loc)
		{
			if (this.IsConstant && this.const_value != null)
			{
				return Constant.CreateConstantFromValue(this.Type, this.const_value.GetValue(), loc);
			}
			return new LocalVariableReference(this, loc);
		}

		// Token: 0x0600211E RID: 8478 RVA: 0x000A1F98 File Offset: 0x000A0198
		public void Emit(EmitContext ec)
		{
			if ((this.flags & LocalVariable.Flags.CompilerGenerated) != (LocalVariable.Flags)0)
			{
				this.CreateBuilder(ec);
			}
			ec.Emit(OpCodes.Ldloc, this.builder);
		}

		// Token: 0x0600211F RID: 8479 RVA: 0x000A1FBC File Offset: 0x000A01BC
		public void EmitAssign(EmitContext ec)
		{
			if ((this.flags & LocalVariable.Flags.CompilerGenerated) != (LocalVariable.Flags)0)
			{
				this.CreateBuilder(ec);
			}
			ec.Emit(OpCodes.Stloc, this.builder);
		}

		// Token: 0x06002120 RID: 8480 RVA: 0x000A1FE0 File Offset: 0x000A01E0
		public void EmitAddressOf(EmitContext ec)
		{
			if ((this.flags & LocalVariable.Flags.CompilerGenerated) != (LocalVariable.Flags)0)
			{
				this.CreateBuilder(ec);
			}
			ec.Emit(OpCodes.Ldloca, this.builder);
		}

		// Token: 0x06002121 RID: 8481 RVA: 0x000A2004 File Offset: 0x000A0204
		public static string GetCompilerGeneratedName(Block block)
		{
			string str = "$locvar";
			ParametersBlock parametersBlock = block.ParametersBlock;
			int temporaryLocalsCount = parametersBlock.TemporaryLocalsCount;
			parametersBlock.TemporaryLocalsCount = temporaryLocalsCount + 1;
			return str + temporaryLocalsCount.ToString("X");
		}

		// Token: 0x06002122 RID: 8482 RVA: 0x000A203C File Offset: 0x000A023C
		public string GetReadOnlyContext()
		{
			LocalVariable.Flags flags = this.flags & LocalVariable.Flags.ReadonlyMask;
			if (flags == LocalVariable.Flags.ForeachVariable)
			{
				return "foreach iteration variable";
			}
			if (flags == LocalVariable.Flags.FixedVariable)
			{
				return "fixed variable";
			}
			if (flags != LocalVariable.Flags.UsingVariable)
			{
				throw new InternalErrorException("Variable is not readonly");
			}
			return "using variable";
		}

		// Token: 0x06002123 RID: 8483 RVA: 0x000A2086 File Offset: 0x000A0286
		public bool IsThisAssigned(FlowAnalysisContext fc, Block block)
		{
			if (this.VariableInfo == null)
			{
				throw new Exception();
			}
			return this.IsAssigned(fc) || this.VariableInfo.IsFullyInitialized(fc, block.StartLocation);
		}

		// Token: 0x06002124 RID: 8484 RVA: 0x000A20B3 File Offset: 0x000A02B3
		public bool IsAssigned(FlowAnalysisContext fc)
		{
			return fc.IsDefinitelyAssigned(this.VariableInfo);
		}

		// Token: 0x06002125 RID: 8485 RVA: 0x000A20C1 File Offset: 0x000A02C1
		public void PrepareAssignmentAnalysis(BlockContext bc)
		{
			if ((this.flags & (LocalVariable.Flags.CompilerGenerated | LocalVariable.Flags.Constant | LocalVariable.Flags.ForeachVariable | LocalVariable.Flags.FixedVariable | LocalVariable.Flags.UsingVariable)) != (LocalVariable.Flags)0)
			{
				return;
			}
			this.VariableInfo = VariableInfo.Create(bc, this);
		}

		// Token: 0x06002126 RID: 8486 RVA: 0x000A20DF File Offset: 0x000A02DF
		public void SetIsUsed()
		{
			this.flags |= LocalVariable.Flags.Used;
		}

		// Token: 0x06002127 RID: 8487 RVA: 0x000A20EF File Offset: 0x000A02EF
		public void SetHasAddressTaken()
		{
			this.flags |= (LocalVariable.Flags.Used | LocalVariable.Flags.AddressTaken);
		}

		// Token: 0x06002128 RID: 8488 RVA: 0x000A20FF File Offset: 0x000A02FF
		public override string ToString()
		{
			return string.Format("LocalInfo ({0},{1},{2},{3})", new object[]
			{
				this.name,
				this.type,
				this.VariableInfo,
				this.Location
			});
		}

		// Token: 0x04000C42 RID: 3138
		private TypeSpec type;

		// Token: 0x04000C43 RID: 3139
		private readonly string name;

		// Token: 0x04000C44 RID: 3140
		private readonly Location loc;

		// Token: 0x04000C45 RID: 3141
		private readonly Block block;

		// Token: 0x04000C46 RID: 3142
		private LocalVariable.Flags flags;

		// Token: 0x04000C47 RID: 3143
		private Constant const_value;

		// Token: 0x04000C48 RID: 3144
		public VariableInfo VariableInfo;

		// Token: 0x04000C49 RID: 3145
		private HoistedVariable hoisted_variant;

		// Token: 0x04000C4A RID: 3146
		private LocalBuilder builder;

		// Token: 0x020003F0 RID: 1008
		[Flags]
		public enum Flags
		{
			// Token: 0x0400112D RID: 4397
			Used = 1,
			// Token: 0x0400112E RID: 4398
			IsThis = 2,
			// Token: 0x0400112F RID: 4399
			AddressTaken = 4,
			// Token: 0x04001130 RID: 4400
			CompilerGenerated = 8,
			// Token: 0x04001131 RID: 4401
			Constant = 16,
			// Token: 0x04001132 RID: 4402
			ForeachVariable = 32,
			// Token: 0x04001133 RID: 4403
			FixedVariable = 64,
			// Token: 0x04001134 RID: 4404
			UsingVariable = 128,
			// Token: 0x04001135 RID: 4405
			IsLocked = 256,
			// Token: 0x04001136 RID: 4406
			ReadonlyMask = 224
		}
	}
}
