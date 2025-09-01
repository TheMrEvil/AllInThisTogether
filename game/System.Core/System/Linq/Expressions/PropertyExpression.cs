using System;
using System.Reflection;

namespace System.Linq.Expressions
{
	// Token: 0x0200027A RID: 634
	internal sealed class PropertyExpression : MemberExpression
	{
		// Token: 0x06001279 RID: 4729 RVA: 0x0003B664 File Offset: 0x00039864
		public PropertyExpression(Expression expression, PropertyInfo member) : base(expression)
		{
			this._property = member;
		}

		// Token: 0x0600127A RID: 4730 RVA: 0x0003B674 File Offset: 0x00039874
		internal override MemberInfo GetMember()
		{
			return this._property;
		}

		// Token: 0x1700030D RID: 781
		// (get) Token: 0x0600127B RID: 4731 RVA: 0x0003B67C File Offset: 0x0003987C
		public sealed override Type Type
		{
			get
			{
				return this._property.PropertyType;
			}
		}

		// Token: 0x04000A25 RID: 2597
		private readonly PropertyInfo _property;
	}
}
