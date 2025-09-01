using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Linq.Expressions.Compiler
{
	// Token: 0x020002D1 RID: 721
	internal static class TypeInfoExtensions
	{
		// Token: 0x060015FA RID: 5626 RVA: 0x0004A240 File Offset: 0x00048440
		public static Type MakeDelegateType(this DelegateHelpers.TypeInfo info, Type retType, params Expression[] args)
		{
			return info.MakeDelegateType(retType, args);
		}

		// Token: 0x060015FB RID: 5627 RVA: 0x0004A24C File Offset: 0x0004844C
		public static Type MakeDelegateType(this DelegateHelpers.TypeInfo info, Type retType, IList<Expression> args)
		{
			Type[] array = new Type[args.Count + 2];
			array[0] = typeof(CallSite);
			array[array.Length - 1] = retType;
			for (int i = 0; i < args.Count; i++)
			{
				array[i + 1] = args[i].Type;
			}
			return info.DelegateType = DelegateHelpers.MakeNewDelegate(array);
		}
	}
}
