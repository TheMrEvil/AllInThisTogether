using System;
using UnityEngine.Serialization;

namespace UnityEngine.Events
{
	// Token: 0x020002B7 RID: 695
	[Serializable]
	internal class ArgumentCache : ISerializationCallbackReceiver
	{
		// Token: 0x170005BE RID: 1470
		// (get) Token: 0x06001D20 RID: 7456 RVA: 0x0002EAC8 File Offset: 0x0002CCC8
		// (set) Token: 0x06001D21 RID: 7457 RVA: 0x0002EAE0 File Offset: 0x0002CCE0
		public Object unityObjectArgument
		{
			get
			{
				return this.m_ObjectArgument;
			}
			set
			{
				this.m_ObjectArgument = value;
				this.m_ObjectArgumentAssemblyTypeName = ((value != null) ? value.GetType().AssemblyQualifiedName : string.Empty);
			}
		}

		// Token: 0x170005BF RID: 1471
		// (get) Token: 0x06001D22 RID: 7458 RVA: 0x0002EB0C File Offset: 0x0002CD0C
		public string unityObjectArgumentAssemblyTypeName
		{
			get
			{
				return this.m_ObjectArgumentAssemblyTypeName;
			}
		}

		// Token: 0x170005C0 RID: 1472
		// (get) Token: 0x06001D23 RID: 7459 RVA: 0x0002EB24 File Offset: 0x0002CD24
		// (set) Token: 0x06001D24 RID: 7460 RVA: 0x0002EB3C File Offset: 0x0002CD3C
		public int intArgument
		{
			get
			{
				return this.m_IntArgument;
			}
			set
			{
				this.m_IntArgument = value;
			}
		}

		// Token: 0x170005C1 RID: 1473
		// (get) Token: 0x06001D25 RID: 7461 RVA: 0x0002EB48 File Offset: 0x0002CD48
		// (set) Token: 0x06001D26 RID: 7462 RVA: 0x0002EB60 File Offset: 0x0002CD60
		public float floatArgument
		{
			get
			{
				return this.m_FloatArgument;
			}
			set
			{
				this.m_FloatArgument = value;
			}
		}

		// Token: 0x170005C2 RID: 1474
		// (get) Token: 0x06001D27 RID: 7463 RVA: 0x0002EB6C File Offset: 0x0002CD6C
		// (set) Token: 0x06001D28 RID: 7464 RVA: 0x0002EB84 File Offset: 0x0002CD84
		public string stringArgument
		{
			get
			{
				return this.m_StringArgument;
			}
			set
			{
				this.m_StringArgument = value;
			}
		}

		// Token: 0x170005C3 RID: 1475
		// (get) Token: 0x06001D29 RID: 7465 RVA: 0x0002EB90 File Offset: 0x0002CD90
		// (set) Token: 0x06001D2A RID: 7466 RVA: 0x0002EBA8 File Offset: 0x0002CDA8
		public bool boolArgument
		{
			get
			{
				return this.m_BoolArgument;
			}
			set
			{
				this.m_BoolArgument = value;
			}
		}

		// Token: 0x06001D2B RID: 7467 RVA: 0x0002EBB2 File Offset: 0x0002CDB2
		public void OnBeforeSerialize()
		{
			this.m_ObjectArgumentAssemblyTypeName = UnityEventTools.TidyAssemblyTypeName(this.m_ObjectArgumentAssemblyTypeName);
		}

		// Token: 0x06001D2C RID: 7468 RVA: 0x0002EBB2 File Offset: 0x0002CDB2
		public void OnAfterDeserialize()
		{
			this.m_ObjectArgumentAssemblyTypeName = UnityEventTools.TidyAssemblyTypeName(this.m_ObjectArgumentAssemblyTypeName);
		}

		// Token: 0x06001D2D RID: 7469 RVA: 0x00002072 File Offset: 0x00000272
		public ArgumentCache()
		{
		}

		// Token: 0x04000992 RID: 2450
		[FormerlySerializedAs("objectArgument")]
		[SerializeField]
		private Object m_ObjectArgument;

		// Token: 0x04000993 RID: 2451
		[FormerlySerializedAs("objectArgumentAssemblyTypeName")]
		[SerializeField]
		private string m_ObjectArgumentAssemblyTypeName;

		// Token: 0x04000994 RID: 2452
		[SerializeField]
		[FormerlySerializedAs("intArgument")]
		private int m_IntArgument;

		// Token: 0x04000995 RID: 2453
		[FormerlySerializedAs("floatArgument")]
		[SerializeField]
		private float m_FloatArgument;

		// Token: 0x04000996 RID: 2454
		[FormerlySerializedAs("stringArgument")]
		[SerializeField]
		private string m_StringArgument;

		// Token: 0x04000997 RID: 2455
		[SerializeField]
		private bool m_BoolArgument;
	}
}
