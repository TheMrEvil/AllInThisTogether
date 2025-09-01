using System;
using Mono.CSharp.Linq;
using Mono.CSharp.Nullable;

namespace Mono.CSharp
{
	// Token: 0x020002E8 RID: 744
	public abstract class StructuralVisitor
	{
		// Token: 0x06002371 RID: 9073 RVA: 0x0000AF70 File Offset: 0x00009170
		public virtual void Visit(MemberCore member)
		{
		}

		// Token: 0x06002372 RID: 9074 RVA: 0x000AD0E0 File Offset: 0x000AB2E0
		private void VisitTypeContainer(TypeContainer tc)
		{
			foreach (TypeContainer typeContainer in tc.Containers)
			{
				typeContainer.Accept(this);
			}
		}

		// Token: 0x06002373 RID: 9075 RVA: 0x000AD12C File Offset: 0x000AB32C
		private void VisitTypeContainer(TypeDefinition tc)
		{
			foreach (MemberCore memberCore in tc.Members)
			{
				memberCore.Accept(this);
			}
		}

		// Token: 0x06002374 RID: 9076 RVA: 0x000AD180 File Offset: 0x000AB380
		public virtual void Visit(ModuleContainer module)
		{
			this.VisitTypeContainer(module);
		}

		// Token: 0x06002375 RID: 9077 RVA: 0x0000AF70 File Offset: 0x00009170
		public virtual void Visit(UsingNamespace un)
		{
		}

		// Token: 0x06002376 RID: 9078 RVA: 0x0000AF70 File Offset: 0x00009170
		public virtual void Visit(UsingAliasNamespace uan)
		{
		}

		// Token: 0x06002377 RID: 9079 RVA: 0x0000AF70 File Offset: 0x00009170
		public virtual void Visit(UsingExternAlias uea)
		{
		}

		// Token: 0x06002378 RID: 9080 RVA: 0x000AD180 File Offset: 0x000AB380
		public virtual void Visit(NamespaceContainer ns)
		{
			this.VisitTypeContainer(ns);
		}

		// Token: 0x06002379 RID: 9081 RVA: 0x000AD180 File Offset: 0x000AB380
		public virtual void Visit(CompilationSourceFile csf)
		{
			this.VisitTypeContainer(csf);
		}

		// Token: 0x0600237A RID: 9082 RVA: 0x000AD189 File Offset: 0x000AB389
		public virtual void Visit(Class c)
		{
			this.VisitTypeContainer(c);
		}

		// Token: 0x0600237B RID: 9083 RVA: 0x000AD189 File Offset: 0x000AB389
		public virtual void Visit(Struct s)
		{
			this.VisitTypeContainer(s);
		}

		// Token: 0x0600237C RID: 9084 RVA: 0x000AD189 File Offset: 0x000AB389
		public virtual void Visit(Interface i)
		{
			this.VisitTypeContainer(i);
		}

		// Token: 0x0600237D RID: 9085 RVA: 0x0000AF70 File Offset: 0x00009170
		public virtual void Visit(Delegate d)
		{
		}

		// Token: 0x0600237E RID: 9086 RVA: 0x000AD189 File Offset: 0x000AB389
		public virtual void Visit(Enum e)
		{
			this.VisitTypeContainer(e);
		}

		// Token: 0x0600237F RID: 9087 RVA: 0x0000AF70 File Offset: 0x00009170
		public virtual void Visit(FixedField f)
		{
		}

		// Token: 0x06002380 RID: 9088 RVA: 0x0000AF70 File Offset: 0x00009170
		public virtual void Visit(Const c)
		{
		}

		// Token: 0x06002381 RID: 9089 RVA: 0x0000AF70 File Offset: 0x00009170
		public virtual void Visit(Field f)
		{
		}

		// Token: 0x06002382 RID: 9090 RVA: 0x0000AF70 File Offset: 0x00009170
		public virtual void Visit(Operator o)
		{
		}

		// Token: 0x06002383 RID: 9091 RVA: 0x0000AF70 File Offset: 0x00009170
		public virtual void Visit(Indexer i)
		{
		}

