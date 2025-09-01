using System;
using System.Collections;
using System.Xml.Xsl.XsltOld.Debugger;

namespace System.Xml.Xsl.XsltOld
{
	// Token: 0x0200036B RID: 875
	internal class DbgCompiler : Compiler
	{
		// Token: 0x0600242E RID: 9262 RVA: 0x000DF56C File Offset: 0x000DD76C
		public DbgCompiler(IXsltDebugger debugger)
		{
			this.debugger = debugger;
		}

		// Token: 0x1700071C RID: 1820
		// (get) Token: 0x0600242F RID: 9263 RVA: 0x000DF591 File Offset: 0x000DD791
		public override IXsltDebugger Debugger
		{
			get
			{
				return this.debugger;
			}
		}

		// Token: 0x1700071D RID: 1821
		// (get) Token: 0x06002430 RID: 9264 RVA: 0x000DF599 File Offset: 0x000DD799
		public virtual VariableAction[] GlobalVariables
		{
			get
			{
				if (this.globalVarsCache == null)
				{
					this.globalVarsCache = (VariableAction[])this.globalVars.ToArray(typeof(VariableAction));
				}
				return this.globalVarsCache;
			}
		}

		// Token: 0x1700071E RID: 1822
		// (get) Token: 0x06002431 RID: 9265 RVA: 0x000DF5C9 File Offset: 0x000DD7C9
		public virtual VariableAction[] LocalVariables
		{
			get
			{
				if (this.localVarsCache == null)
				{
					this.localVarsCache = (VariableAction[])this.localVars.ToArray(typeof(VariableAction));
				}
				return this.localVarsCache;
			}
		}

		// Token: 0x06002432 RID: 9266 RVA: 0x000DF5FC File Offset: 0x000DD7FC
		private void DefineVariable(VariableAction variable)
		{
			if (variable.IsGlobal)
			{
				for (int i = 0; i < this.globalVars.Count; i++)
				{
					VariableAction variableAction = (VariableAction)this.globalVars[i];
					if (variableAction.Name == variable.Name)
					{
						if (variable.Stylesheetid < variableAction.Stylesheetid)
						{
							this.globalVars[i] = variable;
							this.globalVarsCache = null;
						}
						return;
					}
				}
				this.globalVars.Add(variable);
				this.globalVarsCache = null;
				return;
			}
			this.localVars.Add(variable);
			this.localVarsCache = null;
		}

		// Token: 0x06002433 RID: 9267 RVA: 0x000DF698 File Offset: 0x000DD898
		private void UnDefineVariables(int count)
		{
			if (count != 0)
			{
				this.localVars.RemoveRange(this.localVars.Count - count, count);
				this.localVarsCache = null;
			}
		}

		// Token: 0x06002434 RID: 9268 RVA: 0x000DF6BD File Offset: 0x000DD8BD
		internal override void PopScope()
		{
			this.UnDefineVariables(base.ScopeManager.CurrentScope.GetVeriablesCount());
			base.PopScope();
		}

		// Token: 0x06002435 RID: 9269 RVA: 0x000DF6DB File Offset: 0x000DD8DB
		public override ApplyImportsAction CreateApplyImportsAction()
		{
			DbgCompiler.ApplyImportsActionDbg applyImportsActionDbg = new DbgCompiler.ApplyImportsActionDbg();
			applyImportsActionDbg.Compile(this);
			return applyImportsActionDbg;
		}

		// Token: 0x06002436 RID: 9270 RVA: 0x000DF6E9 File Offset: 0x000DD8E9
		public override ApplyTemplatesAction CreateApplyTemplatesAction()
		{
			DbgCompiler.ApplyTemplatesActionDbg applyTemplatesActionDbg = new DbgCompiler.ApplyTemplatesActionDbg();
			applyTemplatesActionDbg.Compile(this);
			return applyTemplatesActionDbg;
		}

		// Token: 0x06002437 RID: 9271 RVA: 0x000DF6F7 File Offset: 0x000DD8F7
		public override AttributeAction CreateAttributeAction()
		{
			DbgCompiler.AttributeActionDbg attributeActionDbg = new DbgCompiler.AttributeActionDbg();
			attributeActionDbg.Compile(this);
			return attributeActionDbg;
		}

		// Token: 0x06002438 RID: 9272 RVA: 0x000DF705 File Offset: 0x000DD905
		public override AttributeSetAction CreateAttributeSetAction()
		{
			DbgCompiler.AttributeSetActionDbg attributeSetActionDbg = new DbgCompiler.AttributeSetActionDbg();
			attributeSetActionDbg.Compile(this);
			return attributeSetActionDbg;
		}

		// Token: 0x06002439 RID: 9273 RVA: 0x000DF713 File Offset: 0x000DD913
		public override CallTemplateAction CreateCallTemplateAction()
		{
			DbgCompiler.CallTemplateActionDbg callTemplateActionDbg = new DbgCompiler.CallTemplateActionDbg();
			callTemplateActionDbg.Compile(this);
			return callTemplateActionDbg;
		}

		// Token: 0x0600243A RID: 9274 RVA: 0x000DD5C0 File Offset: 0x000DB7C0
		public override ChooseAction CreateChooseAction()
		{
			ChooseAction chooseAction = new ChooseAction();
			chooseAction.Compile(this);
			return chooseAction;
		}

