using System;
using System.Reflection.Emit;

namespace Mono.CSharp
{
	// Token: 0x02000195 RID: 405
	public class ImplicitDelegateCreation : DelegateCreation
	{
		// Token: 0x060015DD RID: 5597 RVA: 0x00068BE3 File Offset: 0x00066DE3
		public ImplicitDelegateCreation(TypeSpec delegateType, MethodGroupExpr mg, Location loc)
		{
			this.type = delegateType;
			this.method_group = mg;
			this.loc = loc;
		}

		// Token: 0x060015DE RID: 5598 RVA: 0x00068C00 File Offset: 0x00066E00
		public static bool ContainsMethodTypeParameter(TypeSpec type)
		{
			TypeParameterSpec typeParameterSpec = type as TypeParameterSpec;
			if (typeParameterSpec != null)
			{
				return typeParameterSpec.IsMethodOwned;
			}
			ElementTypeSpec elementTypeSpec = type as ElementTypeSpec;
			if (elementTypeSpec != null)
			{
				return ImplicitDelegateCreation.ContainsMethodTypeParameter(elementTypeSpec.Element);
			}
			TypeSpec[] typeArguments = type.TypeArguments;
			for (int i = 0; i < typeArguments.Length; i++)
			{
				if (ImplicitDelegateCreation.ContainsMethodTypeParameter(typeArguments[i]))
				{
					return true;
				}
			}
			return type.IsNested && ImplicitDelegateCreation.ContainsMethodTypeParameter(type.DeclaringType);
		}

		// Token: 0x060015DF RID: 5599 RVA: 0x00068C6C File Offset: 0x00066E6C
		private bool HasMvar()
		{
			if (ImplicitDelegateCreation.ContainsMethodTypeParameter(this.type))
			{
				return false;
			}
			MethodSpec bestCandidate = this.method_group.BestCandidate;
			if (ImplicitDelegateCreation.ContainsMethodTypeParameter(bestCandidate.DeclaringType))
			{
				return false;
			}
			if (bestCandidate.TypeArguments != null)
			{
				TypeSpec[] typeArguments = bestCandidate.TypeArguments;
				for (int i = 0; i < typeArguments.Length; i++)
				{
					if (ImplicitDelegateCreation.ContainsMethodTypeParameter(typeArguments[i]))
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x060015E0 RID: 5600 RVA: 0x00068CD0 File Offset: 0x00066ED0
		protected override Expression DoResolve(ResolveContext ec)
		{
			Expression expression = base.DoResolve(ec);
			if (expression == null)
			{
				return ErrorExpression.Instance;
			}
			if (ec.IsInProbingMode)
			{
				return expression;
			}
			if (this.method_group.InstanceExpression != null)
			{
				return expression;
			}
			if (!this.HasMvar())
			{
				return expression;
			}
			TypeDefinition partialContainer = ec.CurrentMemberDefinition.Parent.PartialContainer;
			TypeDefinition typeDefinition = partialContainer;
			int methodGroupsCounter = typeDefinition.MethodGroupsCounter;
			typeDefinition.MethodGroupsCounter = methodGroupsCounter + 1;
			int id = methodGroupsCounter;
			this.mg_cache = new Field(partialContainer, new TypeExpression(this.type, this.loc), Modifiers.PRIVATE | Modifiers.STATIC | Modifiers.COMPILER_GENERATED, new MemberName(CompilerGeneratedContainer.MakeName(null, "f", "mg$cache", id), this.loc), null);
			this.mg_cache.Define();
			partialContainer.AddField(this.mg_cache);
			return expression;
		}

		// Token: 0x060015E1 RID: 5601 RVA: 0x00068D90 File Offset: 0x00066F90
		public override void Emit(EmitContext ec)
		{
			Label label = ec.DefineLabel();
			if (this.mg_cache != null)
			{
				ec.Emit(OpCodes.Ldsfld, this.mg_cache.Spec);
				ec.Emit(OpCodes.Brtrue_S, label);
			}
			base.Emit(ec);
			if (this.mg_cache != null)
			{
				ec.Emit(OpCodes.Stsfld, this.mg_cache.Spec);
				ec.MarkLabel(label);
				ec.Emit(OpCodes.Ldsfld, this.mg_cache.Spec);
			}
		}

		// Token: 0x04000923 RID: 2339
		private Field mg_cache;
	}
}
