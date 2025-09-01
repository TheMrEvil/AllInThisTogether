using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace UnityEngine.SocialPlatforms.Impl
{
	// Token: 0x02000015 RID: 21
	public class AchievementDescription : IAchievementDescription
	{
		// Token: 0x0600009C RID: 156 RVA: 0x000031C0 File Offset: 0x000013C0
		public AchievementDescription(string id, string title, Texture2D image, string achievedDescription, string unachievedDescription, bool hidden, int points)
		{
			this.id = id;
			this.m_Title = title;
			this.m_Image = image;
			this.m_AchievedDescription = achievedDescription;
			this.m_UnachievedDescription = unachievedDescription;
			this.m_Hidden = hidden;
			this.m_Points = points;
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00003200 File Offset: 0x00001400
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				this.id,
				" - ",
				this.title,
				" - ",
				this.achievedDescription,
				" - ",
				this.unachievedDescription,
				" - ",
				this.points.ToString(),
				" - ",
				this.hidden.ToString()
			});
		}

		// Token: 0x0600009E RID: 158 RVA: 0x0000328E File Offset: 0x0000148E
		public void SetImage(Texture2D image)
		{
			this.m_Image = image;
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x0600009F RID: 159 RVA: 0x00003298 File Offset: 0x00001498
		// (set) Token: 0x060000A0 RID: 160 RVA: 0x000032A0 File Offset: 0x000014A0
		public string id
		{
			[CompilerGenerated]
			get
			{
				return this.<id>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<id>k__BackingField = value;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x000032AC File Offset: 0x000014AC
		public string title
		{
			get
			{
				return this.m_Title;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x000032C4 File Offset: 0x000014C4
		public Texture2D image
		{
			get
			{
				return this.m_Image;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x000032DC File Offset: 0x000014DC
		public string achievedDescription
		{
			get
			{
				return this.m_AchievedDescription;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000A4 RID: 164 RVA: 0x000032F4 File Offset: 0x000014F4
		public string unachievedDescription
		{
			get
			{
				return this.m_UnachievedDescription;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x0000330C File Offset: 0x0000150C
		public bool hidden
		{
			get
			{
				return this.m_Hidden;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x00003324 File Offset: 0x00001524
		public int points
		{
			get
			{
				return this.m_Points;
			}
		}

		// Token: 0x0400002B RID: 43
		private string m_Title;

		// Token: 0x0400002C RID: 44
		private Texture2D m_Image;

		// Token: 0x0400002D RID: 45
		private string m_AchievedDescription;

		// Token: 0x0400002E RID: 46
		private string m_UnachievedDescription;

		// Token: 0x0400002F RID: 47
		private bool m_Hidden;

		// Token: 0x04000030 RID: 48
		private int m_Points;

		// Token: 0x04000031 RID: 49
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string <id>k__BackingField;
	}
}
