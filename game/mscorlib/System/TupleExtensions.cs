using System;
using System.Runtime.CompilerServices;

namespace System
{
	/// <summary>Provides extension methods for tuples to interoperate with language support for tuples in C#.</summary>
	// Token: 0x020001A0 RID: 416
	public static class TupleExtensions
	{
		/// <summary>Deconstructs a tuple with 1 element into a separate variable.</summary>
		/// <param name="value">The 1-element tuple to deconstruct into a separate variable.</param>
		/// <param name="item1">The value of the single element.</param>
		/// <typeparam name="T1">The type of the single element.</typeparam>
		// Token: 0x060010C8 RID: 4296 RVA: 0x00044331 File Offset: 0x00042531
		public static void Deconstruct<T1>(this Tuple<T1> value, out T1 item1)
		{
			item1 = value.Item1;
		}

		/// <summary>Deconstructs a tuple with 2 elements into separate variables.</summary>
		/// <param name="value">The 2-element tuple to deconstruct into 2 separate variables.</param>
		/// <param name="item1">The value of the first element.</param>
		/// <param name="item2">The value of the second element.</param>
		/// <typeparam name="T1">The type of the first element.</typeparam>
		/// <typeparam name="T2">The type of the second element.</typeparam>
		// Token: 0x060010C9 RID: 4297 RVA: 0x0004433F File Offset: 0x0004253F
		public static void Deconstruct<T1, T2>(this Tuple<T1, T2> value, out T1 item1, out T2 item2)
		{
			item1 = value.Item1;
			item2 = value.Item2;
		}

		/// <summary>Deconstructs a tuple with 3 elements into separate variables.</summary>
		/// <param name="value">The 3-element tuple to deconstruct into 3 separate variables.</param>
		/// <param name="item1">The value of the first element.</param>
		/// <param name="item2">The value of the second element.</param>
		/// <param name="item3">The value of the third element.</param>
		/// <typeparam name="T1">The type of the first element.</typeparam>
		/// <typeparam name="T2">The type of the second element.</typeparam>
		/// <typeparam name="T3">The type of the third element.</typeparam>
		// Token: 0x060010CA RID: 4298 RVA: 0x00044359 File Offset: 0x00042559
		public static void Deconstruct<T1, T2, T3>(this Tuple<T1, T2, T3> value, out T1 item1, out T2 item2, out T3 item3)
		{
			item1 = value.Item1;
			item2 = value.Item2;
			item3 = value.Item3;
		}

		/// <summary>Deconstructs a tuple with 4 elements into separate variables.</summary>
		/// <param name="value">The 4-element tuple to deconstruct into 4 separate variables.</param>
		/// <param name="item1">The value of the first element.</param>
		/// <param name="item2">The value of the second element.</param>
		/// <param name="item3">The value of the third element.</param>
		/// <param name="item4">The value of the fourth element.</param>
		/// <typeparam name="T1">The type of the first element.</typeparam>
		/// <typeparam name="T2">The type of the second element.</typeparam>
		/// <typeparam name="T3">The type of the third element.</typeparam>
		/// <typeparam name="T4">The type of the fourth element.</typeparam>
		// Token: 0x060010CB RID: 4299 RVA: 0x0004437F File Offset: 0x0004257F
		public static void Deconstruct<T1, T2, T3, T4>(this Tuple<T1, T2, T3, T4> value, out T1 item1, out T2 item2, out T3 item3, out T4 item4)
		{
			item1 = value.Item1;
			item2 = value.Item2;
			item3 = value.Item3;
			item4 = value.Item4;
		}

		/// <summary>Deconstructs a tuple with 5 elements into separate variables.</summary>
		/// <param name="value">The 5-element tuple to deconstruct into 5 separate variables.</param>
		/// <param name="item1">The value of the first element.</param>
		/// <param name="item2">The value of the second element.</param>
		/// <param name="item3">The value of the third element.</param>
		/// <param name="item4">The value of the fourth element.</param>
		/// <param name="item5">The value of the fifth element.</param>
		/// <typeparam name="T1">The type of the first element.</typeparam>
		/// <typeparam name="T2">The type of the second element.</typeparam>
		/// <typeparam name="T3">The type of the third element.</typeparam>
		/// <typeparam name="T4">The type of the fourth element.</typeparam>
		/// <typeparam name="T5">The type of the fifth element.</typeparam>
		// Token: 0x060010CC RID: 4300 RVA: 0x000443B2 File Offset: 0x000425B2
		public static void Deconstruct<T1, T2, T3, T4, T5>(this Tuple<T1, T2, T3, T4, T5> value, out T1 item1, out T2 item2, out T3 item3, out T4 item4, out T5 item5)
		{
			item1 = value.Item1;
			item2 = value.Item2;
			item3 = value.Item3;
			item4 = value.Item4;
			item5 = value.Item5;
		}

		/// <summary>Deconstructs a tuple with 6 elements into separate variables.</summary>
		/// <param name="value">The 6-element tuple to deconstruct into 6 separate variables.</param>
		/// <param name="item1">The value of the first element.</param>
		/// <param name="item2">The value of the second element.</param>
		/// <param name="item3">The value of the third element.</param>
		/// <param name="item4">The value of the fourth element.</param>
		/// <param name="item5">The value of the fifth element.</param>
		/// <param name="item6">The value of the sixth element.</param>
		/// <typeparam name="T1">The type of the first element.</typeparam>
		/// <typeparam name="T2">The type of the second element.</typeparam>
		/// <typeparam name="T3">The type of the third element.</typeparam>
		/// <typeparam name="T4">The type of the fourth element.</typeparam>
		/// <typeparam name="T5">The type of the fifth element.</typeparam>
		/// <typeparam name="T6">The type of the sixth element.</typeparam>
		// Token: 0x060010CD RID: 4301 RVA: 0x000443F4 File Offset: 0x000425F4
		public static void Deconstruct<T1, T2, T3, T4, T5, T6>(this Tuple<T1, T2, T3, T4, T5, T6> value, out T1 item1, out T2 item2, out T3 item3, out T4 item4, out T5 item5, out T6 item6)
		{
			item1 = value.Item1;
			item2 = value.Item2;
			item3 = value.Item3;
			item4 = value.Item4;
			item5 = value.Item5;
			item6 = value.Item6;
		}

		/// <summary>Deconstructs a tuple with 7 elements into separate variables.</summary>
		/// <param name="value">The 7-element tuple to deconstruct into 7 separate variables.</param>
		/// <param name="item1">The value of the first element.</param>
		/// <param name="item2">The value of the second element.</param>
		/// <param name="item3">The value of the third element.</param>
		/// <param name="item4">The value of the fourth element.</param>
		/// <param name="item5">The value of the fifth element.</param>
		/// <param name="item6">The value of the sixth element.</param>
		/// <param name="item7">The value of the seventh element.</param>
		/// <typeparam name="T1">The type of the first element.</typeparam>
		/// <typeparam name="T2">The type of the second element.</typeparam>
		/// <typeparam name="T3">The type of the third element.</typeparam>
		/// <typeparam name="T4">The type of the fourth element.</typeparam>
		/// <typeparam name="T5">The type of the fifth element.</typeparam>
		/// <typeparam name="T6">The type of the sixth element.</typeparam>
		/// <typeparam name="T7">The type of the seventh element.</typeparam>
		// Token: 0x060010CE RID: 4302 RVA: 0x0004444C File Offset: 0x0004264C
		public static void Deconstruct<T1, T2, T3, T4, T5, T6, T7>(this Tuple<T1, T2, T3, T4, T5, T6, T7> value, out T1 item1, out T2 item2, out T3 item3, out T4 item4, out T5 item5, out T6 item6, out T7 item7)
		{
			item1 = value.Item1;
			item2 = value.Item2;
			item3 = value.Item3;
			item4 = value.Item4;
			item5 = value.Item5;
			item6 = value.Item6;
			item7 = value.Item7;
		}

		/// <summary>Deconstructs a tuple with 8 elements into separate variables.</summary>
		/// <param name="value">The 8-element tuple to deconstruct into 8 separate variables.</param>
		/// <param name="item1">The value of the first element.</param>
		/// <param name="item2">The value of the second element.</param>
		/// <param name="item3">The value of the third element.</param>
		/// <param name="item4">The value of the fourth element.</param>
		/// <param name="item5">The value of the fifth element.</param>
		/// <param name="item6">The value of the sixth element.</param>
		/// <param name="item7">The value of the seventh element.</param>
		/// <param name="item8">The value of the eighth element, or <paramref name="value" /><see langword=".Rest.Item1" />.</param>
		/// <typeparam name="T1">The type of the first element.</typeparam>
		/// <typeparam name="T2">The type of the second element.</typeparam>
		/// <typeparam name="T3">The type of the third element.</typeparam>
		/// <typeparam name="T4">The type of the fourth element.</typeparam>
		/// <typeparam name="T5">The type of the fifth element.</typeparam>
		/// <typeparam name="T6">The type of the sixth element.</typeparam>
		/// <typeparam name="T7">The type of the seventh element.</typeparam>
		/// <typeparam name="T8">The type of the eighth element.</typeparam>
		// Token: 0x060010CF RID: 4303 RVA: 0x000444B4 File Offset: 0x000426B4
		public static void Deconstruct<T1, T2, T3, T4, T5, T6, T7, T8>(this Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8>> value, out T1 item1, out T2 item2, out T3 item3, out T4 item4, out T5 item5, out T6 item6, out T7 item7, out T8 item8)
		{
			item1 = value.Item1;
			item2 = value.Item2;
			item3 = value.Item3;
			item4 = value.Item4;
			item5 = value.Item5;
			item6 = value.Item6;
			item7 = value.Item7;
			item8 = value.Rest.Item1;
		}

		/// <summary>Deconstructs a tuple with 9 elements into separate variables.</summary>
		/// <param name="value">The 9-element tuple to deconstruct into 9 separate variables.</param>
		/// <param name="item1">The value of the first element.</param>
		/// <param name="item2">The value of the second element.</param>
		/// <param name="item3">The value of the third element.</param>
		/// <param name="item4">The value of the fourth element.</param>
		/// <param name="item5">The value of the fifth element.</param>
		/// <param name="item6">The value of the sixth element.</param>
		/// <param name="item7">The value of the seventh element.</param>
		/// <param name="item8">The value of the eighth element, or <paramref name="value" /><see langword=".Rest.Item1" />.</param>
		/// <param name="item9">The value of the ninth element, or <paramref name="value" /><see langword=".Rest.Item2" />.</param>
		/// <typeparam name="T1">The type of the first element.</typeparam>
		/// <typeparam name="T2">The type of the second element.</typeparam>
		/// <typeparam name="T3">The type of the third element.</typeparam>
		/// <typeparam name="T4">The type of the fourth element.</typeparam>
		/// <typeparam name="T5">The type of the fifth element.</typeparam>
		/// <typeparam name="T6">The type of the sixth element.</typeparam>
		/// <typeparam name="T7">The type of the seventh element.</typeparam>
		/// <typeparam name="T8">The type of the eighth element.</typeparam>
		/// <typeparam name="T9">The type of the ninth element.</typeparam>
		// Token: 0x060010D0 RID: 4304 RVA: 0x0004452C File Offset: 0x0004272C
		public static void Deconstruct<T1, T2, T3, T4, T5, T6, T7, T8, T9>(this Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9>> value, out T1 item1, out T2 item2, out T3 item3, out T4 item4, out T5 item5, out T6 item6, out T7 item7, out T8 item8, out T9 item9)
		{
			item1 = value.Item1;
			item2 = value.Item2;
			item3 = value.Item3;
			item4 = value.Item4;
			item5 = value.Item5;
			item6 = value.Item6;
			item7 = value.Item7;
			item8 = value.Rest.Item1;
			item9 = value.Rest.Item2;
		}

		/// <summary>Deconstructs a tuple with 10 elements into separate variables.</summary>
		/// <param name="value">The 10-element tuple to deconstruct into 10 separate variables.</param>
		/// <param name="item1">The value of the first element.</param>
		/// <param name="item2">The value of the second element.</param>
		/// <param name="item3">The value of the third element.</param>
		/// <param name="item4">The value of the fourth element.</param>
		/// <param name="item5">The value of the fifth element.</param>
		/// <param name="item6">The value of the sixth element.</param>
		/// <param name="item7">The value of the seventh element.</param>
		/// <param name="item8">The value of the eighth element, or <paramref name="value" /><see langword=".Rest.Item1" />.</param>
		/// <param name="item9">The value of the ninth element, or <paramref name="value" /><see langword=".Rest.Item2" />.</param>
		/// <param name="item10">The value of the tenth element, or <paramref name="value" /><see langword=".Rest.Item3" />.</param>
		/// <typeparam name="T1">The type of the first element.</typeparam>
		/// <typeparam name="T2">The type of the second element.</typeparam>
		/// <typeparam name="T3">The type of the third element.</typeparam>
		/// <typeparam name="T4">The type of the fourth element.</typeparam>
		/// <typeparam name="T5">The type of the fifth element.</typeparam>
		/// <typeparam name="T6">The type of the sixth element.</typeparam>
		/// <typeparam name="T7">The type of the seventh element.</typeparam>
		/// <typeparam name="T8">The type of the eighth element.</typeparam>
		/// <typeparam name="T9">The type of the ninth element.</typeparam>
		/// <typeparam name="T10">The type of the tenth element.</typeparam>
		// Token: 0x060010D1 RID: 4305 RVA: 0x000445B8 File Offset: 0x000427B8
		public static void Deconstruct<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(this Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10>> value, out T1 item1, out T2 item2, out T3 item3, out T4 item4, out T5 item5, out T6 item6, out T7 item7, out T8 item8, out T9 item9, out T10 item10)
		{
			item1 = value.Item1;
			item2 = value.Item2;
			item3 = value.Item3;
			item4 = value.Item4;
			item5 = value.Item5;
			item6 = value.Item6;
			item7 = value.Item7;
			item8 = value.Rest.Item1;
			item9 = value.Rest.Item2;
			item10 = value.Rest.Item3;
		}

		/// <summary>Deconstructs a tuple with 11 elements into separate variables.</summary>
		/// <param name="value">The 11-element tuple to deconstruct into 11 separate variables.</param>
		/// <param name="item1">The value of the first element.</param>
		/// <param name="item2">The value of the second element.</param>
		/// <param name="item3">The value of the third element.</param>
		/// <param name="item4">The value of the fourth element.</param>
		/// <param name="item5">The value of the fifth element.</param>
		/// <param name="item6">The value of the sixth element.</param>
		/// <param name="item7">The value of the seventh element.</param>
		/// <param name="item8">The value of the eighth element, or <paramref name="value" /><see langword=".Rest.Item1" />.</param>
		/// <param name="item9">The value of the ninth element, or <paramref name="value" /><see langword=".Rest.Item2" />.</param>
		/// <param name="item10">The value of the tenth element, or <paramref name="value" /><see langword=".Rest.Item3" />.</param>
		/// <param name="item11">The value of the eleventh element, or <paramref name="value" /><see langword=".Rest.Item4" />.</param>
		/// <typeparam name="T1">The type of the first element.</typeparam>
		/// <typeparam name="T2">The type of the second element.</typeparam>
		/// <typeparam name="T3">The type of the third element.</typeparam>
		/// <typeparam name="T4">The type of the fourth element.</typeparam>
		/// <typeparam name="T5">The type of the fifth element.</typeparam>
		/// <typeparam name="T6">The type of the sixth element.</typeparam>
		/// <typeparam name="T7">The type of the seventh element.</typeparam>
		/// <typeparam name="T8">The type of the eighth element.</typeparam>
		/// <typeparam name="T9">The type of the ninth element.</typeparam>
		/// <typeparam name="T10">The type of the tenth element.</typeparam>
		/// <typeparam name="T11">The type of the eleventh element.</typeparam>
		// Token: 0x060010D2 RID: 4306 RVA: 0x00044654 File Offset: 0x00042854
		public static void Deconstruct<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(this Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11>> value, out T1 item1, out T2 item2, out T3 item3, out T4 item4, out T5 item5, out T6 item6, out T7 item7, out T8 item8, out T9 item9, out T10 item10, out T11 item11)
		{
			item1 = value.Item1;
			item2 = value.Item2;
			item3 = value.Item3;
			item4 = value.Item4;
			item5 = value.Item5;
			item6 = value.Item6;
			item7 = value.Item7;
			item8 = value.Rest.Item1;
			item9 = value.Rest.Item2;
			item10 = value.Rest.Item3;
			item11 = value.Rest.Item4;
		}

