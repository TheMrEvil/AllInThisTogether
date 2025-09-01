using System;
using System.Collections.Generic;

namespace UnityEngine.EventSystems
{
	// Token: 0x0200006F RID: 111
	public abstract class BaseRaycaster : UIBehaviour
	{
		// Token: 0x0600065F RID: 1631
		public abstract void Raycast(PointerEventData eventData, List<RaycastResult> resultAppendList);

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x06000660 RID: 1632
		public abstract Camera eventCamera { get; }

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x06000661 RID: 1633 RVA: 0x0001B122 File Offset: 0x00019322
		[Obsolete("Please use sortOrderPriority and renderOrderPriority", false)]
		public virtual int priority
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x06000662 RID: 1634 RVA: 0x0001B125 File Offset: 0x00019325
		public virtual int sortOrderPriority
		{
			get
			{
				return int.MinValue;
			}
		}

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x06000663 RID: 1635 RVA: 0x0001B12C File Offset: 0x0001932C
		public virtual int renderOrderPriority
		{
			get
			{
				return int.MinValue;
			}
		}

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x06000664 RID: 1636 RVA: 0x0001B134 File Offset: 0x00019334
		public BaseRaycaster rootRaycaster
		{
			get
			{
				if (this.m_RootRaycaster == null)
				{
					BaseRaycaster[] componentsInParent = base.GetComponentsInParent<BaseRaycaster>();
					if (componentsInParent.Length != 0)
					{
						this.m_RootRaycaster = componentsInParent[componentsInParent.Length - 1];
					}
				}
				return this.m_RootRaycaster;
			}
		}

		// Token: 0x06000665 RID: 1637 RVA: 0x0001B170 File Offset: 0x00019370
		public override string ToString()
		{
			string[] array = new string[8];
			array[0] = "Name: ";
			int num = 1;
			GameObject gameObject = base.gameObject;
			array[num] = ((gameObject != null) ? gameObject.ToString() : null);
			array[2] = "\neventCamera: ";
			int num2 = 3;
			Camera eventCamera = this.eventCamera;
			array[num2] = ((eventCamera != null) ? eventCamera.ToString() : null);
			array[4] = "\nsortOrderPriority: ";
			array[5] = this.sortOrderPriority.ToString();
			array[6] = "\nrenderOrderPriority: ";
			array[7] = this.renderOrderPriority.ToString();
			return string.Concat(array);
		}

		// Token: 0x06000666 RID: 1638 RVA: 0x0001B1F4 File Offset: 0x000193F4
		protected override void OnEnable()
		{
			base.OnEnable();
			RaycasterManager.AddRaycaster(this);
		}

		// Token: 0x06000667 RID: 1639 RVA: 0x0001B202 File Offset: 0x00019402
		protected override void OnDisable()
		{
			RaycasterManager.RemoveRaycasters(this);
			base.OnDisable();
		}

		// Token: 0x06000668 RID: 1640 RVA: 0x0001B210 File Offset: 0x00019410
		protected override void OnCanvasHierarchyChanged()
		{
			base.OnCanvasHierarchyChanged();
			this.m_RootRaycaster = null;
		}

		// Token: 0x06000669 RID: 1641 RVA: 0x0001B21F File Offset: 0x0001941F
		protected override void OnTransformParentChanged()
		{
			base.OnTransformParentChanged();
			this.m_RootRaycaster = null;
		}

		// Token: 0x0600066A RID: 1642 RVA: 0x0001B22E File Offset: 0x0001942E
		protected BaseRaycaster()
		{
		}

		// Token: 0x04000229 RID: 553
		private BaseRaycaster m_RootRaycaster;
	}
}