		// Token: 0x0600243B RID: 9275 RVA: 0x000DF721 File Offset: 0x000DD921
		public override CommentAction CreateCommentAction()
		{
			DbgCompiler.CommentActionDbg commentActionDbg = new DbgCompiler.CommentActionDbg();
			commentActionDbg.Compile(this);
			return commentActionDbg;
		}

		// Token: 0x0600243C RID: 9276 RVA: 0x000DF72F File Offset: 0x000DD92F
		public override CopyAction CreateCopyAction()
		{
			DbgCompiler.CopyActionDbg copyActionDbg = new DbgCompiler.CopyActionDbg();
			copyActionDbg.Compile(this);
			return copyActionDbg;
		}

		// Token: 0x0600243D RID: 9277 RVA: 0x000DF73D File Offset: 0x000DD93D
		public override CopyOfAction CreateCopyOfAction()
		{
			DbgCompiler.CopyOfActionDbg copyOfActionDbg = new DbgCompiler.CopyOfActionDbg();
			copyOfActionDbg.Compile(this);
			return copyOfActionDbg;
		}

		// Token: 0x0600243E RID: 9278 RVA: 0x000DF74B File Offset: 0x000DD94B
		public override ElementAction CreateElementAction()
		{
			DbgCompiler.ElementActionDbg elementActionDbg = new DbgCompiler.ElementActionDbg();
			elementActionDbg.Compile(this);
			return elementActionDbg;
		}

		// Token: 0x0600243F RID: 9279 RVA: 0x000DF759 File Offset: 0x000DD959
		public override ForEachAction CreateForEachAction()
		{
			DbgCompiler.ForEachActionDbg forEachActionDbg = new DbgCompiler.ForEachActionDbg();
			forEachActionDbg.Compile(this);
			return forEachActionDbg;
		}

		// Token: 0x06002440 RID: 9280 RVA: 0x000DF767 File Offset: 0x000DD967
		public override IfAction CreateIfAction(IfAction.ConditionType type)
		{
			DbgCompiler.IfActionDbg ifActionDbg = new DbgCompiler.IfActionDbg(type);
			ifActionDbg.Compile(this);
			return ifActionDbg;
		}

		// Token: 0x06002441 RID: 9281 RVA: 0x000DF776 File Offset: 0x000DD976
		public override MessageAction CreateMessageAction()
		{
			DbgCompiler.MessageActionDbg messageActionDbg = new DbgCompiler.MessageActionDbg();
			messageActionDbg.Compile(this);
			return messageActionDbg;
		}

		// Token: 0x06002442 RID: 9282 RVA: 0x000DF784 File Offset: 0x000DD984
		public override NewInstructionAction CreateNewInstructionAction()
		{
			DbgCompiler.NewInstructionActionDbg newInstructionActionDbg = new DbgCompiler.NewInstructionActionDbg();
			newInstructionActionDbg.Compile(this);
			return newInstructionActionDbg;
		}

		// Token: 0x06002443 RID: 9283 RVA: 0x000DF792 File Offset: 0x000DD992
		public override NumberAction CreateNumberAction()
		{
			DbgCompiler.NumberActionDbg numberActionDbg = new DbgCompiler.NumberActionDbg();
			numberActionDbg.Compile(this);
			return numberActionDbg;
		}

		// Token: 0x06002444 RID: 9284 RVA: 0x000DF7A0 File Offset: 0x000DD9A0
		public override ProcessingInstructionAction CreateProcessingInstructionAction()
		{
			DbgCompiler.ProcessingInstructionActionDbg processingInstructionActionDbg = new DbgCompiler.ProcessingInstructionActionDbg();
			processingInstructionActionDbg.Compile(this);
			return processingInstructionActionDbg;
		}

		// Token: 0x06002445 RID: 9285 RVA: 0x000DF7AE File Offset: 0x000DD9AE
		public override void CreateRootAction()
		{
			base.RootAction = new DbgCompiler.RootActionDbg();
			base.RootAction.Compile(this);
		}

		// Token: 0x06002446 RID: 9286 RVA: 0x000DF7C7 File Offset: 0x000DD9C7
		public override SortAction CreateSortAction()
		{
			DbgCompiler.SortActionDbg sortActionDbg = new DbgCompiler.SortActionDbg();
			sortActionDbg.Compile(this);
			return sortActionDbg;
		}

		// Token: 0x06002447 RID: 9287 RVA: 0x000DF7D5 File Offset: 0x000DD9D5
		public override TemplateAction CreateTemplateAction()
		{
			DbgCompiler.TemplateActionDbg templateActionDbg = new DbgCompiler.TemplateActionDbg();
			templateActionDbg.Compile(this);
			return templateActionDbg;
		}

		// Token: 0x06002448 RID: 9288 RVA: 0x000DF7E3 File Offset: 0x000DD9E3
		public override TemplateAction CreateSingleTemplateAction()
		{
			DbgCompiler.TemplateActionDbg templateActionDbg = new DbgCompiler.TemplateActionDbg();
			templateActionDbg.CompileSingle(this);
			return templateActionDbg;
		}

		// Token: 0x06002449 RID: 9289 RVA: 0x000DF7F1 File Offset: 0x000DD9F1
		public override TextAction CreateTextAction()
		{
			DbgCompiler.TextActionDbg textActionDbg = new DbgCompiler.TextActionDbg();
			textActionDbg.Compile(this);
			return textActionDbg;
		}