		/// <summary>Deconstructs a tuple with 12 elements into separate variables.</summary>
		/// <param name="value">The 12-element tuple to deconstruct into 12 separate variables.</param>
		/// <param name="item1">The value of the first element.</param>
		/// <param name="item2">The value of the second element.</param>
		/// <param name="item3">The value of the third element.</param>
		/// <param name="item4">The value of the fourth element.</param>
		/// <param name="item5">The value of the fifth element.</param>
		/// <param name="item6">The value of the sixth element.</param>
		/// <param name="item7">The value of the seventh element.</param>
		/// <param name="item8">The value of the eighth element, or <paramref name="value" /><see langword=".Rest.Item1" />.</param>
		/// <param name="item9">The value of the ninth element, or <paramref name="value" /><see langword=".Rest.Item2" />.</param>
		/// <param name="item10">The value of the tenth element, or <paramref name="value" /><see langword=".Rest.Item3" />.</param>
		/// <param name="item11">The value of the eleventh element, or <paramref name="value" /><see langword=".Rest.Item4" />.</param>
		/// <param name="item12">The value of the twelfth element, or <paramref name="value" /><see langword=".Rest.Item5" />.</param>
		/// <typeparam name="T1">The type of the first element.</typeparam>
		/// <typeparam name="T2">The type of the second element.</typeparam>
		/// <typeparam name="T3">The type of the third element.</typeparam>
		/// <typeparam name="T4">The type of the fourth element.</typeparam>
		/// <typeparam name="T5">The type of the fifth element.</typeparam>
		/// <typeparam name="T6">The type of the sixth element.</typeparam>
		/// <typeparam name="T7">The type of the seventh element.</typeparam>
		/// <typeparam name="T8">The type of the eighth element.</typeparam>
		/// <typeparam name="T9">The type of the ninth element.</typeparam>
		/// <typeparam name="T10">The type of the tenth element.</typeparam>
		/// <typeparam name="T11">The type of the eleventh element.</typeparam>
		/// <typeparam name="T12">The type of the twelfth element.</typeparam>
		// Token: 0x060010D3 RID: 4307 RVA: 0x00044704 File Offset: 0x00042904
		public static void Deconstruct<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(this Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12>> value, out T1 item1, out T2 item2, out T3 item3, out T4 item4, out T5 item5, out T6 item6, out T7 item7, out T8 item8, out T9 item9, out T10 item10, out T11 item11, out T12 item12)
		{
			item1 = value.Item1;
			item2 = value.Item2;
			item3 = value.Item3;
			item4 = value.Item4;
			item5 = value.Item5;
			item6 = value.Item6;
			item7 = value.Item7;
			item8 = value.Rest.Item1;
			item9 = value.Rest.Item2;
			item10 = value.Rest.Item3;
			item11 = value.Rest.Item4;
			item12 = value.Rest.Item5;
		}

		/// <summary>Deconstructs a tuple with 13 elements into separate variables.</summary>
		/// <param name="value">The 13-element tuple to deconstruct into 13 separate variables.</param>
		/// <param name="item1">The value of the first element.</param>
		/// <param name="item2">The value of the second element.</param>
		/// <param name="item3">The value of the third element.</param>
		/// <param name="item4">The value of the fourth element.</param>
		/// <param name="item5">The value of the fifth element.</param>
		/// <param name="item6">The value of the sixth element.</param>
		/// <param name="item7">The value of the seventh element.</param>
		/// <param name="item8">The value of the eighth element, or <paramref name="value" /><see langword=".Rest.Item1" />.</param>
		/// <param name="item9">The value of the ninth element, or <paramref name="value" /><see langword=".Rest.Item2" />.</param>
		/// <param name="item10">The value of the tenth element, or <paramref name="value" /><see langword=".Rest.Item3" />.</param>
		/// <param name="item11">The value of the eleventh element, or <paramref name="value" /><see langword=".Rest.Item4" />.</param>
		/// <param name="item12">The value of the twelfth element, or <paramref name="value" /><see langword=".Rest.Item5" />.</param>
		/// <param name="item13">The value of the thirteenth element, or <paramref name="value" /><see langword=".Rest.Item6" />.</param>
		/// <typeparam name="T1">The type of the first element.</typeparam>
		/// <typeparam name="T2">The type of the second element.</typeparam>
		/// <typeparam name="T3">The type of the third element.</typeparam>
		/// <typeparam name="T4">The type of the fourth element.</typeparam>
		/// <typeparam name="T5">The type of the fifth element.</typeparam>
		/// <typeparam name="T6">The type of the sixth element.</typeparam>
		/// <typeparam name="T7">The type of the seventh element.</typeparam>
		/// <typeparam name="T8">The type of the eighth element.</typeparam>
		/// <typeparam name="T9">The type of the ninth element.</typeparam>
		/// <typeparam name="T10">The type of the tenth element.</typeparam>
		/// <typeparam name="T11">The type of the eleventh element.</typeparam>
		/// <typeparam name="T12">The type of the twelfth element.</typeparam>
		/// <typeparam name="T13">The type of the thirteenth element.</typeparam>
		// Token: 0x060010D4 RID: 4308 RVA: 0x000447C4 File Offset: 0x000429C4
		public static void Deconstruct<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(this Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13>> value, out T1 item1, out T2 item2, out T3 item3, out T4 item4, out T5 item5, out T6 item6, out T7 item7, out T8 item8, out T9 item9, out T10 item10, out T11 item11, out T12 item12, out T13 item13)
		{
			item1 = value.Item1;
			item2 = value.Item2;
			item3 = value.Item3;
			item4 = value.Item4;
			item5 = value.Item5;
			item6 = value.Item6;
			item7 = value.Item7;
			item8 = value.Rest.Item1;
			item9 = value.Rest.Item2;
			item10 = value.Rest.Item3;
			item11 = value.Rest.Item4;
			item12 = value.Rest.Item5;
			item13 = value.Rest.Item6;
		}

		/// <summary>Deconstructs a tuple with 14 elements into separate variables.</summary>
		/// <param name="value">The 14-element tuple to deconstruct into 14 separate variables.</param>
		/// <param name="item1">The value of the first element.</param>
		/// <param name="item2">The value of the second element.</param>
		/// <param name="item3">The value of the third element.</param>
		/// <param name="item4">The value of the fourth element.</param>
		/// <param name="item5">The value of the fifth element.</param>
		/// <param name="item6">The value of the sixth element.</param>
		/// <param name="item7">The value of the seventh element.</param>
		/// <param name="item8">The value of the eighth element, or <paramref name="value" /><see langword=".Rest.Item1" />.</param>
		/// <param name="item9">The value of the ninth element, or <paramref name="value" /><see langword=".Rest.Item2" />.</param>
		/// <param name="item10">The value of the tenth element, or <paramref name="value" /><see langword=".Rest.Item3" />.</param>
		/// <param name="item11">The value of the eleventh element, or <paramref name="value" /><see langword=".Rest.Item4" />.</param>
		/// <param name="item12">The value of the twelfth element, or <paramref name="value" /><see langword=".Rest.Item5" />.</param>
		/// <param name="item13">The value of the thirteenth element, or <paramref name="value" /><see langword=".Rest.Item6" />.</param>
		/// <param name="item14">The value of the fourteenth element, or <paramref name="value" /><see langword=".Rest.Item7" />.</param>
		/// <typeparam name="T1">The type of the first element.</typeparam>
		/// <typeparam name="T2">The type of the second element.</typeparam>
		/// <typeparam name="T3">The type of the third element.</typeparam>
		/// <typeparam name="T4">The type of the fourth element.</typeparam>
		/// <typeparam name="T5">The type of the fifth element.</typeparam>
		/// <typeparam name="T6">The type of the sixth element.</typeparam>
		/// <typeparam name="T7">The type of the seventh element.</typeparam>
		/// <typeparam name="T8">The type of the eighth element.</typeparam>
		/// <typeparam name="T9">The type of the ninth element.</typeparam>
		/// <typeparam name="T10">The type of the tenth element.</typeparam>
		/// <typeparam name="T11">The type of the eleventh element.</typeparam>
		/// <typeparam name="T12">The type of the twelfth element.</typeparam>
		/// <typeparam name="T13">The type of the thirteenth element.</typeparam>
		/// <typeparam name="T14">The type of the fourteenth element.</typeparam>
		// Token: 0x060010D5 RID: 4309 RVA: 0x00044898 File Offset: 0x00042A98
		public static void Deconstruct<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(this Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14>> value, out T1 item1, out T2 item2, out T3 item3, out T4 item4, out T5 item5, out T6 item6, out T7 item7, out T8 item8, out T9 item9, out T10 item10, out T11 item11, out T12 item12, out T13 item13, out T14 item14)
		{
			item1 = value.Item1;
			item2 = value.Item2;
			item3 = value.Item3;
			item4 = value.Item4;
			item5 = value.Item5;
			item6 = value.Item6;
			item7 = value.Item7;
			item8 = value.Rest.Item1;
			item9 = value.Rest.Item2;
			item10 = value.Rest.Item3;
			item11 = value.Rest.Item4;
			item12 = value.Rest.Item5;
			item13 = value.Rest.Item6;
			item14 = value.Rest.Item7;
		}

		/// <summary>Deconstructs a tuple with 15 elements into separate variables.</summary>
		/// <param name="value">The 15-element tuple to deconstruct into 15 separate variables.</param>
		/// <param name="item1">The value of the first element.</param>
		/// <param name="item2">The value of the second element.</param>
		/// <param name="item3">The value of the third element.</param>
		/// <param name="item4">The value of the fourth element.</param>
		/// <param name="item5">The value of the fifth element.</param>
		/// <param name="item6">The value of the sixth element.</param>
		/// <param name="item7">The value of the seventh element.</param>
		/// <param name="item8">The value of the eighth element, or <paramref name="value" /><see langword=".Rest.Item1" />.</param>
		/// <param name="item9">The value of the ninth element, or <paramref name="value" /><see langword=".Rest.Item2" />.</param>
		/// <param name="item10">The value of the tenth element, or <paramref name="value" /><see langword=".Rest.Item3" />.</param>
		/// <param name="item11">The value of the eleventh element, or <paramref name="value" /><see langword=".Rest.Item4" />.</param>
		/// <param name="item12">The value of the twelfth element, or <paramref name="value" /><see langword=".Rest.Item5" />.</param>
		/// <param name="item13">The value of the thirteenth element, or <paramref name="value" /><see langword=".Rest.Item6" />.</param>
		/// <param name="item14">The value of the fourteenth element, or <paramref name="value" /><see langword=".Rest.Item7" />.</param>
		/// <param name="item15">The value of the fifteenth element, or <paramref name="value" /><see langword=".Rest.Rest.Item1" />.</param>
		/// <typeparam name="T1">The type of the first element.</typeparam>
		/// <typeparam name="T2">The type of the second element.</typeparam>
		/// <typeparam name="T3">The type of the third element.</typeparam>
		/// <typeparam name="T4">The type of the fourth element.</typeparam>
		/// <typeparam name="T5">The type of the fifth element.</typeparam>
		/// <typeparam name="T6">The type of the sixth element.</typeparam>
		/// <typeparam name="T7">The type of the seventh element.</typeparam>
		/// <typeparam name="T8">The type of the eighth element.</typeparam>
		/// <typeparam name="T9">The type of the ninth element.</typeparam>
		/// <typeparam name="T10">The type of the tenth element.</typeparam>
		/// <typeparam name="T11">The type of the eleventh element.</typeparam>
		/// <typeparam name="T12">The type of the twelfth element.</typeparam>
		/// <typeparam name="T13">The type of the thirteenth element.</typeparam>
		/// <typeparam name="T14">The type of the fourteenth element.</typeparam>
		/// <typeparam name="T15">The type of the fifteenth element.</typeparam>
		// Token: 0x060010D6 RID: 4310 RVA: 0x0004497C File Offset: 0x00042B7C
		public static void Deconstruct<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(this Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14, Tuple<T15>>> value, out T1 item1, out T2 item2, out T3 item3, out T4 item4, out T5 item5, out T6 item6, out T7 item7, out T8 item8, out T9 item9, out T10 item10, out T11 item11, out T12 item12, out T13 item13, out T14 item14, out T15 item15)
		{
			item1 = value.Item1;
			item2 = value.Item2;
			item3 = value.Item3;
			item4 = value.Item4;
			item5 = value.Item5;
			item6 = value.Item6;
			item7 = value.Item7;
			item8 = value.Rest.Item1;
			item9 = value.Rest.Item2;
			item10 = value.Rest.Item3;
			item11 = value.Rest.Item4;
			item12 = value.Rest.Item5;
			item13 = value.Rest.Item6;
			item14 = value.Rest.Item7;
			item15 = value.Rest.Rest.Item1;
		}

		/// <summary>Deconstructs a tuple with 16 elements into separate variables.</summary>
		/// <param name="value">The 16-element tuple to deconstruct into 16 separate variables.</param>
		/// <param name="item1">The value of the first element.</param>
		/// <param name="item2">The value of the second element.</param>
		/// <param name="item3">The value of the third element.</param>
		/// <param name="item4">The value of the fourth element.</param>
		/// <param name="item5">The value of the fifth element.</param>
		/// <param name="item6">The value of the sixth element.</param>
		/// <param name="item7">The value of the seventh element.</param>
		/// <param name="item8">The value of the eighth element, or <paramref name="value" /><see langword=".Rest.Item1" />.</param>
		/// <param name="item9">The value of the ninth element, or <paramref name="value" /><see langword=".Rest.Item2" />.</param>
		/// <param name="item10">The value of the tenth element, or <paramref name="value" /><see langword=".Rest.Item3" />.</param>
		/// <param name="item11">The value of the eleventh element, or <paramref name="value" /><see langword=".Rest.Item4" />.</param>
		/// <param name="item12">The value of the twelfth element, or <paramref name="value" /><see langword=".Rest.Item5" />.</param>
		/// <param name="item13">The value of the thirteenth element, or <paramref name="value" /><see langword=".Rest.Item6" />.</param>
		/// <param name="item14">The value of the fourteenth element, or <paramref name="value" /><see langword=".Rest.Item7" />.</param>
		/// <param name="item15">The value of the fifteenth element, or <paramref name="value" /><see langword=".Rest.Rest.Item1" />.</param>
		/// <param name="item16">The value of the sixteenth element, or <paramref name="value" /><see langword=".Rest.Rest.Item2" />.</param>
		/// <typeparam name="T1">The type of the first element.</typeparam>
		/// <typeparam name="T2">The type of the second element.</typeparam>
		/// <typeparam name="T3">The type of the third element.</typeparam>
		/// <typeparam name="T4">The type of the fourth element.</typeparam>
		/// <typeparam name="T5">The type of the fifth element.</typeparam>
		/// <typeparam name="T6">The type of the sixth element.</typeparam>
		/// <typeparam name="T7">The type of the seventh element.</typeparam>
		/// <typeparam name="T8">The type of the eighth element.</typeparam>
		/// <typeparam name="T9">The type of the ninth element.</typeparam>
		/// <typeparam name="T10">The type of the tenth element.</typeparam>
		/// <typeparam name="T11">The type of the eleventh element.</typeparam>
		/// <typeparam name="T12">The type of the twelfth element.</typeparam>
		/// <typeparam name="T13">The type of the thirteenth element.</typeparam>
		/// <typeparam name="T14">The type of the fourteenth element.</typeparam>
		/// <typeparam name="T15">The type of the fifteenth element.</typeparam>
		/// <typeparam name="T16">The type of the sixteenth element.</typeparam>
		// Token: 0x060010D7 RID: 4311 RVA: 0x00044A78 File Offset: 0x00042C78
		public static void Deconstruct<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(this Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14, Tuple<T15, T16>>> value, out T1 item1, out T2 item2, out T3 item3, out T4 item4, out T5 item5, out T6 item6, out T7 item7, out T8 item8, out T9 item9, out T10 item10, out T11 item11, out T12 item12, out T13 item13, out T14 item14, out T15 item15, out T16 item16)
		{
			item1 = value.Item1;
			item2 = value.Item2;
			item3 = value.Item3;
			item4 = value.Item4;
			item5 = value.Item5;
			item6 = value.Item6;
			item7 = value.Item7;
			item8 = value.Rest.Item1;
			item9 = value.Rest.Item2;
			item10 = value.Rest.Item3;
			item11 = value.Rest.Item4;
			item12 = value.Rest.Item5;
			item13 = value.Rest.Item6;
			item14 = value.Rest.Item7;
			item15 = value.Rest.Rest.Item1;
			item16 = value.Rest.Rest.Item2;
		}

