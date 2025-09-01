using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;

namespace Mono.CSharp
{
	// Token: 0x020001BB RID: 443
	public abstract class MemberExpr : Expression, OverloadResolver.IInstanceQualifier
	{
		// Token: 0x1700056C RID: 1388
		// (get) Token: 0x0600171B RID: 5915
		public abstract string Name { get; }

		// Token: 0x1700056D RID: 1389
		// (get) Token: 0x0600171C RID: 5916 RVA: 0x0006E58B File Offset: 0x0006C78B
		public bool IsBase
		{
			get
			{
				return this.InstanceExpression is BaseThis;
			}
		}

		// Token: 0x1700056E RID: 1390
		// (get) Token: 0x0600171D RID: 5917
		public abstract bool IsInstance { get; }

		// Token: 0x1700056F RID: 1391
		// (get) Token: 0x0600171E RID: 5918
		public abstract bool IsStatic { get; }

		// Token: 0x17000570 RID: 1392
		// (get) Token: 0x0600171F RID: 5919
		public abstract string KindName { get; }

		// Token: 0x17000571 RID: 1393
		// (get) Token: 0x06001720 RID: 5920 RVA: 0x0006E59B File Offset: 0x0006C79B
		// (set) Token: 0x06001721 RID: 5921 RVA: 0x0006E5A3 File Offset: 0x0006C7A3
		public bool ConditionalAccess
		{
			[CompilerGenerated]
			get
			{
				return this.<ConditionalAccess>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<ConditionalAccess>k__BackingField = value;
			}
		}

		// Token: 0x17000572 RID: 1394
		// (get) Token: 0x06001722 RID: 5922
		protected abstract TypeSpec DeclaringType { get; }

		// Token: 0x17000573 RID: 1395
		// (get) Token: 0x06001723 RID: 5923 RVA: 0x0006E5AC File Offset: 0x0006C7AC
		TypeSpec OverloadResolver.IInstanceQualifier.InstanceType
		{
			get
			{
				return this.InstanceExpression.Type;
			}
		}

		// Token: 0x06001724 RID: 5924 RVA: 0x0006E5BC File Offset: 0x0006C7BC
		protected MethodSpec CandidateToBaseOverride(ResolveContext rc, MethodSpec method)
		{
			if (!this.IsBase)
			{
				return method;
			}
			if ((method.Modifiers & (Modifiers.ABSTRACT | Modifiers.VIRTUAL | Modifiers.OVERRIDE)) != (Modifiers)0)
			{
				TypeSpec[] array = null;
				if (method.DeclaringType != this.InstanceExpression.Type)
				{
					AParametersCollection aparametersCollection = method.Parameters;
					if (method.Arity > 0)
					{
						aparametersCollection = ((IParametersMember)method.MemberDefinition).Parameters;
						InflatedTypeSpec inflatedTypeSpec = method.DeclaringType as InflatedTypeSpec;
						if (inflatedTypeSpec != null)
						{
							aparametersCollection = aparametersCollection.Inflate(inflatedTypeSpec.CreateLocalInflator(rc));
						}
					}
					MemberFilter filter = new MemberFilter(method.Name, method.Arity, MemberKind.Method, aparametersCollection, null);
					MethodSpec methodSpec = MemberCache.FindMember(this.InstanceExpression.Type, filter, BindingRestriction.InstanceOnly | BindingRestriction.OverrideOnly) as MethodSpec;
					if (methodSpec != null && methodSpec.DeclaringType != method.DeclaringType)
					{
						if (methodSpec.IsGeneric)
						{
							array = method.TypeArguments;
						}
						method = methodSpec;
					}
				}
				if (rc.CurrentAnonymousMethod != null)
				{
					if (array == null && method.IsGeneric)
					{
						array = method.TypeArguments;
						method = method.GetGenericMethodDefinition();
					}
					if (method.Parameters.HasArglist)
					{
						throw new NotImplementedException("__arglist base call proxy");
					}
					method = rc.CurrentMemberDefinition.Parent.PartialContainer.CreateHoistedBaseCallProxy(rc, method);
					if (rc.CurrentType.IsStruct || rc.CurrentAnonymousMethod.Storey is AsyncTaskStorey)
					{
						this.InstanceExpression = new This(this.loc).Resolve(rc);
					}
				}
				if (array != null)
				{
					method = method.MakeGenericMethod(rc, array);
				}
			}
			if (method.IsAbstract)
			{
				rc.Report.SymbolRelatedToPreviousError(method);
				this.Error_CannotCallAbstractBase(rc, method.GetSignatureForError());
			}
			return method;
		}

