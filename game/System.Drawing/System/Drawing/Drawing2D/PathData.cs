using System;
using System.Runtime.CompilerServices;

namespace System.Drawing.Drawing2D
{
	/// <summary>Contains the graphical data that makes up a <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> object. This class cannot be inherited.</summary>
	// Token: 0x02000149 RID: 329
	public sealed class PathData
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Drawing2D.PathData" /> class.</summary>
		// Token: 0x06000E46 RID: 3654 RVA: 0x00002050 File Offset: 0x00000250
		public PathData()
		{
		}

		/// <summary>Gets or sets an array of <see cref="T:System.Drawing.PointF" /> structures that represents the points through which the path is constructed.</summary>
		/// <returns>An array of <see cref="T:System.Drawing.PointF" /> objects that represents the points through which the path is constructed.</returns>
		// Token: 0x170003CC RID: 972
		// (get) Token: 0x06000E47 RID: 3655 RVA: 0x000206CF File Offset: 0x0001E8CF
		// (set) Token: 0x06000E48 RID: 3656 RVA: 0x000206D7 File Offset: 0x0001E8D7
		public PointF[] Points
		{
			[CompilerGenerated]
			get
			{
				return this.<Points>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Points>k__BackingField = value;
			}
		}

		/// <summary>Gets or sets the types of the corresponding points in the path.</summary>
		/// <returns>An array of bytes that specify the types of the corresponding points in the path.</returns>
		// Token: 0x170003CD RID: 973
		// (get) Token: 0x06000E49 RID: 3657 RVA: 0x000206E0 File Offset: 0x0001E8E0
		// (set) Token: 0x06000E4A RID: 3658 RVA: 0x000206E8 File Offset: 0x0001E8E8
		public byte[] Types
		{
			[CompilerGenerated]
			get
			{
				return this.<Types>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Types>k__BackingField = value;
			}
		}

		// Token: 0x04000B3F RID: 2879
		[CompilerGenerated]
		private PointF[] <Points>k__BackingField;

		// Token: 0x04000B40 RID: 2880
		[CompilerGenerated]
		private byte[] <Types>k__BackingField;
	}
}
