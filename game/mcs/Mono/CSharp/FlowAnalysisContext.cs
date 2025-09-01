using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Mono.CSharp
{
	// Token: 0x0200015A RID: 346
	public class FlowAnalysisContext
	{
		// Token: 0x06001138 RID: 4408 RVA: 0x00047866 File Offset: 0x00045A66
		public FlowAnalysisContext(CompilerContext ctx, ParametersBlock parametersBlock, int definiteAssignmentLength)
		{
			this.ctx = ctx;
			this.ParametersBlock = parametersBlock;
			this.DefiniteAssignment = ((definiteAssignmentLength == 0) ? DefiniteAssignmentBitSet.Empty : new DefiniteAssignmentBitSet(definiteAssignmentLength));
		}

		// Token: 0x170004BE RID: 1214
		// (get) Token: 0x06001139 RID: 4409 RVA: 0x00047892 File Offset: 0x00045A92
		// (set) Token: 0x0600113A RID: 4410 RVA: 0x0004789A File Offset: 0x00045A9A
		public DefiniteAssignmentBitSet DefiniteAssignment
		{
			[CompilerGenerated]
			get
			{
				return this.<DefiniteAssignment>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<DefiniteAssignment>k__BackingField = value;
			}
		}

		// Token: 0x170004BF RID: 1215
		// (get) Token: 0x0600113B RID: 4411 RVA: 0x000478A3 File Offset: 0x00045AA3
		// (set) Token: 0x0600113C RID: 4412 RVA: 0x000478AB File Offset: 0x00045AAB
		public DefiniteAssignmentBitSet DefiniteAssignmentOnTrue
		{
			[CompilerGenerated]
			get
			{
				return this.<DefiniteAssignmentOnTrue>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<DefiniteAssignmentOnTrue>k__BackingField = value;
			}
		}

		// Token: 0x170004C0 RID: 1216
		// (get) Token: 0x0600113D RID: 4413 RVA: 0x000478B4 File Offset: 0x00045AB4
		// (set) Token: 0x0600113E RID: 4414 RVA: 0x000478BC File Offset: 0x00045ABC
		public DefiniteAssignmentBitSet DefiniteAssignmentOnFalse
		{
			[CompilerGenerated]
			get
			{
				return this.<DefiniteAssignmentOnFalse>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<DefiniteAssignmentOnFalse>k__BackingField = value;
			}
		}

		// Token: 0x170004C1 RID: 1217
		// (get) Token: 0x0600113F RID: 4415 RVA: 0x000478C5 File Offset: 0x00045AC5
		// (set) Token: 0x06001140 RID: 4416 RVA: 0x000478CD File Offset: 0x00045ACD
		private Dictionary<Statement, List<DefiniteAssignmentBitSet>> LabelStack
		{
			[CompilerGenerated]
			get
			{
				return this.<LabelStack>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<LabelStack>k__BackingField = value;
			}
		}

		// Token: 0x170004C2 RID: 1218
		// (get) Token: 0x06001141 RID: 4417 RVA: 0x000478D6 File Offset: 0x00045AD6
		// (set) Token: 0x06001142 RID: 4418 RVA: 0x000478DE File Offset: 0x00045ADE
		public ParametersBlock ParametersBlock
		{
			[CompilerGenerated]
			get
			{
				return this.<ParametersBlock>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<ParametersBlock>k__BackingField = value;
			}
		}

		// Token: 0x170004C3 RID: 1219
		// (get) Token: 0x06001143 RID: 4419 RVA: 0x000478E7 File Offset: 0x00045AE7
		public Report Report
		{
			get
			{
				return this.ctx.Report;
			}
		}

		// Token: 0x170004C4 RID: 1220
		// (get) Token: 0x06001144 RID: 4420 RVA: 0x000478F4 File Offset: 0x00045AF4
		// (set) Token: 0x06001145 RID: 4421 RVA: 0x000478FC File Offset: 0x00045AFC
		public DefiniteAssignmentBitSet SwitchInitialDefinitiveAssignment
		{
			[CompilerGenerated]
			get
			{
				return this.<SwitchInitialDefinitiveAssignment>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<SwitchInitialDefinitiveAssignment>k__BackingField = value;
			}
		}

		// Token: 0x170004C5 RID: 1221
		// (get) Token: 0x06001146 RID: 4422 RVA: 0x00047905 File Offset: 0x00045B05
		// (set) Token: 0x06001147 RID: 4423 RVA: 0x0004790D File Offset: 0x00045B0D
		public TryFinally TryFinally
		{
			[CompilerGenerated]
			get
			{
				return this.<TryFinally>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<TryFinally>k__BackingField = value;
			}
		}

		// Token: 0x170004C6 RID: 1222
		// (get) Token: 0x06001148 RID: 4424 RVA: 0x00047916 File Offset: 0x00045B16
		// (set) Token: 0x06001149 RID: 4425 RVA: 0x0004791E File Offset: 0x00045B1E
		public bool UnreachableReported
		{
			[CompilerGenerated]
			get
			{
				return this.<UnreachableReported>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<UnreachableReported>k__BackingField = value;
			}
		}

		// Token: 0x0600114A RID: 4426 RVA: 0x00047928 File Offset: 0x00045B28
		public bool AddReachedLabel(Statement label)
		{
			List<DefiniteAssignmentBitSet> list;
			if (this.LabelStack == null)
			{
				this.LabelStack = new Dictionary<Statement, List<DefiniteAssignmentBitSet>>();
				list = null;
			}
			else
			{
				this.LabelStack.TryGetValue(label, out list);
			}
			if (list == null)
			{
				list = new List<DefiniteAssignmentBitSet>();
				list.Add(new DefiniteAssignmentBitSet(this.DefiniteAssignment));
				this.LabelStack.Add(label, list);
				return false;
			}
			using (List<DefiniteAssignmentBitSet>.Enumerator enumerator = list.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (DefiniteAssignmentBitSet.AreEqual(enumerator.Current, this.DefiniteAssignment))
					{
						return true;
					}
				}
			}
			if (this.DefiniteAssignment == DefiniteAssignmentBitSet.Empty)
			{
				list.Add(this.DefiniteAssignment);
			}
			else
			{
				list.Add(new DefiniteAssignmentBitSet(this.DefiniteAssignment));
			}
			return false;
		}

		// Token: 0x0600114B RID: 4427 RVA: 0x00047A00 File Offset: 0x00045C00
		public DefiniteAssignmentBitSet BranchDefiniteAssignment()
		{
			return this.BranchDefiniteAssignment(this.DefiniteAssignment);
		}

		// Token: 0x0600114C RID: 4428 RVA: 0x00047A0E File Offset: 0x00045C0E
		public DefiniteAssignmentBitSet BranchDefiniteAssignment(DefiniteAssignmentBitSet da)
		{
			if (da != DefiniteAssignmentBitSet.Empty)
			{
				this.DefiniteAssignment = new DefiniteAssignmentBitSet(da);
			}
			return da;
		}

		// Token: 0x0600114D RID: 4429 RVA: 0x00047A25 File Offset: 0x00045C25
		public void BranchConditionalAccessDefiniteAssignment()
		{
			if (this.conditional_access == null)
			{
				this.conditional_access = this.BranchDefiniteAssignment();
			}
		}

		// Token: 0x0600114E RID: 4430 RVA: 0x00047A3B File Offset: 0x00045C3B
		public void ConditionalAccessEnd()
		{
			this.DefiniteAssignment = this.conditional_access;
			this.conditional_access = null;
		}

		// Token: 0x0600114F RID: 4431 RVA: 0x00047A50 File Offset: 0x00045C50
		public bool IsDefinitelyAssigned(VariableInfo variable)
		{
			return variable.IsAssigned(this.DefiniteAssignment);
		}

		// Token: 0x06001150 RID: 4432 RVA: 0x00047A5E File Offset: 0x00045C5E
		public bool IsStructFieldDefinitelyAssigned(VariableInfo variable, string name)
		{
			return variable.IsStructFieldAssigned(this.DefiniteAssignment, name);
		}

		// Token: 0x06001151 RID: 4433 RVA: 0x00047A6D File Offset: 0x00045C6D
		public void SetVariableAssigned(VariableInfo variable, bool generatedAssignment = false)
		{
			variable.SetAssigned(this.DefiniteAssignment, generatedAssignment);
		}

		// Token: 0x06001152 RID: 4434 RVA: 0x00047A7C File Offset: 0x00045C7C
		public void SetStructFieldAssigned(VariableInfo variable, string name)
		{
			variable.SetStructFieldAssigned(this.DefiniteAssignment, name);
		}

		// Token: 0x04000756 RID: 1878
		private readonly CompilerContext ctx;

		// Token: 0x04000757 RID: 1879
		private DefiniteAssignmentBitSet conditional_access;

		// Token: 0x04000758 RID: 1880
		[CompilerGenerated]
		private DefiniteAssignmentBitSet <DefiniteAssignment>k__BackingField;

		// Token: 0x04000759 RID: 1881
		[CompilerGenerated]
		private DefiniteAssignmentBitSet <DefiniteAssignmentOnTrue>k__BackingField;

		// Token: 0x0400075A RID: 1882
		[CompilerGenerated]
		private DefiniteAssignmentBitSet <DefiniteAssignmentOnFalse>k__BackingField;

		// Token: 0x0400075B RID: 1883
		[CompilerGenerated]
		private Dictionary<Statement, List<DefiniteAssignmentBitSet>> <LabelStack>k__BackingField;

		// Token: 0x0400075C RID: 1884
		[CompilerGenerated]
		private ParametersBlock <ParametersBlock>k__BackingField;

		// Token: 0x0400075D RID: 1885
		[CompilerGenerated]
		private DefiniteAssignmentBitSet <SwitchInitialDefinitiveAssignment>k__BackingField;

		// Token: 0x0400075E RID: 1886
		[CompilerGenerated]
		private TryFinally <TryFinally>k__BackingField;

		// Token: 0x0400075F RID: 1887
		[CompilerGenerated]
		private bool <UnreachableReported>k__BackingField;
	}
}
