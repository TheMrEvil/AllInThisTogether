using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace QFSW.QC.Parsers
{
	// Token: 0x0200000E RID: 14
	public class TupleParser : MassGenericQcParser
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000024 RID: 36 RVA: 0x000028F0 File Offset: 0x00000AF0
		protected override HashSet<Type> GenericTypes
		{
			[CompilerGenerated]
			get
			{
				return this.<GenericTypes>k__BackingField;
			}
		} = new HashSet<Type>
		{
			typeof(ValueTuple<>),
			typeof(ValueTuple<, >),
			typeof(ValueTuple<, , >),
			typeof(ValueTuple<, , , >),
			typeof(ValueTuple<, , , , >),
			typeof(ValueTuple<, , , , , >),
			typeof(ValueTuple<, , , , , , >),
			typeof(ValueTuple<, , , , , , , >),
			typeof(Tuple<>),
			typeof(Tuple<, >),
			typeof(Tuple<, , >),
			typeof(Tuple<, , , >),
			typeof(Tuple<, , , , >),
			typeof(Tuple<, , , , , >),
			typeof(Tuple<, , , , , , >),
			typeof(Tuple<, , , , , , , >)
		};

		// Token: 0x06000025 RID: 37 RVA: 0x000028F8 File Offset: 0x00000AF8
		public override object Parse(string value, Type type)
		{
			TextProcessing.ScopedSplitOptions @default = TextProcessing.ScopedSplitOptions.Default;
			@default.MaxCount = 8;
			string[] array = value.ReduceScope('(', ')').SplitScoped(',', @default);
			Type[] genericArguments = type.GetGenericArguments();
			if (genericArguments.Length != array.Length)
			{
				throw new ParserInputException(string.Format("Desired tuple type {0} has {1} elements but input contained {2}.", type, genericArguments.Length, array.Length));
			}
			object[] array2 = new object[array.Length];
			for (int i = 0; i < array2.Length; i++)
			{
				array2[i] = base.ParseRecursive(array[i], genericArguments[i]);
			}
			return Activator.CreateInstance(type, array2);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x0000298C File Offset: 0x00000B8C
		public TupleParser()
		{
		}

		// Token: 0x04000005 RID: 5
		private const int MaxFlatTupleSize = 8;

		// Token: 0x04000006 RID: 6
		[CompilerGenerated]
		private readonly HashSet<Type> <GenericTypes>k__BackingField;
	}
}
