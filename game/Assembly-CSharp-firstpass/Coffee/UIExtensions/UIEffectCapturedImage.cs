using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Coffee.UIExtensions
{
	// Token: 0x0200009D RID: 157
	[AddComponentMenu("UI/UIEffect/UIEffectCapturedImage", 200)]
	public class UIEffectCapturedImage : RawImage
	{
		// Token: 0x170000CA RID: 202
		// (get) Token: 0x060005BF RID: 1471 RVA: 0x000292CF File Offset: 0x000274CF
		// (set) Token: 0x060005C0 RID: 1472 RVA: 0x000292D7 File Offset: 0x000274D7
		[Obsolete("Use effectFactor instead (UnityUpgradable) -> effectFactor")]
		public float toneLevel
		{
			get
			{
				return this.m_EffectFactor;
			}
			set
			{
				this.m_EffectFactor = Mathf.Clamp(value, 0f, 1f);
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x060005C1 RID: 1473 RVA: 0x000292EF File Offset: 0x000274EF
		// (set) Token: 0x060005C2 RID: 1474 RVA: 0x000292F7 File Offset: 0x000274F7
		public float effectFactor
		{
			get
			{
				return this.m_EffectFactor;
			}
			set
			{
				this.m_EffectFactor = Mathf.Clamp(value, 0f, 1f);
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x060005C3 RID: 1475 RVA: 0x0002930F File Offset: 0x0002750F
		// (set) Token: 0x060005C4 RID: 1476 RVA: 0x00029317 File Offset: 0x00027517
		public float colorFactor
		{
			get
			{
				return this.m_ColorFactor;
			}
			set
			{
				this.m_ColorFactor = Mathf.Clamp(value, 0f, 1f);
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x060005C5 RID: 1477 RVA: 0x0002932F File Offset: 0x0002752F
		// (set) Token: 0x060005C6 RID: 1478 RVA: 0x00029337 File Offset: 0x00027537
		[Obsolete("Use blurFactor instead (UnityUpgradable) -> blurFactor")]
		public float blur
		{
			get
			{
				return this.m_BlurFactor;
			}
			set
			{
				this.m_BlurFactor = Mathf.Clamp(value, 0f, 4f);
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x060005C7 RID: 1479 RVA: 0x0002934F File Offset: 0x0002754F
		// (set) Token: 0x060005C8 RID: 1480 RVA: 0x00029357 File Offset: 0x00027557
		public float blurFactor
		{
			get
			{
				return this.m_BlurFactor;
			}
			set
			{
				this.m_BlurFactor = Mathf.Clamp(value, 0f, 4f);
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x060005C9 RID: 1481 RVA: 0x0002936F File Offset: 0x0002756F
		[Obsolete("Use effectMode instead (UnityUpgradable) -> effectMode")]
		public EffectMode toneMode
		{
			get
			{
				return this.m_EffectMode;
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x060005CA RID: 1482 RVA: 0x00029377 File Offset: 0x00027577
		public EffectMode effectMode
		{
			get
			{
				return this.m_EffectMode;
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x060005CB RID: 1483 RVA: 0x0002937F File Offset: 0x0002757F
		public ColorMode colorMode
		{
			get
			{
				return this.m_ColorMode;
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x060005CC RID: 1484 RVA: 0x00029387 File Offset: 0x00027587
		public BlurMode blurMode
		{
			get
			{
				return this.m_BlurMode;
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x060005CD RID: 1485 RVA: 0x0002938F File Offset: 0x0002758F
		// (set) Token: 0x060005CE RID: 1486 RVA: 0x00029397 File Offset: 0x00027597
		public Color effectColor
		{
			get
			{
				return this.m_EffectColor;
			}
			set
			{
				this.m_EffectColor = value;
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x060005CF RID: 1487 RVA: 0x000293A0 File Offset: 0x000275A0
		public virtual Material effectMaterial
		{
			get
			{
				return this.m_EffectMaterial;
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x060005D0 RID: 1488 RVA: 0x000293A8 File Offset: 0x000275A8
		// (set) Token: 0x060005D1 RID: 1489 RVA: 0x000293B0 File Offset: 0x000275B0
		public UIEffectCapturedImage.DesamplingRate desamplingRate
		{
			get
			{
				return this.m_DesamplingRate;
			}
			set
			{
				this.m_DesamplingRate = value;
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x060005D2 RID: 1490 RVA: 0x000293B9 File Offset: 0x000275B9
		// (set) Token: 0x060005D3 RID: 1491 RVA: 0x000293C1 File Offset: 0x000275C1
		public UIEffectCapturedImage.DesamplingRate reductionRate
		{
			get
			{
				return this.m_ReductionRate;
			}
			set
			{
				this.m_ReductionRate = value;
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x060005D4 RID: 1492 RVA: 0x000293CA File Offset: 0x000275CA
		// (set) Token: 0x060005D5 RID: 1493 RVA: 0x000293D2 File Offset: 0x000275D2
		public FilterMode filterMode
		{
			get
			{
				return this.m_FilterMode;
			}
			set
			{
				this.m_FilterMode = value;
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x060005D6 RID: 1494 RVA: 0x000293DB File Offset: 0x000275DB
		public RenderTexture capturedTexture
		{
			get
			{
				return this._rt;
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x060005D7 RID: 1495 RVA: 0x000293E3 File Offset: 0x000275E3
		// (set) Token: 0x060005D8 RID: 1496 RVA: 0x000293EB File Offset: 0x000275EB
		[Obsolete("Use blurIterations instead (UnityUpgradable) -> blurIterations")]
		public int iterations
		{
			get
			{
				return this.m_BlurIterations;
			}
			set
			{
				this.m_BlurIterations = value;
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x060005D9 RID: 1497 RVA: 0x000293F4 File Offset: 0x000275F4
		// (set) Token: 0x060005DA RID: 1498 RVA: 0x000293FC File Offset: 0x000275FC
		public int blurIterations
		{
			get
			{
				return this.m_BlurIterations;
			}
			set
			{
				this.m_BlurIterations = value;
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x060005DB RID: 1499 RVA: 0x00029405 File Offset: 0x00027605
		// (set) Token: 0x060005DC RID: 1500 RVA: 0x0002940D File Offset: 0x0002760D
		[Obsolete("Use fitToScreen instead (UnityUpgradable) -> fitToScreen")]
		public bool keepCanvasSize
		{
			get
			{
				return this.m_FitToScreen;
			}
			set
			{
				this.m_FitToScreen = value;
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x060005DD RID: 1501 RVA: 0x00029416 File Offset: 0x00027616
		// (set) Token: 0x060005DE RID: 1502 RVA: 0x0002941E File Offset: 0x0002761E
		public bool fitToScreen
		{
			get
			{
				return this.m_FitToScreen;
			}
			set
			{
				this.m_FitToScreen = value;
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x060005DF RID: 1503 RVA: 0x00029427 File Offset: 0x00027627
		// (set) Token: 0x060005E0 RID: 1504 RVA: 0x0002942A File Offset: 0x0002762A
		[Obsolete]
		public RenderTexture targetTexture
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x060005E1 RID: 1505 RVA: 0x0002942C File Offset: 0x0002762C
		// (set) Token: 0x060005E2 RID: 1506 RVA: 0x00029434 File Offset: 0x00027634
		public bool captureOnEnable
		{
			get
			{
				return this.m_CaptureOnEnable;
			}
			set
			{
				this.m_CaptureOnEnable = value;
			}
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x060005E3 RID: 1507 RVA: 0x0002943D File Offset: 0x0002763D
		// (set) Token: 0x060005E4 RID: 1508 RVA: 0x00029445 File Offset: 0x00027645
		public bool immediateCapturing
		{
			get
			{
				return this.m_ImmediateCapturing;
			}
			set
			{
				this.m_ImmediateCapturing = value;
			}
		}

		// Token: 0x060005E5 RID: 1509 RVA: 0x0002944E File Offset: 0x0002764E
		protected override void OnEnable()
		{
			base.OnEnable();
			if (this.m_CaptureOnEnable && Application.isPlaying)
			{
				this.Capture();
			}
		}

		// Token: 0x060005E6 RID: 1510 RVA: 0x0002946B File Offset: 0x0002766B
		protected override void OnDisable()
		{
			base.OnDisable();
			if (this.m_CaptureOnEnable && Application.isPlaying)
			{
				this._Release(false);
				base.texture = null;
			}
		}

		// Token: 0x060005E7 RID: 1511 RVA: 0x00029490 File Offset: 0x00027690
		protected override void OnDestroy()
		{
			this.Release();
			base.OnDestroy();
		}

		// Token: 0x060005E8 RID: 1512 RVA: 0x000294A0 File Offset: 0x000276A0
		protected override void OnPopulateMesh(VertexHelper vh)
		{
			if (base.texture == null || this.color.a < 0.003921569f || base.canvasRenderer.GetAlpha() < 0.003921569f)
			{
				vh.Clear();
				return;
			}
			base.OnPopulateMesh(vh);
			int currentVertCount = vh.currentVertCount;
			UIVertex vertex = default(UIVertex);
			Color c = new Color(1f, 1f, 1f, this.color.a);
			for (int i = 0; i < currentVertCount; i++)
			{
				vh.PopulateUIVertex(ref vertex, i);
				vertex.color = c;
				vh.SetUIVertex(vertex, i);
			}
		}

		// Token: 0x060005E9 RID: 1513 RVA: 0x00029548 File Offset: 0x00027748
		public void GetDesamplingSize(UIEffectCapturedImage.DesamplingRate rate, out int w, out int h)
		{
			w = Screen.width;
			h = Screen.height;
			if (rate == UIEffectCapturedImage.DesamplingRate.None)
			{
				return;
			}
			float num = (float)w / (float)h;
			if (w < h)
			{
				h = Mathf.ClosestPowerOfTwo(h / (int)rate);
				w = Mathf.CeilToInt((float)h * num);
				return;
			}
			w = Mathf.ClosestPowerOfTwo(w / (int)rate);
			h = Mathf.CeilToInt((float)w / num);
		}

		// Token: 0x060005EA RID: 1514 RVA: 0x000295A4 File Offset: 0x000277A4
		public void Capture()
		{
			Canvas rootCanvas = base.canvas.rootCanvas;
			if (this.m_FitToScreen)
			{
				RectTransform rectTransform = rootCanvas.transform as RectTransform;
				Vector2 size = rectTransform.rect.size;
				base.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, size.x);
				base.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, size.y);
				base.rectTransform.position = rectTransform.position;
			}
			if (UIEffectCapturedImage.s_CopyId == 0)
			{
				UIEffectCapturedImage.s_CopyId = Shader.PropertyToID("_UIEffectCapturedImage_ScreenCopyId");
				UIEffectCapturedImage.s_EffectId1 = Shader.PropertyToID("_UIEffectCapturedImage_EffectId1");
				UIEffectCapturedImage.s_EffectId2 = Shader.PropertyToID("_UIEffectCapturedImage_EffectId2");
				UIEffectCapturedImage.s_EffectFactorId = Shader.PropertyToID("_EffectFactor");
				UIEffectCapturedImage.s_ColorFactorId = Shader.PropertyToID("_ColorFactor");
				UIEffectCapturedImage.s_CommandBuffer = new CommandBuffer();
			}
			int num;
			int num2;
			this.GetDesamplingSize(this.m_DesamplingRate, out num, out num2);
			if (this._rt && (this._rt.width != num || this._rt.height != num2))
			{
				this._Release(ref this._rt);
			}
			if (this._rt == null)
			{
				this._rt = new RenderTexture(num, num2, 0, RenderTextureFormat.ARGB32, RenderTextureReadWrite.Default);
				this._rt.filterMode = this.m_FilterMode;
				this._rt.useMipMap = false;
				this._rt.wrapMode = TextureWrapMode.Clamp;
				this._rt.hideFlags = HideFlags.HideAndDontSave;
			}
			this.SetupCommandBuffer();
		}

		// Token: 0x060005EB RID: 1515 RVA: 0x00029714 File Offset: 0x00027914
		private void SetupCommandBuffer()
		{
			Material effectMaterial = this.m_EffectMaterial;
			if (UIEffectCapturedImage.s_CommandBuffer == null)
			{
				UIEffectCapturedImage.s_CommandBuffer = new CommandBuffer();
			}
			int width;
			int height;
			this.GetDesamplingSize(UIEffectCapturedImage.DesamplingRate.None, out width, out height);
			UIEffectCapturedImage.s_CommandBuffer.GetTemporaryRT(UIEffectCapturedImage.s_CopyId, width, height, 0, this.m_FilterMode);
			UIEffectCapturedImage.s_CommandBuffer.Blit(BuiltinRenderTextureType.BindableTexture, UIEffectCapturedImage.s_CopyId);
			UIEffectCapturedImage.s_CommandBuffer.SetGlobalVector(UIEffectCapturedImage.s_EffectFactorId, new Vector4(this.m_EffectFactor, 0f));
			UIEffectCapturedImage.s_CommandBuffer.SetGlobalVector(UIEffectCapturedImage.s_ColorFactorId, new Vector4(this.m_EffectColor.r, this.m_EffectColor.g, this.m_EffectColor.b, this.m_EffectColor.a));
			this.GetDesamplingSize(this.m_ReductionRate, out width, out height);
			UIEffectCapturedImage.s_CommandBuffer.GetTemporaryRT(UIEffectCapturedImage.s_EffectId1, width, height, 0, this.m_FilterMode);
			UIEffectCapturedImage.s_CommandBuffer.Blit(UIEffectCapturedImage.s_CopyId, UIEffectCapturedImage.s_EffectId1, effectMaterial, 0);
			UIEffectCapturedImage.s_CommandBuffer.ReleaseTemporaryRT(UIEffectCapturedImage.s_CopyId);
			if (this.m_BlurMode != BlurMode.None)
			{
				UIEffectCapturedImage.s_CommandBuffer.GetTemporaryRT(UIEffectCapturedImage.s_EffectId2, width, height, 0, this.m_FilterMode);
				for (int i = 0; i < this.m_BlurIterations; i++)
				{
					UIEffectCapturedImage.s_CommandBuffer.SetGlobalVector(UIEffectCapturedImage.s_EffectFactorId, new Vector4(this.m_BlurFactor, 0f));
					UIEffectCapturedImage.s_CommandBuffer.Blit(UIEffectCapturedImage.s_EffectId1, UIEffectCapturedImage.s_EffectId2, effectMaterial, 1);
					UIEffectCapturedImage.s_CommandBuffer.SetGlobalVector(UIEffectCapturedImage.s_EffectFactorId, new Vector4(0f, this.m_BlurFactor));
					UIEffectCapturedImage.s_CommandBuffer.Blit(UIEffectCapturedImage.s_EffectId2, UIEffectCapturedImage.s_EffectId1, effectMaterial, 1);
				}
				UIEffectCapturedImage.s_CommandBuffer.ReleaseTemporaryRT(UIEffectCapturedImage.s_EffectId2);
			}
			UIEffectCapturedImage.s_CommandBuffer.Blit(UIEffectCapturedImage.s_EffectId1, new RenderTargetIdentifier(this.capturedTexture));
			UIEffectCapturedImage.s_CommandBuffer.ReleaseTemporaryRT(UIEffectCapturedImage.s_EffectId1);
			if (this.m_ImmediateCapturing)
			{
				this.UpdateTexture();
				return;
			}
			base.canvas.rootCanvas.GetComponent<CanvasScaler>().StartCoroutine(this._CoUpdateTextureOnNextFrame());
		}

		// Token: 0x060005EC RID: 1516 RVA: 0x0002994C File Offset: 0x00027B4C
		public void Release()
		{
			this._Release(true);
			base.texture = null;
		}

		// Token: 0x060005ED RID: 1517 RVA: 0x0002995C File Offset: 0x00027B5C
		private void _Release(bool releaseRT)
		{
			if (releaseRT)
			{
				base.texture = null;
				this._Release(ref this._rt);
			}
			if (UIEffectCapturedImage.s_CommandBuffer != null)
			{
				UIEffectCapturedImage.s_CommandBuffer.Clear();
				if (releaseRT)
				{
					UIEffectCapturedImage.s_CommandBuffer.Release();
					UIEffectCapturedImage.s_CommandBuffer = null;
				}
			}
		}

		// Token: 0x060005EE RID: 1518 RVA: 0x00029998 File Offset: 0x00027B98
		[Conditional("UNITY_EDITOR")]
		private void _SetDirty()
		{
		}

		// Token: 0x060005EF RID: 1519 RVA: 0x0002999A File Offset: 0x00027B9A
		private void _Release(ref RenderTexture obj)
		{
			if (obj)
			{
				obj.Release();
				UnityEngine.Object.Destroy(obj);
				obj = null;
			}
		}

		// Token: 0x060005F0 RID: 1520 RVA: 0x000299B6 File Offset: 0x00027BB6
		private IEnumerator _CoUpdateTextureOnNextFrame()
		{
			yield return new WaitForEndOfFrame();
			this.UpdateTexture();
			yield break;
		}

		// Token: 0x060005F1 RID: 1521 RVA: 0x000299C5 File Offset: 0x00027BC5
		private void UpdateTexture()
		{
			Graphics.ExecuteCommandBuffer(UIEffectCapturedImage.s_CommandBuffer);
			this._Release(false);
			base.texture = this.capturedTexture;
		}

		// Token: 0x060005F2 RID: 1522 RVA: 0x000299E4 File Offset: 0x00027BE4
		public UIEffectCapturedImage()
		{
		}

		// Token: 0x04000549 RID: 1353
		public const string shaderName = "UI/Hidden/UI-EffectCapture";

		// Token: 0x0400054A RID: 1354
		[Tooltip("Effect factor between 0(no effect) and 1(complete effect).")]
		[FormerlySerializedAs("m_ToneLevel")]
		[SerializeField]
		[Range(0f, 1f)]
		private float m_EffectFactor = 1f;

		// Token: 0x0400054B RID: 1355
		[Tooltip("Color effect factor between 0(no effect) and 1(complete effect).")]
		[SerializeField]
		[Range(0f, 1f)]
		private float m_ColorFactor = 1f;

		// Token: 0x0400054C RID: 1356
		[Tooltip("How far is the blurring from the graphic.")]
		[FormerlySerializedAs("m_Blur")]
		[SerializeField]
		[Range(0f, 1f)]
		private float m_BlurFactor = 1f;

		// Token: 0x0400054D RID: 1357
		[Tooltip("Effect mode.")]
		[FormerlySerializedAs("m_ToneMode")]
		[SerializeField]
		private EffectMode m_EffectMode;

		// Token: 0x0400054E RID: 1358
		[Tooltip("Color effect mode.")]
		[SerializeField]
		private ColorMode m_ColorMode;

		// Token: 0x0400054F RID: 1359
		[Tooltip("Blur effect mode.")]
		[SerializeField]
		private BlurMode m_BlurMode = BlurMode.DetailBlur;

		// Token: 0x04000550 RID: 1360
		[Tooltip("Color for the color effect.")]
		[SerializeField]
		private Color m_EffectColor = Color.white;

		// Token: 0x04000551 RID: 1361
		[Tooltip("Desampling rate of the generated RenderTexture.")]
		[SerializeField]
		private UIEffectCapturedImage.DesamplingRate m_DesamplingRate = UIEffectCapturedImage.DesamplingRate.x1;

		// Token: 0x04000552 RID: 1362
		[Tooltip("Desampling rate of reduction buffer to apply effect.")]
		[SerializeField]
		private UIEffectCapturedImage.DesamplingRate m_ReductionRate = UIEffectCapturedImage.DesamplingRate.x1;

		// Token: 0x04000553 RID: 1363
		[Tooltip("FilterMode for capturing.")]
		[SerializeField]
		private FilterMode m_FilterMode = FilterMode.Bilinear;

		// Token: 0x04000554 RID: 1364
		[Tooltip("Effect material.")]
		[SerializeField]
		private Material m_EffectMaterial;

		// Token: 0x04000555 RID: 1365
		[Tooltip("Blur iterations.")]
		[FormerlySerializedAs("m_Iterations")]
		[SerializeField]
		[Range(1f, 8f)]
		private int m_BlurIterations = 3;

		// Token: 0x04000556 RID: 1366
		[Tooltip("Fits graphic size to screen on captured.")]
		[FormerlySerializedAs("m_KeepCanvasSize")]
		[SerializeField]
		private bool m_FitToScreen = true;

		// Token: 0x04000557 RID: 1367
		[Tooltip("Capture automatically on enable.")]
		[SerializeField]
		private bool m_CaptureOnEnable;

		// Token: 0x04000558 RID: 1368
		[Tooltip("Capture immediately.")]
		[SerializeField]
		private bool m_ImmediateCapturing = true;

		// Token: 0x04000559 RID: 1369
		private RenderTexture _rt;

		// Token: 0x0400055A RID: 1370
		private static int s_CopyId;

		// Token: 0x0400055B RID: 1371
		private static int s_EffectId1;

		// Token: 0x0400055C RID: 1372
		private static int s_EffectId2;

		// Token: 0x0400055D RID: 1373
		private static int s_EffectFactorId;

		// Token: 0x0400055E RID: 1374
		private static int s_ColorFactorId;

		// Token: 0x0400055F RID: 1375
		private static CommandBuffer s_CommandBuffer;

		// Token: 0x020001CE RID: 462
		public enum DesamplingRate
		{
			// Token: 0x04000DFC RID: 3580
			None,
			// Token: 0x04000DFD RID: 3581
			x1,
			// Token: 0x04000DFE RID: 3582
			x2,
			// Token: 0x04000DFF RID: 3583
			x4 = 4,
			// Token: 0x04000E00 RID: 3584
			x8 = 8
		}

		// Token: 0x020001CF RID: 463
		[CompilerGenerated]
		private sealed class <_CoUpdateTextureOnNextFrame>d__95 : IEnumerator<object>, IEnumerator, IDisposable
		{
			// Token: 0x06000FA6 RID: 4006 RVA: 0x00063B30 File Offset: 0x00061D30
			[DebuggerHidden]
			public <_CoUpdateTextureOnNextFrame>d__95(int <>1__state)
			{
				this.<>1__state = <>1__state;
			}

			// Token: 0x06000FA7 RID: 4007 RVA: 0x00063B3F File Offset: 0x00061D3F
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x06000FA8 RID: 4008 RVA: 0x00063B44 File Offset: 0x00061D44
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				UIEffectCapturedImage uieffectCapturedImage = this;
				if (num == 0)
				{
					this.<>1__state = -1;
					this.<>2__current = new WaitForEndOfFrame();
					this.<>1__state = 1;
					return true;
				}
				if (num != 1)
				{
					return false;
				}
				this.<>1__state = -1;
				uieffectCapturedImage.UpdateTexture();
				return false;
			}

			// Token: 0x170001ED RID: 493
			// (get) Token: 0x06000FA9 RID: 4009 RVA: 0x00063B91 File Offset: 0x00061D91
			object IEnumerator<object>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06000FAA RID: 4010 RVA: 0x00063B99 File Offset: 0x00061D99
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x170001EE RID: 494
			// (get) Token: 0x06000FAB RID: 4011 RVA: 0x00063BA0 File Offset: 0x00061DA0
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x04000E01 RID: 3585
			private int <>1__state;

			// Token: 0x04000E02 RID: 3586
			private object <>2__current;

			// Token: 0x04000E03 RID: 3587
			public UIEffectCapturedImage <>4__this;
		}
	}
}
