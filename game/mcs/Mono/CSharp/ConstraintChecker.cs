using System;

namespace Mono.CSharp
{
	// Token: 0x0200022A RID: 554
	internal struct ConstraintChecker
	{
		// Token: 0x06001C16 RID: 7190 RVA: 0x00087D48 File Offset: 0x00085F48
		public ConstraintChecker(IMemberContext ctx)
		{
			this.mc = ctx;
			this.recursive_checks = false;
		}

		// Token: 0x06001C17 RID: 7191 RVA: 0x00087D58 File Offset: 0x00085F58
		public static bool Check(IMemberContext mc, TypeSpec type, Location loc)
		{
			if (type.DeclaringType != null && !ConstraintChecker.Check(mc, type.DeclaringType, loc))
			{
				return false;
			}
			while (type is ElementTypeSpec)
			{
				type = ((ElementTypeSpec)type).Element;
			}
			if (type.Arity == 0)
			{
				return true;
			}
			InflatedTypeSpec inflatedTypeSpec = type as InflatedTypeSpec;
			if (inflatedTypeSpec == null)
			{
				return true;
			}
			TypeParameterSpec[] constraints = inflatedTypeSpec.Constraints;
			if (constraints == null)
			{
				return true;
			}
			if (inflatedTypeSpec.HasConstraintsChecked)
			{
				return true;
			}
			ConstraintChecker constraintChecker = new ConstraintChecker(mc);
			constraintChecker.recursive_checks = true;
			if (constraintChecker.CheckAll(inflatedTypeSpec.GetDefinition(), type.TypeArguments, constraints, loc))
			{
				inflatedTypeSpec.HasConstraintsChecked = true;
				return true;
			}
			return false;
		}

