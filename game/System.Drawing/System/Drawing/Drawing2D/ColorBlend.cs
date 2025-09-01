using System;
using System.Runtime.CompilerServices;

namespace System.Drawing.Drawing2D
{
	/// <summary>Defines arrays of colors and positions used for interpolating color blending in a multicolor gradient. This class cannot be inherited.</summary>
	// Token: 0x02000136 RID: 310
	public sealed class ColorBlend
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Drawing2D.ColorBlend" /> class.</summary>
		// Token: 0x06000E12 RID: 3602 RVA: 0x0001FD9B File Offset: 0x0001DF9B
		public ColorBlend()
		{
			this.Colors = new Color[1];
			this.Positions = new float[1];
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Drawing2D.ColorBlend" /> class with the specified number of colors and positions.</summary>
		/// <param name="count">The number of colors and positions in this <see cref="T:System.Drawing.Drawing2D.ColorBlend" />.</param>
		// Token: 0x06000E13 RID: 3603 RVA: 0x0001FDBB File Offset: 0x0001DFBB
		public ColorBlend(int count)
		{
			this.Colors = new Color[count];
			this.Positions = new float[count];
		}

		/// <summary>Gets or sets an array of colors that represents the colors to use at corresponding positions along a gradient.</summary>
		/// <returns>An array of <see cref="T:System.Drawing.Color" /> structures that represents the colors to use at corresponding positions along a gradient.</returns>
		// Token: 0x170003C1 RID: 961
		// (get) Token: 0x06000E14 RID: 3604 RVA: 0x0001FDDB File Offset: 0x0001DFDB
		// (set) Token: 0x06000E15 RID: 3605 RVA: 0x0001FDE3 File Offset: 0x0001DFE3
		public Color[] Colors
		{
			[CompilerGenerated]
			get
			{
				return this.<Colors>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Colors>k__BackingField = value;
			}
		}

		/// <summary>Gets or sets the positions along a gradient line.</summary>
		/// <returns>An array of values that specify percentages of distance along the gradient line.</returns>
		// Token: 0x170003C2 RID: 962
		// (get) Token: 0x06000E16 RID: 3606 RVA: 0x0001FDEC File Offset: 0x0001DFEC
		// (set) Token: 0x06000E17 RID: 3607 RVA: 0x0001FDF4 File Offset: 0x0001DFF4
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

		// Token: 0x04000AB8 RID: 2744
		[CompilerGenerated]
		private Color[] <Colors>k__BackingField;

		// Token: 0x04000AB9 RID: 2745
		[CompilerGenerated]
		private float[] <Positions>k__BackingField;
	}
}
