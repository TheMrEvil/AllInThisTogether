using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x020002EE RID: 750
	public class UxmlFactory<TCreatedType, TTraits> : IUxmlFactory, IUxmlFactoryInternal where TCreatedType : VisualElement, new() where TTraits : UxmlTraits, new()
	{
		// Token: 0x060018E1 RID: 6369 RVA: 0x00065AEC File Offset: 0x00063CEC
		protected UxmlFactory()
		{
			this.m_Traits = Activator.CreateInstance<TTraits>();
		}

		// Token: 0x1700060E RID: 1550
		// (get) Token: 0x060018E2 RID: 6370 RVA: 0x00065B04 File Offset: 0x00063D04
		public virtual string uxmlName
		{
			get
			{
				return typeof(TCreatedType).Name;
			}
		}

		// Token: 0x1700060F RID: 1551
		// (get) Token: 0x060018E3 RID: 6371 RVA: 0x00065B28 File Offset: 0x00063D28
		public virtual string uxmlNamespace
		{
			get
			{
				return typeof(TCreatedType).Namespace ?? string.Empty;
			}
		}

		// Token: 0x17000610 RID: 1552
		// (get) Token: 0x060018E4 RID: 6372 RVA: 0x00065B54 File Offset: 0x00063D54
		public virtual string uxmlQualifiedName
		{
			get
			{
				return typeof(TCreatedType).FullName;
			}
		}

		// Token: 0x17000611 RID: 1553
		// (get) Token: 0x060018E5 RID: 6373 RVA: 0x00065B78 File Offset: 0x00063D78
		public bool canHaveAnyAttribute
		{
			get
			{
				return this.m_Traits.canHaveAnyAttribute;
			}
		}

		// Token: 0x17000612 RID: 1554
		// (get) Token: 0x060018E6 RID: 6374 RVA: 0x00065B9C File Offset: 0x00063D9C
		public virtual IEnumerable<UxmlAttributeDescription> uxmlAttributesDescription
		{
			get
			{
				return this.m_Traits.uxmlAttributesDescription;
			}
		}

		// Token: 0x17000613 RID: 1555
		// (get) Token: 0x060018E7 RID: 6375 RVA: 0x00065BC0 File Offset: 0x00063DC0
		public virtual IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription
		{
			get
			{
				return this.m_Traits.uxmlChildElementsDescription;
			}
		}

		// Token: 0x17000614 RID: 1556
		// (get) Token: 0x060018E8 RID: 6376 RVA: 0x00065BE4 File Offset: 0x00063DE4
		public virtual string substituteForTypeName
		{
			get
			{
				bool flag = typeof(TCreatedType) == typeof(VisualElement);
				string result;
				if (flag)
				{
					result = string.Empty;
				}
				else
				{
					result = typeof(VisualElement).Name;
				}
				return result;
			}
		}

		// Token: 0x17000615 RID: 1557
		// (get) Token: 0x060018E9 RID: 6377 RVA: 0x00065C28 File Offset: 0x00063E28
		public virtual string substituteForTypeNamespace
		{
			get
			{
				bool flag = typeof(TCreatedType) == typeof(VisualElement);
				string result;
				if (flag)
				{
					result = string.Empty;
				}
				else
				{
					result = (typeof(VisualElement).Namespace ?? string.Empty);
				}
				return result;
			}
		}

		// Token: 0x17000616 RID: 1558
		// (get) Token: 0x060018EA RID: 6378 RVA: 0x00065C78 File Offset: 0x00063E78
		public virtual string substituteForTypeQualifiedName
		{
			get
			{
				bool flag = typeof(TCreatedType) == typeof(VisualElement);
				string result;
				if (flag)
				{
					result = string.Empty;
				}
				else
				{
					result = typeof(VisualElement).FullName;
				}
				return result;
			}
		}

		// Token: 0x060018EB RID: 6379 RVA: 0x00065CC0 File Offset: 0x00063EC0
		public virtual bool AcceptsAttributeBag(IUxmlAttributes bag, CreationContext cc)
		{
			return true;
		}

		// Token: 0x060018EC RID: 6380 RVA: 0x00065CD4 File Offset: 0x00063ED4
		public virtual VisualElement Create(IUxmlAttributes bag, CreationContext cc)
		{
			TCreatedType tcreatedType = Activator.CreateInstance<TCreatedType>();
			this.m_Traits.Init(tcreatedType, bag, cc);
			return tcreatedType;
		}

		// Token: 0x17000617 RID: 1559
		// (get) Token: 0x060018ED RID: 6381 RVA: 0x00065D0B File Offset: 0x00063F0B
		Type IUxmlFactoryInternal.uxmlType
		{
			get
			{
				return typeof(TCreatedType);
			}
		}

		// Token: 0x04000AB2 RID: 2738
		internal TTraits m_Traits;
	}
}
