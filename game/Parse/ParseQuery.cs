using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Parse.Abstractions.Infrastructure;
using Parse.Abstractions.Platform.Objects;
using Parse.Infrastructure;
using Parse.Infrastructure.Data;
using Parse.Infrastructure.Utilities;

namespace Parse
{
	// Token: 0x02000011 RID: 17
	public class ParseQuery<T> where T : ParseObject
	{
		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060000DD RID: 221 RVA: 0x00005470 File Offset: 0x00003670
		private Dictionary<string, object> Filters
		{
			[CompilerGenerated]
			get
			{
				return this.<Filters>k__BackingField;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060000DE RID: 222 RVA: 0x00005478 File Offset: 0x00003678
		private ReadOnlyCollection<string> Orderings
		{
			[CompilerGenerated]
			get
			{
				return this.<Orderings>k__BackingField;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060000DF RID: 223 RVA: 0x00005480 File Offset: 0x00003680
		private ReadOnlyCollection<string> Includes
		{
			[CompilerGenerated]
			get
			{
				return this.<Includes>k__BackingField;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060000E0 RID: 224 RVA: 0x00005488 File Offset: 0x00003688
		private ReadOnlyCollection<string> KeySelections
		{
			[CompilerGenerated]
			get
			{
				return this.<KeySelections>k__BackingField;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060000E1 RID: 225 RVA: 0x00005490 File Offset: 0x00003690
		private string RedirectClassNameForKey
		{
			[CompilerGenerated]
			get
			{
				return this.<RedirectClassNameForKey>k__BackingField;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060000E2 RID: 226 RVA: 0x00005498 File Offset: 0x00003698
		private int? SkipAmount
		{
			[CompilerGenerated]
			get
			{
				return this.<SkipAmount>k__BackingField;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060000E3 RID: 227 RVA: 0x000054A0 File Offset: 0x000036A0
		private int? LimitAmount
		{
			[CompilerGenerated]
			get
			{
				return this.<LimitAmount>k__BackingField;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060000E4 RID: 228 RVA: 0x000054A8 File Offset: 0x000036A8
		internal string ClassName
		{
			[CompilerGenerated]
			get
			{
				return this.<ClassName>k__BackingField;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060000E5 RID: 229 RVA: 0x000054B0 File Offset: 0x000036B0
		internal IServiceHub Services
		{
			[CompilerGenerated]
			get
			{
				return this.<Services>k__BackingField;
			}
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x000054B8 File Offset: 0x000036B8
		internal ParseQuery(ParseQuery<T> source, IDictionary<string, object> where = null, IEnumerable<string> replacementOrderBy = null, IEnumerable<string> thenBy = null, int? skip = null, int? limit = null, IEnumerable<string> includes = null, IEnumerable<string> selectedKeys = null, string redirectClassNameForKey = null)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			this.Services = source.Services;
			this.ClassName = source.ClassName;
			this.Filters = source.Filters;
			this.Orderings = ((replacementOrderBy == null) ? source.Orderings : new ReadOnlyCollection<string>(replacementOrderBy.ToList<string>()));
			this.SkipAmount = ((skip == null) ? source.SkipAmount : (source.SkipAmount.GetValueOrDefault() + skip));
			int? num = limit;
			this.LimitAmount = ((num != null) ? num : source.LimitAmount);
			this.Includes = source.Includes;
			this.KeySelections = source.KeySelections;
			this.RedirectClassNameForKey = (redirectClassNameForKey ?? source.RedirectClassNameForKey);
			if (thenBy != null)
			{
				ReadOnlyCollection<string> orderings = this.Orderings;
				if (orderings == null)
				{
					throw new ArgumentException("You must call OrderBy before calling ThenBy.");
				}
				List<string> list = new List<string>(orderings);
				list.AddRange(thenBy);
				this.Orderings = new ReadOnlyCollection<string>(list);
			}
			if (this.Orderings != null)
			{
				this.Orderings = new ReadOnlyCollection<string>(new HashSet<string>(this.Orderings).ToList<string>());
			}
			if (where != null)
			{
				this.Filters = new Dictionary<string, object>(this.MergeWhereClauses(where));
			}
			if (includes != null)
			{
				this.Includes = new ReadOnlyCollection<string>(this.MergeIncludes(includes).ToList<string>());
			}
			if (selectedKeys != null)
			{
				this.KeySelections = new ReadOnlyCollection<string>(this.MergeSelectedKeys(selectedKeys).ToList<string>());
			}
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x0000564C File Offset: 0x0000384C
		private HashSet<string> MergeIncludes(IEnumerable<string> includes)
		{
			if (this.Includes == null)
			{
				return new HashSet<string>(includes);
			}
			HashSet<string> hashSet = new HashSet<string>(this.Includes);
			foreach (string item in includes)
			{
				hashSet.Add(item);
			}
			return hashSet;
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x000056B4 File Offset: 0x000038B4
		private HashSet<string> MergeSelectedKeys(IEnumerable<string> selectedKeys)
		{
			IEnumerable<string> keySelections = this.KeySelections;
			return new HashSet<string>((keySelections ?? Enumerable.Empty<string>()).Concat(selectedKeys));
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x000056E0 File Offset: 0x000038E0
		private IDictionary<string, object> MergeWhereClauses(IDictionary<string, object> where)
		{
			if (this.Filters == null)
			{
				return where;
			}
			Dictionary<string, object> dictionary = new Dictionary<string, object>(this.Filters);
			foreach (KeyValuePair<string, object> keyValuePair in where)
			{
				if (dictionary.ContainsKey(keyValuePair.Key))
				{
					IDictionary<string, object> dictionary2 = dictionary[keyValuePair.Key] as IDictionary<string, object>;
					if (dictionary2 != null)
					{
						IDictionary<string, object> dictionary3 = keyValuePair.Value as IDictionary<string, object>;
						if (dictionary3 != null)
						{
							Dictionary<string, object> dictionary4 = new Dictionary<string, object>(dictionary2);
							foreach (KeyValuePair<string, object> keyValuePair2 in dictionary3)
							{
								if (dictionary4.ContainsKey(keyValuePair2.Key))
								{
									throw new ArgumentException("More than one condition for the given key provided.");
								}
								dictionary4[keyValuePair2.Key] = keyValuePair2.Value;
							}
							dictionary[keyValuePair.Key] = dictionary4;
							continue;
						}
					}
					throw new ArgumentException("More than one where clause for the given key provided.");
				}
				dictionary[keyValuePair.Key] = keyValuePair.Value;
			}
			return dictionary;
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00005814 File Offset: 0x00003A14
		public ParseQuery(IServiceHub serviceHub) : this(serviceHub, serviceHub.ClassController.GetClassName(typeof(T)))
		{
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00005834 File Offset: 0x00003A34
		public ParseQuery(IServiceHub serviceHub, string className)
		{
			if (className == null)
			{
				throw new ArgumentNullException("className", "Must specify a ParseObject class name when creating a ParseQuery.");
			}
			this.ClassName = className;
			this.Services = serviceHub;
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00005870 File Offset: 0x00003A70
		public ParseQuery<T> OrderBy(string key)
		{
			return new ParseQuery<T>(this, null, new List<string>
			{
				key
			}, null, null, null, null, null, null);
		}

		// Token: 0x060000ED RID: 237 RVA: 0x000058A8 File Offset: 0x00003AA8
		public ParseQuery<T> OrderByDescending(string key)
		{
			return new ParseQuery<T>(this, null, new List<string>
			{
				"-" + key
			}, null, null, null, null, null, null);
		}

		// Token: 0x060000EE RID: 238 RVA: 0x000058E8 File Offset: 0x00003AE8
		public ParseQuery<T> ThenBy(string key)
		{
			return new ParseQuery<T>(this, null, null, new List<string>
			{
				key
			}, null, null, null, null, null);
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00005920 File Offset: 0x00003B20
		public ParseQuery<T> ThenByDescending(string key)
		{
			return new ParseQuery<T>(this, null, null, new List<string>
			{
				"-" + key
			}, null, null, null, null, null);
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00005960 File Offset: 0x00003B60
		public ParseQuery<T> Include(string key)
		{
			return new ParseQuery<T>(this, null, null, null, null, null, new List<string>
			{
				key
			}, null, null);
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00005998 File Offset: 0x00003B98
		public ParseQuery<T> Select(string key)
		{
			return new ParseQuery<T>(this, null, null, null, null, null, null, new List<string>
			{
				key
			}, null);
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x000059D0 File Offset: 0x00003BD0
		public ParseQuery<T> Skip(int count)
		{
			return new ParseQuery<T>(this, null, null, null, new int?(count), null, null, null, null);
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x000059F8 File Offset: 0x00003BF8
		public ParseQuery<T> Limit(int count)
		{
			return new ParseQuery<T>(this, null, null, null, null, new int?(count), null, null, null);
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00005A20 File Offset: 0x00003C20
		internal ParseQuery<T> RedirectClassName(string key)
		{
			return new ParseQuery<T>(this, null, null, null, null, null, null, null, key);
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00005A4C File Offset: 0x00003C4C
		public ParseQuery<T> WhereContainedIn<TIn>(string key, IEnumerable<TIn> values)
		{
			return new ParseQuery<T>(this, new Dictionary<string, object>
			{
				{
					key,
					new Dictionary<string, object>
					{
						{
							"$in",
							values.ToList<TIn>()
						}
					}
				}
			}, null, null, null, null, null, null, null);
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00005A98 File Offset: 0x00003C98
		public ParseQuery<T> WhereContainsAll<TIn>(string key, IEnumerable<TIn> values)
		{
			return new ParseQuery<T>(this, new Dictionary<string, object>
			{
				{
					key,
					new Dictionary<string, object>
					{
						{
							"$all",
							values.ToList<TIn>()
						}
					}
				}
			}, null, null, null, null, null, null, null);
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00005AE4 File Offset: 0x00003CE4
		public ParseQuery<T> WhereContains(string key, string substring)
		{
			return new ParseQuery<T>(this, new Dictionary<string, object>
			{
				{
					key,
					new Dictionary<string, object>
					{
						{
							"$regex",
							this.RegexQuote(substring)
						}
					}
				}
			}, null, null, null, null, null, null, null);
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00005B34 File Offset: 0x00003D34
		public ParseQuery<T> WhereDoesNotExist(string key)
		{
			return new ParseQuery<T>(this, new Dictionary<string, object>
			{
				{
					key,
					new Dictionary<string, object>
					{
						{
							"$exists",
							false
						}
					}
				}
			}, null, null, null, null, null, null, null);
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00005B80 File Offset: 0x00003D80
		public ParseQuery<T> WhereDoesNotMatchQuery<TOther>(string key, ParseQuery<TOther> query) where TOther : ParseObject
		{
			return new ParseQuery<T>(this, new Dictionary<string, object>
			{
				{
					key,
					new Dictionary<string, object>
					{
						{
							"$notInQuery",
							query.BuildParameters(true)
						}
					}
				}
			}, null, null, null, null, null, null, null);
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00005BD0 File Offset: 0x00003DD0
		public ParseQuery<T> WhereEndsWith(string key, string suffix)
		{
			return new ParseQuery<T>(this, new Dictionary<string, object>
			{
				{
					key,
					new Dictionary<string, object>
					{
						{
							"$regex",
							this.RegexQuote(suffix) + "$"
						}
					}
				}
			}, null, null, null, null, null, null, null);
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00005C28 File Offset: 0x00003E28
		public ParseQuery<T> WhereEqualTo(string key, object value)
		{
			return new ParseQuery<T>(this, new Dictionary<string, object>
			{
				{
					key,
					value
				}
			}, null, null, null, null, null, null, null);
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00005C60 File Offset: 0x00003E60
		public ParseQuery<T> WhereExists(string key)
		{
			return new ParseQuery<T>(this, new Dictionary<string, object>
			{
				{
					key,
					new Dictionary<string, object>
					{
						{
							"$exists",
							true
						}
					}
				}
			}, null, null, null, null, null, null, null);
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00005CAC File Offset: 0x00003EAC
		public ParseQuery<T> WhereGreaterThan(string key, object value)
		{
			return new ParseQuery<T>(this, new Dictionary<string, object>
			{
				{
					key,
					new Dictionary<string, object>
					{
						{
							"$gt",
							value
						}
					}
				}
			}, null, null, null, null, null, null, null);
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00005CF4 File Offset: 0x00003EF4
		public ParseQuery<T> WhereGreaterThanOrEqualTo(string key, object value)
		{
			return new ParseQuery<T>(this, new Dictionary<string, object>
			{
				{
					key,
					new Dictionary<string, object>
					{
						{
							"$gte",
							value
						}
					}
				}
			}, null, null, null, null, null, null, null);
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00005D3C File Offset: 0x00003F3C
		public ParseQuery<T> WhereLessThan(string key, object value)
		{
			return new ParseQuery<T>(this, new Dictionary<string, object>
			{
				{
					key,
					new Dictionary<string, object>
					{
						{
							"$lt",
							value
						}
					}
				}
			}, null, null, null, null, null, null, null);
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00005D84 File Offset: 0x00003F84
		public ParseQuery<T> WhereLessThanOrEqualTo(string key, object value)
		{
			return new ParseQuery<T>(this, new Dictionary<string, object>
			{
				{
					key,
					new Dictionary<string, object>
					{
						{
							"$lte",
							value
						}
					}
				}
			}, null, null, null, null, null, null, null);
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00005DCC File Offset: 0x00003FCC
		public ParseQuery<T> WhereMatches(string key, Regex regex, string modifiers)
		{
			if (regex.Options.HasFlag(RegexOptions.ECMAScript))
			{
				return new ParseQuery<T>(this, new Dictionary<string, object>
				{
					{
						key,
						this.EncodeRegex(regex, modifiers)
					}
				}, null, null, null, null, null, null, null);
			}
			throw new ArgumentException("Only ECMAScript-compatible regexes are supported. Please use the ECMAScript RegexOptions flag when creating your regex.");
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00005E31 File Offset: 0x00004031
		public ParseQuery<T> WhereMatches(string key, Regex regex)
		{
			return this.WhereMatches(key, regex, null);
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00005E3C File Offset: 0x0000403C
		public ParseQuery<T> WhereMatches(string key, string pattern, string modifiers = null)
		{
			return this.WhereMatches(key, new Regex(pattern, RegexOptions.ECMAScript), modifiers);
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00005E51 File Offset: 0x00004051
		public ParseQuery<T> WhereMatches(string key, string pattern)
		{
			return this.WhereMatches(key, pattern, null);
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00005E5C File Offset: 0x0000405C
		public ParseQuery<T> WhereMatchesKeyInQuery<TOther>(string key, string keyInQuery, ParseQuery<TOther> query) where TOther : ParseObject
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
			string key2 = "$select";
			Dictionary<string, object> dictionary3 = new Dictionary<string, object>();
			dictionary3["query"] = query.BuildParameters(true);
			dictionary3["key"] = keyInQuery;
			dictionary2[key2] = dictionary3;
			dictionary[key] = dictionary2;
			return new ParseQuery<T>(this, dictionary, null, null, null, null, null, null, null);
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00005EC8 File Offset: 0x000040C8
		public ParseQuery<T> WhereDoesNotMatchesKeyInQuery<TOther>(string key, string keyInQuery, ParseQuery<TOther> query) where TOther : ParseObject
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
			string key2 = "$dontSelect";
			Dictionary<string, object> dictionary3 = new Dictionary<string, object>();
			dictionary3["query"] = query.BuildParameters(true);
			dictionary3["key"] = keyInQuery;
			dictionary2[key2] = dictionary3;
			dictionary[key] = dictionary2;
			return new ParseQuery<T>(this, dictionary, null, null, null, null, null, null, null);
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00005F34 File Offset: 0x00004134
		public ParseQuery<T> WhereMatchesQuery<TOther>(string key, ParseQuery<TOther> query) where TOther : ParseObject
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
			dictionary2["$inQuery"] = query.BuildParameters(true);
			dictionary[key] = dictionary2;
			return new ParseQuery<T>(this, dictionary, null, null, null, null, null, null, null);
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00005F84 File Offset: 0x00004184
		public ParseQuery<T> WhereNear(string key, ParseGeoPoint point)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
			dictionary2["$nearSphere"] = point;
			dictionary[key] = dictionary2;
			return new ParseQuery<T>(this, dictionary, null, null, null, null, null, null, null);
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00005FD4 File Offset: 0x000041D4
		public ParseQuery<T> WhereNotContainedIn<TIn>(string key, IEnumerable<TIn> values)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
			dictionary2["$nin"] = values.ToList<TIn>();
			dictionary[key] = dictionary2;
			return new ParseQuery<T>(this, dictionary, null, null, null, null, null, null, null);
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00006024 File Offset: 0x00004224
		public ParseQuery<T> WhereNotEqualTo(string key, object value)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
			dictionary2["$ne"] = value;
			dictionary[key] = dictionary2;
			return new ParseQuery<T>(this, dictionary, null, null, null, null, null, null, null);
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00006070 File Offset: 0x00004270
		public ParseQuery<T> WhereStartsWith(string key, string suffix)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
			dictionary2["$regex"] = "^" + this.RegexQuote(suffix);
			dictionary[key] = dictionary2;
			return new ParseQuery<T>(this, dictionary, null, null, null, null, null, null, null);
		}

		// Token: 0x0600010C RID: 268 RVA: 0x000060CC File Offset: 0x000042CC
		public ParseQuery<T> WhereWithinGeoBox(string key, ParseGeoPoint southwest, ParseGeoPoint northeast)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			Dictionary<string, object> dictionary2 = dictionary;
			Dictionary<string, object> dictionary3 = new Dictionary<string, object>();
			Dictionary<string, object> dictionary4 = dictionary3;
			string key2 = "$within";
			Dictionary<string, object> dictionary5 = new Dictionary<string, object>();
			dictionary5["$box"] = new ParseGeoPoint[]
			{
				southwest,
				northeast
			};
			dictionary4[key2] = dictionary5;
			dictionary2[key] = dictionary3;
			return new ParseQuery<T>(this, dictionary, null, null, null, null, null, null, null);
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00006144 File Offset: 0x00004344
		public ParseQuery<T> WhereWithinDistance(string key, ParseGeoPoint point, ParseGeoDistance maxDistance)
		{
			ParseQuery<T> source = this.WhereNear(key, point);
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
			dictionary2["$maxDistance"] = maxDistance.Radians;
			dictionary[key] = dictionary2;
			return new ParseQuery<T>(source, dictionary, null, null, null, null, null, null, null);
		}

		// Token: 0x0600010E RID: 270 RVA: 0x000061A0 File Offset: 0x000043A0
		internal ParseQuery<T> WhereRelatedTo(ParseObject parent, string key)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			string key2 = "$relatedTo";
			Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
			dictionary2["object"] = parent;
			dictionary2["key"] = key;
			dictionary[key2] = dictionary2;
			return new ParseQuery<T>(this, dictionary, null, null, null, null, null, null, null);
		}

		// Token: 0x0600010F RID: 271 RVA: 0x000061F7 File Offset: 0x000043F7
		public Task<IEnumerable<T>> FindAsync()
		{
			return this.FindAsync(CancellationToken.None);
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00006204 File Offset: 0x00004404
		public Task<IEnumerable<T>> FindAsync(CancellationToken cancellationToken)
		{
			this.EnsureNotInstallationQuery();
			return this.Services.QueryController.FindAsync<T>(this, this.Services.GetCurrentUser(), cancellationToken).OnSuccess((Task<IEnumerable<IObjectState>> task) => from state in task.Result
			select this.Services.GenerateObjectFromState(state, this.ClassName));
		}

		// Token: 0x06000111 RID: 273 RVA: 0x0000623A File Offset: 0x0000443A
		public Task<T> FirstOrDefaultAsync()
		{
			return this.FirstOrDefaultAsync(CancellationToken.None);
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00006247 File Offset: 0x00004447
		public Task<T> FirstOrDefaultAsync(CancellationToken cancellationToken)
		{
			this.EnsureNotInstallationQuery();
			return this.Services.QueryController.FirstAsync<T>(this, this.Services.GetCurrentUser(), cancellationToken).OnSuccess(delegate(Task<IObjectState> task)
			{
				IObjectState result = task.Result;
				if (result == null || result == null)
				{
					return default(T);
				}
				return this.Services.GenerateObjectFromState(result, this.ClassName);
			});
		}

		// Token: 0x06000113 RID: 275 RVA: 0x0000627D File Offset: 0x0000447D
		public Task<T> FirstAsync()
		{
			return this.FirstAsync(CancellationToken.None);
		}

		// Token: 0x06000114 RID: 276 RVA: 0x0000628A File Offset: 0x0000448A
		public Task<T> FirstAsync(CancellationToken cancellationToken)
		{
			return this.FirstOrDefaultAsync(cancellationToken).OnSuccess(delegate(Task<T> task)
			{
				T result = task.Result;
				if (result == null)
				{
					throw new ParseFailureException(ParseFailureException.ErrorCode.ObjectNotFound, "No results matched the query.", null);
				}
				return result;
			});
		}

		// Token: 0x06000115 RID: 277 RVA: 0x000062B7 File Offset: 0x000044B7
		public Task<int> CountAsync()
		{
			return this.CountAsync(CancellationToken.None);
		}

		// Token: 0x06000116 RID: 278 RVA: 0x000062C4 File Offset: 0x000044C4
		public Task<int> CountAsync(CancellationToken cancellationToken)
		{
			this.EnsureNotInstallationQuery();
			return this.Services.QueryController.CountAsync<T>(this, this.Services.GetCurrentUser(), cancellationToken);
		}

		// Token: 0x06000117 RID: 279 RVA: 0x000062E9 File Offset: 0x000044E9
		public Task<T> GetAsync(string objectId)
		{
			return this.GetAsync(objectId, CancellationToken.None);
		}

		// Token: 0x06000118 RID: 280 RVA: 0x000062F8 File Offset: 0x000044F8
		public Task<T> GetAsync(string objectId, CancellationToken cancellationToken)
		{
			ParseQuery<T> source = new ParseQuery<T>(this.Services, this.ClassName).WhereEqualTo("objectId", objectId);
			IDictionary<string, object> where = null;
			IEnumerable<string> replacementOrderBy = null;
			IEnumerable<string> thenBy = null;
			int? skip = null;
			IEnumerable<string> includes = this.Includes;
			IEnumerable<string> keySelections = this.KeySelections;
			return new ParseQuery<T>(source, where, replacementOrderBy, thenBy, skip, new int?(1), includes, keySelections, null).FindAsync(cancellationToken).OnSuccess(delegate(Task<IEnumerable<T>> t)
			{
				T t2 = t.Result.FirstOrDefault<T>();
				if (t2 == null)
				{
					throw new ParseFailureException(ParseFailureException.ErrorCode.ObjectNotFound, "Object with the given objectId not found.", null);
				}
				return t2;
			});
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00006373 File Offset: 0x00004573
		internal object GetConstraint(string key)
		{
			Dictionary<string, object> filters = this.Filters;
			if (filters == null)
			{
				return null;
			}
			return filters.GetOrDefault(key, null);
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00006388 File Offset: 0x00004588
		internal IDictionary<string, object> BuildParameters(bool includeClassName = false)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			if (this.Filters != null)
			{
				dictionary["where"] = PointerOrLocalIdEncoder.Instance.Encode(this.Filters, this.Services);
			}
			if (this.Orderings != null)
			{
				dictionary["order"] = string.Join(",", this.Orderings.ToArray<string>());
			}
			if (this.SkipAmount != null)
			{
				dictionary["skip"] = this.SkipAmount.Value;
			}
			if (this.LimitAmount != null)
			{
				dictionary["limit"] = this.LimitAmount.Value;
			}
			if (this.Includes != null)
			{
				dictionary["include"] = string.Join(",", this.Includes.ToArray<string>());
			}
			if (this.KeySelections != null)
			{
				dictionary["keys"] = string.Join(",", this.KeySelections.ToArray<string>());
			}
			if (includeClassName)
			{
				dictionary["className"] = this.ClassName;
			}
			if (this.RedirectClassNameForKey != null)
			{
				dictionary["redirectClassNameForKey"] = this.RedirectClassNameForKey;
			}
			return dictionary;
		}

		// Token: 0x0600011B RID: 283 RVA: 0x000064C6 File Offset: 0x000046C6
		private string RegexQuote(string input)
		{
			return "\\Q" + input.Replace("\\E", "\\E\\\\E\\Q") + "\\E";
		}

		// Token: 0x0600011C RID: 284 RVA: 0x000064E8 File Offset: 0x000046E8
		private string GetRegexOptions(Regex regex, string modifiers)
		{
			string text = modifiers ?? "";
			if (regex.Options.HasFlag(RegexOptions.IgnoreCase) && !modifiers.Contains("i"))
			{
				text += "i";
			}
			if (regex.Options.HasFlag(RegexOptions.Multiline) && !modifiers.Contains("m"))
			{
				text += "m";
			}
			return text;
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00006564 File Offset: 0x00004764
		private IDictionary<string, object> EncodeRegex(Regex regex, string modifiers)
		{
			string regexOptions = this.GetRegexOptions(regex, modifiers);
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary["$regex"] = regex.ToString();
			Dictionary<string, object> dictionary2 = dictionary;
			if (!string.IsNullOrEmpty(regexOptions))
			{
				dictionary2["$options"] = regexOptions;
			}
			return dictionary2;
		}

		// Token: 0x0600011E RID: 286 RVA: 0x000065A6 File Offset: 0x000047A6
		private void EnsureNotInstallationQuery()
		{
			if (this.ClassName.Equals("_Installation"))
			{
				throw new InvalidOperationException("Cannot directly query the Installation class.");
			}
		}

		// Token: 0x0600011F RID: 287 RVA: 0x000065C8 File Offset: 0x000047C8
		public override bool Equals(object obj)
		{
			if (obj != null)
			{
				ParseQuery<T> parseQuery = obj as ParseQuery<T>;
				if (parseQuery != null)
				{
					return object.Equals(this.ClassName, parseQuery.ClassName) && this.Filters.CollectionsEqual(parseQuery.Filters) && this.Orderings.CollectionsEqual(parseQuery.Orderings) && this.Includes.CollectionsEqual(parseQuery.Includes) && this.KeySelections.CollectionsEqual(parseQuery.KeySelections) && object.Equals(this.SkipAmount, parseQuery.SkipAmount) && object.Equals(this.LimitAmount, parseQuery.LimitAmount);
				}
			}
			return false;
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00006686 File Offset: 0x00004886
		public override int GetHashCode()
		{
			return 0;
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00006689 File Offset: 0x00004889
		[CompilerGenerated]
		private IEnumerable<T> <FindAsync>b__69_0(Task<IEnumerable<IObjectState>> task)
		{
			return from state in task.Result
			select this.Services.GenerateObjectFromState(state, this.ClassName);
		}

		// Token: 0x06000122 RID: 290 RVA: 0x000066A2 File Offset: 0x000048A2
		[CompilerGenerated]
		private T <FindAsync>b__69_1(IObjectState state)
		{
			return this.Services.GenerateObjectFromState(state, this.ClassName);
		}

		// Token: 0x06000123 RID: 291 RVA: 0x000066B8 File Offset: 0x000048B8
		[CompilerGenerated]
		private T <FirstOrDefaultAsync>b__71_0(Task<IObjectState> task)
		{
			IObjectState result = task.Result;
			if (result == null || result == null)
			{
				return default(T);
			}
			return this.Services.GenerateObjectFromState(result, this.ClassName);
		}

		// Token: 0x04000025 RID: 37
		[CompilerGenerated]
		private readonly Dictionary<string, object> <Filters>k__BackingField;

		// Token: 0x04000026 RID: 38
		[CompilerGenerated]
		private readonly ReadOnlyCollection<string> <Orderings>k__BackingField;

		// Token: 0x04000027 RID: 39
		[CompilerGenerated]
		private readonly ReadOnlyCollection<string> <Includes>k__BackingField;

		// Token: 0x04000028 RID: 40
		[CompilerGenerated]
		private readonly ReadOnlyCollection<string> <KeySelections>k__BackingField;

		// Token: 0x04000029 RID: 41
		[CompilerGenerated]
		private readonly string <RedirectClassNameForKey>k__BackingField;

		// Token: 0x0400002A RID: 42
		[CompilerGenerated]
		private readonly int? <SkipAmount>k__BackingField;

		// Token: 0x0400002B RID: 43
		[CompilerGenerated]
		private readonly int? <LimitAmount>k__BackingField;

		// Token: 0x0400002C RID: 44
		[CompilerGenerated]
		private readonly string <ClassName>k__BackingField;

		// Token: 0x0400002D RID: 45
		[CompilerGenerated]
		private readonly IServiceHub <Services>k__BackingField;

		// Token: 0x020000B8 RID: 184
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06000602 RID: 1538 RVA: 0x0001333A File Offset: 0x0001153A
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06000603 RID: 1539 RVA: 0x00013346 File Offset: 0x00011546
			public <>c()
			{
			}

			// Token: 0x06000604 RID: 1540 RVA: 0x00013350 File Offset: 0x00011550
			internal T <FirstAsync>b__73_0(Task<T> task)
			{
				T result = task.Result;
				if (result == null)
				{
					throw new ParseFailureException(ParseFailureException.ErrorCode.ObjectNotFound, "No results matched the query.", null);
				}
				return result;
			}

			// Token: 0x06000605 RID: 1541 RVA: 0x0001337C File Offset: 0x0001157C
			internal T <GetAsync>b__77_0(Task<IEnumerable<T>> t)
			{
				T t2 = t.Result.FirstOrDefault<T>();
				if (t2 == null)
				{
					throw new ParseFailureException(ParseFailureException.ErrorCode.ObjectNotFound, "Object with the given objectId not found.", null);
				}
				return t2;
			}

			// Token: 0x04000143 RID: 323
			public static readonly ParseQuery<T>.<>c <>9 = new ParseQuery<T>.<>c();

			// Token: 0x04000144 RID: 324
			public static Func<Task<T>, T> <>9__73_0;

			// Token: 0x04000145 RID: 325
			public static Func<Task<IEnumerable<T>>, T> <>9__77_0;
		}
	}
}