		// Token: 0x06001C18 RID: 7192 RVA: 0x00087DF0 File Offset: 0x00085FF0
		public bool CheckAll(MemberSpec context, TypeSpec[] targs, TypeParameterSpec[] tparams, Location loc)
		{
			if (targs == null)
			{
				return true;
			}
			for (int i = 0; i < tparams.Length; i++)
			{
				TypeSpec typeSpec = targs[i];
				if (!this.CheckConstraint(context, typeSpec, tparams[i], loc))
				{
					return false;
				}
				if (this.recursive_checks && !ConstraintChecker.Check(this.mc, typeSpec, loc))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001C19 RID: 7193 RVA: 0x00087E40 File Offset: 0x00086040
		private bool CheckConstraint(MemberSpec context, TypeSpec atype, TypeParameterSpec tparam, Location loc)
		{
			if (tparam.HasSpecialClass && !TypeSpec.IsReferenceType(atype))
			{
				if (this.mc != null)
				{
					this.mc.Module.Compiler.Report.Error(452, loc, "The type `{0}' must be a reference type in order to use it as type parameter `{1}' in the generic type or method `{2}'", new string[]
					{
						atype.GetSignatureForError(),
						tparam.GetSignatureForError(),
						context.GetSignatureForError()
					});
				}
				return false;
			}
			if (tparam.HasSpecialStruct && (!TypeSpec.IsValueType(atype) || atype.IsNullableType))
			{
				if (this.mc != null)
				{
					this.mc.Module.Compiler.Report.Error(453, loc, "The type `{0}' must be a non-nullable value type in order to use it as type parameter `{1}' in the generic type or method `{2}'", new string[]
					{
						atype.GetSignatureForError(),
						tparam.GetSignatureForError(),
						context.GetSignatureForError()
					});
				}
				return false;
			}
			bool result = true;
			if (tparam.HasTypeConstraint && !this.CheckConversion(this.mc, context, atype, tparam, tparam.BaseType, loc))
			{
				if (this.mc == null)
				{
					return false;
				}
				result = false;
			}
			if (tparam.InterfacesDefined != null)
			{
				TypeSpec[] array = tparam.InterfacesDefined;
				int i = 0;
				while (i < array.Length)
				{
					TypeSpec ttype = array[i];
					if (!this.CheckConversion(this.mc, context, atype, tparam, ttype, loc))
					{
						if (this.mc == null)
						{
							return false;
						}
						result = false;
						break;
					}
					else
					{
						i++;
					}
				}
			}
			if (tparam.TypeArguments != null)
			{
				TypeSpec[] array = tparam.TypeArguments;
				int i = 0;
				while (i < array.Length)
				{
					TypeSpec ttype2 = array[i];
					if (!this.CheckConversion(this.mc, context, atype, tparam, ttype2, loc))
					{
						if (this.mc == null)
						{
							return false;
						}
						result = false;
						break;
					}
					else
					{
						i++;
					}
				}
			}
			if (!tparam.HasSpecialConstructor)
			{
				return result;
			}
			if (!ConstraintChecker.HasDefaultConstructor(atype))
			{
				if (this.mc != null)
				{
					this.mc.Module.Compiler.Report.SymbolRelatedToPreviousError(atype);
					this.mc.Module.Compiler.Report.Error(310, loc, "The type `{0}' must have a public parameterless constructor in order to use it as parameter `{1}' in the generic type or method `{2}'", new string[]
					{
						atype.GetSignatureForError(),
						tparam.GetSignatureForError(),
						context.GetSignatureForError()
					});
				}
				return false;
			}
			return result;
		}

		// Token: 0x06001C1A RID: 7194 RVA: 0x00088050 File Offset: 0x00086250
		private static bool HasDynamicTypeArgument(TypeSpec[] targs)
		{
			foreach (TypeSpec typeSpec in targs)
			{
				if (typeSpec.BuiltinType == BuiltinTypeSpec.Type.Dynamic)
				{
					return true;
				}
				if (ConstraintChecker.HasDynamicTypeArgument(typeSpec.TypeArguments))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001C1B RID: 7195 RVA: 0x0008808C File Offset: 0x0008628C
		private bool CheckConversion(IMemberContext mc, MemberSpec context, TypeSpec atype, TypeParameterSpec tparam, TypeSpec ttype, Location loc)
		{
			if (atype == ttype)
			{
				return true;
			}
			if (atype.IsGenericParameter)
			{
				TypeParameterSpec typeParameterSpec = (TypeParameterSpec)atype;
				if (typeParameterSpec.HasDependencyOn(ttype))
				{
					return true;
				}
				if (Convert.ImplicitTypeParameterConversion(null, typeParameterSpec, ttype) != null)
				{
					return true;
				}
			}
			else if (TypeSpec.IsValueType(atype))
			{
				if (atype.IsNullableType)
				{
					if (TypeSpec.IsBaseClass(atype, ttype, false))
					{
						return true;
					}
				}
				else if (Convert.ImplicitBoxingConversion(null, atype, ttype) != null)
				{
					return true;
				}
			}
			else if (Convert.ImplicitReferenceConversionExists(atype, ttype) || Convert.ImplicitBoxingConversion(null, atype, ttype) != null)
			{
				return true;
			}
			if (mc != null)
			{
				mc.Module.Compiler.Report.SymbolRelatedToPreviousError(tparam);
				if (atype.IsGenericParameter)
				{
					mc.Module.Compiler.Report.Error(314, loc, "The type `{0}' cannot be used as type parameter `{1}' in the generic type or method `{2}'. There is no boxing or type parameter conversion from `{0}' to `{3}'", new string[]
					{
						atype.GetSignatureForError(),
						tparam.GetSignatureForError(),
						context.GetSignatureForError(),
						ttype.GetSignatureForError()
					});
				}
				else if (TypeSpec.IsValueType(atype))
				{
					if (atype.IsNullableType)
					{
						if (ttype.IsInterface)
						{
							mc.Module.Compiler.Report.Error(313, loc, "The type `{0}' cannot be used as type parameter `{1}' in the generic type or method `{2}'. The nullable type `{0}' never satisfies interface constraint `{3}'", new string[]
							{
								atype.GetSignatureForError(),
								tparam.GetSignatureForError(),
								context.GetSignatureForError(),
								ttype.GetSignatureForError()
							});
						}
						else
						{
							mc.Module.Compiler.Report.Error(312, loc, "The type `{0}' cannot be used as type parameter `{1}' in the generic type or method `{2}'. The nullable type `{0}' does not satisfy constraint `{3}'", new string[]
							{
								atype.GetSignatureForError(),
								tparam.GetSignatureForError(),
								context.GetSignatureForError(),
								ttype.GetSignatureForError()
							});
						}
					}
					else
					{
						mc.Module.Compiler.Report.Error(315, loc, "The type `{0}' cannot be used as type parameter `{1}' in the generic type or method `{2}'. There is no boxing conversion from `{0}' to `{3}'", new string[]
						{
							atype.GetSignatureForError(),
							tparam.GetSignatureForError(),
							context.GetSignatureForError(),
							ttype.GetSignatureForError()
						});
					}
				}
				else
				{
					mc.Module.Compiler.Report.Error(311, loc, "The type `{0}' cannot be used as type parameter `{1}' in the generic type or method `{2}'. There is no implicit reference conversion from `{0}' to `{3}'", new string[]
					{
						atype.GetSignatureForError(),
						tparam.GetSignatureForError(),
						context.GetSignatureForError(),
						ttype.GetSignatureForError()
					});
				}
			}
			return false;
		}

		// Token: 0x06001C1C RID: 7196 RVA: 0x000882E0 File Offset: 0x000864E0
		private static bool HasDefaultConstructor(TypeSpec atype)
		{
			TypeParameterSpec typeParameterSpec = atype as TypeParameterSpec;
			if (typeParameterSpec != null)
			{
				return typeParameterSpec.HasSpecialConstructor || typeParameterSpec.HasSpecialStruct;
			}
			if (atype.IsStruct || atype.IsEnum)
			{
				return true;
			}
			if (atype.IsAbstract)
			{
				return false;
			}
			MemberSpec memberSpec = MemberCache.FindMember(atype.GetDefinition(), MemberFilter.Constructor(ParametersCompiled.EmptyReadOnlyParameters), BindingRestriction.DeclaredOnly | BindingRestriction.InstanceOnly);
			return memberSpec != null && (memberSpec.Modifiers & Modifiers.PUBLIC) > (Modifiers)0;
		}

		// Token: 0x04000A62 RID: 2658
		private IMemberContext mc;

		// Token: 0x04000A63 RID: 2659
		private bool recursive_checks;
	}
}
