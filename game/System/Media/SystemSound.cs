using System;
using System.IO;
using Unity;

namespace System.Media
{
	/// <summary>Represents a system sound type.</summary>
	// Token: 0x02000178 RID: 376
	public class SystemSound
	{
		// Token: 0x06000A0F RID: 2575 RVA: 0x0002C30D File Offset: 0x0002A50D
		internal SystemSound(string tag)
		{
			this.resource = typeof(SystemSound).Assembly.GetManifestResourceStream(tag + ".wav");
		}

		/// <summary>Plays the system sound type.</summary>
		// Token: 0x06000A10 RID: 2576 RVA: 0x0002C33A File Offset: 0x0002A53A
		public void Play()
		{
			new SoundPlayer(this.resource).Play();
		}

		// Token: 0x06000A11 RID: 2577 RVA: 0x00013BCA File Offset: 0x00011DCA
		internal SystemSound()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x040006C5 RID: 1733
		private Stream resource;
	}
}
