using System;
using UnityEngine;

namespace MagicaCloth2
{
	// Token: 0x02000017 RID: 23
	[Serializable]
	public class ClothSerializeData2 : IDataValidate, IValid
	{
		// Token: 0x0600005D RID: 93 RVA: 0x000052EF File Offset: 0x000034EF
		public ClothSerializeData2()
		{
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00005302 File Offset: 0x00003502
		public bool IsValid()
		{
			return true;
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00005305 File Offset: 0x00003505
		public void DataValidate()
		{
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00005307 File Offset: 0x00003507
		public override int GetHashCode()
		{
			return 0;
		}

		// Token: 0x0400009E RID: 158
		[SerializeField]
		public SelectionData selectionData = new SelectionData();
	}
}