		// Token: 0x06002384 RID: 9092 RVA: 0x0000AF70 File Offset: 0x00009170
		public virtual void Visit(Method m)
		{
		}

		// Token: 0x06002385 RID: 9093 RVA: 0x0000AF70 File Offset: 0x00009170
		public virtual void Visit(Property p)
		{
		}

		// Token: 0x06002386 RID: 9094 RVA: 0x0000AF70 File Offset: 0x00009170
		public virtual void Visit(Constructor c)
		{
		}

		// Token: 0x06002387 RID: 9095 RVA: 0x0000AF70 File Offset: 0x00009170
		public virtual void Visit(Destructor d)
		{
		}

		// Token: 0x06002388 RID: 9096 RVA: 0x0000AF70 File Offset: 0x00009170
		public virtual void Visit(EventField e)
		{
		}

		// Token: 0x06002389 RID: 9097 RVA: 0x0000AF70 File Offset: 0x00009170
		public virtual void Visit(EventProperty ep)
		{
		}

		// Token: 0x0600238A RID: 9098 RVA: 0x0000AF70 File Offset: 0x00009170
		public virtual void Visit(EnumMember em)
		{
		}

		// Token: 0x0600238B RID: 9099 RVA: 0x000055E7 File Offset: 0x000037E7
		public virtual object Visit(Statement stmt)
		{
			return null;
		}

		// Token: 0x0600238C RID: 9100 RVA: 0x000055E7 File Offset: 0x000037E7
		public virtual object Visit(BlockVariable blockVariableDeclaration)
		{
			return null;
		}

		// Token: 0x0600238D RID: 9101 RVA: 0x000055E7 File Offset: 0x000037E7
		public virtual object Visit(BlockConstant blockConstantDeclaration)
		{
			return null;
		}

		// Token: 0x0600238E RID: 9102 RVA: 0x000055E7 File Offset: 0x000037E7
		public virtual object Visit(EmptyStatement emptyStatement)
		{
			return null;
		}

		// Token: 0x0600238F RID: 9103 RVA: 0x000055E7 File Offset: 0x000037E7
		public virtual object Visit(EmptyExpressionStatement emptyExpressionStatement)
		{
			return null;
		}

		// Token: 0x06002390 RID: 9104 RVA: 0x000055E7 File Offset: 0x000037E7
		public virtual object Visit(EmptyExpression emptyExpression)
		{
			return null;
		}

		// Token: 0x06002391 RID: 9105 RVA: 0x000055E7 File Offset: 0x000037E7
		public virtual object Visit(ErrorExpression errorExpression)
		{
			return null;
		}

		// Token: 0x06002392 RID: 9106 RVA: 0x000055E7 File Offset: 0x000037E7
		public virtual object Visit(If ifStatement)
		{
			return null;
		}

		// Token: 0x06002393 RID: 9107 RVA: 0x000055E7 File Offset: 0x000037E7
		public virtual object Visit(Do doStatement)
		{
			return null;
		}

		// Token: 0x06002394 RID: 9108 RVA: 0x000055E7 File Offset: 0x000037E7
		public virtual object Visit(While whileStatement)
		{
			return null;
		}

		// Token: 0x06002395 RID: 9109 RVA: 0x000055E7 File Offset: 0x000037E7
		public virtual object Visit(For forStatement)
		{
			return null;
		}

		// Token: 0x06002396 RID: 9110 RVA: 0x000055E7 File Offset: 0x000037E7
		public virtual object Visit(StatementExpression statementExpression)
		{
			return null;
		}

		// Token: 0x06002397 RID: 9111 RVA: 0x000055E7 File Offset: 0x000037E7
		public virtual object Visit(StatementErrorExpression errorStatement)
		{
			return null;
		}

		// Token: 0x06002398 RID: 9112 RVA: 0x000055E7 File Offset: 0x000037E7
		public virtual object Visit(Return returnStatement)
		{
			return null;
		}

