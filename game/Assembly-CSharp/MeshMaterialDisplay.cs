using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x02000253 RID: 595
public class MeshMaterialDisplay : MonoBehaviour
{
	// Token: 0x06001800 RID: 6144 RVA: 0x00096180 File Offset: 0x00094380
	public static MeshMaterialDisplay CreateDisplay(Renderer copyFrom, Material material, float fadeInTime, bool hideBase, float scaleFactor = 1f)
	{
		if (copyFrom == null || material == null)
		{
			return null;
		}
		GameObject gameObject = new GameObject("_MeshMaterial_" + copyFrom.name);
		gameObject.transform.SetParent(copyFrom.transform.parent);
		gameObject.transform.localPosition = copyFrom.transform.localPosition;
		gameObject.transform.localRotation = copyFrom.transform.localRotation;
		gameObject.transform.localScale = copyFrom.transform.localScale;
		Renderer renderer = null;
		if (copyFrom is SkinnedMeshRenderer)
		{
			renderer = gameObject.AddComponent(copyFrom as SkinnedMeshRenderer);
		}
		else if (copyFrom is MeshRenderer)
		{
			renderer = gameObject.AddComponent(copyFrom as MeshRenderer);
			gameObject.AddComponent(copyFrom.gameObject.GetComponent<MeshFilter>());
			LockFollow lockFollow = gameObject.AddComponent<LockFollow>();
			lockFollow.UpdateType = ChestFollow.UpdateType.LateUpdate;
			lockFollow.lockRotation = true;
			lockFollow.followObj = copyFrom.transform;
		}
		int limit = copyFrom.sharedMaterials.Length;
		MeshMaterialLimit component = copyFrom.GetComponent<MeshMaterialLimit>();
		if (component != null)
		{
			limit = component.Count;
		}
		MeshMaterialDisplay meshMaterialDisplay = renderer.gameObject.AddComponent<MeshMaterialDisplay>();
		meshMaterialDisplay.Setup(renderer, material, limit, fadeInTime, hideBase, scaleFactor);
		return meshMaterialDisplay;
	}

	// Token: 0x06001801 RID: 6145 RVA: 0x000962A8 File Offset: 0x000944A8
	private void Setup(Renderer rend, Material material, int limit, float fadeTime, bool hideBase, float scaleFactor)
	{
		this.Material = material;
		this.Render = rend;
		this.baseHidden = hideBase;
		this.mInternal = new Material(material);
		this.mInternal.SetFloat("_ScaleFactor", scaleFactor);
		Material[] array = new Material[limit];
		for (int i = 0; i < limit; i++)
		{
			array[i] = this.mInternal;
		}
		rend.sharedMaterials = array;
		if (fadeTime > 0f)
		{
			base.StartCoroutine(this.FadeIn(this.mInternal, fadeTime));
		}
		this.entity = rend.GetComponentInParent<EntityControl>();
		if (this.entity != null)
		{
			EntityHealth health = this.entity.health;
			health.OnDie = (Action<DamageInfo>)Delegate.Combine(health.OnDie, new Action<DamageInfo>(this.OnOwnerDied));
		}
	}

	// Token: 0x06001802 RID: 6146 RVA: 0x00096370 File Offset: 0x00094570
	private IEnumerator FadeIn(Material m, float overTime)
	{
		while (this.fadeT < 1f)
		{
			this.fadeT += Time.deltaTime / overTime;
			m.SetFloat(MeshMaterialDisplay.Opacity, this.fadeT);
			yield return true;
		}
		m.SetFloat(MeshMaterialDisplay.Opacity, 1f);
		yield break;
	}

	// Token: 0x06001803 RID: 6147 RVA: 0x0009638D File Offset: 0x0009458D
	public void Modify(string property, float value)
	{
		this.mInternal.SetFloat(property, value);
	}

	// Token: 0x06001804 RID: 6148 RVA: 0x0009639C File Offset: 0x0009459C
	private void OnOwnerDied(DamageInfo dmg)
	{
		this.RemoveEffect();
	}

	// Token: 0x06001805 RID: 6149 RVA: 0x000963A4 File Offset: 0x000945A4
	public void RemoveEffect()
	{
		this.RemoveEffect(0f);
	}

	// Token: 0x06001806 RID: 6150 RVA: 0x000963B4 File Offset: 0x000945B4
	public void RemoveEffect(float fadeOutTime)
	{
		if (fadeOutTime <= 0f)
		{
			if (this != null && base.gameObject != null)
			{
				UnityEngine.Object.Destroy(base.gameObject);
			}
			return;
		}
		try
		{
			base.StopAllCoroutines();
			base.StartCoroutine(this.FadeOut(this.mInternal, fadeOutTime));
		}
		catch (Exception)
		{
		}
	}

	// Token: 0x06001807 RID: 6151 RVA: 0x0009641C File Offset: 0x0009461C
	private IEnumerator FadeOut(Material m, float overTime)
	{
		while (this.fadeT > 0f)
		{
			this.fadeT -= Time.deltaTime / overTime;
			m.SetFloat(MeshMaterialDisplay.Opacity, this.fadeT);
			yield return true;
		}
		UnityEngine.Object.Destroy(base.gameObject);
		yield break;
	}

