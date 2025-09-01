using System;

namespace UnityEngine.EventSystems
{
	// Token: 0x02000072 RID: 114
	public struct RaycastResult
	{
		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x06000677 RID: 1655 RVA: 0x0001B83C File Offset: 0x00019A3C
		// (set) Token: 0x06000678 RID: 1656 RVA: 0x0001B844 File Offset: 0x00019A44
		public GameObject gameObject
		{
			get
			{
				return this.m_GameObject;
			}
			set
			{
				this.m_GameObject = value;
			}
		}

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x06000679 RID: 1657 RVA: 0x0001B84D File Offset: 0x00019A4D
		public bool isValid
		{
			get
			{
				return this.module != null && this.gameObject != null;
			}
		}

		// Token: 0x0600067A RID: 1658 RVA: 0x0001B86C File Offset: 0x00019A6C
		public void Clear()
		{
			this.gameObject = null;
			this.module = null;
			this.distance = 0f;
			this.index = 0f;
			this.depth = 0;
			this.sortingLayer = 0;
			this.sortingOrder = 0;
			this.worldNormal = Vector3.up;
			this.worldPosition = Vector3.zero;
			this.screenPosition = Vector3.zero;
		}

		// Token: 0x0600067B RID: 1659 RVA: 0x0001B8D8 File Offset: 0x00019AD8
		public override string ToString()
		{
			if (!this.isValid)
			{
				return "";
			}
			string[] array = new string[24];
			array[0] = "Name: ";
			int num = 1;
			GameObject gameObject = this.gameObject;
			array[num] = ((gameObject != null) ? gameObject.ToString() : null);
			array[2] = "\nmodule: ";
			int num2 = 3;
			BaseRaycaster baseRaycaster = this.module;
			array[num2] = ((baseRaycaster != null) ? baseRaycaster.ToString() : null);
			array[4] = "\ndistance: ";
			array[5] = this.distance.ToString();
			array[6] = "\nindex: ";
			array[7] = this.index.ToString();
			array[8] = "\ndepth: ";
			array[9] = this.depth.ToString();
			array[10] = "\nworldNormal: ";
			int num3 = 11;
			Vector3 vector = this.worldNormal;
			array[num3] = vector.ToString();
			array[12] = "\nworldPosition: ";
			int num4 = 13;
			vector = this.worldPosition;
			array[num4] = vector.ToString();
			array[14] = "\nscreenPosition: ";
			int num5 = 15;
			Vector2 vector2 = this.screenPosition;
			array[num5] = vector2.ToString();
			array[16] = "\nmodule.sortOrderPriority: ";
			array[17] = this.module.sortOrderPriority.ToString();
			array[18] = "\nmodule.renderOrderPriority: ";
			array[19] = this.module.renderOrderPriority.ToString();
			array[20] = "\nsortingLayer: ";
			array[21] = this.sortingLayer.ToString();
			array[22] = "\nsortingOrder: ";
			array[23] = this.sortingOrder.ToString();
			return string.Concat(array);
		}

		// Token: 0x04000231 RID: 561
		private GameObject m_GameObject;

		// Token: 0x04000232 RID: 562
		public BaseRaycaster module;

		// Token: 0x04000233 RID: 563
		public float distance;

		// Token: 0x04000234 RID: 564
		public float index;

		// Token: 0x04000235 RID: 565
		public int depth;

		// Token: 0x04000236 RID: 566
		public int sortingGroupID;

		// Token: 0x04000237 RID: 567
		public int sortingGroupOrder;

		// Token: 0x04000238 RID: 568
		public int sortingLayer;

		// Token: 0x04000239 RID: 569
		public int sortingOrder;

		// Token: 0x0400023A RID: 570
		public Vector3 worldPosition;

		// Token: 0x0400023B RID: 571
		public Vector3 worldNormal;

		// Token: 0x0400023C RID: 572
		public Vector2 screenPosition;

		// Token: 0x0400023D RID: 573
		public int displayIndex;
	}
}