		/// <summary>Deconstructs a tuple with 17 elements into separate variables.</summary>
		/// <param name="value">The 17-element tuple to deconstruct into 17 separate variables.</param>
		/// <param name="item1">The value of the first element.</param>
		/// <param name="item2">The value of the second element.</param>
		/// <param name="item3">The value of the third element.</param>
		/// <param name="item4">The value of the fourth element.</param>
		/// <param name="item5">The value of the fifth element.</param>
		/// <param name="item6">The value of the sixth element.</param>
		/// <param name="item7">The value of the seventh element.</param>
		/// <param name="item8">The value of the eighth element, or <paramref name="value" /><see langword=".Rest.Item1" />.</param>
		/// <param name="item9">The value of the ninth element, or <paramref name="value" /><see langword=".Rest.Item2" />.</param>
		/// <param name="item10">The value of the tenth element, or <paramref name="value" /><see langword=".Rest.Item3" />.</param>
		/// <param name="item11">The value of the eleventh element, or <paramref name="value" /><see langword=".Rest.Item4" />.</param>
		/// <param name="item12">The value of the twelfth element, or <paramref name="value" /><see langword=".Rest.Item5" />.</param>
		/// <param name="item13">The value of the thirteenth element, or <paramref name="value" /><see langword=".Rest.Item6" />.</param>
		/// <param name="item14">The value of the fourteenth element, or <paramref name="value" /><see langword=".Rest.Item7" />.</param>
		/// <param name="item15">The value of the fifteenth element, or <paramref name="value" /><see langword=".Rest.Rest.Item1" />.</param>
		/// <param name="item16">The value of the sixteenth element, or <paramref name="value" /><see langword=".Rest.Rest.Item2" />.</param>
		/// <param name="item17">The value of the seventeenth element, or <paramref name="value" /><see langword=".Rest.Rest.Item3" />.</param>
		/// <typeparam name="T1">The type of the first element.</typeparam>
		/// <typeparam name="T2">The type of the second element.</typeparam>
		/// <typeparam name="T3">The type of the third element.</typeparam>
		/// <typeparam name="T4">The type of the fourth element.</typeparam>
		/// <typeparam name="T5">The type of the fifth element.</typeparam>
		/// <typeparam name="T6">The type of the sixth element.</typeparam>
		/// <typeparam name="T7">The type of the seventh element.</typeparam>
		/// <typeparam name="T8">The type of the eighth element.</typeparam>
		/// <typeparam name="T9">The type of the ninth element.</typeparam>
		/// <typeparam name="T10">The type of the tenth element.</typeparam>
		/// <typeparam name="T11">The type of the eleventh element.</typeparam>
		/// <typeparam name="T12">The type of the twelfth element.</typeparam>
		/// <typeparam name="T13">The type of the thirteenth element.</typeparam>
		/// <typeparam name="T14">The type of the fourteenth element.</typeparam>
		/// <typeparam name="T15">The type of the fifteenth element.</typeparam>
		/// <typeparam name="T16">The type of the sixteenth element.</typeparam>
		/// <typeparam name="T17">The type of the seventeenth element.</typeparam>
		// Token: 0x060010D8 RID: 4312 RVA: 0x00044B8C File Offset: 0x00042D8C
		public static void Deconstruct<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17>(this Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14, Tuple<T15, T16, T17>>> value, out T1 item1, out T2 item2, out T3 item3, out T4 item4, out T5 item5, out T6 item6, out T7 item7, out T8 item8, out T9 item9, out T10 item10, out T11 item11, out T12 item12, out T13 item13, out T14 item14, out T15 item15, out T16 item16, out T17 item17)
		{
			item1 = value.Item1;
			item2 = value.Item2;
			item3 = value.Item3;
			item4 = value.Item4;
			item5 = value.Item5;
			item6 = value.Item6;
			item7 = value.Item7;
			item8 = value.Rest.Item1;
			item9 = value.Rest.Item2;
			item10 = value.Rest.Item3;
			item11 = value.Rest.Item4;
			item12 = value.Rest.Item5;
			item13 = value.Rest.Item6;
			item14 = value.Rest.Item7;
			item15 = value.Rest.Rest.Item1;
			item16 = value.Rest.Rest.Item2;
			item17 = value.Rest.Rest.Item3;
		}

		/// <summary>Deconstructs a tuple with 18 elements into separate variables.</summary>
		/// <param name="value">The 18-element tuple to deconstruct into 18 separate variables.</param>
		/// <param name="item1">The value of the first element.</param>
		/// <param name="item2">The value of the second element.</param>
		/// <param name="item3">The value of the third element.</param>
		/// <param name="item4">The value of the fourth element.</param>
		/// <param name="item5">The value of the fifth element.</param>
		/// <param name="item6">The value of the sixth element.</param>
		/// <param name="item7">The value of the seventh element.</param>
		/// <param name="item8">The value of the eighth element, or <paramref name="value" /><see langword=".Rest.Item1" />.</param>
		/// <param name="item9">The value of the ninth element, or <paramref name="value" /><see langword=".Rest.Item2" />.</param>
		/// <param name="item10">The value of the tenth element, or <paramref name="value" /><see langword=".Rest.Item3" />.</param>
		/// <param name="item11">The value of the eleventh element, or <paramref name="value" /><see langword=".Rest.Item4" />.</param>
		/// <param name="item12">The value of the twelfth element, or <paramref name="value" /><see langword=".Rest.Item5" />.</param>
		/// <param name="item13">The value of the thirteenth element, or <paramref name="value" /><see langword=".Rest.Item6" />.</param>
		/// <param name="item14">The value of the fourteenth element, or <paramref name="value" /><see langword=".Rest.Item7" />.</param>
		/// <param name="item15">The value of the fifteenth element, or <paramref name="value" /><see langword=".Rest.Rest.Item1" />.</param>
		/// <param name="item16">The value of the sixteenth element, or <paramref name="value" /><see langword=".Rest.Rest.Item2" />.</param>
		/// <param name="item17">The value of the seventeenth element, or <paramref name="value" /><see langword=".Rest.Rest.Item3" />.</param>
		/// <param name="item18">The value of the eighteenth element, or <paramref name="value" /><see langword=".Rest.Rest.Item4" />.</param>
		/// <typeparam name="T1">The type of the first element.</typeparam>
		/// <typeparam name="T2">The type of the second element.</typeparam>
		/// <typeparam name="T3">The type of the third element.</typeparam>
		/// <typeparam name="T4">The type of the fourth element.</typeparam>
		/// <typeparam name="T5">The type of the fifth element.</typeparam>
		/// <typeparam name="T6">The type of the sixth element.</typeparam>
		/// <typeparam name="T7">The type of the seventh element.</typeparam>
		/// <typeparam name="T8">The type of the eighth element.</typeparam>
		/// <typeparam name="T9">The type of the ninth element.</typeparam>
		/// <typeparam name="T10">The type of the tenth element.</typeparam>
		/// <typeparam name="T11">The type of the eleventh element.</typeparam>
		/// <typeparam name="T12">The type of the twelfth element.</typeparam>
		/// <typeparam name="T13">The type of the thirteenth element.</typeparam>
		/// <typeparam name="T14">The type of the fourteenth element.</typeparam>
		/// <typeparam name="T15">The type of the fifteenth element.</typeparam>
		/// <typeparam name="T16">The type of the sixteenth element.</typeparam>
		/// <typeparam name="T17">The type of the seventeenth element.</typeparam>
		/// <typeparam name="T18">The type of the eighteenth element.</typeparam>
		// Token: 0x060010D9 RID: 4313 RVA: 0x00044CB4 File Offset: 0x00042EB4
		public static void Deconstruct<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18>(this Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14, Tuple<T15, T16, T17, T18>>> value, out T1 item1, out T2 item2, out T3 item3, out T4 item4, out T5 item5, out T6 item6, out T7 item7, out T8 item8, out T9 item9, out T10 item10, out T11 item11, out T12 item12, out T13 item13, out T14 item14, out T15 item15, out T16 item16, out T17 item17, out T18 item18)
		{
			item1 = value.Item1;
			item2 = value.Item2;
			item3 = value.Item3;
			item4 = value.Item4;
			item5 = value.Item5;
			item6 = value.Item6;
			item7 = value.Item7;
			item8 = value.Rest.Item1;
			item9 = value.Rest.Item2;
			item10 = value.Rest.Item3;
			item11 = value.Rest.Item4;
			item12 = value.Rest.Item5;
			item13 = value.Rest.Item6;
			item14 = value.Rest.Item7;
			item15 = value.Rest.Rest.Item1;
			item16 = value.Rest.Rest.Item2;
			item17 = value.Rest.Rest.Item3;
			item18 = value.Rest.Rest.Item4;
		}

		/// <summary>Deconstructs a tuple with 19 elements into separate variables.</summary>
		/// <param name="value">The 19-element tuple to deconstruct into 19 separate variables.</param>
		/// <param name="item1">The value of the first element.</param>
		/// <param name="item2">The value of the second element.</param>
		/// <param name="item3">The value of the third element.</param>
		/// <param name="item4">The value of the fourth element.</param>
		/// <param name="item5">The value of the fifth element.</param>
		/// <param name="item6">The value of the sixth element.</param>
		/// <param name="item7">The value of the seventh element.</param>
		/// <param name="item8">The value of the eighth element, or <paramref name="value" /><see langword=".Rest.Item1" />.</param>
		/// <param name="item9">The value of the ninth element, or <paramref name="value" /><see langword=".Rest.Item2" />.</param>
		/// <param name="item10">The value of the tenth element, or <paramref name="value" /><see langword=".Rest.Item3" />.</param>
		/// <param name="item11">The value of the eleventh element, or <paramref name="value" /><see langword=".Rest.Item4" />.</param>
		/// <param name="item12">The value of the twelfth element, or <paramref name="value" /><see langword=".Rest.Item5" />.</param>
		/// <param name="item13">The value of the thirteenth element, or <paramref name="value" /><see langword=".Rest.Item6" />.</param>
		/// <param name="item14">The value of the fourteenth element, or <paramref name="value" /><see langword=".Rest.Item7" />.</param>
		/// <param name="item15">The value of the fifteenth element, or <paramref name="value" /><see langword=".Rest.Rest.Item1" />.</param>
		/// <param name="item16">The value of the sixteenth element, or <paramref name="value" /><see langword=".Rest.Rest.Item2" />.</param>
		/// <param name="item17">The value of the seventeenth element, or <paramref name="value" /><see langword=".Rest.Rest.Item3" />.</param>
		/// <param name="item18">The value of the eighteenth element, or <paramref name="value" /><see langword=".Rest.Rest.Item4" />.</param>
		/// <param name="item19">The value of the nineteenth element, or <paramref name="value" /><see langword=".Rest.Rest.Item5" />.</param>
		/// <typeparam name="T1">The type of the first element.</typeparam>
		/// <typeparam name="T2">The type of the second element.</typeparam>
		/// <typeparam name="T3">The type of the third element.</typeparam>
		/// <typeparam name="T4">The type of the fourth element.</typeparam>
		/// <typeparam name="T5">The type of the fifth element.</typeparam>
		/// <typeparam name="T6">The type of the sixth element.</typeparam>
		/// <typeparam name="T7">The type of the seventh element.</typeparam>
		/// <typeparam name="T8">The type of the eighth element.</typeparam>
		/// <typeparam name="T9">The type of the ninth element.</typeparam>
		/// <typeparam name="T10">The type of the tenth element.</typeparam>
		/// <typeparam name="T11">The type of the eleventh element.</typeparam>
		/// <typeparam name="T12">The type of the twelfth element.</typeparam>
		/// <typeparam name="T13">The type of the thirteenth element.</typeparam>
		/// <typeparam name="T14">The type of the fourteenth element.</typeparam>
		/// <typeparam name="T15">The type of the fifteenth element.</typeparam>
		/// <typeparam name="T16">The type of the sixteenth element.</typeparam>
		/// <typeparam name="T17">The type of the seventeenth element.</typeparam>
		/// <typeparam name="T18">The type of the eighteenth element.</typeparam>
		/// <typeparam name="T19">The type of the nineteenth element.</typeparam>
		// Token: 0x060010DA RID: 4314 RVA: 0x00044DF4 File Offset: 0x00042FF4
		public static void Deconstruct<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19>(this Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14, Tuple<T15, T16, T17, T18, T19>>> value, out T1 item1, out T2 item2, out T3 item3, out T4 item4, out T5 item5, out T6 item6, out T7 item7, out T8 item8, out T9 item9, out T10 item10, out T11 item11, out T12 item12, out T13 item13, out T14 item14, out T15 item15, out T16 item16, out T17 item17, out T18 item18, out T19 item19)
		{
			item1 = value.Item1;
			item2 = value.Item2;
			item3 = value.Item3;
			item4 = value.Item4;
			item5 = value.Item5;
			item6 = value.Item6;
			item7 = value.Item7;
			item8 = value.Rest.Item1;
			item9 = value.Rest.Item2;
			item10 = value.Rest.Item3;
			item11 = value.Rest.Item4;
			item12 = value.Rest.Item5;
			item13 = value.Rest.Item6;
			item14 = value.Rest.Item7;
			item15 = value.Rest.Rest.Item1;
			item16 = value.Rest.Rest.Item2;
			item17 = value.Rest.Rest.Item3;
			item18 = value.Rest.Rest.Item4;
			item19 = value.Rest.Rest.Item5;
		}

		/// <summary>Deconstructs a tuple with 20 elements into separate variables.</summary>
		/// <param name="value">The 20-element tuple to deconstruct into 20 separate variables.</param>
		/// <param name="item1">The value of the first element.</param>
		/// <param name="item2">The value of the second element.</param>
		/// <param name="item3">The value of the third element.</param>
		/// <param name="item4">The value of the fourth element.</param>
		/// <param name="item5">The value of the fifth element.</param>
		/// <param name="item6">The value of the sixth element.</param>
		/// <param name="item7">The value of the seventh element.</param>
		/// <param name="item8">The value of the eighth element, or <paramref name="value" /><see langword=".Rest.Item1" />.</param>
		/// <param name="item9">The value of the ninth element, or <paramref name="value" /><see langword=".Rest.Item2" />.</param>
		/// <param name="item10">The value of the tenth element, or <paramref name="value" /><see langword=".Rest.Item3" />.</param>
		/// <param name="item11">The value of the eleventh element, or <paramref name="value" /><see langword=".Rest.Item4" />.</param>
		/// <param name="item12">The value of the twelfth element, or <paramref name="value" /><see langword=".Rest.Item5" />.</param>
		/// <param name="item13">The value of the thirteenth element, or <paramref name="value" /><see langword=".Rest.Item6" />.</param>
		/// <param name="item14">The value of the fourteenth element, or <paramref name="value" /><see langword=".Rest.Item7" />.</param>
		/// <param name="item15">The value of the fifteenth element, or <paramref name="value" /><see langword=".Rest.Rest.Item1" />.</param>
		/// <param name="item16">The value of the sixteenth element, or <paramref name="value" /><see langword=".Rest.Rest.Item2" />.</param>
		/// <param name="item17">The value of the seventeenth element, or <paramref name="value" /><see langword=".Rest.Rest.Item3" />.</param>
		/// <param name="item18">The value of the eighteenth element, or <paramref name="value" /><see langword=".Rest.Rest.Item4" />.</param>
		/// <param name="item19">The value of the nineteenth element, or <paramref name="value" /><see langword=".Rest.Rest.Item5" />.</param>
		/// <param name="item20">The value of the twentieth element, or <paramref name="value" /><see langword=".Rest.Rest.Item6" />.</param>
		/// <typeparam name="T1">The type of the first element.</typeparam>
		/// <typeparam name="T2">The type of the second element.</typeparam>
		/// <typeparam name="T3">The type of the third element.</typeparam>
		/// <typeparam name="T4">The type of the fourth element.</typeparam>
		/// <typeparam name="T5">The type of the fifth element.</typeparam>
		/// <typeparam name="T6">The type of the sixth element.</typeparam>
		/// <typeparam name="T7">The type of the seventh element.</typeparam>
		/// <typeparam name="T8">The type of the eighth element.</typeparam>
		/// <typeparam name="T9">The type of the ninth element.</typeparam>
		/// <typeparam name="T10">The type of the tenth element.</typeparam>
		/// <typeparam name="T11">The type of the eleventh element.</typeparam>
		/// <typeparam name="T12">The type of the twelfth element.</typeparam>
		/// <typeparam name="T13">The type of the thirteenth element.</typeparam>
		/// <typeparam name="T14">The type of the fourteenth element.</typeparam>
		/// <typeparam name="T15">The type of the fifteenth element.</typeparam>
		/// <typeparam name="T16">The type of the sixteenth element.</typeparam>
		/// <typeparam name="T17">The type of the seventeenth element.</typeparam>
		/// <typeparam name="T18">The type of the eighteenth element.</typeparam>
		/// <typeparam name="T19">The type of the nineteenth element.</typeparam>
		/// <typeparam name="T20">The type of the twentieth element.</typeparam>
		// Token: 0x060010DB RID: 4315 RVA: 0x00044F4C File Offset: 0x0004314C
		public static void Deconstruct<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20>(this Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14, Tuple<T15, T16, T17, T18, T19, T20>>> value, out T1 item1, out T2 item2, out T3 item3, out T4 item4, out T5 item5, out T6 item6, out T7 item7, out T8 item8, out T9 item9, out T10 item10, out T11 item11, out T12 item12, out T13 item13, out T14 item14, out T15 item15, out T16 item16, out T17 item17, out T18 item18, out T19 item19, out T20 item20)
		{
			item1 = value.Item1;
			item2 = value.Item2;
			item3 = value.Item3;
			item4 = value.Item4;
			item5 = value.Item5;
			item6 = value.Item6;
			item7 = value.Item7;
			item8 = value.Rest.Item1;
			item9 = value.Rest.Item2;
			item10 = value.Rest.Item3;
			item11 = value.Rest.Item4;
			item12 = value.Rest.Item5;
			item13 = value.Rest.Item6;
			item14 = value.Rest.Item7;
			item15 = value.Rest.Rest.Item1;
			item16 = value.Rest.Rest.Item2;
			item17 = value.Rest.Rest.Item3;
			item18 = value.Rest.Rest.Item4;
			item19 = value.Rest.Rest.Item5;
			item20 = value.Rest.Rest.Item6;
		}

