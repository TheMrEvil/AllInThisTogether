using System;
using Unity;

namespace System.Drawing.Drawing2D
{
	/// <summary>Represents the state of a <see cref="T:System.Drawing.Graphics" /> object. This object is returned by a call to the <see cref="M:System.Drawing.Graphics.Save" /> methods. This class cannot be inherited.</summary>
	// Token: 0x02000141 RID: 321
	public sealed class GraphicsState : MarshalByRefObject
	{
		// Token: 0x06000E3D RID: 3645 RVA: 0x00020584 File Offset: 0x0001E784
		internal GraphicsState(int nativeState)
		{
			this.nativeState = nativeState;
		}

		// Token: 0x06000E3E RID: 3646 RVA: 0x00005B7D File Offset: 0x00003D7D
		internal GraphicsState()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04000AE2 RID: 2786
		internal int nativeState;
	}
}
