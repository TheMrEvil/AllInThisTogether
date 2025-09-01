using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;

namespace System
{
	// Token: 0x02000262 RID: 610
	internal class TypeSpec
	{
		// Token: 0x17000337 RID: 823
		// (get) Token: 0x06001BD9 RID: 7129 RVA: 0x00067CE5 File Offset: 0x00065EE5
		internal bool HasModifiers
		{
			get
			{
				return this.modifier_spec != null;
			}
		}

		// Token: 0x17000338 RID: 824
		// (get) Token: 0x06001BDA RID: 7130 RVA: 0x00067CF0 File Offset: 0x00065EF0
		internal bool IsNested
		{
			get
			{
				return this.nested != null && this.nested.Count > 0;
			}
		}

		// Token: 0x17000339 RID: 825
		// (get) Token: 0x06001BDB RID: 7131 RVA: 0x00067D0A File Offset: 0x00065F0A
		internal bool IsByRef
		{
			get
			{
				return this.is_byref;
			}
		}

		// Token: 0x1700033A RID: 826
		// (get) Token: 0x06001BDC RID: 7132 RVA: 0x00067D12 File Offset: 0x00065F12
		internal TypeName Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x1700033B RID: 827
		// (get) Token: 0x06001BDD RID: 7133 RVA: 0x00067D1A File Offset: 0x00065F1A
		internal IEnumerable<TypeName> Nested
		{
			get
			{
				if (this.nested != null)
				{
					return this.nested;
				}
				return Array.Empty<TypeName>();
			}
		}

		// Token: 0x1700033C RID: 828
		// (get) Token: 0x06001BDE RID: 7134 RVA: 0x00067D30 File Offset: 0x00065F30
		internal IEnumerable<ModifierSpec> Modifiers
		{
			get
			{
				if (this.modifier_spec != null)
				{
					return this.modifier_spec;
				}
				return Array.Empty<ModifierSpec>();
			}
		}