		/// <summary>Deconstructs a tuple with 21 elements into separate variables.</summary>
		/// <param name="value">The 21-element tuple to deconstruct into 21 separate variables.</param>
		/// <param name="item1">The value of the first element.</param>
		/// <param name="item2">The value of the second element.</param>
		/// <param name="item3">The value of the third element.</param>
		/// <param name="item4">The value of the fourth element.</param>
		/// <param name="item5">The value of the fifth element.</param>
		/// <param name="item6">The value of the sixth element.</param>
		/// <param name="item7">The value of the seventh element.</param>
		/// <param name="item8">The value of the eighth element, or <paramref name="value" /><see langword=".Rest.Item1" />.</param>
		/// <param name="item9">The value of the ninth element, or <paramref name="value" /><see langword=".Rest.Item2" />.</param>
		/// <param name="item10">The value of the tenth element, or <paramref name="value" /><see langword=".Rest.Item3" />.</param>
		/// <param name="item11">The value of the eleventh element, or <paramref name="value" /><see langword=".Rest.Item4" />.</param>
		/// <param name="item12">The value of the twelfth element, or <paramref name="value" /><see langword=".Rest.Item5" />.</param>
		/// <param name="item13">The value of the thirteenth element, or <paramref name="value" /><see langword=".Rest.Item6" />.</param>
		/// <param name="item14">The value of the fourteenth element, or <paramref name="value" /><see langword=".Rest.Item7" />.</param>
		/// <param name="item15">The value of the fifteenth element, or <paramref name="value" /><see langword=".Rest.Rest.Item1" />.</param>
		/// <param name="item16">The value of the sixteenth element, or <paramref name="value" /><see langword=".Rest.Rest.Item2" />.</param>
		/// <param name="item17">The value of the seventeenth element, or <paramref name="value" /><see langword=".Rest.Rest.Item3" />.</param>
		/// <param name="item18">The value of the eighteenth element, or <paramref name="value" /><see langword=".Rest.Rest.Item4" />.</param>
		/// <param name="item19">The value of the nineteenth element, or <paramref name="value" /><see langword=".Rest.Rest.Item5" />.</param>
		/// <param name="item20">The value of the twentieth element, or <paramref name="value" /><see langword=".Rest.Rest.Item6" />.</param>
		/// <param name="item21">The value of the twenty-first element, or <paramref name="value" /><see langword=".Rest.Rest.Item7" />.</param>
		/// <typeparam name="T1">The type of the first element.</typeparam>
		/// <typeparam name="T2">The type of the second element.</typeparam>
		/// <typeparam name="T3">The type of the third element.</typeparam>
		/// <typeparam name="T4">The type of the fourth element.</typeparam>
		/// <typeparam name="T5">The type of the fifth element.</typeparam>
		/// <typeparam name="T6">The type of the sixth element.</typeparam>
		/// <typeparam name="T7">The type of the seventh element.</typeparam>
		/// <typeparam name="T8">The type of the eighth element.</typeparam>
		/// <typeparam name="T9">The type of the ninth element.</typeparam>
		/// <typeparam name="T10">The type of the tenth element.</typeparam>
		/// <typeparam name="T11">The type of the eleventh element.</typeparam>
		/// <typeparam name="T12">The type of the twelfth element.</typeparam>
		/// <typeparam name="T13">The type of the thirteenth element.</typeparam>
		/// <typeparam name="T14">The type of the fourteenth element.</typeparam>
		/// <typeparam name="T15">The type of the fifteenth element.</typeparam>
		/// <typeparam name="T16">The type of the sixteenth element.</typeparam>
		/// <typeparam name="T17">The type of the seventeenth element.</typeparam>
		/// <typeparam name="T18">The type of the eighteenth element.</typeparam>
		/// <typeparam name="T19">The type of the nineteenth element.</typeparam>
		/// <typeparam name="T20">The type of the twentieth element.</typeparam>
		/// <typeparam name="T21">The type of the twenty-first element.</typeparam>
		// Token: 0x060010DC RID: 4316 RVA: 0x000450BC File Offset: 0x000432BC
		public static void Deconstruct<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21>(this Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14, Tuple<T15, T16, T17, T18, T19, T20, T21>>> value, out T1 item1, out T2 item2, out T3 item3, out T4 item4, out T5 item5, out T6 item6, out T7 item7, out T8 item8, out T9 item9, out T10 item10, out T11 item11, out T12 item12, out T13 item13, out T14 item14, out T15 item15, out T16 item16, out T17 item17, out T18 item18, out T19 item19, out T20 item20, out T21 item21)
		{
			item1 = value.Item1;
			item2 = value.Item2;
			item3 = value.Item3;
			item4 = value.Item4;
			item5 = value.Item5;
			item6 = value.Item6;
			item7 = value.Item7;
			item8 = value.Rest.Item1;
			item9 = value.Rest.Item2;
			item10 = value.Rest.Item3;
			item11 = value.Rest.Item4;
			item12 = value.Rest.Item5;
			item13 = value.Rest.Item6;
			item14 = value.Rest.Item7;
			item15 = value.Rest.Rest.Item1;
			item16 = value.Rest.Rest.Item2;
			item17 = value.Rest.Rest.Item3;
			item18 = value.Rest.Rest.Item4;
			item19 = value.Rest.Rest.Item5;
			item20 = value.Rest.Rest.Item6;
			item21 = value.Rest.Rest.Item7;
		}

		/// <summary>Converts an instance of the <see langword="Tuple" /> class to an instance of the  <see langword="ValueTuple" /> structure.</summary>
		/// <param name="value">The tuple object to convert to a value tuple</param>
		/// <typeparam name="T1">The type of the first element.</typeparam>
		/// <returns>The converted value tuple instance.</returns>
		// Token: 0x060010DD RID: 4317 RVA: 0x00045240 File Offset: 0x00043440
		public static ValueTuple<T1> ToValueTuple<T1>(this Tuple<T1> value)
		{
			return ValueTuple.Create<T1>(value.Item1);
		}

		/// <summary>Converts an instance of the <see langword="Tuple" /> class to an instance of the  <see langword="ValueTuple" /> structure.</summary>
		/// <param name="value">The tuple object to convert to a value tuple</param>
		/// <typeparam name="T1">The type of the first element.</typeparam>
		/// <typeparam name="T2">The type of the second element.</typeparam>
		/// <returns>The converted value tuple instance.</returns>
		// Token: 0x060010DE RID: 4318 RVA: 0x0004524D File Offset: 0x0004344D
		public static ValueTuple<T1, T2> ToValueTuple<T1, T2>(this Tuple<T1, T2> value)
		{
			return ValueTuple.Create<T1, T2>(value.Item1, value.Item2);
		}

		/// <summary>Converts an instance of the <see langword="Tuple" /> class to an instance of the  <see langword="ValueTuple" /> structure.</summary>
		/// <param name="value">The tuple object to convert to a value tuple</param>
		/// <typeparam name="T1">The type of the first element.</typeparam>
		/// <typeparam name="T2">The type of the second element.</typeparam>
		/// <typeparam name="T3">The type of the third element.</typeparam>
		/// <returns>The converted value tuple instance.</returns>
		// Token: 0x060010DF RID: 4319 RVA: 0x00045260 File Offset: 0x00043460
		public static ValueTuple<T1, T2, T3> ToValueTuple<T1, T2, T3>(this Tuple<T1, T2, T3> value)
		{
			return ValueTuple.Create<T1, T2, T3>(value.Item1, value.Item2, value.Item3);
		}

		/// <summary>Converts an instance of the <see langword="Tuple" /> class to an instance of the  <see langword="ValueTuple" /> structure.</summary>
		/// <param name="value">The tuple object to convert to a value tuple</param>
		/// <typeparam name="T1">The type of the first element.</typeparam>
		/// <typeparam name="T2">The type of the second element.</typeparam>
		/// <typeparam name="T3">The type of the third element.</typeparam>
		/// <typeparam name="T4">The type of the fourth element.</typeparam>
		/// <returns>The converted value tuple instance.</returns>
		// Token: 0x060010E0 RID: 4320 RVA: 0x00045279 File Offset: 0x00043479
		public static ValueTuple<T1, T2, T3, T4> ToValueTuple<T1, T2, T3, T4>(this Tuple<T1, T2, T3, T4> value)
		{
			return ValueTuple.Create<T1, T2, T3, T4>(value.Item1, value.Item2, value.Item3, value.Item4);
		}

		/// <summary>Converts an instance of the <see langword="Tuple" /> class to an instance of the  <see langword="ValueTuple" /> structure.</summary>
		/// <param name="value">The tuple object to convert to a value tuple</param>
		/// <typeparam name="T1">The type of the first element.</typeparam>
		/// <typeparam name="T2">The type of the second element.</typeparam>
		/// <typeparam name="T3">The type of the third element.</typeparam>
		/// <typeparam name="T4">The type of the fourth element.</typeparam>
		/// <typeparam name="T5">The type of the fifth element.</typeparam>
		/// <returns>The converted value tuple instance.</returns>
		// Token: 0x060010E1 RID: 4321 RVA: 0x00045298 File Offset: 0x00043498
		public static ValueTuple<T1, T2, T3, T4, T5> ToValueTuple<T1, T2, T3, T4, T5>(this Tuple<T1, T2, T3, T4, T5> value)
		{
			return ValueTuple.Create<T1, T2, T3, T4, T5>(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5);
		}

		/// <summary>Converts an instance of the <see langword="Tuple" /> class to an instance of the  <see langword="ValueTuple" /> structure.</summary>
		/// <param name="value">The tuple object to convert to a value tuple</param>
		/// <typeparam name="T1">The type of the first element.</typeparam>
		/// <typeparam name="T2">The type of the second element.</typeparam>
		/// <typeparam name="T3">The type of the third element.</typeparam>
		/// <typeparam name="T4">The type of the fourth element.</typeparam>
		/// <typeparam name="T5">The type of the fifth element.</typeparam>
		/// <typeparam name="T6">The type of the sixth element.</typeparam>
		/// <returns>The converted value tuple instance.</returns>
		// Token: 0x060010E2 RID: 4322 RVA: 0x000452BD File Offset: 0x000434BD
		public static ValueTuple<T1, T2, T3, T4, T5, T6> ToValueTuple<T1, T2, T3, T4, T5, T6>(this Tuple<T1, T2, T3, T4, T5, T6> value)
		{
			return ValueTuple.Create<T1, T2, T3, T4, T5, T6>(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6);
		}

		/// <summary>Converts an instance of the <see langword="Tuple" /> class to an instance of the  <see langword="ValueTuple" /> structure.</summary>
		/// <param name="value">The tuple object to convert to a value tuple</param>
		/// <typeparam name="T1">The type of the first element.</typeparam>
		/// <typeparam name="T2">The type of the second element.</typeparam>
		/// <typeparam name="T3">The type of the third element.</typeparam>
		/// <typeparam name="T4">The type of the fourth element.</typeparam>
		/// <typeparam name="T5">The type of the fifth element.</typeparam>
		/// <typeparam name="T6">The type of the sixth element.</typeparam>
		/// <typeparam name="T7">The type of the seventh element.</typeparam>
		/// <returns>The converted value tuple instance.</returns>
		// Token: 0x060010E3 RID: 4323 RVA: 0x000452E8 File Offset: 0x000434E8
		public static ValueTuple<T1, T2, T3, T4, T5, T6, T7> ToValueTuple<T1, T2, T3, T4, T5, T6, T7>(this Tuple<T1, T2, T3, T4, T5, T6, T7> value)
		{
			return ValueTuple.Create<T1, T2, T3, T4, T5, T6, T7>(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7);
		}

		/// <summary>Converts an instance of the <see langword="Tuple" /> class to an instance of the  <see langword="ValueTuple" /> structure.</summary>
		/// <param name="value">The tuple object to convert to a value tuple</param>
		/// <typeparam name="T1">The type of the first element.</typeparam>
		/// <typeparam name="T2">The type of the second element.</typeparam>
		/// <typeparam name="T3">The type of the third element.</typeparam>
		/// <typeparam name="T4">The type of the fourth element.</typeparam>
		/// <typeparam name="T5">The type of the fifth element.</typeparam>
		/// <typeparam name="T6">The type of the sixth element.</typeparam>
		/// <typeparam name="T7">The type of the seventh element.</typeparam>
		/// <typeparam name="T8">The type of the eighth element, or <paramref name="value" /><see langword=".Rest.Item1" />.</typeparam>
		/// <returns>The converted value tuple instance.</returns>
		// Token: 0x060010E4 RID: 4324 RVA: 0x0004531C File Offset: 0x0004351C
		public static ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8>> ToValueTuple<T1, T2, T3, T4, T5, T6, T7, T8>(this Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8>> value)
		{
			return TupleExtensions.CreateLong<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8>>(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7, ValueTuple.Create<T8>(value.Rest.Item1));
		}

		/// <summary>Converts an instance of the <see langword="Tuple" /> class to an instance of the  <see langword="ValueTuple" /> structure.</summary>
		/// <param name="value">The tuple object to convert to a value tuple</param>
		/// <typeparam name="T1">The type of the first element.</typeparam>
		/// <typeparam name="T2">The type of the second element.</typeparam>
		/// <typeparam name="T3">The type of the third element.</typeparam>
		/// <typeparam name="T4">The type of the fourth element.</typeparam>
		/// <typeparam name="T5">The type of the fifth element.</typeparam>
		/// <typeparam name="T6">The type of the sixth element.</typeparam>
		/// <typeparam name="T7">The type of the seventh element.</typeparam>
		/// <typeparam name="T8">The type of the eighth element, or <paramref name="value" /><see langword=".Rest.Item1" />.</typeparam>
		/// <typeparam name="T9">The type of the ninth element, or <paramref name="value" /><see langword=".Rest.Item2" />.</typeparam>
		/// <returns>The converted value tuple instance.</returns>
		// Token: 0x060010E5 RID: 4325 RVA: 0x00045368 File Offset: 0x00043568
		public static ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9>> ToValueTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9>(this Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9>> value)
		{
			return TupleExtensions.CreateLong<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9>>(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7, ValueTuple.Create<T8, T9>(value.Rest.Item1, value.Rest.Item2));
		}

		/// <summary>Converts an instance of the <see langword="Tuple" /> class to an instance of the  <see langword="ValueTuple" /> structure.</summary>
		/// <param name="value">The tuple object to convert to a value tuple</param>
		/// <typeparam name="T1">The type of the first element.</typeparam>
		/// <typeparam name="T2">The type of the second element.</typeparam>
		/// <typeparam name="T3">The type of the third element.</typeparam>
		/// <typeparam name="T4">The type of the fourth element.</typeparam>
		/// <typeparam name="T5">The type of the fifth element.</typeparam>
		/// <typeparam name="T6">The type of the sixth element.</typeparam>
		/// <typeparam name="T7">The type of the seventh element.</typeparam>
		/// <typeparam name="T8">The type of the eighth element, or <paramref name="value" /><see langword=".Rest.Item1" />.</typeparam>
		/// <typeparam name="T9">The type of the ninth element, or <paramref name="value" /><see langword=".Rest.Item2" />.</typeparam>
		/// <typeparam name="T10">The type of the tenth element, or <paramref name="value" /><see langword=".Rest.Item3" />.</typeparam>
		/// <returns>The converted value tuple instance.</returns>
		// Token: 0x060010E6 RID: 4326 RVA: 0x000453C0 File Offset: 0x000435C0
		public static ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10>> ToValueTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(this Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10>> value)
		{
			return TupleExtensions.CreateLong<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10>>(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7, ValueTuple.Create<T8, T9, T10>(value.Rest.Item1, value.Rest.Item2, value.Rest.Item3));
		}

		/// <summary>Converts an instance of the <see langword="Tuple" /> class to an instance of the  <see langword="ValueTuple" /> structure.</summary>
		/// <param name="value">The tuple object to convert to a value tuple</param>
		/// <typeparam name="T1">The type of the first element.</typeparam>
		/// <typeparam name="T2">The type of the second element.</typeparam>
		/// <typeparam name="T3">The type of the third element.</typeparam>
		/// <typeparam name="T4">The type of the fourth element.</typeparam>
		/// <typeparam name="T5">The type of the fifth element.</typeparam>
		/// <typeparam name="T6">The type of the sixth element.</typeparam>
		/// <typeparam name="T7">The type of the seventh element.</typeparam>
		/// <typeparam name="T8">The type of the eighth element, or <paramref name="value" /><see langword=".Rest.Item1" />.</typeparam>
		/// <typeparam name="T9">The type of the ninth element, or <paramref name="value" /><see langword=".Rest.Item2" />.</typeparam>
		/// <typeparam name="T10">The type of the tenth element, or <paramref name="value" /><see langword=".Rest.Item3" />.</typeparam>
		/// <typeparam name="T11">The type of the eleventh element, or <paramref name="value" /><see langword=".Rest.Item4" />.</typeparam>
		/// <returns>The converted value tuple instance.</returns>
		// Token: 0x060010E7 RID: 4327 RVA: 0x00045424 File Offset: 0x00043624
		public static ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11>> ToValueTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(this Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11>> value)
		{
			return TupleExtensions.CreateLong<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11>>(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7, ValueTuple.Create<T8, T9, T10, T11>(value.Rest.Item1, value.Rest.Item2, value.Rest.Item3, value.Rest.Item4));
		}

