using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using Parse.Abstractions.Infrastructure;
using Parse.Abstractions.Infrastructure.Control;
using Parse.Infrastructure.Utilities;

namespace Parse.Infrastructure.Data
{
	// Token: 0x02000066 RID: 102
	public abstract class ParseDataEncoder
	{
		// Token: 0x0600048D RID: 1165 RVA: 0x0000FF38 File Offset: 0x0000E138
		public static bool Validate(object value)
		{
			return value == null || value.GetType().IsPrimitive || value is string || value is ParseObject || value is ParseACL || value is ParseFile || value is ParseGeoPoint || value is ParseRelationBase || value is DateTime || value is byte[] || Conversion.As<IDictionary<string, object>>(value) != null || Conversion.As<IList<object>>(value) != null;
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x0000FFA8 File Offset: 0x0000E1A8
		public object Encode(object value, IServiceHub serviceHub)
		{
			object result;
			if (value is DateTime)
			{
				DateTime dateTime = (DateTime)value;
				Dictionary<string, object> dictionary = new Dictionary<string, object>();
				dictionary["iso"] = dateTime.ToString(ParseClient.DateFormatStrings.First<string>(), CultureInfo.InvariantCulture);
				dictionary["__type"] = "Date";
				result = dictionary;
			}
			else
			{
				byte[] array = value as byte[];
				if (array == null)
				{
					ParseObject parseObject = value as ParseObject;
					if (parseObject == null)
					{
						IJsonConvertible jsonConvertible = value as IJsonConvertible;
						if (jsonConvertible == null)
						{
							if (value != null)
							{
								IDictionary<string, object> dictionary2 = Conversion.As<IDictionary<string, object>>(value);
								if (dictionary2 != null)
								{
									return dictionary2.ToDictionary((KeyValuePair<string, object> pair) => pair.Key, (KeyValuePair<string, object> pair) => this.Encode(pair.Value, serviceHub));
								}
								IList<object> list = Conversion.As<IList<object>>(value);
								if (list != null)
								{
									return this.EncodeList(list, serviceHub);
								}
								IParseFieldOperation parseFieldOperation = value as IParseFieldOperation;
								if (parseFieldOperation != null)
								{
									return parseFieldOperation.Encode(serviceHub);
								}
							}
							result = value;
						}
						else
						{
							result = jsonConvertible.ConvertToJSON();
						}
					}
					else
					{
						result = this.EncodeObject(parseObject);
					}
				}
				else
				{
					Dictionary<string, object> dictionary3 = new Dictionary<string, object>();
					dictionary3["__type"] = "Bytes";
					dictionary3["base64"] = Convert.ToBase64String(array);
					result = dictionary3;
				}
			}
			return result;
		}

		// Token: 0x0600048F RID: 1167
		protected abstract IDictionary<string, object> EncodeObject(ParseObject value);

		// Token: 0x06000490 RID: 1168 RVA: 0x0001011C File Offset: 0x0000E31C
		private object EncodeList(IList<object> list, IServiceHub serviceHub)
		{
			List<object> list2 = new List<object>();
			if (ParseClient.IL2CPPCompiled && list.GetType().IsArray)
			{
				list = new List<object>(list);
			}
			foreach (object value in list)
			{
				if (!ParseDataEncoder.Validate(value))
				{
					throw new ArgumentException("Invalid type for value in an array");
				}
				list2.Add(this.Encode(value, serviceHub));
			}
			return list2;
		}

		// Token: 0x06000491 RID: 1169 RVA: 0x000101A4 File Offset: 0x0000E3A4
		protected ParseDataEncoder()
		{
		}

		// Token: 0x02000136 RID: 310
		[CompilerGenerated]
		private sealed class <>c__DisplayClass1_0
		{
			// Token: 0x060007B4 RID: 1972 RVA: 0x000176C6 File Offset: 0x000158C6
			public <>c__DisplayClass1_0()
			{
			}

			// Token: 0x060007B5 RID: 1973 RVA: 0x000176CE File Offset: 0x000158CE
			internal object <Encode>b__1(KeyValuePair<string, object> pair)
			{
				return this.<>4__this.Encode(pair.Value, this.serviceHub);
			}

			// Token: 0x040002D9 RID: 729
			public ParseDataEncoder <>4__this;

			// Token: 0x040002DA RID: 730
			public IServiceHub serviceHub;
		}

		// Token: 0x02000137 RID: 311
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x060007B6 RID: 1974 RVA: 0x000176E8 File Offset: 0x000158E8
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x060007B7 RID: 1975 RVA: 0x000176F4 File Offset: 0x000158F4
			public <>c()
			{
			}

			// Token: 0x060007B8 RID: 1976 RVA: 0x000176FC File Offset: 0x000158FC
			internal string <Encode>b__1_0(KeyValuePair<string, object> pair)
			{
				return pair.Key;
			}

			// Token: 0x040002DB RID: 731
			public static readonly ParseDataEncoder.<>c <>9 = new ParseDataEncoder.<>c();

			// Token: 0x040002DC RID: 732
			public static Func<KeyValuePair<string, object>, string> <>9__1_0;
		}
	}
}
