using System;
using System.Xml;

namespace System.Runtime.Serialization
{
	// Token: 0x020000CE RID: 206
	internal static class DictionaryGlobals
	{
		// Token: 0x06000C02 RID: 3074 RVA: 0x00031CD8 File Offset: 0x0002FED8
		static DictionaryGlobals()
		{
			XmlDictionary xmlDictionary = new XmlDictionary(61);
			try
			{
				DictionaryGlobals.SchemaInstanceNamespace = xmlDictionary.Add("http://www.w3.org/2001/XMLSchema-instance");
				DictionaryGlobals.SerializationNamespace = xmlDictionary.Add("http://schemas.microsoft.com/2003/10/Serialization/");
				DictionaryGlobals.SchemaNamespace = xmlDictionary.Add("http://www.w3.org/2001/XMLSchema");
				DictionaryGlobals.XsiTypeLocalName = xmlDictionary.Add("type");
				DictionaryGlobals.XsiNilLocalName = xmlDictionary.Add("nil");
				DictionaryGlobals.IdLocalName = xmlDictionary.Add("Id");
				DictionaryGlobals.RefLocalName = xmlDictionary.Add("Ref");
				DictionaryGlobals.ArraySizeLocalName = xmlDictionary.Add("Size");
				DictionaryGlobals.EmptyString = xmlDictionary.Add(string.Empty);
				DictionaryGlobals.ISerializableFactoryTypeLocalName = xmlDictionary.Add("FactoryType");
				DictionaryGlobals.XmlnsNamespace = xmlDictionary.Add("http://www.w3.org/2000/xmlns/");
				DictionaryGlobals.CharLocalName = xmlDictionary.Add("char");
				DictionaryGlobals.BooleanLocalName = xmlDictionary.Add("boolean");
				DictionaryGlobals.SignedByteLocalName = xmlDictionary.Add("byte");
				DictionaryGlobals.UnsignedByteLocalName = xmlDictionary.Add("unsignedByte");
				DictionaryGlobals.ShortLocalName = xmlDictionary.Add("short");
				DictionaryGlobals.UnsignedShortLocalName = xmlDictionary.Add("unsignedShort");
				DictionaryGlobals.IntLocalName = xmlDictionary.Add("int");
				DictionaryGlobals.UnsignedIntLocalName = xmlDictionary.Add("unsignedInt");
				DictionaryGlobals.LongLocalName = xmlDictionary.Add("long");
				DictionaryGlobals.UnsignedLongLocalName = xmlDictionary.Add("unsignedLong");
				DictionaryGlobals.FloatLocalName = xmlDictionary.Add("float");
				DictionaryGlobals.DoubleLocalName = xmlDictionary.Add("double");
				DictionaryGlobals.DecimalLocalName = xmlDictionary.Add("decimal");
				DictionaryGlobals.DateTimeLocalName = xmlDictionary.Add("dateTime");
				DictionaryGlobals.StringLocalName = xmlDictionary.Add("string");
				DictionaryGlobals.ByteArrayLocalName = xmlDictionary.Add("base64Binary");
				DictionaryGlobals.ObjectLocalName = xmlDictionary.Add("anyType");
				DictionaryGlobals.TimeSpanLocalName = xmlDictionary.Add("duration");
				DictionaryGlobals.GuidLocalName = xmlDictionary.Add("guid");
				DictionaryGlobals.UriLocalName = xmlDictionary.Add("anyURI");
				DictionaryGlobals.QNameLocalName = xmlDictionary.Add("QName");
				DictionaryGlobals.ClrTypeLocalName = xmlDictionary.Add("Type");
				DictionaryGlobals.ClrAssemblyLocalName = xmlDictionary.Add("Assembly");
				DictionaryGlobals.Space = xmlDictionary.Add(" ");
				DictionaryGlobals.timeLocalName = xmlDictionary.Add("time");
				DictionaryGlobals.dateLocalName = xmlDictionary.Add("date");
				DictionaryGlobals.hexBinaryLocalName = xmlDictionary.Add("hexBinary");
				DictionaryGlobals.gYearMonthLocalName = xmlDictionary.Add("gYearMonth");
				DictionaryGlobals.gYearLocalName = xmlDictionary.Add("gYear");
				DictionaryGlobals.gMonthDayLocalName = xmlDictionary.Add("gMonthDay");
				DictionaryGlobals.gDayLocalName = xmlDictionary.Add("gDay");
				DictionaryGlobals.gMonthLocalName = xmlDictionary.Add("gMonth");
				DictionaryGlobals.integerLocalName = xmlDictionary.Add("integer");
				DictionaryGlobals.positiveIntegerLocalName = xmlDictionary.Add("positiveInteger");
				DictionaryGlobals.negativeIntegerLocalName = xmlDictionary.Add("negativeInteger");
				DictionaryGlobals.nonPositiveIntegerLocalName = xmlDictionary.Add("nonPositiveInteger");
				DictionaryGlobals.nonNegativeIntegerLocalName = xmlDictionary.Add("nonNegativeInteger");
				DictionaryGlobals.normalizedStringLocalName = xmlDictionary.Add("normalizedString");
				DictionaryGlobals.tokenLocalName = xmlDictionary.Add("token");
				DictionaryGlobals.languageLocalName = xmlDictionary.Add("language");
				DictionaryGlobals.NameLocalName = xmlDictionary.Add("Name");
				DictionaryGlobals.NCNameLocalName = xmlDictionary.Add("NCName");
				DictionaryGlobals.XSDIDLocalName = xmlDictionary.Add("ID");
				DictionaryGlobals.IDREFLocalName = xmlDictionary.Add("IDREF");
				DictionaryGlobals.IDREFSLocalName = xmlDictionary.Add("IDREFS");
				DictionaryGlobals.ENTITYLocalName = xmlDictionary.Add("ENTITY");
				DictionaryGlobals.ENTITIESLocalName = xmlDictionary.Add("ENTITIES");
				DictionaryGlobals.NMTOKENLocalName = xmlDictionary.Add("NMTOKEN");
				DictionaryGlobals.NMTOKENSLocalName = xmlDictionary.Add("NMTOKENS");
				DictionaryGlobals.AsmxTypesNamespace = xmlDictionary.Add("http://microsoft.com/wsdl/types/");
			}
			catch (Exception ex)
			{
				if (Fx.IsFatal(ex))
				{
					throw;
				}
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperFatal(ex.Message, ex);
			}
		}

