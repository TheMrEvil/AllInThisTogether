using System;
using System.Xml;

namespace System.Runtime.Serialization.Json
{
	// Token: 0x02000182 RID: 386
	internal static class JsonGlobals
	{
		// Token: 0x0600138F RID: 5007 RVA: 0x0004B7C0 File Offset: 0x000499C0
		// Note: this type is marked as 'beforefieldinit'.
		static JsonGlobals()
		{
		}

		// Token: 0x040009BD RID: 2493
		public static readonly int DataContractXsdBaseNamespaceLength = "http://schemas.datacontract.org/2004/07/".Length;

		// Token: 0x040009BE RID: 2494
		public static readonly XmlDictionaryString dDictionaryString = new XmlDictionary().Add("d");

		// Token: 0x040009BF RID: 2495
		public static readonly char[] floatingPointCharacters = new char[]
		{
			'.',
			'e'
		};

		// Token: 0x040009C0 RID: 2496
		public static readonly XmlDictionaryString itemDictionaryString = new XmlDictionary().Add("item");

		// Token: 0x040009C1 RID: 2497
		public static readonly XmlDictionaryString rootDictionaryString = new XmlDictionary().Add("root");

		// Token: 0x040009C2 RID: 2498
		public static readonly long unixEpochTicks = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).Ticks;

		// Token: 0x040009C3 RID: 2499
		public const string applicationJsonMediaType = "application/json";

		// Token: 0x040009C4 RID: 2500
		public const string arrayString = "array";

		// Token: 0x040009C5 RID: 2501
		public const string booleanString = "boolean";

		// Token: 0x040009C6 RID: 2502
		public const string CacheControlString = "Cache-Control";

		// Token: 0x040009C7 RID: 2503
		public const byte CollectionByte = 91;

		// Token: 0x040009C8 RID: 2504
		public const char CollectionChar = '[';

		// Token: 0x040009C9 RID: 2505
		public const string DateTimeEndGuardReader = ")/";

		// Token: 0x040009CA RID: 2506
		public const string DateTimeEndGuardWriter = ")\\/";

		// Token: 0x040009CB RID: 2507
		public const string DateTimeStartGuardReader = "/Date(";

		// Token: 0x040009CC RID: 2508
		public const string DateTimeStartGuardWriter = "\\/Date(";

		// Token: 0x040009CD RID: 2509
		public const string dString = "d";

		// Token: 0x040009CE RID: 2510
		public const byte EndCollectionByte = 93;

		// Token: 0x040009CF RID: 2511
		public const char EndCollectionChar = ']';

		// Token: 0x040009D0 RID: 2512
		public const byte EndObjectByte = 125;

		// Token: 0x040009D1 RID: 2513
		public const char EndObjectChar = '}';

		// Token: 0x040009D2 RID: 2514
		public const string ExpiresString = "Expires";

		// Token: 0x040009D3 RID: 2515
		public const string IfModifiedSinceString = "If-Modified-Since";

		// Token: 0x040009D4 RID: 2516
		public const string itemString = "item";

		// Token: 0x040009D5 RID: 2517
		public const string jsonerrorString = "jsonerror";

		// Token: 0x040009D6 RID: 2518
		public const string KeyString = "Key";

		// Token: 0x040009D7 RID: 2519
		public const string LastModifiedString = "Last-Modified";

		// Token: 0x040009D8 RID: 2520
		public const int maxScopeSize = 25;

		// Token: 0x040009D9 RID: 2521
		public const byte MemberSeparatorByte = 44;

		// Token: 0x040009DA RID: 2522
		public const char MemberSeparatorChar = ',';

		// Token: 0x040009DB RID: 2523
		public const byte NameValueSeparatorByte = 58;

		// Token: 0x040009DC RID: 2524
		public const char NameValueSeparatorChar = ':';

		// Token: 0x040009DD RID: 2525
		public const string NameValueSeparatorString = ":";

		// Token: 0x040009DE RID: 2526
		public const string nullString = "null";

		// Token: 0x040009DF RID: 2527
		public const string numberString = "number";

		// Token: 0x040009E0 RID: 2528
		public const byte ObjectByte = 123;

		// Token: 0x040009E1 RID: 2529
		public const char ObjectChar = '{';

		// Token: 0x040009E2 RID: 2530
		public const string objectString = "object";

		// Token: 0x040009E3 RID: 2531
		public const string publicString = "public";

		// Token: 0x040009E4 RID: 2532
		public const byte QuoteByte = 34;

		// Token: 0x040009E5 RID: 2533
		public const char QuoteChar = '"';

		// Token: 0x040009E6 RID: 2534
		public const string rootString = "root";

		// Token: 0x040009E7 RID: 2535
		public const string serverTypeString = "__type";

		// Token: 0x040009E8 RID: 2536
		public const string stringString = "string";

		// Token: 0x040009E9 RID: 2537
		public const string textJsonMediaType = "text/json";

		// Token: 0x040009EA RID: 2538
		public const string trueString = "true";

		// Token: 0x040009EB RID: 2539
		public const string typeString = "type";

		// Token: 0x040009EC RID: 2540
		public const string ValueString = "Value";

		// Token: 0x040009ED RID: 2541
		public const char WhitespaceChar = ' ';

		// Token: 0x040009EE RID: 2542
		public const string xmlnsPrefix = "xmlns";

		// Token: 0x040009EF RID: 2543
		public const string xmlPrefix = "xml";
	}
}
