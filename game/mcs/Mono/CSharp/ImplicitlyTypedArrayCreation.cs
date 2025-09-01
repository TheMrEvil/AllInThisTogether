using System;

namespace Mono.CSharp
{
	// Token: 0x020001EB RID: 491
	internal class ImplicitlyTypedArrayCreation : ArrayCreation
	{
		// Token: 0x060019C2 RID: 6594 RVA: 0x0007F4D5 File Offset: 0x0007D6D5
		public ImplicitlyTypedArrayCreation(ComposedTypeSpecifier rank, ArrayInitializer initializers, Location loc) : base(null, rank, initializers, loc)
		{
		}

		// Token: 0x060019C3 RID: 6595 RVA: 0x0007F4E1 File Offset: 0x0007D6E1
		public ImplicitlyTypedArrayCreation(ArrayInitializer initializers, Location loc) : base(null, initializers, loc)
		{
		}

		// Token: 0x060019C4 RID: 6596 RVA: 0x0007F4EC File Offset: 0x0007D6EC
		protected override Expression DoResolve(ResolveContext ec)
		{
			if (this.type != null)
			{
				return this;
			}
			this.dimensions = this.rank.Dimension;
			this.best_type_inference = new TypeInferenceContext();
			if (!base.ResolveInitializers(ec))
			{
				return null;
			}
			this.best_type_inference.FixAllTypes(ec);
			this.array_element_type = this.best_type_inference.InferredTypeArguments[0];
			this.best_type_inference = null;
			if (this.array_element_type == null || this.array_element_type == InternalType.NullLiteral || this.array_element_type == InternalType.MethodGroup || this.array_element_type == InternalType.AnonymousMethod || this.arguments.Count != this.rank.Dimension)
			{
				ec.Report.Error(826, this.loc, "The type of an implicitly typed array cannot be inferred from the initializer. Try specifying array type explicitly");
				return null;
			}
			this.UnifyInitializerElement(ec);
			this.type = ArrayContainer.MakeType(ec.Module, this.array_element_type, this.dimensions);
			this.eclass = ExprClass.Value;
			return this;
		}

		// Token: 0x060019C5 RID: 6597 RVA: 0x0007F5E4 File Offset: 0x0007D7E4
		private void UnifyInitializerElement(ResolveContext ec)
		{
			for (int i = 0; i < this.array_data.Count; i++)
			{
				Expression expression = this.array_data[i];
				if (expression != null)
				{
					this.array_data[i] = Convert.ImplicitConversion(ec, expression, this.array_element_type, Location.Null);
				}
			}
		}

		// Token: 0x060019C6 RID: 6598 RVA: 0x0007F635 File Offset: 0x0007D835
		protected override Expression ResolveArrayElement(ResolveContext ec, Expression element)
		{
			element = element.Resolve(ec);
			if (element != null)
			{
				this.best_type_inference.AddCommonTypeBound(element.Type);
			}
			return element;
		}

		// Token: 0x040009DD RID: 2525
		private TypeInferenceContext best_type_inference;
	}
}
