using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace QFSW.QC.Extras
{
	// Token: 0x0200000F RID: 15
	public static class ScreenCommands
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000044 RID: 68 RVA: 0x00003683 File Offset: 0x00001883
		// (set) Token: 0x06000045 RID: 69 RVA: 0x0000368A File Offset: 0x0000188A
		private static bool Fullscreen
		{
			get
			{
				return Screen.fullScreen;
			}
			set
			{
				Screen.fullScreen = value;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000046 RID: 70 RVA: 0x00003692 File Offset: 0x00001892
		private static float DPI
		{
			get
			{
				return Screen.dpi;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000047 RID: 71 RVA: 0x00003699 File Offset: 0x00001899
		// (set) Token: 0x06000048 RID: 72 RVA: 0x000036A0 File Offset: 0x000018A0
		private static ScreenOrientation Orientation
		{
			get
			{
				return Screen.orientation;
			}
			set
			{
				Screen.orientation = value;
			}
		}

		// Token: 0x06000049 RID: 73 RVA: 0x000036A8 File Offset: 0x000018A8
		private static Resolution GetCurrentResolution()
		{
			return new Resolution
			{
				width = Screen.width,
				height = Screen.height,
				refreshRate = Screen.currentResolution.refreshRate
			};
		}

		// Token: 0x0600004A RID: 74 RVA: 0x000036EA File Offset: 0x000018EA
		private static IEnumerable<Resolution> GetSupportedResolutions()
		{
			foreach (Resolution resolution in Screen.resolutions)
			{
				yield return resolution;
			}
			Resolution[] array = null;
			yield break;
		}

		// Token: 0x0600004B RID: 75 RVA: 0x000036F3 File Offset: 0x000018F3
		[Command("resolution", Platform.AllPlatforms, MonoTargetType.Single)]
		private static void SetResolution(int x, int y)
		{
			ScreenCommands.SetResolution(x, y, Screen.fullScreen);
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00003701 File Offset: 0x00001901
		private static void SetResolution(int x, int y, bool fullscreen)
		{
			Screen.SetResolution(x, y, fullscreen);
		}

		// Token: 0x0600004D RID: 77 RVA: 0x0000370B File Offset: 0x0000190B
		private static void CaptureScreenshot([CommandParameterDescription("The name of the file to save the screenshot in")] string filename, [CommandParameterDescription("Factor by which to increase resolution")] int superSize = 1)
		{
			ScreenCapture.CaptureScreenshot(filename, superSize);
		}

		// Token: 0x02000022 RID: 34
		[CompilerGenerated]
		private sealed class <GetSupportedResolutions>d__9 : IEnumerable<Resolution>, IEnumerable, IEnumerator<Resolution>, IEnumerator, IDisposable
		{
			// Token: 0x06000091 RID: 145 RVA: 0x0000436A File Offset: 0x0000256A
			[DebuggerHidden]
			public <GetSupportedResolutions>d__9(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
			}

			// Token: 0x06000092 RID: 146 RVA: 0x00004384 File Offset: 0x00002584
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x06000093 RID: 147 RVA: 0x00004388 File Offset: 0x00002588
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				if (num != 0)
				{
					if (num != 1)
					{
						return false;
					}
					this.<>1__state = -1;
					i++;
				}
				else
				{
					this.<>1__state = -1;
					array = Screen.resolutions;
					i = 0;
				}
				if (i >= array.Length)
				{
					array = null;
					return false;
				}
				Resolution resolution = array[i];
				this.<>2__current = resolution;
				this.<>1__state = 1;
				return true;
			}

			// Token: 0x17000011 RID: 17
			// (get) Token: 0x06000094 RID: 148 RVA: 0x0000440F File Offset: 0x0000260F
			Resolution IEnumerator<Resolution>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06000095 RID: 149 RVA: 0x00004417 File Offset: 0x00002617
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x17000012 RID: 18
			// (get) Token: 0x06000096 RID: 150 RVA: 0x0000441E File Offset: 0x0000261E
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06000097 RID: 151 RVA: 0x0000442C File Offset: 0x0000262C
			[DebuggerHidden]
			IEnumerator<Resolution> IEnumerable<Resolution>.GetEnumerator()
			{
				ScreenCommands.<GetSupportedResolutions>d__9 result;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Environment.CurrentManagedThreadId)
				{
					this.<>1__state = 0;
					result = this;
				}
				else
				{
					result = new ScreenCommands.<GetSupportedResolutions>d__9(0);
				}
				return result;
			}

			// Token: 0x06000098 RID: 152 RVA: 0x00004463 File Offset: 0x00002663
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<UnityEngine.Resolution>.GetEnumerator();
			}

			// Token: 0x0400004E RID: 78
			private int <>1__state;

			// Token: 0x0400004F RID: 79
			private Resolution <>2__current;

			// Token: 0x04000050 RID: 80
			private int <>l__initialThreadId;

			// Token: 0x04000051 RID: 81
			private Resolution[] <>7__wrap1;

			// Token: 0x04000052 RID: 82
			private int <>7__wrap2;
		}
	}
}