		// Token: 0x040004CA RID: 1226
		public static readonly XmlDictionaryString EmptyString;

		// Token: 0x040004CB RID: 1227
		public static readonly XmlDictionaryString SchemaInstanceNamespace;

		// Token: 0x040004CC RID: 1228
		public static readonly XmlDictionaryString SchemaNamespace;

		// Token: 0x040004CD RID: 1229
		public static readonly XmlDictionaryString SerializationNamespace;

		// Token: 0x040004CE RID: 1230
		public static readonly XmlDictionaryString XmlnsNamespace;

		// Token: 0x040004CF RID: 1231
		public static readonly XmlDictionaryString XsiTypeLocalName;

		// Token: 0x040004D0 RID: 1232
		public static readonly XmlDictionaryString XsiNilLocalName;

		// Token: 0x040004D1 RID: 1233
		public static readonly XmlDictionaryString ClrTypeLocalName;

		// Token: 0x040004D2 RID: 1234
		public static readonly XmlDictionaryString ClrAssemblyLocalName;

		// Token: 0x040004D3 RID: 1235
		public static readonly XmlDictionaryString ArraySizeLocalName;

		// Token: 0x040004D4 RID: 1236
		public static readonly XmlDictionaryString IdLocalName;

		// Token: 0x040004D5 RID: 1237
		public static readonly XmlDictionaryString RefLocalName;

		// Token: 0x040004D6 RID: 1238
		public static readonly XmlDictionaryString ISerializableFactoryTypeLocalName;

		// Token: 0x040004D7 RID: 1239
		public static readonly XmlDictionaryString CharLocalName;

		// Token: 0x040004D8 RID: 1240
		public static readonly XmlDictionaryString BooleanLocalName;

		// Token: 0x040004D9 RID: 1241
		public static readonly XmlDictionaryString SignedByteLocalName;

		// Token: 0x040004DA RID: 1242
		public static readonly XmlDictionaryString UnsignedByteLocalName;

		// Token: 0x040004DB RID: 1243
		public static readonly XmlDictionaryString ShortLocalName;

		// Token: 0x040004DC RID: 1244
		public static readonly XmlDictionaryString UnsignedShortLocalName;

		// Token: 0x040004DD RID: 1245
		public static readonly XmlDictionaryString IntLocalName;

		// Token: 0x040004DE RID: 1246
		public static readonly XmlDictionaryString UnsignedIntLocalName;

		// Token: 0x040004DF RID: 1247
		public static readonly XmlDictionaryString LongLocalName;

