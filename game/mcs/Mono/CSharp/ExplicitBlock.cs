using System;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Mono.CSharp
{
	// Token: 0x020002B5 RID: 693
	public class ExplicitBlock : Block
	{
		// Token: 0x0600214E RID: 8526 RVA: 0x000A2A39 File Offset: 0x000A0C39
		public ExplicitBlock(Block parent, Location start, Location end) : this(parent, (Block.Flags)0, start, end)
		{
		}

		// Token: 0x0600214F RID: 8527 RVA: 0x000A2A45 File Offset: 0x000A0C45
		public ExplicitBlock(Block parent, Block.Flags flags, Location start, Location end) : base(parent, flags, start, end)
		{
			this.Explicit = this;
		}

		// Token: 0x1700079D RID: 1949
		// (get) Token: 0x06002150 RID: 8528 RVA: 0x000A2A59 File Offset: 0x000A0C59
		public AnonymousMethodStorey AnonymousMethodStorey
		{
			get
			{
				return this.am_storey;
			}
		}

		// Token: 0x1700079E RID: 1950
		// (get) Token: 0x06002151 RID: 8529 RVA: 0x000A2A61 File Offset: 0x000A0C61
		public bool HasAwait
		{
			get
			{
				return (this.flags & Block.Flags.AwaitBlock) > (Block.Flags)0;
			}
		}

		// Token: 0x1700079F RID: 1951
		// (get) Token: 0x06002153 RID: 8531 RVA: 0x000A2A97 File Offset: 0x000A0C97
		// (set) Token: 0x06002152 RID: 8530 RVA: 0x000A2A72 File Offset: 0x000A0C72
		public bool HasCapturedThis
		{
			get
			{
				return (this.flags & Block.Flags.HasCapturedThis) > (Block.Flags)0;
			}
			set
			{
				this.flags = (value ? (this.flags | Block.Flags.HasCapturedThis) : (this.flags & ~Block.Flags.HasCapturedThis));
			}
		}

		// Token: 0x170007A0 RID: 1952
		// (get) Token: 0x06002155 RID: 8533 RVA: 0x000A2AC7 File Offset: 0x000A0CC7
		// (set) Token: 0x06002154 RID: 8532 RVA: 0x000A2AA8 File Offset: 0x000A0CA8
		public bool HasCapturedVariable
		{
			get
			{
				return (this.flags & Block.Flags.HasCapturedVariable) > (Block.Flags)0;
			}
			set
			{
				this.flags = (value ? (this.flags | Block.Flags.HasCapturedVariable) : (this.flags & ~Block.Flags.HasCapturedVariable));
			}
		}

		// Token: 0x170007A1 RID: 1953
		// (get) Token: 0x06002156 RID: 8534 RVA: 0x000A2AD5 File Offset: 0x000A0CD5
		// (set) Token: 0x06002157 RID: 8535 RVA: 0x000A2AE2 File Offset: 0x000A0CE2
		public bool HasReachableClosingBrace
		{
			get
			{
				return (this.flags & Block.Flags.ReachableEnd) > (Block.Flags)0;
			}
			set
			{
				this.flags = (value ? (this.flags | Block.Flags.ReachableEnd) : (this.flags & ~Block.Flags.ReachableEnd));
			}
		}

		// Token: 0x170007A2 RID: 1954
		// (get) Token: 0x06002158 RID: 8536 RVA: 0x000A2B00 File Offset: 0x000A0D00
		public bool HasYield
		{
			get
			{
				return (this.flags & Block.Flags.YieldBlock) > (Block.Flags)0;
			}
		}

		// Token: 0x06002159 RID: 8537 RVA: 0x000A2B14 File Offset: 0x000A0D14
		public AnonymousMethodStorey CreateAnonymousMethodStorey(ResolveContext ec)
		{
			if (ec.CurrentAnonymousMethod is StateMachineInitializer && this.ParametersBlock.Original == ec.CurrentAnonymousMethod.Block.Original)
			{
				return ec.CurrentAnonymousMethod.Storey;
			}
			if (this.am_storey == null)
			{
				MemberBase host = ec.MemberContext as MemberBase;
				this.am_storey = new AnonymousMethodStorey(this, ec.CurrentMemberDefinition.Parent.PartialContainer, host, ec.CurrentTypeParameters, "AnonStorey", MemberKind.Class);
			}
			return this.am_storey;
		}

		// Token: 0x0600215A RID: 8538 RVA: 0x000A2BA0 File Offset: 0x000A0DA0
		public void EmitScopeInitialization(EmitContext ec)
		{
			if ((this.flags & Block.Flags.InitializationEmitted) != (Block.Flags)0)
			{
				return;
			}
			if (this.am_storey != null)
			{
				this.DefineStoreyContainer(ec, this.am_storey);
				this.am_storey.EmitStoreyInstantiation(ec, this);
			}
			if (this.scope_initializers != null)
			{
				base.EmitScopeInitializers(ec);
			}
			this.flags |= Block.Flags.InitializationEmitted;
		}

		// Token: 0x0600215B RID: 8539 RVA: 0x000A2C00 File Offset: 0x000A0E00
		public override void Emit(EmitContext ec)
		{
			if (this.Parent != null)
			{
				ec.BeginScope();
			}
			this.EmitScopeInitialization(ec);
			if (ec.EmitAccurateDebugInfo && !base.IsCompilerGenerated && ec.Mark(this.StartLocation))
			{
				ec.Emit(OpCodes.Nop);
			}
			this.DoEmit(ec);
			if (this.Parent != null)
			{
				ec.EndScope();
			}
			if (ec.EmitAccurateDebugInfo && this.HasReachableClosingBrace && !(this is ParametersBlock) && !base.IsCompilerGenerated && ec.Mark(this.EndLocation))
			{
				ec.Emit(OpCodes.Nop);
			}
		}

		// Token: 0x0600215C RID: 8540 RVA: 0x000A2C9C File Offset: 0x000A0E9C
		protected void DefineStoreyContainer(EmitContext ec, AnonymousMethodStorey storey)
		{
			if (ec.CurrentAnonymousMethod != null && ec.CurrentAnonymousMethod.Storey != null)
			{
				storey.SetNestedStoryParent(ec.CurrentAnonymousMethod.Storey);
				storey.Mutator = ec.CurrentAnonymousMethod.Storey.Mutator;
			}
			storey.CreateContainer();
			storey.DefineContainer();
			if (base.Original.Explicit.HasCapturedThis && base.Original.ParametersBlock.TopBlock.ThisReferencesFromChildrenBlock != null)
			{
				for (Block block = base.Original.Explicit; block != null; block = block.Parent)
				{
					if (block.Parent != null)
					{
						AnonymousMethodStorey anonymousMethodStorey = block.Parent.Explicit.AnonymousMethodStorey;
						if (anonymousMethodStorey != null)
						{
							storey.HoistedThis = anonymousMethodStorey.HoistedThis;
							break;
						}
					}
					if (block.Explicit == block.Explicit.ParametersBlock && block.Explicit.ParametersBlock.StateMachine != null)
					{
						if (storey.HoistedThis == null)
						{
							storey.HoistedThis = block.Explicit.ParametersBlock.StateMachine.HoistedThis;
						}
						if (storey.HoistedThis != null)
						{
							break;
						}
					}
				}
				if (storey.HoistedThis == null || !(storey.Parent is HoistedStoreyClass))
				{
					foreach (ExplicitBlock explicitBlock in base.Original.ParametersBlock.TopBlock.ThisReferencesFromChildrenBlock)
					{
						Block block2 = explicitBlock;
						while (block2 != null && block2 != base.Original)
						{
							block2 = block2.Parent;
						}
						if (block2 != null)
						{
							if (storey.HoistedThis == null)
							{
								storey.AddCapturedThisField(ec, null);
							}
							ExplicitBlock explicitBlock2 = explicitBlock;
							while (explicitBlock2.AnonymousMethodStorey != storey)
							{
								AnonymousMethodStorey anonymousMethodStorey2 = explicitBlock2.AnonymousMethodStorey;
								ParametersBlock parametersBlock;
								if (anonymousMethodStorey2 != null)
								{
									if (explicitBlock2.ParametersBlock.StateMachine == null)
									{
										AnonymousMethodStorey anonymousMethodStorey3 = null;
										for (Block parent = explicitBlock2.AnonymousMethodStorey.OriginalSourceBlock.Parent; parent != null; parent = parent.Parent)
										{
											anonymousMethodStorey3 = parent.Explicit.AnonymousMethodStorey;
											if (anonymousMethodStorey3 != null)
											{
												break;
											}
										}
										if (anonymousMethodStorey3 == null)
										{
											AnonymousMethodStorey parent2 = (storey == null || storey.Kind == MemberKind.Struct) ? null : storey;
											explicitBlock2.AnonymousMethodStorey.AddCapturedThisField(ec, parent2);
											break;
										}
									}
									if (explicitBlock2.ParametersBlock == this.ParametersBlock.Original)
									{
										anonymousMethodStorey2.AddParentStoreyReference(ec, storey);
										break;
									}
									parametersBlock = (explicitBlock2 = explicitBlock2.ParametersBlock);
								}
								else
								{
									parametersBlock = (explicitBlock2 as ParametersBlock);
								}
								if (parametersBlock == null || parametersBlock.StateMachine == null)
								{
									goto IL_2FF;
								}
								if (parametersBlock.StateMachine == storey)
								{
									break;
								}
								ExplicitBlock explicitBlock3 = parametersBlock;
								while (explicitBlock3.Parent != null)
								{
									explicitBlock3 = explicitBlock3.Parent.Explicit;
									if (explicitBlock3.AnonymousMethodStorey != null)
									{
										break;
									}
								}
								if (explicitBlock3.AnonymousMethodStorey != null)
								{
									ParametersBlock parametersBlock2 = parametersBlock;
									while (parametersBlock2.Parent != null)
									{
										parametersBlock2 = parametersBlock2.Parent.ParametersBlock;
										if (parametersBlock2.StateMachine != null)
										{
											break;
										}
									}
									parametersBlock.StateMachine.AddParentStoreyReference(ec, parametersBlock2.StateMachine ?? storey);
									goto IL_2FF;
								}
								if (parametersBlock.StateMachine.HoistedThis == null)
								{
									parametersBlock.StateMachine.AddCapturedThisField(ec, null);
									explicitBlock2.HasCapturedThis = true;
								}
								IL_319:
								explicitBlock2 = explicitBlock2.Parent.Explicit;
								continue;
								IL_2FF:
								if (anonymousMethodStorey2 != null)
								{
									anonymousMethodStorey2.AddParentStoreyReference(ec, storey);
									anonymousMethodStorey2.HoistedThis = storey.HoistedThis;
									goto IL_319;
								}
								goto IL_319;
							}
						}
					}
				}
			}
			IList<ExplicitBlock> referencesFromChildrenBlock = storey.ReferencesFromChildrenBlock;
			if (referencesFromChildrenBlock != null)
			{
				foreach (ExplicitBlock explicitBlock4 in referencesFromChildrenBlock)
				{
					while (explicitBlock4.AnonymousMethodStorey != storey)
					{
						if (explicitBlock4.AnonymousMethodStorey != null)
						{
							explicitBlock4.AnonymousMethodStorey.AddParentStoreyReference(ec, storey);
							if (explicitBlock4.ParametersBlock == this.ParametersBlock.Original)
							{
								break;
							}
							explicitBlock4 = explicitBlock4.ParametersBlock;
						}
						ParametersBlock parametersBlock3 = explicitBlock4 as ParametersBlock;
						if (parametersBlock3 != null && parametersBlock3.StateMachine != null)
						{
							if (parametersBlock3.StateMachine == storey)
							{
								break;
							}
							parametersBlock3.StateMachine.AddParentStoreyReference(ec, storey);
						}
						explicitBlock4.HasCapturedVariable = true;
						explicitBlock4 = explicitBlock4.Parent.Explicit;
					}
				}
			}
			storey.Define();
			storey.PrepareEmit();
			storey.Parent.PartialContainer.AddCompilerGeneratedClass(storey);
		}

		// Token: 0x0600215D RID: 8541 RVA: 0x000A3110 File Offset: 0x000A1310
		public void RegisterAsyncAwait()
		{
			ExplicitBlock explicitBlock = this;
			while ((explicitBlock.flags & Block.Flags.AwaitBlock) == (Block.Flags)0)
			{
				explicitBlock.flags |= Block.Flags.AwaitBlock;
				if (explicitBlock is ParametersBlock)
				{
					return;
				}
				explicitBlock = explicitBlock.Parent.Explicit;
			}
		}

		// Token: 0x0600215E RID: 8542 RVA: 0x000A3158 File Offset: 0x000A1358
		public void RegisterIteratorYield()
		{
			this.ParametersBlock.TopBlock.IsIterator = true;
			ExplicitBlock explicitBlock = this;
			while ((explicitBlock.flags & Block.Flags.YieldBlock) == (Block.Flags)0)
			{
				explicitBlock.flags |= Block.Flags.YieldBlock;
				if (explicitBlock.Parent == null)
				{
					return;
				}
				explicitBlock = explicitBlock.Parent.Explicit;
			}
		}

		// Token: 0x0600215F RID: 8543 RVA: 0x000A31AF File Offset: 0x000A13AF
		public void SetCatchBlock()
		{
			this.flags |= Block.Flags.CatchBlock;
		}

		// Token: 0x06002160 RID: 8544 RVA: 0x000A31C3 File Offset: 0x000A13C3
		public void SetFinallyBlock()
		{
			this.flags |= Block.Flags.FinallyBlock;
		}

		// Token: 0x06002161 RID: 8545 RVA: 0x000A31D7 File Offset: 0x000A13D7
		public void WrapIntoDestructor(TryFinally tf, ExplicitBlock tryBlock)
		{
			tryBlock.statements = this.statements;
			this.statements = new List<Statement>(1);
			this.statements.Add(tf);
		}

		// Token: 0x04000C55 RID: 3157
		protected AnonymousMethodStorey am_storey;
	}
}
