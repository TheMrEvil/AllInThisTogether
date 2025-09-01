using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000027 RID: 39
	[NativeHeader("Modules/IMGUI/GUISkin.bindings.h")]
	[Serializable]
	public sealed class GUISettings
	{
		// Token: 0x06000285 RID: 645
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float Internal_GetCursorFlashSpeed();

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000286 RID: 646 RVA: 0x0000A660 File Offset: 0x00008860
		// (set) Token: 0x06000287 RID: 647 RVA: 0x0000A678 File Offset: 0x00008878
		public bool doubleClickSelectsWord
		{
			get
			{
				return this.m_DoubleClickSelectsWord;
			}
			set
			{
				this.m_DoubleClickSelectsWord = value;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000288 RID: 648 RVA: 0x0000A684 File Offset: 0x00008884
		// (set) Token: 0x06000289 RID: 649 RVA: 0x0000A69C File Offset: 0x0000889C
		public bool tripleClickSelectsLine
		{
			get
			{
				return this.m_TripleClickSelectsLine;
			}
			set
			{
				this.m_TripleClickSelectsLine = value;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x0600028A RID: 650 RVA: 0x0000A6A8 File Offset: 0x000088A8
		// (set) Token: 0x0600028B RID: 651 RVA: 0x0000A6C0 File Offset: 0x000088C0
		public Color cursorColor
		{
			get
			{
				return this.m_CursorColor;
			}
			set
			{
				this.m_CursorColor = value;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x0600028C RID: 652 RVA: 0x0000A6CC File Offset: 0x000088CC
		// (set) Token: 0x0600028D RID: 653 RVA: 0x0000A701 File Offset: 0x00008901
		public float cursorFlashSpeed
		{
			get
			{
				bool flag = this.m_CursorFlashSpeed >= 0f;
				float result;
				if (flag)
				{
					result = this.m_CursorFlashSpeed;
				}
				else
				{
					result = GUISettings.Internal_GetCursorFlashSpeed();
				}
				return result;
			}
			set
			{
				this.m_CursorFlashSpeed = value;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x0600028E RID: 654 RVA: 0x0000A70C File Offset: 0x0000890C
		// (set) Token: 0x0600028F RID: 655 RVA: 0x0000A724 File Offset: 0x00008924
		public Color selectionColor
		{
			get
			{
				return this.m_SelectionColor;
			}
			set
			{
				this.m_SelectionColor = value;
			}
		}

		// Token: 0x06000290 RID: 656 RVA: 0x0000A730 File Offset: 0x00008930
		public GUISettings()
		{
		}

		// Token: 0x040000A1 RID: 161
		[SerializeField]
		private bool m_DoubleClickSelectsWord = true;

		// Token: 0x040000A2 RID: 162
		[SerializeField]
		private bool m_TripleClickSelectsLine = true;

		// Token: 0x040000A3 RID: 163
		[SerializeField]
		private Color m_CursorColor = Color.white;

		// Token: 0x040000A4 RID: 164
		[SerializeField]
		private float m_CursorFlashSpeed = -1f;

		// Token: 0x040000A5 RID: 165
		[SerializeField]
		private Color m_SelectionColor = new Color(0.5f, 0.5f, 1f);
	}
}
