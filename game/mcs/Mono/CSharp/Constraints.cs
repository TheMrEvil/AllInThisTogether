using System;
using System.Collections.Generic;

namespace Mono.CSharp
{
	// Token: 0x0200021E RID: 542
	public class Constraints
	{
		// Token: 0x06001B6D RID: 7021 RVA: 0x00084ED2 File Offset: 0x000830D2
		public Constraints(SimpleMemberName tparam, List<FullNamedExpression> constraints, Location loc)
		{
			this.tparam = tparam;
			this.constraints = constraints;
			this.loc = loc;
		}

		// Token: 0x1700062F RID: 1583
		// (get) Token: 0x06001B6E RID: 7022 RVA: 0x00084EEF File Offset: 0x000830EF
		public List<FullNamedExpression> TypeExpressions
		{
			get
			{
				return this.constraints;
			}
		}

		// Token: 0x17000630 RID: 1584
		// (get) Token: 0x06001B6F RID: 7023 RVA: 0x00084EF7 File Offset: 0x000830F7
		public Location Location
		{
			get
			{
				return this.loc;
			}
		}

		// Token: 0x17000631 RID: 1585
		// (get) Token: 0x06001B70 RID: 7024 RVA: 0x00084EFF File Offset: 0x000830FF
		public SimpleMemberName TypeParameter
		{
			get
			{
				return this.tparam;
			}
		}

		// Token: 0x06001B71 RID: 7025 RVA: 0x00084F08 File Offset: 0x00083108
		public static bool CheckConflictingInheritedConstraint(TypeParameterSpec spec, TypeSpec bb, IMemberContext context, Location loc)
		{
			if (spec.HasSpecialClass && bb.IsStruct)
			{
				context.Module.Compiler.Report.Error(455, loc, "Type parameter `{0}' inherits conflicting constraints `{1}' and `{2}'", new string[]
				{
					spec.Name,
					"class",
					bb.GetSignatureForError()
				});
				return false;
			}
			return Constraints.CheckConflictingInheritedConstraint(spec, spec.BaseType, bb, context, loc);
		}

		// Token: 0x06001B72 RID: 7026 RVA: 0x00084F76 File Offset: 0x00083176
		private static bool CheckConflictingInheritedConstraint(TypeParameterSpec spec, TypeSpec ba, TypeSpec bb, IMemberContext context, Location loc)
		{
			if (ba == bb)
			{
				return true;
			}
			if (TypeSpec.IsBaseClass(ba, bb, false) || TypeSpec.IsBaseClass(bb, ba, false))
			{
				return true;
			}
			Constraints.Error_ConflictingConstraints(context, spec, ba, bb, loc);
			return false;
		}

		// Token: 0x06001B73 RID: 7027 RVA: 0x00084FA0 File Offset: 0x000831A0
		public static void Error_ConflictingConstraints(IMemberContext context, TypeParameterSpec tp, TypeSpec ba, TypeSpec bb, Location loc)
		{
			context.Module.Compiler.Report.Error(455, loc, "Type parameter `{0}' inherits conflicting constraints `{1}' and `{2}'", new string[]
			{
				tp.Name,
				ba.GetSignatureForError(),
				bb.GetSignatureForError()
			});
		}

		// Token: 0x06001B74 RID: 7028 RVA: 0x00084FF0 File Offset: 0x000831F0
		public void CheckGenericConstraints(IMemberContext context, bool obsoleteCheck)
		{
			foreach (FullNamedExpression fullNamedExpression in this.constraints)
			{
				if (fullNamedExpression != null)
				{
					TypeSpec type = fullNamedExpression.Type;
					if (type != null)
					{
						if (obsoleteCheck)
						{
							ObsoleteAttribute attributeObsolete = type.GetAttributeObsolete();
							if (attributeObsolete != null)
							{
								AttributeTester.Report_ObsoleteMessage(attributeObsolete, type.GetSignatureForError(), fullNamedExpression.Location, context.Module.Compiler.Report);
							}
						}
						ConstraintChecker.Check(context, type, fullNamedExpression.Location);
					}
				}
			}
		}