		// Token: 0x0600244A RID: 9290 RVA: 0x000DF7FF File Offset: 0x000DD9FF
		public override UseAttributeSetsAction CreateUseAttributeSetsAction()
		{
			DbgCompiler.UseAttributeSetsActionDbg useAttributeSetsActionDbg = new DbgCompiler.UseAttributeSetsActionDbg();
			useAttributeSetsActionDbg.Compile(this);
			return useAttributeSetsActionDbg;
		}

		// Token: 0x0600244B RID: 9291 RVA: 0x000DF80D File Offset: 0x000DDA0D
		public override ValueOfAction CreateValueOfAction()
		{
			DbgCompiler.ValueOfActionDbg valueOfActionDbg = new DbgCompiler.ValueOfActionDbg();
			valueOfActionDbg.Compile(this);
			return valueOfActionDbg;
		}

		// Token: 0x0600244C RID: 9292 RVA: 0x000DF81B File Offset: 0x000DDA1B
		public override VariableAction CreateVariableAction(VariableType type)
		{
			DbgCompiler.VariableActionDbg variableActionDbg = new DbgCompiler.VariableActionDbg(type);
			variableActionDbg.Compile(this);
			return variableActionDbg;
		}

		// Token: 0x0600244D RID: 9293 RVA: 0x000DF82A File Offset: 0x000DDA2A
		public override WithParamAction CreateWithParamAction()
		{
			DbgCompiler.WithParamActionDbg withParamActionDbg = new DbgCompiler.WithParamActionDbg();
			withParamActionDbg.Compile(this);
			return withParamActionDbg;
		}

		// Token: 0x0600244E RID: 9294 RVA: 0x000DF838 File Offset: 0x000DDA38
		public override BeginEvent CreateBeginEvent()
		{
			return new DbgCompiler.BeginEventDbg(this);
		}

		// Token: 0x0600244F RID: 9295 RVA: 0x000DF840 File Offset: 0x000DDA40
		public override TextEvent CreateTextEvent()
		{
			return new DbgCompiler.TextEventDbg(this);
		}

		// Token: 0x04001CE3 RID: 7395
		private IXsltDebugger debugger;

		// Token: 0x04001CE4 RID: 7396
		private ArrayList globalVars = new ArrayList();

		// Token: 0x04001CE5 RID: 7397
		private ArrayList localVars = new ArrayList();

		// Token: 0x04001CE6 RID: 7398
		private VariableAction[] globalVarsCache;

		// Token: 0x04001CE7 RID: 7399
		private VariableAction[] localVarsCache;

		// Token: 0x0200036C RID: 876
		private class ApplyImportsActionDbg : ApplyImportsAction
		{
			// Token: 0x06002450 RID: 9296 RVA: 0x000DF848 File Offset: 0x000DDA48
			internal override DbgData GetDbgData(ActionFrame frame)
			{
				return this.dbgData;
			}

			// Token: 0x06002451 RID: 9297 RVA: 0x000DF850 File Offset: 0x000DDA50
			internal override void Compile(Compiler compiler)
			{
				this.dbgData = new DbgData(compiler);
				base.Compile(compiler);
			}

			// Token: 0x06002452 RID: 9298 RVA: 0x000DF865 File Offset: 0x000DDA65
			internal override void Execute(Processor processor, ActionFrame frame)
			{
				if (frame.State == 0)
				{
					processor.OnInstructionExecute();
				}
				base.Execute(processor, frame);
			}

			// Token: 0x06002453 RID: 9299 RVA: 0x000DF87D File Offset: 0x000DDA7D
			public ApplyImportsActionDbg()
			{
			}

			// Token: 0x04001CE8 RID: 7400
			private DbgData dbgData;
		}

		// Token: 0x0200036D RID: 877
		private class ApplyTemplatesActionDbg : ApplyTemplatesAction
		{
			// Token: 0x06002454 RID: 9300 RVA: 0x000DF885 File Offset: 0x000DDA85
			internal override DbgData GetDbgData(ActionFrame frame)
			{
				return this.dbgData;
			}

			// Token: 0x06002455 RID: 9301 RVA: 0x000DF88D File Offset: 0x000DDA8D
			internal override void Compile(Compiler compiler)
			{
				this.dbgData = new DbgData(compiler);
				base.Compile(compiler);
			}

			// Token: 0x06002456 RID: 9302 RVA: 0x000DF8A2 File Offset: 0x000DDAA2
			internal override void Execute(Processor processor, ActionFrame frame)
			{
				if (frame.State == 0)
				{
					processor.OnInstructionExecute();
				}
				base.Execute(processor, frame);
			}

			// Token: 0x06002457 RID: 9303 RVA: 0x000DF8BA File Offset: 0x000DDABA
			public ApplyTemplatesActionDbg()
			{
			}

			// Token: 0x04001CE9 RID: 7401
			private DbgData dbgData;
		}

		// Token: 0x0200036E RID: 878
		private class AttributeActionDbg : AttributeAction
		{
			// Token: 0x06002458 RID: 9304 RVA: 0x000DF8C2 File Offset: 0x000DDAC2
			internal override DbgData GetDbgData(ActionFrame frame)
			{
				return this.dbgData;
			}