		// Token: 0x040004E0 RID: 1248
		public static readonly XmlDictionaryString UnsignedLongLocalName;

		// Token: 0x040004E1 RID: 1249
		public static readonly XmlDictionaryString FloatLocalName;

		// Token: 0x040004E2 RID: 1250
		public static readonly XmlDictionaryString DoubleLocalName;

		// Token: 0x040004E3 RID: 1251
		public static readonly XmlDictionaryString DecimalLocalName;

		// Token: 0x040004E4 RID: 1252
		public static readonly XmlDictionaryString DateTimeLocalName;

		// Token: 0x040004E5 RID: 1253
		public static readonly XmlDictionaryString StringLocalName;

		// Token: 0x040004E6 RID: 1254
		public static readonly XmlDictionaryString ByteArrayLocalName;

		// Token: 0x040004E7 RID: 1255
		public static readonly XmlDictionaryString ObjectLocalName;

		// Token: 0x040004E8 RID: 1256
		public static readonly XmlDictionaryString TimeSpanLocalName;

		// Token: 0x040004E9 RID: 1257
		public static readonly XmlDictionaryString GuidLocalName;

		// Token: 0x040004EA RID: 1258
		public static readonly XmlDictionaryString UriLocalName;

		// Token: 0x040004EB RID: 1259
		public static readonly XmlDictionaryString QNameLocalName;

		// Token: 0x040004EC RID: 1260
		public static readonly XmlDictionaryString Space;

		// Token: 0x040004ED RID: 1261
		public static readonly XmlDictionaryString timeLocalName;

		// Token: 0x040004EE RID: 1262
		public static readonly XmlDictionaryString dateLocalName;

		// Token: 0x040004EF RID: 1263
		public static readonly XmlDictionaryString hexBinaryLocalName;

		// Token: 0x040004F0 RID: 1264
		public static readonly XmlDictionaryString gYearMonthLocalName;

		// Token: 0x040004F1 RID: 1265
		public static readonly XmlDictionaryString gYearLocalName;

		// Token: 0x040004F2 RID: 1266
		public static readonly XmlDictionaryString gMonthDayLocalName;

		// Token: 0x040004F3 RID: 1267
		public static readonly XmlDictionaryString gDayLocalName;

		// Token: 0x040004F4 RID: 1268
		public static readonly XmlDictionaryString gMonthLocalName;

		// Token: 0x040004F5 RID: 1269
		public static readonly XmlDictionaryString integerLocalName;

		// Token: 0x040004F6 RID: 1270
		public static readonly XmlDictionaryString positiveIntegerLocalName;

		// Token: 0x040004F7 RID: 1271
		public static readonly XmlDictionaryString negativeIntegerLocalName;

		// Token: 0x040004F8 RID: 1272
		public static readonly XmlDictionaryString nonPositiveIntegerLocalName;

		// Token: 0x040004F9 RID: 1273
		public static readonly XmlDictionaryString nonNegativeIntegerLocalName;

		// Token: 0x040004FA RID: 1274
		public static readonly XmlDictionaryString normalizedStringLocalName;

		// Token: 0x040004FB RID: 1275
		public static readonly XmlDictionaryString tokenLocalName;

		// Token: 0x040004FC RID: 1276
		public static readonly XmlDictionaryString languageLocalName;

		// Token: 0x040004FD RID: 1277
		public static readonly XmlDictionaryString NameLocalName;

		// Token: 0x040004FE RID: 1278
		public static readonly XmlDictionaryString NCNameLocalName;

		// Token: 0x040004FF RID: 1279
		public static readonly XmlDictionaryString XSDIDLocalName;

		// Token: 0x04000500 RID: 1280
		public static readonly XmlDictionaryString IDREFLocalName;

		// Token: 0x04000501 RID: 1281
		public static readonly XmlDictionaryString IDREFSLocalName;

		// Token: 0x04000502 RID: 1282
		public static readonly XmlDictionaryString ENTITYLocalName;

		// Token: 0x04000503 RID: 1283
		public static readonly XmlDictionaryString ENTITIESLocalName;

		// Token: 0x04000504 RID: 1284
		public static readonly XmlDictionaryString NMTOKENLocalName;

		// Token: 0x04000505 RID: 1285
		public static readonly XmlDictionaryString NMTOKENSLocalName;

		// Token: 0x04000506 RID: 1286
		public static readonly XmlDictionaryString AsmxTypesNamespace;
	}
}
