using System;
using System.Runtime.CompilerServices;

namespace System.Drawing.Drawing2D
{
	/// <summary>Defines a blend pattern for a <see cref="T:System.Drawing.Drawing2D.LinearGradientBrush" /> object. This class cannot be inherited.</summary>
	// Token: 0x02000134 RID: 308
	public sealed class Blend
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Drawing2D.Blend" /> class.</summary>
		// Token: 0x06000E0C RID: 3596 RVA: 0x0001FD39 File Offset: 0x0001DF39
		public Blend()
		{
			this.Factors = new float[1];
			this.Positions = new float[1];
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Drawing2D.Blend" /> class with the specified number of factors and positions.</summary>
		/// <param name="count">The number of elements in the <see cref="P:System.Drawing.Drawing2D.Blend.Factors" /> and <see cref="P:System.Drawing.Drawing2D.Blend.Positions" /> arrays.</param>
		// Token: 0x06000E0D RID: 3597 RVA: 0x0001FD59 File Offset: 0x0001DF59
		public Blend(int count)
		{
			this.Factors = new float[count];
			this.Positions = new float[count];
		}

		/// <summary>Gets or sets an array of blend factors for the gradient.</summary>
		/// <returns>An array of blend factors that specify the percentages of the starting color and the ending color to be used at the corresponding position.</returns>
		// Token: 0x170003BF RID: 959
		// (get) Token: 0x06000E0E RID: 3598 RVA: 0x0001FD79 File Offset: 0x0001DF79
		// (set) Token: 0x06000E0F RID: 3599 RVA: 0x0001FD81 File Offset: 0x0001DF81
		public float[] Factors
		{
			[CompilerGenerated]
			get
			{
				return this.<Factors>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Factors>k__BackingField = value;
			}
		}

		/// <summary>Gets or sets an array of blend positions for the gradient.</summary>
		/// <returns>An array of blend positions that specify the percentages of distance along the gradient line.</returns>
		// Token: 0x170003C0 RID: 960
		// (get) Token: 0x06000E10 RID: 3600 RVA: 0x0001FD8A File Offset: 0x0001DF8A
		// (set) Token: 0x06000E11 RID: 3601 RVA: 0x0001FD92 File Offset: 0x0001DF92
		public float[] Positions
		{
			[CompilerGenerated]
			get
			{
				return this.<Positions>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Positions>k__BackingField = value;
			}
		}

		// Token: 0x04000AB0 RID: 2736
		[CompilerGenerated]
		private float[] <Factors>k__BackingField;

		// Token: 0x04000AB1 RID: 2737
		[CompilerGenerated]
		private float[] <Positions>k__BackingField;
	}
}
