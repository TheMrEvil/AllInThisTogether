using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Parse.Abstractions.Infrastructure;
using Parse.Abstractions.Infrastructure.Control;

namespace Parse.Infrastructure.Control
{
	// Token: 0x0200006E RID: 110
	public class ParseIncrementOperation : IParseFieldOperation
	{
		// Token: 0x1700016F RID: 367
		// (get) Token: 0x060004B4 RID: 1204 RVA: 0x00010840 File Offset: 0x0000EA40
		private static IDictionary<Tuple<Type, Type>, Func<object, object, object>> Adders
		{
			[CompilerGenerated]
			get
			{
				return ParseIncrementOperation.<Adders>k__BackingField;
			}
		}

		// Token: 0x060004B5 RID: 1205 RVA: 0x00010848 File Offset: 0x0000EA48
		static ParseIncrementOperation()
		{
			Dictionary<Tuple<Type, Type>, Func<object, object, object>> dictionary = new Dictionary<Tuple<Type, Type>, Func<object, object, object>>();
			Tuple<Type, Type> key = new Tuple<Type, Type>(typeof(sbyte), typeof(sbyte));
			dictionary[key] = ((object left, object right) => (int)((sbyte)left + (sbyte)right));
			Tuple<Type, Type> key2 = new Tuple<Type, Type>(typeof(sbyte), typeof(short));
			dictionary[key2] = ((object left, object right) => (int)((short)((sbyte)left) + (short)right));
			Tuple<Type, Type> key3 = new Tuple<Type, Type>(typeof(sbyte), typeof(int));
			dictionary[key3] = ((object left, object right) => (int)((sbyte)left) + (int)right);
			Tuple<Type, Type> key4 = new Tuple<Type, Type>(typeof(sbyte), typeof(long));
			dictionary[key4] = ((object left, object right) => (long)((sbyte)left) + (long)right);
			Tuple<Type, Type> key5 = new Tuple<Type, Type>(typeof(sbyte), typeof(float));
			dictionary[key5] = ((object left, object right) => (float)((sbyte)left) + (float)right);
			Tuple<Type, Type> key6 = new Tuple<Type, Type>(typeof(sbyte), typeof(double));
			dictionary[key6] = ((object left, object right) => (double)((sbyte)left) + (double)right);
			Tuple<Type, Type> key7 = new Tuple<Type, Type>(typeof(sbyte), typeof(decimal));
			dictionary[key7] = ((object left, object right) => (sbyte)left + (decimal)right);
			Tuple<Type, Type> key8 = new Tuple<Type, Type>(typeof(byte), typeof(byte));
			dictionary[key8] = ((object left, object right) => (int)((byte)left + (byte)right));
			Tuple<Type, Type> key9 = new Tuple<Type, Type>(typeof(byte), typeof(short));
			dictionary[key9] = ((object left, object right) => (int)((short)((byte)left) + (short)right));
			Tuple<Type, Type> key10 = new Tuple<Type, Type>(typeof(byte), typeof(ushort));
			dictionary[key10] = ((object left, object right) => (int)((ushort)((byte)left) + (ushort)right));
			Tuple<Type, Type> key11 = new Tuple<Type, Type>(typeof(byte), typeof(int));
			dictionary[key11] = ((object left, object right) => (int)((byte)left) + (int)right);
			Tuple<Type, Type> key12 = new Tuple<Type, Type>(typeof(byte), typeof(uint));
			dictionary[key12] = ((object left, object right) => (uint)((byte)left) + (uint)right);
			Tuple<Type, Type> key13 = new Tuple<Type, Type>(typeof(byte), typeof(long));
			dictionary[key13] = ((object left, object right) => (long)((ulong)((byte)left) + (ulong)((long)right)));
			Tuple<Type, Type> key14 = new Tuple<Type, Type>(typeof(byte), typeof(ulong));
			dictionary[key14] = ((object left, object right) => (ulong)((byte)left) + (ulong)right);
			Tuple<Type, Type> key15 = new Tuple<Type, Type>(typeof(byte), typeof(float));
			dictionary[key15] = ((object left, object right) => (float)((byte)left) + (float)right);
			Tuple<Type, Type> key16 = new Tuple<Type, Type>(typeof(byte), typeof(double));
			dictionary[key16] = ((object left, object right) => (double)((byte)left) + (double)right);
			Tuple<Type, Type> key17 = new Tuple<Type, Type>(typeof(byte), typeof(decimal));
			dictionary[key17] = ((object left, object right) => (byte)left + (decimal)right);
			Tuple<Type, Type> key18 = new Tuple<Type, Type>(typeof(short), typeof(short));
			dictionary[key18] = ((object left, object right) => (int)((short)left + (short)right));
			Tuple<Type, Type> key19 = new Tuple<Type, Type>(typeof(short), typeof(int));
			dictionary[key19] = ((object left, object right) => (int)((short)left) + (int)right);
			Tuple<Type, Type> key20 = new Tuple<Type, Type>(typeof(short), typeof(long));
			dictionary[key20] = ((object left, object right) => (long)((short)left) + (long)right);
			Tuple<Type, Type> key21 = new Tuple<Type, Type>(typeof(short), typeof(float));
			dictionary[key21] = ((object left, object right) => (float)((short)left) + (float)right);
			Tuple<Type, Type> key22 = new Tuple<Type, Type>(typeof(short), typeof(double));
			dictionary[key22] = ((object left, object right) => (double)((short)left) + (double)right);
			Tuple<Type, Type> key23 = new Tuple<Type, Type>(typeof(short), typeof(decimal));
			dictionary[key23] = ((object left, object right) => (short)left + (decimal)right);
			Tuple<Type, Type> key24 = new Tuple<Type, Type>(typeof(ushort), typeof(ushort));
			dictionary[key24] = ((object left, object right) => (int)((ushort)left + (ushort)right));
			Tuple<Type, Type> key25 = new Tuple<Type, Type>(typeof(ushort), typeof(int));
			dictionary[key25] = ((object left, object right) => (int)((ushort)left) + (int)right);
			Tuple<Type, Type> key26 = new Tuple<Type, Type>(typeof(ushort), typeof(uint));
			dictionary[key26] = ((object left, object right) => (uint)((ushort)left) + (uint)right);
			Tuple<Type, Type> key27 = new Tuple<Type, Type>(typeof(ushort), typeof(long));
			dictionary[key27] = ((object left, object right) => (long)((ulong)((ushort)left) + (ulong)((long)right)));
			Tuple<Type, Type> key28 = new Tuple<Type, Type>(typeof(ushort), typeof(ulong));
			dictionary[key28] = ((object left, object right) => (ulong)((ushort)left) + (ulong)right);
			Tuple<Type, Type> key29 = new Tuple<Type, Type>(typeof(ushort), typeof(float));
			dictionary[key29] = ((object left, object right) => (float)((ushort)left) + (float)right);
			Tuple<Type, Type> key30 = new Tuple<Type, Type>(typeof(ushort), typeof(double));
			dictionary[key30] = ((object left, object right) => (double)((ushort)left) + (double)right);
			Tuple<Type, Type> key31 = new Tuple<Type, Type>(typeof(ushort), typeof(decimal));
			dictionary[key31] = ((object left, object right) => (ushort)left + (decimal)right);
			Tuple<Type, Type> key32 = new Tuple<Type, Type>(typeof(int), typeof(int));
			dictionary[key32] = ((object left, object right) => (int)left + (int)right);
			Tuple<Type, Type> key33 = new Tuple<Type, Type>(typeof(int), typeof(long));
			dictionary[key33] = ((object left, object right) => (long)((int)left) + (long)right);
			Tuple<Type, Type> key34 = new Tuple<Type, Type>(typeof(int), typeof(float));
			dictionary[key34] = ((object left, object right) => (float)((int)left) + (float)right);
			Tuple<Type, Type> key35 = new Tuple<Type, Type>(typeof(int), typeof(double));
			dictionary[key35] = ((object left, object right) => (double)((int)left) + (double)right);
			Tuple<Type, Type> key36 = new Tuple<Type, Type>(typeof(int), typeof(decimal));
			dictionary[key36] = ((object left, object right) => (int)left + (decimal)right);
			Tuple<Type, Type> key37 = new Tuple<Type, Type>(typeof(uint), typeof(uint));
			dictionary[key37] = ((object left, object right) => (uint)left + (uint)right);
			Tuple<Type, Type> key38 = new Tuple<Type, Type>(typeof(uint), typeof(long));
			dictionary[key38] = ((object left, object right) => (long)((ulong)((uint)left) + (ulong)((long)right)));
			Tuple<Type, Type> key39 = new Tuple<Type, Type>(typeof(uint), typeof(ulong));
			dictionary[key39] = ((object left, object right) => (ulong)((uint)left) + (ulong)right);
			Tuple<Type, Type> key40 = new Tuple<Type, Type>(typeof(uint), typeof(float));
			dictionary[key40] = ((object left, object right) => (uint)left + (float)right);
			Tuple<Type, Type> key41 = new Tuple<Type, Type>(typeof(uint), typeof(double));
			dictionary[key41] = ((object left, object right) => (uint)left + (double)right);
			Tuple<Type, Type> key42 = new Tuple<Type, Type>(typeof(uint), typeof(decimal));
			dictionary[key42] = ((object left, object right) => (uint)left + (decimal)right);
			Tuple<Type, Type> key43 = new Tuple<Type, Type>(typeof(long), typeof(long));
			dictionary[key43] = ((object left, object right) => (long)left + (long)right);
			Tuple<Type, Type> key44 = new Tuple<Type, Type>(typeof(long), typeof(float));
			dictionary[key44] = ((object left, object right) => (float)((long)left) + (float)right);
			Tuple<Type, Type> key45 = new Tuple<Type, Type>(typeof(long), typeof(double));
			dictionary[key45] = ((object left, object right) => (double)((long)left) + (double)right);
			Tuple<Type, Type> key46 = new Tuple<Type, Type>(typeof(long), typeof(decimal));
			dictionary[key46] = ((object left, object right) => (long)left + (decimal)right);
			Tuple<Type, Type> key47 = new Tuple<Type, Type>(typeof(char), typeof(char));
			dictionary[key47] = ((object left, object right) => (int)((char)left + (char)right));
			Tuple<Type, Type> key48 = new Tuple<Type, Type>(typeof(char), typeof(ushort));
			dictionary[key48] = ((object left, object right) => (int)((char)left + (char)((ushort)right)));
			Tuple<Type, Type> key49 = new Tuple<Type, Type>(typeof(char), typeof(int));
			dictionary[key49] = ((object left, object right) => (int)((char)left) + (int)right);
			Tuple<Type, Type> key50 = new Tuple<Type, Type>(typeof(char), typeof(uint));
			dictionary[key50] = ((object left, object right) => (uint)((char)left) + (uint)right);
			Tuple<Type, Type> key51 = new Tuple<Type, Type>(typeof(char), typeof(long));
			dictionary[key51] = ((object left, object right) => (long)((ulong)((char)left) + (ulong)((long)right)));
			Tuple<Type, Type> key52 = new Tuple<Type, Type>(typeof(char), typeof(ulong));
			dictionary[key52] = ((object left, object right) => (ulong)((char)left) + (ulong)right);
			Tuple<Type, Type> key53 = new Tuple<Type, Type>(typeof(char), typeof(float));
			dictionary[key53] = ((object left, object right) => (float)((char)left) + (float)right);
			Tuple<Type, Type> key54 = new Tuple<Type, Type>(typeof(char), typeof(double));
			dictionary[key54] = ((object left, object right) => (double)((char)left) + (double)right);
			Tuple<Type, Type> key55 = new Tuple<Type, Type>(typeof(char), typeof(decimal));
			dictionary[key55] = ((object left, object right) => (char)left + (decimal)right);
			Tuple<Type, Type> key56 = new Tuple<Type, Type>(typeof(float), typeof(float));
			dictionary[key56] = ((object left, object right) => (float)left + (float)right);
			Tuple<Type, Type> key57 = new Tuple<Type, Type>(typeof(float), typeof(double));
			dictionary[key57] = ((object left, object right) => (double)((float)left) + (double)right);
			Tuple<Type, Type> key58 = new Tuple<Type, Type>(typeof(ulong), typeof(ulong));
			dictionary[key58] = ((object left, object right) => (ulong)left + (ulong)right);
			Tuple<Type, Type> key59 = new Tuple<Type, Type>(typeof(ulong), typeof(float));
			dictionary[key59] = ((object left, object right) => (ulong)left + (float)right);
			Tuple<Type, Type> key60 = new Tuple<Type, Type>(typeof(ulong), typeof(double));
			dictionary[key60] = ((object left, object right) => (ulong)left + (double)right);
			Tuple<Type, Type> key61 = new Tuple<Type, Type>(typeof(ulong), typeof(decimal));
			dictionary[key61] = ((object left, object right) => (ulong)left + (decimal)right);
			Tuple<Type, Type> key62 = new Tuple<Type, Type>(typeof(double), typeof(double));
			dictionary[key62] = ((object left, object right) => (double)left + (double)right);
			Tuple<Type, Type> key63 = new Tuple<Type, Type>(typeof(decimal), typeof(decimal));
			dictionary[key63] = ((object left, object right) => (decimal)left + (decimal)right);
			ParseIncrementOperation.Adders = dictionary;
			foreach (Tuple<Type, Type> tuple in ParseIncrementOperation.Adders.Keys.ToList<Tuple<Type, Type>>())
			{
				if (!tuple.Item1.Equals(tuple.Item2))
				{
					Tuple<Type, Type> key64 = new Tuple<Type, Type>(tuple.Item2, tuple.Item1);
					Func<object, object, object> func = ParseIncrementOperation.Adders[tuple];
					ParseIncrementOperation.Adders[key64] = ((object left, object right) => func(right, left));
				}
			}
		}

