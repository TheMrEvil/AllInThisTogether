using System;
using System.Collections;
using Febucci.UI.Core;
using Febucci.UI.Core.Parsing;
using UnityEngine;

namespace Febucci.UI.Actions
{
	// Token: 0x02000033 RID: 51
	[Serializable]
	public abstract class ActionScriptableBase : ScriptableObject, ITagProvider
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x060000BB RID: 187 RVA: 0x00004B45 File Offset: 0x00002D45
		// (set) Token: 0x060000BC RID: 188 RVA: 0x00004B4D File Offset: 0x00002D4D
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

		// Token: 0x060000BD RID: 189
		public abstract IEnumerator DoAction(ActionMarker action, TypewriterCore typewriter, TypingInfo typingInfo);

		// Token: 0x060000BE RID: 190 RVA: 0x00004B56 File Offset: 0x00002D56
		protected ActionScriptableBase()
		{
		}

		// Token: 0x0400009F RID: 159
		[SerializeField]
		private string tagID;
	}
}
