using System;
using Febucci.UI.Actions;
using Febucci.UI.Effects;
using UnityEngine;

namespace Febucci.UI
{
	// Token: 0x02000008 RID: 8
	[CreateAssetMenu(fileName = "Text Animator Settings", menuName = "Text Animator/Settings", order = 100)]
	[Serializable]
	public sealed class TextAnimatorSettings : ScriptableObject
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000013 RID: 19 RVA: 0x000023D9 File Offset: 0x000005D9
		public static TextAnimatorSettings Instance
		{
			get
			{
				if (TextAnimatorSettings.instance)
				{
					return TextAnimatorSettings.instance;
				}
				TextAnimatorSettings.LoadSettings();
				return TextAnimatorSettings.instance;
			}
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000023F7 File Offset: 0x000005F7
		public static void LoadSettings()
		{
			if (TextAnimatorSettings.instance)
			{
				return;
			}
			TextAnimatorSettings.instance = Resources.Load<TextAnimatorSettings>("TextAnimatorSettings");
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002415 File Offset: 0x00000615
		public static void UnloadSettings()
		{
			if (!TextAnimatorSettings.instance)
			{
				return;
			}
			Resources.UnloadAsset(TextAnimatorSettings.instance);
			TextAnimatorSettings.instance = null;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002434 File Offset: 0x00000634
		public static void SetAllEffectsActive(bool enabled)
		{
			TextAnimatorSettings.SetAppearancesActive(enabled);
			TextAnimatorSettings.SetBehaviorsActive(enabled);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002442 File Offset: 0x00000642
		public static void SetAppearancesActive(bool enabled)
		{
			if (TextAnimatorSettings.Instance)
			{
				TextAnimatorSettings.Instance.appearances.enabled = enabled;
			}
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002460 File Offset: 0x00000660
		public static void SetBehaviorsActive(bool enabled)
		{
			if (TextAnimatorSettings.Instance)
			{
				TextAnimatorSettings.Instance.behaviors.enabled = enabled;
			}
		}

		// Token: 0x06000019 RID: 25 RVA: 0x0000247E File Offset: 0x0000067E
		public TextAnimatorSettings()
		{
		}

		// Token: 0x04000018 RID: 24
		public const string expectedName = "TextAnimatorSettings";

		// Token: 0x04000019 RID: 25
		private static TextAnimatorSettings instance;

		// Token: 0x0400001A RID: 26
		[Header("Default info")]
		public TextAnimatorSettings.Category<AnimationsDatabase> behaviors = new TextAnimatorSettings.Category<AnimationsDatabase>('<', '>');

		// Token: 0x0400001B RID: 27
		public TextAnimatorSettings.Category<AnimationsDatabase> appearances = new TextAnimatorSettings.Category<AnimationsDatabase>('{', '}');

		// Token: 0x0400001C RID: 28
		public TextAnimatorSettings.Category<ActionDatabase> actions = new TextAnimatorSettings.Category<ActionDatabase>('<', '>');

		// Token: 0x02000051 RID: 81
		[Serializable]
		public struct Category<T> where T : ScriptableObject
		{
			// Token: 0x06000199 RID: 409 RVA: 0x00007B13 File Offset: 0x00005D13
			public Category(char openingSymbol, char closingSymbol)
			{
				this.defaultDatabase = default(T);
				this.enabled = true;
				this.openingSymbol = openingSymbol;
				this.closingSymbol = closingSymbol;
			}

			// Token: 0x0400011A RID: 282
			public T defaultDatabase;

			// Token: 0x0400011B RID: 283
			public bool enabled;

			// Token: 0x0400011C RID: 284
			public char openingSymbol;

			// Token: 0x0400011D RID: 285
			public char closingSymbol;
		}
	}
}
