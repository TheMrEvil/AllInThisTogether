using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Parse.Abstractions.Infrastructure;
using Parse.Abstractions.Infrastructure.Control;
using Parse.Infrastructure.Data;
using Parse.Infrastructure.Utilities;

namespace Parse.Infrastructure.Control
{
	// Token: 0x0200006A RID: 106
	public class ParseAddUniqueOperation : IParseFieldOperation
	{
		// Token: 0x1700016A RID: 362
		// (get) Token: 0x060004A2 RID: 1186 RVA: 0x0001057C File Offset: 0x0000E77C
		private ReadOnlyCollection<object> Data
		{
			[CompilerGenerated]
			get
			{
				return this.<Data>k__BackingField;
			}
		}

		// Token: 0x060004A3 RID: 1187 RVA: 0x00010584 File Offset: 0x0000E784
		public ParseAddUniqueOperation(IEnumerable<object> objects)
		{
			this.Data = new ReadOnlyCollection<object>(objects.Distinct<object>().ToList<object>());
		}

		// Token: 0x060004A4 RID: 1188 RVA: 0x000105A2 File Offset: 0x0000E7A2
		public object Encode(IServiceHub serviceHub)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary["__op"] = "AddUnique";
			dictionary["objects"] = PointerOrLocalIdEncoder.Instance.Encode(this.Data, serviceHub);
			return dictionary;
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x000105D8 File Offset: 0x0000E7D8
		public IParseFieldOperation MergeWithPrevious(IParseFieldOperation previous)
		{
			IParseFieldOperation result;
			if (previous != null)
			{
				if (!(previous is ParseDeleteOperation))
				{
					ParseSetOperation parseSetOperation = previous as ParseSetOperation;
					if (parseSetOperation == null)
					{
						ParseAddUniqueOperation parseAddUniqueOperation = previous as ParseAddUniqueOperation;
						if (parseAddUniqueOperation == null)
						{
							throw new InvalidOperationException("Operation is invalid after previous operation.");
						}
						result = new ParseAddUniqueOperation(this.Apply(parseAddUniqueOperation.Objects, null) as IList<object>);
					}
					else
					{
						result = new ParseSetOperation(this.Apply(Conversion.To<IList<object>>(parseSetOperation.Value), null));
					}
				}
				else
				{
					result = new ParseSetOperation(this.Data.ToList<object>());
				}
			}
			else
			{
				result = this;
			}
			return result;
		}

		// Token: 0x060004A6 RID: 1190 RVA: 0x00010660 File Offset: 0x0000E860
		public object Apply(object oldValue, string key)
		{
			if (oldValue == null)
			{
				return this.Data.ToList<object>();
			}
			List<object> list = Conversion.To<IList<object>>(oldValue).ToList<object>();
			IEqualityComparer<object> comparer = ParseFieldOperations.ParseObjectComparer;
			using (IEnumerator<object> enumerator = this.Data.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					object target = enumerator.Current;
					if (target is ParseObject)
					{
						object obj = list.FirstOrDefault((object reference) => comparer.Equals(target, reference));
						if (obj == null)
						{
							list.Add(target);
						}
						else
						{
							list[list.IndexOf(obj)] = target;
						}
					}
					else if (!list.Contains(target, comparer))
					{
						list.Add(target);
					}
				}
			}
			return list;
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x060004A7 RID: 1191 RVA: 0x0001075C File Offset: 0x0000E95C
		public IEnumerable<object> Objects
		{
			get
			{
				return this.Data;
			}
		}

		// Token: 0x040000F3 RID: 243
		[CompilerGenerated]
		private readonly ReadOnlyCollection<object> <Data>k__BackingField;

		// Token: 0x02000139 RID: 313
		[CompilerGenerated]
		private sealed class <>c__DisplayClass6_0
		{
			// Token: 0x060007BF RID: 1983 RVA: 0x00017752 File Offset: 0x00015952
			public <>c__DisplayClass6_0()
			{
			}

			// Token: 0x040002E2 RID: 738
			public IEqualityComparer<object> comparer;
		}

		// Token: 0x0200013A RID: 314
		[CompilerGenerated]
		private sealed class <>c__DisplayClass6_1
		{
			// Token: 0x060007C0 RID: 1984 RVA: 0x0001775A File Offset: 0x0001595A
			public <>c__DisplayClass6_1()
			{
			}

			// Token: 0x060007C1 RID: 1985 RVA: 0x00017762 File Offset: 0x00015962
			internal bool <Apply>b__0(object reference)
			{
				return this.CS$<>8__locals1.comparer.Equals(this.target, reference);
			}

			// Token: 0x040002E3 RID: 739
			public object target;

			// Token: 0x040002E4 RID: 740
			public ParseAddUniqueOperation.<>c__DisplayClass6_0 CS$<>8__locals1;
		}
	}
}
