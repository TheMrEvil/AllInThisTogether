using System;
using System.Runtime.CompilerServices;

namespace Mono.CSharp
{
	// Token: 0x02000230 RID: 560
	public abstract class StateMachine : AnonymousMethodStorey
	{
		// Token: 0x06001C4B RID: 7243 RVA: 0x00089554 File Offset: 0x00087754
		protected StateMachine(ParametersBlock block, TypeDefinition parent, MemberBase host, TypeParameters tparams, string name, MemberKind kind) : base(block, parent, host, tparams, name, kind)
		{
			this.OriginalTypeParameters = tparams;
		}

		// Token: 0x1700066D RID: 1645
		// (get) Token: 0x06001C4C RID: 7244 RVA: 0x0008956D File Offset: 0x0008776D
		// (set) Token: 0x06001C4D RID: 7245 RVA: 0x00089575 File Offset: 0x00087775
		public TypeParameters OriginalTypeParameters
		{
			[CompilerGenerated]
			get
			{
				return this.<OriginalTypeParameters>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<OriginalTypeParameters>k__BackingField = value;
			}
		}

		// Token: 0x1700066E RID: 1646
		// (get) Token: 0x06001C4E RID: 7246 RVA: 0x0008957E File Offset: 0x0008777E
		public StateMachineMethod StateMachineMethod
		{
			get
			{
				return this.method;
			}
		}

		// Token: 0x1700066F RID: 1647
		// (get) Token: 0x06001C4F RID: 7247 RVA: 0x00089586 File Offset: 0x00087786
		public Field PC
		{
			get
			{
				return this.pc_field;
			}
		}

		// Token: 0x06001C50 RID: 7248 RVA: 0x0008958E File Offset: 0x0008778E
		public void AddEntryMethod(StateMachineMethod method)
		{
			if (this.method != null)
			{
				throw new InternalErrorException();
			}
			this.method = method;
			base.Members.Add(method);
		}

		// Token: 0x06001C51 RID: 7249 RVA: 0x000895B1 File Offset: 0x000877B1
		protected override bool DoDefineMembers()
		{
			this.pc_field = base.AddCompilerGeneratedField("$PC", new TypeExpression(this.Compiler.BuiltinTypes.Int, base.Location));
			return base.DoDefineMembers();
		}

		// Token: 0x06001C52 RID: 7250 RVA: 0x000895E8 File Offset: 0x000877E8
		protected override string GetVariableMangledName(LocalVariable local_info)
		{
			if (local_info.IsCompilerGenerated)
			{
				return base.GetVariableMangledName(local_info);
			}
			string str = "<";
			string name = local_info.Name;
			string str2 = ">__";
			int num = this.local_name_idx;
			this.local_name_idx = num + 1;
			return str + name + str2 + num.ToString("X");
		}

		// Token: 0x04000A70 RID: 2672
		private Field pc_field;

		// Token: 0x04000A71 RID: 2673
		private StateMachineMethod method;

		// Token: 0x04000A72 RID: 2674
		private int local_name_idx;

		// Token: 0x04000A73 RID: 2675
		[CompilerGenerated]
		private TypeParameters <OriginalTypeParameters>k__BackingField;

		// Token: 0x020003C6 RID: 966
		public enum State
		{
			// Token: 0x040010BC RID: 4284
			Running = -3,
			// Token: 0x040010BD RID: 4285
			Uninitialized,
			// Token: 0x040010BE RID: 4286
			After,
			// Token: 0x040010BF RID: 4287
			Start
		}
	}
}
