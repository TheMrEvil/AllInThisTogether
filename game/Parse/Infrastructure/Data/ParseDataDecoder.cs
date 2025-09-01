using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using Parse.Abstractions.Infrastructure;
using Parse.Abstractions.Infrastructure.Data;
using Parse.Abstractions.Platform.Objects;
using Parse.Infrastructure.Control;
using Parse.Infrastructure.Utilities;

namespace Parse.Infrastructure.Data
{
	// Token: 0x02000065 RID: 101
	public class ParseDataDecoder : IParseDataDecoder
	{
		// Token: 0x17000164 RID: 356
		// (get) Token: 0x06000486 RID: 1158 RVA: 0x0000FB67 File Offset: 0x0000DD67
		private IParseObjectClassController ClassController
		{
			[CompilerGenerated]
			get
			{
				return this.<ClassController>k__BackingField;
			}
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x0000FB6F File Offset: 0x0000DD6F
		public ParseDataDecoder(IParseObjectClassController classController)
		{
			this.ClassController = classController;
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x06000488 RID: 1160 RVA: 0x0000FB7E File Offset: 0x0000DD7E
		private static string[] Types
		{
			[CompilerGenerated]
			get
			{
				return ParseDataDecoder.<Types>k__BackingField;
			}
		} = new string[]
		{
			"Date",
			"Bytes",
			"Pointer",
			"File",
			"GeoPoint",
			"Object",
			"Relation"
		};

		// Token: 0x06000489 RID: 1161 RVA: 0x0000FB88 File Offset: 0x0000DD88
		public object Decode(object data, IServiceHub serviceHub)
		{
			object result;
			if (data != null)
			{
				IDictionary<string, object> dictionary = data as IDictionary<string, object>;
				if (dictionary == null)
				{
					IList<object> list = data as IList<object>;
					if (list == null)
					{
						result = data;
					}
					else
					{
						result = (from item in list
						select this.Decode(item, serviceHub)).ToList<object>();
					}
				}
				else
				{
					IDictionary<string, object> dictionary2 = dictionary;
					if (dictionary2.ContainsKey("__op"))
					{
						result = ParseFieldOperations.Decode(dictionary2);
					}
					else
					{
						IDictionary<string, object> dictionary3 = dictionary;
						object obj;
						if (dictionary3.TryGetValue("__type", out obj) && ParseDataDecoder.Types.Contains(obj))
						{
							string text = obj as string;
							if (text != null)
							{
								uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
								object result2;
								if (num <= 934040228U)
								{
									if (num != 704662986U)
									{
										if (num != 723007075U)
										{
											if (num != 934040228U)
											{
												goto IL_2CA;
											}
											if (!(text == "Bytes"))
											{
												goto IL_2CA;
											}
											result2 = Convert.FromBase64String(dictionary3["base64"] as string);
										}
										else
										{
											if (!(text == "File"))
											{
												goto IL_2CA;
											}
											result2 = new ParseFile(dictionary3["name"] as string, new Uri(dictionary3["url"] as string), null);
										}
									}
									else
									{
										if (!(text == "Pointer"))
										{
											goto IL_2CA;
										}
										result2 = this.DecodePointer(dictionary3["className"] as string, dictionary3["objectId"] as string, serviceHub);
									}
								}
								else if (num <= 3031640513U)
								{
									if (num != 995259257U)
									{
										if (num != 3031640513U)
										{
											goto IL_2CA;
										}
										if (!(text == "Relation"))
										{
											goto IL_2CA;
										}
										result2 = serviceHub.CreateRelation(null, null, dictionary3["className"] as string);
									}
									else
									{
										if (!(text == "Date"))
										{
											goto IL_2CA;
										}
										result2 = ParseDataDecoder.ParseDate(dictionary3["iso"] as string);
									}
								}
								else if (num != 3848795188U)
								{
									if (num != 3851314394U)
									{
										goto IL_2CA;
									}
									if (!(text == "Object"))
									{
										goto IL_2CA;
									}
									result2 = this.ClassController.GenerateObjectFromState(ParseObjectCoder.Instance.Decode(dictionary3, this, serviceHub), dictionary3["className"] as string, serviceHub);
								}
								else
								{
									if (!(text == "GeoPoint"))
									{
										goto IL_2CA;
									}
									result2 = new ParseGeoPoint(Conversion.To<double>(dictionary3["latitude"]), Conversion.To<double>(dictionary3["longitude"]));
								}
								return result2;
							}
							IL_2CA:
							throw new InvalidOperationException();
						}
						result = dictionary.ToDictionary((KeyValuePair<string, object> pair) => pair.Key, (KeyValuePair<string, object> pair) => this.Decode(pair.Value, serviceHub));
					}
				}
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x0000FEC2 File Offset: 0x0000E0C2
		protected virtual object DecodePointer(string className, string objectId, IServiceHub serviceHub)
		{
			return this.ClassController.CreateObjectWithoutData(className, objectId, serviceHub);
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x0000FED2 File Offset: 0x0000E0D2
		public static DateTime ParseDate(string input)
		{
			return DateTime.ParseExact(input, ParseClient.DateFormatStrings, CultureInfo.InvariantCulture, DateTimeStyles.None);
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x0000FEE8 File Offset: 0x0000E0E8
		// Note: this type is marked as 'beforefieldinit'.
		static ParseDataDecoder()
		{
		}

		// Token: 0x040000EE RID: 238
		[CompilerGenerated]
		private readonly IParseObjectClassController <ClassController>k__BackingField;

		// Token: 0x040000EF RID: 239
		[CompilerGenerated]
		private static readonly string[] <Types>k__BackingField;

		// Token: 0x02000134 RID: 308
		[CompilerGenerated]
		private sealed class <>c__DisplayClass7_0
		{
			// Token: 0x060007AE RID: 1966 RVA: 0x00017673 File Offset: 0x00015873
			public <>c__DisplayClass7_0()
			{
			}

			// Token: 0x060007AF RID: 1967 RVA: 0x0001767B File Offset: 0x0001587B
			internal object <Decode>b__1(KeyValuePair<string, object> pair)
			{
				return this.<>4__this.Decode(pair.Value, this.serviceHub);
			}

			// Token: 0x060007B0 RID: 1968 RVA: 0x00017695 File Offset: 0x00015895
			internal object <Decode>b__2(object item)
			{
				return this.<>4__this.Decode(item, this.serviceHub);
			}

			// Token: 0x040002D5 RID: 725
			public ParseDataDecoder <>4__this;

			// Token: 0x040002D6 RID: 726
			public IServiceHub serviceHub;
		}

		// Token: 0x02000135 RID: 309
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x060007B1 RID: 1969 RVA: 0x000176A9 File Offset: 0x000158A9
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x060007B2 RID: 1970 RVA: 0x000176B5 File Offset: 0x000158B5
			public <>c()
			{
			}

			// Token: 0x060007B3 RID: 1971 RVA: 0x000176BD File Offset: 0x000158BD
			internal string <Decode>b__7_0(KeyValuePair<string, object> pair)
			{
				return pair.Key;
			}

			// Token: 0x040002D7 RID: 727
			public static readonly ParseDataDecoder.<>c <>9 = new ParseDataDecoder.<>c();

			// Token: 0x040002D8 RID: 728
			public static Func<KeyValuePair<string, object>, string> <>9__7_0;
		}
	}
}
