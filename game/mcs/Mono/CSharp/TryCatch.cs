using System;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Mono.CSharp
{
	// Token: 0x020002C4 RID: 708
	public class TryCatch : ExceptionStatement
	{
		// Token: 0x06002220 RID: 8736 RVA: 0x000A740A File Offset: 0x000A560A
		public TryCatch(Block block, List<Catch> catch_clauses, Location l, bool inside_try_finally) : base(l)
		{
			this.Block = block;
			this.clauses = catch_clauses;
			this.inside_try_finally = inside_try_finally;
		}

		// Token: 0x170007C6 RID: 1990
		// (get) Token: 0x06002221 RID: 8737 RVA: 0x000A7429 File Offset: 0x000A5629
		public List<Catch> Clauses
		{
			get
			{
				return this.clauses;
			}
		}

		// Token: 0x170007C7 RID: 1991
		// (get) Token: 0x06002222 RID: 8738 RVA: 0x000A7431 File Offset: 0x000A5631
		public bool IsTryCatchFinally
		{
			get
			{
				return this.inside_try_finally;
			}
		}

		// Token: 0x06002223 RID: 8739 RVA: 0x000A743C File Offset: 0x000A563C
		public override bool Resolve(BlockContext bc)
		{
			bool flag;
			using (bc.Set(ResolveContext.Options.TryScope))
			{
				this.parent = bc.CurrentTryBlock;
				if (this.IsTryCatchFinally)
				{
					flag = this.Block.Resolve(bc);
				}
				else
				{
					using (bc.Set(ResolveContext.Options.TryWithCatchScope))
					{
						bc.CurrentTryBlock = this;
						flag = this.Block.Resolve(bc);
						bc.CurrentTryBlock = this.parent;
					}
				}
			}
			for (int i = 0; i < this.clauses.Count; i++)
			{
				Catch @catch = this.clauses[i];
				flag &= @catch.Resolve(bc);
				if (@catch.Block.HasAwait)
				{
					if (this.catch_sm == null)
					{
						this.catch_sm = new List<Catch>();
					}
					this.catch_sm.Add(@catch);
				}
				if (@catch.Filter == null)
				{
					TypeSpec catchType = @catch.CatchType;
					if (catchType != null)
					{
						for (int j = 0; j < this.clauses.Count; j++)
						{
							if (j != i && this.clauses[j].Filter == null)
							{
								if (this.clauses[j].IsGeneral)
								{
									if (catchType.BuiltinType == BuiltinTypeSpec.Type.Exception && bc.Module.DeclaringAssembly.WrapNonExceptionThrows && bc.Module.PredefinedAttributes.RuntimeCompatibility.IsDefined)
									{
										bc.Report.Warning(1058, 1, @catch.loc, "A previous catch clause already catches all exceptions. All non-exceptions thrown will be wrapped in a `System.Runtime.CompilerServices.RuntimeWrappedException'");
									}
								}
								else if (j < i)
								{
									TypeSpec catchType2 = this.clauses[j].CatchType;
									if (catchType2 != null && (catchType == catchType2 || TypeSpec.IsBaseClass(catchType, catchType2, true)))
									{
										bc.Report.Error(160, @catch.loc, "A previous catch clause already catches all exceptions of this or a super type `{0}'", catchType2.GetSignatureForError());
										flag = false;
									}
								}
							}
						}
					}
				}
			}
			return base.Resolve(bc) && flag;
		}

		// Token: 0x06002224 RID: 8740 RVA: 0x000A7668 File Offset: 0x000A5868
		protected sealed override void DoEmit(EmitContext ec)
		{
			if (!this.inside_try_finally)
			{
				this.EmitTryBodyPrepare(ec);
			}
			this.Block.Emit(ec);
			LocalBuilder localBuilder = null;
			foreach (Catch @catch in this.clauses)
			{
				@catch.Emit(ec);
				if (this.catch_sm != null)
				{
					if (localBuilder == null)
					{
						localBuilder = ec.DeclareLocal(ec.Module.Compiler.BuiltinTypes.Int, false);
					}
					int num = this.catch_sm.IndexOf(@catch);
					if (num >= 0)
					{
						ec.EmitInt(num + 1);
						ec.Emit(OpCodes.Stloc, localBuilder);
					}
				}
			}
			if (!this.inside_try_finally)
			{
				ec.EndExceptionBlock();
			}
			if (localBuilder != null)
			{
				ec.Emit(OpCodes.Ldloc, localBuilder);
				Label[] array = new Label[this.catch_sm.Count + 1];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = ec.DefineLabel();
				}
				Label label = ec.DefineLabel();
				ec.Emit(OpCodes.Switch, array);
				ec.MarkLabel(array[0]);
				ec.Emit(OpCodes.Br, label);
				LocalVariable asyncThrowVariable = ec.AsyncThrowVariable;
				Catch catch2 = null;
				for (int j = 0; j < this.catch_sm.Count; j++)
				{
					if (catch2 != null && catch2.Block.HasReachableClosingBrace)
					{
						ec.Emit(OpCodes.Br, label);
					}
					ec.MarkLabel(array[j + 1]);
					catch2 = this.catch_sm[j];
					ec.AsyncThrowVariable = catch2.Variable;
					catch2.Block.Emit(ec);
				}
				ec.AsyncThrowVariable = asyncThrowVariable;
				ec.MarkLabel(label);
			}
		}

		// Token: 0x06002225 RID: 8741 RVA: 0x000A783C File Offset: 0x000A5A3C
		protected override bool DoFlowAnalysis(FlowAnalysisContext fc)
		{
			DefiniteAssignmentBitSet definiteAssignmentBitSet = fc.BranchDefiniteAssignment();
			bool flag = this.Block.FlowAnalysis(fc);
			DefiniteAssignmentBitSet definiteAssignmentBitSet2 = flag ? null : fc.DefiniteAssignment;
			foreach (Statement statement in this.clauses)
			{
				fc.BranchDefiniteAssignment(definiteAssignmentBitSet);
				if (!statement.FlowAnalysis(fc))
				{
					if (definiteAssignmentBitSet2 == null)
					{
						definiteAssignmentBitSet2 = fc.DefiniteAssignment;
					}
					else
					{
						definiteAssignmentBitSet2 &= fc.DefiniteAssignment;
					}
					flag = false;
				}
			}
			fc.DefiniteAssignment = (definiteAssignmentBitSet2 ?? definiteAssignmentBitSet);
			this.parent = null;
			return flag;
		}

		// Token: 0x06002226 RID: 8742 RVA: 0x000A78E8 File Offset: 0x000A5AE8
		public override Reachability MarkReachable(Reachability rc)
		{
			if (rc.IsUnreachable)
			{
				return rc;
			}
			base.MarkReachable(rc);
			Reachability reachability = this.Block.MarkReachable(rc);
			foreach (Catch @catch in this.clauses)
			{
				reachability &= @catch.MarkReachable(rc);
			}
			return reachability;
		}

		// Token: 0x06002227 RID: 8743 RVA: 0x000A7964 File Offset: 0x000A5B64
		protected override void CloneTo(CloneContext clonectx, Statement t)
		{
			TryCatch tryCatch = (TryCatch)t;
			tryCatch.Block = clonectx.LookupBlock(this.Block);
			if (this.clauses != null)
			{
				tryCatch.clauses = new List<Catch>();
				foreach (Catch @catch in this.clauses)
				{
					tryCatch.clauses.Add((Catch)@catch.Clone(clonectx));
				}
			}
		}

		// Token: 0x06002228 RID: 8744 RVA: 0x000A79F4 File Offset: 0x000A5BF4
		public override object Accept(StructuralVisitor visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x04000C97 RID: 3223
		public Block Block;

		// Token: 0x04000C98 RID: 3224
		private List<Catch> clauses;

		// Token: 0x04000C99 RID: 3225
		private readonly bool inside_try_finally;

		// Token: 0x04000C9A RID: 3226
		private List<Catch> catch_sm;
	}
}