			// Token: 0x06002459 RID: 9305 RVA: 0x000DF8CA File Offset: 0x000DDACA
			internal override void Compile(Compiler compiler)
			{
				this.dbgData = new DbgData(compiler);
				base.Compile(compiler);
			}

			// Token: 0x0600245A RID: 9306 RVA: 0x000DF8DF File Offset: 0x000DDADF
			internal override void Execute(Processor processor, ActionFrame frame)
			{
				if (frame.State == 0)
				{
					processor.OnInstructionExecute();
				}
				base.Execute(processor, frame);
			}

			// Token: 0x0600245B RID: 9307 RVA: 0x000DF8F7 File Offset: 0x000DDAF7
			public AttributeActionDbg()
			{
			}

			// Token: 0x04001CEA RID: 7402
			private DbgData dbgData;
		}

		// Token: 0x0200036F RID: 879
		private class AttributeSetActionDbg : AttributeSetAction
		{
			// Token: 0x0600245C RID: 9308 RVA: 0x000DF8FF File Offset: 0x000DDAFF
			internal override DbgData GetDbgData(ActionFrame frame)
			{
				return this.dbgData;
			}

			// Token: 0x0600245D RID: 9309 RVA: 0x000DF907 File Offset: 0x000DDB07
			internal override void Compile(Compiler compiler)
			{
				this.dbgData = new DbgData(compiler);
				base.Compile(compiler);
			}

			// Token: 0x0600245E RID: 9310 RVA: 0x000DF91C File Offset: 0x000DDB1C
			internal override void Execute(Processor processor, ActionFrame frame)
			{
				if (frame.State == 0)
				{
					processor.OnInstructionExecute();
				}
				base.Execute(processor, frame);
			}

			// Token: 0x0600245F RID: 9311 RVA: 0x000DF934 File Offset: 0x000DDB34
			public AttributeSetActionDbg()
			{
			}

			// Token: 0x04001CEB RID: 7403
			private DbgData dbgData;
		}

		// Token: 0x02000370 RID: 880
		private class CallTemplateActionDbg : CallTemplateAction
		{
			// Token: 0x06002460 RID: 9312 RVA: 0x000DF93C File Offset: 0x000DDB3C
			internal override DbgData GetDbgData(ActionFrame frame)
			{
				return this.dbgData;
			}

			// Token: 0x06002461 RID: 9313 RVA: 0x000DF944 File Offset: 0x000DDB44
			internal override void Compile(Compiler compiler)
			{
				this.dbgData = new DbgData(compiler);
				base.Compile(compiler);
			}

			// Token: 0x06002462 RID: 9314 RVA: 0x000DF959 File Offset: 0x000DDB59
			internal override void Execute(Processor processor, ActionFrame frame)
			{
				if (frame.State == 0)
				{
					processor.OnInstructionExecute();
				}
				base.Execute(processor, frame);
			}

			// Token: 0x06002463 RID: 9315 RVA: 0x000DF971 File Offset: 0x000DDB71
			public CallTemplateActionDbg()
			{
			}

			// Token: 0x04001CEC RID: 7404
			private DbgData dbgData;
		}

		// Token: 0x02000371 RID: 881
		private class CommentActionDbg : CommentAction
		{
			// Token: 0x06002464 RID: 9316 RVA: 0x000DF979 File Offset: 0x000DDB79
			internal override DbgData GetDbgData(ActionFrame frame)
			{
				return this.dbgData;
			}

			// Token: 0x06002465 RID: 9317 RVA: 0x000DF981 File Offset: 0x000DDB81
			internal override void Compile(Compiler compiler)
			{
				this.dbgData = new DbgData(compiler);
				base.Compile(compiler);
			}

			// Token: 0x06002466 RID: 9318 RVA: 0x000DF996 File Offset: 0x000DDB96
			internal override void Execute(Processor processor, ActionFrame frame)
			{
				if (frame.State == 0)
				{
					processor.OnInstructionExecute();
				}
				base.Execute(processor, frame);
			}

			// Token: 0x06002467 RID: 9319 RVA: 0x000DF9AE File Offset: 0x000DDBAE
			public CommentActionDbg()
			{
			}

			// Token: 0x04001CED RID: 7405
			private DbgData dbgData;
		}

		// Token: 0x02000372 RID: 882
		private class CopyActionDbg : CopyAction
		{
			// Token: 0x06002468 RID: 9320 RVA: 0x000DF9B6 File Offset: 0x000DDBB6
			internal override DbgData GetDbgData(ActionFrame frame)
			{
				return this.dbgData;
			}

			// Token: 0x06002469 RID: 9321 RVA: 0x000DF9BE File Offset: 0x000DDBBE
			internal override void Compile(Compiler compiler)
			{
				this.dbgData = new DbgData(compiler);
				base.Compile(compiler);
			}

			// Token: 0x0600246A RID: 9322 RVA: 0x000DF9D3 File Offset: 0x000DDBD3
			internal override void Execute(Processor processor, ActionFrame frame)
			{
				if (frame.State == 0)
				{
					processor.OnInstructionExecute();
				}
				base.Execute(processor, frame);
			}

			// Token: 0x0600246B RID: 9323 RVA: 0x000DF9EB File Offset: 0x000DDBEB
			public CopyActionDbg()
			{
			}

			// Token: 0x04001CEE RID: 7406
			private DbgData dbgData;
		}

