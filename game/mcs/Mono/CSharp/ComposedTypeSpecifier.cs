using System;
using System.Runtime.CompilerServices;

namespace Mono.CSharp
{
	// Token: 0x02000206 RID: 518
	public class ComposedTypeSpecifier
	{
		// Token: 0x06001ACD RID: 6861 RVA: 0x000825E0 File Offset: 0x000807E0
		public ComposedTypeSpecifier(int specifier, Location loc)
		{
			this.Dimension = specifier;
			this.Location = loc;
		}

		// Token: 0x1700061A RID: 1562
		// (get) Token: 0x06001ACE RID: 6862 RVA: 0x000825F6 File Offset: 0x000807F6
		public bool IsNullable
		{
			get
			{
				return this.Dimension == -1;
			}
		}

		// Token: 0x1700061B RID: 1563
		// (get) Token: 0x06001ACF RID: 6863 RVA: 0x00082601 File Offset: 0x00080801
		public bool IsPointer
		{
			get
			{
				return this.Dimension == -2;
			}
		}

		// Token: 0x1700061C RID: 1564
		// (get) Token: 0x06001AD0 RID: 6864 RVA: 0x0008260D File Offset: 0x0008080D
		// (set) Token: 0x06001AD1 RID: 6865 RVA: 0x00082615 File Offset: 0x00080815
		public ComposedTypeSpecifier Next
		{
			[CompilerGenerated]
			get
			{
				return this.<Next>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Next>k__BackingField = value;
			}
		}

		// Token: 0x06001AD2 RID: 6866 RVA: 0x0008261E File Offset: 0x0008081E
		public static ComposedTypeSpecifier CreateArrayDimension(int dimension, Location loc)
		{
			return new ComposedTypeSpecifier(dimension, loc);
		}

		// Token: 0x06001AD3 RID: 6867 RVA: 0x00082627 File Offset: 0x00080827
		public static ComposedTypeSpecifier CreateNullable(Location loc)
		{
			return new ComposedTypeSpecifier(-1, loc);
		}

		// Token: 0x06001AD4 RID: 6868 RVA: 0x00082630 File Offset: 0x00080830
		public static ComposedTypeSpecifier CreatePointer(Location loc)
		{
			return new ComposedTypeSpecifier(-2, loc);
		}

		// Token: 0x06001AD5 RID: 6869 RVA: 0x0008263C File Offset: 0x0008083C
		public string GetSignatureForError()
		{
			string text = this.IsPointer ? "*" : (this.IsNullable ? "?" : ArrayContainer.GetPostfixSignature(this.Dimension));
			if (this.Next == null)
			{
				return text;
			}
			return text + this.Next.GetSignatureForError();
		}

		// Token: 0x06001AD6 RID: 6870 RVA: 0x0008268E File Offset: 0x0008088E
		// Note: this type is marked as 'beforefieldinit'.
		static ComposedTypeSpecifier()
		{
		}

		// Token: 0x04000A03 RID: 2563
		public static readonly ComposedTypeSpecifier SingleDimension = new ComposedTypeSpecifier(1, Location.Null);

		// Token: 0x04000A04 RID: 2564
		public readonly int Dimension;

		// Token: 0x04000A05 RID: 2565
		public readonly Location Location;

		// Token: 0x04000A06 RID: 2566
		[CompilerGenerated]
		private ComposedTypeSpecifier <Next>k__BackingField;
	}
}
