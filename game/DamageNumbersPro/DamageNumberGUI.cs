using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace DamageNumbersPro
{
	// Token: 0x02000006 RID: 6
	[DisallowMultipleComponent]
	public class DamageNumberGUI : DamageNumber
	{
		// Token: 0x06000005 RID: 5 RVA: 0x000021C0 File Offset: 0x000003C0
		protected override void OnPreSpawn()
		{
			if (this.textMeshProA != null)
			{
				this.textMeshProA.enabled = (this.textMeshProB.enabled = false);
			}
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000021F8 File Offset: 0x000003F8
		protected override void OnStart()
		{
			if (this.spamGroup != "" && base.transform.parent != null)
			{
				this.spamGroup += base.transform.parent.GetInstanceID().ToString();
			}
			this.skippedFrames = 0;
			this.skipFrames = true;
			this.realStartTime = Time.unscaledTime;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x0000226C File Offset: 0x0000046C
		protected override void OnLateUpdate()
		{
			if (this.skipFrames)
			{
				base.transform.localScale = Vector3.one * 0.0001f;
				this.currentFade = 0f;
				if (this.skippedFrames > 2 && Time.unscaledTime > this.realStartTime + 0.03f)
				{
					this.skipFrames = false;
					base.transform.localScale = this.originalScale;
					this.currentFade = 0f;
				}
			}
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000022E5 File Offset: 0x000004E5
		protected override void OnStop()
		{
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000022E7 File Offset: 0x000004E7
		protected override void OnUpdate(float deltaTime)
		{
			this.skippedFrames++;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000022F7 File Offset: 0x000004F7
		protected override void OnAbsorb(float number, float newSum)
		{
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000022F9 File Offset: 0x000004F9
		protected override void OnTextUpdate()
		{
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000022FB File Offset: 0x000004FB
		public override void GetReferencesIfNecessary()
		{
			if (this.textMeshProA == null)
			{
				this.GetReferences();
			}
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002314 File Offset: 0x00000514
		public override void GetReferences()
		{
			this.baseAlpha = 0.9f;
			this.myRect = base.GetComponent<RectTransform>();
			this.transformA = base.transform.Find("TMPA");
			this.transformB = base.transform.Find("TMPB");
			this.textMeshProA = this.transformA.GetComponent<TextMeshProUGUI>();
			this.textMeshProB = this.transformB.GetComponent<TextMeshProUGUI>();
			this.textRectA = this.transformA.GetComponent<RectTransform>();
			this.textRectB = this.transformB.GetComponent<RectTransform>();
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000023A8 File Offset: 0x000005A8
		public override TMP_Text[] GetTextMeshs()
		{
			return new TMP_Text[]
			{
				this.textMeshProA,
				this.textMeshProB
			};
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000023C2 File Offset: 0x000005C2
		public override TMP_Text GetTextMesh()
		{
			return this.textMeshProA;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000023CA File Offset: 0x000005CA
		public override Material[] GetSharedMaterials()
		{
			return this.textMeshProA.fontSharedMaterials;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000023D7 File Offset: 0x000005D7
		public override Material[] GetMaterials()
		{
			return this.textMeshProA.fontMaterials;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000023E4 File Offset: 0x000005E4
		public override Material GetSharedMaterial()
		{
			return this.textMeshProA.fontSharedMaterial;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000023F1 File Offset: 0x000005F1
		public override Material GetMaterial()
		{
			return this.textMeshProA.fontMaterial;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002400 File Offset: 0x00000600
		protected override void SetTextString(string fullString)
		{
			TMP_Text tmp_Text = this.textMeshProA;
			this.textMeshProB.text = fullString;
			tmp_Text.text = fullString;
			if (!this.textMeshProA.enabled)
			{
				this.textMeshProA.enabled = (this.textMeshProB.enabled = true);
			}
			this.textMeshProA.ForceMeshUpdate(false, false);
			this.textMeshProB.ForceMeshUpdate(false, false);
			this.textMeshProA.canvasRenderer.SetMesh(this.textMeshProA.mesh);
			this.textMeshProB.canvasRenderer.SetMesh(this.textMeshProB.mesh);
			this.meshs = new List<Mesh>();
			this.meshs.Add(this.textMeshProA.mesh);
			this.meshs.Add(this.textMeshProB.mesh);
			this.subMeshs = new List<TMP_SubMeshUI>();
			foreach (TMP_SubMeshUI tmp_SubMeshUI in this.textMeshProA.GetComponentsInChildren<TMP_SubMeshUI>())
			{
				this.subMeshs.Add(tmp_SubMeshUI);
				this.meshs.Add(tmp_SubMeshUI.mesh);
			}
			foreach (TMP_SubMeshUI tmp_SubMeshUI2 in this.textMeshProB.GetComponentsInChildren<TMP_SubMeshUI>())
			{
				this.subMeshs.Add(tmp_SubMeshUI2);
				this.meshs.Add(tmp_SubMeshUI2.mesh);
			}
		}

		// Token: 0x06000015 RID: 21 RVA: 0x0000255C File Offset: 0x0000075C
		public override Vector3 GetPosition()
		{
			return this.myRect.anchoredPosition3D;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x0000256C File Offset: 0x0000076C
		public override void SetPosition(Vector3 newPosition)
		{
			this.GetReferencesIfNecessary();
			this.myRect.anchoredPosition3D = newPosition;
			this.position = newPosition;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002594 File Offset: 0x00000794
		public override void SetAnchoredPosition(Transform rectParent, Vector2 anchoredPosition)
		{
			Vector3 localScale = base.transform.localScale;
			this.GetReferencesIfNecessary();
			this.myRect.SetParent(rectParent, false);
			this.myRect.anchoredPosition3D = anchoredPosition;
			base.transform.localScale = localScale;
			base.transform.eulerAngles = this.textMeshProA.canvas.transform.eulerAngles;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002600 File Offset: 0x00000800
		public override void SetAnchoredPosition(Transform rectParent, Transform rectPosition, Vector2 relativeAnchoredPosition)
		{
			Vector3 localScale = base.transform.localScale;
			this.GetReferencesIfNecessary();
			this.myRect.SetParent(rectParent, false);
			this.myRect.position = rectPosition.position;
			this.myRect.anchoredPosition += relativeAnchoredPosition;
			base.transform.localScale = localScale;
			base.transform.eulerAngles = this.textMeshProA.canvas.transform.eulerAngles;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002680 File Offset: 0x00000880
		protected override void SetLocalPositionA(Vector3 localPosition)
		{
			this.textRectA.anchoredPosition = localPosition * 50f;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x0000269D File Offset: 0x0000089D
		protected override void SetLocalPositionB(Vector3 localPosition)
		{
			this.textRectB.anchoredPosition = localPosition * 50f;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000026BA File Offset: 0x000008BA
		protected override float GetPositionFactor()
		{
			return 100f;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000026C4 File Offset: 0x000008C4
		protected override void OnFade(float currentFade)
		{
			this.textMeshProA.canvasRenderer.SetMesh(this.textMeshProA.mesh);
			this.textMeshProB.canvasRenderer.SetMesh(this.textMeshProB.mesh);
			foreach (TMP_SubMeshUI tmp_SubMeshUI in this.subMeshs)
			{
				tmp_SubMeshUI.canvasRenderer.SetMesh(tmp_SubMeshUI.mesh);
			}
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002758 File Offset: 0x00000958
		protected override void UpdateRotationZ()
		{
			base.SetRotationZ(this.textMeshProA.transform);
			base.SetRotationZ(this.textMeshProB.transform);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x0000277C File Offset: 0x0000097C
		public override void CheckAndEnable3D()
		{
			this.enable3DGame = false;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002785 File Offset: 0x00000985
		public override bool IsMesh()
		{
			return false;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002788 File Offset: 0x00000988
		public override Vector3 GetUpVector()
		{
			return Vector3.up;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x0000278F File Offset: 0x0000098F
		public override Vector3 GetRightVector()
		{
			return Vector3.right;
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002796 File Offset: 0x00000996
		public override Vector3 GetFreshUpVector()
		{
			return Vector3.up;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x0000279D File Offset: 0x0000099D
		public override Vector3 GetFreshRightVector()
		{
			return Vector3.right;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000027A4 File Offset: 0x000009A4
		public DamageNumberGUI()
		{
		}

		// Token: 0x0400001A RID: 26
		private RectTransform myRect;

		// Token: 0x0400001B RID: 27
		private TextMeshProUGUI textMeshProA;

		// Token: 0x0400001C RID: 28
		private TextMeshProUGUI textMeshProB;

		// Token: 0x0400001D RID: 29
		private RectTransform textRectA;

		// Token: 0x0400001E RID: 30
		private RectTransform textRectB;

		// Token: 0x0400001F RID: 31
		private List<TMP_SubMeshUI> subMeshs;

		// Token: 0x04000020 RID: 32
		private float realStartTime;

		// Token: 0x04000021 RID: 33
		private bool skipFrames;

		// Token: 0x04000022 RID: 34
		private int skippedFrames;
	}
}