		// Token: 0x02000373 RID: 883
		private class CopyOfActionDbg : CopyOfAction
		{
			// Token: 0x0600246C RID: 9324 RVA: 0x000DF9F3 File Offset: 0x000DDBF3
			internal override DbgData GetDbgData(ActionFrame frame)
			{
				return this.dbgData;
			}

			// Token: 0x0600246D RID: 9325 RVA: 0x000DF9FB File Offset: 0x000DDBFB
			internal override void Compile(Compiler compiler)
			{
				this.dbgData = new DbgData(compiler);
				base.Compile(compiler);
			}

			// Token: 0x0600246E RID: 9326 RVA: 0x000DFA10 File Offset: 0x000DDC10
			internal override void Execute(Processor processor, ActionFrame frame)
			{
				if (frame.State == 0)
				{
					processor.OnInstructionExecute();
				}
				base.Execute(processor, frame);
			}

			// Token: 0x0600246F RID: 9327 RVA: 0x000DFA28 File Offset: 0x000DDC28
			public CopyOfActionDbg()
			{
			}

			// Token: 0x04001CEF RID: 7407
			private DbgData dbgData;
		}

		// Token: 0x02000374 RID: 884
		private class ElementActionDbg : ElementAction
		{
			// Token: 0x06002470 RID: 9328 RVA: 0x000DFA30 File Offset: 0x000DDC30
			internal override DbgData GetDbgData(ActionFrame frame)
			{
				return this.dbgData;
			}

			// Token: 0x06002471 RID: 9329 RVA: 0x000DFA38 File Offset: 0x000DDC38
			internal override void Compile(Compiler compiler)
			{
				this.dbgData = new DbgData(compiler);
				base.Compile(compiler);
			}

			// Token: 0x06002472 RID: 9330 RVA: 0x000DFA4D File Offset: 0x000DDC4D
			internal override void Execute(Processor processor, ActionFrame frame)
			{
				if (frame.State == 0)
				{
					processor.OnInstructionExecute();
				}
				base.Execute(processor, frame);
			}

			// Token: 0x06002473 RID: 9331 RVA: 0x000DFA65 File Offset: 0x000DDC65
			public ElementActionDbg()
			{
			}

			// Token: 0x04001CF0 RID: 7408
			private DbgData dbgData;
		}

		// Token: 0x02000375 RID: 885
		private class ForEachActionDbg : ForEachAction
		{
			// Token: 0x06002474 RID: 9332 RVA: 0x000DFA6D File Offset: 0x000DDC6D
			internal override DbgData GetDbgData(ActionFrame frame)
			{
				return this.dbgData;
			}

			// Token: 0x06002475 RID: 9333 RVA: 0x000DFA75 File Offset: 0x000DDC75
			internal override void Compile(Compiler compiler)
			{
				this.dbgData = new DbgData(compiler);
				base.Compile(compiler);
			}

			// Token: 0x06002476 RID: 9334 RVA: 0x000DFA8A File Offset: 0x000DDC8A
			internal override void Execute(Processor processor, ActionFrame frame)
			{
				if (frame.State == 0)
				{
					processor.PushDebuggerStack();
					processor.OnInstructionExecute();
				}
				base.Execute(processor, frame);
				if (frame.State == -1)
				{
					processor.PopDebuggerStack();
				}
			}

			// Token: 0x06002477 RID: 9335 RVA: 0x000DFAB7 File Offset: 0x000DDCB7
			public ForEachActionDbg()
			{
			}

			// Token: 0x04001CF1 RID: 7409
			private DbgData dbgData;
		}

		// Token: 0x02000376 RID: 886
		private class IfActionDbg : IfAction
		{
			// Token: 0x06002478 RID: 9336 RVA: 0x000DFABF File Offset: 0x000DDCBF
			internal IfActionDbg(IfAction.ConditionType type) : base(type)
			{
			}

			// Token: 0x06002479 RID: 9337 RVA: 0x000DFAC8 File Offset: 0x000DDCC8
			internal override DbgData GetDbgData(ActionFrame frame)
			{
				return this.dbgData;
			}

			// Token: 0x0600247A RID: 9338 RVA: 0x000DFAD0 File Offset: 0x000DDCD0
			internal override void Compile(Compiler compiler)
			{
				this.dbgData = new DbgData(compiler);
				base.Compile(compiler);
			}

			// Token: 0x0600247B RID: 9339 RVA: 0x000DFAE5 File Offset: 0x000DDCE5
			internal override void Execute(Processor processor, ActionFrame frame)
			{
				if (frame.State == 0)
				{
					processor.OnInstructionExecute();
				}
				base.Execute(processor, frame);
			}

			// Token: 0x04001CF2 RID: 7410
			private DbgData dbgData;
		}

		// Token: 0x02000377 RID: 887
		private class MessageActionDbg : MessageAction
		{
			// Token: 0x0600247C RID: 9340 RVA: 0x000DFAFD File Offset: 0x000DDCFD
			internal override DbgData GetDbgData(ActionFrame frame)
			{
				return this.dbgData;
			}

			// Token: 0x0600247D RID: 9341 RVA: 0x000DFB05 File Offset: 0x000DDD05
			internal override void Compile(Compiler compiler)
			{
				this.dbgData = new DbgData(compiler);
				base.Compile(compiler);
			}