		// Token: 0x06001725 RID: 5925 RVA: 0x0006E74C File Offset: 0x0006C94C
		protected void CheckProtectedMemberAccess(ResolveContext rc, MemberSpec member)
		{
			if (this.InstanceExpression == null)
			{
				return;
			}
			if ((member.Modifiers & Modifiers.PROTECTED) != (Modifiers)0 && !(this.InstanceExpression is This) && !MemberExpr.CheckProtectedMemberAccess<MemberSpec>(rc, member, this.InstanceExpression.Type))
			{
				MemberExpr.Error_ProtectedMemberAccess(rc, member, this.InstanceExpression.Type, this.loc);
			}
		}

		// Token: 0x06001726 RID: 5926 RVA: 0x0006E7A5 File Offset: 0x0006C9A5
		bool OverloadResolver.IInstanceQualifier.CheckProtectedMemberAccess(ResolveContext rc, MemberSpec member)
		{
			return this.InstanceExpression == null || this.InstanceExpression is This || MemberExpr.CheckProtectedMemberAccess<MemberSpec>(rc, member, this.InstanceExpression.Type);
		}

		// Token: 0x06001727 RID: 5927 RVA: 0x0006E7D4 File Offset: 0x0006C9D4
		public static bool CheckProtectedMemberAccess<T>(ResolveContext rc, T member, TypeSpec qualifier) where T : MemberSpec
		{
			TypeSpec currentType = rc.CurrentType;
			if (currentType == qualifier)
			{
				return true;
			}
			if ((member.Modifiers & Modifiers.INTERNAL) != (Modifiers)0 && member.DeclaringType.MemberDefinition.IsInternalAsPublic(currentType.MemberDefinition.DeclaringAssembly))
			{
				return true;
			}
			qualifier = qualifier.GetDefinition();
			return currentType == qualifier || MemberExpr.IsSameOrBaseQualifier(currentType, qualifier);
		}

		// Token: 0x06001728 RID: 5928 RVA: 0x0006E839 File Offset: 0x0006CA39
		public override bool ContainsEmitWithAwait()
		{
			return this.InstanceExpression != null && this.InstanceExpression.ContainsEmitWithAwait();
		}

		// Token: 0x06001729 RID: 5929 RVA: 0x0006E850 File Offset: 0x0006CA50
		public override bool HasConditionalAccess()
		{
			return this.ConditionalAccess || (this.InstanceExpression != null && this.InstanceExpression.HasConditionalAccess());
		}

