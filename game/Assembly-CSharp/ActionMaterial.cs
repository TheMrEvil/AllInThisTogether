using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x02000016 RID: 22
public class ActionMaterial : MonoBehaviour
{
	// Token: 0x06000064 RID: 100 RVA: 0x00005A48 File Offset: 0x00003C48
	private void OnEnable()
	{
		if (this.mesh == null)
		{
			this.mesh = base.GetComponent<MeshRenderer>();
		}
		if (this.mesh == null)
		{
			this.sm = base.GetComponent<SkinnedMeshRenderer>();
		}
		if (this.AutoIn)
		{
			base.StartCoroutine(this.AutoRoutine());
			return;
		}
		if (this.StartOff)
		{
			base.StartCoroutine(this.AutoRoutine());
			this.Exit();
		}
	}

	// Token: 0x06000065 RID: 101 RVA: 0x00005ABA File Offset: 0x00003CBA
	private IEnumerator AutoRoutine()
	{
		this.Enter();
		for (;;)
		{
			yield return true;
			this.TickUpdate();
		}
		yield break;
	}

	// Token: 0x06000066 RID: 102 RVA: 0x00005ACC File Offset: 0x00003CCC
	public void Enter()
	{
		this._propBlock = new MaterialPropertyBlock();
		if (this.UseTint)
		{
			this.GetColorProp();
			Color value = (this.mesh == null) ? this.sm.material.GetColor(this.ColorProp) : this.mesh.material.GetColor(this.ColorProp);
			if (!this.SkipIn)
			{
				value.a = 0f;
			}
			this._propBlock.SetColor(this.ColorProp, value);
		}
		if (this.UseGlow)
		{
			Color value2 = (this.mesh == null) ? this.sm.material.GetColor(ActionMaterial.GlowColor) : this.mesh.material.GetColor(ActionMaterial.GlowColor);
			if (!this.SkipIn)
			{
				value2.a = 0f;
			}
			this._propBlock.SetColor(ActionMaterial.GlowColor, value2);
		}
		if (this.UseDissolve)
		{
			this._propBlock.SetFloat(ActionMaterial.DissolveProp, (float)(this.SkipIn ? 0 : 1));
		}
		if (this.UseOpacity)
		{
			this._propBlock.SetFloat(ActionMaterial.OpacityProp, (float)(this.SkipIn ? 1 : 0));
		}
		if (this.mesh != null)
		{
			this.mesh.SetPropertyBlock(this._propBlock);
		}
		else if (this.sm != null)
		{
			this.sm.SetPropertyBlock(this._propBlock);
		}
		this.isEntering = true;
	}

	// Token: 0x06000067 RID: 103 RVA: 0x00005C4E File Offset: 0x00003E4E
	public void Exit()
	{
		this.isEntering = false;
		this.isExiting = true;
		if (this.UseTint)
		{
			this.GetColorProp();
		}
	}

	// Token: 0x06000068 RID: 104 RVA: 0x00005C6C File Offset: 0x00003E6C
	public void InstantOn()
	{
		this.isEntering = false;
		this.isExiting = false;
		if (this.UseTint)
		{
			Color color = this._propBlock.GetColor(this.ColorProp);
			color.a = 1f;
			this._propBlock.SetColor(this.ColorProp, color);
		}
		if (this.UseGlow)
		{
			Color color2 = this._propBlock.GetColor(ActionMaterial.GlowColor);
			color2.a = 1f;
			this._propBlock.SetColor(ActionMaterial.GlowColor, color2);
		}
		if (this.UseDissolve)
		{
			this._propBlock.SetFloat(ActionMaterial.DissolveProp, 0f);
		}
		if (this.UseOpacity)
		{
			this._propBlock.SetFloat(ActionMaterial.OpacityProp, 1f);
		}
		if (this.mesh != null)
		{
			this.mesh.SetPropertyBlock(this._propBlock);
			return;
		}
		if (this.sm != null)
		{
			this.sm.SetPropertyBlock(this._propBlock);
		}
	}

	// Token: 0x06000069 RID: 105 RVA: 0x00005D70 File Offset: 0x00003F70
	public void InstantOff()
	{
		this.isEntering = false;
		this.isExiting = false;
		if (this.UseTint)
		{
			Color color = this._propBlock.GetColor(this.ColorProp);
			color.a = 0f;
			this._propBlock.SetColor(this.ColorProp, color);
		}
		if (this.UseGlow)
		{
			Color color2 = this._propBlock.GetColor(ActionMaterial.GlowColor);
			color2.a = 0f;
			this._propBlock.SetColor(ActionMaterial.GlowColor, color2);
		}
		if (this.UseDissolve)
		{
			this._propBlock.SetFloat(ActionMaterial.DissolveProp, 1f);
		}
		if (this.UseOpacity)
		{
			this._propBlock.SetFloat(ActionMaterial.OpacityProp, 0f);
		}
		if (this.mesh != null)
		{
			this.mesh.SetPropertyBlock(this._propBlock);
			return;
		}
		if (this.sm != null)
		{
			this.sm.SetPropertyBlock(this._propBlock);
		}
	}