			// Token: 0x0600247E RID: 9342 RVA: 0x000DFB1A File Offset: 0x000DDD1A
			internal override void Execute(Processor processor, ActionFrame frame)
			{
				if (frame.State == 0)
				{
					processor.OnInstructionExecute();
				}
				base.Execute(processor, frame);
			}

			// Token: 0x0600247F RID: 9343 RVA: 0x000DFB32 File Offset: 0x000DDD32
			public MessageActionDbg()
			{
			}

			// Token: 0x04001CF3 RID: 7411
			private DbgData dbgData;
		}

		// Token: 0x02000378 RID: 888
		private class NewInstructionActionDbg : NewInstructionAction
		{
			// Token: 0x06002480 RID: 9344 RVA: 0x000DFB3A File Offset: 0x000DDD3A
			internal override DbgData GetDbgData(ActionFrame frame)
			{
				return this.dbgData;
			}

			// Token: 0x06002481 RID: 9345 RVA: 0x000DFB42 File Offset: 0x000DDD42
			internal override void Compile(Compiler compiler)
			{
				this.dbgData = new DbgData(compiler);
				base.Compile(compiler);
			}

			// Token: 0x06002482 RID: 9346 RVA: 0x000DFB57 File Offset: 0x000DDD57
			internal override void Execute(Processor processor, ActionFrame frame)
			{
				if (frame.State == 0)
				{
					processor.OnInstructionExecute();
				}
				base.Execute(processor, frame);
			}

			// Token: 0x06002483 RID: 9347 RVA: 0x000DFB6F File Offset: 0x000DDD6F
			public NewInstructionActionDbg()
			{
			}

			// Token: 0x04001CF4 RID: 7412
			private DbgData dbgData;
		}

		// Token: 0x02000379 RID: 889
		private class NumberActionDbg : NumberAction
		{
			// Token: 0x06002484 RID: 9348 RVA: 0x000DFB77 File Offset: 0x000DDD77
			internal override DbgData GetDbgData(ActionFrame frame)
			{
				return this.dbgData;
			}

			// Token: 0x06002485 RID: 9349 RVA: 0x000DFB7F File Offset: 0x000DDD7F
			internal override void Compile(Compiler compiler)
			{
				this.dbgData = new DbgData(compiler);
				base.Compile(compiler);
			}

			// Token: 0x06002486 RID: 9350 RVA: 0x000DFB94 File Offset: 0x000DDD94
			internal override void Execute(Processor processor, ActionFrame frame)
			{
				if (frame.State == 0)
				{
					processor.OnInstructionExecute();
				}
				base.Execute(processor, frame);
			}

			// Token: 0x06002487 RID: 9351 RVA: 0x000DFBAC File Offset: 0x000DDDAC
			public NumberActionDbg()
			{
			}

			// Token: 0x04001CF5 RID: 7413
			private DbgData dbgData;
		}

		// Token: 0x0200037A RID: 890
		private class ProcessingInstructionActionDbg : ProcessingInstructionAction
		{
			// Token: 0x06002488 RID: 9352 RVA: 0x000DFBB4 File Offset: 0x000DDDB4
			internal override DbgData GetDbgData(ActionFrame frame)
			{
				return this.dbgData;
			}

			// Token: 0x06002489 RID: 9353 RVA: 0x000DFBBC File Offset: 0x000DDDBC
			internal override void Compile(Compiler compiler)
			{
				this.dbgData = new DbgData(compiler);
				base.Compile(compiler);
			}

			// Token: 0x0600248A RID: 9354 RVA: 0x000DFBD1 File Offset: 0x000DDDD1
			internal override void Execute(Processor processor, ActionFrame frame)
			{
				if (frame.State == 0)
				{
					processor.OnInstructionExecute();
				}
				base.Execute(processor, frame);
			}

			// Token: 0x0600248B RID: 9355 RVA: 0x000DFBE9 File Offset: 0x000DDDE9
			public ProcessingInstructionActionDbg()
			{
			}

			// Token: 0x04001CF6 RID: 7414
			private DbgData dbgData;
		}

		// Token: 0x0200037B RID: 891
		private class RootActionDbg : RootAction
		{
			// Token: 0x0600248C RID: 9356 RVA: 0x000DFBF1 File Offset: 0x000DDDF1
			internal override DbgData GetDbgData(ActionFrame frame)
			{
				return this.dbgData;
			}

			// Token: 0x0600248D RID: 9357 RVA: 0x000DFBFC File Offset: 0x000DDDFC
			internal override void Compile(Compiler compiler)
			{
				this.dbgData = new DbgData(compiler);
				base.Compile(compiler);
				string builtInTemplatesUri = compiler.Debugger.GetBuiltInTemplatesUri();
				if (builtInTemplatesUri != null && builtInTemplatesUri.Length != 0)
				{
					compiler.AllowBuiltInMode = true;
					this.builtInSheet = compiler.RootAction.CompileImport(compiler, compiler.ResolveUri(builtInTemplatesUri), int.MaxValue);
					compiler.AllowBuiltInMode = false;
				}
				this.dbgData.ReplaceVariables(((DbgCompiler)compiler).GlobalVariables);
			}

			// Token: 0x0600248E RID: 9358 RVA: 0x000DFC75 File Offset: 0x000DDE75
			internal override void Execute(Processor processor, ActionFrame frame)
			{
				if (frame.State == 0)
				{
					processor.PushDebuggerStack();
					processor.OnInstructionExecute();
					processor.PushDebuggerStack();
				}
				base.Execute(processor, frame);
				if (frame.State == -1)
				{
					processor.PopDebuggerStack();
					processor.PopDebuggerStack();
				}
			}