		// Token: 0x060004B6 RID: 1206 RVA: 0x0001158C File Offset: 0x0000F78C
		public ParseIncrementOperation(object amount)
		{
			this.Amount = amount;
		}

		// Token: 0x060004B7 RID: 1207 RVA: 0x0001159B File Offset: 0x0000F79B
		public object Encode(IServiceHub serviceHub)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary["__op"] = "Increment";
			dictionary["amount"] = this.Amount;
			return dictionary;
		}

		// Token: 0x060004B8 RID: 1208 RVA: 0x000115C4 File Offset: 0x0000F7C4
		private static object Add(object first, object second)
		{
			Func<object, object, object> func;
			if (!ParseIncrementOperation.Adders.TryGetValue(new Tuple<Type, Type>(first.GetType(), second.GetType()), out func))
			{
				throw new InvalidCastException(string.Format("Could not add objects of type {0} and {1} to each other.", first.GetType(), second.GetType()));
			}
			return func(first, second);
		}

		// Token: 0x060004B9 RID: 1209 RVA: 0x00011614 File Offset: 0x0000F814
		public IParseFieldOperation MergeWithPrevious(IParseFieldOperation previous)
		{
			IParseFieldOperation result;
			if (previous != null)
			{
				if (!(previous is ParseDeleteOperation))
				{
					ParseSetOperation parseSetOperation = previous as ParseSetOperation;
					if (parseSetOperation != null)
					{
						object value = parseSetOperation.Value;
						if (value is string)
						{
							throw new InvalidOperationException("Cannot increment a non-number type.");
						}
						result = new ParseSetOperation(ParseIncrementOperation.Add(value, this.Amount));
					}
					else
					{
						ParseIncrementOperation parseIncrementOperation = previous as ParseIncrementOperation;
						if (parseIncrementOperation == null)
						{
							throw new InvalidOperationException("Operation is invalid after previous operation.");
						}
						object amount = parseIncrementOperation.Amount;
						result = new ParseIncrementOperation(ParseIncrementOperation.Add(amount, this.Amount));
					}
				}
				else
				{
					result = new ParseSetOperation(this.Amount);
				}
			}
			else
			{
				result = this;
			}
			return result;
		}

