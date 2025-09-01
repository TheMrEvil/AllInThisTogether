using System;
using System.Runtime.CompilerServices;

namespace System.Net.Http.Headers
{
	/// <summary>Represents a byte range in a Range header value.</summary>
	// Token: 0x02000066 RID: 102
	public class RangeItemHeaderValue : ICloneable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.Headers.RangeItemHeaderValue" /> class.</summary>
		/// <param name="from">The position at which to start sending data.</param>
		/// <param name="to">The position at which to stop sending data.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="from" /> is greater than <paramref name="to" />  
		/// -or-  
		/// <paramref name="from" /> or <paramref name="to" /> is less than 0.</exception>
		// Token: 0x06000395 RID: 917 RVA: 0x0000C6B0 File Offset: 0x0000A8B0
		public RangeItemHeaderValue(long? from, long? to)
		{
			if (from == null && to == null)
			{
				throw new ArgumentException();
			}
			long? num2;
			if (from != null && to != null)
			{
				long? num = from;
				num2 = to;
				if (num.GetValueOrDefault() > num2.GetValueOrDefault() & (num != null & num2 != null))
				{
					throw new ArgumentOutOfRangeException("from");
				}
			}
			num2 = from;
			long num3 = 0L;
			if (num2.GetValueOrDefault() < num3 & num2 != null)
			{
				throw new ArgumentOutOfRangeException("from");
			}
			num2 = to;
			num3 = 0L;
			if (num2.GetValueOrDefault() < num3 & num2 != null)
			{
				throw new ArgumentOutOfRangeException("to");
			}
			this.From = from;
			this.To = to;
		}

		/// <summary>Gets the position at which to start sending data.</summary>
		/// <returns>The position at which to start sending data.</returns>
		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000396 RID: 918 RVA: 0x0000C774 File Offset: 0x0000A974
		// (set) Token: 0x06000397 RID: 919 RVA: 0x0000C77C File Offset: 0x0000A97C
		public long? From
		{
			[CompilerGenerated]
			get
			{
				return this.<From>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<From>k__BackingField = value;
			}
		}

		/// <summary>Gets the position at which to stop sending data.</summary>
		/// <returns>The position at which to stop sending data.</returns>
		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000398 RID: 920 RVA: 0x0000C785 File Offset: 0x0000A985
		// (set) Token: 0x06000399 RID: 921 RVA: 0x0000C78D File Offset: 0x0000A98D
		public long? To
		{
			[CompilerGenerated]
			get
			{
				return this.<To>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<To>k__BackingField = value;
			}
		}

		/// <summary>Creates a new object that is a copy of the current <see cref="T:System.Net.Http.Headers.RangeItemHeaderValue" /> instance.</summary>
		/// <returns>A copy of the current instance.</returns>
		// Token: 0x0600039A RID: 922 RVA: 0x00006AEE File Offset: 0x00004CEE
		object ICloneable.Clone()
		{
			return base.MemberwiseClone();
		}

		/// <summary>Determines whether the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Net.Http.Headers.RangeItemHeaderValue" /> object.</summary>
		/// <param name="obj">The object to compare with the current object.</param>
		/// <returns>
		///   <see langword="true" /> if the specified <see cref="T:System.Object" /> is equal to the current object; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600039B RID: 923 RVA: 0x0000C798 File Offset: 0x0000A998
		public override bool Equals(object obj)
		{
			RangeItemHeaderValue rangeItemHeaderValue = obj as RangeItemHeaderValue;
			if (rangeItemHeaderValue != null)
			{
				long? num = rangeItemHeaderValue.From;
				long? num2 = this.From;
				if (num.GetValueOrDefault() == num2.GetValueOrDefault() & num != null == (num2 != null))
				{
					num2 = rangeItemHeaderValue.To;
					num = this.To;
					return num2.GetValueOrDefault() == num.GetValueOrDefault() & num2 != null == (num != null);
				}
			}
			return false;
		}

		/// <summary>Serves as a hash function for an <see cref="T:System.Net.Http.Headers.RangeItemHeaderValue" /> object.</summary>
		/// <returns>A hash code for the current object.</returns>
		// Token: 0x0600039C RID: 924 RVA: 0x0000C814 File Offset: 0x0000AA14
		public override int GetHashCode()
		{
			return this.From.GetHashCode() ^ this.To.GetHashCode();
		}

		/// <summary>Returns a string that represents the current <see cref="T:System.Net.Http.Headers.RangeItemHeaderValue" /> object.</summary>
		/// <returns>A string that represents the current object.</returns>
		// Token: 0x0600039D RID: 925 RVA: 0x0000C84C File Offset: 0x0000AA4C
		public override string ToString()
		{
			if (this.From == null)
			{
				return "-" + this.To.Value.ToString();
			}
			if (this.To == null)
			{
				return this.From.Value.ToString() + "-";
			}
			return this.From.Value.ToString() + "-" + this.To.Value.ToString();
		}

		// Token: 0x04000148 RID: 328
		[CompilerGenerated]
		private long? <From>k__BackingField;

		// Token: 0x04000149 RID: 329
		[CompilerGenerated]
		private long? <To>k__BackingField;
	}
}
