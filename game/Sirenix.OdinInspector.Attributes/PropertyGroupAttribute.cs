using System;
using System.Diagnostics;

namespace Sirenix.OdinInspector
{
	// Token: 0x02000052 RID: 82
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = true)]
	[Conditional("UNITY_EDITOR")]
	public abstract class PropertyGroupAttribute : Attribute
	{
		// Token: 0x06000118 RID: 280 RVA: 0x000032E0 File Offset: 0x000014E0
		public PropertyGroupAttribute(string groupId, float order)
		{
			this.GroupID = groupId;
			this.Order = order;
			int num = groupId.LastIndexOf('/');
			this.GroupName = ((num >= 0 && num < groupId.Length) ? groupId.Substring(num + 1) : groupId);
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00003337 File Offset: 0x00001537
		public PropertyGroupAttribute(string groupId) : this(groupId, 0f)
		{
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00003348 File Offset: 0x00001548
		public PropertyGroupAttribute Combine(PropertyGroupAttribute other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			if (other.GetType() != base.GetType())
			{
				throw new ArgumentException("Attributes to combine are not of the same type.");
			}
			if (other.GroupID != this.GroupID)
			{
				throw new ArgumentException("PropertyGroupAttributes to combine must have the same group id.");
			}
			if (this.Order == 0f)
			{
				this.Order = other.Order;
			}
			else if (other.Order != 0f)
			{
				this.Order = Math.Min(this.Order, other.Order);
			}
			this.HideWhenChildrenAreInvisible &= other.HideWhenChildrenAreInvisible;
			if (this.VisibleIf == null)
			{
				this.VisibleIf = other.VisibleIf;
			}
			this.AnimateVisibility &= other.AnimateVisibility;
			this.CombineValuesWith(other);
			return this;
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00003420 File Offset: 0x00001620
		protected virtual void CombineValuesWith(PropertyGroupAttribute other)
		{
		}

		// Token: 0x040000E6 RID: 230
		public string GroupID;

		// Token: 0x040000E7 RID: 231
		public string GroupName;

		// Token: 0x040000E8 RID: 232
		public float Order;

		// Token: 0x040000E9 RID: 233
		public bool HideWhenChildrenAreInvisible = true;

		// Token: 0x040000EA RID: 234
		public string VisibleIf;

		// Token: 0x040000EB RID: 235
		public bool AnimateVisibility = true;
	}
}