		// Token: 0x060004BA RID: 1210 RVA: 0x000116AE File Offset: 0x0000F8AE
		public object Apply(object oldValue, string key)
		{
			if (!(oldValue is string))
			{
				return ParseIncrementOperation.Add(oldValue ?? 0, this.Amount);
			}
			throw new InvalidOperationException("Cannot increment a non-number type.");
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x060004BB RID: 1211 RVA: 0x000116D9 File Offset: 0x0000F8D9
		public object Amount
		{
			[CompilerGenerated]
			get
			{
				return this.<Amount>k__BackingField;
			}
		}

		// Token: 0x040000F7 RID: 247
		[CompilerGenerated]
		private static readonly IDictionary<Tuple<Type, Type>, Func<object, object, object>> <Adders>k__BackingField;

		// Token: 0x040000F8 RID: 248
		[CompilerGenerated]
		private readonly object <Amount>k__BackingField;

		// Token: 0x0200013B RID: 315
		[CompilerGenerated]
		private sealed class <>c__DisplayClass3_0
		{
			// Token: 0x060007C2 RID: 1986 RVA: 0x0001777B File Offset: 0x0001597B
			public <>c__DisplayClass3_0()
			{
			}

			// Token: 0x060007C3 RID: 1987 RVA: 0x00017783 File Offset: 0x00015983
			internal object <.cctor>b__0(object left, object right)
			{
				return this.func(right, left);
			}