		// Token: 0x06001BDF RID: 7135 RVA: 0x00067D48 File Offset: 0x00065F48
		private string GetDisplayFullName(TypeSpec.DisplayNameFormat flags)
		{
			bool flag = (flags & TypeSpec.DisplayNameFormat.WANT_ASSEMBLY) > TypeSpec.DisplayNameFormat.Default;
			bool flag2 = (flags & TypeSpec.DisplayNameFormat.NO_MODIFIERS) == TypeSpec.DisplayNameFormat.Default;
			StringBuilder stringBuilder = new StringBuilder(this.name.DisplayName);
			if (this.nested != null)
			{
				foreach (TypeIdentifier typeIdentifier in this.nested)
				{
					stringBuilder.Append('+').Append(typeIdentifier.DisplayName);
				}
			}
			if (this.generic_params != null)
			{
				stringBuilder.Append('[');
				for (int i = 0; i < this.generic_params.Count; i++)
				{
					if (i > 0)
					{
						stringBuilder.Append(", ");
					}
					if (this.generic_params[i].assembly_name != null)
					{
						stringBuilder.Append('[').Append(this.generic_params[i].DisplayFullName).Append(']');
					}
					else
					{
						stringBuilder.Append(this.generic_params[i].DisplayFullName);
					}
				}
				stringBuilder.Append(']');
			}
			if (flag2)
			{
				this.GetModifierString(stringBuilder);
			}
			if (this.assembly_name != null && flag)
			{
				stringBuilder.Append(", ").Append(this.assembly_name);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001BE0 RID: 7136 RVA: 0x00067EA4 File Offset: 0x000660A4
		internal string ModifierString()
		{
			return this.GetModifierString(new StringBuilder()).ToString();
		}

		// Token: 0x06001BE1 RID: 7137 RVA: 0x00067EB8 File Offset: 0x000660B8
		private StringBuilder GetModifierString(StringBuilder sb)
		{
			if (this.modifier_spec != null)
			{
				foreach (ModifierSpec modifierSpec in this.modifier_spec)
				{
					modifierSpec.Append(sb);
				}
			}
			if (this.is_byref)
			{
				sb.Append('&');
			}
			return sb;
		}

		// Token: 0x1700033D RID: 829
		// (get) Token: 0x06001BE2 RID: 7138 RVA: 0x00067F24 File Offset: 0x00066124
		internal string DisplayFullName
		{
			get
			{
				if (this.display_fullname == null)
				{
					this.display_fullname = this.GetDisplayFullName(TypeSpec.DisplayNameFormat.Default);
				}
				return this.display_fullname;
			}
		}

		// Token: 0x06001BE3 RID: 7139 RVA: 0x00067F44 File Offset: 0x00066144
		internal static TypeSpec Parse(string typeName)
		{
			int num = 0;
			if (typeName == null)
			{
				throw new ArgumentNullException("typeName");
			}
			TypeSpec result = TypeSpec.Parse(typeName, ref num, false, true);
			if (num < typeName.Length)
			{
				throw new ArgumentException("Count not parse the whole type name", "typeName");
			}
			return result;
		}

		// Token: 0x06001BE4 RID: 7140 RVA: 0x00067F84 File Offset: 0x00066184
		internal static string EscapeDisplayName(string internalName)
		{
			StringBuilder stringBuilder = new StringBuilder(internalName.Length);
			int i = 0;
			while (i < internalName.Length)
			{
				char value = internalName[i];
				switch (value)
				{
				case '&':
				case '*':
				case '+':
				case ',':
					goto IL_56;
				case '\'':
				case '(':
				case ')':
					goto IL_67;
				default:
					switch (value)
					{
					case '[':
					case '\\':
					case ']':
						goto IL_56;
					default:
						goto IL_67;
					}
					break;
				}
				IL_6F:
				i++;
				continue;
				IL_56:
				stringBuilder.Append('\\').Append(value);
				goto IL_6F;
				IL_67:
				stringBuilder.Append(value);
				goto IL_6F;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001BE5 RID: 7141 RVA: 0x00068014 File Offset: 0x00066214
		internal static string UnescapeInternalName(string displayName)
		{
			StringBuilder stringBuilder = new StringBuilder(displayName.Length);
			for (int i = 0; i < displayName.Length; i++)
			{
				char c = displayName[i];
				if (c == '\\' && ++i < displayName.Length)
				{
					c = displayName[i];
				}
				stringBuilder.Append(c);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001BE6 RID: 7142 RVA: 0x00068070 File Offset: 0x00066270
		internal static bool NeedsEscaping(string internalName)
		{
			foreach (char c in internalName)
			{
				switch (c)
				{
				case '&':
				case '*':
				case '+':
				case ',':
					return true;
				case '\'':
				case '(':
				case ')':
					break;
				default:
					switch (c)
					{
					case '[':
					case '\\':
					case ']':
						return true;
					}
					break;
				}
			}
			return false;
		}

		// Token: 0x06001BE7 RID: 7143 RVA: 0x000680D8 File Offset: 0x000662D8
		internal Type Resolve(Func<AssemblyName, Assembly> assemblyResolver, Func<Assembly, string, bool, Type> typeResolver, bool throwOnError, bool ignoreCase, ref StackCrawlMark stackMark)
		{
			Assembly assembly = null;
			if (assemblyResolver == null && typeResolver == null)
			{
				return RuntimeType.GetType(this.DisplayFullName, throwOnError, ignoreCase, false, ref stackMark);
			}
			if (this.assembly_name != null)
			{
				if (assemblyResolver != null)
				{
					assembly = assemblyResolver(new AssemblyName(this.assembly_name));
				}
				else
				{
					assembly = Assembly.Load(this.assembly_name);
				}
				if (assembly == null)
				{
					if (throwOnError)
					{
						throw new FileNotFoundException("Could not resolve assembly '" + this.assembly_name + "'");
					}
					return null;
				}
			}
			Type type = null;
			if (typeResolver != null)
			{
				type = typeResolver(assembly, this.name.DisplayName, ignoreCase);
			}
			else
			{
				type = assembly.GetType(this.name.DisplayName, false, ignoreCase);
			}
			if (!(type == null))
			{
				if (this.nested != null)
				{
					foreach (TypeIdentifier typeIdentifier in this.nested)
					{
						Type nestedType = type.GetNestedType(typeIdentifier.DisplayName, BindingFlags.Public | BindingFlags.NonPublic);
						if (nestedType == null)
						{
							if (throwOnError)
							{
								string str = "Could not resolve type '";
								TypeIdentifier typeIdentifier2 = typeIdentifier;
								throw new TypeLoadException(str + ((typeIdentifier2 != null) ? typeIdentifier2.ToString() : null) + "'");
							}
							return null;
						}
						else
						{
							type = nestedType;
						}
					}
				}
				if (this.generic_params != null)
				{
					Type[] array = new Type[this.generic_params.Count];
					int i = 0;
					while (i < array.Length)
					{
						Type type2 = this.generic_params[i].Resolve(assemblyResolver, typeResolver, throwOnError, ignoreCase, ref stackMark);
						if (type2 == null)
						{
							if (throwOnError)
							{
								string str2 = "Could not resolve type '";
								TypeIdentifier typeIdentifier3 = this.generic_params[i].name;
								throw new TypeLoadException(str2 + ((typeIdentifier3 != null) ? typeIdentifier3.ToString() : null) + "'");
							}
							return null;
						}
						else
						{
							array[i] = type2;
							i++;
						}
					}
					type = type.MakeGenericType(array);
				}
				if (this.modifier_spec != null)
				{
					foreach (ModifierSpec modifierSpec in this.modifier_spec)
					{
						type = modifierSpec.Resolve(type);
					}
				}
				if (this.is_byref)
				{
					type = type.MakeByRefType();
				}
				return type;
			}
			if (throwOnError)
			{
				string str3 = "Could not resolve type '";
				TypeIdentifier typeIdentifier4 = this.name;
				throw new TypeLoadException(str3 + ((typeIdentifier4 != null) ? typeIdentifier4.ToString() : null) + "'");
			}
			return null;
		}

		// Token: 0x06001BE8 RID: 7144 RVA: 0x00068344 File Offset: 0x00066544
		private void AddName(string type_name)
		{
			if (this.name == null)
			{
				this.name = TypeSpec.ParsedTypeIdentifier(type_name);
				return;
			}
			if (this.nested == null)
			{
				this.nested = new List<TypeIdentifier>();
			}
			this.nested.Add(TypeSpec.ParsedTypeIdentifier(type_name));
		}

		// Token: 0x06001BE9 RID: 7145 RVA: 0x0006837F File Offset: 0x0006657F
		private void AddModifier(ModifierSpec md)
		{
			if (this.modifier_spec == null)
			{
				this.modifier_spec = new List<ModifierSpec>();
			}
			this.modifier_spec.Add(md);
		}

		// Token: 0x06001BEA RID: 7146 RVA: 0x000683A0 File Offset: 0x000665A0
		private static void SkipSpace(string name, ref int pos)
		{
			int num = pos;
			while (num < name.Length && char.IsWhiteSpace(name[num]))
			{
				num++;
			}
			pos = num;
		}

		// Token: 0x06001BEB RID: 7147 RVA: 0x000683D0 File Offset: 0x000665D0
		private static void BoundCheck(int idx, string s)
		{
			if (idx >= s.Length)
			{
				throw new ArgumentException("Invalid generic arguments spec", "typeName");
			}
		}

		// Token: 0x06001BEC RID: 7148 RVA: 0x000683EB File Offset: 0x000665EB
		private static TypeIdentifier ParsedTypeIdentifier(string displayName)
		{
			return TypeIdentifiers.FromDisplay(displayName);
		}

		// Token: 0x06001BED RID: 7149 RVA: 0x000683F4 File Offset: 0x000665F4
		private static TypeSpec Parse(string name, ref int p, bool is_recurse, bool allow_aqn)
		{
			int i = p;
			bool flag = false;
			TypeSpec typeSpec = new TypeSpec();
			TypeSpec.SkipSpace(name, ref i);
			int num = i;
			while (i < name.Length)
			{
				char c = name[i];
				switch (c)
				{
				case '&':
				case '*':
					goto IL_98;
				case '\'':
				case '(':
				case ')':
					break;
				case '+':
					typeSpec.AddName(name.Substring(num, i - num));
					num = i + 1;
					break;
				case ',':
					goto IL_77;
				default:
					switch (c)
					{
					case '[':
						goto IL_98;
					case '\\':
						i++;
						break;
					case ']':
						goto IL_77;
					}
					break;
				}
				IL_D6:
				if (!flag)
				{
					i++;
					continue;
				}
				break;
				IL_77:
				typeSpec.AddName(name.Substring(num, i - num));
				num = i + 1;
				flag = true;
				if (is_recurse && !allow_aqn)
				{
					p = i;
					return typeSpec;
				}
				goto IL_D6;
				IL_98:
				if (name[i] != '[' && is_recurse)
				{
					throw new ArgumentException("Generic argument can't be byref or pointer type", "typeName");
				}
				typeSpec.AddName(name.Substring(num, i - num));
				num = i + 1;
				flag = true;
				goto IL_D6;
			}
			if (num < i)
			{
				typeSpec.AddName(name.Substring(num, i - num));
			}
			else if (num == i)
			{
				typeSpec.AddName(string.Empty);
			}
			if (flag)
			{
				while (i < name.Length)
				{
					char c = name[i];
					if (c <= '*')
					{
						if (c != '&')
						{
							if (c != '*')
							{
								goto IL_4BE;
							}
							if (typeSpec.is_byref)
							{
								throw new ArgumentException("Can't have a pointer to a byref type", "typeName");
							}
							int num2 = 1;
							while (i + 1 < name.Length && name[i + 1] == '*')
							{
								i++;
								num2++;
							}
							typeSpec.AddModifier(new PointerSpec(num2));
						}
						else
						{
							if (typeSpec.is_byref)
							{
								throw new ArgumentException("Can't have a byref of a byref", "typeName");
							}
							typeSpec.is_byref = true;
						}
					}
					else if (c != ',')
					{
						if (c != '[')
						{
							if (c != ']')
							{
								goto IL_4BE;
							}
							if (is_recurse)
							{
								p = i;
								return typeSpec;
							}
							throw new ArgumentException("Unmatched ']'", "typeName");
						}
						else
						{
							if (typeSpec.is_byref)
							{
								throw new ArgumentException("Byref qualifier must be the last one of a type", "typeName");
							}
							i++;
							if (i >= name.Length)
							{
								throw new ArgumentException("Invalid array/generic spec", "typeName");
							}
							TypeSpec.SkipSpace(name, ref i);
							if (name[i] != ',' && name[i] != '*' && name[i] != ']')
							{
								List<TypeSpec> list = new List<TypeSpec>();
								if (typeSpec.HasModifiers)
								{
									throw new ArgumentException("generic args after array spec or pointer type", "typeName");
								}
								while (i < name.Length)
								{
									TypeSpec.SkipSpace(name, ref i);
									bool flag2 = name[i] == '[';
									if (flag2)
									{
										i++;
									}
									list.Add(TypeSpec.Parse(name, ref i, true, flag2));
									TypeSpec.BoundCheck(i, name);
									if (flag2)
									{
										if (name[i] != ']')
										{
											throw new ArgumentException("Unclosed assembly-qualified type name at " + name[i].ToString(), "typeName");
										}
										i++;
										TypeSpec.BoundCheck(i, name);
									}
									if (name[i] == ']')
									{
										break;
									}
									if (name[i] != ',')
									{
										throw new ArgumentException("Invalid generic arguments separator " + name[i].ToString(), "typeName");
									}
									i++;
								}
								if (i >= name.Length || name[i] != ']')
								{
									throw new ArgumentException("Error parsing generic params spec", "typeName");
								}
								typeSpec.generic_params = list;
							}
							else
							{
								int num3 = 1;
								bool flag3 = false;
								while (i < name.Length && name[i] != ']')
								{
									if (name[i] == '*')
									{
										if (flag3)
										{
											throw new ArgumentException("Array spec cannot have 2 bound dimensions", "typeName");
										}
										flag3 = true;
									}
									else
									{
										if (name[i] != ',')
										{
											throw new ArgumentException("Invalid character in array spec " + name[i].ToString(), "typeName");
										}
										num3++;
									}
									i++;
									TypeSpec.SkipSpace(name, ref i);
								}
								if (i >= name.Length || name[i] != ']')
								{
									throw new ArgumentException("Error parsing array spec", "typeName");
								}
								if (num3 > 1 && flag3)
								{
									throw new ArgumentException("Invalid array spec, multi-dimensional array cannot be bound", "typeName");
								}
								typeSpec.AddModifier(new ArraySpec(num3, flag3));
							}
						}
					}
					else if (is_recurse && allow_aqn)
					{
						int num4 = i;
						while (num4 < name.Length && name[num4] != ']')
						{
							num4++;
						}
						if (num4 >= name.Length)
						{
							throw new ArgumentException("Unmatched ']' while parsing generic argument assembly name");
						}
						typeSpec.assembly_name = name.Substring(i + 1, num4 - i - 1).Trim();
						p = num4;
						return typeSpec;
					}
					else
					{
						if (is_recurse)
						{
							p = i;
							return typeSpec;
						}
						if (allow_aqn)
						{
							typeSpec.assembly_name = name.Substring(i + 1).Trim();
							i = name.Length;
						}
					}
					i++;
					continue;
					IL_4BE:
					throw new ArgumentException("Bad type def, can't handle '" + name[i].ToString() + "' at " + i.ToString(), "typeName");
				}
			}
			p = i;
			return typeSpec;
		}

		// Token: 0x06001BEE RID: 7150 RVA: 0x00068904 File Offset: 0x00066B04
		internal TypeName TypeNameWithoutModifiers()
		{
			return new TypeSpec.TypeSpecTypeName(this, false);
		}

		// Token: 0x1700033E RID: 830
		// (get) Token: 0x06001BEF RID: 7151 RVA: 0x0006890D File Offset: 0x00066B0D
		internal TypeName TypeName
		{
			get
			{
				return new TypeSpec.TypeSpecTypeName(this, true);
			}
		}

		// Token: 0x06001BF0 RID: 7152 RVA: 0x0000259F File Offset: 0x0000079F
		public TypeSpec()
		{
		}

		// Token: 0x04001996 RID: 6550
		private TypeIdentifier name;

		// Token: 0x04001997 RID: 6551
		private string assembly_name;

		// Token: 0x04001998 RID: 6552
		private List<TypeIdentifier> nested;

		// Token: 0x04001999 RID: 6553
		private List<TypeSpec> generic_params;

		// Token: 0x0400199A RID: 6554
		private List<ModifierSpec> modifier_spec;

		// Token: 0x0400199B RID: 6555
		private bool is_byref;

		// Token: 0x0400199C RID: 6556
		private string display_fullname;

		// Token: 0x02000263 RID: 611
		[Flags]
		internal enum DisplayNameFormat
		{
			// Token: 0x0400199E RID: 6558
			Default = 0,
			// Token: 0x0400199F RID: 6559
			WANT_ASSEMBLY = 1,
			// Token: 0x040019A0 RID: 6560
			NO_MODIFIERS = 2
		}

		// Token: 0x02000264 RID: 612
		private class TypeSpecTypeName : TypeNames.ATypeName, TypeName, IEquatable<TypeName>
		{
			// Token: 0x06001BF1 RID: 7153 RVA: 0x00068916 File Offset: 0x00066B16
			internal TypeSpecTypeName(TypeSpec ts, bool wantModifiers)
			{
				this.ts = ts;
				this.want_modifiers = wantModifiers;
			}

			// Token: 0x1700033F RID: 831
			// (get) Token: 0x06001BF2 RID: 7154 RVA: 0x0006892C File Offset: 0x00066B2C
			public override string DisplayName
			{
				get
				{
					if (this.want_modifiers)
					{
						return this.ts.DisplayFullName;
					}
					return this.ts.GetDisplayFullName(TypeSpec.DisplayNameFormat.NO_MODIFIERS);
				}
			}

			// Token: 0x06001BF3 RID: 7155 RVA: 0x00067B51 File Offset: 0x00065D51
			public override TypeName NestedName(TypeIdentifier innerName)
			{
				return TypeNames.FromDisplay(this.DisplayName + "+" + innerName.DisplayName);
			}

			// Token: 0x040019A1 RID: 6561
			private TypeSpec ts;

			// Token: 0x040019A2 RID: 6562
			private bool want_modifiers;
		}
	}
}
