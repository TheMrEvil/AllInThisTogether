using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace UnityEngine.Rendering
{
	// Token: 0x020000BE RID: 190
	[DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	[Serializable]
	public class VolumeParameter<T> : VolumeParameter, IEquatable<VolumeParameter<T>>
	{
		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x06000658 RID: 1624 RVA: 0x0001D65F File Offset: 0x0001B85F
		// (set) Token: 0x06000659 RID: 1625 RVA: 0x0001D667 File Offset: 0x0001B867
		public virtual T value
		{
			get
			{
				return this.m_Value;
			}
			set
			{
				this.m_Value = value;
			}
		}

		// Token: 0x0600065A RID: 1626 RVA: 0x0001D670 File Offset: 0x0001B870
		public VolumeParameter() : this(default(T), false)
		{
		}

		// Token: 0x0600065B RID: 1627 RVA: 0x0001D68D File Offset: 0x0001B88D
		protected VolumeParameter(T value, bool overrideState)
		{
			this.m_Value = value;
			this.overrideState = overrideState;
		}

		// Token: 0x0600065C RID: 1628 RVA: 0x0001D6A3 File Offset: 0x0001B8A3
		internal override void Interp(VolumeParameter from, VolumeParameter to, float t)
		{
			this.Interp(from.GetValue<T>(), to.GetValue<T>(), t);
		}

		// Token: 0x0600065D RID: 1629 RVA: 0x0001D6B8 File Offset: 0x0001B8B8
		public virtual void Interp(T from, T to, float t)
		{
			this.m_Value = ((t > 0f) ? to : from);
		}

		// Token: 0x0600065E RID: 1630 RVA: 0x0001D6CC File Offset: 0x0001B8CC
		public void Override(T x)
		{
			this.overrideState = true;
			this.m_Value = x;
		}

		// Token: 0x0600065F RID: 1631 RVA: 0x0001D6DC File Offset: 0x0001B8DC
		public override void SetValue(VolumeParameter parameter)
		{
			this.m_Value = parameter.GetValue<T>();
		}

		// Token: 0x06000660 RID: 1632 RVA: 0x0001D6EC File Offset: 0x0001B8EC
		public override int GetHashCode()
		{
			int num = 17;
			num = num * 23 + this.overrideState.GetHashCode();
			if (!EqualityComparer<T>.Default.Equals(this.value, default(T)))
			{
				int num2 = num * 23;
				T value = this.value;
				num = num2 + value.GetHashCode();
			}
			return num;
		}

		// Token: 0x06000661 RID: 1633 RVA: 0x0001D746 File Offset: 0x0001B946
		public override string ToString()
		{
			return string.Format("{0} ({1})", this.value, this.overrideState);
		}

		// Token: 0x06000662 RID: 1634 RVA: 0x0001D768 File Offset: 0x0001B968
		public static bool operator ==(VolumeParameter<T> lhs, T rhs)
		{
			if (lhs != null && lhs.value != null)
			{
				T value = lhs.value;
				return value.Equals(rhs);
			}
			return false;
		}

		// Token: 0x06000663 RID: 1635 RVA: 0x0001D7A1 File Offset: 0x0001B9A1
		public static bool operator !=(VolumeParameter<T> lhs, T rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x06000664 RID: 1636 RVA: 0x0001D7AD File Offset: 0x0001B9AD
		public bool Equals(VolumeParameter<T> other)
		{
			return other != null && (this == other || EqualityComparer<T>.Default.Equals(this.m_Value, other.m_Value));
		}

		// Token: 0x06000665 RID: 1637 RVA: 0x0001D7D0 File Offset: 0x0001B9D0
		public override bool Equals(object obj)
		{
			return obj != null && (this == obj || (!(obj.GetType() != base.GetType()) && this.Equals((VolumeParameter<T>)obj)));
		}

		// Token: 0x06000666 RID: 1638 RVA: 0x0001D7FE File Offset: 0x0001B9FE
		public static explicit operator T(VolumeParameter<T> prop)
		{
			return prop.m_Value;
		}

		// Token: 0x040003AA RID: 938
		[SerializeField]
		protected T m_Value;
	}
}
