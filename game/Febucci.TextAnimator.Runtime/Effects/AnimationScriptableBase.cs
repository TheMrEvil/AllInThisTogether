using System;
using Febucci.UI.Core;
using UnityEngine;

namespace Febucci.UI.Effects
{
	// Token: 0x0200002C RID: 44
	public abstract class AnimationScriptableBase : ScriptableObject, ITagProvider
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x000049D5 File Offset: 0x00002BD5
		// (set) Token: 0x060000A3 RID: 163 RVA: 0x000049DD File Offset: 0x00002BDD
		public string TagID
		{
			get
			{
				return this.tagID;
			}
			set
			{
				this.tagID = value;
			}
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x000049E6 File Offset: 0x00002BE6
		public void InitializeOnce()
		{
			if (this.initialized)
			{
				return;
			}
			this.initialized = true;
			this.OnInitialize();
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x000049FE File Offset: 0x00002BFE
		protected virtual void OnInitialize()
		{
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00004A00 File Offset: 0x00002C00
		private void OnEnable()
		{
			this.initialized = false;
		}

		// Token: 0x060000A7 RID: 167
		public abstract void ResetContext(TAnimCore animator);

		// Token: 0x060000A8 RID: 168 RVA: 0x00004A09 File Offset: 0x00002C09
		public virtual void SetModifier(ModifierInfo modifier)
		{
		}

		// Token: 0x060000A9 RID: 169
		public abstract float GetMaxDuration();

		// Token: 0x060000AA RID: 170
		public abstract bool CanApplyEffectTo(CharacterData character, TAnimCore animator);

		// Token: 0x060000AB RID: 171
		public abstract void ApplyEffectTo(ref CharacterData character, TAnimCore animator);

		// Token: 0x060000AC RID: 172 RVA: 0x00004A0B File Offset: 0x00002C0B
		protected AnimationScriptableBase()
		{
		}

		// Token: 0x0400009B RID: 155
		[SerializeField]
		private string tagID;

		// Token: 0x0400009C RID: 156
		private bool initialized;
	}
}
