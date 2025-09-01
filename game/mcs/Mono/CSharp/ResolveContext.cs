using System;

namespace Mono.CSharp
{
	// Token: 0x02000159 RID: 345
	public class ResolveContext : IMemberContext, IModuleContext
	{
		// Token: 0x0600111E RID: 4382 RVA: 0x000475E8 File Offset: 0x000457E8
		public ResolveContext(IMemberContext mc)
		{
			if (mc == null)
			{
				throw new ArgumentNullException();
			}
			this.MemberContext = mc;
			if (mc.Module.Compiler.Settings.Checked)
			{
				this.flags |= ResolveContext.Options.CheckedScope;
			}
			this.flags |= ResolveContext.Options.ConstantCheckState;
		}

		// Token: 0x0600111F RID: 4383 RVA: 0x0004763E File Offset: 0x0004583E
		public ResolveContext(IMemberContext mc, ResolveContext.Options options) : this(mc)
		{
			this.flags |= options;
		}

		// Token: 0x170004AF RID: 1199
		// (get) Token: 0x06001120 RID: 4384 RVA: 0x00047655 File Offset: 0x00045855
		public BuiltinTypes BuiltinTypes
		{
			get
			{
				return this.MemberContext.Module.Compiler.BuiltinTypes;
			}
		}

		// Token: 0x170004B0 RID: 1200
		// (get) Token: 0x06001121 RID: 4385 RVA: 0x0004766C File Offset: 0x0004586C
		public virtual ExplicitBlock ConstructorBlock
		{
			get
			{
				return this.CurrentBlock.Explicit;
			}
		}

		// Token: 0x170004B1 RID: 1201
		// (get) Token: 0x06001122 RID: 4386 RVA: 0x00047679 File Offset: 0x00045879
		public Iterator CurrentIterator
		{
			get
			{
				return this.CurrentAnonymousMethod as Iterator;
			}
		}

		// Token: 0x170004B2 RID: 1202
		// (get) Token: 0x06001123 RID: 4387 RVA: 0x00047686 File Offset: 0x00045886
		public TypeSpec CurrentType
		{
			get
			{
				return this.MemberContext.CurrentType;
			}
		}

		// Token: 0x170004B3 RID: 1203
		// (get) Token: 0x06001124 RID: 4388 RVA: 0x00047693 File Offset: 0x00045893
		public TypeParameters CurrentTypeParameters
		{
			get
			{
				return this.MemberContext.CurrentTypeParameters;
			}
		}

		// Token: 0x170004B4 RID: 1204
		// (get) Token: 0x06001125 RID: 4389 RVA: 0x000476A0 File Offset: 0x000458A0
		public MemberCore CurrentMemberDefinition
		{
			get
			{
				return this.MemberContext.CurrentMemberDefinition;
			}
		}

		// Token: 0x170004B5 RID: 1205
		// (get) Token: 0x06001126 RID: 4390 RVA: 0x000476AD File Offset: 0x000458AD
		public bool ConstantCheckState
		{
			get
			{
				return (this.flags & ResolveContext.Options.ConstantCheckState) > (ResolveContext.Options)0;
			}
		}

		// Token: 0x170004B6 RID: 1206
		// (get) Token: 0x06001127 RID: 4391 RVA: 0x000476BA File Offset: 0x000458BA
		public bool IsInProbingMode
		{
			get
			{
				return (this.flags & ResolveContext.Options.ProbingMode) > (ResolveContext.Options)0;
			}
		}

		// Token: 0x170004B7 RID: 1207
		// (get) Token: 0x06001128 RID: 4392 RVA: 0x000476CB File Offset: 0x000458CB
		public bool IsObsolete
		{
			get
			{
				return this.MemberContext.IsObsolete;
			}
		}

		// Token: 0x170004B8 RID: 1208
		// (get) Token: 0x06001129 RID: 4393 RVA: 0x000476D8 File Offset: 0x000458D8
		public bool IsStatic
		{
			get
			{
				return this.MemberContext.IsStatic;
			}
		}

