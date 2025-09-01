using System;
using System.Collections.Generic;

namespace UnityEngine.TextCore.Text
{
	// Token: 0x02000044 RID: 68
	[Serializable]
	public class UnicodeLineBreakingRules
	{
		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060001CC RID: 460 RVA: 0x0001CE46 File Offset: 0x0001B046
		public TextAsset lineBreakingRules
		{
			get
			{
				return this.m_UnicodeLineBreakingRules;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060001CD RID: 461 RVA: 0x0001CE4E File Offset: 0x0001B04E
		public TextAsset leadingCharacters
		{
			get
			{
				return this.m_LeadingCharacters;
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060001CE RID: 462 RVA: 0x0001CE56 File Offset: 0x0001B056
		public TextAsset followingCharacters
		{
			get
			{
				return this.m_FollowingCharacters;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060001CF RID: 463 RVA: 0x0001CE60 File Offset: 0x0001B060
		// (set) Token: 0x060001D0 RID: 464 RVA: 0x0001CE89 File Offset: 0x0001B089
		internal HashSet<uint> leadingCharactersLookup
		{
			get
			{
				bool flag = UnicodeLineBreakingRules.s_LeadingCharactersLookup == null;
				if (flag)
				{
					UnicodeLineBreakingRules.LoadLineBreakingRules();
				}
				return UnicodeLineBreakingRules.s_LeadingCharactersLookup;
			}
			set
			{
				UnicodeLineBreakingRules.s_LeadingCharactersLookup = value;
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060001D1 RID: 465 RVA: 0x0001CE94 File Offset: 0x0001B094
		// (set) Token: 0x060001D2 RID: 466 RVA: 0x0001CEBD File Offset: 0x0001B0BD
		internal HashSet<uint> followingCharactersLookup
		{
			get
			{
				bool flag = UnicodeLineBreakingRules.s_LeadingCharactersLookup == null;
				if (flag)
				{
					UnicodeLineBreakingRules.LoadLineBreakingRules();
				}
				return UnicodeLineBreakingRules.s_FollowingCharactersLookup;
			}
			set
			{
				UnicodeLineBreakingRules.s_FollowingCharactersLookup = value;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060001D3 RID: 467 RVA: 0x0001CEC5 File Offset: 0x0001B0C5
		// (set) Token: 0x060001D4 RID: 468 RVA: 0x0001CECD File Offset: 0x0001B0CD
		public bool useModernHangulLineBreakingRules
		{
			get
			{
				return this.m_UseModernHangulLineBreakingRules;
			}
			set
			{
				this.m_UseModernHangulLineBreakingRules = value;
			}
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x0001CED8 File Offset: 0x0001B0D8
		internal static void LoadLineBreakingRules()
		{
			bool flag = UnicodeLineBreakingRules.s_LeadingCharactersLookup == null;
			if (flag)
			{
				bool flag2 = UnicodeLineBreakingRules.s_Instance.m_LeadingCharacters == null;
				if (flag2)
				{
					UnicodeLineBreakingRules.s_Instance.m_LeadingCharacters = Resources.Load<TextAsset>("LineBreaking Leading Characters");
				}
				UnicodeLineBreakingRules.s_LeadingCharactersLookup = ((UnicodeLineBreakingRules.s_Instance.m_LeadingCharacters != null) ? UnicodeLineBreakingRules.GetCharacters(UnicodeLineBreakingRules.s_Instance.m_LeadingCharacters) : new HashSet<uint>());
				bool flag3 = UnicodeLineBreakingRules.s_Instance.m_FollowingCharacters == null;
				if (flag3)
				{
					UnicodeLineBreakingRules.s_Instance.m_FollowingCharacters = Resources.Load<TextAsset>("LineBreaking Following Characters");
				}
				UnicodeLineBreakingRules.s_FollowingCharactersLookup = ((UnicodeLineBreakingRules.s_Instance.m_FollowingCharacters != null) ? UnicodeLineBreakingRules.GetCharacters(UnicodeLineBreakingRules.s_Instance.m_FollowingCharacters) : new HashSet<uint>());
			}
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x0001CFA4 File Offset: 0x0001B1A4
		internal static void LoadLineBreakingRules(TextAsset leadingRules, TextAsset followingRules)
		{
			bool flag = UnicodeLineBreakingRules.s_LeadingCharactersLookup == null;
			if (flag)
			{
				bool flag2 = leadingRules == null;
				if (flag2)
				{
					leadingRules = Resources.Load<TextAsset>("LineBreaking Leading Characters");
				}
				UnicodeLineBreakingRules.s_LeadingCharactersLookup = ((leadingRules != null) ? UnicodeLineBreakingRules.GetCharacters(leadingRules) : new HashSet<uint>());
				bool flag3 = followingRules == null;
				if (flag3)
				{
					followingRules = Resources.Load<TextAsset>("LineBreaking Following Characters");
				}
				UnicodeLineBreakingRules.s_FollowingCharactersLookup = ((followingRules != null) ? UnicodeLineBreakingRules.GetCharacters(followingRules) : new HashSet<uint>());
			}
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x0001D024 File Offset: 0x0001B224
		private static HashSet<uint> GetCharacters(TextAsset file)
		{
			HashSet<uint> hashSet = new HashSet<uint>();
			string text = file.text;
			for (int i = 0; i < text.Length; i++)
			{
				hashSet.Add((uint)text[i]);
			}
			return hashSet;
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x000059A4 File Offset: 0x00003BA4
		public UnicodeLineBreakingRules()
		{
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x0001D069 File Offset: 0x0001B269
		// Note: this type is marked as 'beforefieldinit'.
		static UnicodeLineBreakingRules()
		{
		}

		// Token: 0x0400038D RID: 909
		private static UnicodeLineBreakingRules s_Instance = new UnicodeLineBreakingRules();

		// Token: 0x0400038E RID: 910
		[SerializeField]
		private TextAsset m_UnicodeLineBreakingRules;

		// Token: 0x0400038F RID: 911
		[SerializeField]
		private TextAsset m_LeadingCharacters;

		// Token: 0x04000390 RID: 912
		[SerializeField]
		private TextAsset m_FollowingCharacters;

		// Token: 0x04000391 RID: 913
		[SerializeField]
		private bool m_UseModernHangulLineBreakingRules;

		// Token: 0x04000392 RID: 914
		private static HashSet<uint> s_LeadingCharactersLookup;

		// Token: 0x04000393 RID: 915
		private static HashSet<uint> s_FollowingCharactersLookup;
	}
}