			// Token: 0x040002E5 RID: 741
			public Func<object, object, object> func;
		}

		// Token: 0x0200013C RID: 316
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x060007C4 RID: 1988 RVA: 0x00017792 File Offset: 0x00015992
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x060007C5 RID: 1989 RVA: 0x0001779E File Offset: 0x0001599E
			public <>c()
			{
			}

			// Token: 0x060007C6 RID: 1990 RVA: 0x000177A6 File Offset: 0x000159A6
			internal object <.cctor>b__3_1(object left, object right)
			{
				return (int)((sbyte)left + (sbyte)right);
			}

			// Token: 0x060007C7 RID: 1991 RVA: 0x000177BA File Offset: 0x000159BA
			internal object <.cctor>b__3_2(object left, object right)
			{
				return (int)((short)((sbyte)left) + (short)right);
			}

			// Token: 0x060007C8 RID: 1992 RVA: 0x000177CE File Offset: 0x000159CE
			internal object <.cctor>b__3_3(object left, object right)
			{
				return (int)((sbyte)left) + (int)right;
			}

			// Token: 0x060007C9 RID: 1993 RVA: 0x000177E2 File Offset: 0x000159E2
			internal object <.cctor>b__3_4(object left, object right)
			{
				return (long)((sbyte)left) + (long)right;
			}

