using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x02000249 RID: 585
public class CosmeticDisplay : MonoBehaviour
{
	// Token: 0x060017B6 RID: 6070 RVA: 0x00094BDC File Offset: 0x00092DDC
	private void Update()
	{
		Quaternion b = Quaternion.Euler(0f, this.targetAngle, 0f);
		base.transform.localRotation = Quaternion.Lerp(base.transform.localRotation, b, Time.deltaTime * 4f);
	}

	// Token: 0x060017B7 RID: 6071 RVA: 0x00094C28 File Offset: 0x00092E28
	public void SetPreviewTab(CosmeticType cType)
	{
		int num;
		switch (cType)
		{
		case CosmeticType.Skin:
			num = -256;
			break;
		case CosmeticType.Head:
			num = -256;
			break;
		case CosmeticType.Book:
			num = -256;
			break;
		case CosmeticType.Signature:
			num = -452;
			break;
		case CosmeticType.Emote:
			num = -256;
			break;
		default:
			num = -256;
			break;
		}
		this.targetAngle = (float)num;
	}

	// Token: 0x060017B8 RID: 6072 RVA: 0x00094C8C File Offset: 0x00092E8C
	public void SetCosmetic(Cosmetic c)
	{
		this.StopEmote();
		Cosmetic_Head cosmetic_Head = c as Cosmetic_Head;
		if (cosmetic_Head != null)
		{
			this.SetHead(cosmetic_Head);
		}
		Cosmetic_Skin cosmetic_Skin = c as Cosmetic_Skin;
		if (cosmetic_Skin != null)
		{
			this.SetSkin(cosmetic_Skin);
		}
		Cosmetic_Book cosmetic_Book = c as Cosmetic_Book;
		if (cosmetic_Book != null)
		{
			this.SetBook(cosmetic_Book);
		}
		Cosmetic_Emote cosmetic_Emote = c as Cosmetic_Emote;
		if (cosmetic_Emote != null)
		{
			this.SetEmote(cosmetic_Emote);
		}
		Cosmetic_Signature cosmetic_Signature = c as Cosmetic_Signature;
		if (cosmetic_Signature != null)
		{
			this.SetBack(cosmetic_Signature);
		}
	}

	// Token: 0x060017B9 RID: 6073 RVA: 0x00094CF8 File Offset: 0x00092EF8
	private void SetHead(Cosmetic_Head head)
	{
		if (this.curHead != null)
		{
			UnityEngine.Object.Destroy(this.curHead);
		}
		this.curHead = UnityEngine.Object.Instantiate<GameObject>(head.Prefab, this.HeadHolder);
		this.curHead.transform.localPosition = Vector3.zero;
		this.curHead.transform.localRotation = Quaternion.identity;
	}

	// Token: 0x060017BA RID: 6074 RVA: 0x00094D60 File Offset: 0x00092F60
	private void SetSkin(Cosmetic_Skin skin)
	{
		this.Coat.material = skin.Torso;
		this.Legs.material = skin.Legs;
		this.ArmL.material = skin.Arm_L;
		this.ArmR.material = skin.Arm_R;
	}

	// Token: 0x060017BB RID: 6075 RVA: 0x00094DB4 File Offset: 0x00092FB4
	private void SetBack(Cosmetic_Signature signature)
	{
		if (this.curBack != null)
		{
			UnityEngine.Object.Destroy(this.curBack);
		}
		if (signature == null || signature.Prefab == null)
		{
			return;
		}
		this.curBack = UnityEngine.Object.Instantiate<GameObject>(signature.Prefab, this.BackHolder);
		this.curBack.transform.localPosition = Vector3.zero;
		this.curBack.transform.localRotation = Quaternion.identity;
		this.curBack.transform.localScale = signature.Prefab.transform.localScale * 1f / this.BackHolder.lossyScale.x;
	}

	// Token: 0x060017BC RID: 6076 RVA: 0x00094E6C File Offset: 0x0009306C
	private void SetBook(Cosmetic_Book book)
	{
		this.Book.ChangeCosmetic(book, PlayerDB.GetCore(PlayerControl.myInstance.actions.core).BodyGlowColor);
	}

	// Token: 0x060017BD RID: 6077 RVA: 0x00094E94 File Offset: 0x00093094
	private void SetEmote(Cosmetic_Emote emote)
	{
		if (this.anim == null)
		{
			this.anim = base.GetComponent<Animator>();
		}
		base.StopAllCoroutines();
		if (!string.IsNullOrEmpty(emote.Animation))
		{
			int num = emote.UseLegs ? 3 : 4;
			int layerIndex = emote.UseLegs ? 4 : 3;
			this.anim.SetLayerWeight(layerIndex, 0f);
			this.anim.SetLayerWeight(num, 1f);
			this.anim.CrossFade(emote.Animation, 0.25f, num);
		}
	}