		/// <summary>Converts an instance of the <see langword="Tuple" /> class to an instance of the  <see langword="ValueTuple" /> structure.</summary>
		/// <param name="value">The tuple object to convert to a value tuple</param>
		/// <typeparam name="T1">The type of the first element.</typeparam>
		/// <typeparam name="T2">The type of the second element.</typeparam>
		/// <typeparam name="T3">The type of the third element.</typeparam>
		/// <typeparam name="T4">The type of the fourth element.</typeparam>
		/// <typeparam name="T5">The type of the fifth element.</typeparam>
		/// <typeparam name="T6">The type of the sixth element.</typeparam>
		/// <typeparam name="T7">The type of the seventh element.</typeparam>
		/// <typeparam name="T8">The type of the eighth element, or <paramref name="value" /><see langword=".Rest.Item1" />.</typeparam>
		/// <typeparam name="T9">The type of the ninth element, or <paramref name="value" /><see langword=".Rest.Item2" />.</typeparam>
		/// <typeparam name="T10">The type of the tenth element, or <paramref name="value" /><see langword=".Rest.Item3" />.</typeparam>
		/// <typeparam name="T11">The type of the eleventh element, or <paramref name="value" /><see langword=".Rest.Item4" />.</typeparam>
		/// <typeparam name="T12">The type of the twelfth element, or <paramref name="value" /><see langword=".Rest.Item5" />.</typeparam>
		/// <returns>The converted value tuple instance.</returns>
		// Token: 0x060010E8 RID: 4328 RVA: 0x00045494 File Offset: 0x00043694
		public static ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12>> ToValueTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(this Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12>> value)
		{
			return TupleExtensions.CreateLong<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12>>(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7, ValueTuple.Create<T8, T9, T10, T11, T12>(value.Rest.Item1, value.Rest.Item2, value.Rest.Item3, value.Rest.Item4, value.Rest.Item5));
		}

		/// <summary>Converts an instance of the <see langword="Tuple" /> class to an instance of the  <see langword="ValueTuple" /> structure.</summary>
		/// <param name="value">The tuple object to convert to a value tuple</param>
		/// <typeparam name="T1">The type of the first element.</typeparam>
		/// <typeparam name="T2">The type of the second element.</typeparam>
		/// <typeparam name="T3">The type of the third element.</typeparam>
		/// <typeparam name="T4">The type of the fourth element.</typeparam>
		/// <typeparam name="T5">The type of the fifth element.</typeparam>
		/// <typeparam name="T6">The type of the sixth element.</typeparam>
		/// <typeparam name="T7">The type of the seventh element.</typeparam>
		/// <typeparam name="T8">The type of the eighth element, or <paramref name="value" /><see langword=".Rest.Item1" />.</typeparam>
		/// <typeparam name="T9">The type of the ninth element, or <paramref name="value" /><see langword=".Rest.Item2" />.</typeparam>
		/// <typeparam name="T10">The type of the tenth element, or <paramref name="value" /><see langword=".Rest.Item3" />.</typeparam>
		/// <typeparam name="T11">The type of the eleventh element, or <paramref name="value" /><see langword=".Rest.Item4" />.</typeparam>
		/// <typeparam name="T12">The type of the twelfth element, or <paramref name="value" /><see langword=".Rest.Item5" />.</typeparam>
		/// <typeparam name="T13">The type of the thirteenth element, or <paramref name="value" /><see langword=".Rest.Item6" />.</typeparam>
		/// <returns>The converted value tuple instance.</returns>
		// Token: 0x060010E9 RID: 4329 RVA: 0x0004550C File Offset: 0x0004370C
		public static ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13>> ToValueTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(this Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13>> value)
		{
			return TupleExtensions.CreateLong<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13>>(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7, ValueTuple.Create<T8, T9, T10, T11, T12, T13>(value.Rest.Item1, value.Rest.Item2, value.Rest.Item3, value.Rest.Item4, value.Rest.Item5, value.Rest.Item6));
		}

		/// <summary>Converts an instance of the <see langword="Tuple" /> class to an instance of the  <see langword="ValueTuple" /> structure.</summary>
		/// <param name="value">The tuple object to convert to a value tuple</param>
		/// <typeparam name="T1">The type of the first element.</typeparam>
		/// <typeparam name="T2">The type of the second element.</typeparam>
		/// <typeparam name="T3">The type of the third element.</typeparam>
		/// <typeparam name="T4">The type of the fourth element.</typeparam>
		/// <typeparam name="T5">The type of the fifth element.</typeparam>
		/// <typeparam name="T6">The type of the sixth element.</typeparam>
		/// <typeparam name="T7">The type of the seventh element.</typeparam>
		/// <typeparam name="T8">The type of the eighth element, or <paramref name="value" /><see langword=".Rest.Item1" />.</typeparam>
		/// <typeparam name="T9">The type of the ninth element, or <paramref name="value" /><see langword=".Rest.Item2" />.</typeparam>
		/// <typeparam name="T10">The type of the tenth element, or <paramref name="value" /><see langword=".Rest.Item3" />.</typeparam>
		/// <typeparam name="T11">The type of the eleventh element, or <paramref name="value" /><see langword=".Rest.Item4" />.</typeparam>
		/// <typeparam name="T12">The type of the twelfth element, or <paramref name="value" /><see langword=".Rest.Item5" />.</typeparam>
		/// <typeparam name="T13">The type of the thirteenth element, or <paramref name="value" /><see langword=".Rest.Item6" />.</typeparam>
		/// <typeparam name="T14">The type of the fourteenth element, or <paramref name="value" /><see langword=".Rest.Item7" />.</typeparam>
		/// <returns>The converted value tuple instance.</returns>
		// Token: 0x060010EA RID: 4330 RVA: 0x00045590 File Offset: 0x00043790
		public static ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14>> ToValueTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(this Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14>> value)
		{
			return TupleExtensions.CreateLong<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14>>(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7, ValueTuple.Create<T8, T9, T10, T11, T12, T13, T14>(value.Rest.Item1, value.Rest.Item2, value.Rest.Item3, value.Rest.Item4, value.Rest.Item5, value.Rest.Item6, value.Rest.Item7));
		}

		/// <summary>Converts an instance of the <see langword="Tuple" /> class to an instance of the  <see langword="ValueTuple" /> structure.</summary>
		/// <param name="value">The tuple object to convert to a value tuple</param>
		/// <typeparam name="T1">The type of the first element.</typeparam>
		/// <typeparam name="T2">The type of the second element.</typeparam>
		/// <typeparam name="T3">The type of the third element.</typeparam>
		/// <typeparam name="T4">The type of the fourth element.</typeparam>
		/// <typeparam name="T5">The type of the fifth element.</typeparam>
		/// <typeparam name="T6">The type of the sixth element.</typeparam>
		/// <typeparam name="T7">The type of the seventh element.</typeparam>
		/// <typeparam name="T8">The type of the eighth element, or <paramref name="value" /><see langword=".Rest.Item1" />.</typeparam>
		/// <typeparam name="T9">The type of the ninth element, or <paramref name="value" /><see langword=".Rest.Item2" />.</typeparam>
		/// <typeparam name="T10">The type of the tenth element, or <paramref name="value" /><see langword=".Rest.Item3" />.</typeparam>
		/// <typeparam name="T11">The type of the eleventh element, or <paramref name="value" /><see langword=".Rest.Item4" />.</typeparam>
		/// <typeparam name="T12">The type of the twelfth element, or <paramref name="value" /><see langword=".Rest.Item5" />.</typeparam>
		/// <typeparam name="T13">The type of the thirteenth element, or <paramref name="value" /><see langword=".Rest.Item6" />.</typeparam>
		/// <typeparam name="T14">The type of the fourteenth element, or <paramref name="value" /><see langword=".Rest.Item7" />.</typeparam>
		/// <typeparam name="T15">The type of the fifteenth element, or <paramref name="value" /><see langword=".Rest.Rest.Item1" />.</typeparam>
		/// <returns>The converted value tuple instance.</returns>
		// Token: 0x060010EB RID: 4331 RVA: 0x00045620 File Offset: 0x00043820
		public static ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15>>> ToValueTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(this Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14, Tuple<T15>>> value)
		{
			return TupleExtensions.CreateLong<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15>>>(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7, TupleExtensions.CreateLong<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15>>(value.Rest.Item1, value.Rest.Item2, value.Rest.Item3, value.Rest.Item4, value.Rest.Item5, value.Rest.Item6, value.Rest.Item7, ValueTuple.Create<T15>(value.Rest.Rest.Item1)));
		}

		/// <summary>Converts an instance of the <see langword="Tuple" /> class to an instance of the  <see langword="ValueTuple" /> structure.</summary>
		/// <param name="value">The tuple object to convert to a value tuple</param>
		/// <typeparam name="T1">The type of the first element.</typeparam>
		/// <typeparam name="T2">The type of the second element.</typeparam>
		/// <typeparam name="T3">The type of the third element.</typeparam>
		/// <typeparam name="T4">The type of the fourth element.</typeparam>
		/// <typeparam name="T5">The type of the fifth element.</typeparam>
		/// <typeparam name="T6">The type of the sixth element.</typeparam>
		/// <typeparam name="T7">The type of the seventh element.</typeparam>
		/// <typeparam name="T8">The type of the eighth element, or <paramref name="value" /><see langword=".Rest.Item1" />.</typeparam>
		/// <typeparam name="T9">The type of the ninth element, or <paramref name="value" /><see langword=".Rest.Item2" />.</typeparam>
		/// <typeparam name="T10">The type of the tenth element, or <paramref name="value" /><see langword=".Rest.Item3" />.</typeparam>
		/// <typeparam name="T11">The type of the eleventh element, or <paramref name="value" /><see langword=".Rest.Item4" />.</typeparam>
		/// <typeparam name="T12">The type of the twelfth element, or <paramref name="value" /><see langword=".Rest.Item5" />.</typeparam>
		/// <typeparam name="T13">The type of the thirteenth element, or <paramref name="value" /><see langword=".Rest.Item6" />.</typeparam>
		/// <typeparam name="T14">The type of the fourteenth element, or <paramref name="value" /><see langword=".Rest.Item7" />.</typeparam>
		/// <typeparam name="T15">The type of the fifteenth element, or <paramref name="value" /><see langword=".Rest.Rest.Item1" />.</typeparam>
		/// <typeparam name="T16">The type of the sixteenth element, or <paramref name="value" /><see langword=".Rest.Rest.Item2" />.</typeparam>
		/// <returns>The converted value tuple instance.</returns>
		// Token: 0x060010EC RID: 4332 RVA: 0x000456C4 File Offset: 0x000438C4
		public static ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15, T16>>> ToValueTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(this Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14, Tuple<T15, T16>>> value)
		{
			return TupleExtensions.CreateLong<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15, T16>>>(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7, TupleExtensions.CreateLong<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15, T16>>(value.Rest.Item1, value.Rest.Item2, value.Rest.Item3, value.Rest.Item4, value.Rest.Item5, value.Rest.Item6, value.Rest.Item7, ValueTuple.Create<T15, T16>(value.Rest.Rest.Item1, value.Rest.Rest.Item2)));
		}

		/// <summary>Converts an instance of the <see langword="Tuple" /> class to an instance of the  <see langword="ValueTuple" /> structure.</summary>
		/// <param name="value">The tuple object to convert to a value tuple</param>
		/// <typeparam name="T1">The type of the first element.</typeparam>
		/// <typeparam name="T2">The type of the second element.</typeparam>
		/// <typeparam name="T3">The type of the third element.</typeparam>
		/// <typeparam name="T4">The type of the fourth element.</typeparam>
		/// <typeparam name="T5">The type of the fifth element.</typeparam>
		/// <typeparam name="T6">The type of the sixth element.</typeparam>
		/// <typeparam name="T7">The type of the seventh element.</typeparam>
		/// <typeparam name="T8">The type of the eighth element, or <paramref name="value" /><see langword=".Rest.Item1" />.</typeparam>
		/// <typeparam name="T9">The type of the ninth element, or <paramref name="value" /><see langword=".Rest.Item2" />.</typeparam>
		/// <typeparam name="T10">The type of the tenth element, or <paramref name="value" /><see langword=".Rest.Item3" />.</typeparam>
		/// <typeparam name="T11">The type of the eleventh element, or <paramref name="value" /><see langword=".Rest.Item4" />.</typeparam>
		/// <typeparam name="T12">The type of the twelfth element, or <paramref name="value" /><see langword=".Rest.Item5" />.</typeparam>
		/// <typeparam name="T13">The type of the thirteenth element, or <paramref name="value" /><see langword=".Rest.Item6" />.</typeparam>
		/// <typeparam name="T14">The type of the fourteenth element, or <paramref name="value" /><see langword=".Rest.Item7" />.</typeparam>
		/// <typeparam name="T15">The type of the fifteenth element, or <paramref name="value" /><see langword=".Rest.Rest.Item1" />.</typeparam>
		/// <typeparam name="T16">The type of the sixteenth element, or <paramref name="value" /><see langword=".Rest.Rest.Item2" />.</typeparam>
		/// <typeparam name="T17">The type of the seventeenth element, or <paramref name="value" /><see langword=".Rest.Rest.Item3" />.</typeparam>
		/// <returns>The converted value tuple instance.</returns>
		// Token: 0x060010ED RID: 4333 RVA: 0x00045778 File Offset: 0x00043978
		public static ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15, T16, T17>>> ToValueTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17>(this Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14, Tuple<T15, T16, T17>>> value)
		{
			return TupleExtensions.CreateLong<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15, T16, T17>>>(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7, TupleExtensions.CreateLong<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15, T16, T17>>(value.Rest.Item1, value.Rest.Item2, value.Rest.Item3, value.Rest.Item4, value.Rest.Item5, value.Rest.Item6, value.Rest.Item7, ValueTuple.Create<T15, T16, T17>(value.Rest.Rest.Item1, value.Rest.Rest.Item2, value.Rest.Rest.Item3)));
		}

		/// <summary>Converts an instance of the <see langword="Tuple" /> class to an instance of the  <see langword="ValueTuple" /> structure.</summary>
		/// <param name="value">The tuple object to convert to a value tuple</param>
		/// <typeparam name="T1">The type of the first element.</typeparam>
		/// <typeparam name="T2">The type of the second element.</typeparam>
		/// <typeparam name="T3">The type of the third element.</typeparam>
		/// <typeparam name="T4">The type of the fourth element.</typeparam>
		/// <typeparam name="T5">The type of the fifth element.</typeparam>
		/// <typeparam name="T6">The type of the sixth element.</typeparam>
		/// <typeparam name="T7">The type of the seventh element.</typeparam>
		/// <typeparam name="T8">The type of the eighth element, or <paramref name="value" /><see langword=".Rest.Item1" />.</typeparam>
		/// <typeparam name="T9">The type of the ninth element, or <paramref name="value" /><see langword=".Rest.Item2" />.</typeparam>
		/// <typeparam name="T10">The type of the tenth element, or <paramref name="value" /><see langword=".Rest.Item3" />.</typeparam>
		/// <typeparam name="T11">The type of the eleventh element, or <paramref name="value" /><see langword=".Rest.Item4" />.</typeparam>
		/// <typeparam name="T12">The type of the twelfth element, or <paramref name="value" /><see langword=".Rest.Item5" />.</typeparam>
		/// <typeparam name="T13">The type of the thirteenth element, or <paramref name="value" /><see langword=".Rest.Item6" />.</typeparam>
		/// <typeparam name="T14">The type of the fourteenth element, or <paramref name="value" /><see langword=".Rest.Item7" />.</typeparam>
		/// <typeparam name="T15">The type of the fifteenth element, or <paramref name="value" /><see langword=".Rest.Rest.Item1" />.</typeparam>
		/// <typeparam name="T16">The type of the sixteenth element, or <paramref name="value" /><see langword=".Rest.Rest.Item2" />.</typeparam>
		/// <typeparam name="T17">The type of the seventeenth element, or <paramref name="value" /><see langword=".Rest.Rest.Item3" />.</typeparam>
		/// <typeparam name="T18">The type of the eighteenth element, or <paramref name="value" /><see langword=".Rest.Rest.Item4" />.</typeparam>
		/// <returns>The converted value tuple instance.</returns>
		// Token: 0x060010EE RID: 4334 RVA: 0x0004583C File Offset: 0x00043A3C
		public static ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15, T16, T17, T18>>> ToValueTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18>(this Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14, Tuple<T15, T16, T17, T18>>> value)
		{
			return TupleExtensions.CreateLong<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15, T16, T17, T18>>>(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7, TupleExtensions.CreateLong<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15, T16, T17, T18>>(value.Rest.Item1, value.Rest.Item2, value.Rest.Item3, value.Rest.Item4, value.Rest.Item5, value.Rest.Item6, value.Rest.Item7, ValueTuple.Create<T15, T16, T17, T18>(value.Rest.Rest.Item1, value.Rest.Rest.Item2, value.Rest.Rest.Item3, value.Rest.Rest.Item4)));
		}

