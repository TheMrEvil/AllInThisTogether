using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace QFSW.QC.Parsers
{
	// Token: 0x02000004 RID: 4
	public class CollectionParser : MassGenericQcParser
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000007 RID: 7 RVA: 0x000021EC File Offset: 0x000003EC
		protected override HashSet<Type> GenericTypes
		{
			[CompilerGenerated]
			get
			{
				return this.<GenericTypes>k__BackingField;
			}
		} = new HashSet<Type>
		{
			typeof(List<>),
			typeof(Stack<>),
			typeof(Queue<>),
			typeof(HashSet<>),
			typeof(LinkedList<>),
			typeof(ConcurrentStack<>),
			typeof(ConcurrentQueue<>),
			typeof(ConcurrentBag<>)
		};

		// Token: 0x06000008 RID: 8 RVA: 0x000021F4 File Offset: 0x000003F4
		public override object Parse(string value, Type type)
		{
			Type type2 = type.GetGenericArguments()[0].MakeArrayType();
			object obj = base.ParseRecursive(value, type2);
			return Activator.CreateInstance(type, new object[]
			{
				obj
			});
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002228 File Offset: 0x00000428
		public CollectionParser()
		{
		}

		// Token: 0x04000001 RID: 1
		[CompilerGenerated]
		private readonly HashSet<Type> <GenericTypes>k__BackingField;
	}
}
