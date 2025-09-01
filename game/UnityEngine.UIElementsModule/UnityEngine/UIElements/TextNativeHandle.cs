using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.Collections;
using UnityEngine.Assertions;
using UnityEngine.TextCore.Text;

namespace UnityEngine.UIElements
{
	// Token: 0x020002BE RID: 702
	internal struct TextNativeHandle : ITextHandle
	{
		// Token: 0x170005C1 RID: 1473
		// (get) Token: 0x060017D6 RID: 6102 RVA: 0x000635E6 File Offset: 0x000617E6
		// (set) Token: 0x060017D7 RID: 6103 RVA: 0x000635EE File Offset: 0x000617EE
		public Vector2 MeasuredSizes
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<MeasuredSizes>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<MeasuredSizes>k__BackingField = value;
			}
		}

		// Token: 0x170005C2 RID: 1474
		// (get) Token: 0x060017D8 RID: 6104 RVA: 0x000635F7 File Offset: 0x000617F7
		// (set) Token: 0x060017D9 RID: 6105 RVA: 0x000635FF File Offset: 0x000617FF
		public Vector2 RoundedSizes
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<RoundedSizes>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<RoundedSizes>k__BackingField = value;
			}
		}

		// Token: 0x060017DA RID: 6106 RVA: 0x00063608 File Offset: 0x00061808
		public static ITextHandle New()
		{
			return new TextNativeHandle
			{
				textVertices = default(NativeArray<TextVertex>)
			};
		}

		// Token: 0x060017DB RID: 6107 RVA: 0x00063638 File Offset: 0x00061838
		public bool IsLegacy()
		{
			return true;
		}

		// Token: 0x060017DC RID: 6108 RVA: 0x00002166 File Offset: 0x00000366
		public void SetDirty()
		{
		}

		// Token: 0x060017DD RID: 6109 RVA: 0x0006364C File Offset: 0x0006184C
		ITextHandle ITextHandle.New()
		{
			return TextNativeHandle.New();
		}

		// Token: 0x060017DE RID: 6110 RVA: 0x00063664 File Offset: 0x00061864
		public float GetLineHeight(int characterIndex, MeshGenerationContextUtils.TextParams textParams, float textScaling, float pixelPerPoint)
		{
			textParams.wordWrapWidth = 0f;
			textParams.wordWrap = false;
			return this.ComputeTextHeight(textParams, textScaling);
		}

		// Token: 0x060017DF RID: 6111 RVA: 0x00063694 File Offset: 0x00061894
		public TextInfo Update(MeshGenerationContextUtils.TextParams parms, float pixelsPerPoint)
		{
			Debug.Log("TextNative Update should not be called");
			return null;
		}

		// Token: 0x060017E0 RID: 6112 RVA: 0x000636B4 File Offset: 0x000618B4
		public int VerticesCount(MeshGenerationContextUtils.TextParams parms, float pixelPerPoint)
		{
			return this.GetVertices(parms, pixelPerPoint).Length;
		}

		// Token: 0x060017E1 RID: 6113 RVA: 0x000636D8 File Offset: 0x000618D8
		public NativeArray<TextVertex> GetVertices(MeshGenerationContextUtils.TextParams parms, float scaling)
		{
			Vector2 vector = parms.rect.size;
			bool flag = Mathf.Abs(parms.rect.size.x - this.RoundedSizes.x) < 0.01f && Mathf.Abs(parms.rect.size.y - this.RoundedSizes.y) < 0.01f;
			if (flag)
			{
				vector = this.MeasuredSizes;
				parms.wordWrapWidth = vector.x;
			}
			else
			{
				this.RoundedSizes = vector;
				this.MeasuredSizes = vector;
			}
			parms.rect = new Rect(Vector2.zero, vector);
			int hashCode = parms.GetHashCode();
			bool flag2 = this.m_PreviousTextParamsHash == hashCode;
			NativeArray<TextVertex> result;
			if (flag2)
			{
				result = this.textVertices;
			}
			else
			{
				this.m_PreviousTextParamsHash = hashCode;
				TextNativeSettings textNativeSettings = MeshGenerationContextUtils.TextParams.GetTextNativeSettings(parms, scaling);
				Assert.IsNotNull<Font>(textNativeSettings.font);
				this.textVertices = TextNative.GetVertices(textNativeSettings);
				result = this.textVertices;
			}
			return result;
		}

		// Token: 0x060017E2 RID: 6114 RVA: 0x000637E4 File Offset: 0x000619E4
		public Vector2 GetCursorPosition(CursorPositionStylePainterParameters parms, float scaling)
		{
			return TextNative.GetCursorPosition(parms.GetTextNativeSettings(scaling), parms.rect, parms.cursorIndex);
		}

		// Token: 0x060017E3 RID: 6115 RVA: 0x00063810 File Offset: 0x00061A10
		public float ComputeTextWidth(MeshGenerationContextUtils.TextParams parms, float scaling)
		{
			float num = TextNative.ComputeTextWidth(MeshGenerationContextUtils.TextParams.GetTextNativeSettings(parms, scaling));
			bool flag = scaling != 1f && num != 0f;
			float result;
			if (flag)
			{
				result = num + 0.0001f;
			}
			else
			{
				result = num;
			}
			return result;
		}

		// Token: 0x060017E4 RID: 6116 RVA: 0x00063854 File Offset: 0x00061A54
		public float ComputeTextHeight(MeshGenerationContextUtils.TextParams parms, float scaling)
		{
			return TextNative.ComputeTextHeight(MeshGenerationContextUtils.TextParams.GetTextNativeSettings(parms, scaling));
		}

		// Token: 0x060017E5 RID: 6117 RVA: 0x00063874 File Offset: 0x00061A74
		public bool IsElided()
		{
			return false;
		}

		// Token: 0x04000A45 RID: 2629
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private Vector2 <MeasuredSizes>k__BackingField;

		// Token: 0x04000A46 RID: 2630
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private Vector2 <RoundedSizes>k__BackingField;

		// Token: 0x04000A47 RID: 2631
		internal NativeArray<TextVertex> textVertices;

		// Token: 0x04000A48 RID: 2632
		private int m_PreviousTextParamsHash;
	}
}