		// Token: 0x170004B9 RID: 1209
		// (get) Token: 0x0600112A RID: 4394 RVA: 0x000476E5 File Offset: 0x000458E5
		public bool IsUnsafe
		{
			get
			{
				return this.HasSet(ResolveContext.Options.UnsafeScope) || this.MemberContext.IsUnsafe;
			}
		}

		// Token: 0x170004BA RID: 1210
		// (get) Token: 0x0600112B RID: 4395 RVA: 0x000476FD File Offset: 0x000458FD
		public bool IsRuntimeBinder
		{
			get
			{
				return this.Module.Compiler.IsRuntimeBinder;
			}
		}

		// Token: 0x170004BB RID: 1211
		// (get) Token: 0x0600112C RID: 4396 RVA: 0x0004770F File Offset: 0x0004590F
		public bool IsVariableCapturingRequired
		{
			get
			{
				return !this.IsInProbingMode;
			}
		}

		// Token: 0x170004BC RID: 1212
		// (get) Token: 0x0600112D RID: 4397 RVA: 0x0004771A File Offset: 0x0004591A
		public ModuleContainer Module
		{
			get
			{
				return this.MemberContext.Module;
			}
		}

		// Token: 0x170004BD RID: 1213
		// (get) Token: 0x0600112E RID: 4398 RVA: 0x00047727 File Offset: 0x00045927
		public Report Report
		{
			get
			{
				return this.Module.Compiler.Report;
			}
		}

		// Token: 0x0600112F RID: 4399 RVA: 0x0004773C File Offset: 0x0004593C
		public bool MustCaptureVariable(INamedBlockVariable local)
		{
			if (this.CurrentAnonymousMethod == null)
			{
				return false;
			}
			if (this.CurrentAnonymousMethod.IsIterator)
			{
				return local.IsParameter || local.Block.Explicit.HasYield;
			}
			if (this.CurrentAnonymousMethod is AsyncInitializer)
			{
				return local.IsParameter || local.Block.Explicit.HasAwait || this.CurrentBlock.Explicit.HasAwait || local.Block.ParametersBlock != this.CurrentBlock.ParametersBlock.Original;
			}
			return local.Block.ParametersBlock != this.CurrentBlock.ParametersBlock.Original;
		}

		// Token: 0x06001130 RID: 4400 RVA: 0x000477F7 File Offset: 0x000459F7
		public bool HasSet(ResolveContext.Options options)
		{
			return (this.flags & options) == options;
		}

		// Token: 0x06001131 RID: 4401 RVA: 0x00047804 File Offset: 0x00045A04
		public bool HasAny(ResolveContext.Options options)
		{
			return (this.flags & options) > (ResolveContext.Options)0;
		}

		// Token: 0x06001132 RID: 4402 RVA: 0x00047811 File Offset: 0x00045A11
		public ResolveContext.FlagsHandle Set(ResolveContext.Options options)
		{
			return new ResolveContext.FlagsHandle(this, options);
		}

		// Token: 0x06001133 RID: 4403 RVA: 0x0004781A File Offset: 0x00045A1A
		public ResolveContext.FlagsHandle With(ResolveContext.Options options, bool enable)
		{
			return new ResolveContext.FlagsHandle(this, options, enable ? options : ((ResolveContext.Options)0));
		}

		// Token: 0x06001134 RID: 4404 RVA: 0x0004782A File Offset: 0x00045A2A
		public string GetSignatureForError()
		{
			return this.MemberContext.GetSignatureForError();
		}

		// Token: 0x06001135 RID: 4405 RVA: 0x00047837 File Offset: 0x00045A37
		public ExtensionMethodCandidates LookupExtensionMethod(string name, int arity)
		{
			return this.MemberContext.LookupExtensionMethod(name, arity);
		}

		// Token: 0x06001136 RID: 4406 RVA: 0x00047846 File Offset: 0x00045A46
		public FullNamedExpression LookupNamespaceOrType(string name, int arity, LookupMode mode, Location loc)
		{
			return this.MemberContext.LookupNamespaceOrType(name, arity, mode, loc);
		}

