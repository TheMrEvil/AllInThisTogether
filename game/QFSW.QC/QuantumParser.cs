using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using QFSW.QC.Utilities;
using UnityEngine;

namespace QFSW.QC
{
	// Token: 0x02000027 RID: 39
	public class QuantumParser
	{
		// Token: 0x0600008D RID: 141 RVA: 0x00003558 File Offset: 0x00001758
		public QuantumParser(IEnumerable<IQcParser> parsers, IEnumerable<IQcGrammarConstruct> grammarConstructs)
		{
			this._recursiveParser = new Func<string, Type, object>(this.Parse);
			this._parsers = (from x in parsers
			orderby x.Priority descending
			select x).ToArray<IQcParser>();
			this._grammarConstructs = (from x in grammarConstructs
			orderby x.Precedence
			select x).ToArray<IQcGrammarConstruct>();
		}

		// Token: 0x0600008E RID: 142 RVA: 0x000035F3 File Offset: 0x000017F3
		public QuantumParser() : this(new InjectionLoader<IQcParser>().GetInjectedInstances(false), new InjectionLoader<IQcGrammarConstruct>().GetInjectedInstances(false))
		{
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00003614 File Offset: 0x00001814
		public IQcParser GetParser(Type type)
		{
			if (this._parserLookup.ContainsKey(type))
			{
				return this._parserLookup[type];
			}
			if (!this._unparseableLookup.Contains(type))
			{
				foreach (IQcParser qcParser in this._parsers)
				{
					try
					{
						if (qcParser.CanParse(type))
						{
							return this._parserLookup[type] = qcParser;
						}
					}
					catch (Exception exception)
					{
						Debug.LogError(qcParser.GetType().GetDisplayName(false) + ".CanParse is malformed and throws");
						Debug.LogException(exception);
					}
				}
				this._unparseableLookup.Add(type);
			}
			return null;
		}

		// Token: 0x06000090 RID: 144 RVA: 0x000036C4 File Offset: 0x000018C4
		public bool CanParse(Type type)
		{
			return this.GetParser(type) != null;
		}

		// Token: 0x06000091 RID: 145 RVA: 0x000036D0 File Offset: 0x000018D0
		private IQcGrammarConstruct GetMatchingGrammar(string value, Type type)
		{
			foreach (IQcGrammarConstruct qcGrammarConstruct in this._grammarConstructs)
			{
				try
				{
					if (qcGrammarConstruct.Match(value, type))
					{
						return qcGrammarConstruct;
					}
				}
				catch (Exception exception)
				{
					Debug.LogError(qcGrammarConstruct.GetType().GetDisplayName(false) + ".Match is malformed and throws");
					Debug.LogException(exception);
				}
			}
			return null;
		}

		// Token: 0x06000092 RID: 146 RVA: 0x0000373C File Offset: 0x0000193C
		public T Parse<T>(string value)
		{
			return (T)((object)this.Parse(value, typeof(T)));
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00003754 File Offset: 0x00001954
		public object Parse(string value, Type type)
		{
			value = value.ReduceScope('(', ')');
			if (type.IsClass && value == "null")
			{
				return null;
			}
			IQcGrammarConstruct matchingGrammar = this.GetMatchingGrammar(value, type);
			if (matchingGrammar != null)
			{
				try
				{
					return matchingGrammar.Parse(value, type, this._recursiveParser);
				}
				catch (ParserException)
				{
					throw;
				}
				catch (Exception ex)
				{
					throw new Exception(string.Format("Parsing of {0} via {1} failed:\n{2}", type.GetDisplayName(false), matchingGrammar, ex.Message), ex);
				}
			}
			IQcParser parser = this.GetParser(type);
			if (parser == null)
			{
				throw new ArgumentException("Cannot parse object of type '" + type.GetDisplayName(false) + "'");
			}
			object result;
			try
			{
				result = parser.Parse(value, type, this._recursiveParser);
			}
			catch (ParserException)
			{
				throw;
			}
			catch (Exception ex2)
			{
				throw new Exception(string.Format("Parsing of {0} via {1} failed:\n{2}", type.GetDisplayName(false), parser, ex2.Message), ex2);
			}
			return result;
		}

		// Token: 0x06000094 RID: 148 RVA: 0x0000385C File Offset: 0x00001A5C
		public static void ResetNamespaceTable()
		{
			QuantumParser._namespaceTable.Clear();
			QuantumParser._namespaceTable.AddRange(QuantumParser._defaultNamespaces);
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00003877 File Offset: 0x00001A77
		public static void AddNamespace(string namespaceName)
		{
			if (!QuantumParser._namespaceTable.Contains(namespaceName))
			{
				QuantumParser._namespaceTable.Add(namespaceName);
			}
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00003891 File Offset: 0x00001A91
		public static void RemoveNamespace(string namespaceName)
		{
			if (QuantumParser._namespaceTable.Contains(namespaceName))
			{
				QuantumParser._namespaceTable.Remove(namespaceName);
				return;
			}
			throw new ArgumentException("No namespace named " + namespaceName + " was present in the table");
		}

		// Token: 0x06000097 RID: 151 RVA: 0x000038C2 File Offset: 0x00001AC2
		private static string ShowNamespaces()
		{
			QuantumParser._namespaceTable.Sort();
			if (QuantumParser._namespaceTable.Count == 0)
			{
				return "Namespace table is empty";
			}
			return string.Join("\n", QuantumParser._namespaceTable);
		}

		// Token: 0x06000098 RID: 152 RVA: 0x000038EF File Offset: 0x00001AEF
		public static IEnumerable<string> GetAllNamespaces()
		{
			return QuantumParser._namespaceTable;
		}

		// Token: 0x06000099 RID: 153 RVA: 0x000038F8 File Offset: 0x00001AF8
		public static Type ParseType(string typeName)
		{
			typeName = typeName.Trim();
			if (QuantumParser._reverseTypeDisplayNames.ContainsKey(typeName))
			{
				return QuantumParser._reverseTypeDisplayNames[typeName];
			}
			if (QuantumParser._tupleTypeRegex.IsMatch(typeName))
			{
				return QuantumParser.ParseTupleType(typeName);
			}
			if (QuantumParser._arrayTypeRegex.IsMatch(typeName))
			{
				return QuantumParser.ParseArrayType(typeName);
			}
			if (QuantumParser._genericTypeRegex.IsMatch(typeName))
			{
				return QuantumParser.ParseGenericType(typeName);
			}
			if (QuantumParser._nullableTypeRegex.IsMatch(typeName))
			{
				return QuantumParser.ParseNullableType(typeName);
			}
			if (typeName.Contains('`'))
			{
				string key = typeName.Split('`', StringSplitOptions.None)[0];
				if (QuantumParser._reverseTypeDisplayNames.ContainsKey(key))
				{
					return QuantumParser._reverseTypeDisplayNames[key];
				}
			}
			return QuantumParser.ParseTypeBaseCase(typeName);
		}

		// Token: 0x0600009A RID: 154 RVA: 0x000039AC File Offset: 0x00001BAC
		private static Type ParseArrayType(string typeName)
		{
			int num = typeName.LastIndexOf('[');
			int num2 = typeName.CountFromIndex(',', num) + 1;
			Type type = QuantumParser.ParseType(typeName.Substring(0, num));
			if (num2 <= 1)
			{
				return type.MakeArrayType();
			}
			return type.MakeArrayType(num2);
		}

		// Token: 0x0600009B RID: 155 RVA: 0x000039F0 File Offset: 0x00001BF0
		private static Type ParseGenericType(string typeName)
		{
			string[] array = typeName.Split(new char[]
			{
				'<'
			}, 2);
			string[] array2 = ("<" + array[1]).ReduceScope('<', '>').SplitScoped(',');
			Type type = QuantumParser.ParseType(string.Format("{0}`{1}", array[0], Math.Max(1, array2.Length)));
			if (array2.All(new Func<string, bool>(string.IsNullOrWhiteSpace)))
			{
				return type;
			}
			Type[] typeArguments = array2.Select(new Func<string, Type>(QuantumParser.ParseType)).ToArray<Type>();
			return type.MakeGenericType(typeArguments);
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00003A84 File Offset: 0x00001C84
		private static Type ParseNullableType(string typeName)
		{
			Type type = QuantumParser.ParseType(typeName.Substring(0, typeName.Length - 1));
			if (!type.IsClass)
			{
				return typeof(Nullable<>).MakeGenericType(new Type[]
				{
					type
				});
			}
			return type;
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00003AC9 File Offset: 0x00001CC9
		private static Type ParseTupleType(string typeName)
		{
			return QuantumParser.CreateTupleType(typeName.Substring(1, typeName.Length - 2).SplitScoped(',').Select(new Func<string, Type>(QuantumParser.ParseType)).ToArray<Type>());
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00003AFC File Offset: 0x00001CFC
		private static Type CreateTupleType(Type[] types)
		{
			if (types.Length > 7)
			{
				Type[] types2 = types.Skip(7).ToArray<Type>();
				types = types.Take(7).Concat(QuantumParser.CreateTupleType(types2).Yield<Type>()).ToArray<Type>();
			}
			return QuantumParser._valueTupleTypes[types.Length - 1].MakeGenericType(types);
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00003B4C File Offset: 0x00001D4C
		private static Type ParseTypeBaseCase(string typeName)
		{
			Type result;
			if ((result = QuantumParser.GetTypeFromAssemblies(typeName, QuantumParser._loadedAssemblies, false, false)) == null && (result = QuantumParser.GetTypeFromAssemblies(typeName, QuantumParser._namespaceTable, QuantumParser._loadedAssemblies, false, false)) == null)
			{
				result = (QuantumParser.GetTypeFromAssemblies(typeName, QuantumParser._loadedAssemblies, false, true) ?? QuantumParser.GetTypeFromAssemblies(typeName, QuantumParser._namespaceTable, QuantumParser._loadedAssemblies, true, true));
			}
			return result;
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00003BA4 File Offset: 0x00001DA4
		private static Type GetTypeFromAssemblies(string typeName, IEnumerable<string> namespaces, IEnumerable<Assembly> assemblies, bool throwOnError, bool ignoreCase)
		{
			foreach (string str in namespaces)
			{
				Type typeFromAssemblies = QuantumParser.GetTypeFromAssemblies(str + "." + typeName, assemblies, false, ignoreCase);
				if (typeFromAssemblies != null)
				{
					return typeFromAssemblies;
				}
			}
			if (throwOnError)
			{
				throw new TypeLoadException("No type of name '" + typeName + "' could be found in the specified assemblies and namespaces.");
			}
			return null;
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00003C24 File Offset: 0x00001E24
		private static Type GetTypeFromAssemblies(string typeName, IEnumerable<Assembly> assemblies, bool throwOnError, bool ignoreCase)
		{
			foreach (Assembly assembly in assemblies)
			{
				Type type = Type.GetType(typeName + ", " + assembly.FullName, false, ignoreCase);
				if (type != null)
				{
					return type;
				}
			}
			if (throwOnError)
			{
				throw new TypeLoadException("No type of name '" + typeName + "' could be found in the specified assemblies.");
			}
			return null;
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00003CA8 File Offset: 0x00001EA8
		// Note: this type is marked as 'beforefieldinit'.
		static QuantumParser()
		{
		}

		// Token: 0x04000046 RID: 70
		private readonly IQcParser[] _parsers;

		// Token: 0x04000047 RID: 71
		private readonly IQcGrammarConstruct[] _grammarConstructs;

		// Token: 0x04000048 RID: 72
		private readonly ConcurrentDictionary<Type, IQcParser> _parserLookup = new ConcurrentDictionary<Type, IQcParser>();

		// Token: 0x04000049 RID: 73
		private readonly HashSet<Type> _unparseableLookup = new HashSet<Type>();

		// Token: 0x0400004A RID: 74
		private readonly Func<string, Type, object> _recursiveParser;

		// Token: 0x0400004B RID: 75
		private static readonly Dictionary<Type, string> _typeDisplayNames = new Dictionary<Type, string>
		{
			{
				typeof(int),
				"int"
			},
			{
				typeof(float),
				"float"
			},
			{
				typeof(decimal),
				"decimal"
			},
			{
				typeof(double),
				"double"
			},
			{
				typeof(string),
				"string"
			},
			{
				typeof(bool),
				"bool"
			},
			{
				typeof(byte),
				"byte"
			},
			{
				typeof(sbyte),
				"sbyte"
			},
			{
				typeof(uint),
				"uint"
			},
			{
				typeof(short),
				"short"
			},
			{
				typeof(ushort),
				"ushort"
			},
			{
				typeof(long),
				"long"
			},
			{
				typeof(ulong),
				"ulong"
			},
			{
				typeof(char),
				"char"
			},
			{
				typeof(object),
				"object"
			}
		};

		// Token: 0x0400004C RID: 76
		private static readonly Dictionary<string, Type> _reverseTypeDisplayNames = QuantumParser._typeDisplayNames.Invert<Type, string>();

		// Token: 0x0400004D RID: 77
		private static readonly Assembly[] _loadedAssemblies = AppDomain.CurrentDomain.GetAssemblies();

		// Token: 0x0400004E RID: 78
		private static readonly string[] _defaultNamespaces = new string[]
		{
			"System",
			"System.Collections",
			"System.Collections.Generic",
			"UnityEngine",
			"UnityEngine.UI",
			"QFSW.QC"
		};

		// Token: 0x0400004F RID: 79
		private static readonly List<string> _namespaceTable = new List<string>(QuantumParser._defaultNamespaces);

		// Token: 0x04000050 RID: 80
		private static readonly Regex _arrayTypeRegex = new Regex("^.*\\[,*\\]$");

		// Token: 0x04000051 RID: 81
		private static readonly Regex _genericTypeRegex = new Regex("^.+<.*>$");

		// Token: 0x04000052 RID: 82
		private static readonly Regex _tupleTypeRegex = new Regex("^\\(.*\\)$");

		// Token: 0x04000053 RID: 83
		private static readonly Regex _nullableTypeRegex = new Regex("^.*\\?$");

		// Token: 0x04000054 RID: 84
		private static readonly Type[] _valueTupleTypes = new Type[]
		{
			typeof(ValueTuple<>),
			typeof(ValueTuple<, >),
			typeof(ValueTuple<, , >),
			typeof(ValueTuple<, , , >),
			typeof(ValueTuple<, , , , >),
			typeof(ValueTuple<, , , , , >),
			typeof(ValueTuple<, , , , , , >),
			typeof(ValueTuple<, , , , , , , >)
		};

		// Token: 0x0200008A RID: 138
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x060002C0 RID: 704 RVA: 0x0000B003 File Offset: 0x00009203
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x060002C1 RID: 705 RVA: 0x0000B00F File Offset: 0x0000920F
			public <>c()
			{
			}

			// Token: 0x060002C2 RID: 706 RVA: 0x0000B017 File Offset: 0x00009217
			internal int <.ctor>b__5_0(IQcParser x)
			{
				return x.Priority;
			}

			// Token: 0x060002C3 RID: 707 RVA: 0x0000B01F File Offset: 0x0000921F
			internal int <.ctor>b__5_1(IQcGrammarConstruct x)
			{
				return x.Precedence;
			}

			// Token: 0x04000193 RID: 403
			public static readonly QuantumParser.<>c <>9 = new QuantumParser.<>c();

			// Token: 0x04000194 RID: 404
			public static Func<IQcParser, int> <>9__5_0;

			// Token: 0x04000195 RID: 405
			public static Func<IQcGrammarConstruct, int> <>9__5_1;
		}
	}
}
