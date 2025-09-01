using System;
using Unity;

namespace System.Drawing.Drawing2D
{
	/// <summary>Represents the internal data of a graphics container. This class is used when saving the state of a <see cref="T:System.Drawing.Graphics" /> object using the <see cref="M:System.Drawing.Graphics.BeginContainer" /> and <see cref="M:System.Drawing.Graphics.EndContainer(System.Drawing.Drawing2D.GraphicsContainer)" /> methods. This class cannot be inherited.</summary>
	// Token: 0x02000154 RID: 340
	public sealed class GraphicsContainer : MarshalByRefObject
	{
		// Token: 0x06000E54 RID: 3668 RVA: 0x000207B7 File Offset: 0x0001E9B7
		internal GraphicsContainer(uint state)
		{
			this.nativeState = state;
		}

		// Token: 0x170003D0 RID: 976
		// (get) Token: 0x06000E55 RID: 3669 RVA: 0x000207C6 File Offset: 0x0001E9C6
		internal uint NativeObject
		{
			get
			{
				return this.nativeState;
			}
		}

		// Token: 0x06000E56 RID: 3670 RVA: 0x00005B7D File Offset: 0x00003D7D
		internal GraphicsContainer()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04000B73 RID: 2931
		private uint nativeState;
	}
}