		// Token: 0x06001137 RID: 4407 RVA: 0x00047858 File Offset: 0x00045A58
		public FullNamedExpression LookupNamespaceAlias(string name)
		{
			return this.MemberContext.LookupNamespaceAlias(name);
		}

		// Token: 0x04000751 RID: 1873
		protected ResolveContext.Options flags;

		// Token: 0x04000752 RID: 1874
		public AnonymousExpression CurrentAnonymousMethod;

		// Token: 0x04000753 RID: 1875
		public Expression CurrentInitializerVariable;

		// Token: 0x04000754 RID: 1876
		public Block CurrentBlock;

		// Token: 0x04000755 RID: 1877
		public readonly IMemberContext MemberContext;

		// Token: 0x0200038B RID: 907
		[Flags]
		public enum Options
		{
			// Token: 0x04000F65 RID: 3941
			CheckedScope = 1,
			// Token: 0x04000F66 RID: 3942
			ConstantCheckState = 2,
			// Token: 0x04000F67 RID: 3943
			AllCheckStateFlags = 3,
			// Token: 0x04000F68 RID: 3944
			UnsafeScope = 4,
			// Token: 0x04000F69 RID: 3945
			CatchScope = 8,
			// Token: 0x04000F6A RID: 3946
			FinallyScope = 16,
			// Token: 0x04000F6B RID: 3947
			FieldInitializerScope = 32,
			// Token: 0x04000F6C RID: 3948
			CompoundAssignmentScope = 64,
			// Token: 0x04000F6D RID: 3949
			FixedInitializerScope = 128,
			// Token: 0x04000F6E RID: 3950
			BaseInitializer = 256,
			// Token: 0x04000F6F RID: 3951
			EnumScope = 512,
			// Token: 0x04000F70 RID: 3952
			ConstantScope = 1024,
			// Token: 0x04000F71 RID: 3953
			ConstructorScope = 2048,
			// Token: 0x04000F72 RID: 3954
			UsingInitializerScope = 4096,
			// Token: 0x04000F73 RID: 3955
			LockScope = 8192,
			// Token: 0x04000F74 RID: 3956
			TryScope = 16384,
			// Token: 0x04000F75 RID: 3957
			TryWithCatchScope = 32768,
			// Token: 0x04000F76 RID: 3958
			ConditionalAccessReceiver = 65536,
			// Token: 0x04000F77 RID: 3959
			ProbingMode = 4194304,
			// Token: 0x04000F78 RID: 3960
			InferReturnType = 8388608,
			// Token: 0x04000F79 RID: 3961
			OmitDebuggingInfo = 16777216,
			// Token: 0x04000F7A RID: 3962
			ExpressionTreeConversion = 33554432,
			// Token: 0x04000F7B RID: 3963
			InvokeSpecialName = 67108864
		}

		// Token: 0x0200038C RID: 908
		public struct FlagsHandle : IDisposable
		{
			// Token: 0x060026BF RID: 9919 RVA: 0x000B6EA1 File Offset: 0x000B50A1
			public FlagsHandle(ResolveContext ec, ResolveContext.Options flagsToSet)
			{
				this = new ResolveContext.FlagsHandle(ec, flagsToSet, flagsToSet);
			}

			// Token: 0x060026C0 RID: 9920 RVA: 0x000B6EAC File Offset: 0x000B50AC
			public FlagsHandle(ResolveContext ec, ResolveContext.Options mask, ResolveContext.Options val)
			{
				this.ec = ec;
				this.invmask = ~mask;
				this.oldval = (ec.flags & mask);
				ec.flags = ((ec.flags & this.invmask) | (val & mask));
			}

			// Token: 0x060026C1 RID: 9921 RVA: 0x000B6EE2 File Offset: 0x000B50E2
			public void Dispose()
			{
				this.ec.flags = ((this.ec.flags & this.invmask) | this.oldval);
			}

			// Token: 0x04000F7C RID: 3964
			private readonly ResolveContext ec;

			// Token: 0x04000F7D RID: 3965
			private readonly ResolveContext.Options invmask;

			// Token: 0x04000F7E RID: 3966
			private readonly ResolveContext.Options oldval;
		}
	}
}
