using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;

namespace Parse.Infrastructure.Utilities
{
	// Token: 0x02000056 RID: 86
	public class JsonUtilities
	{
		// Token: 0x06000437 RID: 1079 RVA: 0x0000D2A4 File Offset: 0x0000B4A4
		public static object Parse(string input)
		{
			input = input.Trim();
			JsonUtilities.JsonStringParser jsonStringParser = new JsonUtilities.JsonStringParser(input);
			object result;
			if ((jsonStringParser.ParseObject(out result) || jsonStringParser.ParseArray(out result)) && jsonStringParser.CurrentIndex == input.Length)
			{
				return result;
			}
			throw new ArgumentException("Input JSON was invalid.");
		}

		// Token: 0x06000438 RID: 1080 RVA: 0x0000D2F0 File Offset: 0x0000B4F0
		public static string Encode(IDictionary<string, object> dict)
		{
			if (dict == null)
			{
				throw new ArgumentNullException();
			}
			if (dict.Count == 0)
			{
				return "{}";
			}
			StringBuilder stringBuilder = new StringBuilder("{");
			foreach (KeyValuePair<string, object> keyValuePair in dict)
			{
				stringBuilder.Append(JsonUtilities.Encode(keyValuePair.Key));
				stringBuilder.Append(":");
				stringBuilder.Append(JsonUtilities.Encode(keyValuePair.Value));
				stringBuilder.Append(",");
			}
			stringBuilder[stringBuilder.Length - 1] = '}';
			return stringBuilder.ToString();
		}

		// Token: 0x06000439 RID: 1081 RVA: 0x0000D3A8 File Offset: 0x0000B5A8
		public static string Encode(IList<object> list)
		{
			if (list == null)
			{
				throw new ArgumentNullException();
			}
			if (list.Count == 0)
			{
				return "[]";
			}
			StringBuilder stringBuilder = new StringBuilder("[");
			foreach (object obj in list)
			{
				stringBuilder.Append(JsonUtilities.Encode(obj));
				stringBuilder.Append(",");
			}
			stringBuilder[stringBuilder.Length - 1] = ']';
			return stringBuilder.ToString();
		}

