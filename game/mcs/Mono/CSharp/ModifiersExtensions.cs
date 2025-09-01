using System;
using System.Reflection;

namespace Mono.CSharp
{
	// Token: 0x02000258 RID: 600
	internal static class ModifiersExtensions
	{
		// Token: 0x06001DBE RID: 7614 RVA: 0x00091418 File Offset: 0x0008F618
		public static string AccessibilityName(Modifiers mod)
		{
			switch (mod & Modifiers.AccessibilityMask)
			{
			case Modifiers.PROTECTED:
				return "protected";
			case Modifiers.PUBLIC:
				return "public";
			case Modifiers.PRIVATE:
				return "private";
			case Modifiers.INTERNAL:
				return "internal";
			case Modifiers.PROTECTED | Modifiers.INTERNAL:
				return "protected internal";
			}
			throw new NotImplementedException(mod.ToString());
		}

		// Token: 0x06001DBF RID: 7615 RVA: 0x00091488 File Offset: 0x0008F688
		public static string Name(Modifiers i)
		{
			string result = "";
			if (i <= Modifiers.STATIC)
			{
				if (i <= Modifiers.NEW)
				{
					switch (i)
					{
					case Modifiers.PROTECTED:
						result = "protected";
						break;
					case Modifiers.PUBLIC:
						result = "public";
						break;
					case Modifiers.PROTECTED | Modifiers.PUBLIC:
						break;
					case Modifiers.PRIVATE:
						result = "private";
						break;
					default:
						if (i != Modifiers.INTERNAL)
						{
							if (i == Modifiers.NEW)
							{
								result = "new";
							}
						}
						else
						{
							result = "internal";
						}
						break;
					}
				}
				else if (i != Modifiers.ABSTRACT)
				{
					if (i != Modifiers.SEALED)
					{
						if (i == Modifiers.STATIC)
						{
							result = "static";
						}
					}
					else
					{
						result = "sealed";
					}
				}
				else
				{
					result = "abstract";
				}
			}
			else if (i <= Modifiers.OVERRIDE)
			{
				if (i != Modifiers.READONLY)
				{
					if (i != Modifiers.VIRTUAL)
					{
						if (i == Modifiers.OVERRIDE)
						{
							result = "override";
						}
					}
					else
					{
						result = "virtual";
					}
				}
				else
				{
					result = "readonly";
				}
			}
			else if (i <= Modifiers.VOLATILE)
			{
				if (i != Modifiers.EXTERN)
				{
					if (i == Modifiers.VOLATILE)
					{
						result = "volatile";
					}
				}
				else
				{
					result = "extern";
				}
			}
			else if (i != Modifiers.UNSAFE)
			{
				if (i == Modifiers.ASYNC)
				{
					result = "async";
				}
			}
			else
			{
				result = "unsafe";
			}
			return result;
		}

		// Token: 0x06001DC0 RID: 7616 RVA: 0x000915C8 File Offset: 0x0008F7C8
		public static bool IsRestrictedModifier(Modifiers modA, Modifiers modB)
		{
			Modifiers modifiers = (Modifiers)0;
			if ((modB & Modifiers.PUBLIC) != (Modifiers)0)
			{
				modifiers = (Modifiers.PROTECTED | Modifiers.PRIVATE | Modifiers.INTERNAL);
			}
			else if ((modB & Modifiers.PROTECTED) != (Modifiers)0)
			{
				if ((modB & Modifiers.INTERNAL) != (Modifiers)0)
				{
					modifiers = (Modifiers.PROTECTED | Modifiers.INTERNAL);
				}
				modifiers |= Modifiers.PRIVATE;
			}
			else if ((modB & Modifiers.INTERNAL) != (Modifiers)0)
			{
				modifiers = Modifiers.PRIVATE;
			}
			return modB != modA && (modA & ~modifiers) == (Modifiers)0;
		}

		// Token: 0x06001DC1 RID: 7617 RVA: 0x00091608 File Offset: 0x0008F808
		public static TypeAttributes TypeAttr(Modifiers mod_flags, bool is_toplevel)
		{
			TypeAttributes typeAttributes = TypeAttributes.NotPublic;
			if (is_toplevel)
			{
				if ((mod_flags & Modifiers.PUBLIC) != (Modifiers)0)
				{
					typeAttributes = TypeAttributes.Public;
				}
				else if ((mod_flags & Modifiers.PRIVATE) != (Modifiers)0)
				{
					typeAttributes = TypeAttributes.NotPublic;
				}
			}
			else if ((mod_flags & Modifiers.PUBLIC) != (Modifiers)0)
			{
				typeAttributes = TypeAttributes.NestedPublic;
			}
			else if ((mod_flags & Modifiers.PRIVATE) != (Modifiers)0)
			{
				typeAttributes = TypeAttributes.NestedPrivate;
			}
			else if ((mod_flags & (Modifiers.PROTECTED | Modifiers.INTERNAL)) == (Modifiers.PROTECTED | Modifiers.INTERNAL))
			{
				typeAttributes = TypeAttributes.VisibilityMask;
			}
			else if ((mod_flags & Modifiers.PROTECTED) != (Modifiers)0)
			{
				typeAttributes = TypeAttributes.NestedFamily;
			}
			else if ((mod_flags & Modifiers.INTERNAL) != (Modifiers)0)
			{
				typeAttributes = TypeAttributes.NestedAssembly;
			}
			if ((mod_flags & Modifiers.SEALED) != (Modifiers)0)
			{
				typeAttributes |= TypeAttributes.Sealed;
			}
			if ((mod_flags & Modifiers.ABSTRACT) != (Modifiers)0)
			{
				typeAttributes |= TypeAttributes.Abstract;
			}
			return typeAttributes;
		}