		// Token: 0x06002399 RID: 9113 RVA: 0x000055E7 File Offset: 0x000037E7
		public virtual object Visit(Goto gotoStatement)
		{
			return null;
		}

		// Token: 0x0600239A RID: 9114 RVA: 0x000055E7 File Offset: 0x000037E7
		public virtual object Visit(LabeledStatement labeledStatement)
		{
			return null;
		}

		// Token: 0x0600239B RID: 9115 RVA: 0x000055E7 File Offset: 0x000037E7
		public virtual object Visit(SwitchLabel switchLabel)
		{
			return null;
		}

		// Token: 0x0600239C RID: 9116 RVA: 0x000055E7 File Offset: 0x000037E7
		public virtual object Visit(GotoDefault gotoDefault)
		{
			return null;
		}

		// Token: 0x0600239D RID: 9117 RVA: 0x000055E7 File Offset: 0x000037E7
		public virtual object Visit(GotoCase gotoCase)
		{
			return null;
		}

		// Token: 0x0600239E RID: 9118 RVA: 0x000055E7 File Offset: 0x000037E7
		public virtual object Visit(Throw throwStatement)
		{
			return null;
		}

		// Token: 0x0600239F RID: 9119 RVA: 0x000055E7 File Offset: 0x000037E7
		public virtual object Visit(Break breakStatement)
		{
			return null;
		}

		// Token: 0x060023A0 RID: 9120 RVA: 0x000055E7 File Offset: 0x000037E7
		public virtual object Visit(Continue continueStatement)
		{
			return null;
		}

		// Token: 0x060023A1 RID: 9121 RVA: 0x000055E7 File Offset: 0x000037E7
		public virtual object Visit(Block blockStatement)
		{
			return null;
		}

		// Token: 0x060023A2 RID: 9122 RVA: 0x000055E7 File Offset: 0x000037E7
		public virtual object Visit(Switch switchStatement)
		{
			return null;
		}

		// Token: 0x060023A3 RID: 9123 RVA: 0x000055E7 File Offset: 0x000037E7
		public virtual object Visit(StatementList statementList)
		{
			return null;
		}

		// Token: 0x060023A4 RID: 9124 RVA: 0x000055E7 File Offset: 0x000037E7
		public virtual object Visit(Lock lockStatement)
		{
			return null;
		}

		// Token: 0x060023A5 RID: 9125 RVA: 0x000055E7 File Offset: 0x000037E7
		public virtual object Visit(Unchecked uncheckedStatement)
		{
			return null;
		}

		// Token: 0x060023A6 RID: 9126 RVA: 0x000055E7 File Offset: 0x000037E7
		public virtual object Visit(Checked checkedStatement)
		{
			return null;
		}

		// Token: 0x060023A7 RID: 9127 RVA: 0x000055E7 File Offset: 0x000037E7
		public virtual object Visit(Unsafe unsafeStatement)
		{
			return null;
		}

		// Token: 0x060023A8 RID: 9128 RVA: 0x000055E7 File Offset: 0x000037E7
		public virtual object Visit(Fixed fixedStatement)
		{
			return null;
		}

		// Token: 0x060023A9 RID: 9129 RVA: 0x000055E7 File Offset: 0x000037E7
		public virtual object Visit(TryFinally tryFinallyStatement)
		{
			return null;
		}

		// Token: 0x060023AA RID: 9130 RVA: 0x000055E7 File Offset: 0x000037E7
		public virtual object Visit(TryCatch tryCatchStatement)
		{
			return null;
		}

		// Token: 0x060023AB RID: 9131 RVA: 0x000055E7 File Offset: 0x000037E7
		public virtual object Visit(Using usingStatement)
		{
			return null;
		}

		// Token: 0x060023AC RID: 9132 RVA: 0x000055E7 File Offset: 0x000037E7
		public virtual object Visit(Foreach foreachStatement)
		{
			return null;
		}

		// Token: 0x060023AD RID: 9133 RVA: 0x000055E7 File Offset: 0x000037E7
		public virtual object Visit(Yield yieldStatement)
		{
			return null;
		}