		// Token: 0x0600172A RID: 5930 RVA: 0x0006E871 File Offset: 0x0006CA71
		private static bool IsSameOrBaseQualifier(TypeSpec type, TypeSpec qtype)
		{
			for (;;)
			{
				type = type.GetDefinition();
				if (type == qtype || TypeManager.IsFamilyAccessible(qtype, type))
				{
					break;
				}
				type = type.DeclaringType;
				if (type == null)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600172B RID: 5931 RVA: 0x0006E898 File Offset: 0x0006CA98
		protected void DoBestMemberChecks<T>(ResolveContext rc, T member) where T : MemberSpec, IInterfaceMemberSpec
		{
			if (this.InstanceExpression != null)
			{
				this.InstanceExpression = this.InstanceExpression.Resolve(rc);
				this.CheckProtectedMemberAccess(rc, member);
			}
			if (member.MemberType.IsPointer && !rc.IsUnsafe)
			{
				Expression.UnsafeError(rc, this.loc);
			}
			List<MissingTypeSpecReference> missingDependencies = member.GetMissingDependencies();
			if (missingDependencies != null)
			{
				ImportedTypeDefinition.Error_MissingDependency(rc, missingDependencies, this.loc);
			}
			if (!rc.IsObsolete)
			{
				ObsoleteAttribute attributeObsolete = member.GetAttributeObsolete();
				if (attributeObsolete != null)
				{
					AttributeTester.Report_ObsoleteMessage(attributeObsolete, member.GetSignatureForError(), this.loc, rc.Report);
				}
			}
			if (!(member is FieldSpec))
			{
				member.MemberDefinition.SetIsUsed();
			}
		}

		// Token: 0x0600172C RID: 5932 RVA: 0x0006E95F File Offset: 0x0006CB5F
		protected virtual void Error_CannotCallAbstractBase(ResolveContext rc, string name)
		{
			rc.Report.Error(205, this.loc, "Cannot call an abstract base member `{0}'", name);
		}

		// Token: 0x0600172D RID: 5933 RVA: 0x0006E980 File Offset: 0x0006CB80
		public static void Error_ProtectedMemberAccess(ResolveContext rc, MemberSpec member, TypeSpec qualifier, Location loc)
		{
			rc.Report.SymbolRelatedToPreviousError(member);
			rc.Report.Error(1540, loc, "Cannot access protected member `{0}' via a qualifier of type `{1}'. The qualifier must be of type `{2}' or derived from it", new string[]
			{
				member.GetSignatureForError(),
				qualifier.GetSignatureForError(),
				rc.CurrentType.GetSignatureForError()
			});
		}

		// Token: 0x0600172E RID: 5934 RVA: 0x0006E9D5 File Offset: 0x0006CBD5
		public override void FlowAnalysis(FlowAnalysisContext fc)
		{
			if (this.InstanceExpression != null)
			{
				this.InstanceExpression.FlowAnalysis(fc);
				if (this.ConditionalAccess)
				{
					fc.BranchConditionalAccessDefiniteAssignment();
				}
			}
		}

		// Token: 0x0600172F RID: 5935 RVA: 0x0006E9F9 File Offset: 0x0006CBF9
		protected void ResolveConditionalAccessReceiver(ResolveContext rc)
		{
			if (!rc.HasSet(ResolveContext.Options.ConditionalAccessReceiver) && this.HasConditionalAccess())
			{
				this.conditional_access_receiver = true;
				rc.Set(ResolveContext.Options.ConditionalAccessReceiver);
			}
		}

		// Token: 0x06001730 RID: 5936 RVA: 0x0006EA24 File Offset: 0x0006CC24
		public bool ResolveInstanceExpression(ResolveContext rc, Expression rhs)
		{
			if (!this.ResolveInstanceExpressionCore(rc, rhs))
			{
				return false;
			}
			if (rhs != null && TypeSpec.IsValueType(this.InstanceExpression.Type))
			{
				FieldExpr fieldExpr = this.InstanceExpression as FieldExpr;
				if (fieldExpr != null)
				{
					if (!fieldExpr.Spec.IsReadOnly || rc.HasAny(ResolveContext.Options.FieldInitializerScope | ResolveContext.Options.ConstructorScope))
					{
						return true;
					}
					if (fieldExpr.IsStatic)
					{
						rc.Report.Error(1650, this.loc, "Fields of static readonly field `{0}' cannot be assigned to (except in a static constructor or a variable initializer)", fieldExpr.GetSignatureForError());
					}
					else
					{
						rc.Report.Error(1648, this.loc, "Members of readonly field `{0}' cannot be modified (except in a constructor or a variable initializer)", fieldExpr.GetSignatureForError());
					}
					return true;
				}
				else
				{
					if (this.InstanceExpression is PropertyExpr || this.InstanceExpression is IndexerExpr || this.InstanceExpression is Invocation)
					{
						if (rc.CurrentInitializerVariable != null)
						{
							rc.Report.Error(1918, this.loc, "Members of value type `{0}' cannot be assigned using a property `{1}' object initializer", this.InstanceExpression.Type.GetSignatureForError(), this.InstanceExpression.GetSignatureForError());
						}
						else
						{
							rc.Report.Error(1612, this.loc, "Cannot modify a value type return value of `{0}'. Consider storing the value in a temporary variable", this.InstanceExpression.GetSignatureForError());
						}
						return true;
					}
					LocalVariableReference localVariableReference = this.InstanceExpression as LocalVariableReference;
					if (localVariableReference != null)
					{
						if (!localVariableReference.local_info.IsReadonly)
						{
							return true;
						}
						rc.Report.Error(1654, this.loc, "Cannot assign to members of `{0}' because it is a `{1}'", this.InstanceExpression.GetSignatureForError(), localVariableReference.local_info.GetReadOnlyContext());
					}
				}
			}
			return true;
		}

		// Token: 0x06001731 RID: 5937 RVA: 0x0006EBB0 File Offset: 0x0006CDB0
		private bool ResolveInstanceExpressionCore(ResolveContext rc, Expression rhs)
		{
			if (this.IsStatic)
			{
				if (this.InstanceExpression != null)
				{
					if (this.InstanceExpression is TypeExpr)
					{
						TypeSpec typeSpec = this.InstanceExpression.Type;
						do
						{
							ObsoleteAttribute attributeObsolete = typeSpec.GetAttributeObsolete();
							if (attributeObsolete != null && !rc.IsObsolete)
							{
								AttributeTester.Report_ObsoleteMessage(attributeObsolete, typeSpec.GetSignatureForError(), this.loc, rc.Report);
							}
							typeSpec = typeSpec.DeclaringType;
						}
						while (typeSpec != null);
					}
					else
					{
						RuntimeValueExpression runtimeValueExpression = this.InstanceExpression as RuntimeValueExpression;
						if (runtimeValueExpression == null || !runtimeValueExpression.IsSuggestionOnly)
						{
							rc.Report.Error(176, this.loc, "Static member `{0}' cannot be accessed with an instance reference, qualify it with a type name instead", this.GetSignatureForError());
						}
					}
					this.InstanceExpression = null;
				}
				return false;
			}
			if (this.InstanceExpression == null || this.InstanceExpression is TypeExpr)
			{
				if (this.InstanceExpression != null || !This.IsThisAvailable(rc, true))
				{
					if (rc.HasSet(ResolveContext.Options.FieldInitializerScope))
					{
						rc.Report.Error(236, this.loc, "A field initializer cannot reference the nonstatic field, method, or property `{0}'", this.GetSignatureForError());
					}
					else
					{
						FieldExpr fieldExpr = this as FieldExpr;
						if (fieldExpr != null && fieldExpr.Spec.MemberDefinition is PrimaryConstructorField)
						{
							if (rc.HasSet(ResolveContext.Options.BaseInitializer))
							{
								rc.Report.Error(9005, this.loc, "Constructor initializer cannot access primary constructor parameters");
							}
							else
							{
								rc.Report.Error(9006, this.loc, "An object reference is required to access primary constructor parameter `{0}'", fieldExpr.Name);
							}
						}
						else
						{
							rc.Report.Error(120, this.loc, "An object reference is required to access non-static member `{0}'", this.GetSignatureForError());
						}
					}
					this.InstanceExpression = new CompilerGeneratedThis(rc.CurrentType, this.loc).Resolve(rc);
					return false;
				}
				if (!TypeManager.IsFamilyAccessible(rc.CurrentType, this.DeclaringType))
				{
					rc.Report.Error(38, this.loc, "Cannot access a nonstatic member of outer type `{0}' via nested type `{1}'", this.DeclaringType.GetSignatureForError(), rc.CurrentType.GetSignatureForError());
				}
				this.InstanceExpression = new This(this.loc).Resolve(rc);
				return false;
			}
			else
			{
				MemberExpr memberExpr = this.InstanceExpression as MemberExpr;
				if (memberExpr != null)
				{
					memberExpr.ResolveInstanceExpressionCore(rc, rhs);
					FieldExpr fieldExpr2 = memberExpr as FieldExpr;
					if (fieldExpr2 != null && fieldExpr2.IsMarshalByRefAccess(rc))
					{
						rc.Report.SymbolRelatedToPreviousError(memberExpr.DeclaringType);
						rc.Report.Warning(1690, 1, this.loc, "Cannot call methods, properties, or indexers on `{0}' because it is a value type member of a marshal-by-reference class", memberExpr.GetSignatureForError());
					}
					return true;
				}
				if (rhs != null && this.InstanceExpression is UnboxCast)
				{
					rc.Report.Error(445, this.InstanceExpression.Location, "Cannot modify the result of an unboxing conversion");
				}
				return true;
			}
		}

		// Token: 0x06001732 RID: 5938 RVA: 0x0006EE58 File Offset: 0x0006D058
		public virtual MemberExpr ResolveMemberAccess(ResolveContext ec, Expression left, SimpleName original)
		{
			if (left != null && !this.ConditionalAccess && left.IsNull && TypeSpec.IsReferenceType(left.Type))
			{
				ec.Report.Warning(1720, 1, left.Location, "Expression will always cause a `{0}'", "System.NullReferenceException");
			}
			this.InstanceExpression = left;
			return this;
		}

		// Token: 0x06001733 RID: 5939 RVA: 0x0006EEB0 File Offset: 0x0006D0B0
		protected void EmitInstance(EmitContext ec, bool prepare_for_load)
		{
			InstanceEmitter instanceEmitter = new InstanceEmitter(this.InstanceExpression, TypeSpec.IsValueType(this.InstanceExpression.Type));
			instanceEmitter.Emit(ec, this.ConditionalAccess);
			if (prepare_for_load)
			{
				ec.Emit(OpCodes.Dup);
			}
		}

		// Token: 0x06001734 RID: 5940
		public abstract void SetTypeArguments(ResolveContext ec, TypeArguments ta);

		// Token: 0x06001735 RID: 5941 RVA: 0x00068BDB File Offset: 0x00066DDB
		protected MemberExpr()
		{
		}

		// Token: 0x04000962 RID: 2402
		protected bool conditional_access_receiver;

		// Token: 0x04000963 RID: 2403
		public Expression InstanceExpression;

		// Token: 0x04000964 RID: 2404
		[CompilerGenerated]
		private bool <ConditionalAccess>k__BackingField;
	}
}
