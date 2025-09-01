using System;
using System.Collections;

namespace System.Xml.Serialization
{
	// Token: 0x02000290 RID: 656
	internal class MemberMappingComparer : IComparer
	{
		// Token: 0x060018C7 RID: 6343 RVA: 0x0008F448 File Offset: 0x0008D648
		public int Compare(object o1, object o2)
		{
			MemberMapping memberMapping = (MemberMapping)o1;
			MemberMapping memberMapping2 = (MemberMapping)o2;
			if (memberMapping.IsText)
			{
				if (memberMapping2.IsText)
				{
					return 0;
				}
				return 1;
			}
			else
			{
				if (memberMapping2.IsText)
				{
					return -1;
				}
				if (memberMapping.SequenceId < 0 && memberMapping2.SequenceId < 0)
				{
					return 0;
				}
				if (memberMapping.SequenceId < 0)
				{
					return 1;
				}
				if (memberMapping2.SequenceId < 0)
				{
					return -1;
				}
				if (memberMapping.SequenceId < memberMapping2.SequenceId)
				{
					return -1;
				}
				if (memberMapping.SequenceId > memberMapping2.SequenceId)
				{
					return 1;
				}
				return 0;
			}
		}

		// Token: 0x060018C8 RID: 6344 RVA: 0x0000216B File Offset: 0x0000036B
		public MemberMappingComparer()
		{
		}
	}
}