		// Token: 0x060023AE RID: 9134 RVA: 0x000055E7 File Offset: 0x000037E7
		public virtual object Visit(YieldBreak yieldBreakStatement)
		{
			return null;
		}

		// Token: 0x060023AF RID: 9135 RVA: 0x000055E7 File Offset: 0x000037E7
		public virtual object Visit(Expression expression)
		{
			return null;
		}

		// Token: 0x060023B0 RID: 9136 RVA: 0x000055E7 File Offset: 0x000037E7
		public virtual object Visit(MemberAccess memberAccess)
		{
			return null;
		}

		// Token: 0x060023B1 RID: 9137 RVA: 0x000055E7 File Offset: 0x000037E7
		public virtual object Visit(QualifiedAliasMember qualifiedAliasMember)
		{
			return null;
		}

		// Token: 0x060023B2 RID: 9138 RVA: 0x000055E7 File Offset: 0x000037E7
		public virtual object Visit(LocalVariableReference localVariableReference)
		{
			return null;
		}

		// Token: 0x060023B3 RID: 9139 RVA: 0x000055E7 File Offset: 0x000037E7
		public virtual object Visit(Constant constant)
		{
			return null;
		}

		// Token: 0x060023B4 RID: 9140 RVA: 0x000055E7 File Offset: 0x000037E7
		public virtual object Visit(BooleanExpression booleanExpression)
		{
			return null;
		}

		// Token: 0x060023B5 RID: 9141 RVA: 0x000055E7 File Offset: 0x000037E7
		public virtual object Visit(SimpleName simpleName)
		{
			return null;
		}

		// Token: 0x060023B6 RID: 9142 RVA: 0x000055E7 File Offset: 0x000037E7
		public virtual object Visit(ParenthesizedExpression parenthesizedExpression)
		{
			return null;
		}

		// Token: 0x060023B7 RID: 9143 RVA: 0x000055E7 File Offset: 0x000037E7
		public virtual object Visit(Unary unaryExpression)
		{
			return null;
		}

		// Token: 0x060023B8 RID: 9144 RVA: 0x000055E7 File Offset: 0x000037E7
		public virtual object Visit(UnaryMutator unaryMutatorExpression)
		{
			return null;
		}

		// Token: 0x060023B9 RID: 9145 RVA: 0x000055E7 File Offset: 0x000037E7
		public virtual object Visit(Indirection indirectionExpression)
		{
			return null;
		}

		// Token: 0x060023BA RID: 9146 RVA: 0x000055E7 File Offset: 0x000037E7
		public virtual object Visit(Is isExpression)
		{
			return null;
		}

		// Token: 0x060023BB RID: 9147 RVA: 0x000055E7 File Offset: 0x000037E7
		public virtual object Visit(As asExpression)
		{
			return null;
		}

		// Token: 0x060023BC RID: 9148 RVA: 0x000055E7 File Offset: 0x000037E7
		public virtual object Visit(Cast castExpression)
		{
			return null;
		}

		// Token: 0x060023BD RID: 9149 RVA: 0x000055E7 File Offset: 0x000037E7
		public virtual object Visit(ComposedCast composedCast)
		{
			return null;
		}

		// Token: 0x060023BE RID: 9150 RVA: 0x000055E7 File Offset: 0x000037E7
		public virtual object Visit(DefaultValueExpression defaultValueExpression)
		{
			return null;
		}

		// Token: 0x060023BF RID: 9151 RVA: 0x000055E7 File Offset: 0x000037E7
		public virtual object Visit(DefaultParameterValueExpression defaultParameterValueExpression)
		{
			return null;
		}

		// Token: 0x060023C0 RID: 9152 RVA: 0x000055E7 File Offset: 0x000037E7
		public virtual object Visit(Binary binaryExpression)
		{
			return null;
		}

		// Token: 0x060023C1 RID: 9153 RVA: 0x000055E7 File Offset: 0x000037E7
		public virtual object Visit(NullCoalescingOperator nullCoalescingOperator)
		{
			return null;
		}

