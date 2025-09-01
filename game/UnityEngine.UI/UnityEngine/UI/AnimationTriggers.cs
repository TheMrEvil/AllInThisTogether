using System;
using UnityEngine.Serialization;

namespace UnityEngine.UI
{
	// Token: 0x02000002 RID: 2
	[Serializable]
	public class AnimationTriggers
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		// (set) Token: 0x06000002 RID: 2 RVA: 0x00002058 File Offset: 0x00000258
		public string normalTrigger
		{
			get
			{
				return this.m_NormalTrigger;
			}
			set
			{
				this.m_NormalTrigger = value;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000003 RID: 3 RVA: 0x00002061 File Offset: 0x00000261
		// (set) Token: 0x06000004 RID: 4 RVA: 0x00002069 File Offset: 0x00000269
		public string highlightedTrigger
		{
			get
			{
				return this.m_HighlightedTrigger;
			}
			set
			{
				this.m_HighlightedTrigger = value;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000005 RID: 5 RVA: 0x00002072 File Offset: 0x00000272
		// (set) Token: 0x06000006 RID: 6 RVA: 0x0000207A File Offset: 0x0000027A
		public string pressedTrigger
		{
			get
			{
				return this.m_PressedTrigger;
			}
			set
			{
				this.m_PressedTrigger = value;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000007 RID: 7 RVA: 0x00002083 File Offset: 0x00000283
		// (set) Token: 0x06000008 RID: 8 RVA: 0x0000208B File Offset: 0x0000028B
		public string selectedTrigger
		{
			get
			{
				return this.m_SelectedTrigger;
			}
			set
			{
				this.m_SelectedTrigger = value;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000009 RID: 9 RVA: 0x00002094 File Offset: 0x00000294
		// (set) Token: 0x0600000A RID: 10 RVA: 0x0000209C File Offset: 0x0000029C
		public string disabledTrigger
		{
			get
			{
				return this.m_DisabledTrigger;
			}
			set
			{
				this.m_DisabledTrigger = value;
			}
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000020A5 File Offset: 0x000002A5
		public AnimationTriggers()
		{
		}

		// Token: 0x04000001 RID: 1
		private const string kDefaultNormalAnimName = "Normal";

		// Token: 0x04000002 RID: 2
		private const string kDefaultHighlightedAnimName = "Highlighted";

		// Token: 0x04000003 RID: 3
		private const string kDefaultPressedAnimName = "Pressed";

		// Token: 0x04000004 RID: 4
		private const string kDefaultSelectedAnimName = "Selected";

		// Token: 0x04000005 RID: 5
		private const string kDefaultDisabledAnimName = "Disabled";

		// Token: 0x04000006 RID: 6
		[FormerlySerializedAs("normalTrigger")]
		[SerializeField]
		private string m_NormalTrigger = "Normal";

		// Token: 0x04000007 RID: 7
		[FormerlySerializedAs("highlightedTrigger")]
		[SerializeField]
		private string m_HighlightedTrigger = "Highlighted";

		// Token: 0x04000008 RID: 8
		[FormerlySerializedAs("pressedTrigger")]
		[SerializeField]
		private string m_PressedTrigger = "Pressed";

		// Token: 0x04000009 RID: 9
		[FormerlySerializedAs("m_HighlightedTrigger")]
		[SerializeField]
		private string m_SelectedTrigger = "Selected";

		// Token: 0x0400000A RID: 10
		[FormerlySerializedAs("disabledTrigger")]
		[SerializeField]
		private string m_DisabledTrigger = "Disabled";
	}
}
