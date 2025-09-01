using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using Febucci.UI.Actions;
using Febucci.UI.Core.Parsing;
using Febucci.UI.Effects;
using UnityEngine;
using UnityEngine.Serialization;

namespace Febucci.UI.Core
{
	// Token: 0x02000034 RID: 52
	[DisallowMultipleComponent]
	[HelpURL("https://www.febucci.com/text-animator-unity/docs/how-to-add-effects-to-your-texts/")]
	public abstract class TAnimCore : MonoBehaviour
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x060000BF RID: 191 RVA: 0x00004B60 File Offset: 0x00002D60
		private TypewriterCore typewriter
		{
			get
			{
				if (this._typewriterCache != null)
				{
					return this._typewriterCache;
				}
				if (!base.TryGetComponent<TypewriterCore>(out this._typewriterCache))
				{
					Debug.LogError("Typewriter component is null on GameObject " + base.gameObject.name + ". Please add a typewriter on the same GameObject or set 'Typewriter Starts Automatically' to false.", base.gameObject);
				}
				return this._typewriterCache;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x060000C0 RID: 192 RVA: 0x00004BBB File Offset: 0x00002DBB
		// (set) Token: 0x060000C1 RID: 193 RVA: 0x00004BC3 File Offset: 0x00002DC3
		public string textFull
		{
			get
			{
				return this._text;
			}
			set
			{
				if (this.typewriterStartsAutomatically && this.typewriter)
				{
					this.SetTypewriterText(value);
					return;
				}
				this.SetText(value);
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x060000C2 RID: 194 RVA: 0x00004BE9 File Offset: 0x00002DE9
		// (set) Token: 0x060000C3 RID: 195 RVA: 0x00004BF1 File Offset: 0x00002DF1
		public string textWithoutTextAnimTags
		{
			[CompilerGenerated]
			get
			{
				return this.<textWithoutTextAnimTags>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<textWithoutTextAnimTags>k__BackingField = value;
			}
		} = string.Empty;

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x00004BFA File Offset: 0x00002DFA
		// (set) Token: 0x060000C5 RID: 197 RVA: 0x00004C02 File Offset: 0x00002E02
		public string textWithoutAnyTag
		{
			[CompilerGenerated]
			get
			{
				return this.<textWithoutAnyTag>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<textWithoutAnyTag>k__BackingField = value;
			}
		} = string.Empty;

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x00004C0B File Offset: 0x00002E0B
		private bool hasText
		{
			get
			{
				return this.charactersCount > 0;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x00004C16 File Offset: 0x00002E16
		// (set) Token: 0x060000C8 RID: 200 RVA: 0x00004C1E File Offset: 0x00002E1E
		public CharacterData latestCharacterShown
		{
			[CompilerGenerated]
			get
			{
				return this.<latestCharacterShown>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<latestCharacterShown>k__BackingField = value;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x060000C9 RID: 201 RVA: 0x00004C28 File Offset: 0x00002E28
		public bool allLettersShown
		{
			get
			{
				if (this._maxVisibleCharacters < this.charactersCount)
				{
					return false;
				}
				if (this._firstVisibleCharacter == this._maxVisibleCharacters)
				{
					return false;
				}
				for (int i = 0; i < this.charactersCount; i++)
				{
					if (!this.characters[i].isVisible)
					{
						if (this.characters[i].passedTime <= 0f)
						{
							return false;
						}
					}
					else if (this.characters[i].info.isRendered && this.characters[i].passedTime < this.characters[i].info.appearancesMaxDuration)
					{
						return false;
					}
				}
				return true;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x060000CA RID: 202 RVA: 0x00004CD8 File Offset: 0x00002ED8
		public bool anyLetterVisible
		{
			get
			{
				if (this.characters.Length == 0)
				{
					return true;
				}
				if (this.<get_anyLetterVisible>g__IsCharacterVisible|30_0(0) || this.<get_anyLetterVisible>g__IsCharacterVisible|30_0(this.charactersCount - 1))
				{
					return true;
				}
				for (int i = 1; i < this.charactersCount - 1; i++)
				{
					if (this.<get_anyLetterVisible>g__IsCharacterVisible|30_0(i))
					{
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x060000CB RID: 203 RVA: 0x00004D2A File Offset: 0x00002F2A
		public int CharactersCount
		{
			get
			{
				return this.charactersCount;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x060000CC RID: 204 RVA: 0x00004D32 File Offset: 0x00002F32
		public CharacterData[] Characters
		{
			get
			{
				return this.characters;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x060000CD RID: 205 RVA: 0x00004D3A File Offset: 0x00002F3A
		public int WordsCount
		{
			get
			{
				return this.wordsCount;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x060000CE RID: 206 RVA: 0x00004D42 File Offset: 0x00002F42
		public WordInfo[] Words
		{
			get
			{
				return this.words;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x060000CF RID: 207 RVA: 0x00004D4A File Offset: 0x00002F4A
		// (set) Token: 0x060000D0 RID: 208 RVA: 0x00004D6A File Offset: 0x00002F6A
		public AnimationsDatabase DatabaseBehaviors
		{
			get
			{
				if (!this.useDefaultDatabases)
				{
					return this.databaseBehaviors;
				}
				return TextAnimatorSettings.Instance.behaviors.defaultDatabase;
			}
			set
			{
				this.useDefaultDatabases = false;
				this.databaseBehaviors = value;
				this.requiresTagRefresh = true;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x060000D1 RID: 209 RVA: 0x00004D81 File Offset: 0x00002F81
		// (set) Token: 0x060000D2 RID: 210 RVA: 0x00004DA1 File Offset: 0x00002FA1
		public AnimationsDatabase DatabaseAppearances
		{
			get
			{
				if (!this.useDefaultDatabases)
				{
					return this.databaseAppearances;
				}
				return TextAnimatorSettings.Instance.appearances.defaultDatabase;
			}
			set
			{
				this.useDefaultDatabases = false;
				this.databaseAppearances = value;
				this.requiresTagRefresh = true;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x060000D3 RID: 211 RVA: 0x00004DB8 File Offset: 0x00002FB8
		// (set) Token: 0x060000D4 RID: 212 RVA: 0x00004DC0 File Offset: 0x00002FC0
		public AnimationRegion[] Behaviors
		{
			get
			{
				return this.behaviors;
			}
			set
			{
				this.behaviors = value;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x060000D5 RID: 213 RVA: 0x00004DC9 File Offset: 0x00002FC9
		// (set) Token: 0x060000D6 RID: 214 RVA: 0x00004DD1 File Offset: 0x00002FD1
		public AnimationRegion[] Appearances
		{
			get
			{
				return this.appearances;
			}
			set
			{
				this.appearances = value;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060000D7 RID: 215 RVA: 0x00004DDA File Offset: 0x00002FDA
		// (set) Token: 0x060000D8 RID: 216 RVA: 0x00004DE2 File Offset: 0x00002FE2
		public AnimationRegion[] Disappearances
		{
			get
			{
				return this.disappearances;
			}
			set
			{
				this.disappearances = value;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060000D9 RID: 217 RVA: 0x00004DEB File Offset: 0x00002FEB
		// (set) Token: 0x060000DA RID: 218 RVA: 0x00004DF3 File Offset: 0x00002FF3
		public ActionMarker[] Actions
		{
			get
			{
				return this.actions;
			}
			set
			{
				this.actions = value;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060000DB RID: 219 RVA: 0x00004DFC File Offset: 0x00002FFC
		// (set) Token: 0x060000DC RID: 220 RVA: 0x00004E1C File Offset: 0x0000301C
		public ActionDatabase DatabaseActions
		{
			get
			{
				if (!this.useDefaultDatabases)
				{
					return this.databaseActions;
				}
				return TextAnimatorSettings.Instance.actions.defaultDatabase;
			}
			set
			{
				this.databaseActions = value;
				this.requiresTagRefresh = true;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060000DD RID: 221 RVA: 0x00004E2C File Offset: 0x0000302C
		// (set) Token: 0x060000DE RID: 222 RVA: 0x00004E34 File Offset: 0x00003034
		public EventMarker[] Events
		{
			get
			{
				return this.events;
			}
			set
			{
				this.events = value;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000DF RID: 223 RVA: 0x00004E3D File Offset: 0x0000303D
		// (set) Token: 0x060000E0 RID: 224 RVA: 0x00004E45 File Offset: 0x00003045
		public string[] DefaultAppearancesTags
		{
			get
			{
				return this.defaultAppearancesTags;
			}
			set
			{
				this.defaultAppearancesTags = value;
				this.requiresTagRefresh = true;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000E1 RID: 225 RVA: 0x00004E55 File Offset: 0x00003055
		// (set) Token: 0x060000E2 RID: 226 RVA: 0x00004E5D File Offset: 0x0000305D
		public string[] DefaultDisappearancesTags
		{
			get
			{
				return this.defaultDisappearancesTags;
			}
			set
			{
				this.defaultDisappearancesTags = value;
				this.requiresTagRefresh = true;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000E3 RID: 227 RVA: 0x00004E6D File Offset: 0x0000306D
		// (set) Token: 0x060000E4 RID: 228 RVA: 0x00004E75 File Offset: 0x00003075
		public string[] DefaultBehaviorsTags
		{
			get
			{
				return this.defaultBehaviorsTags;
			}
			set
			{
				this.defaultBehaviorsTags = value;
				this.requiresTagRefresh = true;
			}
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00004E85 File Offset: 0x00003085
		protected virtual void OnInitialized()
		{
		}

		// Token: 0x060000E6 RID: 230
		public abstract string GetOriginalTextFromSource();

		// Token: 0x060000E7 RID: 231
		public abstract string GetStrippedTextFromSource();

		// Token: 0x060000E8 RID: 232
		public abstract void SetTextToSource(string text);

		// Token: 0x060000E9 RID: 233
		protected abstract bool HasChangedText(string strippedText);

		// Token: 0x060000EA RID: 234
		protected abstract bool HasChangedRenderingSettings();

		// Token: 0x060000EB RID: 235
		protected abstract int GetCharactersCount();

		// Token: 0x060000EC RID: 236
		protected abstract void OnForceMeshUpdate();

		// Token: 0x060000ED RID: 237
		protected abstract void CopyMeshFromSource(ref CharacterData[] characters);

		// Token: 0x060000EE RID: 238
		protected abstract void PasteMeshToSource(CharacterData[] characters);

		// Token: 0x060000EF RID: 239 RVA: 0x00004E87 File Offset: 0x00003087
		private void ForceMeshUpdate()
		{
			this.requiresMeshUpdate = false;
			this.OnForceMeshUpdate();
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00004E96 File Offset: 0x00003096
		private void Awake()
		{
			this.requiresTagRefresh = true;
			this.TryInitializing();
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00004EA8 File Offset: 0x000030A8
		private void TryInitializing()
		{
			if (this.initialized)
			{
				return;
			}
			this.initialized = true;
			TextUtilities.Initialize();
			this.charactersCount = 0;
			this.characters = new CharacterData[0];
			this.wordsCount = 0;
			this.words = new WordInfo[0];
			this.behaviors = new AnimationRegion[0];
			this.appearances = new AnimationRegion[0];
			this.disappearances = new AnimationRegion[0];
			this.actions = new ActionMarker[0];
			this.events = new EventMarker[0];
			if (this.DatabaseActions)
			{
				this.DatabaseActions.ForceBuildRefresh();
			}
			if (this.DatabaseAppearances)
			{
				this.DatabaseAppearances.ForceBuildRefresh();
			}
			if (this.DatabaseBehaviors)
			{
				this.DatabaseBehaviors.ForceBuildRefresh();
			}
			this.visibilityGroups = base.GetComponentsInParent<CanvasGroup>();
			this.OnInitialized();
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00004F88 File Offset: 0x00003188
		private void UpdateUniformIntensity()
		{
			if (this.useDynamicScaling)
			{
				for (int i = 0; i < this.characters.Length; i++)
				{
					this.characters[i].UpdateIntensity(this.referenceFontSize);
				}
				return;
			}
			for (int j = 0; j < this.characters.Length; j++)
			{
				this.characters[j].uniformIntensity = 1f;
			}
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00004FF1 File Offset: 0x000031F1
		protected virtual TagParserBase[] GetExtraParsers()
		{
			return Array.Empty<TagParserBase>();
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00004FF8 File Offset: 0x000031F8
		private void ConvertText(string textToParse, TAnimCore.ShowTextMode showTextMode)
		{
			this.TryInitializing();
			this.requiresTagRefresh = false;
			this._text = textToParse;
			this.settings = TextAnimatorSettings.Instance;
			if (!this.settings)
			{
				this.charactersCount = 0;
				Debug.LogError("Text Animator Settings not found. Skipping setting the text to Text Animator.");
				return;
			}
			if (this.useDefaultDatabases)
			{
				this.databaseBehaviors = this.settings.behaviors.defaultDatabase;
				this.databaseAppearances = this.settings.appearances.defaultDatabase;
				this.databaseActions = this.settings.actions.defaultDatabase;
			}
			AnimationParser<AnimationScriptableBase> animationParser = new AnimationParser<AnimationScriptableBase>(this.settings.behaviors.openingSymbol, '/', this.settings.behaviors.closingSymbol, VisibilityMode.Persistent, this.databaseBehaviors);
			AnimationParser<AnimationScriptableBase> animationParser2 = new AnimationParser<AnimationScriptableBase>(this.settings.appearances.openingSymbol, '/', this.settings.appearances.closingSymbol, VisibilityMode.OnVisible, this.databaseAppearances);
			AnimationParser<AnimationScriptableBase> animationParser3 = new AnimationParser<AnimationScriptableBase>(this.settings.appearances.openingSymbol, '/', '#', this.settings.appearances.closingSymbol, VisibilityMode.OnHiding, this.databaseAppearances);
			ActionParser actionParser = new ActionParser(this.settings.actions.openingSymbol, '/', this.settings.actions.closingSymbol, this.databaseActions);
			EventParser eventParser = new EventParser('<', '/', '>');
			List<TagParserBase> list = new List<TagParserBase>
			{
				animationParser,
				animationParser2,
				animationParser3,
				actionParser,
				eventParser
			};
			foreach (TagParserBase item in this.GetExtraParsers())
			{
				list.Add(item);
			}
			this.textWithoutTextAnimTags = TextParser.ParseText(this._text, list.ToArray());
			this.SetTextToSource(this.textWithoutTextAnimTags);
			this.textWithoutAnyTag = this.GetStrippedTextFromSource();
			this.charactersCount = this.GetCharactersCount();
			this.behaviors = animationParser.results;
			this.appearances = animationParser2.results;
			this.disappearances = animationParser3.results;
			this.actions = actionParser.results;
			this.events = eventParser.results;
			this.<ConvertText>g__AddFallbackEffectsFor|115_6<AnimationScriptableBase>(ref this.behaviors, VisibilityMode.Persistent, this.databaseBehaviors, this.defaultBehaviorsTags);
			this.<ConvertText>g__AddFallbackEffectsFor|115_6<AnimationScriptableBase>(ref this.appearances, VisibilityMode.OnVisible, this.databaseAppearances, this.defaultAppearancesTags);
			this.<ConvertText>g__AddFallbackEffectsFor|115_6<AnimationScriptableBase>(ref this.disappearances, VisibilityMode.OnHiding, this.databaseAppearances, this.defaultDisappearancesTags);
			AnimationRegion[] array = this.behaviors;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].animation.InitializeOnce();
			}
			array = this.appearances;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].animation.InitializeOnce();
			}
			array = this.disappearances;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].animation.InitializeOnce();
			}
			this.<ConvertText>g__PopulateCharacters|115_0();
			this.CopyMeshFromSource(ref this.characters);
			this.<ConvertText>g__CalculateWords|115_1();
			switch (showTextMode)
			{
			case TAnimCore.ShowTextMode.Hidden:
				this.<ConvertText>g__HideAllCharactersTime|115_3();
				break;
			case TAnimCore.ShowTextMode.Shown:
				this.<ConvertText>g__ShowCharacterTimes|115_4();
				break;
			case TAnimCore.ShowTextMode.UserTyping:
				this.<ConvertText>g__ShowCharacterTimes|115_4();
				if (this.charactersCount > 1)
				{
					this.<ConvertText>g__HideCharacterTime|115_2(this.charactersCount - 1);
					this.characters[this.charactersCount - 1].isVisible = true;
				}
				break;
			}
			this._maxVisibleCharacters = this.charactersCount;
			if (this.isResettingTimeOnNewText && showTextMode != TAnimCore.ShowTextMode.Refresh)
			{
				this.time.RestartTime();
			}
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00005388 File Offset: 0x00003588
		public void SetText(string text)
		{
			this.ConvertText(text, TAnimCore.ShowTextMode.Shown);
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00005392 File Offset: 0x00003592
		public void SetText(string text, bool hideText)
		{
			this.ConvertText(text, hideText ? TAnimCore.ShowTextMode.Hidden : TAnimCore.ShowTextMode.Shown);
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x000053A4 File Offset: 0x000035A4
		public void AppendText(string appendedText, bool hideText = false)
		{
			if (string.IsNullOrEmpty(appendedText))
			{
				return;
			}
			if (!this.hasText)
			{
				this.SetText(appendedText, hideText);
				return;
			}
			bool flag = this.isResettingTimeOnNewText;
			this.isResettingTimeOnNewText = false;
			int maxVisibleCharacters = this.maxVisibleCharacters;
			int firstVisibleCharacter = this.firstVisibleCharacter;
			this.SetText(this.textFull + appendedText, hideText);
			this.isResettingTimeOnNewText = flag;
			this.maxVisibleCharacters = maxVisibleCharacters;
			this.firstVisibleCharacter = firstVisibleCharacter;
			for (int i = this.firstVisibleCharacter; i < this.maxVisibleCharacters; i++)
			{
				this.characters[i].isVisible = true;
				this.characters[i].passedTime = this.characters[i].info.appearancesMaxDuration;
			}
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x0000545E File Offset: 0x0000365E
		private void SetTypewriterText(string text)
		{
			if (text.Length <= 0)
			{
				this.typewriter.ShowText("");
				return;
			}
			this.typewriter.ShowText("<noparse></noparse>" + text);
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00005490 File Offset: 0x00003690
		public void SetVisibilityChar(int index, bool isVisible)
		{
			if (index < 0 || index >= this.charactersCount)
			{
				return;
			}
			this.characters[index].isVisible = isVisible;
			if (isVisible)
			{
				this.latestCharacterShown = this.characters[index];
			}
		}

		// Token: 0x060000FA RID: 250 RVA: 0x000054C8 File Offset: 0x000036C8
		public void SetVisibilityWord(int index, bool isVisible)
		{
			if (index < 0 || index >= this.wordsCount)
			{
				return;
			}
			WordInfo wordInfo = this.words[index];
			int num = Mathf.Max(wordInfo.firstCharacterIndex, 0);
			while (num <= wordInfo.lastCharacterIndex && num < this.charactersCount)
			{
				this.SetVisibilityChar(num, isVisible);
				num++;
			}
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00005520 File Offset: 0x00003720
		public void SetVisibilityEntireText(bool isVisible, bool canPlayEffects = true)
		{
			for (int i = 0; i < this.charactersCount; i++)
			{
				this.SetVisibilityChar(i, isVisible);
			}
			if (!canPlayEffects)
			{
				if (isVisible)
				{
					for (int j = 0; j < this.charactersCount; j++)
					{
						this.characters[j].passedTime = this.characters[j].info.appearancesMaxDuration;
					}
					return;
				}
				for (int k = 0; k < this.charactersCount; k++)
				{
					this.characters[k].passedTime = 0f;
				}
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000FC RID: 252 RVA: 0x000055AC File Offset: 0x000037AC
		// (set) Token: 0x060000FD RID: 253 RVA: 0x000055B4 File Offset: 0x000037B4
		public int firstVisibleCharacter
		{
			get
			{
				return this._firstVisibleCharacter;
			}
			set
			{
				this._firstVisibleCharacter = value;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000FE RID: 254 RVA: 0x000055BD File Offset: 0x000037BD
		// (set) Token: 0x060000FF RID: 255 RVA: 0x000055C5 File Offset: 0x000037C5
		public int maxVisibleCharacters
		{
			get
			{
				return this._maxVisibleCharacters;
			}
			set
			{
				if (this._maxVisibleCharacters == value)
				{
					return;
				}
				this._maxVisibleCharacters = value;
				if (this._maxVisibleCharacters < 0)
				{
					this._maxVisibleCharacters = 0;
				}
			}
		}

		// Token: 0x06000100 RID: 256 RVA: 0x000055E8 File Offset: 0x000037E8
		private void Update()
		{
			if (!this.HasChangedText(this.textWithoutTextAnimTags))
			{
				if (this.animationLoop == AnimationLoop.Update)
				{
					this.Animate((this.timeScale == TimeScale.Unscaled) ? Time.unscaledDeltaTime : Time.deltaTime);
				}
				return;
			}
			if (this.typewriterStartsAutomatically && this.typewriter)
			{
				this.SetTypewriterText(this.GetOriginalTextFromSource());
				return;
			}
			this.ConvertText(this.GetOriginalTextFromSource(), TAnimCore.ShowTextMode.UserTyping);
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00005656 File Offset: 0x00003856
		private void LateUpdate()
		{
			if (this.animationLoop == AnimationLoop.LateUpdate)
			{
				this.Animate((this.timeScale == TimeScale.Unscaled) ? Time.unscaledDeltaTime : Time.deltaTime);
			}
		}

		// Token: 0x06000102 RID: 258 RVA: 0x0000567C File Offset: 0x0000387C
		public void Animate(float deltaTime)
		{
			if (this.requiresTagRefresh)
			{
				this.ConvertText(this._text, TAnimCore.ShowTextMode.Refresh);
			}
			this.time.UpdateDeltaTime(deltaTime);
			this.time.IncreaseTime();
			this.AnimateText();
		}

		// Token: 0x06000103 RID: 259 RVA: 0x000056B0 File Offset: 0x000038B0
		private bool IsCharacterAppearing(int i)
		{
			return i >= this._firstVisibleCharacter && i < this._maxVisibleCharacters && this.characters[i].isVisible;
		}

		// Token: 0x06000104 RID: 260 RVA: 0x000056D8 File Offset: 0x000038D8
		private bool CanSeeText()
		{
			float num = 1f;
			foreach (CanvasGroup canvasGroup in this.visibilityGroups)
			{
				num *= canvasGroup.alpha;
			}
			return num > 0f;
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00005718 File Offset: 0x00003918
		private void ProcessAnimationRegions(AnimationRegion[] regions)
		{
			foreach (AnimationRegion animationRegion in regions)
			{
				foreach (TagRange tagRange in animationRegion.ranges)
				{
					animationRegion.SetupContextFor(this, tagRange.modifiers);
					Vector2Int indexes = tagRange.indexes;
					int num = indexes.x;
					for (;;)
					{
						int num2 = num;
						indexes = tagRange.indexes;
						if (num2 >= indexes.y || num >= this.charactersCount)
						{
							break;
						}
						if (this.characters[num].passedTime > 0f && animationRegion.IsVisibilityPolicySatisfied(this.IsCharacterAppearing(num)) && animationRegion.animation.CanApplyEffectTo(this.characters[num], this))
						{
							animationRegion.animation.ApplyEffectTo(ref this.characters[num], this);
						}
						num++;
					}
				}
			}
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00005810 File Offset: 0x00003A10
		private void AnimateText()
		{
			if (!this.hasText)
			{
				return;
			}
			this.TryInitializing();
			if (!this.CanSeeText())
			{
				return;
			}
			int num = 0;
			while (num < this.charactersCount && num < this.characters.Length)
			{
				if (!this.characters[num].info.isRendered)
				{
					this.characters[num].passedTime = 0f;
					this.characters[num].Hide();
				}
				else
				{
					this.characters[num].ResetAnimation();
					if (this.IsCharacterAppearing(num))
					{
						CharacterData[] array = this.characters;
						int num2 = num;
						array[num2].passedTime = array[num2].passedTime + this.time.deltaTime;
					}
					else
					{
						if (this.characters[num].passedTime > this.characters[num].info.disappearancesMaxDuration)
						{
							this.characters[num].passedTime = this.characters[num].info.disappearancesMaxDuration;
						}
						else
						{
							CharacterData[] array2 = this.characters;
							int num3 = num;
							array2[num3].passedTime = array2[num3].passedTime - this.time.deltaTime;
						}
						if (this.characters[num].passedTime <= 0f)
						{
							this.characters[num].passedTime = 0f;
							this.characters[num].Hide();
						}
					}
				}
				num++;
			}
			this.UpdateUniformIntensity();
			if (this.isAnimatingBehaviors && this.settings.behaviors.enabled)
			{
				this.ProcessAnimationRegions(this.behaviors);
			}
			if (this.isAnimatingAppearances && this.settings.appearances.enabled)
			{
				this.ProcessAnimationRegions(this.appearances);
				this.ProcessAnimationRegions(this.disappearances);
			}
			this.PasteMeshToSource(this.characters);
			if (this.requiresMeshUpdate || this.HasChangedRenderingSettings())
			{
				this.ForceMeshUpdate();
				this.CopyMeshFromSource(ref this.characters);
			}
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00005A17 File Offset: 0x00003C17
		public void ScheduleMeshRefresh()
		{
			this.requiresMeshUpdate = true;
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00005A20 File Offset: 0x00003C20
		public void ForceDatabaseRefresh()
		{
			if (this.DatabaseActions)
			{
				this.DatabaseActions.ForceBuildRefresh();
			}
			if (this.DatabaseAppearances)
			{
				this.DatabaseAppearances.ForceBuildRefresh();
			}
			if (this.DatabaseBehaviors)
			{
				this.DatabaseBehaviors.ForceBuildRefresh();
			}
			this.ConvertText(this.GetOriginalTextFromSource(), TAnimCore.ShowTextMode.Refresh);
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00005A82 File Offset: 0x00003C82
		public void SetBehaviorsActive(bool isCategoryEnabled)
		{
			this.isAnimatingBehaviors = isCategoryEnabled;
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00005A8B File Offset: 0x00003C8B
		public void SetAppearancesActive(bool isCategoryEnabled)
		{
			this.isAnimatingAppearances = isCategoryEnabled;
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00005A94 File Offset: 0x00003C94
		private void OnRectTransformDimensionsChange()
		{
			this.AnimateText();
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00005A9C File Offset: 0x00003C9C
		public void ResetState()
		{
			this._text = string.Empty;
			this.textWithoutTextAnimTags = string.Empty;
			this.textWithoutAnyTag = string.Empty;
			this.charactersCount = 0;
			this.wordsCount = 0;
			this.initialized = false;
			this.TryInitializing();
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00005ADA File Offset: 0x00003CDA
		[Obsolete("Use TextAnimatorSettings.SetAllEffectsActive instead")]
		public static void EnableAllEffects(bool enabled)
		{
			TextAnimatorSettings.SetAllEffectsActive(enabled);
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00005AE2 File Offset: 0x00003CE2
		[Obsolete("Use TextAnimatorSettings.SetAppearancesActive instead")]
		public static void EnableAppearances(bool enabled)
		{
			TextAnimatorSettings.SetAppearancesActive(enabled);
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00005AEA File Offset: 0x00003CEA
		[Obsolete("Use TextAnimatorSettings.SetBehaviorsActive instead")]
		public static void EnableBehaviors(bool enabled)
		{
			TextAnimatorSettings.SetBehaviorsActive(enabled);
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00005AF2 File Offset: 0x00003CF2
		[Obsolete("Use SetAppearancesActive instead")]
		public void EnableAppearancesLocally(bool value)
		{
			this.SetAppearancesActive(value);
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00005AFB File Offset: 0x00003CFB
		[Obsolete("Use SetBehaviorsActive instead")]
		public void EnableBehaviorsLocally(bool value)
		{
			this.SetBehaviorsActive(value);
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00005B04 File Offset: 0x00003D04
		[Obsolete("Use SetVisibilityEntireText instead")]
		public void ShowAllCharacters(bool skipAppearanceEffects)
		{
			this.SetVisibilityEntireText(true, skipAppearanceEffects);
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00005B0E File Offset: 0x00003D0E
		[Obsolete("Use 'Animate' instead.")]
		public void UpdateEffects()
		{
			this.Animate((this.timeScale == TimeScale.Unscaled) ? Time.unscaledDeltaTime : Time.deltaTime);
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00005B2B File Offset: 0x00003D2B
		[Obsolete("Events are not tied to TextAnimators anymore, but to their Typewriters. Please invoke 'TriggerRemainingEvents' on the Typewriter component instead.")]
		public void TriggerRemainingEvents()
		{
			if (this.typewriter)
			{
				this.typewriter.TriggerRemainingEvents();
			}
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00005B45 File Offset: 0x00003D45
		[Obsolete("Events are not tied to TextAnimators anymore, but to their related typewriters. Please invoke 'TriggerVisibleEvents' on the Typewriter component instead.")]
		public void TriggerVisibleEvents()
		{
			if (this.typewriter)
			{
				this.typewriter.TriggerVisibleEvents();
			}
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00005B5F File Offset: 0x00003D5F
		[Obsolete("Use 'ScheduleMeshRefresh' instead")]
		public void ForceMeshRefresh()
		{
			this.ScheduleMeshRefresh();
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00005B67 File Offset: 0x00003D67
		[Obsolete("To restart TextAnimator's time, please use 'time.RestartTime()'. To skip appearances effects please set 'SetVisibilityEntireText(true, false)' instead")]
		public void ResetEffectsTime(bool skipAppearances)
		{
			this.time.RestartTime();
			if (skipAppearances)
			{
				this.SetVisibilityEntireText(true, false);
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000118 RID: 280 RVA: 0x00005B7F File Offset: 0x00003D7F
		[Obsolete("Please use 'isResettingTimeOnNewText' instead")]
		public bool isResettingEffectsOnNewText
		{
			get
			{
				return this.isResettingTimeOnNewText;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000119 RID: 281 RVA: 0x00005B87 File Offset: 0x00003D87
		[Obsolete("Please use 'animationLoop' instead")]
		public AnimationLoop updateMode
		{
			get
			{
				return this.animationLoop;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600011A RID: 282 RVA: 0x00005B8F File Offset: 0x00003D8F
		[Obsolete("Events are now handled/stored by Typewriters instead.")]
		public MessageEvent onEvent
		{
			get
			{
				return this.typewriter.onMessage;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600011B RID: 283 RVA: 0x00005B9C File Offset: 0x00003D9C
		[Obsolete("Please use TextAnimatorSettings.Instance.appearances.enabled instead")]
		public static bool effectsAppearancesEnabled
		{
			get
			{
				return TextAnimatorSettings.Instance.appearances.enabled;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600011C RID: 284 RVA: 0x00005BAD File Offset: 0x00003DAD
		[Obsolete("Please use TextAnimatorSettings.Instance.behaviors.enabled instead")]
		public static bool effectsBehaviorsEnabled
		{
			get
			{
				return TextAnimatorSettings.Instance.behaviors.enabled;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600011D RID: 285 RVA: 0x00005BBE File Offset: 0x00003DBE
		[Obsolete("Please use 'textFull' instead")]
		public string text
		{
			get
			{
				return this.textFull;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600011E RID: 286 RVA: 0x00005BC6 File Offset: 0x00003DC6
		// (set) Token: 0x0600011F RID: 287 RVA: 0x00005BCE File Offset: 0x00003DCE
		[Obsolete("Please change 'referenceFontSize' instead")]
		public float effectIntensityMultiplier
		{
			get
			{
				return this.referenceFontSize;
			}
			set
			{
				this.referenceFontSize = value;
			}
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00005BD8 File Offset: 0x00003DD8
		protected TAnimCore()
		{
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00005C62 File Offset: 0x00003E62
		[CompilerGenerated]
		private bool <get_anyLetterVisible>g__IsCharacterVisible|30_0(int index)
		{
			return this.characters[index].passedTime > 0f;
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00005C7C File Offset: 0x00003E7C
		[CompilerGenerated]
		private void <ConvertText>g__PopulateCharacters|115_0()
		{
			if (this.characters.Length < this.charactersCount)
			{
				Array.Resize<CharacterData>(ref this.characters, this.charactersCount);
			}
			TAnimCore.<>c__DisplayClass115_0 CS$<>8__locals1;
			CS$<>8__locals1.i = 0;
			while (CS$<>8__locals1.i < this.charactersCount)
			{
				this.characters[CS$<>8__locals1.i].ResetInfo(CS$<>8__locals1.i);
				this.characters[CS$<>8__locals1.i].info.disappearancesMaxDuration = this.<ConvertText>g__CalculateRegionMaxDuration|115_7(this.disappearances, ref CS$<>8__locals1);
				this.characters[CS$<>8__locals1.i].info.appearancesMaxDuration = this.<ConvertText>g__CalculateRegionMaxDuration|115_7(this.appearances, ref CS$<>8__locals1);
				int i = CS$<>8__locals1.i;
				CS$<>8__locals1.i = i + 1;
			}
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00005D44 File Offset: 0x00003F44
		[CompilerGenerated]
		private float <ConvertText>g__CalculateRegionMaxDuration|115_7(AnimationRegion[] tags, ref TAnimCore.<>c__DisplayClass115_0 A_2)
		{
			float num = 0f;
			foreach (AnimationRegion animationRegion in tags)
			{
				foreach (TagRange tagRange in animationRegion.ranges)
				{
					int i2 = A_2.i;
					Vector2Int indexes = tagRange.indexes;
					if (i2 >= indexes.x)
					{
						int i3 = A_2.i;
						indexes = tagRange.indexes;
						if (i3 < indexes.y)
						{
							animationRegion.SetupContextFor(this, tagRange.modifiers);
							float maxDuration = animationRegion.animation.GetMaxDuration();
							if (maxDuration > num)
							{
								num = maxDuration;
							}
						}
					}
				}
			}
			return num;
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00005DEC File Offset: 0x00003FEC
		[CompilerGenerated]
		private void <ConvertText>g__CalculateWords|115_1()
		{
			StringBuilder stringBuilder = new StringBuilder();
			this.wordsCount = this.charactersCount;
			if (this.words.Length < this.wordsCount)
			{
				Array.Resize<WordInfo>(ref this.words, this.wordsCount);
			}
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			for (int i = 0; i < this.charactersCount; i++)
			{
				if (!char.IsWhiteSpace(this.characters[i].info.character))
				{
					this.characters[i].wordIndex = num2;
					stringBuilder.Append(this.characters[i].info.character);
					num++;
				}
				else
				{
					this.characters[i].wordIndex = -1;
					if (num > 0)
					{
						this.words[num2] = new WordInfo(num3, num3 + num - 1, stringBuilder.ToString());
						num3 += num + 1;
						num2++;
					}
					else
					{
						num3++;
					}
					stringBuilder.Clear();
					num = 0;
				}
			}
			if (num > 0)
			{
				this.words[num2] = new WordInfo(num3, num3 + num - 1, stringBuilder.ToString());
				num2++;
			}
			this.wordsCount = num2;
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00005F1C File Offset: 0x0000411C
		[CompilerGenerated]
		private void <ConvertText>g__HideCharacterTime|115_2(int charIndex)
		{
			CharacterData characterData = this.characters[charIndex];
			characterData.isVisible = false;
			characterData.passedTime = 0f;
			characterData.Hide();
			this.characters[charIndex] = characterData;
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00005F60 File Offset: 0x00004160
		[CompilerGenerated]
		private void <ConvertText>g__HideAllCharactersTime|115_3()
		{
			for (int i = 0; i < this.charactersCount; i++)
			{
				this.<ConvertText>g__HideCharacterTime|115_2(i);
			}
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00005F88 File Offset: 0x00004188
		[CompilerGenerated]
		private void <ConvertText>g__ShowCharacterTimes|115_4()
		{
			for (int i = 0; i < this.charactersCount; i++)
			{
				CharacterData characterData = this.characters[i];
				characterData.isVisible = true;
				characterData.passedTime = characterData.info.appearancesMaxDuration;
				this.characters[i] = characterData;
			}
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00005FDC File Offset: 0x000041DC
		[CompilerGenerated]
		internal static bool <ConvertText>g__IsCharacterInsideAnyEffect|115_5(int charIndex, AnimationRegion[] regions)
		{
			for (int i = 0; i < regions.Length; i++)
			{
				foreach (TagRange tagRange in regions[i].ranges)
				{
					Vector2Int indexes = tagRange.indexes;
					if (charIndex >= indexes.x)
					{
						indexes = tagRange.indexes;
						if (indexes.y != 2147483647)
						{
							indexes = tagRange.indexes;
							if (charIndex >= indexes.y)
							{
								goto IL_5B;
							}
						}
						return true;
					}
					IL_5B:;
				}
			}
			return false;
		}

		// Token: 0x06000129 RID: 297 RVA: 0x0000605C File Offset: 0x0000425C
		[CompilerGenerated]
		private void <ConvertText>g__AddFallbackEffectsFor|115_6<T>(ref AnimationRegion[] currentEffects, VisibilityMode visibilityMode, Database<T> database, string[] defaultEffectsTags) where T : AnimationScriptableBase
		{
			if (!database)
			{
				return;
			}
			if (defaultEffectsTags == null || defaultEffectsTags.Length == 0)
			{
				return;
			}
			List<TAnimCore.DefaultRegion> list = new List<TAnimCore.DefaultRegion>();
			foreach (string text in defaultEffectsTags)
			{
				if (string.IsNullOrEmpty(text))
				{
					if (Application.isPlaying)
					{
						Debug.LogError("Empty tag as default effect in database " + database.name + ". Skipping.", base.gameObject);
					}
				}
				else
				{
					string[] array = text.Split(' ', StringSplitOptions.None);
					string text2 = array[0];
					if (!database.ContainsKey(text2))
					{
						if (Application.isPlaying)
						{
							Debug.LogError(string.Concat(new string[]
							{
								"Fallback effect with tag '",
								text2,
								"' not found in database ",
								database.name,
								". Skipping."
							}), base.gameObject);
						}
					}
					else
					{
						list.Add(new TAnimCore.DefaultRegion(text2, visibilityMode, database[text2], array));
					}
				}
			}
			if (currentEffects.Length == 0 || this.defaultTagsMode == TAnimCore.DefaultTagsMode.Constant)
			{
				using (List<TAnimCore.DefaultRegion>.Enumerator enumerator = list.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						TAnimCore.DefaultRegion defaultRegion = enumerator.Current;
						defaultRegion.region.OpenNewRange(0, defaultRegion.tagWords);
					}
					goto IL_200;
				}
			}
			for (int j = 0; j < this.charactersCount; j++)
			{
				if (!TAnimCore.<ConvertText>g__IsCharacterInsideAnyEffect|115_5(j, currentEffects))
				{
					foreach (TAnimCore.DefaultRegion defaultRegion2 in list)
					{
						defaultRegion2.region.OpenNewRange(j, defaultRegion2.tagWords);
					}
					int num = j + 1;
					while (num < this.charactersCount && !TAnimCore.<ConvertText>g__IsCharacterInsideAnyEffect|115_5(num, currentEffects))
					{
						num++;
					}
					foreach (TAnimCore.DefaultRegion defaultRegion3 in list)
					{
						defaultRegion3.region.TryClosingRange(num);
					}
					j = num;
				}
			}
			IL_200:
			int num2 = currentEffects.Length;
			Array.Resize<AnimationRegion>(ref currentEffects, currentEffects.Length + list.Count);
			for (int k = 0; k < list.Count; k++)
			{
				currentEffects[num2 + k] = list[k].region;
			}
		}

		// Token: 0x040000A0 RID: 160
		private bool initialized;

		// Token: 0x040000A1 RID: 161
		private bool requiresTagRefresh;

		// Token: 0x040000A2 RID: 162
		[Tooltip("If the source text changes, should the typewriter start automatically? Requires a Typewriter component if true.\nP.s. Previously, this option was called 'Use Easy Integration'.")]
		public bool typewriterStartsAutomatically;

		// Token: 0x040000A3 RID: 163
		private TypewriterCore _typewriterCache;

		// Token: 0x040000A4 RID: 164
		[Tooltip("Controls when this TextAnimator component should update its effects. Defaults in the 'Update' Loop.\nSet it to 'Manual' if you want to control the animations from your own loop instead.")]
		public AnimationLoop animationLoop;

		// Token: 0x040000A5 RID: 165
		[Tooltip("Chooses which Time Scale to use when animating effects.\nSet it to 'Unscaled' if you want to animate effects even when the game is paused.")]
		public TimeScale timeScale;

		// Token: 0x040000A6 RID: 166
		[SerializeField]
		[TextArea(4, 10)]
		[HideInInspector]
		private string _text = string.Empty;

		// Token: 0x040000A7 RID: 167
		[CompilerGenerated]
		private string <textWithoutTextAnimTags>k__BackingField;

		// Token: 0x040000A8 RID: 168
		[CompilerGenerated]
		private string <textWithoutAnyTag>k__BackingField;

		// Token: 0x040000A9 RID: 169
		[CompilerGenerated]
		private CharacterData <latestCharacterShown>k__BackingField;

		// Token: 0x040000AA RID: 170
		private int charactersCount;

		// Token: 0x040000AB RID: 171
		private CharacterData[] characters;

		// Token: 0x040000AC RID: 172
		private int wordsCount;

		// Token: 0x040000AD RID: 173
		private WordInfo[] words;

		// Token: 0x040000AE RID: 174
		[Tooltip("True if you want the animations to be uniform/consistent across different font sizes. Default/Suggested to leave this as true, and change the 'Reference Font Size'.\nOtherwise, effects will move more when the text is smaller (requires less space on screen)")]
		public bool useDynamicScaling = true;

		// Token: 0x040000AF RID: 175
		[Tooltip("Font size that will be used as reference to keep animations consistent/uniform at different scales.")]
		public float referenceFontSize = 10f;

		// Token: 0x040000B0 RID: 176
		[Tooltip("True if you want the animator's time to be reset on new text.")]
		[FormerlySerializedAs("isResettingEffectsOnNewText")]
		public bool isResettingTimeOnNewText = true;

		// Token: 0x040000B1 RID: 177
		private bool isAnimatingBehaviors = true;

		// Token: 0x040000B2 RID: 178
		private bool isAnimatingAppearances = true;

		// Token: 0x040000B3 RID: 179
		[Tooltip("Lets you use the databases referenced in the 'TextAnimatorSettings' asset.\nSet to false if you'd like to specify which databases to use in this component.")]
		public bool useDefaultDatabases = true;

		// Token: 0x040000B4 RID: 180
		[SerializeField]
		private AnimationsDatabase databaseBehaviors;

		// Token: 0x040000B5 RID: 181
		[SerializeField]
		private AnimationsDatabase databaseAppearances;

		// Token: 0x040000B6 RID: 182
		private AnimationRegion[] behaviors;

		// Token: 0x040000B7 RID: 183
		private AnimationRegion[] appearances;

		// Token: 0x040000B8 RID: 184
		private AnimationRegion[] disappearances;

		// Token: 0x040000B9 RID: 185
		private ActionMarker[] actions;

		// Token: 0x040000BA RID: 186
		[SerializeField]
		private ActionDatabase databaseActions;

		// Token: 0x040000BB RID: 187
		private EventMarker[] events;

		// Token: 0x040000BC RID: 188
		[SerializeField]
		private string[] defaultAppearancesTags = new string[]
		{
			"size"
		};

		// Token: 0x040000BD RID: 189
		[SerializeField]
		private string[] defaultDisappearancesTags = new string[]
		{
			"fade"
		};

		// Token: 0x040000BE RID: 190
		[SerializeField]
		private string[] defaultBehaviorsTags;

		// Token: 0x040000BF RID: 191
		public CanvasGroup[] visibilityGroups;

		// Token: 0x040000C0 RID: 192
		private bool requiresMeshUpdate;

		// Token: 0x040000C1 RID: 193
		[HideInInspector]
		public TimeData time;

		// Token: 0x040000C2 RID: 194
		[Tooltip("Controls how default tags should be applied.\n\"Fallback\" will apply the effects only to characters that don't have any.\n\"Constant\" will apply the default effects to all the characters, even if they already have other tags via text.")]
		public TAnimCore.DefaultTagsMode defaultTagsMode;

		// Token: 0x040000C3 RID: 195
		private TextAnimatorSettings settings;

		// Token: 0x040000C4 RID: 196
		private int _firstVisibleCharacter;

		// Token: 0x040000C5 RID: 197
		private int _maxVisibleCharacters;

		// Token: 0x02000056 RID: 86
		private enum ShowTextMode : byte
		{
			// Token: 0x04000131 RID: 305
			Hidden,
			// Token: 0x04000132 RID: 306
			Shown,
			// Token: 0x04000133 RID: 307
			UserTyping,
			// Token: 0x04000134 RID: 308
			Refresh
		}

		// Token: 0x02000057 RID: 87
		private struct DefaultRegion
		{
			// Token: 0x060001AC RID: 428 RVA: 0x00007D17 File Offset: 0x00005F17
			public DefaultRegion(string tagID, VisibilityMode visibilityMode, AnimationScriptableBase scriptable, string[] tagWords)
			{
				this.tagWords = tagWords;
				this.region = new AnimationRegion(tagID, visibilityMode, scriptable);
			}

			// Token: 0x04000135 RID: 309
			public string[] tagWords;

			// Token: 0x04000136 RID: 310
			public AnimationRegion region;
		}

		// Token: 0x02000058 RID: 88
		public enum DefaultTagsMode
		{
			// Token: 0x04000138 RID: 312
			Fallback,
			// Token: 0x04000139 RID: 313
			Constant
		}

		// Token: 0x02000059 RID: 89
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <>c__DisplayClass115_0
		{
			// Token: 0x0400013A RID: 314
			public int i;
		}
	}
}