		// Token: 0x060023C2 RID: 9154 RVA: 0x000055E7 File Offset: 0x000037E7
		public virtual object Visit(Conditional conditionalExpression)
		{
			return null;
		}

		// Token: 0x060023C3 RID: 9155 RVA: 0x000055E7 File Offset: 0x000037E7
		public virtual object Visit(Invocation invocationExpression)
		{
			return null;
		}

		// Token: 0x060023C4 RID: 9156 RVA: 0x000055E7 File Offset: 0x000037E7
		public virtual object Visit(New newExpression)
		{
			return null;
		}

		// Token: 0x060023C5 RID: 9157 RVA: 0x000055E7 File Offset: 0x000037E7
		public virtual object Visit(NewAnonymousType newAnonymousType)
		{
			return null;
		}

		// Token: 0x060023C6 RID: 9158 RVA: 0x000055E7 File Offset: 0x000037E7
		public virtual object Visit(NewInitialize newInitializeExpression)
		{
			return null;
		}

		// Token: 0x060023C7 RID: 9159 RVA: 0x000055E7 File Offset: 0x000037E7
		public virtual object Visit(ArrayCreation arrayCreationExpression)
		{
			return null;
		}

		// Token: 0x060023C8 RID: 9160 RVA: 0x000055E7 File Offset: 0x000037E7
		public virtual object Visit(This thisExpression)
		{
			return null;
		}

		// Token: 0x060023C9 RID: 9161 RVA: 0x000055E7 File Offset: 0x000037E7
		public virtual object Visit(ArglistAccess argListAccessExpression)
		{
			return null;
		}

		// Token: 0x060023CA RID: 9162 RVA: 0x000055E7 File Offset: 0x000037E7
		public virtual object Visit(Arglist argListExpression)
		{
			return null;
		}

		// Token: 0x060023CB RID: 9163 RVA: 0x000055E7 File Offset: 0x000037E7
		public virtual object Visit(TypeOf typeOfExpression)
		{
			return null;
		}

		// Token: 0x060023CC RID: 9164 RVA: 0x000055E7 File Offset: 0x000037E7
		public virtual object Visit(SizeOf sizeOfExpression)
		{
			return null;
		}

		// Token: 0x060023CD RID: 9165 RVA: 0x000055E7 File Offset: 0x000037E7
		public virtual object Visit(CheckedExpr checkedExpression)
		{
			return null;
		}

		// Token: 0x060023CE RID: 9166 RVA: 0x000055E7 File Offset: 0x000037E7
		public virtual object Visit(UnCheckedExpr uncheckedExpression)
		{
			return null;
		}

		// Token: 0x060023CF RID: 9167 RVA: 0x000055E7 File Offset: 0x000037E7
		public virtual object Visit(ElementAccess elementAccessExpression)
		{
			return null;
		}

		// Token: 0x060023D0 RID: 9168 RVA: 0x000055E7 File Offset: 0x000037E7
		public virtual object Visit(BaseThis baseAccessExpression)
		{
			return null;
		}

		// Token: 0x060023D1 RID: 9169 RVA: 0x000055E7 File Offset: 0x000037E7
		public virtual object Visit(StackAlloc stackAllocExpression)
		{
			return null;
		}

		// Token: 0x060023D2 RID: 9170 RVA: 0x000055E7 File Offset: 0x000037E7
		public virtual object Visit(SimpleAssign simpleAssign)
		{
			return null;
		}

		// Token: 0x060023D3 RID: 9171 RVA: 0x000055E7 File Offset: 0x000037E7
		public virtual object Visit(CompoundAssign compoundAssign)
		{
			return null;
		}

		// Token: 0x060023D4 RID: 9172 RVA: 0x000055E7 File Offset: 0x000037E7
		public virtual object Visit(TypeExpression typeExpression)
		{
			return null;
		}

		// Token: 0x060023D5 RID: 9173 RVA: 0x000055E7 File Offset: 0x000037E7
		public virtual object Visit(AnonymousMethodExpression anonymousMethodExpression)
		{
			return null;
		}