	// Token: 0x0600006A RID: 106 RVA: 0x00005E72 File Offset: 0x00004072
	public void TickUpdate()
	{
		if (this.mesh == null && this.sm == null)
		{
			return;
		}
		if (this.isExiting)
		{
			this.TickExit();
			return;
		}
		if (this.isEntering)
		{
			this.TickEnter();
		}
	}

	// Token: 0x0600006B RID: 107 RVA: 0x00005EB0 File Offset: 0x000040B0
	private void TickEnter()
	{
		bool flag = true;
		if (this.UseTint)
		{
			Color color = this._propBlock.GetColor(this.ColorProp);
			color.a = Mathf.Lerp(color.a, 1f, this.TintSpeed * Time.deltaTime);
			this._propBlock.SetColor(this.ColorProp, color);
			if (color.a < 1f)
			{
				flag = false;
			}
		}
		if (this.UseGlow)
		{
			Color color2 = this._propBlock.GetColor(ActionMaterial.GlowColor);
			color2.a = Mathf.Lerp(color2.a, 1f, this.GlowSpeed * Time.deltaTime);
			this._propBlock.SetColor(ActionMaterial.GlowColor, color2);
			if (color2.a < 1f)
			{
				flag = false;
			}
		}
		if (this.UseDissolve)
		{
			float num = this._propBlock.GetFloat(ActionMaterial.DissolveProp);
			num = Mathf.Max(num - Time.deltaTime / Mathf.Max(this.DissolveSpeed, 0.01f), 0f);
			this._propBlock.SetFloat(ActionMaterial.DissolveProp, num);
			if (num > 0f)
			{
				flag = false;
			}
		}
		if (this.UseOpacity)
		{
			float num2 = this._propBlock.GetFloat(ActionMaterial.OpacityProp);
			num2 = Mathf.Min(num2 + Time.deltaTime / Mathf.Max(this.OpacityDuration, 0.01f), 1f);
			this._propBlock.SetFloat(ActionMaterial.OpacityProp, num2);
			if (num2 < 1f)
			{
				flag = false;
			}
		}
		if (this.mesh != null)
		{
			this.mesh.SetPropertyBlock(this._propBlock);
		}
		else if (this.sm != null)
		{
			this.sm.SetPropertyBlock(this._propBlock);
		}
		if (flag)
		{
			this.isEntering = false;
		}
	}

	// Token: 0x0600006C RID: 108 RVA: 0x00006078 File Offset: 0x00004278
	private void TickExit()
	{
		bool flag = true;
		if (this.UseTint)
		{
			Color color = this._propBlock.GetColor(this.ColorProp);
			color.a = Mathf.Lerp(color.a, 0f, this.TintSpeed * Time.deltaTime);
			this._propBlock.SetColor(this.ColorProp, color);
			if (color.a > Mathf.Epsilon)
			{
				flag = false;
			}
		}
		if (this.UseGlow)
		{
			Color color2 = this._propBlock.GetColor(ActionMaterial.GlowColor);
			color2.a = Mathf.Lerp(color2.a, 0f, this.GlowSpeed * Time.deltaTime);
			this._propBlock.SetColor(ActionMaterial.GlowColor, color2);
			if (color2.a > Mathf.Epsilon)
			{
				flag = false;
			}
		}
		if (this.UseDissolve)
		{
			float num = this._propBlock.GetFloat(ActionMaterial.DissolveProp);
			num = Mathf.Min(num + Time.deltaTime / Mathf.Max(this.DissolveSpeed, 0.01f), 1f);
			this._propBlock.SetFloat(ActionMaterial.DissolveProp, num);
			if (num < 1f)
			{
				flag = false;
			}
		}
		if (this.UseOpacity)
		{
			float num2 = this._propBlock.GetFloat(ActionMaterial.OpacityProp);
			num2 = Mathf.Max(num2 - Time.deltaTime / Mathf.Max(this.OpacityDuration, 0.01f), 0f);
			this._propBlock.SetFloat(ActionMaterial.OpacityProp, num2);
			if (num2 > 0f)
			{
				flag = false;
			}
		}
		if (this.mesh != null)
		{
			this.mesh.SetPropertyBlock(this._propBlock);
		}
		else if (this.sm != null)
		{
			this.sm.SetPropertyBlock(this._propBlock);
		}
		if (flag)
		{
			this.isExiting = false;
		}
	}

