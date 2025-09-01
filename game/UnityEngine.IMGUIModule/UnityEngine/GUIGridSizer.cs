using System;

namespace UnityEngine
{
	// Token: 0x02000036 RID: 54
	internal sealed class GUIGridSizer : GUILayoutEntry
	{
		// Token: 0x060003EF RID: 1007 RVA: 0x0000D064 File Offset: 0x0000B264
		public static Rect GetRect(GUIContent[] contents, int xCount, GUIStyle style, GUILayoutOption[] options)
		{
			Rect rect = new Rect(0f, 0f, 0f, 0f);
			EventType type = Event.current.type;
			EventType eventType = type;
			if (eventType != EventType.Layout)
			{
				if (eventType == EventType.Used)
				{
					return GUILayoutEntry.kDummyRect;
				}
				rect = GUILayoutUtility.current.topLevel.GetNext().rect;
			}
			else
			{
				GUILayoutUtility.current.topLevel.Add(new GUIGridSizer(contents, xCount, style, options));
			}
			return rect;
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x0000D0E8 File Offset: 0x0000B2E8
		private GUIGridSizer(GUIContent[] contents, int xCount, GUIStyle buttonStyle, GUILayoutOption[] options) : base(0f, 0f, 0f, 0f, GUIStyle.none)
		{
			this.m_Count = contents.Length;
			this.m_XCount = xCount;
			this.ApplyStyleSettings(buttonStyle);
			this.ApplyOptions(options);
			bool flag = xCount == 0 || contents.Length == 0;
			if (!flag)
			{
				float num = (float)(Mathf.Max(buttonStyle.margin.left, buttonStyle.margin.right) * (this.m_XCount - 1));
				float num2 = (float)(Mathf.Max(buttonStyle.margin.top, buttonStyle.margin.bottom) * (this.rows - 1));
				bool flag2 = buttonStyle.fixedWidth != 0f;
				if (flag2)
				{
					this.m_MinButtonWidth = (this.m_MaxButtonWidth = buttonStyle.fixedWidth);
				}
				bool flag3 = buttonStyle.fixedHeight != 0f;
				if (flag3)
				{
					this.m_MinButtonHeight = (this.m_MaxButtonHeight = buttonStyle.fixedHeight);
				}
				bool flag4 = this.m_MinButtonWidth == -1f;
				if (flag4)
				{
					bool flag5 = this.minWidth != 0f;
					if (flag5)
					{
						this.m_MinButtonWidth = (this.minWidth - num) / (float)this.m_XCount;
					}
					bool flag6 = this.maxWidth != 0f;
					if (flag6)
					{
						this.m_MaxButtonWidth = (this.maxWidth - num) / (float)this.m_XCount;
					}
				}
				bool flag7 = this.m_MinButtonHeight == -1f;
				if (flag7)
				{
					bool flag8 = this.minHeight != 0f;
					if (flag8)
					{
						this.m_MinButtonHeight = (this.minHeight - num2) / (float)this.rows;
					}
					bool flag9 = this.maxHeight != 0f;
					if (flag9)
					{
						this.m_MaxButtonHeight = (this.maxHeight - num2) / (float)this.rows;
					}
				}
				bool flag10 = this.m_MinButtonHeight == -1f || this.m_MaxButtonHeight == -1f || this.m_MinButtonWidth == -1f || this.m_MaxButtonWidth == -1f;
				if (flag10)
				{
					float num3 = 0f;
					float num4 = 0f;
					foreach (GUIContent content in contents)
					{
						Vector2 vector = buttonStyle.CalcSize(content);
						num4 = Mathf.Max(num4, vector.x);
						num3 = Mathf.Max(num3, vector.y);
					}
					bool flag11 = this.m_MinButtonWidth == -1f;
					if (flag11)
					{
						bool flag12 = this.m_MaxButtonWidth != -1f;
						if (flag12)
						{
							this.m_MinButtonWidth = Mathf.Min(num4, this.m_MaxButtonWidth);
						}
						else
						{
							this.m_MinButtonWidth = num4;
						}
					}
					bool flag13 = this.m_MaxButtonWidth == -1f;
					if (flag13)
					{
						bool flag14 = this.m_MinButtonWidth != -1f;
						if (flag14)
						{
							this.m_MaxButtonWidth = Mathf.Max(num4, this.m_MinButtonWidth);
						}
						else
						{
							this.m_MaxButtonWidth = num4;
						}
					}
					bool flag15 = this.m_MinButtonHeight == -1f;
					if (flag15)
					{
						bool flag16 = this.m_MaxButtonHeight != -1f;
						if (flag16)
						{
							this.m_MinButtonHeight = Mathf.Min(num3, this.m_MaxButtonHeight);
						}
						else
						{
							this.m_MinButtonHeight = num3;
						}
					}
					bool flag17 = this.m_MaxButtonHeight == -1f;
					if (flag17)
					{
						bool flag18 = this.m_MinButtonHeight != -1f;
						if (flag18)
						{
							this.maxHeight = Mathf.Max(this.maxHeight, this.m_MinButtonHeight);
						}
						this.m_MaxButtonHeight = this.maxHeight;
					}
				}
				this.minWidth = this.m_MinButtonWidth * (float)this.m_XCount + num;
				this.maxWidth = this.m_MaxButtonWidth * (float)this.m_XCount + num;
				this.minHeight = this.m_MinButtonHeight * (float)this.rows + num2;
				this.maxHeight = this.m_MaxButtonHeight * (float)this.rows + num2;
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060003F1 RID: 1009 RVA: 0x0000D510 File Offset: 0x0000B710
		private int rows
		{
			get
			{
				int num = this.m_Count / this.m_XCount;
				bool flag = this.m_Count % this.m_XCount != 0;
				if (flag)
				{
					num++;
				}
				return num;
			}
		}

		// Token: 0x040000FB RID: 251
		private readonly int m_Count;

		// Token: 0x040000FC RID: 252
		private readonly int m_XCount;

		// Token: 0x040000FD RID: 253
		private readonly float m_MinButtonWidth = -1f;

		// Token: 0x040000FE RID: 254
		private readonly float m_MaxButtonWidth = -1f;

		// Token: 0x040000FF RID: 255
		private readonly float m_MinButtonHeight = -1f;

		// Token: 0x04000100 RID: 256
		private readonly float m_MaxButtonHeight = -1f;
	}
}
