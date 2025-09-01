using System;

namespace System.Media
{
	/// <summary>Retrieves sounds associated with a set of Windows operating system sound-event types. This class cannot be inherited.</summary>
	// Token: 0x02000179 RID: 377
	public sealed class SystemSounds
	{
		// Token: 0x06000A12 RID: 2578 RVA: 0x0000219B File Offset: 0x0000039B
		private SystemSounds()
		{
		}

		/// <summary>Gets the sound associated with the <see langword="Asterisk" /> program event in the current Windows sound scheme.</summary>
		/// <returns>A <see cref="T:System.Media.SystemSound" /> associated with the <see langword="Asterisk" /> program event in the current Windows sound scheme.</returns>
		// Token: 0x17000192 RID: 402
		// (get) Token: 0x06000A13 RID: 2579 RVA: 0x0002C34C File Offset: 0x0002A54C
		public static SystemSound Asterisk
		{
			get
			{
				return new SystemSound("Asterisk");
			}
		}

		/// <summary>Gets the sound associated with the <see langword="Beep" /> program event in the current Windows sound scheme.</summary>
		/// <returns>A <see cref="T:System.Media.SystemSound" /> associated with the <see langword="Beep" /> program event in the current Windows sound scheme.</returns>
		// Token: 0x17000193 RID: 403
		// (get) Token: 0x06000A14 RID: 2580 RVA: 0x0002C358 File Offset: 0x0002A558
		public static SystemSound Beep
		{
			get
			{
				return new SystemSound("Beep");
			}
		}

		/// <summary>Gets the sound associated with the <see langword="Exclamation" /> program event in the current Windows sound scheme.</summary>
		/// <returns>A <see cref="T:System.Media.SystemSound" /> associated with the <see langword="Exclamation" /> program event in the current Windows sound scheme.</returns>
		// Token: 0x17000194 RID: 404
		// (get) Token: 0x06000A15 RID: 2581 RVA: 0x0002C364 File Offset: 0x0002A564
		public static SystemSound Exclamation
		{
			get
			{
				return new SystemSound("Exclamation");
			}
		}

		/// <summary>Gets the sound associated with the <see langword="Hand" /> program event in the current Windows sound scheme.</summary>
		/// <returns>A <see cref="T:System.Media.SystemSound" /> associated with the <see langword="Hand" /> program event in the current Windows sound scheme.</returns>
		// Token: 0x17000195 RID: 405
		// (get) Token: 0x06000A16 RID: 2582 RVA: 0x0002C370 File Offset: 0x0002A570
		public static SystemSound Hand
		{
			get
			{
				return new SystemSound("Hand");
			}
		}

		/// <summary>Gets the sound associated with the <see langword="Question" /> program event in the current Windows sound scheme.</summary>
		/// <returns>A <see cref="T:System.Media.SystemSound" /> associated with the <see langword="Question" /> program event in the current Windows sound scheme.</returns>
		// Token: 0x17000196 RID: 406
		// (get) Token: 0x06000A17 RID: 2583 RVA: 0x0002C37C File Offset: 0x0002A57C
		public static SystemSound Question
		{
			get
			{
				return new SystemSound("Question");
			}
		}
	}
}