	// Token: 0x06001808 RID: 6152 RVA: 0x00096439 File Offset: 0x00094639
	private void OnDestroy()
	{
		if (this.entity != null)
		{
			EntityHealth health = this.entity.health;
			health.OnDie = (Action<DamageInfo>)Delegate.Remove(health.OnDie, new Action<DamageInfo>(this.OnOwnerDied));
		}
	}

	// Token: 0x06001809 RID: 6153 RVA: 0x00096475 File Offset: 0x00094675
	public MeshMaterialDisplay()
	{
	}

	// Token: 0x0600180A RID: 6154 RVA: 0x0009647D File Offset: 0x0009467D
	// Note: this type is marked as 'beforefieldinit'.
	static MeshMaterialDisplay()
	{
	}

	// Token: 0x040017C9 RID: 6089
	public Material Material;

	// Token: 0x040017CA RID: 6090
	public Renderer Render;

	// Token: 0x040017CB RID: 6091
	private Material mInternal;

	// Token: 0x040017CC RID: 6092
	private EntityControl entity;

	// Token: 0x040017CD RID: 6093
	private static readonly int Opacity = Shader.PropertyToID("_Opacity");

	// Token: 0x040017CE RID: 6094
	private float fadeT;

	// Token: 0x040017CF RID: 6095
	[NonSerialized]
	public bool baseHidden;

	// Token: 0x02000617 RID: 1559
	[CompilerGenerated]
	private sealed class <FadeIn>d__9 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x06002737 RID: 10039 RVA: 0x000D52D8 File Offset: 0x000D34D8
		[DebuggerHidden]
		public <FadeIn>d__9(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x06002738 RID: 10040 RVA: 0x000D52E7 File Offset: 0x000D34E7
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x06002739 RID: 10041 RVA: 0x000D52EC File Offset: 0x000D34EC
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			MeshMaterialDisplay meshMaterialDisplay = this;
			if (num != 0)
			{
				if (num != 1)
				{
					return false;
				}
				this.<>1__state = -1;
			}
			else
			{
				this.<>1__state = -1;
			}
			if (meshMaterialDisplay.fadeT >= 1f)
			{
				m.SetFloat(MeshMaterialDisplay.Opacity, 1f);
				return false;
			}
			meshMaterialDisplay.fadeT += Time.deltaTime / overTime;
			m.SetFloat(MeshMaterialDisplay.Opacity, meshMaterialDisplay.fadeT);
			this.<>2__current = true;
			this.<>1__state = 1;
			return true;
		}

		// Token: 0x1700039B RID: 923
		// (get) Token: 0x0600273A RID: 10042 RVA: 0x000D5387 File Offset: 0x000D3587
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x0600273B RID: 10043 RVA: 0x000D538F File Offset: 0x000D358F
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x1700039C RID: 924
		// (get) Token: 0x0600273C RID: 10044 RVA: 0x000D5396 File Offset: 0x000D3596
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x040029C4 RID: 10692
		private int <>1__state;

		// Token: 0x040029C5 RID: 10693
		private object <>2__current;

		// Token: 0x040029C6 RID: 10694
		public MeshMaterialDisplay <>4__this;

		// Token: 0x040029C7 RID: 10695
		public float overTime;

		// Token: 0x040029C8 RID: 10696
		public Material m;
	}

	// Token: 0x02000618 RID: 1560
	[CompilerGenerated]
	private sealed class <FadeOut>d__14 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x0600273D RID: 10045 RVA: 0x000D539E File Offset: 0x000D359E
		[DebuggerHidden]
		public <FadeOut>d__14(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x0600273E RID: 10046 RVA: 0x000D53AD File Offset: 0x000D35AD
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x0600273F RID: 10047 RVA: 0x000D53B0 File Offset: 0x000D35B0
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			MeshMaterialDisplay meshMaterialDisplay = this;
			if (num != 0)
			{
				if (num != 1)
				{
					return false;
				}
				this.<>1__state = -1;
			}
			else
			{
				this.<>1__state = -1;
			}
			if (meshMaterialDisplay.fadeT <= 0f)
			{
				UnityEngine.Object.Destroy(meshMaterialDisplay.gameObject);
				return false;
			}
			meshMaterialDisplay.fadeT -= Time.deltaTime / overTime;
			m.SetFloat(MeshMaterialDisplay.Opacity, meshMaterialDisplay.fadeT);
			this.<>2__current = true;
			this.<>1__state = 1;
			return true;
		}

		// Token: 0x1700039D RID: 925
		// (get) Token: 0x06002740 RID: 10048 RVA: 0x000D5441 File Offset: 0x000D3641
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x06002741 RID: 10049 RVA: 0x000D5449 File Offset: 0x000D3649
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x1700039E RID: 926
		// (get) Token: 0x06002742 RID: 10050 RVA: 0x000D5450 File Offset: 0x000D3650
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x040029C9 RID: 10697
		private int <>1__state;

		// Token: 0x040029CA RID: 10698
		private object <>2__current;

		// Token: 0x040029CB RID: 10699
		public MeshMaterialDisplay <>4__this;

		// Token: 0x040029CC RID: 10700
		public float overTime;

		// Token: 0x040029CD RID: 10701
		public Material m;
	}
}
