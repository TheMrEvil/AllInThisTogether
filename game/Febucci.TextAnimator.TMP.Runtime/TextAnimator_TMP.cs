using System;
using Febucci.UI.Core;
using Febucci.UI.Core.Parsing;
using TMPro;
using UnityEngine;

namespace Febucci.UI
{
	// Token: 0x02000002 RID: 2
	[RequireComponent(typeof(TMP_Text))]
	[AddComponentMenu("Febucci/TextAnimator/Text Animator - Text Mesh Pro")]
	public sealed class TextAnimator_TMP : TAnimCore
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public TMP_Text TMProComponent
		{
			get
			{
				if (this.tmpComponent)
				{
					return this.tmpComponent;
				}
				this.CacheComponentsOnce();
				return this.tmpComponent;
			}
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002074 File Offset: 0x00000274
		private void CacheComponentsOnce()
		{
			if (this.componentsCached)
			{
				return;
			}
			if (!base.gameObject.TryGetComponent<TMP_Text>(out this.tmpComponent))
			{
				Debug.LogError("TextAnimator_TMP " + base.name + " requires a TMP_Text component to work.", base.gameObject);
			}
			base.gameObject.TryGetComponent<TMP_InputField>(out this.attachedInputField);
			this.componentsCached = true;
		}

		// Token: 0x06000003 RID: 3 RVA: 0x000020D6 File Offset: 0x000002D6
		protected override void OnInitialized()
		{
			this.CacheComponentsOnce();
			this.tmpComponent.renderMode = TextRenderFlags.DontRender;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020EA File Offset: 0x000002EA
		protected override TagParserBase[] GetExtraParsers()
		{
			return new TagParserBase[]
			{
				new TMPTagParser(this.tmpComponent.richText, '<', '/', '>')
			};
		}

		// Token: 0x06000005 RID: 5 RVA: 0x0000210B File Offset: 0x0000030B
		public override string GetOriginalTextFromSource()
		{
			return this.TMProComponent.text;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002118 File Offset: 0x00000318
		public override string GetStrippedTextFromSource()
		{
			return this.tmpComponent.GetParsedText();
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002128 File Offset: 0x00000328
		public override void SetTextToSource(string text)
		{
			this.TMProComponent.renderMode = TextRenderFlags.DontRender;
			if (this.attachedInputField)
			{
				this.attachedInputField.text = text;
			}
			else
			{
				this.tmpComponent.text = text;
			}
			this.OnForceMeshUpdate();
			if (this.tmpComponent == null || this.tmpComponent.text == null)
			{
				return;
			}
			this.textInfo = this.tmpComponent.GetTextInfo(this.tmpComponent.text);
			this.tmpComponent.renderMode = TextRenderFlags.DontRender;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000021B2 File Offset: 0x000003B2
		protected override int GetCharactersCount()
		{
			return this.textInfo.characterCount;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000021C0 File Offset: 0x000003C0
		protected override bool HasChangedRenderingSettings()
		{
			return this.tmpComponent.havePropertiesChanged || this.tmpComponent.enableAutoSizing != this.autoSize || this.tmpComponent.rectTransform.rect != this.sourceRect || this.tmpComponent.color != this.sourceColor || this.tmpComponent.firstVisibleCharacter != this.tmpFirstVisibleCharacter || this.tmpComponent.maxVisibleCharacters != this.tmpMaxVisibleCharacters;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002250 File Offset: 0x00000450
		protected override bool HasChangedText(string strippedText)
		{
			return (!string.IsNullOrEmpty(this.tmpComponent.text) || !string.IsNullOrEmpty(strippedText)) && (string.IsNullOrEmpty(this.tmpComponent.text) != string.IsNullOrEmpty(strippedText) || !this.tmpComponent.text.Equals(strippedText));
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000022A8 File Offset: 0x000004A8
		protected override void CopyMeshFromSource(ref CharacterData[] characters)
		{
			this.autoSize = this.tmpComponent.enableAutoSizing;
			this.sourceRect = this.tmpComponent.rectTransform.rect;
			this.sourceColor = this.tmpComponent.color;
			this.tmpFirstVisibleCharacter = this.tmpComponent.firstVisibleCharacter;
			this.tmpMaxVisibleCharacters = this.tmpComponent.maxVisibleCharacters;
			int num = 0;
			while (num < this.textInfo.characterCount && num < characters.Length)
			{
				TMP_CharacterInfo tmp_CharacterInfo = this.textInfo.characterInfo[num];
				characters[num].info.isRendered = tmp_CharacterInfo.isVisible;
				characters[num].info.character = tmp_CharacterInfo.character;
				if (tmp_CharacterInfo.isVisible)
				{
					characters[num].info.pointSize = tmp_CharacterInfo.pointSize;
					for (byte b = 0; b < 4; b += 1)
					{
						characters[num].source.positions[(int)b] = this.textInfo.meshInfo[tmp_CharacterInfo.materialReferenceIndex].vertices[tmp_CharacterInfo.vertexIndex + (int)b];
					}
					for (byte b2 = 0; b2 < 4; b2 += 1)
					{
						characters[num].source.colors[(int)b2] = this.textInfo.meshInfo[tmp_CharacterInfo.materialReferenceIndex].colors32[tmp_CharacterInfo.vertexIndex + (int)b2];
					}
				}
				num++;
			}
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002434 File Offset: 0x00000634
		protected override void PasteMeshToSource(CharacterData[] characters)
		{
			int num = 0;
			while (num < this.textInfo.characterCount && num < characters.Length)
			{
				TMP_CharacterInfo tmp_CharacterInfo = this.textInfo.characterInfo[num];
				if (tmp_CharacterInfo.isVisible)
				{
					for (byte b = 0; b < 4; b += 1)
					{
						this.textInfo.meshInfo[tmp_CharacterInfo.materialReferenceIndex].vertices[tmp_CharacterInfo.vertexIndex + (int)b] = characters[num].current.positions[(int)b];
					}
					for (byte b2 = 0; b2 < 4; b2 += 1)
					{
						this.textInfo.meshInfo[tmp_CharacterInfo.materialReferenceIndex].colors32[tmp_CharacterInfo.vertexIndex + (int)b2] = characters[num].current.colors[(int)b2];
					}
				}
				num++;
			}
			if (this.tmpComponent.mesh != null)
			{
				this.tmpComponent.UpdateVertexData();
			}
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002536 File Offset: 0x00000736
		protected override void OnForceMeshUpdate()
		{
			this.tmpComponent.ForceMeshUpdate(true, false);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002548 File Offset: 0x00000748
		[Obsolete("This method is Obsolete. Please check through the 'Characters' array instead.")]
		public bool TryGetNextCharacter(out TMP_CharacterInfo result)
		{
			if (base.latestCharacterShown.index < base.CharactersCount - 1)
			{
				result = this.textInfo.characterInfo[base.latestCharacterShown.index + 1];
				return true;
			}
			result = default(TMP_CharacterInfo);
			return false;
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000F RID: 15 RVA: 0x00002597 File Offset: 0x00000797
		[Obsolete("Please use TMProComponent instead.")]
		public TMP_Text tmproText
		{
			get
			{
				return this.TMProComponent;
			}
		}

		// Token: 0x06000010 RID: 16 RVA: 0x0000259F File Offset: 0x0000079F
		public TextAnimator_TMP()
		{
		}

		// Token: 0x04000001 RID: 1
		private TMP_Text tmpComponent;

		// Token: 0x04000002 RID: 2
		private TMP_TextInfo textInfo;

		// Token: 0x04000003 RID: 3
		private TMP_InputField attachedInputField;

		// Token: 0x04000004 RID: 4
		private bool autoSize;

		// Token: 0x04000005 RID: 5
		private Rect sourceRect;

		// Token: 0x04000006 RID: 6
		private Color sourceColor;

		// Token: 0x04000007 RID: 7
		private int tmpFirstVisibleCharacter;

		// Token: 0x04000008 RID: 8
		private int tmpMaxVisibleCharacters;

		// Token: 0x04000009 RID: 9
		private bool componentsCached;
	}
}
