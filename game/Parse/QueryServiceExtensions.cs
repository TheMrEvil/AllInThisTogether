using System;
using System.Collections.Generic;
using System.Linq;
using Parse.Abstractions.Infrastructure;

namespace Parse
{
	// Token: 0x02000021 RID: 33
	public static class QueryServiceExtensions
	{
		// Token: 0x060001CD RID: 461 RVA: 0x000082EE File Offset: 0x000064EE
		public static ParseQuery<T> GetQuery<T>(this IServiceHub serviceHub) where T : ParseObject
		{
			return new ParseQuery<T>(serviceHub);
		}

		// Token: 0x060001CE RID: 462 RVA: 0x000082F6 File Offset: 0x000064F6
		public static ParseQuery<T> ConstructOrQuery<T>(this IServiceHub serviceHub, ParseQuery<T> source, params ParseQuery<T>[] queries) where T : ParseObject
		{
			return serviceHub.ConstructOrQuery(queries.Concat(new ParseQuery<T>[]
			{
				source
			}));
		}

		// Token: 0x060001CF RID: 463 RVA: 0x00008310 File Offset: 0x00006510
		public static ParseQuery<T> ConstructOrQuery<T>(this IServiceHub serviceHub, IEnumerable<ParseQuery<T>> queries) where T : ParseObject
		{
			string text = null;
			List<IDictionary<string, object>> list = new List<IDictionary<string, object>>();
			foreach (object obj in queries)
			{
				ParseQuery<T> parseQuery = obj as ParseQuery<T>;
				if (text != null && parseQuery.ClassName != text)
				{
					throw new ArgumentException("All of the queries in an or query must be on the same class.");
				}
				text = parseQuery.ClassName;
				IDictionary<string, object> dictionary = parseQuery.BuildParameters(false);
				if (dictionary.Count != 0)
				{
					object obj2;
					if (!dictionary.TryGetValue("where", out obj2) || dictionary.Count > 1)
					{
						throw new ArgumentException("None of the queries in an or query can have non-filtering clauses");
					}
					list.Add(obj2 as IDictionary<string, object>);
				}
			}
			ParseQuery<T> source = new ParseQuery<T>(serviceHub, text);
			Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
			dictionary2["$or"] = list;
			return new ParseQuery<T>(source, dictionary2, null, null, null, null, null, null, null);
		}
	}
}