			// Token: 0x060007CA RID: 1994 RVA: 0x000177F7 File Offset: 0x000159F7
			internal object <.cctor>b__3_5(object left, object right)
			{
				return (float)((sbyte)left) + (float)right;
			}

			// Token: 0x060007CB RID: 1995 RVA: 0x0001780C File Offset: 0x00015A0C
			internal object <.cctor>b__3_6(object left, object right)
			{
				return (double)((sbyte)left) + (double)right;
			}

			// Token: 0x060007CC RID: 1996 RVA: 0x00017821 File Offset: 0x00015A21
			internal object <.cctor>b__3_7(object left, object right)
			{
				return (sbyte)left + (decimal)right;
			}

			// Token: 0x060007CD RID: 1997 RVA: 0x0001783E File Offset: 0x00015A3E
			internal object <.cctor>b__3_8(object left, object right)
			{
				return (int)((byte)left + (byte)right);
			}

			// Token: 0x060007CE RID: 1998 RVA: 0x00017852 File Offset: 0x00015A52
			internal object <.cctor>b__3_9(object left, object right)
			{
				return (int)((short)((byte)left) + (short)right);
			}

			// Token: 0x060007CF RID: 1999 RVA: 0x00017866 File Offset: 0x00015A66
			internal object <.cctor>b__3_10(object left, object right)
			{
				return (int)((ushort)((byte)left) + (ushort)right);
			}

