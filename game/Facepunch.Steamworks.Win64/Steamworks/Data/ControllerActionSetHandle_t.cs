using System;

namespace Steamworks.Data
{
	// Token: 0x020001DB RID: 475
	internal struct ControllerActionSetHandle_t : IEquatable<ControllerActionSetHandle_t>, IComparable<ControllerActionSetHandle_t>
	{
		// Token: 0x06000F17 RID: 3863 RVA: 0x00018DF0 File Offset: 0x00016FF0
		public static implicit operator ControllerActionSetHandle_t(ulong value)
		{
			return new ControllerActionSetHandle_t
			{
				Value = value
			};
		}

		// Token: 0x06000F18 RID: 3864 RVA: 0x00018E0E File Offset: 0x0001700E
		public static implicit operator ulong(ControllerActionSetHandle_t value)
		{
			return value.Value;
		}

		// Token: 0x06000F19 RID: 3865 RVA: 0x00018E16 File Offset: 0x00017016
		public override string ToString()
		{
			return this.Value.ToString();
		}

		// Token: 0x06000F1A RID: 3866 RVA: 0x00018E23 File Offset: 0x00017023
		public override int GetHashCode()
		{
			return this.Value.GetHashCode();
		}

		// Token: 0x06000F1B RID: 3867 RVA: 0x00018E30 File Offset: 0x00017030
		public override bool Equals(object p)
		{
			return this.Equals((ControllerActionSetHandle_t)p);
		}

		// Token: 0x06000F1C RID: 3868 RVA: 0x00018E3E File Offset: 0x0001703E
		public bool Equals(ControllerActionSetHandle_t p)
		{
			return p.Value == this.Value;
		}

		// Token: 0x06000F1D RID: 3869 RVA: 0x00018E4E File Offset: 0x0001704E
		public static bool operator ==(ControllerActionSetHandle_t a, ControllerActionSetHandle_t b)
		{
			return a.Equals(b);
		}

		// Token: 0x06000F1E RID: 3870 RVA: 0x00018E58 File Offset: 0x00017058
		public static bool operator !=(ControllerActionSetHandle_t a, ControllerActionSetHandle_t b)
		{
			return !a.Equals(b);
		}

		// Token: 0x06000F1F RID: 3871 RVA: 0x00018E65 File Offset: 0x00017065
		public int CompareTo(ControllerActionSetHandle_t other)
		{
			return this.Value.CompareTo(other.Value);
		}

		// Token: 0x04000BCA RID: 3018
		public ulong Value;
	}
}
