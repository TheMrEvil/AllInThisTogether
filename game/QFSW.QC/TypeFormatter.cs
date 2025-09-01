using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Scripting;

namespace QFSW.QC
{
	// Token: 0x0200004D RID: 77
	[Serializable]
	public abstract class TypeFormatter : ISerializationCallbackReceiver
	{
		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000196 RID: 406 RVA: 0x000081F2 File Offset: 0x000063F2
		// (set) Token: 0x06000197 RID: 407 RVA: 0x000081FA File Offset: 0x000063FA
		public Type Type
		{
			[CompilerGenerated]
			get
			{
				return this.<Type>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Type>k__BackingField = value;
			}
		}

		// Token: 0x06000198 RID: 408 RVA: 0x00008203 File Offset: 0x00006403
		[Preserve]
		protected TypeFormatter(Type type)
		{
			this.Type = type;
		}

		// Token: 0x06000199 RID: 409 RVA: 0x00008212 File Offset: 0x00006412
		public void OnAfterDeserialize()
		{
			this.Type = Type.GetType(this._type, false);
			if (this.Type == null)
			{
				this.Type = QuantumParser.ParseType(this._type.Split(',', StringSplitOptions.None)[0]);
			}
		}

		// Token: 0x0600019A RID: 410 RVA: 0x0000824F File Offset: 0x0000644F
		public void OnBeforeSerialize()
		{
			if (this.Type != null)
			{
				this._type = this.Type.AssemblyQualifiedName;
			}
		}

		// Token: 0x0400012A RID: 298
		[CompilerGenerated]
		private Type <Type>k__BackingField;

		// Token: 0x0400012B RID: 299
		[SerializeField]
		[HideInInspector]
		private string _type;
	}
}
