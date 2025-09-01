using System;
using System.Text;

namespace IKVM.Reflection
{
	// Token: 0x0200006A RID: 106
	internal struct TypeNameParser
	{
		// Token: 0x060005F5 RID: 1525 RVA: 0x00011AE4 File Offset: 0x0000FCE4
		internal static string Escape(string name)
		{
			if (name == null)
			{
				return null;
			}
			StringBuilder stringBuilder = null;
			int i = 0;
			while (i < name.Length)
			{
				char value = name[i];
				switch (value)
				{
				case '&':
				case '*':
				case '+':
				case ',':
					goto IL_4F;
				case '\'':
				case '(':
				case ')':
					goto IL_77;
				default:
					switch (value)
					{
					case '[':
					case '\\':
					case ']':
						goto IL_4F;
					default:
						goto IL_77;
					}
					break;
				}
				IL_82:
				i++;
				continue;
				IL_4F:
				if (stringBuilder == null)
				{
					stringBuilder = new StringBuilder(name, 0, i, name.Length + 3);
				}
				stringBuilder.Append("\\").Append(value);
				goto IL_82;
				IL_77:
				if (stringBuilder != null)
				{
					stringBuilder.Append(value);
					goto IL_82;
				}
				goto IL_82;
			}
			if (stringBuilder == null)
			{
				return name;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060005F6 RID: 1526 RVA: 0x00011B90 File Offset: 0x0000FD90
		internal static string Unescape(string name)
		{
			int i = name.IndexOf('\\');
			if (i == -1)
			{
				return name;
			}
			StringBuilder stringBuilder = new StringBuilder(name, 0, i, name.Length - 1);
			while (i < name.Length)
			{
				char c = name[i];
				if (c == '\\')
				{
					c = name[++i];
				}
				stringBuilder.Append(c);
				i++;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060005F7 RID: 1527 RVA: 0x00011BF4 File Offset: 0x0000FDF4
		internal static TypeNameParser Parse(string typeName, bool throwOnError)
		{
			if (throwOnError)
			{
				TypeNameParser.Parser parser = new TypeNameParser.Parser(typeName);
				return new TypeNameParser(ref parser, true);
			}
			TypeNameParser result;
			try
			{
				TypeNameParser.Parser parser2 = new TypeNameParser.Parser(typeName);
				result = new TypeNameParser(ref parser2, true);
			}
			catch (ArgumentException)
			{
				result = default(TypeNameParser);
			}
			return result;
		}

		// Token: 0x060005F8 RID: 1528 RVA: 0x00011C48 File Offset: 0x0000FE48
		private TypeNameParser(ref TypeNameParser.Parser parser, bool withAssemblyName)
		{
			bool genericParameter = parser.pos != 0;
			this.name = parser.NextNamePart();
			this.nested = null;
			parser.ParseNested(ref this.nested);
			this.genericParameters = null;
			parser.ParseGenericParameters(ref this.genericParameters);
			this.modifiers = null;
			parser.ParseModifiers(ref this.modifiers);
			this.assemblyName = null;
			if (withAssemblyName)
			{
				parser.ParseAssemblyName(genericParameter, ref this.assemblyName);
			}
		}

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x060005F9 RID: 1529 RVA: 0x00011CBB File Offset: 0x0000FEBB
		internal bool Error
		{
			get
			{
				return this.name == null;
			}
		}

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x060005FA RID: 1530 RVA: 0x00011CC6 File Offset: 0x0000FEC6
		internal string FirstNamePart
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x060005FB RID: 1531 RVA: 0x00011CCE File Offset: 0x0000FECE
		internal string AssemblyName
		{
			get
			{
				return this.assemblyName;
			}
		}

		// Token: 0x060005FC RID: 1532 RVA: 0x00011CD8 File Offset: 0x0000FED8
		internal Type GetType(Universe universe, Module context, bool throwOnError, string originalName, bool resolve, bool ignoreCase)
		{
			TypeName typeName = TypeName.Split(this.name);
			Type type;
			if (this.assemblyName != null)
			{
				Assembly assembly = universe.Load(this.assemblyName, context, throwOnError);
				if (assembly == null)
				{
					return null;
				}
				if (resolve)
				{
					type = assembly.ResolveType(context, typeName);
				}
				else if (ignoreCase)
				{
					type = assembly.FindTypeIgnoreCase(typeName.ToLowerInvariant());
				}
				else
				{
					type = assembly.FindType(typeName);
				}
			}
			else if (context == null)
			{
				if (resolve)
				{
					type = universe.Mscorlib.ResolveType(context, typeName);
				}
				else if (ignoreCase)
				{
					type = universe.Mscorlib.FindTypeIgnoreCase(typeName.ToLowerInvariant());
				}
				else
				{
					type = universe.Mscorlib.FindType(typeName);
				}
			}
			else
			{
				if (ignoreCase)
				{
					typeName = typeName.ToLowerInvariant();
					type = context.FindTypeIgnoreCase(typeName);
				}
				else
				{
					type = context.FindType(typeName);
				}
				if (type == null && context != universe.Mscorlib.ManifestModule)
				{
					if (ignoreCase)
					{
						type = universe.Mscorlib.FindTypeIgnoreCase(typeName);
					}
					else
					{
						type = universe.Mscorlib.FindType(typeName);
					}
				}
				if (type == null && resolve)
				{
					if (universe.Mscorlib.__IsMissing && !context.__IsMissing)
					{
						type = universe.Mscorlib.ResolveType(context, typeName);
					}
					else
					{
						type = context.Assembly.ResolveType(context, typeName);
					}
				}
			}
			return this.Expand(type, context, throwOnError, originalName, resolve, ignoreCase);
		}

		// Token: 0x060005FD RID: 1533 RVA: 0x00011E30 File Offset: 0x00010030
		internal Type Expand(Type type, Module context, bool throwOnError, string originalName, bool resolve, bool ignoreCase)
		{
			if (!(type == null))
			{
				if (this.nested != null)
				{
					foreach (string text in this.nested)
					{
						Type type2 = type;
						TypeName typeName = TypeName.Split(TypeNameParser.Unescape(text));
						type = (ignoreCase ? type2.FindNestedTypeIgnoreCase(typeName.ToLowerInvariant()) : type2.FindNestedType(typeName));
						if (type == null)
						{
							if (resolve)
							{
								type = type2.Module.universe.GetMissingTypeOrThrow(context, type2.Module, type2, typeName);
							}
							else
							{
								if (throwOnError)
								{
									throw new TypeLoadException(originalName);
								}
								return null;
							}
						}
					}
				}
				if (this.genericParameters != null)
				{
					Type[] array2 = new Type[this.genericParameters.Length];
					for (int j = 0; j < array2.Length; j++)
					{
						array2[j] = this.genericParameters[j].GetType(type.Assembly.universe, context, throwOnError, originalName, resolve, ignoreCase);
						if (array2[j] == null)
						{
							return null;
						}
					}
					type = type.MakeGenericType(array2);
				}
				if (this.modifiers != null)
				{
					foreach (short rank in this.modifiers)
					{
						switch (rank)
						{
						case -3:
							type = type.MakePointerType();
							break;
						case -2:
							type = type.MakeByRefType();
							break;
						case -1:
							type = type.MakeArrayType();
							break;
						default:
							type = type.MakeArrayType((int)rank);
							break;
						}
					}
				}
				return type;
			}
			if (throwOnError)
			{
				throw new TypeLoadException(originalName);
			}
			return null;
		}

		// Token: 0x04000213 RID: 531
		private const string SpecialChars = "\\+,[]*&";

		// Token: 0x04000214 RID: 532
		private const short SZARRAY = -1;

		// Token: 0x04000215 RID: 533
		private const short BYREF = -2;

		// Token: 0x04000216 RID: 534
		private const short POINTER = -3;

		// Token: 0x04000217 RID: 535
		private readonly string name;

		// Token: 0x04000218 RID: 536
		private readonly string[] nested;

		// Token: 0x04000219 RID: 537
		private readonly string assemblyName;

		// Token: 0x0400021A RID: 538
		private readonly short[] modifiers;

		// Token: 0x0400021B RID: 539
		private readonly TypeNameParser[] genericParameters;

		// Token: 0x02000336 RID: 822
		private struct Parser
		{
			// Token: 0x060025C7 RID: 9671 RVA: 0x000B40DC File Offset: 0x000B22DC
			internal Parser(string typeName)
			{
				this.typeName = typeName;
				this.pos = 0;
			}

			// Token: 0x060025C8 RID: 9672 RVA: 0x000B40EC File Offset: 0x000B22EC
			private void Check(bool condition)
			{
				if (!condition)
				{
					throw new ArgumentException("Invalid type name '" + this.typeName + "'");
				}
			}

			// Token: 0x060025C9 RID: 9673 RVA: 0x000B410C File Offset: 0x000B230C
			private void Consume(char c)
			{
				bool condition;
				if (this.pos < this.typeName.Length)
				{
					string text = this.typeName;
					int num = this.pos;
					this.pos = num + 1;
					condition = (text[num] == c);
				}
				else
				{
					condition = false;
				}
				this.Check(condition);
			}

			// Token: 0x060025CA RID: 9674 RVA: 0x000B4154 File Offset: 0x000B2354
			private bool TryConsume(char c)
			{
				if (this.pos < this.typeName.Length && this.typeName[this.pos] == c)
				{
					this.pos++;
					return true;
				}
				return false;
			}

			// Token: 0x060025CB RID: 9675 RVA: 0x000B4190 File Offset: 0x000B2390
			internal string NextNamePart()
			{
				this.SkipWhiteSpace();
				int num = this.pos;
				while (this.pos < this.typeName.Length)
				{
					char c = this.typeName[this.pos];
					if (c == '\\')
					{
						this.pos++;
						this.Check(this.pos < this.typeName.Length && "\\+,[]*&".IndexOf(this.typeName[this.pos]) != -1);
					}
					else if ("\\+,[]*&".IndexOf(c) != -1)
					{
						break;
					}
					this.pos++;
				}
				this.Check(this.pos - num != 0);
				if (num == 0 && this.pos == this.typeName.Length)
				{
					return this.typeName;
				}
				return this.typeName.Substring(num, this.pos - num);
			}

			// Token: 0x060025CC RID: 9676 RVA: 0x000B4287 File Offset: 0x000B2487
			internal void ParseNested(ref string[] nested)
			{
				while (this.TryConsume('+'))
				{
					TypeNameParser.Parser.Add<string>(ref nested, this.NextNamePart());
				}
			}

			// Token: 0x060025CD RID: 9677 RVA: 0x000B42A4 File Offset: 0x000B24A4
			internal void ParseGenericParameters(ref TypeNameParser[] genericParameters)
			{
				int num = this.pos;
				if (this.TryConsume('['))
				{
					this.SkipWhiteSpace();
					if (this.TryConsume(']') || this.TryConsume('*') || this.TryConsume(','))
					{
						this.pos = num;
						return;
					}
					do
					{
						this.SkipWhiteSpace();
						if (this.TryConsume('['))
						{
							TypeNameParser.Parser.Add<TypeNameParser>(ref genericParameters, new TypeNameParser(ref this, true));
							this.Consume(']');
						}
						else
						{
							TypeNameParser.Parser.Add<TypeNameParser>(ref genericParameters, new TypeNameParser(ref this, false));
						}
					}
					while (this.TryConsume(','));
					this.Consume(']');
					this.SkipWhiteSpace();
				}
			}

			// Token: 0x060025CE RID: 9678 RVA: 0x000B433C File Offset: 0x000B253C
			internal void ParseModifiers(ref short[] modifiers)
			{
				while (this.pos < this.typeName.Length)
				{
					char c = this.typeName[this.pos];
					if (c != '&')
					{
						if (c != '*')
						{
							if (c != '[')
							{
								return;
							}
							this.pos++;
							TypeNameParser.Parser.Add<short>(ref modifiers, this.ParseArray());
							this.Consume(']');
						}
						else
						{
							this.pos++;
							TypeNameParser.Parser.Add<short>(ref modifiers, -3);
						}
					}
					else
					{
						this.pos++;
						TypeNameParser.Parser.Add<short>(ref modifiers, -2);
					}
					this.SkipWhiteSpace();
				}
			}

			// Token: 0x060025CF RID: 9679 RVA: 0x000B43DC File Offset: 0x000B25DC
			internal void ParseAssemblyName(bool genericParameter, ref string assemblyName)
			{
				if (this.pos < this.typeName.Length)
				{
					if (this.typeName[this.pos] != ']' || !genericParameter)
					{
						this.Consume(',');
						this.SkipWhiteSpace();
						if (genericParameter)
						{
							int num = this.pos;
							while (this.pos < this.typeName.Length)
							{
								char c = this.typeName[this.pos];
								if (c == '\\')
								{
									this.pos++;
									bool condition;
									if (this.pos < this.typeName.Length)
									{
										string text = this.typeName;
										int num2 = this.pos;
										this.pos = num2 + 1;
										condition = (text[num2] == ']');
									}
									else
									{
										condition = false;
									}
									this.Check(condition);
								}
								else
								{
									if (c == ']')
									{
										break;
									}
									this.pos++;
								}
							}
							this.Check(this.pos < this.typeName.Length && this.typeName[this.pos] == ']');
							assemblyName = this.typeName.Substring(num, this.pos - num).Replace("\\]", "]");
						}
						else
						{
							assemblyName = this.typeName.Substring(this.pos);
						}
						this.Check(assemblyName.Length != 0);
						return;
					}
				}
				else
				{
					this.Check(!genericParameter);
				}
			}

			// Token: 0x060025D0 RID: 9680 RVA: 0x000B454C File Offset: 0x000B274C
			private short ParseArray()
			{
				this.SkipWhiteSpace();
				this.Check(this.pos < this.typeName.Length);
				char c = this.typeName[this.pos];
				if (c == ']')
				{
					return -1;
				}
				if (c == '*')
				{
					this.pos++;
					this.SkipWhiteSpace();
					return 1;
				}
				short num = 1;
				while (this.TryConsume(','))
				{
					this.Check(num < short.MaxValue);
					num += 1;
					this.SkipWhiteSpace();
				}
				return num;
			}

			// Token: 0x060025D1 RID: 9681 RVA: 0x000B45D4 File Offset: 0x000B27D4
			private void SkipWhiteSpace()
			{
				while (this.pos < this.typeName.Length && char.IsWhiteSpace(this.typeName[this.pos]))
				{
					this.pos++;
				}
			}

			// Token: 0x060025D2 RID: 9682 RVA: 0x000B4611 File Offset: 0x000B2811
			private static void Add<T>(ref T[] array, T elem)
			{
				if (array == null)
				{
					array = new T[]
					{
						elem
					};
					return;
				}
				Array.Resize<T>(ref array, array.Length + 1);
				T[] array2 = array;
				array2[array2.Length - 1] = elem;
			}

			// Token: 0x04000E72 RID: 3698
			private readonly string typeName;

			// Token: 0x04000E73 RID: 3699
			internal int pos;
		}
	}
}