	// Token: 0x0600006D RID: 109 RVA: 0x00006240 File Offset: 0x00004440
	private void GetColorProp()
	{
		if (this.mesh != null)
		{
			if (this.mesh.material.HasProperty(ActionMaterial.TintColor))
			{
				this.ColorProp = ActionMaterial.TintColor;
				return;
			}
			if (this.mesh.material.HasProperty(ActionMaterial.BaseColor))
			{
				this.ColorProp = ActionMaterial.BaseColor;
				return;
			}
			if (this.mesh.material.HasProperty(ActionMaterial.RimColor))
			{
				this.ColorProp = ActionMaterial.RimColor;
				return;
			}
		}
		else if (this.sm != null)
		{
			if (this.sm.material.HasProperty(ActionMaterial.TintColor))
			{
				this.ColorProp = ActionMaterial.TintColor;
				return;
			}
			if (this.sm.material.HasProperty(ActionMaterial.BaseColor))
			{
				this.ColorProp = ActionMaterial.BaseColor;
				return;
			}
			if (this.sm.material.HasProperty(ActionMaterial.RimColor))
			{
				this.ColorProp = ActionMaterial.RimColor;
			}
		}
	}

	// Token: 0x0600006E RID: 110 RVA: 0x0000633D File Offset: 0x0000453D
	public ActionMaterial()
	{
	}

	// Token: 0x0600006F RID: 111 RVA: 0x00006374 File Offset: 0x00004574
	// Note: this type is marked as 'beforefieldinit'.
	static ActionMaterial()
	{
	}

	// Token: 0x0400005C RID: 92
	public MeshRenderer mesh;

	// Token: 0x0400005D RID: 93
	private SkinnedMeshRenderer sm;

	// Token: 0x0400005E RID: 94
	public bool SkipIn;

	// Token: 0x0400005F RID: 95
	public bool AutoIn;

	// Token: 0x04000060 RID: 96
	public bool StartOff;

	// Token: 0x04000061 RID: 97
	public bool UseTint;

	// Token: 0x04000062 RID: 98
	public float TintSpeed = 2f;

	// Token: 0x04000063 RID: 99
	private static readonly int TintColor = Shader.PropertyToID("_TintColor");

	// Token: 0x04000064 RID: 100
	private static readonly int BaseColor = Shader.PropertyToID("_Color");

	// Token: 0x04000065 RID: 101
	private static readonly int RimColor = Shader.PropertyToID("_RimColor");

	// Token: 0x04000066 RID: 102
	private int ColorProp;

	// Token: 0x04000067 RID: 103
	public bool UseGlow;

	// Token: 0x04000068 RID: 104
	public float GlowSpeed = 2f;

	// Token: 0x04000069 RID: 105
	private static readonly int GlowColor = Shader.PropertyToID("_GlowColor");

	// Token: 0x0400006A RID: 106
	public bool UseDissolve;

	// Token: 0x0400006B RID: 107
	public float DissolveSpeed = 2f;

	// Token: 0x0400006C RID: 108
	private static readonly int DissolveProp = Shader.PropertyToID("_DissolveAmount");

	// Token: 0x0400006D RID: 109
	public bool UseOpacity;

	// Token: 0x0400006E RID: 110
	public float OpacityDuration = 2f;

	// Token: 0x0400006F RID: 111
	private static readonly int OpacityProp = Shader.PropertyToID("_Opacity");

	// Token: 0x04000070 RID: 112
	private MaterialPropertyBlock _propBlock;

	// Token: 0x04000071 RID: 113
	private bool isEntering;

	// Token: 0x04000072 RID: 114
	private bool isExiting;

	// Token: 0x04000073 RID: 115
	public bool DebugTick;

	// Token: 0x020003E3 RID: 995
	[CompilerGenerated]
	private sealed class <AutoRoutine>d__24 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x06002040 RID: 8256 RVA: 0x000BFD5F File Offset: 0x000BDF5F
		[DebuggerHidden]
		public <AutoRoutine>d__24(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x06002041 RID: 8257 RVA: 0x000BFD6E File Offset: 0x000BDF6E
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x06002042 RID: 8258 RVA: 0x000BFD70 File Offset: 0x000BDF70
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			ActionMaterial actionMaterial = this;
			if (num != 0)
			{
				if (num != 1)
				{
					return false;
				}
				this.<>1__state = -1;
				actionMaterial.TickUpdate();
			}
			else
			{
				this.<>1__state = -1;
				actionMaterial.Enter();
			}
			this.<>2__current = true;
			this.<>1__state = 1;
			return true;
		}

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x06002043 RID: 8259 RVA: 0x000BFDC4 File Offset: 0x000BDFC4
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x06002044 RID: 8260 RVA: 0x000BFDCC File Offset: 0x000BDFCC
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x06002045 RID: 8261 RVA: 0x000BFDD3 File Offset: 0x000BDFD3
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x040020C0 RID: 8384
		private int <>1__state;

		// Token: 0x040020C1 RID: 8385
		private object <>2__current;

		// Token: 0x040020C2 RID: 8386
		public ActionMaterial <>4__this;
	}
}
