using System;
using System.Collections;

namespace QFSW.QC.Serializers
{
	// Token: 0x02000004 RID: 4
	public class IEnumerableSerializer : IEnumerableSerializer<IEnumerable>
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000005 RID: 5 RVA: 0x000020A5 File Offset: 0x000002A5
		public override int Priority
		{
			get
			{
				return base.Priority - 1000;
			}
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000020B3 File Offset: 0x000002B3
		protected override IEnumerable GetObjectStream(IEnumerable value)
		{
			return value;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000020B6 File Offset: 0x000002B6
		public IEnumerableSerializer()
		{
		}
	}
}