	// Token: 0x060017BE RID: 6078 RVA: 0x00094F24 File Offset: 0x00093124
	private void StopEmote()
	{
		if (!base.gameObject.activeSelf)
		{
			return;
		}
		if (this.anim == null)
		{
			this.anim = base.GetComponent<Animator>();
		}
		this.anim.SetTrigger("StopEmote");
		base.StartCoroutine(this.FadeEmoteOut());
	}

	// Token: 0x060017BF RID: 6079 RVA: 0x00094F76 File Offset: 0x00093176
	private IEnumerator FadeEmoteOut()
	{
		float t = 0f;
		while (t < 1f)
		{
			t += Time.deltaTime;
			this.anim.SetLayerWeight(3, Mathf.Lerp(this.anim.GetLayerWeight(3), 0f, Time.deltaTime * 4f));
			this.anim.SetLayerWeight(4, Mathf.Lerp(this.anim.GetLayerWeight(4), 0f, Time.deltaTime * 4f));
			yield return true;
		}
		this.anim.SetLayerWeight(3, 0f);
		this.anim.SetLayerWeight(4, 0f);
		yield break;
	}

	// Token: 0x060017C0 RID: 6080 RVA: 0x00094F85 File Offset: 0x00093185
	public CosmeticDisplay()
	{
	}

	// Token: 0x04001785 RID: 6021
	public Transform HeadHolder;

	// Token: 0x04001786 RID: 6022
	private GameObject curHead;

	// Token: 0x04001787 RID: 6023
	public SkinnedMeshRenderer Coat;

	// Token: 0x04001788 RID: 6024
	public SkinnedMeshRenderer ArmL;

	// Token: 0x04001789 RID: 6025
	public SkinnedMeshRenderer ArmR;

	// Token: 0x0400178A RID: 6026
	public SkinnedMeshRenderer Legs;

	// Token: 0x0400178B RID: 6027
	public PlayerBookDisplay Book;

	// Token: 0x0400178C RID: 6028
	public Transform BackHolder;

	// Token: 0x0400178D RID: 6029
	private GameObject curBack;

	// Token: 0x0400178E RID: 6030
	private Animator anim;

	// Token: 0x0400178F RID: 6031
	private float targetAngle;

	// Token: 0x0200060A RID: 1546
	[CompilerGenerated]
	private sealed class <FadeEmoteOut>d__20 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x060026F6 RID: 9974 RVA: 0x000D489A File Offset: 0x000D2A9A
		[DebuggerHidden]
		public <FadeEmoteOut>d__20(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x060026F7 RID: 9975 RVA: 0x000D48A9 File Offset: 0x000D2AA9
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x060026F8 RID: 9976 RVA: 0x000D48AC File Offset: 0x000D2AAC
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			CosmeticDisplay cosmeticDisplay = this;
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
				t = 0f;
			}
			if (t >= 1f)
			{
				cosmeticDisplay.anim.SetLayerWeight(3, 0f);
				cosmeticDisplay.anim.SetLayerWeight(4, 0f);
				return false;
			}
			t += Time.deltaTime;
			cosmeticDisplay.anim.SetLayerWeight(3, Mathf.Lerp(cosmeticDisplay.anim.GetLayerWeight(3), 0f, Time.deltaTime * 4f));
			cosmeticDisplay.anim.SetLayerWeight(4, Mathf.Lerp(cosmeticDisplay.anim.GetLayerWeight(4), 0f, Time.deltaTime * 4f));
			this.<>2__current = true;
			this.<>1__state = 1;
			return true;
		}

		// Token: 0x17000389 RID: 905
		// (get) Token: 0x060026F9 RID: 9977 RVA: 0x000D49A5 File Offset: 0x000D2BA5
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x060026FA RID: 9978 RVA: 0x000D49AD File Offset: 0x000D2BAD
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x1700038A RID: 906
		// (get) Token: 0x060026FB RID: 9979 RVA: 0x000D49B4 File Offset: 0x000D2BB4
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x04002995 RID: 10645
		private int <>1__state;

		// Token: 0x04002996 RID: 10646
		private object <>2__current;

		// Token: 0x04002997 RID: 10647
		public CosmeticDisplay <>4__this;

		// Token: 0x04002998 RID: 10648
		private float <t>5__2;
	}
}