			// Token: 0x0600248F RID: 9359 RVA: 0x000DFCAE File Offset: 0x000DDEAE
			public RootActionDbg()
			{
			}

			// Token: 0x04001CF7 RID: 7415
			private DbgData dbgData;
		}

		// Token: 0x0200037C RID: 892
		private class SortActionDbg : SortAction
		{
			// Token: 0x06002490 RID: 9360 RVA: 0x000DFCB6 File Offset: 0x000DDEB6
			internal override DbgData GetDbgData(ActionFrame frame)
			{
				return this.dbgData;
			}

			// Token: 0x06002491 RID: 9361 RVA: 0x000DFCBE File Offset: 0x000DDEBE
			internal override void Compile(Compiler compiler)
			{
				this.dbgData = new DbgData(compiler);
				base.Compile(compiler);
			}

			// Token: 0x06002492 RID: 9362 RVA: 0x000DFCD3 File Offset: 0x000DDED3
			internal override void Execute(Processor processor, ActionFrame frame)
			{
				if (frame.State == 0)
				{
					processor.OnInstructionExecute();
				}
				base.Execute(processor, frame);
			}

			// Token: 0x06002493 RID: 9363 RVA: 0x000DFCEB File Offset: 0x000DDEEB
			public SortActionDbg()
			{
			}

			// Token: 0x04001CF8 RID: 7416
			private DbgData dbgData;
		}

		// Token: 0x0200037D RID: 893
		private class TemplateActionDbg : TemplateAction
		{
			// Token: 0x06002494 RID: 9364 RVA: 0x000DFCF3 File Offset: 0x000DDEF3
			internal override DbgData GetDbgData(ActionFrame frame)
			{
				return this.dbgData;
			}

			// Token: 0x06002495 RID: 9365 RVA: 0x000DFCFB File Offset: 0x000DDEFB
			internal override void Compile(Compiler compiler)
			{
				this.dbgData = new DbgData(compiler);
				base.Compile(compiler);
			}

			// Token: 0x06002496 RID: 9366 RVA: 0x000DFD10 File Offset: 0x000DDF10
			internal override void Execute(Processor processor, ActionFrame frame)
			{
				if (frame.State == 0)
				{
					processor.PushDebuggerStack();
					processor.OnInstructionExecute();
				}
				base.Execute(processor, frame);
				if (frame.State == -1)
				{
					processor.PopDebuggerStack();
				}
			}

			// Token: 0x06002497 RID: 9367 RVA: 0x000DFD3D File Offset: 0x000DDF3D
			public TemplateActionDbg()
			{
			}

			// Token: 0x04001CF9 RID: 7417
			private DbgData dbgData;
		}

		// Token: 0x0200037E RID: 894
		private class TextActionDbg : TextAction
		{
			// Token: 0x06002498 RID: 9368 RVA: 0x000DFD45 File Offset: 0x000DDF45
			internal override DbgData GetDbgData(ActionFrame frame)
			{
				return this.dbgData;
			}

			// Token: 0x06002499 RID: 9369 RVA: 0x000DFD4D File Offset: 0x000DDF4D
			internal override void Compile(Compiler compiler)
			{
				this.dbgData = new DbgData(compiler);
				base.Compile(compiler);
			}

			// Token: 0x0600249A RID: 9370 RVA: 0x000DFD62 File Offset: 0x000DDF62
			internal override void Execute(Processor processor, ActionFrame frame)
			{
				if (frame.State == 0)
				{
					processor.OnInstructionExecute();
				}
				base.Execute(processor, frame);
			}

			// Token: 0x0600249B RID: 9371 RVA: 0x000DFD7A File Offset: 0x000DDF7A
			public TextActionDbg()
			{
			}

			// Token: 0x04001CFA RID: 7418
			private DbgData dbgData;
		}

		// Token: 0x0200037F RID: 895
		private class UseAttributeSetsActionDbg : UseAttributeSetsAction
		{
			// Token: 0x0600249C RID: 9372 RVA: 0x000DFD82 File Offset: 0x000DDF82
			internal override DbgData GetDbgData(ActionFrame frame)
			{
				return this.dbgData;
			}

			// Token: 0x0600249D RID: 9373 RVA: 0x000DFD8A File Offset: 0x000DDF8A
			internal override void Compile(Compiler compiler)
			{
				this.dbgData = new DbgData(compiler);
				base.Compile(compiler);
			}

			// Token: 0x0600249E RID: 9374 RVA: 0x000DFD9F File Offset: 0x000DDF9F
			internal override void Execute(Processor processor, ActionFrame frame)
			{
				if (frame.State == 0)
				{
					processor.OnInstructionExecute();
				}
				base.Execute(processor, frame);
			}

			// Token: 0x0600249F RID: 9375 RVA: 0x000DFDB7 File Offset: 0x000DDFB7
			public UseAttributeSetsActionDbg()
			{
			}

			// Token: 0x04001CFB RID: 7419
			private DbgData dbgData;
		}