		/// <summary>Converts an instance of the <see langword="Tuple" /> class to an instance of the  <see langword="ValueTuple" /> structure.</summary>
		/// <param name="value">The tuple object to convert to a value tuple</param>
		/// <typeparam name="T1">The type of the first element.</typeparam>
		/// <typeparam name="T2">The type of the second element.</typeparam>
		/// <typeparam name="T3">The type of the third element.</typeparam>
		/// <typeparam name="T4">The type of the fourth element.</typeparam>
		/// <typeparam name="T5">The type of the fifth element.</typeparam>
		/// <typeparam name="T6">The type of the sixth element.</typeparam>
		/// <typeparam name="T7">The type of the seventh element.</typeparam>
		/// <typeparam name="T8">The type of the eighth element, or <paramref name="value" /><see langword=".Rest.Item1" />.</typeparam>
		/// <typeparam name="T9">The type of the ninth element, or <paramref name="value" /><see langword=".Rest.Item2" />.</typeparam>
		/// <typeparam name="T10">The type of the tenth element, or <paramref name="value" /><see langword=".Rest.Item3" />.</typeparam>
		/// <typeparam name="T11">The type of the eleventh element, or <paramref name="value" /><see langword=".Rest.Item4" />.</typeparam>
		/// <typeparam name="T12">The type of the twelfth element, or <paramref name="value" /><see langword=".Rest.Item5" />.</typeparam>
		/// <typeparam name="T13">The type of the thirteenth element, or <paramref name="value" /><see langword=".Rest.Item6" />.</typeparam>
		/// <typeparam name="T14">The type of the fourteenth element, or <paramref name="value" /><see langword=".Rest.Item7" />.</typeparam>
		/// <typeparam name="T15">The type of the fifteenth element, or <paramref name="value" /><see langword=".Rest.Rest.Item1" />.</typeparam>
		/// <typeparam name="T16">The type of the sixteenth element, or <paramref name="value" /><see langword=".Rest.Rest.Item2" />.</typeparam>
		/// <typeparam name="T17">The type of the seventeenth element, or <paramref name="value" /><see langword=".Rest.Rest.Item3" />.</typeparam>
		/// <typeparam name="T18">The type of the eighteenth element, or <paramref name="value" /><see langword=".Rest.Rest.Item4" />.</typeparam>
		/// <typeparam name="T19">The type of the nineteenth element, or <paramref name="value" /><see langword=".Rest.Rest.Item5" />.</typeparam>
		/// <returns>The converted value tuple instance.</returns>
		// Token: 0x060010EF RID: 4335 RVA: 0x00045910 File Offset: 0x00043B10
		public static ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15, T16, T17, T18, T19>>> ToValueTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19>(this Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14, Tuple<T15, T16, T17, T18, T19>>> value)
		{
			return TupleExtensions.CreateLong<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15, T16, T17, T18, T19>>>(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7, TupleExtensions.CreateLong<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15, T16, T17, T18, T19>>(value.Rest.Item1, value.Rest.Item2, value.Rest.Item3, value.Rest.Item4, value.Rest.Item5, value.Rest.Item6, value.Rest.Item7, ValueTuple.Create<T15, T16, T17, T18, T19>(value.Rest.Rest.Item1, value.Rest.Rest.Item2, value.Rest.Rest.Item3, value.Rest.Rest.Item4, value.Rest.Rest.Item5)));
		}

		/// <summary>Converts an instance of the <see langword="Tuple" /> class to an instance of the  <see langword="ValueTuple" /> structure.</summary>
		/// <param name="value">The tuple object to convert to a value tuple</param>
		/// <typeparam name="T1">The type of the first element.</typeparam>
		/// <typeparam name="T2">The type of the second element.</typeparam>
		/// <typeparam name="T3">The type of the third element.</typeparam>
		/// <typeparam name="T4">The type of the fourth element.</typeparam>
		/// <typeparam name="T5">The type of the fifth element.</typeparam>
		/// <typeparam name="T6">The type of the sixth element.</typeparam>
		/// <typeparam name="T7">The type of the seventh element.</typeparam>
		/// <typeparam name="T8">The type of the eighth element, or <paramref name="value" /><see langword=".Rest.Item1" />.</typeparam>
		/// <typeparam name="T9">The type of the ninth element, or <paramref name="value" /><see langword=".Rest.Item2" />.</typeparam>
		/// <typeparam name="T10">The type of the tenth element, or <paramref name="value" /><see langword=".Rest.Item3" />.</typeparam>
		/// <typeparam name="T11">The type of the eleventh element, or <paramref name="value" /><see langword=".Rest.Item4" />.</typeparam>
		/// <typeparam name="T12">The type of the twelfth element, or <paramref name="value" /><see langword=".Rest.Item5" />.</typeparam>
		/// <typeparam name="T13">The type of the thirteenth element, or <paramref name="value" /><see langword=".Rest.Item6" />.</typeparam>
		/// <typeparam name="T14">The type of the fourteenth element, or <paramref name="value" /><see langword=".Rest.Item7" />.</typeparam>
		/// <typeparam name="T15">The type of the fifteenth element, or <paramref name="value" /><see langword=".Rest.Rest.Item1" />.</typeparam>
		/// <typeparam name="T16">The type of the sixteenth element, or <paramref name="value" /><see langword=".Rest.Rest.Item2" />.</typeparam>
		/// <typeparam name="T17">The type of the seventeenth element, or <paramref name="value" /><see langword=".Rest.Rest.Item3" />.</typeparam>
		/// <typeparam name="T18">The type of the eighteenth element, or <paramref name="value" /><see langword=".Rest.Rest.Item4" />.</typeparam>
		/// <typeparam name="T19">The type of the nineteenth element, or <paramref name="value" /><see langword=".Rest.Rest.Item5" />.</typeparam>
		/// <typeparam name="T20">The type of the twentieth element, or <paramref name="value" /><see langword=".Rest.Rest.Item6" />.</typeparam>
		/// <returns>The converted value tuple instance.</returns>
		// Token: 0x060010F0 RID: 4336 RVA: 0x000459F4 File Offset: 0x00043BF4
		public static ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15, T16, T17, T18, T19, T20>>> ToValueTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20>(this Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14, Tuple<T15, T16, T17, T18, T19, T20>>> value)
		{
			return TupleExtensions.CreateLong<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15, T16, T17, T18, T19, T20>>>(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7, TupleExtensions.CreateLong<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15, T16, T17, T18, T19, T20>>(value.Rest.Item1, value.Rest.Item2, value.Rest.Item3, value.Rest.Item4, value.Rest.Item5, value.Rest.Item6, value.Rest.Item7, ValueTuple.Create<T15, T16, T17, T18, T19, T20>(value.Rest.Rest.Item1, value.Rest.Rest.Item2, value.Rest.Rest.Item3, value.Rest.Rest.Item4, value.Rest.Rest.Item5, value.Rest.Rest.Item6)));
		}

		/// <summary>Converts an instance of the <see langword="Tuple" /> class to an instance of the  <see langword="ValueTuple" /> structure.</summary>
		/// <param name="value">The tuple object to convert to a value tuple</param>
		/// <typeparam name="T1">The type of the first element.</typeparam>
		/// <typeparam name="T2">The type of the second element.</typeparam>
		/// <typeparam name="T3">The type of the third element.</typeparam>
		/// <typeparam name="T4">The type of the fourth element.</typeparam>
		/// <typeparam name="T5">The type of the fifth element.</typeparam>
		/// <typeparam name="T6">The type of the sixth element.</typeparam>
		/// <typeparam name="T7">The type of the seventh element.</typeparam>
		/// <typeparam name="T8">The type of the eighth element, or <paramref name="value" /><see langword=".Rest.Item1" />.</typeparam>
		/// <typeparam name="T9">The type of the ninth element, or <paramref name="value" /><see langword=".Rest.Item2" />.</typeparam>
		/// <typeparam name="T10">The type of the tenth element, or <paramref name="value" /><see langword=".Rest.Item3" />.</typeparam>
		/// <typeparam name="T11">The type of the eleventh element, or <paramref name="value" /><see langword=".Rest.Item4" />.</typeparam>
		/// <typeparam name="T12">The type of the twelfth element, or <paramref name="value" /><see langword=".Rest.Item5" />.</typeparam>
		/// <typeparam name="T13">The type of the thirteenth element, or <paramref name="value" /><see langword=".Rest.Item6" />.</typeparam>
		/// <typeparam name="T14">The type of the fourteenth element, or <paramref name="value" /><see langword=".Rest.Item7" />.</typeparam>
		/// <typeparam name="T15">The type of the fifteenth element, or <paramref name="value" /><see langword=".Rest.Rest.Item1" />.</typeparam>
		/// <typeparam name="T16">The type of the sixteenth element, or <paramref name="value" /><see langword=".Rest.Rest.Item2" />.</typeparam>
		/// <typeparam name="T17">The type of the seventeenth element, or <paramref name="value" /><see langword=".Rest.Rest.Item3" />.</typeparam>
		/// <typeparam name="T18">The type of the eighteenth element, or <paramref name="value" /><see langword=".Rest.Rest.Item4" />.</typeparam>
		/// <typeparam name="T19">The type of the nineteenth element, or <paramref name="value" /><see langword=".Rest.Rest.Item5" />.</typeparam>
		/// <typeparam name="T20">The type of the twentieth element, or <paramref name="value" /><see langword=".Rest.Rest.Item6" />.</typeparam>
		/// <typeparam name="T21">The type of the twenty-first element, or <paramref name="value" /><see langword=".Rest.Rest.Item7" />.</typeparam>
		/// <returns>The converted value tuple instance.</returns>
		// Token: 0x060010F1 RID: 4337 RVA: 0x00045AE8 File Offset: 0x00043CE8
		public static ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15, T16, T17, T18, T19, T20, T21>>> ToValueTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21>(this Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14, Tuple<T15, T16, T17, T18, T19, T20, T21>>> value)
		{
			return TupleExtensions.CreateLong<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15, T16, T17, T18, T19, T20, T21>>>(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7, TupleExtensions.CreateLong<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15, T16, T17, T18, T19, T20, T21>>(value.Rest.Item1, value.Rest.Item2, value.Rest.Item3, value.Rest.Item4, value.Rest.Item5, value.Rest.Item6, value.Rest.Item7, ValueTuple.Create<T15, T16, T17, T18, T19, T20, T21>(value.Rest.Rest.Item1, value.Rest.Rest.Item2, value.Rest.Rest.Item3, value.Rest.Rest.Item4, value.Rest.Rest.Item5, value.Rest.Rest.Item6, value.Rest.Rest.Item7)));
		}

		/// <summary>Converts an instance of the <see langword="ValueTuple" /> structure to an instance of the  <see langword="Tuple" /> class.</summary>
		/// <param name="value">The value tuple instance to convert to a tuple.</param>
		/// <typeparam name="T1">The type of the first element.</typeparam>
		/// <returns>The converted tuple.</returns>
		// Token: 0x060010F2 RID: 4338 RVA: 0x00045BEB File Offset: 0x00043DEB
		public static Tuple<T1> ToTuple<T1>(this ValueTuple<T1> value)
		{
			return Tuple.Create<T1>(value.Item1);
		}

		/// <summary>Converts an instance of the <see langword="ValueTuple" /> structure to an instance of the  <see langword="Tuple" /> class.</summary>
		/// <param name="value">The value tuple instance to convert to a tuple.</param>
		/// <typeparam name="T1">The type of the first element.</typeparam>
		/// <typeparam name="T2">The type of the second element.</typeparam>
		/// <returns>The converted tuple.</returns>
		// Token: 0x060010F3 RID: 4339 RVA: 0x00045BF8 File Offset: 0x00043DF8
		public static Tuple<T1, T2> ToTuple<T1, T2>(this ValueTuple<T1, T2> value)
		{
			return Tuple.Create<T1, T2>(value.Item1, value.Item2);
		}

		/// <summary>Converts an instance of the <see langword="ValueTuple" /> structure to an instance of the  <see langword="Tuple" /> class.</summary>
		/// <param name="value">The value tuple instance to convert to a tuple.</param>
		/// <typeparam name="T1">The type of the first element.</typeparam>
		/// <typeparam name="T2">The type of the second element.</typeparam>
		/// <typeparam name="T3">The type of the third element.</typeparam>
		/// <returns>The converted tuple.</returns>
		// Token: 0x060010F4 RID: 4340 RVA: 0x00045C0B File Offset: 0x00043E0B
		public static Tuple<T1, T2, T3> ToTuple<T1, T2, T3>(this ValueTuple<T1, T2, T3> value)
		{
			return Tuple.Create<T1, T2, T3>(value.Item1, value.Item2, value.Item3);
		}

		/// <summary>Converts an instance of the <see langword="ValueTuple" /> structure to an instance of the  <see langword="Tuple" /> class.</summary>
		/// <param name="value">The value tuple instance to convert to a tuple.</param>
		/// <typeparam name="T1">The type of the first element.</typeparam>
		/// <typeparam name="T2">The type of the second element.</typeparam>
		/// <typeparam name="T3">The type of the third element.</typeparam>
		/// <typeparam name="T4">The type of the fourth element.</typeparam>
		/// <returns>The converted tuple.</returns>
		// Token: 0x060010F5 RID: 4341 RVA: 0x00045C24 File Offset: 0x00043E24
		public static Tuple<T1, T2, T3, T4> ToTuple<T1, T2, T3, T4>(this ValueTuple<T1, T2, T3, T4> value)
		{
			return Tuple.Create<T1, T2, T3, T4>(value.Item1, value.Item2, value.Item3, value.Item4);
		}

		/// <summary>Converts an instance of the <see langword="ValueTuple" /> structure to an instance of the  <see langword="Tuple" /> class.</summary>
		/// <param name="value">The value tuple instance to convert to a tuple.</param>
		/// <typeparam name="T1">The type of the first element.</typeparam>
		/// <typeparam name="T2">The type of the second element.</typeparam>
		/// <typeparam name="T3">The type of the third element.</typeparam>
		/// <typeparam name="T4">The type of the fourth element.</typeparam>
		/// <typeparam name="T5">The type of the fifth element.</typeparam>
		/// <returns>The converted tuple.</returns>
		// Token: 0x060010F6 RID: 4342 RVA: 0x00045C43 File Offset: 0x00043E43
		public static Tuple<T1, T2, T3, T4, T5> ToTuple<T1, T2, T3, T4, T5>(this ValueTuple<T1, T2, T3, T4, T5> value)
		{
			return Tuple.Create<T1, T2, T3, T4, T5>(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5);
		}

		/// <summary>Converts an instance of the <see langword="ValueTuple" /> structure to an instance of the  <see langword="Tuple" /> class.</summary>
		/// <param name="value">The value tuple instance to convert to a tuple.</param>
		/// <typeparam name="T1">The type of the first element.</typeparam>
		/// <typeparam name="T2">The type of the second element.</typeparam>
		/// <typeparam name="T3">The type of the third element.</typeparam>
		/// <typeparam name="T4">The type of the fourth element.</typeparam>
		/// <typeparam name="T5">The type of the fifth element.</typeparam>
		/// <typeparam name="T6">The type of the sixth element.</typeparam>
		/// <returns>The converted tuple.</returns>
		// Token: 0x060010F7 RID: 4343 RVA: 0x00045C68 File Offset: 0x00043E68
		public static Tuple<T1, T2, T3, T4, T5, T6> ToTuple<T1, T2, T3, T4, T5, T6>(this ValueTuple<T1, T2, T3, T4, T5, T6> value)
		{
			return Tuple.Create<T1, T2, T3, T4, T5, T6>(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6);
		}

		/// <summary>Converts an instance of the <see langword="ValueTuple" /> structure to an instance of the  <see langword="Tuple" /> class.</summary>
		/// <param name="value">The value tuple instance to convert to a tuple.</param>
		/// <typeparam name="T1">The type of the first element.</typeparam>
		/// <typeparam name="T2">The type of the second element.</typeparam>
		/// <typeparam name="T3">The type of the third element.</typeparam>
		/// <typeparam name="T4">The type of the fourth element.</typeparam>
		/// <typeparam name="T5">The type of the fifth element.</typeparam>
		/// <typeparam name="T6">The type of the sixth element.</typeparam>
		/// <typeparam name="T7">The type of the seventh element.</typeparam>
		/// <returns>The converted tuple.</returns>
		// Token: 0x060010F8 RID: 4344 RVA: 0x00045C93 File Offset: 0x00043E93
		public static Tuple<T1, T2, T3, T4, T5, T6, T7> ToTuple<T1, T2, T3, T4, T5, T6, T7>(this ValueTuple<T1, T2, T3, T4, T5, T6, T7> value)
		{
			return Tuple.Create<T1, T2, T3, T4, T5, T6, T7>(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7);
		}

