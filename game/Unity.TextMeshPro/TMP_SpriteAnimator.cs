using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace TMPro
{
	// Token: 0x02000052 RID: 82
	[DisallowMultipleComponent]
	public class TMP_SpriteAnimator : MonoBehaviour
	{
		// Token: 0x0600039C RID: 924 RVA: 0x00025DD6 File Offset: 0x00023FD6
		private void Awake()
		{
			this.m_TextComponent = base.GetComponent<TMP_Text>();
		}

		// Token: 0x0600039D RID: 925 RVA: 0x00025DE4 File Offset: 0x00023FE4
		private void OnEnable()
		{
		}

		// Token: 0x0600039E RID: 926 RVA: 0x00025DE6 File Offset: 0x00023FE6
		private void OnDisable()
		{
		}

		// Token: 0x0600039F RID: 927 RVA: 0x00025DE8 File Offset: 0x00023FE8
		public void StopAllAnimations()
		{
			base.StopAllCoroutines();
			this.m_animations.Clear();
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x00025DFC File Offset: 0x00023FFC
		public void DoSpriteAnimation(int currentCharacter, TMP_SpriteAsset spriteAsset, int start, int end, int framerate)
		{
			bool flag;
			if (!this.m_animations.TryGetValue(currentCharacter, out flag))
			{
				base.StartCoroutine(this.DoSpriteAnimationInternal(currentCharacter, spriteAsset, start, end, framerate));
				this.m_animations.Add(currentCharacter, true);
			}
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x00025E3A File Offset: 0x0002403A
		private IEnumerator DoSpriteAnimationInternal(int currentCharacter, TMP_SpriteAsset spriteAsset, int start, int end, int framerate)
		{
			if (this.m_TextComponent == null)
			{
				yield break;
			}
			yield return null;
			int currentFrame = start;
			if (end > spriteAsset.spriteCharacterTable.Count)
			{
				end = spriteAsset.spriteCharacterTable.Count - 1;
			}
			TMP_CharacterInfo charInfo = this.m_TextComponent.textInfo.characterInfo[currentCharacter];
			int materialIndex = charInfo.materialReferenceIndex;
			int vertexIndex = charInfo.vertexIndex;
			TMP_MeshInfo meshInfo = this.m_TextComponent.textInfo.meshInfo[materialIndex];
			float baseSpriteScale = spriteAsset.spriteCharacterTable[start].scale * spriteAsset.spriteCharacterTable[start].glyph.scale;
			float elapsedTime = 0f;
			float targetTime = 1f / (float)Mathf.Abs(framerate);
			for (;;)
			{
				if (elapsedTime > targetTime)
				{
					elapsedTime = 0f;
					char character = this.m_TextComponent.textInfo.characterInfo[currentCharacter].character;
					if (character == '\u0003' || character == '…')
					{
						break;
					}
					TMP_SpriteCharacter tmp_SpriteCharacter = spriteAsset.spriteCharacterTable[currentFrame];
					Vector3[] vertices = meshInfo.vertices;
					Vector2 vector = new Vector2(charInfo.origin, charInfo.baseLine);
					float num = charInfo.scale / baseSpriteScale * tmp_SpriteCharacter.scale * tmp_SpriteCharacter.glyph.scale;
					Vector3 vector2 = new Vector3(vector.x + tmp_SpriteCharacter.glyph.metrics.horizontalBearingX * num, vector.y + (tmp_SpriteCharacter.glyph.metrics.horizontalBearingY - tmp_SpriteCharacter.glyph.metrics.height) * num);
					Vector3 vector3 = new Vector3(vector2.x, vector.y + tmp_SpriteCharacter.glyph.metrics.horizontalBearingY * num);
					Vector3 vector4 = new Vector3(vector.x + (tmp_SpriteCharacter.glyph.metrics.horizontalBearingX + tmp_SpriteCharacter.glyph.metrics.width) * num, vector3.y);
					Vector3 vector5 = new Vector3(vector4.x, vector2.y);
					vertices[vertexIndex] = vector2;
					vertices[vertexIndex + 1] = vector3;
					vertices[vertexIndex + 2] = vector4;
					vertices[vertexIndex + 3] = vector5;
					Vector2[] uvs = meshInfo.uvs0;
					Vector2 vector6 = new Vector2((float)tmp_SpriteCharacter.glyph.glyphRect.x / (float)spriteAsset.spriteSheet.width, (float)tmp_SpriteCharacter.glyph.glyphRect.y / (float)spriteAsset.spriteSheet.height);
					Vector2 vector7 = new Vector2(vector6.x, (float)(tmp_SpriteCharacter.glyph.glyphRect.y + tmp_SpriteCharacter.glyph.glyphRect.height) / (float)spriteAsset.spriteSheet.height);
					Vector2 vector8 = new Vector2((float)(tmp_SpriteCharacter.glyph.glyphRect.x + tmp_SpriteCharacter.glyph.glyphRect.width) / (float)spriteAsset.spriteSheet.width, vector7.y);
					Vector2 vector9 = new Vector2(vector8.x, vector6.y);
					uvs[vertexIndex] = vector6;
					uvs[vertexIndex + 1] = vector7;
					uvs[vertexIndex + 2] = vector8;
					uvs[vertexIndex + 3] = vector9;
					meshInfo.mesh.vertices = vertices;
					meshInfo.mesh.uv = uvs;
					this.m_TextComponent.UpdateGeometry(meshInfo.mesh, materialIndex);
					if (framerate > 0)
					{
						if (currentFrame < end)
						{
							currentFrame++;
						}
						else
						{
							currentFrame = start;
						}
					}
					else if (currentFrame > start)
					{
						currentFrame--;
					}
					else
					{
						currentFrame = end;
					}
				}
				elapsedTime += Time.deltaTime;
				yield return null;
			}
			this.m_animations.Remove(currentCharacter);
			yield break;
			yield break;
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x00025E6E File Offset: 0x0002406E
		public TMP_SpriteAnimator()
		{
		}

		// Token: 0x04000398 RID: 920
		private Dictionary<int, bool> m_animations = new Dictionary<int, bool>(16);

		// Token: 0x04000399 RID: 921
		private TMP_Text m_TextComponent;

		// Token: 0x0200009F RID: 159
		[CompilerGenerated]
		private sealed class <DoSpriteAnimationInternal>d__7 : IEnumerator<object>, IEnumerator, IDisposable
		{
			// Token: 0x06000639 RID: 1593 RVA: 0x00038B0F File Offset: 0x00036D0F
			[DebuggerHidden]
			public <DoSpriteAnimationInternal>d__7(int <>1__state)
			{
				this.<>1__state = <>1__state;
			}

			// Token: 0x0600063A RID: 1594 RVA: 0x00038B1E File Offset: 0x00036D1E
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x0600063B RID: 1595 RVA: 0x00038B20 File Offset: 0x00036D20
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				TMP_SpriteAnimator tmp_SpriteAnimator = this;
				switch (num)
				{
				case 0:
					this.<>1__state = -1;
					if (tmp_SpriteAnimator.m_TextComponent == null)
					{
						return false;
					}
					this.<>2__current = null;
					this.<>1__state = 1;
					return true;
				case 1:
					this.<>1__state = -1;
					currentFrame = start;
					if (end > spriteAsset.spriteCharacterTable.Count)
					{
						end = spriteAsset.spriteCharacterTable.Count - 1;
					}
					charInfo = tmp_SpriteAnimator.m_TextComponent.textInfo.characterInfo[currentCharacter];
					materialIndex = charInfo.materialReferenceIndex;
					vertexIndex = charInfo.vertexIndex;
					meshInfo = tmp_SpriteAnimator.m_TextComponent.textInfo.meshInfo[materialIndex];
					baseSpriteScale = spriteAsset.spriteCharacterTable[start].scale * spriteAsset.spriteCharacterTable[start].glyph.scale;
					elapsedTime = 0f;
					targetTime = 1f / (float)Mathf.Abs(framerate);
					break;
				case 2:
					this.<>1__state = -1;
					break;
				default:
					return false;
				}
				if (elapsedTime > targetTime)
				{
					elapsedTime = 0f;
					char character = tmp_SpriteAnimator.m_TextComponent.textInfo.characterInfo[currentCharacter].character;
					if (character == '\u0003' || character == '…')
					{
						tmp_SpriteAnimator.m_animations.Remove(currentCharacter);
						return false;
					}
					TMP_SpriteCharacter tmp_SpriteCharacter = spriteAsset.spriteCharacterTable[currentFrame];
					Vector3[] vertices = meshInfo.vertices;
					Vector2 vector = new Vector2(charInfo.origin, charInfo.baseLine);
					float num2 = charInfo.scale / baseSpriteScale * tmp_SpriteCharacter.scale * tmp_SpriteCharacter.glyph.scale;
					Vector3 vector2 = new Vector3(vector.x + tmp_SpriteCharacter.glyph.metrics.horizontalBearingX * num2, vector.y + (tmp_SpriteCharacter.glyph.metrics.horizontalBearingY - tmp_SpriteCharacter.glyph.metrics.height) * num2);
					Vector3 vector3 = new Vector3(vector2.x, vector.y + tmp_SpriteCharacter.glyph.metrics.horizontalBearingY * num2);
					Vector3 vector4 = new Vector3(vector.x + (tmp_SpriteCharacter.glyph.metrics.horizontalBearingX + tmp_SpriteCharacter.glyph.metrics.width) * num2, vector3.y);
					Vector3 vector5 = new Vector3(vector4.x, vector2.y);
					vertices[vertexIndex] = vector2;
					vertices[vertexIndex + 1] = vector3;
					vertices[vertexIndex + 2] = vector4;
					vertices[vertexIndex + 3] = vector5;
					Vector2[] uvs = meshInfo.uvs0;
					Vector2 vector6 = new Vector2((float)tmp_SpriteCharacter.glyph.glyphRect.x / (float)spriteAsset.spriteSheet.width, (float)tmp_SpriteCharacter.glyph.glyphRect.y / (float)spriteAsset.spriteSheet.height);
					Vector2 vector7 = new Vector2(vector6.x, (float)(tmp_SpriteCharacter.glyph.glyphRect.y + tmp_SpriteCharacter.glyph.glyphRect.height) / (float)spriteAsset.spriteSheet.height);
					Vector2 vector8 = new Vector2((float)(tmp_SpriteCharacter.glyph.glyphRect.x + tmp_SpriteCharacter.glyph.glyphRect.width) / (float)spriteAsset.spriteSheet.width, vector7.y);
					Vector2 vector9 = new Vector2(vector8.x, vector6.y);
					uvs[vertexIndex] = vector6;
					uvs[vertexIndex + 1] = vector7;
					uvs[vertexIndex + 2] = vector8;
					uvs[vertexIndex + 3] = vector9;
					meshInfo.mesh.vertices = vertices;
					meshInfo.mesh.uv = uvs;
					tmp_SpriteAnimator.m_TextComponent.UpdateGeometry(meshInfo.mesh, materialIndex);
					if (framerate > 0)
					{
						if (currentFrame < end)
						{
							currentFrame++;
						}
						else
						{
							currentFrame = start;
						}
					}
					else if (currentFrame > start)
					{
						currentFrame--;
					}
					else
					{
						currentFrame = end;
					}
				}
				elapsedTime += Time.deltaTime;
				this.<>2__current = null;
				this.<>1__state = 2;
				return true;
			}

			// Token: 0x1700016E RID: 366
			// (get) Token: 0x0600063C RID: 1596 RVA: 0x00039080 File Offset: 0x00037280
			object IEnumerator<object>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x0600063D RID: 1597 RVA: 0x00039088 File Offset: 0x00037288
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x1700016F RID: 367
			// (get) Token: 0x0600063E RID: 1598 RVA: 0x0003908F File Offset: 0x0003728F
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x040005E5 RID: 1509
			private int <>1__state;

			// Token: 0x040005E6 RID: 1510
			private object <>2__current;

			// Token: 0x040005E7 RID: 1511
			public TMP_SpriteAnimator <>4__this;

			// Token: 0x040005E8 RID: 1512
			public int start;

			// Token: 0x040005E9 RID: 1513
			public int end;

			// Token: 0x040005EA RID: 1514
			public TMP_SpriteAsset spriteAsset;

			// Token: 0x040005EB RID: 1515
			public int currentCharacter;

			// Token: 0x040005EC RID: 1516
			public int framerate;

			// Token: 0x040005ED RID: 1517
			private int <currentFrame>5__2;

			// Token: 0x040005EE RID: 1518
			private TMP_CharacterInfo <charInfo>5__3;

			// Token: 0x040005EF RID: 1519
			private int <materialIndex>5__4;

			// Token: 0x040005F0 RID: 1520
			private int <vertexIndex>5__5;

			// Token: 0x040005F1 RID: 1521
			private TMP_MeshInfo <meshInfo>5__6;

			// Token: 0x040005F2 RID: 1522
			private float <baseSpriteScale>5__7;

			// Token: 0x040005F3 RID: 1523
			private float <elapsedTime>5__8;

			// Token: 0x040005F4 RID: 1524
			private float <targetTime>5__9;
		}
	}
}