		// Token: 0x06001B75 RID: 7029 RVA: 0x00085088 File Offset: 0x00083288
		public bool Resolve(IMemberContext context, TypeParameter tp)
		{
			if (this.resolved)
			{
				return true;
			}
			if (this.resolving)
			{
				return false;
			}
			this.resolving = true;
			TypeParameterSpec type = tp.Type;
			List<TypeParameterSpec> list = null;
			bool flag = false;
			type.BaseType = context.Module.Compiler.BuiltinTypes.Object;
			for (int i = 0; i < this.constraints.Count; i++)
			{
				FullNamedExpression fullNamedExpression = this.constraints[i];
				if (fullNamedExpression is SpecialContraintExpr)
				{
					type.SpecialConstraint |= ((SpecialContraintExpr)fullNamedExpression).Constraint;
					if (type.HasSpecialStruct)
					{
						type.BaseType = context.Module.Compiler.BuiltinTypes.ValueType;
					}
					this.constraints[i] = null;
				}
				else
				{
					TypeSpec typeSpec = fullNamedExpression.ResolveAsType(context, false);
					if (typeSpec != null)
					{
						if (typeSpec.Arity > 0 && ((InflatedTypeSpec)typeSpec).HasDynamicArgument())
						{
							context.Module.Compiler.Report.Error(1968, fullNamedExpression.Location, "A constraint cannot be the dynamic type `{0}'", typeSpec.GetSignatureForError());
						}
						else
						{
							if (!context.CurrentMemberDefinition.IsAccessibleAs(typeSpec))
							{
								context.Module.Compiler.Report.SymbolRelatedToPreviousError(typeSpec);
								context.Module.Compiler.Report.Error(703, this.loc, "Inconsistent accessibility: constraint type `{0}' is less accessible than `{1}'", typeSpec.GetSignatureForError(), context.GetSignatureForError());
							}
							if (typeSpec.IsInterface)
							{
								if (!type.AddInterface(typeSpec))
								{
									context.Module.Compiler.Report.Error(405, fullNamedExpression.Location, "Duplicate constraint `{0}' for type parameter `{1}'", typeSpec.GetSignatureForError(), this.tparam.Value);
								}
								flag = true;
							}
							else
							{
								TypeParameterSpec typeParameterSpec = typeSpec as TypeParameterSpec;
								if (typeParameterSpec != null)
								{
									if (list == null)
									{
										list = new List<TypeParameterSpec>(2);
									}
									else if (list.Contains(typeParameterSpec))
									{
										context.Module.Compiler.Report.Error(405, fullNamedExpression.Location, "Duplicate constraint `{0}' for type parameter `{1}'", typeSpec.GetSignatureForError(), this.tparam.Value);
										goto IL_4FE;
									}
									if (tp.IsMethodTypeParameter)
									{
										VarianceDecl.CheckTypeVariance(typeSpec, Variance.Contravariant, context);
									}
									TypeParameter typeParameter = typeParameterSpec.MemberDefinition as TypeParameter;
									if (typeParameter != null && !typeParameter.ResolveConstraints(context))
									{
										context.Module.Compiler.Report.Error(454, fullNamedExpression.Location, "Circular constraint dependency involving `{0}' and `{1}'", typeParameterSpec.GetSignatureForError(), tp.GetSignatureForError());
									}
									else
									{
										if (typeParameterSpec.HasTypeConstraint)
										{
											if (type.HasTypeConstraint || type.HasSpecialStruct)
											{
												if (!Constraints.CheckConflictingInheritedConstraint(type, typeParameterSpec.BaseType, context, fullNamedExpression.Location))
												{
													goto IL_4FE;
												}
											}
											else
											{
												int num = 0;
												while (num < list.Count && (!list[num].HasTypeConstraint || Constraints.CheckConflictingInheritedConstraint(type, list[num].BaseType, typeParameterSpec.BaseType, context, fullNamedExpression.Location)))
												{
													num++;
												}
											}
										}
										if (typeParameterSpec.TypeArguments != null)
										{
											TypeSpec effectiveBase = typeParameterSpec.GetEffectiveBase();
											if (effectiveBase != null && !Constraints.CheckConflictingInheritedConstraint(type, effectiveBase, type.BaseType, context, fullNamedExpression.Location))
											{
												break;
											}
										}
										if (typeParameterSpec.HasSpecialStruct)
										{
											context.Module.Compiler.Report.Error(456, fullNamedExpression.Location, "Type parameter `{0}' has the `struct' constraint, so it cannot be used as a constraint for `{1}'", typeParameterSpec.GetSignatureForError(), tp.GetSignatureForError());
										}
										else
										{
											list.Add(typeParameterSpec);
										}
									}
								}
								else
								{
									if (flag || type.HasTypeConstraint)
									{
										context.Module.Compiler.Report.Error(406, fullNamedExpression.Location, "The class type constraint `{0}' must be listed before any other constraints. Consider moving type constraint to the beginning of the constraint list", typeSpec.GetSignatureForError());
									}
									if (type.HasSpecialStruct || type.HasSpecialClass)
									{
										context.Module.Compiler.Report.Error(450, fullNamedExpression.Location, "`{0}': cannot specify both a constraint class and the `class' or `struct' constraint", typeSpec.GetSignatureForError());
									}
									switch (typeSpec.BuiltinType)
									{
									case BuiltinTypeSpec.Type.Object:
									case BuiltinTypeSpec.Type.ValueType:
									case BuiltinTypeSpec.Type.Enum:
									case BuiltinTypeSpec.Type.Delegate:
									case BuiltinTypeSpec.Type.MulticastDelegate:
									case BuiltinTypeSpec.Type.Array:
										context.Module.Compiler.Report.Error(702, fullNamedExpression.Location, "A constraint cannot be special class `{0}'", typeSpec.GetSignatureForError());
										goto IL_4FE;
									case BuiltinTypeSpec.Type.Dynamic:
										context.Module.Compiler.Report.Error(1967, fullNamedExpression.Location, "A constraint cannot be the dynamic type");
										goto IL_4FE;
									}
									if (typeSpec.IsSealed || !typeSpec.IsClass)
									{
										context.Module.Compiler.Report.Error(701, this.loc, "`{0}' is not a valid constraint. A constraint must be an interface, a non-sealed class or a type parameter", typeSpec.GetSignatureForError());
									}
									else
									{
										if (typeSpec.IsStatic)
										{
											context.Module.Compiler.Report.Error(717, fullNamedExpression.Location, "`{0}' is not a valid constraint. Static classes cannot be used as constraints", typeSpec.GetSignatureForError());
										}
										type.BaseType = typeSpec;
									}
								}
							}
						}
					}
				}
				IL_4FE:;
			}
			if (list != null)
			{
				type.TypeArguments = list.ToArray();
			}
			this.resolving = false;
			this.resolved = true;
			return true;
		}

		// Token: 0x06001B76 RID: 7030 RVA: 0x000855C8 File Offset: 0x000837C8
		public void VerifyClsCompliance(Report report)
		{
			foreach (FullNamedExpression fullNamedExpression in this.constraints)
			{
				if (fullNamedExpression != null && !fullNamedExpression.Type.IsCLSCompliant())
				{
					report.SymbolRelatedToPreviousError(fullNamedExpression.Type);
					report.Warning(3024, 1, this.loc, "Constraint type `{0}' is not CLS-compliant", fullNamedExpression.Type.GetSignatureForError());
				}
			}
		}

		// Token: 0x04000A3E RID: 2622
		private readonly SimpleMemberName tparam;

		// Token: 0x04000A3F RID: 2623
		private readonly List<FullNamedExpression> constraints;

		// Token: 0x04000A40 RID: 2624
		private readonly Location loc;

		// Token: 0x04000A41 RID: 2625
		private bool resolved;

		// Token: 0x04000A42 RID: 2626
		private bool resolving;
	}
}
