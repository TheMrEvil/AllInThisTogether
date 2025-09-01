using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace System.Linq
{
	// Token: 0x0200008B RID: 139
	internal static class CachedReflectionInfo
	{
		// Token: 0x0600038D RID: 909 RVA: 0x0000A3E3 File Offset: 0x000085E3
		public static MethodInfo Aggregate_TSource_2(Type TSource)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_Aggregate_TSource_2) == null)
			{
				methodInfo = (CachedReflectionInfo.s_Aggregate_TSource_2 = new Func<IQueryable<object>, Expression<Func<object, object, object>>, object>(Queryable.Aggregate<object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[]
			{
				TSource
			});
		}

		// Token: 0x0600038E RID: 910 RVA: 0x0000A419 File Offset: 0x00008619
		public static MethodInfo Aggregate_TSource_TAccumulate_3(Type TSource, Type TAccumulate)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_Aggregate_TSource_TAccumulate_3) == null)
			{
				methodInfo = (CachedReflectionInfo.s_Aggregate_TSource_TAccumulate_3 = new Func<IQueryable<object>, object, Expression<Func<object, object, object>>, object>(Queryable.Aggregate<object, object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[]
			{
				TSource,
				TAccumulate
			});
		}

		// Token: 0x0600038F RID: 911 RVA: 0x0000A453 File Offset: 0x00008653
		public static MethodInfo Aggregate_TSource_TAccumulate_TResult_4(Type TSource, Type TAccumulate, Type TResult)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_Aggregate_TSource_TAccumulate_TResult_4) == null)
			{
				methodInfo = (CachedReflectionInfo.s_Aggregate_TSource_TAccumulate_TResult_4 = new Func<IQueryable<object>, object, Expression<Func<object, object, object>>, Expression<Func<object, object>>, object>(Queryable.Aggregate<object, object, object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[]
			{
				TSource,
				TAccumulate,
				TResult
			});
		}

		// Token: 0x06000390 RID: 912 RVA: 0x0000A491 File Offset: 0x00008691
		public static MethodInfo All_TSource_2(Type TSource)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_All_TSource_2) == null)
			{
				methodInfo = (CachedReflectionInfo.s_All_TSource_2 = new Func<IQueryable<object>, Expression<Func<object, bool>>, bool>(Queryable.All<object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[]
			{
				TSource
			});
		}

		// Token: 0x06000391 RID: 913 RVA: 0x0000A4C7 File Offset: 0x000086C7
		public static MethodInfo Any_TSource_1(Type TSource)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_Any_TSource_1) == null)
			{
				methodInfo = (CachedReflectionInfo.s_Any_TSource_1 = new Func<IQueryable<object>, bool>(Queryable.Any<object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[]
			{
				TSource
			});
		}

		// Token: 0x06000392 RID: 914 RVA: 0x0000A4FD File Offset: 0x000086FD
		public static MethodInfo Any_TSource_2(Type TSource)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_Any_TSource_2) == null)
			{
				methodInfo = (CachedReflectionInfo.s_Any_TSource_2 = new Func<IQueryable<object>, Expression<Func<object, bool>>, bool>(Queryable.Any<object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[]
			{
				TSource
			});
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000393 RID: 915 RVA: 0x0000A533 File Offset: 0x00008733
		public static MethodInfo Average_Int32_1
		{
			get
			{
				MethodInfo result;
				if ((result = CachedReflectionInfo.s_Average_Int32_1) == null)
				{
					result = (CachedReflectionInfo.s_Average_Int32_1 = new Func<IQueryable<int>, double>(Queryable.Average).GetMethodInfo());
				}
				return result;
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000394 RID: 916 RVA: 0x0000A555 File Offset: 0x00008755
		public static MethodInfo Average_NullableInt32_1
		{
			get
			{
				MethodInfo result;
				if ((result = CachedReflectionInfo.s_Average_NullableInt32_1) == null)
				{
					result = (CachedReflectionInfo.s_Average_NullableInt32_1 = new Func<IQueryable<int?>, double?>(Queryable.Average).GetMethodInfo());
				}
				return result;
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000395 RID: 917 RVA: 0x0000A577 File Offset: 0x00008777
		public static MethodInfo Average_Int64_1
		{
			get
			{
				MethodInfo result;
				if ((result = CachedReflectionInfo.s_Average_Int64_1) == null)
				{
					result = (CachedReflectionInfo.s_Average_Int64_1 = new Func<IQueryable<long>, double>(Queryable.Average).GetMethodInfo());
				}
				return result;
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000396 RID: 918 RVA: 0x0000A599 File Offset: 0x00008799
		public static MethodInfo Average_NullableInt64_1
		{
			get
			{
				MethodInfo result;
				if ((result = CachedReflectionInfo.s_Average_NullableInt64_1) == null)
				{
					result = (CachedReflectionInfo.s_Average_NullableInt64_1 = new Func<IQueryable<long?>, double?>(Queryable.Average).GetMethodInfo());
				}
				return result;
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000397 RID: 919 RVA: 0x0000A5BB File Offset: 0x000087BB
		public static MethodInfo Average_Single_1
		{
			get
			{
				MethodInfo result;
				if ((result = CachedReflectionInfo.s_Average_Single_1) == null)
				{
					result = (CachedReflectionInfo.s_Average_Single_1 = new Func<IQueryable<float>, float>(Queryable.Average).GetMethodInfo());
				}
				return result;
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000398 RID: 920 RVA: 0x0000A5DD File Offset: 0x000087DD
		public static MethodInfo Average_NullableSingle_1
		{
			get
			{
				MethodInfo result;
				if ((result = CachedReflectionInfo.s_Average_NullableSingle_1) == null)
				{
					result = (CachedReflectionInfo.s_Average_NullableSingle_1 = new Func<IQueryable<float?>, float?>(Queryable.Average).GetMethodInfo());
				}
				return result;
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000399 RID: 921 RVA: 0x0000A5FF File Offset: 0x000087FF
		public static MethodInfo Average_Double_1
		{
			get
			{
				MethodInfo result;
				if ((result = CachedReflectionInfo.s_Average_Double_1) == null)
				{
					result = (CachedReflectionInfo.s_Average_Double_1 = new Func<IQueryable<double>, double>(Queryable.Average).GetMethodInfo());
				}
				return result;
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x0600039A RID: 922 RVA: 0x0000A621 File Offset: 0x00008821
		public static MethodInfo Average_NullableDouble_1
		{
			get
			{
				MethodInfo result;
				if ((result = CachedReflectionInfo.s_Average_NullableDouble_1) == null)
				{
					result = (CachedReflectionInfo.s_Average_NullableDouble_1 = new Func<IQueryable<double?>, double?>(Queryable.Average).GetMethodInfo());
				}
				return result;
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x0600039B RID: 923 RVA: 0x0000A643 File Offset: 0x00008843
		public static MethodInfo Average_Decimal_1
		{
			get
			{
				MethodInfo result;
				if ((result = CachedReflectionInfo.s_Average_Decimal_1) == null)
				{
					result = (CachedReflectionInfo.s_Average_Decimal_1 = new Func<IQueryable<decimal>, decimal>(Queryable.Average).GetMethodInfo());
				}
				return result;
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x0600039C RID: 924 RVA: 0x0000A665 File Offset: 0x00008865
		public static MethodInfo Average_NullableDecimal_1
		{
			get
			{
				MethodInfo result;
				if ((result = CachedReflectionInfo.s_Average_NullableDecimal_1) == null)
				{
					result = (CachedReflectionInfo.s_Average_NullableDecimal_1 = new Func<IQueryable<decimal?>, decimal?>(Queryable.Average).GetMethodInfo());
				}
				return result;
			}
		}

		// Token: 0x0600039D RID: 925 RVA: 0x0000A687 File Offset: 0x00008887
		public static MethodInfo Average_Int32_TSource_2(Type TSource)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_Average_Int32_TSource_2) == null)
			{
				methodInfo = (CachedReflectionInfo.s_Average_Int32_TSource_2 = new Func<IQueryable<object>, Expression<Func<object, int>>, double>(Queryable.Average<object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[]
			{
				TSource
			});
		}

		// Token: 0x0600039E RID: 926 RVA: 0x0000A6BD File Offset: 0x000088BD
		public static MethodInfo Average_NullableInt32_TSource_2(Type TSource)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_Average_NullableInt32_TSource_2) == null)
			{
				methodInfo = (CachedReflectionInfo.s_Average_NullableInt32_TSource_2 = new Func<IQueryable<object>, Expression<Func<object, int?>>, double?>(Queryable.Average<object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[]
			{
				TSource
			});
		}

		// Token: 0x0600039F RID: 927 RVA: 0x0000A6F3 File Offset: 0x000088F3
		public static MethodInfo Average_Single_TSource_2(Type TSource)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_Average_Single_TSource_2) == null)
			{
				methodInfo = (CachedReflectionInfo.s_Average_Single_TSource_2 = new Func<IQueryable<object>, Expression<Func<object, float>>, float>(Queryable.Average<object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[]
			{
				TSource
			});
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x0000A729 File Offset: 0x00008929
		public static MethodInfo Average_NullableSingle_TSource_2(Type TSource)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_Average_NullableSingle_TSource_2) == null)
			{
				methodInfo = (CachedReflectionInfo.s_Average_NullableSingle_TSource_2 = new Func<IQueryable<object>, Expression<Func<object, float?>>, float?>(Queryable.Average<object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[]
			{
				TSource
			});
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x0000A75F File Offset: 0x0000895F
		public static MethodInfo Average_Int64_TSource_2(Type TSource)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_Average_Int64_TSource_2) == null)
			{
				methodInfo = (CachedReflectionInfo.s_Average_Int64_TSource_2 = new Func<IQueryable<object>, Expression<Func<object, long>>, double>(Queryable.Average<object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[]
			{
				TSource
			});
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x0000A795 File Offset: 0x00008995
		public static MethodInfo Average_NullableInt64_TSource_2(Type TSource)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_Average_NullableInt64_TSource_2) == null)
			{
				methodInfo = (CachedReflectionInfo.s_Average_NullableInt64_TSource_2 = new Func<IQueryable<object>, Expression<Func<object, long?>>, double?>(Queryable.Average<object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[]
			{
				TSource
			});
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x0000A7CB File Offset: 0x000089CB
		public static MethodInfo Average_Double_TSource_2(Type TSource)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_Average_Double_TSource_2) == null)
			{
				methodInfo = (CachedReflectionInfo.s_Average_Double_TSource_2 = new Func<IQueryable<object>, Expression<Func<object, double>>, double>(Queryable.Average<object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[]
			{
				TSource
			});
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x0000A801 File Offset: 0x00008A01
		public static MethodInfo Average_NullableDouble_TSource_2(Type TSource)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_Average_NullableDouble_TSource_2) == null)
			{
				methodInfo = (CachedReflectionInfo.s_Average_NullableDouble_TSource_2 = new Func<IQueryable<object>, Expression<Func<object, double?>>, double?>(Queryable.Average<object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[]
			{
				TSource
			});
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x0000A837 File Offset: 0x00008A37
		public static MethodInfo Average_Decimal_TSource_2(Type TSource)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_Average_Decimal_TSource_2) == null)
			{
				methodInfo = (CachedReflectionInfo.s_Average_Decimal_TSource_2 = new Func<IQueryable<object>, Expression<Func<object, decimal>>, decimal>(Queryable.Average<object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[]
			{
				TSource
			});
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x0000A86D File Offset: 0x00008A6D
		public static MethodInfo Average_NullableDecimal_TSource_2(Type TSource)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_Average_NullableDecimal_TSource_2) == null)
			{
				methodInfo = (CachedReflectionInfo.s_Average_NullableDecimal_TSource_2 = new Func<IQueryable<object>, Expression<Func<object, decimal?>>, decimal?>(Queryable.Average<object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[]
			{
				TSource
			});
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x0000A8A3 File Offset: 0x00008AA3
		public static MethodInfo Cast_TResult_1(Type TResult)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_Cast_TResult_1) == null)
			{
				methodInfo = (CachedReflectionInfo.s_Cast_TResult_1 = new Func<IQueryable, IQueryable<object>>(Queryable.Cast<object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[]
			{
				TResult
			});
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x0000A8D9 File Offset: 0x00008AD9
		public static MethodInfo Concat_TSource_2(Type TSource)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_Concat_TSource_2) == null)
			{
				methodInfo = (CachedReflectionInfo.s_Concat_TSource_2 = new Func<IQueryable<object>, IEnumerable<object>, IQueryable<object>>(Queryable.Concat<object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[]
			{
				TSource
			});
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x0000A90F File Offset: 0x00008B0F
		public static MethodInfo Contains_TSource_2(Type TSource)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_Contains_TSource_2) == null)
			{
				methodInfo = (CachedReflectionInfo.s_Contains_TSource_2 = new Func<IQueryable<object>, object, bool>(Queryable.Contains<object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[]
			{
				TSource
			});
		}

		// Token: 0x060003AA RID: 938 RVA: 0x0000A945 File Offset: 0x00008B45
		public static MethodInfo Contains_TSource_3(Type TSource)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_Contains_TSource_3) == null)
			{
				methodInfo = (CachedReflectionInfo.s_Contains_TSource_3 = new Func<IQueryable<object>, object, IEqualityComparer<object>, bool>(Queryable.Contains<object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[]
			{
				TSource
			});
		}

		// Token: 0x060003AB RID: 939 RVA: 0x0000A97B File Offset: 0x00008B7B
		public static MethodInfo Count_TSource_1(Type TSource)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_Count_TSource_1) == null)
			{
				methodInfo = (CachedReflectionInfo.s_Count_TSource_1 = new Func<IQueryable<object>, int>(Queryable.Count<object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[]
			{
				TSource
			});
		}

		// Token: 0x060003AC RID: 940 RVA: 0x0000A9B1 File Offset: 0x00008BB1
		public static MethodInfo Count_TSource_2(Type TSource)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_Count_TSource_2) == null)
			{
				methodInfo = (CachedReflectionInfo.s_Count_TSource_2 = new Func<IQueryable<object>, Expression<Func<object, bool>>, int>(Queryable.Count<object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[]
			{
				TSource
			});
		}

		// Token: 0x060003AD RID: 941 RVA: 0x0000A9E7 File Offset: 0x00008BE7
		public static MethodInfo DefaultIfEmpty_TSource_1(Type TSource)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_DefaultIfEmpty_TSource_1) == null)
			{
				methodInfo = (CachedReflectionInfo.s_DefaultIfEmpty_TSource_1 = new Func<IQueryable<object>, IQueryable<object>>(Queryable.DefaultIfEmpty<object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[]
			{
				TSource
			});
		}

		// Token: 0x060003AE RID: 942 RVA: 0x0000AA1D File Offset: 0x00008C1D
		public static MethodInfo DefaultIfEmpty_TSource_2(Type TSource)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_DefaultIfEmpty_TSource_2) == null)
			{
				methodInfo = (CachedReflectionInfo.s_DefaultIfEmpty_TSource_2 = new Func<IQueryable<object>, object, IQueryable<object>>(Queryable.DefaultIfEmpty<object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[]
			{
				TSource
			});
		}

		// Token: 0x060003AF RID: 943 RVA: 0x0000AA53 File Offset: 0x00008C53
		public static MethodInfo Distinct_TSource_1(Type TSource)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_Distinct_TSource_1) == null)
			{
				methodInfo = (CachedReflectionInfo.s_Distinct_TSource_1 = new Func<IQueryable<object>, IQueryable<object>>(Queryable.Distinct<object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[]
			{
				TSource
			});
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x0000AA89 File Offset: 0x00008C89
		public static MethodInfo Distinct_TSource_2(Type TSource)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_Distinct_TSource_2) == null)
			{
				methodInfo = (CachedReflectionInfo.s_Distinct_TSource_2 = new Func<IQueryable<object>, IEqualityComparer<object>, IQueryable<object>>(Queryable.Distinct<object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[]
			{
				TSource
			});
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x0000AABF File Offset: 0x00008CBF
		public static MethodInfo ElementAt_TSource_2(Type TSource)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_ElementAt_TSource_2) == null)
			{
				methodInfo = (CachedReflectionInfo.s_ElementAt_TSource_2 = new Func<IQueryable<object>, int, object>(Queryable.ElementAt<object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[]
			{
				TSource
			});
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x0000AAF5 File Offset: 0x00008CF5
		public static MethodInfo ElementAtOrDefault_TSource_2(Type TSource)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_ElementAtOrDefault_TSource_2) == null)
			{
				methodInfo = (CachedReflectionInfo.s_ElementAtOrDefault_TSource_2 = new Func<IQueryable<object>, int, object>(Queryable.ElementAtOrDefault<object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[]
			{
				TSource
			});
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x0000AB2B File Offset: 0x00008D2B
		public static MethodInfo Except_TSource_2(Type TSource)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_Except_TSource_2) == null)
			{
				methodInfo = (CachedReflectionInfo.s_Except_TSource_2 = new Func<IQueryable<object>, IEnumerable<object>, IQueryable<object>>(Queryable.Except<object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[]
			{
				TSource
			});
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x0000AB61 File Offset: 0x00008D61
		public static MethodInfo Except_TSource_3(Type TSource)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_Except_TSource_3) == null)
			{
				methodInfo = (CachedReflectionInfo.s_Except_TSource_3 = new Func<IQueryable<object>, IEnumerable<object>, IEqualityComparer<object>, IQueryable<object>>(Queryable.Except<object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[]
			{
				TSource
			});
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x0000AB97 File Offset: 0x00008D97
		public static MethodInfo First_TSource_1(Type TSource)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_First_TSource_1) == null)
			{
				methodInfo = (CachedReflectionInfo.s_First_TSource_1 = new Func<IQueryable<object>, object>(Queryable.First<object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[]
			{
				TSource
			});
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x0000ABCD File Offset: 0x00008DCD
		public static MethodInfo First_TSource_2(Type TSource)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_First_TSource_2) == null)
			{
				methodInfo = (CachedReflectionInfo.s_First_TSource_2 = new Func<IQueryable<object>, Expression<Func<object, bool>>, object>(Queryable.First<object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[]
			{
				TSource
			});
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x0000AC03 File Offset: 0x00008E03
		public static MethodInfo FirstOrDefault_TSource_1(Type TSource)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_FirstOrDefault_TSource_1) == null)
			{
				methodInfo = (CachedReflectionInfo.s_FirstOrDefault_TSource_1 = new Func<IQueryable<object>, object>(Queryable.FirstOrDefault<object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[]
			{
				TSource
			});
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x0000AC39 File Offset: 0x00008E39
		public static MethodInfo FirstOrDefault_TSource_2(Type TSource)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_FirstOrDefault_TSource_2) == null)
			{
				methodInfo = (CachedReflectionInfo.s_FirstOrDefault_TSource_2 = new Func<IQueryable<object>, Expression<Func<object, bool>>, object>(Queryable.FirstOrDefault<object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[]
			{
				TSource
			});
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x0000AC6F File Offset: 0x00008E6F
		public static MethodInfo GroupBy_TSource_TKey_2(Type TSource, Type TKey)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_GroupBy_TSource_TKey_2) == null)
			{
				methodInfo = (CachedReflectionInfo.s_GroupBy_TSource_TKey_2 = new Func<IQueryable<object>, Expression<Func<object, object>>, IQueryable<IGrouping<object, object>>>(Queryable.GroupBy<object, object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[]
			{
				TSource,
				TKey
			});
		}

		// Token: 0x060003BA RID: 954 RVA: 0x0000ACA9 File Offset: 0x00008EA9
		public static MethodInfo GroupBy_TSource_TKey_3(Type TSource, Type TKey)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_GroupBy_TSource_TKey_3) == null)
			{
				methodInfo = (CachedReflectionInfo.s_GroupBy_TSource_TKey_3 = new Func<IQueryable<object>, Expression<Func<object, object>>, IEqualityComparer<object>, IQueryable<IGrouping<object, object>>>(Queryable.GroupBy<object, object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[]
			{
				TSource,
				TKey
			});
		}

		// Token: 0x060003BB RID: 955 RVA: 0x0000ACE3 File Offset: 0x00008EE3
		public static MethodInfo GroupBy_TSource_TKey_TElement_3(Type TSource, Type TKey, Type TElement)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_GroupBy_TSource_TKey_TElement_3) == null)
			{
				methodInfo = (CachedReflectionInfo.s_GroupBy_TSource_TKey_TElement_3 = new Func<IQueryable<object>, Expression<Func<object, object>>, Expression<Func<object, object>>, IQueryable<IGrouping<object, object>>>(Queryable.GroupBy<object, object, object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[]
			{
				TSource,
				TKey,
				TElement
			});
		}

		// Token: 0x060003BC RID: 956 RVA: 0x0000AD21 File Offset: 0x00008F21
		public static MethodInfo GroupBy_TSource_TKey_TElement_4(Type TSource, Type TKey, Type TElement)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_GroupBy_TSource_TKey_TElement_4) == null)
			{
				methodInfo = (CachedReflectionInfo.s_GroupBy_TSource_TKey_TElement_4 = new Func<IQueryable<object>, Expression<Func<object, object>>, Expression<Func<object, object>>, IEqualityComparer<object>, IQueryable<IGrouping<object, object>>>(Queryable.GroupBy<object, object, object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[]
			{
				TSource,
				TKey,
				TElement
			});
		}

		// Token: 0x060003BD RID: 957 RVA: 0x0000AD5F File Offset: 0x00008F5F
		public static MethodInfo GroupBy_TSource_TKey_TResult_3(Type TSource, Type TKey, Type TResult)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_GroupBy_TSource_TKey_TResult_3) == null)
			{
				methodInfo = (CachedReflectionInfo.s_GroupBy_TSource_TKey_TResult_3 = new Func<IQueryable<object>, Expression<Func<object, object>>, Expression<Func<object, IEnumerable<object>, object>>, IQueryable<object>>(Queryable.GroupBy<object, object, object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[]
			{
				TSource,
				TKey,
				TResult
			});
		}

		// Token: 0x060003BE RID: 958 RVA: 0x0000AD9D File Offset: 0x00008F9D
		public static MethodInfo GroupBy_TSource_TKey_TResult_4(Type TSource, Type TKey, Type TResult)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_GroupBy_TSource_TKey_TResult_4) == null)
			{
				methodInfo = (CachedReflectionInfo.s_GroupBy_TSource_TKey_TResult_4 = new Func<IQueryable<object>, Expression<Func<object, object>>, Expression<Func<object, IEnumerable<object>, object>>, IEqualityComparer<object>, IQueryable<object>>(Queryable.GroupBy<object, object, object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[]
			{
				TSource,
				TKey,
				TResult
			});
		}

		// Token: 0x060003BF RID: 959 RVA: 0x0000ADDC File Offset: 0x00008FDC
		public static MethodInfo GroupBy_TSource_TKey_TElement_TResult_4(Type TSource, Type TKey, Type TElement, Type TResult)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_GroupBy_TSource_TKey_TElement_TResult_4) == null)
			{
				methodInfo = (CachedReflectionInfo.s_GroupBy_TSource_TKey_TElement_TResult_4 = new Func<IQueryable<object>, Expression<Func<object, object>>, Expression<Func<object, object>>, Expression<Func<object, IEnumerable<object>, object>>, IQueryable<object>>(Queryable.GroupBy<object, object, object, object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[]
			{
				TSource,
				TKey,
				TElement,
				TResult
			});
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x0000AE2C File Offset: 0x0000902C
		public static MethodInfo GroupBy_TSource_TKey_TElement_TResult_5(Type TSource, Type TKey, Type TElement, Type TResult)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_GroupBy_TSource_TKey_TElement_TResult_5) == null)
			{
				methodInfo = (CachedReflectionInfo.s_GroupBy_TSource_TKey_TElement_TResult_5 = new Func<IQueryable<object>, Expression<Func<object, object>>, Expression<Func<object, object>>, Expression<Func<object, IEnumerable<object>, object>>, IEqualityComparer<object>, IQueryable<object>>(Queryable.GroupBy<object, object, object, object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[]
			{
				TSource,
				TKey,
				TElement,
				TResult
			});
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x0000AE7C File Offset: 0x0000907C
		public static MethodInfo GroupJoin_TOuter_TInner_TKey_TResult_5(Type TOuter, Type TInner, Type TKey, Type TResult)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_GroupJoin_TOuter_TInner_TKey_TResult_5) == null)
			{
				methodInfo = (CachedReflectionInfo.s_GroupJoin_TOuter_TInner_TKey_TResult_5 = new Func<IQueryable<object>, IEnumerable<object>, Expression<Func<object, object>>, Expression<Func<object, object>>, Expression<Func<object, IEnumerable<object>, object>>, IQueryable<object>>(Queryable.GroupJoin<object, object, object, object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[]
			{
				TOuter,
				TInner,
				TKey,
				TResult
			});
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x0000AECC File Offset: 0x000090CC
		public static MethodInfo GroupJoin_TOuter_TInner_TKey_TResult_6(Type TOuter, Type TInner, Type TKey, Type TResult)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_GroupJoin_TOuter_TInner_TKey_TResult_6) == null)
			{
				methodInfo = (CachedReflectionInfo.s_GroupJoin_TOuter_TInner_TKey_TResult_6 = new Func<IQueryable<object>, IEnumerable<object>, Expression<Func<object, object>>, Expression<Func<object, object>>, Expression<Func<object, IEnumerable<object>, object>>, IEqualityComparer<object>, IQueryable<object>>(Queryable.GroupJoin<object, object, object, object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[]
			{
				TOuter,
				TInner,
				TKey,
				TResult
			});
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x0000AF19 File Offset: 0x00009119
		public static MethodInfo Intersect_TSource_2(Type TSource)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_Intersect_TSource_2) == null)
			{
				methodInfo = (CachedReflectionInfo.s_Intersect_TSource_2 = new Func<IQueryable<object>, IEnumerable<object>, IQueryable<object>>(Queryable.Intersect<object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[]
			{
				TSource
			});
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x0000AF4F File Offset: 0x0000914F
		public static MethodInfo Intersect_TSource_3(Type TSource)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_Intersect_TSource_3) == null)
			{
				methodInfo = (CachedReflectionInfo.s_Intersect_TSource_3 = new Func<IQueryable<object>, IEnumerable<object>, IEqualityComparer<object>, IQueryable<object>>(Queryable.Intersect<object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[]
			{
				TSource
			});
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x0000AF88 File Offset: 0x00009188
		public static MethodInfo Join_TOuter_TInner_TKey_TResult_5(Type TOuter, Type TInner, Type TKey, Type TResult)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_Join_TOuter_TInner_TKey_TResult_5) == null)
			{
				methodInfo = (CachedReflectionInfo.s_Join_TOuter_TInner_TKey_TResult_5 = new Func<IQueryable<object>, IEnumerable<object>, Expression<Func<object, object>>, Expression<Func<object, object>>, Expression<Func<object, object, object>>, IQueryable<object>>(Queryable.Join<object, object, object, object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[]
			{
				TOuter,
				TInner,
				TKey,
				TResult
			});
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x0000AFD8 File Offset: 0x000091D8
		public static MethodInfo Join_TOuter_TInner_TKey_TResult_6(Type TOuter, Type TInner, Type TKey, Type TResult)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_Join_TOuter_TInner_TKey_TResult_6) == null)
			{
				methodInfo = (CachedReflectionInfo.s_Join_TOuter_TInner_TKey_TResult_6 = new Func<IQueryable<object>, IEnumerable<object>, Expression<Func<object, object>>, Expression<Func<object, object>>, Expression<Func<object, object, object>>, IEqualityComparer<object>, IQueryable<object>>(Queryable.Join<object, object, object, object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[]
			{
				TOuter,
				TInner,
				TKey,
				TResult
			});
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x0000B025 File Offset: 0x00009225
		public static MethodInfo Last_TSource_1(Type TSource)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_Last_TSource_1) == null)
			{
				methodInfo = (CachedReflectionInfo.s_Last_TSource_1 = new Func<IQueryable<object>, object>(Queryable.Last<object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[]
			{
				TSource
			});
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x0000B05B File Offset: 0x0000925B
		public static MethodInfo Last_TSource_2(Type TSource)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_Last_TSource_2) == null)
			{
				methodInfo = (CachedReflectionInfo.s_Last_TSource_2 = new Func<IQueryable<object>, Expression<Func<object, bool>>, object>(Queryable.Last<object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[]
			{
				TSource
			});
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x0000B091 File Offset: 0x00009291
		public static MethodInfo LastOrDefault_TSource_1(Type TSource)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_LastOrDefault_TSource_1) == null)
			{
				methodInfo = (CachedReflectionInfo.s_LastOrDefault_TSource_1 = new Func<IQueryable<object>, object>(Queryable.LastOrDefault<object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[]
			{
				TSource
			});
		}

		// Token: 0x060003CA RID: 970 RVA: 0x0000B0C7 File Offset: 0x000092C7
		public static MethodInfo LastOrDefault_TSource_2(Type TSource)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_LastOrDefault_TSource_2) == null)
			{
				methodInfo = (CachedReflectionInfo.s_LastOrDefault_TSource_2 = new Func<IQueryable<object>, Expression<Func<object, bool>>, object>(Queryable.LastOrDefault<object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[]
			{
				TSource
			});
		}

		// Token: 0x060003CB RID: 971 RVA: 0x0000B0FD File Offset: 0x000092FD
		public static MethodInfo LongCount_TSource_1(Type TSource)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_LongCount_TSource_1) == null)
			{
				methodInfo = (CachedReflectionInfo.s_LongCount_TSource_1 = new Func<IQueryable<object>, long>(Queryable.LongCount<object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[]
			{
				TSource
			});
		}

		// Token: 0x060003CC RID: 972 RVA: 0x0000B133 File Offset: 0x00009333
		public static MethodInfo LongCount_TSource_2(Type TSource)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_LongCount_TSource_2) == null)
			{
				methodInfo = (CachedReflectionInfo.s_LongCount_TSource_2 = new Func<IQueryable<object>, Expression<Func<object, bool>>, long>(Queryable.LongCount<object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[]
			{
				TSource
			});
		}

		// Token: 0x060003CD RID: 973 RVA: 0x0000B169 File Offset: 0x00009369
		public static MethodInfo Max_TSource_1(Type TSource)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_Max_TSource_1) == null)
			{
				methodInfo = (CachedReflectionInfo.s_Max_TSource_1 = new Func<IQueryable<object>, object>(Queryable.Max<object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[]
			{
				TSource
			});
		}

		// Token: 0x060003CE RID: 974 RVA: 0x0000B19F File Offset: 0x0000939F
		public static MethodInfo Max_TSource_TResult_2(Type TSource, Type TResult)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_Max_TSource_TResult_2) == null)
			{
				methodInfo = (CachedReflectionInfo.s_Max_TSource_TResult_2 = new Func<IQueryable<object>, Expression<Func<object, object>>, object>(Queryable.Max<object, object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[]
			{
				TSource,
				TResult
			});
		}

		// Token: 0x060003CF RID: 975 RVA: 0x0000B1D9 File Offset: 0x000093D9
		public static MethodInfo Min_TSource_1(Type TSource)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_Min_TSource_1) == null)
			{
				methodInfo = (CachedReflectionInfo.s_Min_TSource_1 = new Func<IQueryable<object>, object>(Queryable.Min<object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[]
			{
				TSource
			});
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x0000B20F File Offset: 0x0000940F
		public static MethodInfo Min_TSource_TResult_2(Type TSource, Type TResult)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_Min_TSource_TResult_2) == null)
			{
				methodInfo = (CachedReflectionInfo.s_Min_TSource_TResult_2 = new Func<IQueryable<object>, Expression<Func<object, object>>, object>(Queryable.Min<object, object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[]
			{
				TSource,
				TResult
			});
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x0000B249 File Offset: 0x00009449
		public static MethodInfo OfType_TResult_1(Type TResult)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_OfType_TResult_1) == null)
			{
				methodInfo = (CachedReflectionInfo.s_OfType_TResult_1 = new Func<IQueryable, IQueryable<object>>(Queryable.OfType<object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[]
			{
				TResult
			});
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x0000B27F File Offset: 0x0000947F
		public static MethodInfo OrderBy_TSource_TKey_2(Type TSource, Type TKey)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_OrderBy_TSource_TKey_2) == null)
			{
				methodInfo = (CachedReflectionInfo.s_OrderBy_TSource_TKey_2 = new Func<IQueryable<object>, Expression<Func<object, object>>, IOrderedQueryable<object>>(Queryable.OrderBy<object, object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[]
			{
				TSource,
				TKey
			});
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x0000B2B9 File Offset: 0x000094B9
		public static MethodInfo OrderBy_TSource_TKey_3(Type TSource, Type TKey)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_OrderBy_TSource_TKey_3) == null)
			{
				methodInfo = (CachedReflectionInfo.s_OrderBy_TSource_TKey_3 = new Func<IQueryable<object>, Expression<Func<object, object>>, IComparer<object>, IOrderedQueryable<object>>(Queryable.OrderBy<object, object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[]
			{
				TSource,
				TKey
			});
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x0000B2F3 File Offset: 0x000094F3
		public static MethodInfo OrderByDescending_TSource_TKey_2(Type TSource, Type TKey)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_OrderByDescending_TSource_TKey_2) == null)
			{
				methodInfo = (CachedReflectionInfo.s_OrderByDescending_TSource_TKey_2 = new Func<IQueryable<object>, Expression<Func<object, object>>, IOrderedQueryable<object>>(Queryable.OrderByDescending<object, object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[]
			{
				TSource,
				TKey
			});
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x0000B32D File Offset: 0x0000952D
		public static MethodInfo OrderByDescending_TSource_TKey_3(Type TSource, Type TKey)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_OrderByDescending_TSource_TKey_3) == null)
			{
				methodInfo = (CachedReflectionInfo.s_OrderByDescending_TSource_TKey_3 = new Func<IQueryable<object>, Expression<Func<object, object>>, IComparer<object>, IOrderedQueryable<object>>(Queryable.OrderByDescending<object, object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[]
			{
				TSource,
				TKey
			});
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x0000B367 File Offset: 0x00009567
		public static MethodInfo Reverse_TSource_1(Type TSource)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_Reverse_TSource_1) == null)
			{
				methodInfo = (CachedReflectionInfo.s_Reverse_TSource_1 = new Func<IQueryable<object>, IQueryable<object>>(Queryable.Reverse<object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[]
			{
				TSource
			});
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x0000B39D File Offset: 0x0000959D
		public static MethodInfo Select_TSource_TResult_2(Type TSource, Type TResult)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_Select_TSource_TResult_2) == null)
			{
				methodInfo = (CachedReflectionInfo.s_Select_TSource_TResult_2 = new Func<IQueryable<object>, Expression<Func<object, object>>, IQueryable<object>>(Queryable.Select<object, object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[]
			{
				TSource,
				TResult
			});
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x0000B3D7 File Offset: 0x000095D7
		public static MethodInfo Select_Index_TSource_TResult_2(Type TSource, Type TResult)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_Select_Index_TSource_TResult_2) == null)
			{
				methodInfo = (CachedReflectionInfo.s_Select_Index_TSource_TResult_2 = new Func<IQueryable<object>, Expression<Func<object, int, object>>, IQueryable<object>>(Queryable.Select<object, object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[]
			{
				TSource,
				TResult
			});
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x0000B411 File Offset: 0x00009611
		public static MethodInfo SelectMany_TSource_TResult_2(Type TSource, Type TResult)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_SelectMany_TSource_TResult_2) == null)
			{
				methodInfo = (CachedReflectionInfo.s_SelectMany_TSource_TResult_2 = new Func<IQueryable<object>, Expression<Func<object, IEnumerable<object>>>, IQueryable<object>>(Queryable.SelectMany<object, object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[]
			{
				TSource,
				TResult
			});
		}

		// Token: 0x060003DA RID: 986 RVA: 0x0000B44B File Offset: 0x0000964B
		public static MethodInfo SelectMany_Index_TSource_TResult_2(Type TSource, Type TResult)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_SelectMany_Index_TSource_TResult_2) == null)
			{
				methodInfo = (CachedReflectionInfo.s_SelectMany_Index_TSource_TResult_2 = new Func<IQueryable<object>, Expression<Func<object, int, IEnumerable<object>>>, IQueryable<object>>(Queryable.SelectMany<object, object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[]
			{
				TSource,
				TResult
			});
		}

		// Token: 0x060003DB RID: 987 RVA: 0x0000B485 File Offset: 0x00009685
		public static MethodInfo SelectMany_Index_TSource_TCollection_TResult_3(Type TSource, Type TCollection, Type TResult)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_SelectMany_Index_TSource_TCollection_TResult_3) == null)
			{
				methodInfo = (CachedReflectionInfo.s_SelectMany_Index_TSource_TCollection_TResult_3 = new Func<IQueryable<object>, Expression<Func<object, int, IEnumerable<object>>>, Expression<Func<object, object, object>>, IQueryable<object>>(Queryable.SelectMany<object, object, object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[]
			{
				TSource,
				TCollection,
				TResult
			});
		}

		// Token: 0x060003DC RID: 988 RVA: 0x0000B4C3 File Offset: 0x000096C3
		public static MethodInfo SelectMany_TSource_TCollection_TResult_3(Type TSource, Type TCollection, Type TResult)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_SelectMany_TSource_TCollection_TResult_3) == null)
			{
				methodInfo = (CachedReflectionInfo.s_SelectMany_TSource_TCollection_TResult_3 = new Func<IQueryable<object>, Expression<Func<object, IEnumerable<object>>>, Expression<Func<object, object, object>>, IQueryable<object>>(Queryable.SelectMany<object, object, object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[]
			{
				TSource,
				TCollection,
				TResult
			});
		}

		// Token: 0x060003DD RID: 989 RVA: 0x0000B501 File Offset: 0x00009701
		public static MethodInfo SequenceEqual_TSource_2(Type TSource)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_SequenceEqual_TSource_2) == null)
			{
				methodInfo = (CachedReflectionInfo.s_SequenceEqual_TSource_2 = new Func<IQueryable<object>, IEnumerable<object>, bool>(Queryable.SequenceEqual<object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[]
			{
				TSource
			});
		}

		// Token: 0x060003DE RID: 990 RVA: 0x0000B537 File Offset: 0x00009737
		public static MethodInfo SequenceEqual_TSource_3(Type TSource)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_SequenceEqual_TSource_3) == null)
			{
				methodInfo = (CachedReflectionInfo.s_SequenceEqual_TSource_3 = new Func<IQueryable<object>, IEnumerable<object>, IEqualityComparer<object>, bool>(Queryable.SequenceEqual<object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[]
			{
				TSource
			});
		}

		// Token: 0x060003DF RID: 991 RVA: 0x0000B56D File Offset: 0x0000976D
		public static MethodInfo Single_TSource_1(Type TSource)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_Single_TSource_1) == null)
			{
				methodInfo = (CachedReflectionInfo.s_Single_TSource_1 = new Func<IQueryable<object>, object>(Queryable.Single<object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[]
			{
				TSource
			});
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x0000B5A3 File Offset: 0x000097A3
		public static MethodInfo Single_TSource_2(Type TSource)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_Single_TSource_2) == null)
			{
				methodInfo = (CachedReflectionInfo.s_Single_TSource_2 = new Func<IQueryable<object>, Expression<Func<object, bool>>, object>(Queryable.Single<object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[]
			{
				TSource
			});
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x0000B5D9 File Offset: 0x000097D9
		public static MethodInfo SingleOrDefault_TSource_1(Type TSource)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_SingleOrDefault_TSource_1) == null)
			{
				methodInfo = (CachedReflectionInfo.s_SingleOrDefault_TSource_1 = new Func<IQueryable<object>, object>(Queryable.SingleOrDefault<object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[]
			{
				TSource
			});
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x0000B60F File Offset: 0x0000980F
		public static MethodInfo SingleOrDefault_TSource_2(Type TSource)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_SingleOrDefault_TSource_2) == null)
			{
				methodInfo = (CachedReflectionInfo.s_SingleOrDefault_TSource_2 = new Func<IQueryable<object>, Expression<Func<object, bool>>, object>(Queryable.SingleOrDefault<object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[]
			{
				TSource
			});
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x0000B645 File Offset: 0x00009845
		public static MethodInfo Skip_TSource_2(Type TSource)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_Skip_TSource_2) == null)
			{
				methodInfo = (CachedReflectionInfo.s_Skip_TSource_2 = new Func<IQueryable<object>, int, IQueryable<object>>(Queryable.Skip<object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[]
			{
				TSource
			});
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x0000B67B File Offset: 0x0000987B
		public static MethodInfo SkipWhile_TSource_2(Type TSource)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_SkipWhile_TSource_2) == null)
			{
				methodInfo = (CachedReflectionInfo.s_SkipWhile_TSource_2 = new Func<IQueryable<object>, Expression<Func<object, bool>>, IQueryable<object>>(Queryable.SkipWhile<object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[]
			{
				TSource
			});
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x0000B6B1 File Offset: 0x000098B1
		public static MethodInfo SkipWhile_Index_TSource_2(Type TSource)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_SkipWhile_Index_TSource_2) == null)
			{
				methodInfo = (CachedReflectionInfo.s_SkipWhile_Index_TSource_2 = new Func<IQueryable<object>, Expression<Func<object, int, bool>>, IQueryable<object>>(Queryable.SkipWhile<object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[]
			{
				TSource
			});
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060003E6 RID: 998 RVA: 0x0000B6E7 File Offset: 0x000098E7
		public static MethodInfo Sum_Int32_1
		{
			get
			{
				MethodInfo result;
				if ((result = CachedReflectionInfo.s_Sum_Int32_1) == null)
				{
					result = (CachedReflectionInfo.s_Sum_Int32_1 = new Func<IQueryable<int>, int>(Queryable.Sum).GetMethodInfo());
				}
				return result;
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060003E7 RID: 999 RVA: 0x0000B709 File Offset: 0x00009909
		public static MethodInfo Sum_NullableInt32_1
		{
			get
			{
				MethodInfo result;
				if ((result = CachedReflectionInfo.s_Sum_NullableInt32_1) == null)
				{
					result = (CachedReflectionInfo.s_Sum_NullableInt32_1 = new Func<IQueryable<int?>, int?>(Queryable.Sum).GetMethodInfo());
				}
				return result;
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060003E8 RID: 1000 RVA: 0x0000B72B File Offset: 0x0000992B
		public static MethodInfo Sum_Int64_1
		{
			get
			{
				MethodInfo result;
				if ((result = CachedReflectionInfo.s_Sum_Int64_1) == null)
				{
					result = (CachedReflectionInfo.s_Sum_Int64_1 = new Func<IQueryable<long>, long>(Queryable.Sum).GetMethodInfo());
				}
				return result;
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060003E9 RID: 1001 RVA: 0x0000B74D File Offset: 0x0000994D
		public static MethodInfo Sum_NullableInt64_1
		{
			get
			{
				MethodInfo result;
				if ((result = CachedReflectionInfo.s_Sum_NullableInt64_1) == null)
				{
					result = (CachedReflectionInfo.s_Sum_NullableInt64_1 = new Func<IQueryable<long?>, long?>(Queryable.Sum).GetMethodInfo());
				}
				return result;
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060003EA RID: 1002 RVA: 0x0000B76F File Offset: 0x0000996F
		public static MethodInfo Sum_Single_1
		{
			get
			{
				MethodInfo result;
				if ((result = CachedReflectionInfo.s_Sum_Single_1) == null)
				{
					result = (CachedReflectionInfo.s_Sum_Single_1 = new Func<IQueryable<float>, float>(Queryable.Sum).GetMethodInfo());
				}
				return result;
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060003EB RID: 1003 RVA: 0x0000B791 File Offset: 0x00009991
		public static MethodInfo Sum_NullableSingle_1
		{
			get
			{
				MethodInfo result;
				if ((result = CachedReflectionInfo.s_Sum_NullableSingle_1) == null)
				{
					result = (CachedReflectionInfo.s_Sum_NullableSingle_1 = new Func<IQueryable<float?>, float?>(Queryable.Sum).GetMethodInfo());
				}
				return result;
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060003EC RID: 1004 RVA: 0x0000B7B3 File Offset: 0x000099B3
		public static MethodInfo Sum_Double_1
		{
			get
			{
				MethodInfo result;
				if ((result = CachedReflectionInfo.s_Sum_Double_1) == null)
				{
					result = (CachedReflectionInfo.s_Sum_Double_1 = new Func<IQueryable<double>, double>(Queryable.Sum).GetMethodInfo());
				}
				return result;
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060003ED RID: 1005 RVA: 0x0000B7D5 File Offset: 0x000099D5
		public static MethodInfo Sum_NullableDouble_1
		{
			get
			{
				MethodInfo result;
				if ((result = CachedReflectionInfo.s_Sum_NullableDouble_1) == null)
				{
					result = (CachedReflectionInfo.s_Sum_NullableDouble_1 = new Func<IQueryable<double?>, double?>(Queryable.Sum).GetMethodInfo());
				}
				return result;
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060003EE RID: 1006 RVA: 0x0000B7F7 File Offset: 0x000099F7
		public static MethodInfo Sum_Decimal_1
		{
			get
			{
				MethodInfo result;
				if ((result = CachedReflectionInfo.s_Sum_Decimal_1) == null)
				{
					result = (CachedReflectionInfo.s_Sum_Decimal_1 = new Func<IQueryable<decimal>, decimal>(Queryable.Sum).GetMethodInfo());
				}
				return result;
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060003EF RID: 1007 RVA: 0x0000B819 File Offset: 0x00009A19
		public static MethodInfo Sum_NullableDecimal_1
		{
			get
			{
				MethodInfo result;
				if ((result = CachedReflectionInfo.s_Sum_NullableDecimal_1) == null)
				{
					result = (CachedReflectionInfo.s_Sum_NullableDecimal_1 = new Func<IQueryable<decimal?>, decimal?>(Queryable.Sum).GetMethodInfo());
				}
				return result;
			}
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x0000B83B File Offset: 0x00009A3B
		public static MethodInfo Sum_NullableDecimal_TSource_2(Type TSource)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_Sum_NullableDecimal_TSource_2) == null)
			{
				methodInfo = (CachedReflectionInfo.s_Sum_NullableDecimal_TSource_2 = new Func<IQueryable<object>, Expression<Func<object, decimal?>>, decimal?>(Queryable.Sum<object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[]
			{
				TSource
			});
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x0000B871 File Offset: 0x00009A71
		public static MethodInfo Sum_Int32_TSource_2(Type TSource)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_Sum_Int32_TSource_2) == null)
			{
				methodInfo = (CachedReflectionInfo.s_Sum_Int32_TSource_2 = new Func<IQueryable<object>, Expression<Func<object, int>>, int>(Queryable.Sum<object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[]
			{
				TSource
			});
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x0000B8A7 File Offset: 0x00009AA7
		public static MethodInfo Sum_NullableInt32_TSource_2(Type TSource)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_Sum_NullableInt32_TSource_2) == null)
			{
				methodInfo = (CachedReflectionInfo.s_Sum_NullableInt32_TSource_2 = new Func<IQueryable<object>, Expression<Func<object, int?>>, int?>(Queryable.Sum<object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[]
			{
				TSource
			});
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x0000B8DD File Offset: 0x00009ADD
		public static MethodInfo Sum_Int64_TSource_2(Type TSource)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_Sum_Int64_TSource_2) == null)
			{
				methodInfo = (CachedReflectionInfo.s_Sum_Int64_TSource_2 = new Func<IQueryable<object>, Expression<Func<object, long>>, long>(Queryable.Sum<object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[]
			{
				TSource
			});
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x0000B913 File Offset: 0x00009B13
		public static MethodInfo Sum_NullableInt64_TSource_2(Type TSource)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_Sum_NullableInt64_TSource_2) == null)
			{
				methodInfo = (CachedReflectionInfo.s_Sum_NullableInt64_TSource_2 = new Func<IQueryable<object>, Expression<Func<object, long?>>, long?>(Queryable.Sum<object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[]
			{
				TSource
			});
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x0000B949 File Offset: 0x00009B49
		public static MethodInfo Sum_Single_TSource_2(Type TSource)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_Sum_Single_TSource_2) == null)
			{
				methodInfo = (CachedReflectionInfo.s_Sum_Single_TSource_2 = new Func<IQueryable<object>, Expression<Func<object, float>>, float>(Queryable.Sum<object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[]
			{
				TSource
			});
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x0000B97F File Offset: 0x00009B7F
		public static MethodInfo Sum_NullableSingle_TSource_2(Type TSource)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_Sum_NullableSingle_TSource_2) == null)
			{
				methodInfo = (CachedReflectionInfo.s_Sum_NullableSingle_TSource_2 = new Func<IQueryable<object>, Expression<Func<object, float?>>, float?>(Queryable.Sum<object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[]
			{
				TSource
			});
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x0000B9B5 File Offset: 0x00009BB5
		public static MethodInfo Sum_Double_TSource_2(Type TSource)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_Sum_Double_TSource_2) == null)
			{
				methodInfo = (CachedReflectionInfo.s_Sum_Double_TSource_2 = new Func<IQueryable<object>, Expression<Func<object, double>>, double>(Queryable.Sum<object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[]
			{
				TSource
			});
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x0000B9EB File Offset: 0x00009BEB
		public static MethodInfo Sum_NullableDouble_TSource_2(Type TSource)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_Sum_NullableDouble_TSource_2) == null)
			{
				methodInfo = (CachedReflectionInfo.s_Sum_NullableDouble_TSource_2 = new Func<IQueryable<object>, Expression<Func<object, double?>>, double?>(Queryable.Sum<object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[]
			{
				TSource
			});
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x0000BA21 File Offset: 0x00009C21
		public static MethodInfo Sum_Decimal_TSource_2(Type TSource)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_Sum_Decimal_TSource_2) == null)
			{
				methodInfo = (CachedReflectionInfo.s_Sum_Decimal_TSource_2 = new Func<IQueryable<object>, Expression<Func<object, decimal>>, decimal>(Queryable.Sum<object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[]
			{
				TSource
			});
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x0000BA57 File Offset: 0x00009C57
		public static MethodInfo Take_TSource_2(Type TSource)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_Take_TSource_2) == null)
			{
				methodInfo = (CachedReflectionInfo.s_Take_TSource_2 = new Func<IQueryable<object>, int, IQueryable<object>>(Queryable.Take<object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[]
			{
				TSource
			});
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x0000BA8D File Offset: 0x00009C8D
		public static MethodInfo TakeWhile_TSource_2(Type TSource)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_TakeWhile_TSource_2) == null)
			{
				methodInfo = (CachedReflectionInfo.s_TakeWhile_TSource_2 = new Func<IQueryable<object>, Expression<Func<object, bool>>, IQueryable<object>>(Queryable.TakeWhile<object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[]
			{
				TSource
			});
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x0000BAC3 File Offset: 0x00009CC3
		public static MethodInfo TakeWhile_Index_TSource_2(Type TSource)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_TakeWhile_Index_TSource_2) == null)
			{
				methodInfo = (CachedReflectionInfo.s_TakeWhile_Index_TSource_2 = new Func<IQueryable<object>, Expression<Func<object, int, bool>>, IQueryable<object>>(Queryable.TakeWhile<object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[]
			{
				TSource
			});
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x0000BAF9 File Offset: 0x00009CF9
		public static MethodInfo ThenBy_TSource_TKey_2(Type TSource, Type TKey)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_ThenBy_TSource_TKey_2) == null)
			{
				methodInfo = (CachedReflectionInfo.s_ThenBy_TSource_TKey_2 = new Func<IOrderedQueryable<object>, Expression<Func<object, object>>, IOrderedQueryable<object>>(Queryable.ThenBy<object, object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[]
			{
				TSource,
				TKey
			});
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x0000BB33 File Offset: 0x00009D33
		public static MethodInfo ThenBy_TSource_TKey_3(Type TSource, Type TKey)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_ThenBy_TSource_TKey_3) == null)
			{
				methodInfo = (CachedReflectionInfo.s_ThenBy_TSource_TKey_3 = new Func<IOrderedQueryable<object>, Expression<Func<object, object>>, IComparer<object>, IOrderedQueryable<object>>(Queryable.ThenBy<object, object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[]
			{
				TSource,
				TKey
			});
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x0000BB6D File Offset: 0x00009D6D
		public static MethodInfo ThenByDescending_TSource_TKey_2(Type TSource, Type TKey)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_ThenByDescending_TSource_TKey_2) == null)
			{
				methodInfo = (CachedReflectionInfo.s_ThenByDescending_TSource_TKey_2 = new Func<IOrderedQueryable<object>, Expression<Func<object, object>>, IOrderedQueryable<object>>(Queryable.ThenByDescending<object, object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[]
			{
				TSource,
				TKey
			});
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x0000BBA7 File Offset: 0x00009DA7
		public static MethodInfo ThenByDescending_TSource_TKey_3(Type TSource, Type TKey)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_ThenByDescending_TSource_TKey_3) == null)
			{
				methodInfo = (CachedReflectionInfo.s_ThenByDescending_TSource_TKey_3 = new Func<IOrderedQueryable<object>, Expression<Func<object, object>>, IComparer<object>, IOrderedQueryable<object>>(Queryable.ThenByDescending<object, object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[]
			{
				TSource,
				TKey
			});
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x0000BBE1 File Offset: 0x00009DE1
		public static MethodInfo Union_TSource_2(Type TSource)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_Union_TSource_2) == null)
			{
				methodInfo = (CachedReflectionInfo.s_Union_TSource_2 = new Func<IQueryable<object>, IEnumerable<object>, IQueryable<object>>(Queryable.Union<object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[]
			{
				TSource
			});
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x0000BC17 File Offset: 0x00009E17
		public static MethodInfo Union_TSource_3(Type TSource)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_Union_TSource_3) == null)
			{
				methodInfo = (CachedReflectionInfo.s_Union_TSource_3 = new Func<IQueryable<object>, IEnumerable<object>, IEqualityComparer<object>, IQueryable<object>>(Queryable.Union<object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[]
			{
				TSource
			});
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x0000BC4D File Offset: 0x00009E4D
		public static MethodInfo Where_TSource_2(Type TSource)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_Where_TSource_2) == null)
			{
				methodInfo = (CachedReflectionInfo.s_Where_TSource_2 = new Func<IQueryable<object>, Expression<Func<object, bool>>, IQueryable<object>>(Queryable.Where<object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[]
			{
				TSource
			});
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x0000BC83 File Offset: 0x00009E83
		public static MethodInfo Where_Index_TSource_2(Type TSource)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_Where_Index_TSource_2) == null)
			{
				methodInfo = (CachedReflectionInfo.s_Where_Index_TSource_2 = new Func<IQueryable<object>, Expression<Func<object, int, bool>>, IQueryable<object>>(Queryable.Where<object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[]
			{
				TSource
			});
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x0000BCB9 File Offset: 0x00009EB9
		public static MethodInfo Zip_TFirst_TSecond_TResult_3(Type TFirst, Type TSecond, Type TResult)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_Zip_TFirst_TSecond_TResult_3) == null)
			{
				methodInfo = (CachedReflectionInfo.s_Zip_TFirst_TSecond_TResult_3 = new Func<IQueryable<object>, IEnumerable<object>, Expression<Func<object, object, object>>, IQueryable<object>>(Queryable.Zip<object, object, object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[]
			{
				TFirst,
				TSecond,
				TResult
			});
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x0000BCF7 File Offset: 0x00009EF7
		public static MethodInfo SkipLast_TSource_2(Type TSource)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_SkipLast_TSource_2) == null)
			{
				methodInfo = (CachedReflectionInfo.s_SkipLast_TSource_2 = new Func<IQueryable<object>, int, IQueryable<object>>(Queryable.SkipLast<object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[]
			{
				TSource
			});
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x0000BD2D File Offset: 0x00009F2D
		public static MethodInfo TakeLast_TSource_2(Type TSource)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_TakeLast_TSource_2) == null)
			{
				methodInfo = (CachedReflectionInfo.s_TakeLast_TSource_2 = new Func<IQueryable<object>, int, IQueryable<object>>(Queryable.TakeLast<object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[]
			{
				TSource
			});
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x0000BD63 File Offset: 0x00009F63
		public static MethodInfo Append_TSource_2(Type TSource)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_Append_TSource_2) == null)
			{
				methodInfo = (CachedReflectionInfo.s_Append_TSource_2 = new Func<IQueryable<object>, object, IQueryable<object>>(Queryable.Append<object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[]
			{
				TSource
			});
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x0000BD99 File Offset: 0x00009F99
		public static MethodInfo Prepend_TSource_2(Type TSource)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_Prepend_TSource_2) == null)
			{
				methodInfo = (CachedReflectionInfo.s_Prepend_TSource_2 = new Func<IQueryable<object>, object, IQueryable<object>>(Queryable.Prepend<object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[]
			{
				TSource
			});
		}

		// Token: 0x040003CA RID: 970
		private static MethodInfo s_Aggregate_TSource_2;

		// Token: 0x040003CB RID: 971
		private static MethodInfo s_Aggregate_TSource_TAccumulate_3;

		// Token: 0x040003CC RID: 972
		private static MethodInfo s_Aggregate_TSource_TAccumulate_TResult_4;

		// Token: 0x040003CD RID: 973
		private static MethodInfo s_All_TSource_2;

		// Token: 0x040003CE RID: 974
		private static MethodInfo s_Any_TSource_1;

		// Token: 0x040003CF RID: 975
		private static MethodInfo s_Any_TSource_2;

		// Token: 0x040003D0 RID: 976
		private static MethodInfo s_Average_Int32_1;

		// Token: 0x040003D1 RID: 977
		private static MethodInfo s_Average_NullableInt32_1;

		// Token: 0x040003D2 RID: 978
		private static MethodInfo s_Average_Int64_1;

		// Token: 0x040003D3 RID: 979
		private static MethodInfo s_Average_NullableInt64_1;

		// Token: 0x040003D4 RID: 980
		private static MethodInfo s_Average_Single_1;

		// Token: 0x040003D5 RID: 981
		private static MethodInfo s_Average_NullableSingle_1;

		// Token: 0x040003D6 RID: 982
		private static MethodInfo s_Average_Double_1;

		// Token: 0x040003D7 RID: 983
		private static MethodInfo s_Average_NullableDouble_1;

		// Token: 0x040003D8 RID: 984
		private static MethodInfo s_Average_Decimal_1;

		// Token: 0x040003D9 RID: 985
		private static MethodInfo s_Average_NullableDecimal_1;

		// Token: 0x040003DA RID: 986
		private static MethodInfo s_Average_Int32_TSource_2;

		// Token: 0x040003DB RID: 987
		private static MethodInfo s_Average_NullableInt32_TSource_2;

		// Token: 0x040003DC RID: 988
		private static MethodInfo s_Average_Single_TSource_2;

		// Token: 0x040003DD RID: 989
		private static MethodInfo s_Average_NullableSingle_TSource_2;

		// Token: 0x040003DE RID: 990
		private static MethodInfo s_Average_Int64_TSource_2;

		// Token: 0x040003DF RID: 991
		private static MethodInfo s_Average_NullableInt64_TSource_2;

		// Token: 0x040003E0 RID: 992
		private static MethodInfo s_Average_Double_TSource_2;

		// Token: 0x040003E1 RID: 993
		private static MethodInfo s_Average_NullableDouble_TSource_2;

		// Token: 0x040003E2 RID: 994
		private static MethodInfo s_Average_Decimal_TSource_2;

		// Token: 0x040003E3 RID: 995
		private static MethodInfo s_Average_NullableDecimal_TSource_2;

		// Token: 0x040003E4 RID: 996
		private static MethodInfo s_Cast_TResult_1;

		// Token: 0x040003E5 RID: 997
		private static MethodInfo s_Concat_TSource_2;

		// Token: 0x040003E6 RID: 998
		private static MethodInfo s_Contains_TSource_2;

		// Token: 0x040003E7 RID: 999
		private static MethodInfo s_Contains_TSource_3;

		// Token: 0x040003E8 RID: 1000
		private static MethodInfo s_Count_TSource_1;

		// Token: 0x040003E9 RID: 1001
		private static MethodInfo s_Count_TSource_2;

		// Token: 0x040003EA RID: 1002
		private static MethodInfo s_DefaultIfEmpty_TSource_1;

		// Token: 0x040003EB RID: 1003
		private static MethodInfo s_DefaultIfEmpty_TSource_2;

		// Token: 0x040003EC RID: 1004
		private static MethodInfo s_Distinct_TSource_1;

		// Token: 0x040003ED RID: 1005
		private static MethodInfo s_Distinct_TSource_2;

		// Token: 0x040003EE RID: 1006
		private static MethodInfo s_ElementAt_TSource_2;

		// Token: 0x040003EF RID: 1007
		private static MethodInfo s_ElementAtOrDefault_TSource_2;

		// Token: 0x040003F0 RID: 1008
		private static MethodInfo s_Except_TSource_2;

		// Token: 0x040003F1 RID: 1009
		private static MethodInfo s_Except_TSource_3;

		// Token: 0x040003F2 RID: 1010
		private static MethodInfo s_First_TSource_1;

		// Token: 0x040003F3 RID: 1011
		private static MethodInfo s_First_TSource_2;

		// Token: 0x040003F4 RID: 1012
		private static MethodInfo s_FirstOrDefault_TSource_1;

		// Token: 0x040003F5 RID: 1013
		private static MethodInfo s_FirstOrDefault_TSource_2;

		// Token: 0x040003F6 RID: 1014
		private static MethodInfo s_GroupBy_TSource_TKey_2;

		// Token: 0x040003F7 RID: 1015
		private static MethodInfo s_GroupBy_TSource_TKey_3;

		// Token: 0x040003F8 RID: 1016
		private static MethodInfo s_GroupBy_TSource_TKey_TElement_3;

		// Token: 0x040003F9 RID: 1017
		private static MethodInfo s_GroupBy_TSource_TKey_TElement_4;

		// Token: 0x040003FA RID: 1018
		private static MethodInfo s_GroupBy_TSource_TKey_TResult_3;

		// Token: 0x040003FB RID: 1019
		private static MethodInfo s_GroupBy_TSource_TKey_TResult_4;

		// Token: 0x040003FC RID: 1020
		private static MethodInfo s_GroupBy_TSource_TKey_TElement_TResult_4;

		// Token: 0x040003FD RID: 1021
		private static MethodInfo s_GroupBy_TSource_TKey_TElement_TResult_5;

		// Token: 0x040003FE RID: 1022
		private static MethodInfo s_GroupJoin_TOuter_TInner_TKey_TResult_5;

		// Token: 0x040003FF RID: 1023
		private static MethodInfo s_GroupJoin_TOuter_TInner_TKey_TResult_6;

		// Token: 0x04000400 RID: 1024
		private static MethodInfo s_Intersect_TSource_2;

		// Token: 0x04000401 RID: 1025
		private static MethodInfo s_Intersect_TSource_3;

		// Token: 0x04000402 RID: 1026
		private static MethodInfo s_Join_TOuter_TInner_TKey_TResult_5;

		// Token: 0x04000403 RID: 1027
		private static MethodInfo s_Join_TOuter_TInner_TKey_TResult_6;

		// Token: 0x04000404 RID: 1028
		private static MethodInfo s_Last_TSource_1;

		// Token: 0x04000405 RID: 1029
		private static MethodInfo s_Last_TSource_2;

		// Token: 0x04000406 RID: 1030
		private static MethodInfo s_LastOrDefault_TSource_1;

		// Token: 0x04000407 RID: 1031
		private static MethodInfo s_LastOrDefault_TSource_2;

		// Token: 0x04000408 RID: 1032
		private static MethodInfo s_LongCount_TSource_1;

		// Token: 0x04000409 RID: 1033
		private static MethodInfo s_LongCount_TSource_2;

		// Token: 0x0400040A RID: 1034
		private static MethodInfo s_Max_TSource_1;

		// Token: 0x0400040B RID: 1035
		private static MethodInfo s_Max_TSource_TResult_2;

		// Token: 0x0400040C RID: 1036
		private static MethodInfo s_Min_TSource_1;

		// Token: 0x0400040D RID: 1037
		private static MethodInfo s_Min_TSource_TResult_2;

		// Token: 0x0400040E RID: 1038
		private static MethodInfo s_OfType_TResult_1;

		// Token: 0x0400040F RID: 1039
		private static MethodInfo s_OrderBy_TSource_TKey_2;

		// Token: 0x04000410 RID: 1040
		private static MethodInfo s_OrderBy_TSource_TKey_3;

		// Token: 0x04000411 RID: 1041
		private static MethodInfo s_OrderByDescending_TSource_TKey_2;

		// Token: 0x04000412 RID: 1042
		private static MethodInfo s_OrderByDescending_TSource_TKey_3;

		// Token: 0x04000413 RID: 1043
		private static MethodInfo s_Reverse_TSource_1;

		// Token: 0x04000414 RID: 1044
		private static MethodInfo s_Select_TSource_TResult_2;

		// Token: 0x04000415 RID: 1045
		private static MethodInfo s_Select_Index_TSource_TResult_2;

		// Token: 0x04000416 RID: 1046
		private static MethodInfo s_SelectMany_TSource_TResult_2;

		// Token: 0x04000417 RID: 1047
		private static MethodInfo s_SelectMany_Index_TSource_TResult_2;

		// Token: 0x04000418 RID: 1048
		private static MethodInfo s_SelectMany_Index_TSource_TCollection_TResult_3;

		// Token: 0x04000419 RID: 1049
		private static MethodInfo s_SelectMany_TSource_TCollection_TResult_3;

		// Token: 0x0400041A RID: 1050
		private static MethodInfo s_SequenceEqual_TSource_2;

		// Token: 0x0400041B RID: 1051
		private static MethodInfo s_SequenceEqual_TSource_3;

		// Token: 0x0400041C RID: 1052
		private static MethodInfo s_Single_TSource_1;

		// Token: 0x0400041D RID: 1053
		private static MethodInfo s_Single_TSource_2;

		// Token: 0x0400041E RID: 1054
		private static MethodInfo s_SingleOrDefault_TSource_1;

		// Token: 0x0400041F RID: 1055
		private static MethodInfo s_SingleOrDefault_TSource_2;

		// Token: 0x04000420 RID: 1056
		private static MethodInfo s_Skip_TSource_2;

		// Token: 0x04000421 RID: 1057
		private static MethodInfo s_SkipWhile_TSource_2;

		// Token: 0x04000422 RID: 1058
		private static MethodInfo s_SkipWhile_Index_TSource_2;

		// Token: 0x04000423 RID: 1059
		private static MethodInfo s_Sum_Int32_1;

		// Token: 0x04000424 RID: 1060
		private static MethodInfo s_Sum_NullableInt32_1;

		// Token: 0x04000425 RID: 1061
		private static MethodInfo s_Sum_Int64_1;

		// Token: 0x04000426 RID: 1062
		private static MethodInfo s_Sum_NullableInt64_1;

		// Token: 0x04000427 RID: 1063
		private static MethodInfo s_Sum_Single_1;

		// Token: 0x04000428 RID: 1064
		private static MethodInfo s_Sum_NullableSingle_1;

		// Token: 0x04000429 RID: 1065
		private static MethodInfo s_Sum_Double_1;

		// Token: 0x0400042A RID: 1066
		private static MethodInfo s_Sum_NullableDouble_1;

		// Token: 0x0400042B RID: 1067
		private static MethodInfo s_Sum_Decimal_1;

		// Token: 0x0400042C RID: 1068
		private static MethodInfo s_Sum_NullableDecimal_1;

		// Token: 0x0400042D RID: 1069
		private static MethodInfo s_Sum_NullableDecimal_TSource_2;

		// Token: 0x0400042E RID: 1070
		private static MethodInfo s_Sum_Int32_TSource_2;

		// Token: 0x0400042F RID: 1071
		private static MethodInfo s_Sum_NullableInt32_TSource_2;

		// Token: 0x04000430 RID: 1072
		private static MethodInfo s_Sum_Int64_TSource_2;

		// Token: 0x04000431 RID: 1073
		private static MethodInfo s_Sum_NullableInt64_TSource_2;

		// Token: 0x04000432 RID: 1074
		private static MethodInfo s_Sum_Single_TSource_2;

		// Token: 0x04000433 RID: 1075
		private static MethodInfo s_Sum_NullableSingle_TSource_2;

		// Token: 0x04000434 RID: 1076
		private static MethodInfo s_Sum_Double_TSource_2;

		// Token: 0x04000435 RID: 1077
		private static MethodInfo s_Sum_NullableDouble_TSource_2;

		// Token: 0x04000436 RID: 1078
		private static MethodInfo s_Sum_Decimal_TSource_2;

		// Token: 0x04000437 RID: 1079
		private static MethodInfo s_Take_TSource_2;

		// Token: 0x04000438 RID: 1080
		private static MethodInfo s_TakeWhile_TSource_2;

		// Token: 0x04000439 RID: 1081
		private static MethodInfo s_TakeWhile_Index_TSource_2;

		// Token: 0x0400043A RID: 1082
		private static MethodInfo s_ThenBy_TSource_TKey_2;

		// Token: 0x0400043B RID: 1083
		private static MethodInfo s_ThenBy_TSource_TKey_3;

		// Token: 0x0400043C RID: 1084
		private static MethodInfo s_ThenByDescending_TSource_TKey_2;

		// Token: 0x0400043D RID: 1085
		private static MethodInfo s_ThenByDescending_TSource_TKey_3;

		// Token: 0x0400043E RID: 1086
		private static MethodInfo s_Union_TSource_2;

		// Token: 0x0400043F RID: 1087
		private static MethodInfo s_Union_TSource_3;

		// Token: 0x04000440 RID: 1088
		private static MethodInfo s_Where_TSource_2;

		// Token: 0x04000441 RID: 1089
		private static MethodInfo s_Where_Index_TSource_2;

		// Token: 0x04000442 RID: 1090
		private static MethodInfo s_Zip_TFirst_TSecond_TResult_3;

		// Token: 0x04000443 RID: 1091
		private static MethodInfo s_SkipLast_TSource_2;

		// Token: 0x04000444 RID: 1092
		private static MethodInfo s_TakeLast_TSource_2;

		// Token: 0x04000445 RID: 1093
		private static MethodInfo s_Append_TSource_2;

		// Token: 0x04000446 RID: 1094
		private static MethodInfo s_Prepend_TSource_2;
	}
}
