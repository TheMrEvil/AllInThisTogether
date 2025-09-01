using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020002A9 RID: 681
	[Serializable]
	internal struct StyleSelectorPart
	{
		// Token: 0x170005AA RID: 1450
		// (get) Token: 0x06001744 RID: 5956 RVA: 0x000617E4 File Offset: 0x0005F9E4
		// (set) Token: 0x06001745 RID: 5957 RVA: 0x000617FC File Offset: 0x0005F9FC
		public string value
		{
			get
			{
				return this.m_Value;
			}
			internal set
			{
				this.m_Value = value;
			}
		}

		// Token: 0x170005AB RID: 1451
		// (get) Token: 0x06001746 RID: 5958 RVA: 0x00061808 File Offset: 0x0005FA08
		// (set) Token: 0x06001747 RID: 5959 RVA: 0x00061820 File Offset: 0x0005FA20
		public StyleSelectorType type
		{
			get
			{
				return this.m_Type;
			}
			internal set
			{
				this.m_Type = value;
			}
		}

		// Token: 0x06001748 RID: 5960 RVA: 0x0006182C File Offset: 0x0005FA2C
		public override string ToString()
		{
			return UnityString.Format("[StyleSelectorPart: value={0}, type={1}]", new object[]
			{
				this.value,
				this.type
			});
		}

		// Token: 0x06001749 RID: 5961 RVA: 0x00061868 File Offset: 0x0005FA68
		public static StyleSelectorPart CreateClass(string className)
		{
			return new StyleSelectorPart
			{
				m_Type = StyleSelectorType.Class,
				m_Value = className
			};
		}

		// Token: 0x0600174A RID: 5962 RVA: 0x00061894 File Offset: 0x0005FA94
		public static StyleSelectorPart CreatePseudoClass(string className)
		{
			return new StyleSelectorPart
			{
				m_Type = StyleSelectorType.PseudoClass,
				m_Value = className
			};
		}

		// Token: 0x0600174B RID: 5963 RVA: 0x000618C0 File Offset: 0x0005FAC0
		public static StyleSelectorPart CreateId(string Id)
		{
			return new StyleSelectorPart
			{
				m_Type = StyleSelectorType.ID,
				m_Value = Id
			};
		}

		// Token: 0x0600174C RID: 5964 RVA: 0x000618EC File Offset: 0x0005FAEC
		public static StyleSelectorPart CreateType(Type t)
		{
			return new StyleSelectorPart
			{
				m_Type = StyleSelectorType.Type,
				m_Value = t.Name
			};
		}

		// Token: 0x0600174D RID: 5965 RVA: 0x0006191C File Offset: 0x0005FB1C
		public static StyleSelectorPart CreateType(string typeName)
		{
			return new StyleSelectorPart
			{
				m_Type = StyleSelectorType.Type,
				m_Value = typeName
			};
		}

		// Token: 0x0600174E RID: 5966 RVA: 0x00061948 File Offset: 0x0005FB48
		public static StyleSelectorPart CreatePredicate(object predicate)
		{
			return new StyleSelectorPart
			{
				m_Type = StyleSelectorType.Predicate,
				tempData = predicate
			};
		}

		// Token: 0x0600174F RID: 5967 RVA: 0x00061974 File Offset: 0x0005FB74
		public static StyleSelectorPart CreateWildCard()
		{
			return new StyleSelectorPart
			{
				m_Type = StyleSelectorType.Wildcard
			};
		}

		// Token: 0x040009DC RID: 2524
		[SerializeField]
		private string m_Value;

		// Token: 0x040009DD RID: 2525
		[SerializeField]
		private StyleSelectorType m_Type;

		// Token: 0x040009DE RID: 2526
		internal object tempData;
	}
}