		// Token: 0x06001DC2 RID: 7618 RVA: 0x00091678 File Offset: 0x0008F878
		public static FieldAttributes FieldAttr(Modifiers mod_flags)
		{
			FieldAttributes fieldAttributes = FieldAttributes.PrivateScope;
			if ((mod_flags & Modifiers.PUBLIC) != (Modifiers)0)
			{
				fieldAttributes |= FieldAttributes.Public;
			}
			if ((mod_flags & Modifiers.PRIVATE) != (Modifiers)0)
			{
				fieldAttributes |= FieldAttributes.Private;
			}
			if ((mod_flags & Modifiers.PROTECTED) != (Modifiers)0)
			{
				if ((mod_flags & Modifiers.INTERNAL) != (Modifiers)0)
				{
					fieldAttributes |= FieldAttributes.FamORAssem;
				}
				else
				{
					fieldAttributes |= FieldAttributes.Family;
				}
			}
			else if ((mod_flags & Modifiers.INTERNAL) != (Modifiers)0)
			{
				fieldAttributes |= FieldAttributes.Assembly;
			}
			if ((mod_flags & Modifiers.STATIC) != (Modifiers)0)
			{
				fieldAttributes |= FieldAttributes.Static;
			}
			if ((mod_flags & Modifiers.READONLY) != (Modifiers)0)
			{
				fieldAttributes |= FieldAttributes.InitOnly;
			}
			return fieldAttributes;
		}

		// Token: 0x06001DC3 RID: 7619 RVA: 0x000916D8 File Offset: 0x0008F8D8
		public static MethodAttributes MethodAttr(Modifiers mod_flags)
		{
			MethodAttributes methodAttributes = MethodAttributes.HideBySig;
			switch (mod_flags & Modifiers.AccessibilityMask)
			{
			case Modifiers.PROTECTED:
				methodAttributes |= MethodAttributes.Family;
				goto IL_6A;
			case Modifiers.PUBLIC:
				methodAttributes |= MethodAttributes.Public;
				goto IL_6A;
			case Modifiers.PRIVATE:
				methodAttributes |= MethodAttributes.Private;
				goto IL_6A;
			case Modifiers.INTERNAL:
				methodAttributes |= MethodAttributes.Assembly;
				goto IL_6A;
			case Modifiers.PROTECTED | Modifiers.INTERNAL:
				methodAttributes |= MethodAttributes.FamORAssem;
				goto IL_6A;
			}
			throw new NotImplementedException(mod_flags.ToString());
			IL_6A:
			if ((mod_flags & Modifiers.STATIC) != (Modifiers)0)
			{
				methodAttributes |= MethodAttributes.Static;
			}
			if ((mod_flags & Modifiers.ABSTRACT) != (Modifiers)0)
			{
				methodAttributes |= (MethodAttributes.Virtual | MethodAttributes.Abstract);
			}
			if ((mod_flags & Modifiers.SEALED) != (Modifiers)0)
			{
				methodAttributes |= MethodAttributes.Final;
			}
			if ((mod_flags & Modifiers.VIRTUAL) != (Modifiers)0)
			{
				methodAttributes |= MethodAttributes.Virtual;
			}
			if ((mod_flags & Modifiers.OVERRIDE) != (Modifiers)0)
			{
				methodAttributes |= MethodAttributes.Virtual;
			}
			else if ((methodAttributes & MethodAttributes.Virtual) != MethodAttributes.PrivateScope)
			{
				methodAttributes |= MethodAttributes.VtableLayoutMask;
			}
			return methodAttributes;
		}

		// Token: 0x06001DC4 RID: 7620 RVA: 0x000917A4 File Offset: 0x0008F9A4
		public static Modifiers Check(Modifiers allowed, Modifiers mod, Modifiers def_access, Location l, Report Report)
		{
			int num = (int)(~(int)allowed & (mod & (Modifiers.PROTECTED | Modifiers.PUBLIC | Modifiers.PRIVATE | Modifiers.INTERNAL | Modifiers.NEW | Modifiers.ABSTRACT | Modifiers.SEALED | Modifiers.STATIC | Modifiers.READONLY | Modifiers.VIRTUAL | Modifiers.OVERRIDE | Modifiers.EXTERN | Modifiers.VOLATILE | Modifiers.UNSAFE | Modifiers.ASYNC)));
			if (num != 0)
			{
				for (int i = 1; i < 32768; i <<= 1)
				{
					if ((i & num) != 0)
					{
						ModifiersExtensions.Error_InvalidModifier((Modifiers)i, l, Report);
					}
				}
				return allowed & mod;
			}
			if ((mod & Modifiers.AccessibilityMask) == (Modifiers)0)
			{
				mod |= def_access;
				if (def_access != (Modifiers)0)
				{
					mod |= Modifiers.DEFAULT_ACCESS_MODIFIER;
				}
				return mod;
			}
			return mod;
		}

		// Token: 0x06001DC5 RID: 7621 RVA: 0x000917FB File Offset: 0x0008F9FB
		private static void Error_InvalidModifier(Modifiers mod, Location l, Report Report)
		{
			Report.Error(106, l, "The modifier `{0}' is not valid for this item", ModifiersExtensions.Name(mod));
		}
	}
}
