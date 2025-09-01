using System;

namespace UnityEngine.EventSystems
{
	// Token: 0x02000073 RID: 115
	public abstract class UIBehaviour : MonoBehaviour
	{
		// Token: 0x0600067C RID: 1660 RVA: 0x0001BA4F File Offset: 0x00019C4F
		protected virtual void Awake()
		{
		}

		// Token: 0x0600067D RID: 1661 RVA: 0x0001BA51 File Offset: 0x00019C51
		protected virtual void OnEnable()
		{
		}

		// Token: 0x0600067E RID: 1662 RVA: 0x0001BA53 File Offset: 0x00019C53
		protected virtual void Start()
		{
		}

		// Token: 0x0600067F RID: 1663 RVA: 0x0001BA55 File Offset: 0x00019C55
		protected virtual void OnDisable()
		{
		}

		// Token: 0x06000680 RID: 1664 RVA: 0x0001BA57 File Offset: 0x00019C57
		protected virtual void OnDestroy()
		{
		}

		// Token: 0x06000681 RID: 1665 RVA: 0x0001BA59 File Offset: 0x00019C59
		public virtual bool IsActive()
		{
			return base.isActiveAndEnabled;
		}

		// Token: 0x06000682 RID: 1666 RVA: 0x0001BA61 File Offset: 0x00019C61
		protected virtual void OnRectTransformDimensionsChange()
		{
		}

		// Token: 0x06000683 RID: 1667 RVA: 0x0001BA63 File Offset: 0x00019C63
		protected virtual void OnBeforeTransformParentChanged()
		{
		}

		// Token: 0x06000684 RID: 1668 RVA: 0x0001BA65 File Offset: 0x00019C65
		protected virtual void OnTransformParentChanged()
		{
		}

		// Token: 0x06000685 RID: 1669 RVA: 0x0001BA67 File Offset: 0x00019C67
		protected virtual void OnDidApplyAnimationProperties()
		{
		}

		// Token: 0x06000686 RID: 1670 RVA: 0x0001BA69 File Offset: 0x00019C69
		protected virtual void OnCanvasGroupChanged()
		{
		}

		// Token: 0x06000687 RID: 1671 RVA: 0x0001BA6B File Offset: 0x00019C6B
		protected virtual void OnCanvasHierarchyChanged()
		{
		}

		// Token: 0x06000688 RID: 1672 RVA: 0x0001BA6D File Offset: 0x00019C6D
		public bool IsDestroyed()
		{
			return this == null;
		}

		// Token: 0x06000689 RID: 1673 RVA: 0x0001BA76 File Offset: 0x00019C76
		protected UIBehaviour()
		{
		}
	}
}