		// Token: 0x060023D6 RID: 9174 RVA: 0x000055E7 File Offset: 0x000037E7
		public virtual object Visit(LambdaExpression lambdaExpression)
		{
			return null;
		}

		// Token: 0x060023D7 RID: 9175 RVA: 0x000055E7 File Offset: 0x000037E7
		public virtual object Visit(ConstInitializer constInitializer)
		{
			return null;
		}

		// Token: 0x060023D8 RID: 9176 RVA: 0x000055E7 File Offset: 0x000037E7
		public virtual object Visit(ArrayInitializer arrayInitializer)
		{
			return null;
		}

		// Token: 0x060023D9 RID: 9177 RVA: 0x000055E7 File Offset: 0x000037E7
		public virtual object Visit(QueryExpression queryExpression)
		{
			return null;
		}

		// Token: 0x060023DA RID: 9178 RVA: 0x000055E7 File Offset: 0x000037E7
		public virtual object Visit(QueryStartClause queryExpression)
		{
			return null;
		}

		// Token: 0x060023DB RID: 9179 RVA: 0x000055E7 File Offset: 0x000037E7
		public virtual object Visit(SelectMany selectMany)
		{
			return null;
		}

		// Token: 0x060023DC RID: 9180 RVA: 0x000055E7 File Offset: 0x000037E7
		public virtual object Visit(Select select)
		{
			return null;
		}

		// Token: 0x060023DD RID: 9181 RVA: 0x000055E7 File Offset: 0x000037E7
		public virtual object Visit(GroupBy groupBy)
		{
			return null;
		}

		// Token: 0x060023DE RID: 9182 RVA: 0x000055E7 File Offset: 0x000037E7
		public virtual object Visit(Let let)
		{
			return null;
		}

		// Token: 0x060023DF RID: 9183 RVA: 0x000055E7 File Offset: 0x000037E7
		public virtual object Visit(Where where)
		{
			return null;
		}

		// Token: 0x060023E0 RID: 9184 RVA: 0x000055E7 File Offset: 0x000037E7
		public virtual object Visit(Join join)
		{
			return null;
		}

		// Token: 0x060023E1 RID: 9185 RVA: 0x000055E7 File Offset: 0x000037E7
		public virtual object Visit(GroupJoin groupJoin)
		{
			return null;
		}

		// Token: 0x060023E2 RID: 9186 RVA: 0x000055E7 File Offset: 0x000037E7
		public virtual object Visit(OrderByAscending orderByAscending)
		{
			return null;
		}

		// Token: 0x060023E3 RID: 9187 RVA: 0x000055E7 File Offset: 0x000037E7
		public virtual object Visit(OrderByDescending orderByDescending)
		{
			return null;
		}

		// Token: 0x060023E4 RID: 9188 RVA: 0x000055E7 File Offset: 0x000037E7
		public virtual object Visit(ThenByAscending thenByAscending)
		{
			return null;
		}

		// Token: 0x060023E5 RID: 9189 RVA: 0x000055E7 File Offset: 0x000037E7
		public virtual object Visit(ThenByDescending thenByDescending)
		{
			return null;
		}

		// Token: 0x060023E6 RID: 9190 RVA: 0x000055E7 File Offset: 0x000037E7
		public virtual object Visit(RefValueExpr refValueExpr)
		{
			return null;
		}

		// Token: 0x060023E7 RID: 9191 RVA: 0x000055E7 File Offset: 0x000037E7
		public virtual object Visit(RefTypeExpr refTypeExpr)
		{
			return null;
		}

		// Token: 0x060023E8 RID: 9192 RVA: 0x000055E7 File Offset: 0x000037E7
		public virtual object Visit(MakeRefExpr makeRefExpr)
		{
			return null;
		}

		// Token: 0x060023E9 RID: 9193 RVA: 0x000055E7 File Offset: 0x000037E7
		public virtual object Visit(Await awaitExpr)
		{
			return null;
		}

		// Token: 0x060023EA RID: 9194 RVA: 0x00002CCC File Offset: 0x00000ECC
		protected StructuralVisitor()
		{
		}
	}
}