		// Token: 0x0600043A RID: 1082 RVA: 0x0000D43C File Offset: 0x0000B63C
		public static string Encode(object obj)
		{
			IDictionary<string, object> dictionary = obj as IDictionary<string, object>;
			if (dictionary != null)
			{
				return JsonUtilities.Encode(dictionary);
			}
			IList<object> list = obj as IList<object>;
			if (list != null)
			{
				return JsonUtilities.Encode(list);
			}
			string text = obj as string;
			if (text != null)
			{
				text = JsonUtilities.escapePattern.Replace(text, delegate(Match m)
				{
					char c = m.Value[0];
					switch (c)
					{
					case '\b':
						return "\\b";
					case '\t':
						return "\\t";
					case '\n':
						return "\\n";
					case '\v':
						break;
					case '\f':
						return "\\f";
					case '\r':
						return "\\r";
					default:
						if (c == '"')
						{
							return "\\\"";
						}
						if (c == '\\')
						{
							return "\\\\";
						}
						break;
					}
					return "\\u" + ((ushort)m.Value[0]).ToString("x4");
				});
				return "\"" + text + "\"";
			}
			if (obj == null)
			{
				return "null";
			}
			if (obj is bool)
			{
				if (!(bool)obj)
				{
					return "false";
				}
				return "true";
			}
			else
			{
				if (!obj.GetType().GetTypeInfo().IsPrimitive)
				{
					string str = "Unable to encode objects of type ";
					Type type = obj.GetType();
					throw new ArgumentException(str + ((type != null) ? type.ToString() : null));
				}
				return Convert.ToString(obj, CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x0600043B RID: 1083 RVA: 0x0000D515 File Offset: 0x0000B715
		public JsonUtilities()
		{
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x0000D520 File Offset: 0x0000B720
		// Note: this type is marked as 'beforefieldinit'.
		static JsonUtilities()
		{
		}

		// Token: 0x040000CA RID: 202
		private static readonly string startOfString = "\\G";

		// Token: 0x040000CB RID: 203
		private static readonly char startObject = '{';

		// Token: 0x040000CC RID: 204
		private static readonly char endObject = '}';

		// Token: 0x040000CD RID: 205
		private static readonly char startArray = '[';

		// Token: 0x040000CE RID: 206
		private static readonly char endArray = ']';

		// Token: 0x040000CF RID: 207
		private static readonly char valueSeparator = ',';

		// Token: 0x040000D0 RID: 208
		private static readonly char nameSeparator = ':';

		// Token: 0x040000D1 RID: 209
		private static readonly char[] falseValue = "false".ToCharArray();

		// Token: 0x040000D2 RID: 210
		private static readonly char[] trueValue = "true".ToCharArray();

		// Token: 0x040000D3 RID: 211
		private static readonly char[] nullValue = "null".ToCharArray();

		// Token: 0x040000D4 RID: 212
		private static readonly Regex numberValue = new Regex(JsonUtilities.startOfString + "-?(?:0|[1-9]\\d*)(?<frac>\\.\\d+)?(?<exp>(?:e|E)(?:-|\\+)?\\d+)?");

		// Token: 0x040000D5 RID: 213
		private static readonly Regex stringValue = new Regex(JsonUtilities.startOfString + "\"(?<content>(?:[^\\\\\"]|(?<escape>\\\\(?:[\\\\\"/bfnrt]|u[0-9a-fA-F]{4})))*)\"", RegexOptions.Multiline);

		// Token: 0x040000D6 RID: 214
		private static readonly Regex escapePattern = new Regex("\\\\|\"|[\0-\u001f]");

		// Token: 0x02000126 RID: 294
		private class JsonStringParser
		{
			// Token: 0x170001FA RID: 506
			// (get) Token: 0x06000777 RID: 1911 RVA: 0x00016995 File Offset: 0x00014B95
			// (set) Token: 0x06000778 RID: 1912 RVA: 0x0001699D File Offset: 0x00014B9D
			public string Input
			{
				[CompilerGenerated]
				get
				{
					return this.<Input>k__BackingField;
				}
				[CompilerGenerated]
				private set
				{
					this.<Input>k__BackingField = value;
				}
			}

			// Token: 0x170001FB RID: 507
			// (get) Token: 0x06000779 RID: 1913 RVA: 0x000169A6 File Offset: 0x00014BA6
			// (set) Token: 0x0600077A RID: 1914 RVA: 0x000169AE File Offset: 0x00014BAE
			public char[] InputAsArray
			{
				[CompilerGenerated]
				get
				{
					return this.<InputAsArray>k__BackingField;
				}
				[CompilerGenerated]
				private set
				{
					this.<InputAsArray>k__BackingField = value;
				}
			}

			// Token: 0x170001FC RID: 508
			// (get) Token: 0x0600077B RID: 1915 RVA: 0x000169B7 File Offset: 0x00014BB7
			// (set) Token: 0x0600077C RID: 1916 RVA: 0x000169BF File Offset: 0x00014BBF
			public int CurrentIndex
			{
				[CompilerGenerated]
				get
				{
					return this.<CurrentIndex>k__BackingField;
				}
				[CompilerGenerated]
				private set
				{
					this.<CurrentIndex>k__BackingField = value;
				}
			}

			// Token: 0x0600077D RID: 1917 RVA: 0x000169C8 File Offset: 0x00014BC8
			public void Skip(int skip)
			{
				this.CurrentIndex += skip;
			}

			// Token: 0x0600077E RID: 1918 RVA: 0x000169D8 File Offset: 0x00014BD8
			public JsonStringParser(string input)
			{
				this.Input = input;
				this.InputAsArray = input.ToCharArray();
			}

			// Token: 0x0600077F RID: 1919 RVA: 0x000169F4 File Offset: 0x00014BF4
			internal bool ParseObject(out object output)
			{
				output = null;
				int currentIndex = this.CurrentIndex;
				if (!this.Accept(JsonUtilities.startObject))
				{
					return false;
				}
				Dictionary<string, object> dictionary = new Dictionary<string, object>();
				object obj;
				while (this.ParseMember(out obj))
				{
					Tuple<string, object> tuple = obj as Tuple<string, object>;
					dictionary[tuple.Item1] = tuple.Item2;
					if (!this.Accept(JsonUtilities.valueSeparator))
					{
						break;
					}
				}
				if (!this.Accept(JsonUtilities.endObject))
				{
					return false;
				}
				output = dictionary;
				return true;
			}

			// Token: 0x06000780 RID: 1920 RVA: 0x00016A64 File Offset: 0x00014C64
			private bool ParseMember(out object output)
			{
				output = null;
				object obj;
				if (!this.ParseString(out obj))
				{
					return false;
				}
				if (!this.Accept(JsonUtilities.nameSeparator))
				{
					return false;
				}
				object item;
				if (!this.ParseValue(out item))
				{
					return false;
				}
				output = new Tuple<string, object>((string)obj, item);
				return true;
			}

			// Token: 0x06000781 RID: 1921 RVA: 0x00016AAC File Offset: 0x00014CAC
			internal bool ParseArray(out object output)
			{
				output = null;
				if (!this.Accept(JsonUtilities.startArray))
				{
					return false;
				}
				List<object> list = new List<object>();
				object item;
				while (this.ParseValue(out item))
				{
					list.Add(item);
					if (!this.Accept(JsonUtilities.valueSeparator))
					{
						break;
					}
				}
				if (!this.Accept(JsonUtilities.endArray))
				{
					return false;
				}
				output = list;
				return true;
			}

			// Token: 0x06000782 RID: 1922 RVA: 0x00016B04 File Offset: 0x00014D04
			private bool ParseValue(out object output)
			{
				if (this.Accept(JsonUtilities.falseValue))
				{
					output = false;
					return true;
				}
				if (this.Accept(JsonUtilities.nullValue))
				{
					output = null;
					return true;
				}
				if (this.Accept(JsonUtilities.trueValue))
				{
					output = true;
					return true;
				}
				return this.ParseObject(out output) || this.ParseArray(out output) || this.ParseNumber(out output) || this.ParseString(out output);
			}

			// Token: 0x06000783 RID: 1923 RVA: 0x00016B78 File Offset: 0x00014D78
			private bool ParseString(out object output)
			{
				output = null;
				Match match;
				if (!this.Accept(JsonUtilities.stringValue, out match))
				{
					return false;
				}
				int num = 0;
				Group group = match.Groups["content"];
				StringBuilder stringBuilder = new StringBuilder(group.Value);
				foreach (object obj in match.Groups["escape"].Captures)
				{
					Capture capture = (Capture)obj;
					int num2 = capture.Index - group.Index - num;
					num += capture.Length - 1;
					stringBuilder.Remove(num2 + 1, capture.Length - 1);
					char c = capture.Value[1];
					if (c <= '\\')
					{
						if (c == '"')
						{
							stringBuilder[num2] = '"';
							continue;
						}
						if (c == '/')
						{
							stringBuilder[num2] = '/';
							continue;
						}
						if (c == '\\')
						{
							stringBuilder[num2] = '\\';
							continue;
						}
					}
					else if (c <= 'f')
					{
						if (c == 'b')
						{
							stringBuilder[num2] = '\b';
							continue;
						}
						if (c == 'f')
						{
							stringBuilder[num2] = '\f';
							continue;
						}
					}
					else
					{
						if (c == 'n')
						{
							stringBuilder[num2] = '\n';
							continue;
						}
						switch (c)
						{
						case 'r':
							stringBuilder[num2] = '\r';
							continue;
						case 't':
							stringBuilder[num2] = '\t';
							continue;
						case 'u':
							stringBuilder[num2] = (char)ushort.Parse(capture.Value.Substring(2), NumberStyles.AllowHexSpecifier);
							continue;
						}
					}
					throw new ArgumentException("Unexpected escape character in string: " + capture.Value);
				}
				output = stringBuilder.ToString();
				return true;
			}

			// Token: 0x06000784 RID: 1924 RVA: 0x00016D64 File Offset: 0x00014F64
			private bool ParseNumber(out object output)
			{
				output = null;
				Match match;
				if (!this.Accept(JsonUtilities.numberValue, out match))
				{
					return false;
				}
				if (match.Groups["frac"].Length > 0 || match.Groups["exp"].Length > 0)
				{
					output = double.Parse(match.Value, CultureInfo.InvariantCulture);
					return true;
				}
				output = long.Parse(match.Value, CultureInfo.InvariantCulture);
				return true;
			}

			// Token: 0x06000785 RID: 1925 RVA: 0x00016DE6 File Offset: 0x00014FE6
			private bool Accept(Regex matcher, out Match match)
			{
				match = matcher.Match(this.Input, this.CurrentIndex);
				if (match.Success)
				{
					this.Skip(match.Length);
				}
				return match.Success;
			}

			// Token: 0x06000786 RID: 1926 RVA: 0x00016E1C File Offset: 0x0001501C
			private bool Accept(char condition)
			{
				int num = 0;
				int num2 = this.InputAsArray.Length;
				int num3 = this.CurrentIndex;
				char c;
				while (num3 < num2 && ((c = this.InputAsArray[num3]) == ' ' || c == '\r' || c == '\t' || c == '\n'))
				{
					num++;
					num3++;
				}
				bool flag = num3 < num2 && this.InputAsArray[num3] == condition;
				if (flag)
				{
					num++;
					num3++;
					while (num3 < num2 && ((c = this.InputAsArray[num3]) == ' ' || c == '\r' || c == '\t' || c == '\n'))
					{
						num++;
						num3++;
					}
					this.Skip(num);
				}
				return flag;
			}

			// Token: 0x06000787 RID: 1927 RVA: 0x00016EBC File Offset: 0x000150BC
			private bool Accept(char[] condition)
			{
				int num = 0;
				int num2 = this.InputAsArray.Length;
				int num3 = this.CurrentIndex;
				char c;
				while (num3 < num2 && ((c = this.InputAsArray[num3]) == ' ' || c == '\r' || c == '\t' || c == '\n'))
				{
					num++;
					num3++;
				}
				bool flag = true;
				int num4 = 0;
				while (num3 < num2 && num4 < condition.Length)
				{
					if (this.InputAsArray[num3] != condition[num4])
					{
						flag = false;
						break;
					}
					num4++;
					num3++;
				}
				bool flag2 = num3 < num2 && flag;
				if (flag2)
				{
					this.Skip(num + condition.Length);
				}
				return flag2;
			}

			// Token: 0x040002AC RID: 684
			[CompilerGenerated]
			private string <Input>k__BackingField;

			// Token: 0x040002AD RID: 685
			[CompilerGenerated]
			private char[] <InputAsArray>k__BackingField;

			// Token: 0x040002AE RID: 686
			[CompilerGenerated]
			private int <CurrentIndex>k__BackingField;
		}

		// Token: 0x02000127 RID: 295
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06000788 RID: 1928 RVA: 0x00016F4B File Offset: 0x0001514B
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06000789 RID: 1929 RVA: 0x00016F57 File Offset: 0x00015157
			public <>c()
			{
			}

			// Token: 0x0600078A RID: 1930 RVA: 0x00016F60 File Offset: 0x00015160
			internal string <Encode>b__17_0(Match m)
			{
				char c = m.Value[0];
				switch (c)
				{
				case '\b':
					return "\\b";
				case '\t':
					return "\\t";
				case '\n':
					return "\\n";
				case '\v':
					break;
				case '\f':
					return "\\f";
				case '\r':
					return "\\r";
				default:
					if (c == '"')
					{
						return "\\\"";
					}
					if (c == '\\')
					{
						return "\\\\";
					}
					break;
				}
				return "\\u" + ((ushort)m.Value[0]).ToString("x4");
			}

			// Token: 0x040002AF RID: 687
			public static readonly JsonUtilities.<>c <>9 = new JsonUtilities.<>c();

			// Token: 0x040002B0 RID: 688
			public static MatchEvaluator <>9__17_0;
		}
	}
}
