using System;
using UnityEngine.Serialization;

namespace UnityEngine.UI
{
	// Token: 0x02000037 RID: 55
	[Serializable]
	public struct SpriteState : IEquatable<SpriteState>
	{
		// Token: 0x1700011F RID: 287
		// (get) Token: 0x06000427 RID: 1063 RVA: 0x00014806 File Offset: 0x00012A06
		// (set) Token: 0x06000428 RID: 1064 RVA: 0x0001480E File Offset: 0x00012A0E
		public Sprite highlightedSprite
		{
			get
			{
				return this.m_HighlightedSprite;
			}
			set
			{
				this.m_HighlightedSprite = value;
			}
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x06000429 RID: 1065 RVA: 0x00014817 File Offset: 0x00012A17
		// (set) Token: 0x0600042A RID: 1066 RVA: 0x0001481F File Offset: 0x00012A1F
		public Sprite pressedSprite
		{
			get
			{
				return this.m_PressedSprite;
			}
			set
			{
				this.m_PressedSprite = value;
			}
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x0600042B RID: 1067 RVA: 0x00014828 File Offset: 0x00012A28
		// (set) Token: 0x0600042C RID: 1068 RVA: 0x00014830 File Offset: 0x00012A30
		public Sprite selectedSprite
		{
			get
			{
				return this.m_SelectedSprite;
			}
			set
			{
				this.m_SelectedSprite = value;
			}
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x0600042D RID: 1069 RVA: 0x00014839 File Offset: 0x00012A39
		// (set) Token: 0x0600042E RID: 1070 RVA: 0x00014841 File Offset: 0x00012A41
		public Sprite disabledSprite
		{
			get
			{
				return this.m_DisabledSprite;
			}
			set
			{
				this.m_DisabledSprite = value;
			}
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x0001484C File Offset: 0x00012A4C
		public bool Equals(SpriteState other)
		{
			return this.highlightedSprite == other.highlightedSprite && this.pressedSprite == other.pressedSprite && this.selectedSprite == other.selectedSprite && this.disabledSprite == other.disabledSprite;
		}

		// Token: 0x0400016A RID: 362
		[SerializeField]
		private Sprite m_HighlightedSprite;

		// Token: 0x0400016B RID: 363
		[SerializeField]
		private Sprite m_PressedSprite;

		// Token: 0x0400016C RID: 364
		[FormerlySerializedAs("m_HighlightedSprite")]
		[SerializeField]
		private Sprite m_SelectedSprite;

		// Token: 0x0400016D RID: 365
		[SerializeField]
		private Sprite m_DisabledSprite;
	}
}
