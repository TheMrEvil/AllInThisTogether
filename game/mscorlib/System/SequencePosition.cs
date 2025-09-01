using System;
using System.Numerics.Hashing;

namespace System
{
	// Token: 0x020001C5 RID: 453
	public readonly struct SequencePosition : IEquatable<SequencePosition>
	{
		// Token: 0x0600134A RID: 4938 RVA: 0x0004DB2D File Offset: 0x0004BD2D
		public SequencePosition(object @object, int integer)
		{
			this._object = @object;
			this._integer = integer;
		}

		// Token: 0x0600134B RID: 4939 RVA: 0x0004DB3D File Offset: 0x0004BD3D
		public object GetObject()
		{
			return this._object;
		}

		// Token: 0x0600134C RID: 4940 RVA: 0x0004DB45 File Offset: 0x0004BD45
		public int GetInteger()
		{
			return this._integer;
		}

		// Token: 0x0600134D RID: 4941 RVA: 0x0004DB4D File Offset: 0x0004BD4D
		public bool Equals(SequencePosition other)
		{
			return this._integer == other._integer && object.Equals(this._object, other._object);
		}

		// Token: 0x0600134E RID: 4942 RVA: 0x0004DB70 File Offset: 0x0004BD70
		public override bool Equals(object obj)
		{
			if (obj is SequencePosition)
			{
				SequencePosition other = (SequencePosition)obj;
				return this.Equals(other);
			}
			return false;
		}

		// Token: 0x0600134F RID: 4943 RVA: 0x0004DB95 File Offset: 0x0004BD95
		public override int GetHashCode()
		{
			object @object = this._object;
			return HashHelpers.Combine((@object != null) ? @object.GetHashCode() : 0, this._integer);
		}

		// Token: 0x04001444 RID: 5188
		private readonly object _object;

		// Token: 0x04001445 RID: 5189
		private readonly int _integer;
	}
}