		/// <summary>Converts an instance of the <see langword="ValueTuple" /> structure to an instance of the  <see langword="Tuple" /> class.</summary>
		/// <param name="value">The value tuple instance to convert to a tuple.</param>
		/// <typeparam name="T1">The type of the first element.</typeparam>
		/// <typeparam name="T2">The type of the second element.</typeparam>
		/// <typeparam name="T3">The type of the third element.</typeparam>
		/// <typeparam name="T4">The type of the fourth element.</typeparam>
		/// <typeparam name="T5">The type of the fifth element.</typeparam>
		/// <typeparam name="T6">The type of the sixth element.</typeparam>
		/// <typeparam name="T7">The type of the seventh element.</typeparam>
		/// <typeparam name="T8">The type of the eighth element, or <paramref name="value" /><see langword=".Rest.Item1" />.</typeparam>
		/// <returns>The converted tuple.</returns>
		// Token: 0x060010F9 RID: 4345 RVA: 0x00045CC4 File Offset: 0x00043EC4
		public static Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8>> ToTuple<T1, T2, T3, T4, T5, T6, T7, T8>(this ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8>> value)
		{
			return TupleExtensions.CreateLongRef<T1, T2, T3, T4, T5, T6, T7, Tuple<T8>>(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7, Tuple.Create<T8>(value.Rest.Item1));
		}

		/// <summary>Converts an instance of the <see langword="ValueTuple" /> structure to an instance of the  <see langword="Tuple" /> class.</summary>
		/// <param name="value">The value tuple instance to convert to a tuple.</param>
		/// <typeparam name="T1">The type of the first element.</typeparam>
		/// <typeparam name="T2">The type of the second element.</typeparam>
		/// <typeparam name="T3">The type of the third element.</typeparam>
		/// <typeparam name="T4">The type of the fourth element.</typeparam>
		/// <typeparam name="T5">The type of the fifth element.</typeparam>
		/// <typeparam name="T6">The type of the sixth element.</typeparam>
		/// <typeparam name="T7">The type of the seventh element.</typeparam>
		/// <typeparam name="T8">The type of the eighth element, or <paramref name="value" /><see langword=".Rest.Item1" />.</typeparam>
		/// <typeparam name="T9">The type of the ninth element, or <paramref name="value" /><see langword=".Rest.Item2" />.</typeparam>
		/// <returns>The converted tuple.</returns>
		// Token: 0x060010FA RID: 4346 RVA: 0x00045D10 File Offset: 0x00043F10
		public static Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9>> ToTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9>(this ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9>> value)
		{
			return TupleExtensions.CreateLongRef<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9>>(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7, Tuple.Create<T8, T9>(value.Rest.Item1, value.Rest.Item2));
		}

		/// <summary>Converts an instance of the <see langword="ValueTuple" /> structure to an instance of the  <see langword="Tuple" /> class.</summary>
		/// <param name="value">The value tuple instance to convert to a tuple.</param>
		/// <typeparam name="T1">The type of the first element.</typeparam>
		/// <typeparam name="T2">The type of the second element.</typeparam>
		/// <typeparam name="T3">The type of the third element.</typeparam>
		/// <typeparam name="T4">The type of the fourth element.</typeparam>
		/// <typeparam name="T5">The type of the fifth element.</typeparam>
		/// <typeparam name="T6">The type of the sixth element.</typeparam>
		/// <typeparam name="T7">The type of the seventh element.</typeparam>
		/// <typeparam name="T8">The type of the eighth element, or <paramref name="value" /><see langword=".Rest.Item1" />.</typeparam>
		/// <typeparam name="T9">The type of the ninth element, or <paramref name="value" /><see langword=".Rest.Item2" />.</typeparam>
		/// <typeparam name="T10">The type of the tenth element, or <paramref name="value" /><see langword=".Rest.Item3" />.</typeparam>
		/// <returns>The converted tuple.</returns>
		// Token: 0x060010FB RID: 4347 RVA: 0x00045D68 File Offset: 0x00043F68
		public static Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10>> ToTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(this ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10>> value)
		{
			return TupleExtensions.CreateLongRef<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10>>(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7, Tuple.Create<T8, T9, T10>(value.Rest.Item1, value.Rest.Item2, value.Rest.Item3));
		}

		/// <summary>Converts an instance of the <see langword="ValueTuple" /> structure to an instance of the  <see langword="Tuple" /> class.</summary>
		/// <param name="value">The value tuple instance to convert to a tuple.</param>
		/// <typeparam name="T1">The type of the first element.</typeparam>
		/// <typeparam name="T2">The type of the second element.</typeparam>
		/// <typeparam name="T3">The type of the third element.</typeparam>
		/// <typeparam name="T4">The type of the fourth element.</typeparam>
		/// <typeparam name="T5">The type of the fifth element.</typeparam>
		/// <typeparam name="T6">The type of the sixth element.</typeparam>
		/// <typeparam name="T7">The type of the seventh element.</typeparam>
		/// <typeparam name="T8">The type of the eighth element, or <paramref name="value" /><see langword=".Rest.Item1" />.</typeparam>
		/// <typeparam name="T9">The type of the ninth element, or <paramref name="value" /><see langword=".Rest.Item2" />.</typeparam>
		/// <typeparam name="T10">The type of the tenth element, or <paramref name="value" /><see langword=".Rest.Item3" />.</typeparam>
		/// <typeparam name="T11">The type of the eleventh element, or <paramref name="value" /><see langword=".Rest.Item4" />.</typeparam>
		/// <returns>The converted tuple.</returns>
		// Token: 0x060010FC RID: 4348 RVA: 0x00045DCC File Offset: 0x00043FCC
		public static Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11>> ToTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(this ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11>> value)
		{
			return TupleExtensions.CreateLongRef<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11>>(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7, Tuple.Create<T8, T9, T10, T11>(value.Rest.Item1, value.Rest.Item2, value.Rest.Item3, value.Rest.Item4));
		}

		/// <summary>Converts an instance of the <see langword="ValueTuple" /> structure to an instance of the  <see langword="Tuple" /> class.</summary>
		/// <param name="value">The value tuple instance to convert to a tuple.</param>
		/// <typeparam name="T1">The type of the first element.</typeparam>
		/// <typeparam name="T2">The type of the second element.</typeparam>
		/// <typeparam name="T3">The type of the third element.</typeparam>
		/// <typeparam name="T4">The type of the fourth element.</typeparam>
		/// <typeparam name="T5">The type of the fifth element.</typeparam>
		/// <typeparam name="T6">The type of the sixth element.</typeparam>
		/// <typeparam name="T7">The type of the seventh element.</typeparam>
		/// <typeparam name="T8">The type of the eighth element, or <paramref name="value" /><see langword=".Rest.Item1" />.</typeparam>
		/// <typeparam name="T9">The type of the ninth element, or <paramref name="value" /><see langword=".Rest.Item2" />.</typeparam>
		/// <typeparam name="T10">The type of the tenth element, or <paramref name="value" /><see langword=".Rest.Item3" />.</typeparam>
		/// <typeparam name="T11">The type of the eleventh element, or <paramref name="value" /><see langword=".Rest.Item4" />.</typeparam>
		/// <typeparam name="T12">The type of the twelfth element, or <paramref name="value" /><see langword=".Rest.Item5" />.</typeparam>
		/// <returns>The converted tuple.</returns>
		// Token: 0x060010FD RID: 4349 RVA: 0x00045E3C File Offset: 0x0004403C
		public static Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12>> ToTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(this ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12>> value)
		{
			return TupleExtensions.CreateLongRef<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12>>(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7, Tuple.Create<T8, T9, T10, T11, T12>(value.Rest.Item1, value.Rest.Item2, value.Rest.Item3, value.Rest.Item4, value.Rest.Item5));
		}

		/// <summary>Converts an instance of the <see langword="ValueTuple" /> structure to an instance of the  <see langword="Tuple" /> class.</summary>
		/// <param name="value">The value tuple instance to convert to a tuple.</param>
		/// <typeparam name="T1">The type of the first element.</typeparam>
		/// <typeparam name="T2">The type of the second element.</typeparam>
		/// <typeparam name="T3">The type of the third element.</typeparam>
		/// <typeparam name="T4">The type of the fourth element.</typeparam>
		/// <typeparam name="T5">The type of the fifth element.</typeparam>
		/// <typeparam name="T6">The type of the sixth element.</typeparam>
		/// <typeparam name="T7">The type of the seventh element.</typeparam>
		/// <typeparam name="T8">The type of the eighth element, or <paramref name="value" /><see langword=".Rest.Item1" />.</typeparam>
		/// <typeparam name="T9">The type of the ninth element, or <paramref name="value" /><see langword=".Rest.Item2" />.</typeparam>
		/// <typeparam name="T10">The type of the tenth element, or <paramref name="value" /><see langword=".Rest.Item3" />.</typeparam>
		/// <typeparam name="T11">The type of the eleventh element, or <paramref name="value" /><see langword=".Rest.Item4" />.</typeparam>
		/// <typeparam name="T12">The type of the twelfth element, or <paramref name="value" /><see langword=".Rest.Item5" />.</typeparam>
		/// <typeparam name="T13">The type of the thirteenth element, or <paramref name="value" /><see langword=".Rest.Item6" />.</typeparam>
		/// <returns>The converted tuple.</returns>
		// Token: 0x060010FE RID: 4350 RVA: 0x00045EB4 File Offset: 0x000440B4
		public static Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13>> ToTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(this ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13>> value)
		{
			return TupleExtensions.CreateLongRef<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13>>(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7, Tuple.Create<T8, T9, T10, T11, T12, T13>(value.Rest.Item1, value.Rest.Item2, value.Rest.Item3, value.Rest.Item4, value.Rest.Item5, value.Rest.Item6));
		}

		/// <summary>Converts an instance of the <see langword="ValueTuple" /> structure to an instance of the  <see langword="Tuple" /> class.</summary>
		/// <param name="value">The value tuple instance to convert to a tuple.</param>
		/// <typeparam name="T1">The type of the first element.</typeparam>
		/// <typeparam name="T2">The type of the second element.</typeparam>
		/// <typeparam name="T3">The type of the third element.</typeparam>
		/// <typeparam name="T4">The type of the fourth element.</typeparam>
		/// <typeparam name="T5">The type of the fifth element.</typeparam>
		/// <typeparam name="T6">The type of the sixth element.</typeparam>
		/// <typeparam name="T7">The type of the seventh element.</typeparam>
		/// <typeparam name="T8">The type of the eighth element, or <paramref name="value" /><see langword=".Rest.Item1" />.</typeparam>
		/// <typeparam name="T9">The type of the ninth element, or <paramref name="value" /><see langword=".Rest.Item2" />.</typeparam>
		/// <typeparam name="T10">The type of the tenth element, or <paramref name="value" /><see langword=".Rest.Item3" />.</typeparam>
		/// <typeparam name="T11">The type of the eleventh element, or <paramref name="value" /><see langword=".Rest.Item4" />.</typeparam>
		/// <typeparam name="T12">The type of the twelfth element, or <paramref name="value" /><see langword=".Rest.Item5" />.</typeparam>
		/// <typeparam name="T13">The type of the thirteenth element, or <paramref name="value" /><see langword=".Rest.Item6" />.</typeparam>
		/// <typeparam name="T14">The type of the fourteenth element, or <paramref name="value" /><see langword=".Rest.Item7" />.</typeparam>
		/// <returns>The converted tuple.</returns>
		// Token: 0x060010FF RID: 4351 RVA: 0x00045F38 File Offset: 0x00044138
		public static Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14>> ToTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(this ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14>> value)
		{
			return TupleExtensions.CreateLongRef<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14>>(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7, Tuple.Create<T8, T9, T10, T11, T12, T13, T14>(value.Rest.Item1, value.Rest.Item2, value.Rest.Item3, value.Rest.Item4, value.Rest.Item5, value.Rest.Item6, value.Rest.Item7));
		}

		/// <summary>Converts an instance of the <see langword="ValueTuple" /> structure to an instance of the  <see langword="Tuple" /> class.</summary>
		/// <param name="value">The value tuple instance to convert to a tuple.</param>
		/// <typeparam name="T1">The type of the first element.</typeparam>
		/// <typeparam name="T2">The type of the second element.</typeparam>
		/// <typeparam name="T3">The type of the third element.</typeparam>
		/// <typeparam name="T4">The type of the fourth element.</typeparam>
		/// <typeparam name="T5">The type of the fifth element.</typeparam>
		/// <typeparam name="T6">The type of the sixth element.</typeparam>
		/// <typeparam name="T7">The type of the seventh element.</typeparam>
		/// <typeparam name="T8">The type of the eighth element, or <paramref name="value" /><see langword=".Rest.Item1" />.</typeparam>
		/// <typeparam name="T9">The type of the ninth element, or <paramref name="value" /><see langword=".Rest.Item2" />.</typeparam>
		/// <typeparam name="T10">The type of the tenth element, or <paramref name="value" /><see langword=".Rest.Item3" />.</typeparam>
		/// <typeparam name="T11">The type of the eleventh element, or <paramref name="value" /><see langword=".Rest.Item4" />.</typeparam>
		/// <typeparam name="T12">The type of the twelfth element, or <paramref name="value" /><see langword=".Rest.Item5" />.</typeparam>
		/// <typeparam name="T13">The type of the thirteenth element, or <paramref name="value" /><see langword=".Rest.Item6" />.</typeparam>
		/// <typeparam name="T14">The type of the fourteenth element, or <paramref name="value" /><see langword=".Rest.Item7" />.</typeparam>
		/// <typeparam name="T15">The type of the fifteenth element, or <paramref name="value" /><see langword=".Rest.Rest.Item1" />.</typeparam>
		/// <returns>The converted tuple.</returns>
		// Token: 0x06001100 RID: 4352 RVA: 0x00045FC8 File Offset: 0x000441C8
		public static Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14, Tuple<T15>>> ToTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(this ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15>>> value)
		{
			return TupleExtensions.CreateLongRef<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14, Tuple<T15>>>(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7, TupleExtensions.CreateLongRef<T8, T9, T10, T11, T12, T13, T14, Tuple<T15>>(value.Rest.Item1, value.Rest.Item2, value.Rest.Item3, value.Rest.Item4, value.Rest.Item5, value.Rest.Item6, value.Rest.Item7, Tuple.Create<T15>(value.Rest.Rest.Item1)));
		}

		/// <summary>Converts an instance of the <see langword="ValueTuple" /> structure to an instance of the  <see langword="Tuple" /> class.</summary>
		/// <param name="value">The value tuple instance to convert to a tuple.</param>
		/// <typeparam name="T1">The type of the first element.</typeparam>
		/// <typeparam name="T2">The type of the second element.</typeparam>
		/// <typeparam name="T3">The type of the third element.</typeparam>
		/// <typeparam name="T4">The type of the fourth element.</typeparam>
		/// <typeparam name="T5">The type of the fifth element.</typeparam>
		/// <typeparam name="T6">The type of the sixth element.</typeparam>
		/// <typeparam name="T7">The type of the seventh element.</typeparam>
		/// <typeparam name="T8">The type of the eighth element, or <paramref name="value" /><see langword=".Rest.Item1" />.</typeparam>
		/// <typeparam name="T9">The type of the ninth element, or <paramref name="value" /><see langword=".Rest.Item2" />.</typeparam>
		/// <typeparam name="T10">The type of the tenth element, or <paramref name="value" /><see langword=".Rest.Item3" />.</typeparam>
		/// <typeparam name="T11">The type of the eleventh element, or <paramref name="value" /><see langword=".Rest.Item4" />.</typeparam>
		/// <typeparam name="T12">The type of the twelfth element, or <paramref name="value" /><see langword=".Rest.Item5" />.</typeparam>
		/// <typeparam name="T13">The type of the thirteenth element, or <paramref name="value" /><see langword=".Rest.Item6" />.</typeparam>
		/// <typeparam name="T14">The type of the fourteenth element, or <paramref name="value" /><see langword=".Rest.Item7" />.</typeparam>
		/// <typeparam name="T15">The type of the fifteenth element, or <paramref name="value" /><see langword=".Rest.Rest.Item1" />.</typeparam>
		/// <typeparam name="T16">The type of the sixteenth element, or <paramref name="value" /><see langword=".Rest.Rest.Item2" />.</typeparam>
		/// <returns>The converted tuple.</returns>
		// Token: 0x06001101 RID: 4353 RVA: 0x0004606C File Offset: 0x0004426C
		public static Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14, Tuple<T15, T16>>> ToTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(this ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15, T16>>> value)
		{
			return TupleExtensions.CreateLongRef<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14, Tuple<T15, T16>>>(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7, TupleExtensions.CreateLongRef<T8, T9, T10, T11, T12, T13, T14, Tuple<T15, T16>>(value.Rest.Item1, value.Rest.Item2, value.Rest.Item3, value.Rest.Item4, value.Rest.Item5, value.Rest.Item6, value.Rest.Item7, Tuple.Create<T15, T16>(value.Rest.Rest.Item1, value.Rest.Rest.Item2)));
		}