			// Token: 0x060007D0 RID: 2000 RVA: 0x0001787A File Offset: 0x00015A7A
			internal object <.cctor>b__3_11(object left, object right)
			{
				return (int)((byte)left) + (int)right;
			}

			// Token: 0x060007D1 RID: 2001 RVA: 0x0001788E File Offset: 0x00015A8E
			internal object <.cctor>b__3_12(object left, object right)
			{
				return (uint)((byte)left) + (uint)right;
			}

			// Token: 0x060007D2 RID: 2002 RVA: 0x000178A2 File Offset: 0x00015AA2
			internal object <.cctor>b__3_13(object left, object right)
			{
				return (long)((ulong)((byte)left) + (ulong)((long)right));
			}

			// Token: 0x060007D3 RID: 2003 RVA: 0x000178B7 File Offset: 0x00015AB7
			internal object <.cctor>b__3_14(object left, object right)
			{
				return (ulong)((byte)left) + (ulong)right;
			}

			// Token: 0x060007D4 RID: 2004 RVA: 0x000178CC File Offset: 0x00015ACC
			internal object <.cctor>b__3_15(object left, object right)
			{
				return (float)((byte)left) + (float)right;
			}

			// Token: 0x060007D5 RID: 2005 RVA: 0x000178E1 File Offset: 0x00015AE1
			internal object <.cctor>b__3_16(object left, object right)
			{
				return (double)((byte)left) + (double)right;
			}

			// Token: 0x060007D6 RID: 2006 RVA: 0x000178F6 File Offset: 0x00015AF6
			internal object <.cctor>b__3_17(object left, object right)
			{
				return (byte)left + (decimal)right;
			}

			// Token: 0x060007D7 RID: 2007 RVA: 0x00017913 File Offset: 0x00015B13
			internal object <.cctor>b__3_18(object left, object right)
			{
				return (int)((short)left + (short)right);
			}

			// Token: 0x060007D8 RID: 2008 RVA: 0x00017927 File Offset: 0x00015B27
			internal object <.cctor>b__3_19(object left, object right)
			{
				return (int)((short)left) + (int)right;
			}

			// Token: 0x060007D9 RID: 2009 RVA: 0x0001793B File Offset: 0x00015B3B
			internal object <.cctor>b__3_20(object left, object right)
			{
				return (long)((short)left) + (long)right;
			}

			// Token: 0x060007DA RID: 2010 RVA: 0x00017950 File Offset: 0x00015B50
			internal object <.cctor>b__3_21(object left, object right)
			{
				return (float)((short)left) + (float)right;
			}

			// Token: 0x060007DB RID: 2011 RVA: 0x00017965 File Offset: 0x00015B65
			internal object <.cctor>b__3_22(object left, object right)
			{
				return (double)((short)left) + (double)right;
			}

			// Token: 0x060007DC RID: 2012 RVA: 0x0001797A File Offset: 0x00015B7A
			internal object <.cctor>b__3_23(object left, object right)
			{
				return (short)left + (decimal)right;
			}

			// Token: 0x060007DD RID: 2013 RVA: 0x00017997 File Offset: 0x00015B97
			internal object <.cctor>b__3_24(object left, object right)
			{
				return (int)((ushort)left + (ushort)right);
			}

			// Token: 0x060007DE RID: 2014 RVA: 0x000179AB File Offset: 0x00015BAB
			internal object <.cctor>b__3_25(object left, object right)
			{
				return (int)((ushort)left) + (int)right;
			}

			// Token: 0x060007DF RID: 2015 RVA: 0x000179BF File Offset: 0x00015BBF
			internal object <.cctor>b__3_26(object left, object right)
			{
				return (uint)((ushort)left) + (uint)right;
			}

			// Token: 0x060007E0 RID: 2016 RVA: 0x000179D3 File Offset: 0x00015BD3
			internal object <.cctor>b__3_27(object left, object right)
			{
				return (long)((ulong)((ushort)left) + (ulong)((long)right));
			}

			// Token: 0x060007E1 RID: 2017 RVA: 0x000179E8 File Offset: 0x00015BE8
			internal object <.cctor>b__3_28(object left, object right)
			{
				return (ulong)((ushort)left) + (ulong)right;
			}

