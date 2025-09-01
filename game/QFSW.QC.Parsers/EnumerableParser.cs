using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace QFSW.QC.Parsers
{
	// Token: 0x02000007 RID: 7
	public class EnumerableParser : MassGenericQcParser
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000011 RID: 17 RVA: 0x000025AF File Offset: 0x000007AF
		protected override HashSet<Type> GenericTypes
		{
			[CompilerGenerated]
			get
			{
				return this.<GenericTypes>k__BackingField;
			}
		} = new HashSet<Type>
		{
			typeof(IEnumerable<>),
			typeof(ICollection<>),
			typeof(IReadOnlyCollection<>),
			typeof(IList<>),
			typeof(IReadOnlyList<>)
		};

		// Token: 0x06000012 RID: 18 RVA: 0x000025B8 File Offset: 0x000007B8
		public override object Parse(string value, Type type)
		{
			Type type2 = type.GetGenericArguments()[0].MakeArrayType();
			return base.ParseRecursive(value, type2);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000025DC File Offset: 0x000007DC
		public EnumerableParser()
		{
		}

		// Token: 0x04000003 RID: 3
		[CompilerGenerated]
		private readonly HashSet<Type> <GenericTypes>k__BackingField;
	}
}