		/// <summary>Converts an instance of the <see langword="ValueTuple" /> structure to an instance of the  <see langword="Tuple" /> class.</summary>
		/// <param name="value">The value tuple instance to convert to a tuple.</param>
		/// <typeparam name="T1">The type of the first element.</typeparam>
		/// <typeparam name="T2">The type of the second element.</typeparam>
		/// <typeparam name="T3">The type of the third element.</typeparam>
		/// <typeparam name="T4">The type of the fourth element.</typeparam>
		/// <typeparam name="T5">The type of the fifth element.</typeparam>
		/// <typeparam name="T6">The type of the sixth element.</typeparam>
		/// <typeparam name="T7">The type of the seventh element.</typeparam>
		/// <typeparam name="T8">The type of the eighth element, or <paramref name="value" /><see langword=".Rest.Item1" />.</typeparam>
		/// <typeparam name="T9">The type of the ninth element, or <paramref name="value" /><see langword=".Rest.Item2" />.</typeparam>
		/// <typeparam name="T10">The type of the tenth element, or <paramref name="value" /><see langword=".Rest.Item3" />.</typeparam>
		/// <typeparam name="T11">The type of the eleventh element, or <paramref name="value" /><see langword=".Rest.Item4" />.</typeparam>
		/// <typeparam name="T12">The type of the twelfth element, or <paramref name="value" /><see langword=".Rest.Item5" />.</typeparam>
		/// <typeparam name="T13">The type of the thirteenth element, or <paramref name="value" /><see langword=".Rest.Item6" />.</typeparam>
		/// <typeparam name="T14">The type of the fourteenth element, or <paramref name="value" /><see langword=".Rest.Item7" />.</typeparam>
		/// <typeparam name="T15">The type of the fifteenth element, or <paramref name="value" /><see langword=".Rest.Rest.Item1" />.</typeparam>
		/// <typeparam name="T16">The type of the sixteenth element, or <paramref name="value" /><see langword=".Rest.Rest.Item2" />.</typeparam>
		/// <typeparam name="T17">The type of the seventeenth element, or <paramref name="value" /><see langword=".Rest.Rest.Item3" />.</typeparam>
		/// <returns>The converted tuple.</returns>
		// Token: 0x06001102 RID: 4354 RVA: 0x00046120 File Offset: 0x00044320
		public static Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14, Tuple<T15, T16, T17>>> ToTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17>(this ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15, T16, T17>>> value)
		{
			return TupleExtensions.CreateLongRef<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14, Tuple<T15, T16, T17>>>(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7, TupleExtensions.CreateLongRef<T8, T9, T10, T11, T12, T13, T14, Tuple<T15, T16, T17>>(value.Rest.Item1, value.Rest.Item2, value.Rest.Item3, value.Rest.Item4, value.Rest.Item5, value.Rest.Item6, value.Rest.Item7, Tuple.Create<T15, T16, T17>(value.Rest.Rest.Item1, value.Rest.Rest.Item2, value.Rest.Rest.Item3)));
		}

		/// <summary>Converts an instance of the <see langword="ValueTuple" /> structure to an instance of the  <see langword="Tuple" /> class.</summary>
		/// <param name="value">The value tuple instance to convert to a tuple.</param>
		/// <typeparam name="T1">The type of the first element.</typeparam>
		/// <typeparam name="T2">The type of the second element.</typeparam>
		/// <typeparam name="T3">The type of the third element.</typeparam>
		/// <typeparam name="T4">The type of the fourth element.</typeparam>
		/// <typeparam name="T5">The type of the fifth element.</typeparam>
		/// <typeparam name="T6">The type of the sixth element.</typeparam>
		/// <typeparam name="T7">The type of the seventh element.</typeparam>
		/// <typeparam name="T8">The type of the eighth element, or <paramref name="value" /><see langword=".Rest.Item1" />.</typeparam>
		/// <typeparam name="T9">The type of the ninth element, or <paramref name="value" /><see langword=".Rest.Item2" />.</typeparam>
		/// <typeparam name="T10">The type of the tenth element, or <paramref name="value" /><see langword=".Rest.Item3" />.</typeparam>
		/// <typeparam name="T11">The type of the eleventh element, or <paramref name="value" /><see langword=".Rest.Item4" />.</typeparam>
		/// <typeparam name="T12">The type of the twelfth element, or <paramref name="value" /><see langword=".Rest.Item5" />.</typeparam>
		/// <typeparam name="T13">The type of the thirteenth element, or <paramref name="value" /><see langword=".Rest.Item6" />.</typeparam>
		/// <typeparam name="T14">The type of the fourteenth element, or <paramref name="value" /><see langword=".Rest.Item7" />.</typeparam>
		/// <typeparam name="T15">The type of the fifteenth element, or <paramref name="value" /><see langword=".Rest.Rest.Item1" />.</typeparam>
		/// <typeparam name="T16">The type of the sixteenth element, or <paramref name="value" /><see langword=".Rest.Rest.Item2" />.</typeparam>
		/// <typeparam name="T17">The type of the seventeenth element, or <paramref name="value" /><see langword=".Rest.Rest.Item3" />.</typeparam>
		/// <typeparam name="T18">The type of the eighteenth element, or <paramref name="value" /><see langword=".Rest.Rest.Item4" />.</typeparam>
		/// <returns>The converted tuple.</returns>
		// Token: 0x06001103 RID: 4355 RVA: 0x000461E4 File Offset: 0x000443E4
		public static Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14, Tuple<T15, T16, T17, T18>>> ToTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18>(this ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15, T16, T17, T18>>> value)
		{
			return TupleExtensions.CreateLongRef<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14, Tuple<T15, T16, T17, T18>>>(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7, TupleExtensions.CreateLongRef<T8, T9, T10, T11, T12, T13, T14, Tuple<T15, T16, T17, T18>>(value.Rest.Item1, value.Rest.Item2, value.Rest.Item3, value.Rest.Item4, value.Rest.Item5, value.Rest.Item6, value.Rest.Item7, Tuple.Create<T15, T16, T17, T18>(value.Rest.Rest.Item1, value.Rest.Rest.Item2, value.Rest.Rest.Item3, value.Rest.Rest.Item4)));
		}

		/// <summary>Converts an instance of the <see langword="ValueTuple" /> structure to an instance of the  <see langword="Tuple" /> class.</summary>
		/// <param name="value">The value tuple instance to convert to a tuple.</param>
		/// <typeparam name="T1">The type of the first element.</typeparam>
		/// <typeparam name="T2">The type of the second element.</typeparam>
		/// <typeparam name="T3">The type of the third element.</typeparam>
		/// <typeparam name="T4">The type of the fourth element.</typeparam>
		/// <typeparam name="T5">The type of the fifth element.</typeparam>
		/// <typeparam name="T6">The type of the sixth element.</typeparam>
		/// <typeparam name="T7">The type of the seventh element.</typeparam>
		/// <typeparam name="T8">The type of the eighth element, or <paramref name="value" /><see langword=".Rest.Item1" />.</typeparam>
		/// <typeparam name="T9">The type of the ninth element, or <paramref name="value" /><see langword=".Rest.Item2" />.</typeparam>
		/// <typeparam name="T10">The type of the tenth element, or <paramref name="value" /><see langword=".Rest.Item3" />.</typeparam>
		/// <typeparam name="T11">The type of the eleventh element, or <paramref name="value" /><see langword=".Rest.Item4" />.</typeparam>
		/// <typeparam name="T12">The type of the twelfth element, or <paramref name="value" /><see langword=".Rest.Item5" />.</typeparam>
		/// <typeparam name="T13">The type of the thirteenth element, or <paramref name="value" /><see langword=".Rest.Item6" />.</typeparam>
		/// <typeparam name="T14">The type of the fourteenth element, or <paramref name="value" /><see langword=".Rest.Item7" />.</typeparam>
		/// <typeparam name="T15">The type of the fifteenth element, or <paramref name="value" /><see langword=".Rest.Rest.Item1" />.</typeparam>
		/// <typeparam name="T16">The type of the sixteenth element, or <paramref name="value" /><see langword=".Rest.Rest.Item2" />.</typeparam>
		/// <typeparam name="T17">The type of the seventeenth element, or <paramref name="value" /><see langword=".Rest.Rest.Item3" />.</typeparam>
		/// <typeparam name="T18">The type of the eighteenth element, or <paramref name="value" /><see langword=".Rest.Rest.Item4" />.</typeparam>
		/// <typeparam name="T19">The type of the nineteenth element, or <paramref name="value" /><see langword=".Rest.Rest.Item5" />.</typeparam>
		/// <returns>The converted tuple.</returns>
		// Token: 0x06001104 RID: 4356 RVA: 0x000462B8 File Offset: 0x000444B8
		public static Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14, Tuple<T15, T16, T17, T18, T19>>> ToTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19>(this ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15, T16, T17, T18, T19>>> value)
		{
			return TupleExtensions.CreateLongRef<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14, Tuple<T15, T16, T17, T18, T19>>>(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7, TupleExtensions.CreateLongRef<T8, T9, T10, T11, T12, T13, T14, Tuple<T15, T16, T17, T18, T19>>(value.Rest.Item1, value.Rest.Item2, value.Rest.Item3, value.Rest.Item4, value.Rest.Item5, value.Rest.Item6, value.Rest.Item7, Tuple.Create<T15, T16, T17, T18, T19>(value.Rest.Rest.Item1, value.Rest.Rest.Item2, value.Rest.Rest.Item3, value.Rest.Rest.Item4, value.Rest.Rest.Item5)));
		}

		/// <summary>Converts an instance of the <see langword="ValueTuple" /> structure to an instance of the  <see langword="Tuple" /> class.</summary>
		/// <param name="value">The value tuple instance to convert to a tuple.</param>
		/// <typeparam name="T1">The type of the first element.</typeparam>
		/// <typeparam name="T2">The type of the second element.</typeparam>
		/// <typeparam name="T3">The type of the third element.</typeparam>
		/// <typeparam name="T4">The type of the fourth element.</typeparam>
		/// <typeparam name="T5">The type of the fifth element.</typeparam>
		/// <typeparam name="T6">The type of the sixth element.</typeparam>
		/// <typeparam name="T7">The type of the seventh element.</typeparam>
		/// <typeparam name="T8">The type of the eighth element, or <paramref name="value" /><see langword=".Rest.Item1" />.</typeparam>
		/// <typeparam name="T9">The type of the ninth element, or <paramref name="value" /><see langword=".Rest.Item2" />.</typeparam>
		/// <typeparam name="T10">The type of the tenth element, or <paramref name="value" /><see langword=".Rest.Item3" />.</typeparam>
		/// <typeparam name="T11">The type of the eleventh element, or <paramref name="value" /><see langword=".Rest.Item4" />.</typeparam>
		/// <typeparam name="T12">The type of the twelfth element, or <paramref name="value" /><see langword=".Rest.Item5" />.</typeparam>
		/// <typeparam name="T13">The type of the thirteenth element, or <paramref name="value" /><see langword=".Rest.Item6" />.</typeparam>
		/// <typeparam name="T14">The type of the fourteenth element, or <paramref name="value" /><see langword=".Rest.Item7" />.</typeparam>
		/// <typeparam name="T15">The type of the fifteenth element, or <paramref name="value" /><see langword=".Rest.Rest.Item1" />.</typeparam>
		/// <typeparam name="T16">The type of the sixteenth element, or <paramref name="value" /><see langword=".Rest.Rest.Item2" />.</typeparam>
		/// <typeparam name="T17">The type of the seventeenth element, or <paramref name="value" /><see langword=".Rest.Rest.Item3" />.</typeparam>
		/// <typeparam name="T18">The type of the eighteenth element, or <paramref name="value" /><see langword=".Rest.Rest.Item4" />.</typeparam>
		/// <typeparam name="T19">The type of the nineteenth element, or <paramref name="value" /><see langword=".Rest.Rest.Item5" />.</typeparam>
		/// <typeparam name="T20">The type of the twentieth element, or <paramref name="value" /><see langword=".Rest.Rest.Item6" />.</typeparam>
		/// <returns>The converted tuple.</returns>
		// Token: 0x06001105 RID: 4357 RVA: 0x0004639C File Offset: 0x0004459C
		public static Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14, Tuple<T15, T16, T17, T18, T19, T20>>> ToTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20>(this ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15, T16, T17, T18, T19, T20>>> value)
		{
			return TupleExtensions.CreateLongRef<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14, Tuple<T15, T16, T17, T18, T19, T20>>>(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7, TupleExtensions.CreateLongRef<T8, T9, T10, T11, T12, T13, T14, Tuple<T15, T16, T17, T18, T19, T20>>(value.Rest.Item1, value.Rest.Item2, value.Rest.Item3, value.Rest.Item4, value.Rest.Item5, value.Rest.Item6, value.Rest.Item7, Tuple.Create<T15, T16, T17, T18, T19, T20>(value.Rest.Rest.Item1, value.Rest.Rest.Item2, value.Rest.Rest.Item3, value.Rest.Rest.Item4, value.Rest.Rest.Item5, value.Rest.Rest.Item6)));
		}

		/// <summary>Converts an instance of the <see langword="ValueTuple" /> structure to an instance of the  <see langword="Tuple" /> class.</summary>
		/// <param name="value">The value tuple instance to convert to a tuple.</param>
		/// <typeparam name="T1">The type of the first element.</typeparam>
		/// <typeparam name="T2">The type of the second element.</typeparam>
		/// <typeparam name="T3">The type of the third element.</typeparam>
		/// <typeparam name="T4">The type of the fourth element.</typeparam>
		/// <typeparam name="T5">The type of the fifth element.</typeparam>
		/// <typeparam name="T6">The type of the sixth element.</typeparam>
		/// <typeparam name="T7">The type of the seventh element.</typeparam>
		/// <typeparam name="T8">The type of the eighth element, or <paramref name="value" /><see langword=".Rest.Item1" />.</typeparam>
		/// <typeparam name="T9">The type of the ninth element, or <paramref name="value" /><see langword=".Rest.Item2" />.</typeparam>
		/// <typeparam name="T10">The type of the tenth element, or <paramref name="value" /><see langword=".Rest.Item3" />.</typeparam>
		/// <typeparam name="T11">The type of the eleventh element, or <paramref name="value" /><see langword=".Rest.Item4" />.</typeparam>
		/// <typeparam name="T12">The type of the twelfth element, or <paramref name="value" /><see langword=".Rest.Item5" />.</typeparam>
		/// <typeparam name="T13">The type of the thirteenth element, or <paramref name="value" /><see langword=".Rest.Item6" />.</typeparam>
		/// <typeparam name="T14">The type of the fourteenth element, or <paramref name="value" /><see langword=".Rest.Item7" />.</typeparam>
		/// <typeparam name="T15">The type of the fifteenth element, or <paramref name="value" /><see langword=".Rest.Rest.Item1" />.</typeparam>
		/// <typeparam name="T16">The type of the sixteenth element, or <paramref name="value" /><see langword=".Rest.Rest.Item2" />.</typeparam>
		/// <typeparam name="T17">The type of the seventeenth element, or <paramref name="value" /><see langword=".Rest.Rest.Item3" />.</typeparam>
		/// <typeparam name="T18">The type of the eighteenth element, or <paramref name="value" /><see langword=".Rest.Rest.Item4" />.</typeparam>
		/// <typeparam name="T19">The type of the nineteenth element, or <paramref name="value" /><see langword=".Rest.Rest.Item5" />.</typeparam>
		/// <typeparam name="T20">The type of the twentieth element, or <paramref name="value" /><see langword=".Rest.Rest.Item6" />.</typeparam>
		/// <typeparam name="T21">The type of the twenty-first element, or <paramref name="value" /><see langword=".Rest.Rest.Item7" />.</typeparam>
		/// <returns>The converted tuple.</returns>
		// Token: 0x06001106 RID: 4358 RVA: 0x00046490 File Offset: 0x00044690
		public static Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14, Tuple<T15, T16, T17, T18, T19, T20, T21>>> ToTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21>(this ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15, T16, T17, T18, T19, T20, T21>>> value)
		{
			return TupleExtensions.CreateLongRef<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14, Tuple<T15, T16, T17, T18, T19, T20, T21>>>(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7, TupleExtensions.CreateLongRef<T8, T9, T10, T11, T12, T13, T14, Tuple<T15, T16, T17, T18, T19, T20, T21>>(value.Rest.Item1, value.Rest.Item2, value.Rest.Item3, value.Rest.Item4, value.Rest.Item5, value.Rest.Item6, value.Rest.Item7, Tuple.Create<T15, T16, T17, T18, T19, T20, T21>(value.Rest.Rest.Item1, value.Rest.Rest.Item2, value.Rest.Rest.Item3, value.Rest.Rest.Item4, value.Rest.Rest.Item5, value.Rest.Rest.Item6, value.Rest.Rest.Item7)));
		}

		// Token: 0x06001107 RID: 4359 RVA: 0x00046593 File Offset: 0x00044793
		private static ValueTuple<T1, T2, T3, T4, T5, T6, T7, TRest> CreateLong<T1, T2, T3, T4, T5, T6, T7, TRest>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, TRest rest) where TRest : struct, ITuple
		{
			return new ValueTuple<T1, T2, T3, T4, T5, T6, T7, TRest>(item1, item2, item3, item4, item5, item6, item7, rest);
		}

		// Token: 0x06001108 RID: 4360 RVA: 0x000465A6 File Offset: 0x000447A6
		private static Tuple<T1, T2, T3, T4, T5, T6, T7, TRest> CreateLongRef<T1, T2, T3, T4, T5, T6, T7, TRest>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, TRest rest) where TRest : ITuple
		{
			return new Tuple<T1, T2, T3, T4, T5, T6, T7, TRest>(item1, item2, item3, item4, item5, item6, item7, rest);
		}
	}
}
