using System;
using System.Collections.Generic;

namespace Mono.CSharp
{
	// Token: 0x0200020F RID: 527
	public class CollectionOrObjectInitializers : ExpressionStatement
	{
		// Token: 0x06001AFF RID: 6911 RVA: 0x00083198 File Offset: 0x00081398
		public CollectionOrObjectInitializers(Location loc) : this(new Expression[0], loc)
		{
		}

		// Token: 0x06001B00 RID: 6912 RVA: 0x000831A7 File Offset: 0x000813A7
		public CollectionOrObjectInitializers(IList<Expression> initializers, Location loc)
		{
			this.initializers = initializers;
			this.loc = loc;
		}

		// Token: 0x17000620 RID: 1568
		// (get) Token: 0x06001B01 RID: 6913 RVA: 0x000831BD File Offset: 0x000813BD
		public IList<Expression> Initializers
		{
			get
			{
				return this.initializers;
			}
		}

		// Token: 0x17000621 RID: 1569
		// (get) Token: 0x06001B02 RID: 6914 RVA: 0x000831C5 File Offset: 0x000813C5
		public bool IsEmpty
		{
			get
			{
				return this.initializers.Count == 0;
			}
		}

		// Token: 0x17000622 RID: 1570
		// (get) Token: 0x06001B03 RID: 6915 RVA: 0x000831D5 File Offset: 0x000813D5
		public bool IsCollectionInitializer
		{
			get
			{
				return this.is_collection_initialization;
			}
		}

		// Token: 0x06001B04 RID: 6916 RVA: 0x000831E0 File Offset: 0x000813E0
		protected override void CloneTo(CloneContext clonectx, Expression target)
		{
			CollectionOrObjectInitializers collectionOrObjectInitializers = (CollectionOrObjectInitializers)target;
			collectionOrObjectInitializers.initializers = new List<Expression>(this.initializers.Count);
			foreach (Expression expression in this.initializers)
			{
				collectionOrObjectInitializers.initializers.Add(expression.Clone(clonectx));
			}
		}

		// Token: 0x06001B05 RID: 6917 RVA: 0x00083258 File Offset: 0x00081458
		public override bool ContainsEmitWithAwait()
		{
			using (IEnumerator<Expression> enumerator = this.initializers.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.ContainsEmitWithAwait())
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06001B06 RID: 6918 RVA: 0x000832AC File Offset: 0x000814AC
		public override Expression CreateExpressionTree(ResolveContext ec)
		{
			return this.CreateExpressionTree(ec, false);
		}

		// Token: 0x06001B07 RID: 6919 RVA: 0x000832B8 File Offset: 0x000814B8
		public Expression CreateExpressionTree(ResolveContext ec, bool inferType)
		{
			ArrayInitializer arrayInitializer = new ArrayInitializer(this.initializers.Count, this.loc);
			foreach (Expression expression in this.initializers)
			{
				Expression expression2 = expression.CreateExpressionTree(ec);
				if (expression2 != null)
				{
					arrayInitializer.Add(expression2);
				}
			}
			if (inferType)
			{
				return new ImplicitlyTypedArrayCreation(arrayInitializer, this.loc);
			}
			return new ArrayCreation(new TypeExpression(ec.Module.PredefinedTypes.MemberBinding.Resolve(), this.loc), arrayInitializer, this.loc);
		}

		// Token: 0x06001B08 RID: 6920 RVA: 0x00083364 File Offset: 0x00081564
		protected override Expression DoResolve(ResolveContext ec)
		{
			List<string> list = null;
			int i = 0;
			while (i < this.initializers.Count)
			{
				Expression expression = this.initializers[i];
				ElementInitializer elementInitializer = expression as ElementInitializer;
				if (i == 0)
				{
					if (elementInitializer != null)
					{
						list = new List<string>(this.initializers.Count);
						if (!elementInitializer.IsDictionaryInitializer)
						{
							list.Add(elementInitializer.Name);
							goto IL_188;
						}
						goto IL_188;
					}
					else
					{
						if (expression is CompletingExpression)
						{
							expression.Resolve(ec);
							throw new InternalErrorException("This line should never be reached");
						}
						TypeSpec type = ec.CurrentInitializerVariable.Type;
						if (!type.ImplementsInterface(ec.BuiltinTypes.IEnumerable, false) && type.BuiltinType != BuiltinTypeSpec.Type.Dynamic)
						{
							ec.Report.Error(1922, this.loc, "A field or property `{0}' cannot be initialized with a collection object initializer because type `{1}' does not implement `{2}' interface", new string[]
							{
								ec.CurrentInitializerVariable.GetSignatureForError(),
								ec.CurrentInitializerVariable.Type.GetSignatureForError(),
								ec.BuiltinTypes.IEnumerable.GetSignatureForError()
							});
							return null;
						}
						this.is_collection_initialization = true;
						goto IL_188;
					}
				}
				else if (this.is_collection_initialization != (elementInitializer == null))
				{
					ec.Report.Error(747, expression.Location, "Inconsistent `{0}' member declaration", this.is_collection_initialization ? "collection initializer" : "object initializer");
				}
				else
				{
					if (this.is_collection_initialization || elementInitializer.IsDictionaryInitializer)
					{
						goto IL_188;
					}
					if (list.Contains(elementInitializer.Name))
					{
						ec.Report.Error(1912, elementInitializer.Location, "An object initializer includes more than one member `{0}' initialization", elementInitializer.Name);
						goto IL_188;
					}
					list.Add(elementInitializer.Name);
					goto IL_188;
				}
				IL_1BA:
				i++;
				continue;
				IL_188:
				Expression expression2 = expression.Resolve(ec);
				if (expression2 == EmptyExpressionStatement.Instance)
				{
					this.initializers.RemoveAt(i--);
					goto IL_1BA;
				}
				this.initializers[i] = expression2;
				goto IL_1BA;
			}
			this.type = ec.CurrentInitializerVariable.Type;
			if (this.is_collection_initialization && TypeManager.HasElementType(this.type))
			{
				ec.Report.Error(1925, this.loc, "Cannot initialize object of type `{0}' with a collection initializer", this.type.GetSignatureForError());
			}
			this.eclass = ExprClass.Variable;
			return this;
		}

		// Token: 0x06001B09 RID: 6921 RVA: 0x0006C1F3 File Offset: 0x0006A3F3
		public override void Emit(EmitContext ec)
		{
			this.EmitStatement(ec);
		}

		// Token: 0x06001B0A RID: 6922 RVA: 0x00083594 File Offset: 0x00081794
		public override void EmitStatement(EmitContext ec)
		{
			foreach (Expression expression in this.initializers)
			{
				ExpressionStatement expressionStatement = (ExpressionStatement)expression;
				ec.Mark(expressionStatement.Location);
				expressionStatement.EmitStatement(ec);
			}
		}

		// Token: 0x06001B0B RID: 6923 RVA: 0x000835F4 File Offset: 0x000817F4
		public override void FlowAnalysis(FlowAnalysisContext fc)
		{
			foreach (Expression expression in this.initializers)
			{
				if (expression != null)
				{
					expression.FlowAnalysis(fc);
				}
			}
		}

		// Token: 0x04000A0F RID: 2575
		private IList<Expression> initializers;

		// Token: 0x04000A10 RID: 2576
		private bool is_collection_initialization;
	}
}
