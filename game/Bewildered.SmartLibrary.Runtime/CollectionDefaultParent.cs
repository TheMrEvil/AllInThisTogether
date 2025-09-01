using System;
using System.Collections.Generic;
using UnityEngine;

namespace Bewildered.SmartLibrary
{
	// Token: 0x02000002 RID: 2
	[AddComponentMenu("")]
	internal class CollectionDefaultParent : MonoBehaviour
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public List<UniqueID> CollectionIds
		{
			get
			{
				return this._collectionIds;
			}
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002058 File Offset: 0x00000258
		public CollectionDefaultParent()
		{
		}

		// Token: 0x04000001 RID: 1
		[SerializeField]
		private List<UniqueID> _collectionIds = new List<UniqueID>();
	}
}