			// Token: 0x060007E2 RID: 2018 RVA: 0x000179FD File Offset: 0x00015BFD
			internal object <.cctor>b__3_29(object left, object right)
			{
				return (float)((ushort)left) + (float)right;
			}

			// Token: 0x060007E3 RID: 2019 RVA: 0x00017A12 File Offset: 0x00015C12
			internal object <.cctor>b__3_30(object left, object right)
			{
				return (double)((ushort)left) + (double)right;
			}

			// Token: 0x060007E4 RID: 2020 RVA: 0x00017A27 File Offset: 0x00015C27
			internal object <.cctor>b__3_31(object left, object right)
			{
				return (ushort)left + (decimal)right;
			}

			// Token: 0x060007E5 RID: 2021 RVA: 0x00017A44 File Offset: 0x00015C44
			internal object <.cctor>b__3_32(object left, object right)
			{
				return (int)left + (int)right;
			}

			// Token: 0x060007E6 RID: 2022 RVA: 0x00017A58 File Offset: 0x00015C58
			internal object <.cctor>b__3_33(object left, object right)
			{
				return (long)((int)left) + (long)right;
			}

			// Token: 0x060007E7 RID: 2023 RVA: 0x00017A6D File Offset: 0x00015C6D
			internal object <.cctor>b__3_34(object left, object right)
			{
				return (float)((int)left) + (float)right;
			}

			// Token: 0x060007E8 RID: 2024 RVA: 0x00017A82 File Offset: 0x00015C82
			internal object <.cctor>b__3_35(object left, object right)
			{
				return (double)((int)left) + (double)right;
			}

			// Token: 0x060007E9 RID: 2025 RVA: 0x00017A97 File Offset: 0x00015C97
			internal object <.cctor>b__3_36(object left, object right)
			{
				return (int)left + (decimal)right;
			}

			// Token: 0x060007EA RID: 2026 RVA: 0x00017AB4 File Offset: 0x00015CB4
			internal object <.cctor>b__3_37(object left, object right)
			{
				return (uint)left + (uint)right;
			}

			// Token: 0x060007EB RID: 2027 RVA: 0x00017AC8 File Offset: 0x00015CC8
			internal object <.cctor>b__3_38(object left, object right)
			{
				return (long)((ulong)((uint)left) + (ulong)((long)right));
			}

			// Token: 0x060007EC RID: 2028 RVA: 0x00017ADD File Offset: 0x00015CDD
			internal object <.cctor>b__3_39(object left, object right)
			{
				return (ulong)((uint)left) + (ulong)right;
			}

			// Token: 0x060007ED RID: 2029 RVA: 0x00017AF2 File Offset: 0x00015CF2
			internal object <.cctor>b__3_40(object left, object right)
			{
				return (uint)left + (float)right;
			}

			// Token: 0x060007EE RID: 2030 RVA: 0x00017B08 File Offset: 0x00015D08
			internal object <.cctor>b__3_41(object left, object right)
			{
				return (uint)left + (double)right;
			}

			// Token: 0x060007EF RID: 2031 RVA: 0x00017B1E File Offset: 0x00015D1E
			internal object <.cctor>b__3_42(object left, object right)
			{
				return (uint)left + (decimal)right;
			}

			// Token: 0x060007F0 RID: 2032 RVA: 0x00017B3B File Offset: 0x00015D3B
			internal object <.cctor>b__3_43(object left, object right)
			{
				return (long)left + (long)right;
			}

			// Token: 0x060007F1 RID: 2033 RVA: 0x00017B4F File Offset: 0x00015D4F
			internal object <.cctor>b__3_44(object left, object right)
			{
				return (float)((long)left) + (float)right;
			}

			// Token: 0x060007F2 RID: 2034 RVA: 0x00017B64 File Offset: 0x00015D64
			internal object <.cctor>b__3_45(object left, object right)
			{
				return (double)((long)left) + (double)right;
			}

			// Token: 0x060007F3 RID: 2035 RVA: 0x00017B79 File Offset: 0x00015D79
			internal object <.cctor>b__3_46(object left, object right)
			{
				return (long)left + (decimal)right;
			}

