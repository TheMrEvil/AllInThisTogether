using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Net.Http.Headers
{
	// Token: 0x0200003F RID: 63
	internal abstract class HeaderInfo
	{
		// Token: 0x06000229 RID: 553 RVA: 0x00008B90 File Offset: 0x00006D90
		protected HeaderInfo(string name, HttpHeaderKind headerKind)
		{
			this.Name = name;
			this.HeaderKind = headerKind;
		}

		// Token: 0x0600022A RID: 554 RVA: 0x00008BA6 File Offset: 0x00006DA6
		public static HeaderInfo CreateSingle<T>(string name, TryParseDelegate<T> parser, HttpHeaderKind headerKind, Func<object, string> toString = null)
		{
			return new HeaderInfo.HeaderTypeInfo<T, object>(name, parser, headerKind)
			{
				CustomToString = toString
			};
		}

		// Token: 0x0600022B RID: 555 RVA: 0x00008BB7 File Offset: 0x00006DB7
		public static HeaderInfo CreateMulti<T>(string name, TryParseListDelegate<T> elementParser, HttpHeaderKind headerKind, int minimalCount = 1, string separator = ", ") where T : class
		{
			return new HeaderInfo.CollectionHeaderTypeInfo<T, T>(name, elementParser, headerKind, minimalCount, separator);
		}

		// Token: 0x0600022C RID: 556 RVA: 0x00008BC4 File Offset: 0x00006DC4
		public object CreateCollection(HttpHeaders headers)
		{
			return this.CreateCollection(headers, this);
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x0600022D RID: 557 RVA: 0x00008BCE File Offset: 0x00006DCE
		// (set) Token: 0x0600022E RID: 558 RVA: 0x00008BD6 File Offset: 0x00006DD6
		public Func<object, string> CustomToString
		{
			[CompilerGenerated]
			get
			{
				return this.<CustomToString>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<CustomToString>k__BackingField = value;
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x0600022F RID: 559 RVA: 0x00008BDF File Offset: 0x00006DDF
		public virtual string Separator
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06000230 RID: 560
		public abstract void AddToCollection(object collection, object value);

		// Token: 0x06000231 RID: 561
		protected abstract object CreateCollection(HttpHeaders headers, HeaderInfo headerInfo);

		// Token: 0x06000232 RID: 562
		public abstract List<string> ToStringCollection(object collection);

		// Token: 0x06000233 RID: 563
		public abstract bool TryParse(string value, out object result);

		// Token: 0x040000F4 RID: 244
		public bool AllowsMany;

		// Token: 0x040000F5 RID: 245
		public readonly HttpHeaderKind HeaderKind;

		// Token: 0x040000F6 RID: 246
		public readonly string Name;

		// Token: 0x040000F7 RID: 247
		[CompilerGenerated]
		private Func<object, string> <CustomToString>k__BackingField;

		// Token: 0x02000040 RID: 64
		private class HeaderTypeInfo<T, U> : HeaderInfo where U : class
		{
			// Token: 0x06000234 RID: 564 RVA: 0x00008BE6 File Offset: 0x00006DE6
			public HeaderTypeInfo(string name, TryParseDelegate<T> parser, HttpHeaderKind headerKind) : base(name, headerKind)
			{
				this.parser = parser;
			}

			// Token: 0x06000235 RID: 565 RVA: 0x00008BF8 File Offset: 0x00006DF8
			public override void AddToCollection(object collection, object value)
			{
				HttpHeaderValueCollection<U> httpHeaderValueCollection = (HttpHeaderValueCollection<U>)collection;
				List<U> list = value as List<U>;
				if (list != null)
				{
					httpHeaderValueCollection.AddRange(list);
					return;
				}
				httpHeaderValueCollection.Add((U)((object)value));
			}

			// Token: 0x06000236 RID: 566 RVA: 0x00008C2A File Offset: 0x00006E2A
			protected override object CreateCollection(HttpHeaders headers, HeaderInfo headerInfo)
			{
				return new HttpHeaderValueCollection<U>(headers, headerInfo);
			}

			// Token: 0x06000237 RID: 567 RVA: 0x00008C34 File Offset: 0x00006E34
			public override List<string> ToStringCollection(object collection)
			{
				if (collection == null)
				{
					return null;
				}
				HttpHeaderValueCollection<U> httpHeaderValueCollection = (HttpHeaderValueCollection<U>)collection;
				if (httpHeaderValueCollection.Count != 0)
				{
					List<string> list = new List<string>();
					foreach (U u in httpHeaderValueCollection)
					{
						list.Add(u.ToString());
					}
					if (httpHeaderValueCollection.InvalidValues != null)
					{
						list.AddRange(httpHeaderValueCollection.InvalidValues);
					}
					return list;
				}
				if (httpHeaderValueCollection.InvalidValues == null)
				{
					return null;
				}
				return new List<string>(httpHeaderValueCollection.InvalidValues);
			}

			// Token: 0x06000238 RID: 568 RVA: 0x00008CCC File Offset: 0x00006ECC
			public override bool TryParse(string value, out object result)
			{
				T t;
				bool result2 = this.parser(value, out t);
				result = t;
				return result2;
			}

			// Token: 0x040000F8 RID: 248
			private readonly TryParseDelegate<T> parser;
		}

		// Token: 0x02000041 RID: 65
		private class CollectionHeaderTypeInfo<T, U> : HeaderInfo.HeaderTypeInfo<T, U> where U : class
		{
			// Token: 0x06000239 RID: 569 RVA: 0x00008CEF File Offset: 0x00006EEF
			public CollectionHeaderTypeInfo(string name, TryParseListDelegate<T> parser, HttpHeaderKind headerKind, int minimalCount, string separator) : base(name, null, headerKind)
			{
				this.parser = parser;
				this.minimalCount = minimalCount;
				this.AllowsMany = true;
				this.separator = separator;
			}

			// Token: 0x1700008B RID: 139
			// (get) Token: 0x0600023A RID: 570 RVA: 0x00008D18 File Offset: 0x00006F18
			public override string Separator
			{
				get
				{
					return this.separator;
				}
			}

			// Token: 0x0600023B RID: 571 RVA: 0x00008D20 File Offset: 0x00006F20
			public override bool TryParse(string value, out object result)
			{
				List<T> list;
				if (!this.parser(value, this.minimalCount, out list))
				{
					result = null;
					return false;
				}
				result = list;
				return true;
			}

			// Token: 0x040000F9 RID: 249
			private readonly int minimalCount;

			// Token: 0x040000FA RID: 250
			private readonly string separator;

			// Token: 0x040000FB RID: 251
			private readonly TryParseListDelegate<T> parser;
		}
	}
}