		// Token: 0x02000380 RID: 896
		private class ValueOfActionDbg : ValueOfAction
		{
			// Token: 0x060024A0 RID: 9376 RVA: 0x000DFDBF File Offset: 0x000DDFBF
			internal override DbgData GetDbgData(ActionFrame frame)
			{
				return this.dbgData;
			}

			// Token: 0x060024A1 RID: 9377 RVA: 0x000DFDC7 File Offset: 0x000DDFC7
			internal override void Compile(Compiler compiler)
			{
				this.dbgData = new DbgData(compiler);
				base.Compile(compiler);
			}

			// Token: 0x060024A2 RID: 9378 RVA: 0x000DFDDC File Offset: 0x000DDFDC
			internal override void Execute(Processor processor, ActionFrame frame)
			{
				if (frame.State == 0)
				{
					processor.OnInstructionExecute();
				}
				base.Execute(processor, frame);
			}

			// Token: 0x060024A3 RID: 9379 RVA: 0x000DFDF4 File Offset: 0x000DDFF4
			public ValueOfActionDbg()
			{
			}

			// Token: 0x04001CFC RID: 7420
			private DbgData dbgData;
		}

		// Token: 0x02000381 RID: 897
		private class VariableActionDbg : VariableAction
		{
			// Token: 0x060024A4 RID: 9380 RVA: 0x000DFDFC File Offset: 0x000DDFFC
			internal VariableActionDbg(VariableType type) : base(type)
			{
			}

			// Token: 0x060024A5 RID: 9381 RVA: 0x000DFE05 File Offset: 0x000DE005
			internal override DbgData GetDbgData(ActionFrame frame)
			{
				return this.dbgData;
			}

			// Token: 0x060024A6 RID: 9382 RVA: 0x000DFE0D File Offset: 0x000DE00D
			internal override void Compile(Compiler compiler)
			{
				this.dbgData = new DbgData(compiler);
				base.Compile(compiler);
				((DbgCompiler)compiler).DefineVariable(this);
			}

			// Token: 0x060024A7 RID: 9383 RVA: 0x000DFE2E File Offset: 0x000DE02E
			internal override void Execute(Processor processor, ActionFrame frame)
			{
				if (frame.State == 0)
				{
					processor.OnInstructionExecute();
				}
				base.Execute(processor, frame);
			}

			// Token: 0x04001CFD RID: 7421
			private DbgData dbgData;
		}

		// Token: 0x02000382 RID: 898
		private class WithParamActionDbg : WithParamAction
		{
			// Token: 0x060024A8 RID: 9384 RVA: 0x000DFE46 File Offset: 0x000DE046
			internal override DbgData GetDbgData(ActionFrame frame)
			{
				return this.dbgData;
			}

			// Token: 0x060024A9 RID: 9385 RVA: 0x000DFE4E File Offset: 0x000DE04E
			internal override void Compile(Compiler compiler)
			{
				this.dbgData = new DbgData(compiler);
				base.Compile(compiler);
			}

			// Token: 0x060024AA RID: 9386 RVA: 0x000DFE63 File Offset: 0x000DE063
			internal override void Execute(Processor processor, ActionFrame frame)
			{
				if (frame.State == 0)
				{
					processor.OnInstructionExecute();
				}
				base.Execute(processor, frame);
			}

			// Token: 0x060024AB RID: 9387 RVA: 0x000DFE7B File Offset: 0x000DE07B
			public WithParamActionDbg()
			{
			}

			// Token: 0x04001CFE RID: 7422
			private DbgData dbgData;
		}

		// Token: 0x02000383 RID: 899
		private class BeginEventDbg : BeginEvent
		{
			// Token: 0x1700071F RID: 1823
			// (get) Token: 0x060024AC RID: 9388 RVA: 0x000DFE83 File Offset: 0x000DE083
			internal override DbgData DbgData
			{
				get
				{
					return this.dbgData;
				}
			}

			// Token: 0x060024AD RID: 9389 RVA: 0x000DFE8B File Offset: 0x000DE08B
			public BeginEventDbg(Compiler compiler) : base(compiler)
			{
				this.dbgData = new DbgData(compiler);
			}

			// Token: 0x060024AE RID: 9390 RVA: 0x000DFEA0 File Offset: 0x000DE0A0
			public override bool Output(Processor processor, ActionFrame frame)
			{
				base.OnInstructionExecute(processor);
				return base.Output(processor, frame);
			}

			// Token: 0x04001CFF RID: 7423
			private DbgData dbgData;
		}

		// Token: 0x02000384 RID: 900
		private class TextEventDbg : TextEvent
		{
			// Token: 0x17000720 RID: 1824
			// (get) Token: 0x060024AF RID: 9391 RVA: 0x000DFEB1 File Offset: 0x000DE0B1
			internal override DbgData DbgData
			{
				get
				{
					return this.dbgData;
				}
			}

			// Token: 0x060024B0 RID: 9392 RVA: 0x000DFEB9 File Offset: 0x000DE0B9
			public TextEventDbg(Compiler compiler) : base(compiler)
			{
				this.dbgData = new DbgData(compiler);
			}

			// Token: 0x060024B1 RID: 9393 RVA: 0x000DFECE File Offset: 0x000DE0CE
			public override bool Output(Processor processor, ActionFrame frame)
			{
				base.OnInstructionExecute(processor);
				return base.Output(processor, frame);
			}

			// Token: 0x04001D00 RID: 7424
			private DbgData dbgData;
		}
	}
}