			// Token: 0x060007F4 RID: 2036 RVA: 0x00017B96 File Offset: 0x00015D96
			internal object <.cctor>b__3_47(object left, object right)
			{
				return (int)((char)left + (char)right);
			}

			// Token: 0x060007F5 RID: 2037 RVA: 0x00017BAA File Offset: 0x00015DAA
			internal object <.cctor>b__3_48(object left, object right)
			{
				return (int)((char)left + (char)((ushort)right));
			}

			// Token: 0x060007F6 RID: 2038 RVA: 0x00017BBE File Offset: 0x00015DBE
			internal object <.cctor>b__3_49(object left, object right)
			{
				return (int)((char)left) + (int)right;
			}

			// Token: 0x060007F7 RID: 2039 RVA: 0x00017BD2 File Offset: 0x00015DD2
			internal object <.cctor>b__3_50(object left, object right)
			{
				return (uint)((char)left) + (uint)right;
			}

			// Token: 0x060007F8 RID: 2040 RVA: 0x00017BE6 File Offset: 0x00015DE6
			internal object <.cctor>b__3_51(object left, object right)
			{
				return (long)((ulong)((char)left) + (ulong)((long)right));
			}

			// Token: 0x060007F9 RID: 2041 RVA: 0x00017BFB File Offset: 0x00015DFB
			internal object <.cctor>b__3_52(object left, object right)
			{
				return (ulong)((char)left) + (ulong)right;
			}

			// Token: 0x060007FA RID: 2042 RVA: 0x00017C10 File Offset: 0x00015E10
			internal object <.cctor>b__3_53(object left, object right)
			{
				return (float)((char)left) + (float)right;
			}

			// Token: 0x060007FB RID: 2043 RVA: 0x00017C25 File Offset: 0x00015E25
			internal object <.cctor>b__3_54(object left, object right)
			{
				return (double)((char)left) + (double)right;
			}

			// Token: 0x060007FC RID: 2044 RVA: 0x00017C3A File Offset: 0x00015E3A
			internal object <.cctor>b__3_55(object left, object right)
			{
				return (char)left + (decimal)right;
			}

			// Token: 0x060007FD RID: 2045 RVA: 0x00017C57 File Offset: 0x00015E57
			internal object <.cctor>b__3_56(object left, object right)
			{
				return (float)left + (float)right;
			}

			// Token: 0x060007FE RID: 2046 RVA: 0x00017C6B File Offset: 0x00015E6B
			internal object <.cctor>b__3_57(object left, object right)
			{
				return (double)((float)left) + (double)right;
			}

			// Token: 0x060007FF RID: 2047 RVA: 0x00017C80 File Offset: 0x00015E80
			internal object <.cctor>b__3_58(object left, object right)
			{
				return (ulong)left + (ulong)right;
			}

			// Token: 0x06000800 RID: 2048 RVA: 0x00017C94 File Offset: 0x00015E94
			internal object <.cctor>b__3_59(object left, object right)
			{
				return (ulong)left + (float)right;
			}

			// Token: 0x06000801 RID: 2049 RVA: 0x00017CAA File Offset: 0x00015EAA
			internal object <.cctor>b__3_60(object left, object right)
			{
				return (ulong)left + (double)right;
			}

			// Token: 0x06000802 RID: 2050 RVA: 0x00017CC0 File Offset: 0x00015EC0
			internal object <.cctor>b__3_61(object left, object right)
			{
				return (ulong)left + (decimal)right;
			}

			// Token: 0x06000803 RID: 2051 RVA: 0x00017CDD File Offset: 0x00015EDD
			internal object <.cctor>b__3_62(object left, object right)
			{
				return (double)left + (double)right;
			}

			// Token: 0x06000804 RID: 2052 RVA: 0x00017CF1 File Offset: 0x00015EF1
			internal object <.cctor>b__3_63(object left, object right)
			{
				return (decimal)left + (decimal)right;
			}

			// Token: 0x040002E6 RID: 742
			public static readonly ParseIncrementOperation.<>c <>9 = new ParseIncrementOperation.<>c();
		}
	}
}
