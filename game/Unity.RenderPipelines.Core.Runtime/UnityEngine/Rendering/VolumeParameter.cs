using System;

namespace UnityEngine.Rendering
{
	// Token: 0x020000BD RID: 189
	public abstract class VolumeParameter
	{
		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x0600064E RID: 1614 RVA: 0x0001D5F5 File Offset: 0x0001B7F5
		// (set) Token: 0x0600064F RID: 1615 RVA: 0x0001D5FD File Offset: 0x0001B7FD
		public virtual bool overrideState
		{
			get
			{
				return this.m_OverrideState;
			}
			set
			{
				this.m_OverrideState = value;
			}
		}

		// Token: 0x06000650 RID: 1616
		internal abstract void Interp(VolumeParameter from, VolumeParameter to, float t);

		// Token: 0x06000651 RID: 1617 RVA: 0x0001D606 File Offset: 0x0001B806
		public T GetValue<T>()
		{
			return ((VolumeParameter<T>)this).value;
		}

		// Token: 0x06000652 RID: 1618
		public abstract void SetValue(VolumeParameter parameter);

		// Token: 0x06000653 RID: 1619 RVA: 0x0001D613 File Offset: 0x0001B813
		protected internal virtual void OnEnable()
		{
		}

		// Token: 0x06000654 RID: 1620 RVA: 0x0001D615 File Offset: 0x0001B815
		protected internal virtual void OnDisable()
		{
		}

		// Token: 0x06000655 RID: 1621 RVA: 0x0001D617 File Offset: 0x0001B817
		public static bool IsObjectParameter(Type type)
		{
			return (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(ObjectParameter<>)) || (type.BaseType != null && VolumeParameter.IsObjectParameter(type.BaseType));
		}

		// Token: 0x06000656 RID: 1622 RVA: 0x0001D655 File Offset: 0x0001B855
		public virtual void Release()
		{
		}

		// Token: 0x06000657 RID: 1623 RVA: 0x0001D657 File Offset: 0x0001B857
		protected VolumeParameter()
		{
		}

		// Token: 0x040003A8 RID: 936
		public const string k_DebuggerDisplay = "{m_Value} ({m_OverrideState})";

		// Token: 0x040003A9 RID: 937
		[SerializeField]
		protected bool m_OverrideState;
	}
}
