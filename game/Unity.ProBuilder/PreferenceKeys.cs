using System;

namespace UnityEngine.ProBuilder
{
	// Token: 0x02000031 RID: 49
	internal static class PreferenceKeys
	{
		// Token: 0x0600020C RID: 524 RVA: 0x00016B1C File Offset: 0x00014D1C
		// Note: this type is marked as 'beforefieldinit'.
		static PreferenceKeys()
		{
		}

		// Token: 0x040000B6 RID: 182
		public const string pluginTitle = "ProBuilder";

		// Token: 0x040000B7 RID: 183
		internal const float k_MaxPointDistanceFromControl = 20f;

		// Token: 0x040000B8 RID: 184
		internal const char DEGREE_SYMBOL = '°';

		// Token: 0x040000B9 RID: 185
		internal const char CMD_SUPER = '⌘';

		// Token: 0x040000BA RID: 186
		internal const char CMD_SHIFT = '⇧';

		// Token: 0x040000BB RID: 187
		internal const char CMD_OPTION = '⌥';

		// Token: 0x040000BC RID: 188
		internal const char CMD_ALT = '⎇';

		// Token: 0x040000BD RID: 189
		internal const char CMD_DELETE = '⌫';

		// Token: 0x040000BE RID: 190
		internal static readonly Color proBuilderBlue = new Color(0f, 0.682f, 0.937f, 1f);

		// Token: 0x040000BF RID: 191
		internal static readonly Color proBuilderLightGray = new Color(0.35f, 0.35f, 0.35f, 0.4f);

		// Token: 0x040000C0 RID: 192
		internal static readonly Color proBuilderDarkGray = new Color(0.1f, 0.1f, 0.1f, 0.3f);

		// Token: 0x040000C1 RID: 193
		public const int menuEditor = 100;

		// Token: 0x040000C2 RID: 194
		public const int menuSelection = 200;

		// Token: 0x040000C3 RID: 195
		public const int menuGeometry = 200;

		// Token: 0x040000C4 RID: 196
		public const int menuActions = 300;

		// Token: 0x040000C5 RID: 197
		public const int menuMaterialColors = 400;

		// Token: 0x040000C6 RID: 198
		public const int menuVertexColors = 400;

		// Token: 0x040000C7 RID: 199
		public const int menuRepair = 600;

		// Token: 0x040000C8 RID: 200
		public const int menuMisc = 600;

		// Token: 0x040000C9 RID: 201
		public const int menuExport = 800;

		// Token: 0x040000CA RID: 202
		[Obsolete("Use Pref<T> or Settings class directly.")]
		internal const string pbDefaultEditLevel = "pbDefaultEditLevel";

		// Token: 0x040000CB RID: 203
		[Obsolete("Use Pref<T> or Settings class directly.")]
		internal const string pbDefaultSelectionMode = "pbDefaultSelectionMode";

		// Token: 0x040000CC RID: 204
		[Obsolete("Use Pref<T> or Settings class directly.")]
		internal const string pbHandleAlignment = "pbHandleAlignment";

		// Token: 0x040000CD RID: 205
		[Obsolete("Use Pref<T> or Settings class directly.")]
		internal const string pbVertexColorTool = "pbVertexColorTool";

		// Token: 0x040000CE RID: 206
		[Obsolete("Use Pref<T> or Settings class directly.")]
		internal const string pbToolbarLocation = "pbToolbarLocation";

		// Token: 0x040000CF RID: 207
		[Obsolete("Use Pref<T> or Settings class directly.")]
		internal const string pbDefaultEntity = "pbDefaultEntity";

		// Token: 0x040000D0 RID: 208
		[Obsolete("Use Pref<T> or Settings class directly.")]
		internal const string pbExtrudeMethod = "pbExtrudeMethod";

		// Token: 0x040000D1 RID: 209
		[Obsolete("Use Pref<T> or Settings class directly.")]
		internal const string pbDefaultStaticFlags = "pbDefaultStaticFlags";

		// Token: 0x040000D2 RID: 210
		[Obsolete("Use Pref<T> or Settings class directly.")]
		internal const string pbSelectedFaceColor = "pbDefaultFaceColor";

		// Token: 0x040000D3 RID: 211
		[Obsolete("Use Pref<T> or Settings class directly.")]
		internal const string pbWireframeColor = "pbDefaultEdgeColor";

		// Token: 0x040000D4 RID: 212
		[Obsolete("Use Pref<T> or Settings class directly.")]
		internal const string pbUnselectedEdgeColor = "pbUnselectedEdgeColor";

		// Token: 0x040000D5 RID: 213
		[Obsolete("Use Pref<T> or Settings class directly.")]
		internal const string pbSelectedEdgeColor = "pbSelectedEdgeColor";

		// Token: 0x040000D6 RID: 214
		[Obsolete("Use Pref<T> or Settings class directly.")]
		internal const string pbSelectedVertexColor = "pbDefaultSelectedVertexColor";

		// Token: 0x040000D7 RID: 215
		[Obsolete("Use Pref<T> or Settings class directly.")]
		internal const string pbUnselectedVertexColor = "pbDefaultVertexColor";

		// Token: 0x040000D8 RID: 216
		[Obsolete("Use Pref<T> or Settings class directly.")]
		internal const string pbPreselectionColor = "pbPreselectionColor";

		// Token: 0x040000D9 RID: 217
		[Obsolete("Use Pref<T> or Settings class directly.")]
		internal const string pbDefaultOpenInDockableWindow = "pbDefaultOpenInDockableWindow";

		// Token: 0x040000DA RID: 218
		[Obsolete("Use Pref<T> or Settings class directly.")]
		internal const string pbEditorPrefVersion = "pbEditorPrefVersion";

		// Token: 0x040000DB RID: 219
		[Obsolete("Use Pref<T> or Settings class directly.")]
		internal const string pbEditorShortcutsVersion = "pbEditorShortcutsVersion";

		// Token: 0x040000DC RID: 220
		[Obsolete("Use Pref<T> or Settings class directly.")]
		internal const string pbDefaultCollider = "pbDefaultCollider";

		// Token: 0x040000DD RID: 221
		[Obsolete("Use Pref<T> or Settings class directly.")]
		internal const string pbForceConvex = "pbForceConvex";

		// Token: 0x040000DE RID: 222
		[Obsolete("Use Pref<T> or Settings class directly.")]
		internal const string pbVertexColorPrefs = "pbVertexColorPrefs";

		// Token: 0x040000DF RID: 223
		[Obsolete("Use Pref<T> or Settings class directly.")]
		internal const string pbShowEditorNotifications = "pbShowEditorNotifications";

		// Token: 0x040000E0 RID: 224
		[Obsolete("Use Pref<T> or Settings class directly.")]
		internal const string pbDragCheckLimit = "pbDragCheckLimit";

		// Token: 0x040000E1 RID: 225
		[Obsolete("Use Pref<T> or Settings class directly.")]
		internal const string pbForceVertexPivot = "pbForceVertexPivot";

		// Token: 0x040000E2 RID: 226
		[Obsolete("Use Pref<T> or Settings class directly.")]
		internal const string pbForceGridPivot = "pbForceGridPivot";

		// Token: 0x040000E3 RID: 227
		[Obsolete("Use Pref<T> or Settings class directly.")]
		internal const string pbManifoldEdgeExtrusion = "pbManifoldEdgeExtrusion";

		// Token: 0x040000E4 RID: 228
		[Obsolete("Use Pref<T> or Settings class directly.")]
		internal const string pbPerimeterEdgeBridgeOnly = "pbPerimeterEdgeBridgeOnly";

		// Token: 0x040000E5 RID: 229
		[Obsolete("Use Pref<T> or Settings class directly.")]
		internal const string pbPBOSelectionOnly = "pbPBOSelectionOnly";

		// Token: 0x040000E6 RID: 230
		[Obsolete("Use Pref<T> or Settings class directly.")]
		internal const string pbCloseShapeWindow = "pbCloseShapeWindow";

		// Token: 0x040000E7 RID: 231
		[Obsolete("Use Pref<T> or Settings class directly.")]
		internal const string pbUVEditorFloating = "pbUVEditorFloating";

		// Token: 0x040000E8 RID: 232
		[Obsolete("Use Pref<T> or Settings class directly.")]
		internal const string pbUVMaterialPreview = "pbUVMaterialPreview";

		// Token: 0x040000E9 RID: 233
		[Obsolete("Use Pref<T> or Settings class directly.")]
		internal const string pbShowSceneToolbar = "pbShowSceneToolbar";

		// Token: 0x040000EA RID: 234
		[Obsolete("Use Pref<T> or Settings class directly.")]
		internal const string pbNormalizeUVsOnPlanarProjection = "pbNormalizeUVsOnPlanarProjection";

		// Token: 0x040000EB RID: 235
		[Obsolete("Use Pref<T> or Settings class directly.")]
		internal const string pbStripProBuilderOnBuild = "pbStripProBuilderOnBuild";

		// Token: 0x040000EC RID: 236
		[Obsolete("Use Pref<T> or Settings class directly.")]
		internal const string pbDisableAutoUV2Generation = "pbDisableAutoUV2Generation";

		// Token: 0x040000ED RID: 237
		[Obsolete("Use Pref<T> or Settings class directly.")]
		internal const string pbShowSceneInfo = "pbShowSceneInfo";

		// Token: 0x040000EE RID: 238
		[Obsolete("Use Pref<T> or Settings class directly.")]
		internal const string pbEnableBackfaceSelection = "pbEnableBackfaceSelection";

		// Token: 0x040000EF RID: 239
		[Obsolete("Use Pref<T> or Settings class directly.")]
		internal const string pbVertexPaletteDockable = "pbVertexPaletteDockable";

		// Token: 0x040000F0 RID: 240
		[Obsolete("Use Pref<T> or Settings class directly.")]
		internal const string pbExtrudeAsGroup = "pbExtrudeAsGroup";

		// Token: 0x040000F1 RID: 241
		[Obsolete("Use Pref<T> or Settings class directly.")]
		internal const string pbUniqueModeShortcuts = "pbUniqueModeShortcuts";

		// Token: 0x040000F2 RID: 242
		[Obsolete("Use Pref<T> or Settings class directly.")]
		internal const string pbMaterialEditorFloating = "pbMaterialEditorFloating";

		// Token: 0x040000F3 RID: 243
		[Obsolete("Use Pref<T> or Settings class directly.")]
		internal const string pbShapeWindowFloating = "pbShapeWindowFloating";

		// Token: 0x040000F4 RID: 244
		[Obsolete("Use Pref<T> or Settings class directly.")]
		internal const string pbIconGUI = "pbIconGUI";

		// Token: 0x040000F5 RID: 245
		[Obsolete("Use Pref<T> or Settings class directly.")]
		internal const string pbShiftOnlyTooltips = "pbShiftOnlyTooltips";

		// Token: 0x040000F6 RID: 246
		[Obsolete("Use Pref<T> or Settings class directly.")]
		internal const string pbDrawAxisLines = "pbDrawAxisLines";

		// Token: 0x040000F7 RID: 247
		[Obsolete("Use Pref<T> or Settings class directly.")]
		internal const string pbCollapseVertexToFirst = "pbCollapseVertexToFirst";

		// Token: 0x040000F8 RID: 248
		[Obsolete("Use Pref<T> or Settings class directly.")]
		internal const string pbMeshesAreAssets = "pbMeshesAreAssets";

		// Token: 0x040000F9 RID: 249
		[Obsolete("Use Pref<T> or Settings class directly.")]
		internal const string pbElementSelectIsHamFisted = "pbElementSelectIsHamFisted";

		// Token: 0x040000FA RID: 250
		[Obsolete("Use Pref<T> or Settings class directly.")]
		internal const string pbFillHoleSelectsEntirePath = "pbFillHoleSelectsEntirePath";

		// Token: 0x040000FB RID: 251
		[Obsolete("Use Pref<T> or Settings class directly.")]
		internal const string pbDetachToNewObject = "pbDetachToNewObject";

		// Token: 0x040000FC RID: 252
		[Obsolete("Use Pref<T> or Settings class directly.")]
		internal const string pbPreserveFaces = "pbPreserveFaces";

		// Token: 0x040000FD RID: 253
		[Obsolete("Use Pref<T> or Settings class directly.")]
		internal const string pbDragSelectWholeElement = "pbDragSelectWholeElement";

		// Token: 0x040000FE RID: 254
		[Obsolete("Use Pref<T> or Settings class directly.")]
		internal const string pbShowPreselectionHighlight = "pbShowPreselectionHighlight";

		// Token: 0x040000FF RID: 255
		[Obsolete("Use Pref<T> or Settings class directly.")]
		internal const string pbRectSelectMode = "pbRectSelectMode";

		// Token: 0x04000100 RID: 256
		[Obsolete("Use Pref<T> or Settings class directly.")]
		internal const string pbDragSelectMode = "pbDragSelectMode";

		// Token: 0x04000101 RID: 257
		[Obsolete("Use Pref<T> or Settings class directly.")]
		internal const string pbShadowCastingMode = "pbShadowCastingMode";

		// Token: 0x04000102 RID: 258
		[Obsolete("Use Pref<T> or Settings class directly.")]
		internal const string pbEnableExperimental = "pbEnableExperimental";

		// Token: 0x04000103 RID: 259
		[Obsolete("Use Pref<T> or Settings class directly.")]
		internal const string pbCheckForProBuilderUpdates = "pbCheckForProBuilderUpdates";

		// Token: 0x04000104 RID: 260
		[Obsolete("Use Pref<T> or Settings class directly.")]
		internal const string pbManageLightmappingStaticFlag = "pbManageLightmappingStaticFlag";

		// Token: 0x04000105 RID: 261
		[Obsolete("Use Pref<T> or Settings class directly.")]
		internal const string pbShowMissingLightmapUvWarning = "pb_Lightmapping::showMissingLightmapUvWarning";

		// Token: 0x04000106 RID: 262
		[Obsolete("Use Pref<T> or Settings class directly.")]
		internal const string pbSelectedFaceDither = "pbSelectedFaceDither";

		// Token: 0x04000107 RID: 263
		[Obsolete("Use Pref<T> or Settings class directly.")]
		internal const string pbUseUnityColors = "pbUseUnityColors";

		// Token: 0x04000108 RID: 264
		[Obsolete("Use Pref<T> or Settings class directly.")]
		internal const string pbVertexHandleSize = "pbVertexHandleSize";

		// Token: 0x04000109 RID: 265
		[Obsolete("Use Pref<T> or Settings class directly.")]
		internal const string pbUVGridSnapValue = "pbUVGridSnapValue";

		// Token: 0x0400010A RID: 266
		[Obsolete("Use Pref<T> or Settings class directly.")]
		internal const string pbUVWeldDistance = "pbUVWeldDistance";

		// Token: 0x0400010B RID: 267
		[Obsolete("Use Pref<T> or Settings class directly.")]
		internal const string pbLineHandleSize = "pbLineHandleSize";

		// Token: 0x0400010C RID: 268
		[Obsolete("Use Pref<T> or Settings class directly.")]
		internal const string pbWireframeSize = "pbWireframeSize";

		// Token: 0x0400010D RID: 269
		[Obsolete("Use Pref<T> or Settings class directly.")]
		internal const string pbWeldDistance = "pbWeldDistance";

		// Token: 0x0400010E RID: 270
		[Obsolete("Use Pref<T> or Settings class directly.")]
		internal const string pbExtrudeDistance = "pbExtrudeDistance";

		// Token: 0x0400010F RID: 271
		[Obsolete("Use Pref<T> or Settings class directly.")]
		internal const string pbBevelAmount = "pbBevelAmount";

		// Token: 0x04000110 RID: 272
		[Obsolete("Use Pref<T> or Settings class directly.")]
		internal const string pbEdgeSubdivisions = "pbEdgeSubdivisions";

		// Token: 0x04000111 RID: 273
		[Obsolete("Use Pref<T> or Settings class directly.")]
		internal const string pbDefaultShortcuts = "pbDefaultShortcuts";

		// Token: 0x04000112 RID: 274
		[Obsolete("Use Pref<T> or Settings class directly.")]
		internal const string pbDefaultMaterial = "pbDefaultMaterial";

		// Token: 0x04000113 RID: 275
		[Obsolete("Use Pref<T> or Settings class directly.")]
		internal const string pbCurrentMaterialPalette = "pbCurrentMaterialPalette";

		// Token: 0x04000114 RID: 276
		[Obsolete("Use Pref<T> or Settings class directly.")]
		internal const string pbGrowSelectionUsingAngle = "pbGrowSelectionUsingAngle";

		// Token: 0x04000115 RID: 277
		[Obsolete("Use Pref<T> or Settings class directly.")]
		internal const string pbGrowSelectionAngle = "pbGrowSelectionAngle";

		// Token: 0x04000116 RID: 278
		[Obsolete("Use Pref<T> or Settings class directly.")]
		internal const string pbGrowSelectionAngleIterative = "pbGrowSelectionAngleIterative";

		// Token: 0x04000117 RID: 279
		[Obsolete("Use Pref<T> or Settings class directly.")]
		internal const string pbShowDetail = "pbShowDetail";

		// Token: 0x04000118 RID: 280
		[Obsolete("Use Pref<T> or Settings class directly.")]
		internal const string pbShowOccluder = "pbShowOccluder";

		// Token: 0x04000119 RID: 281
		[Obsolete("Use Pref<T> or Settings class directly.")]
		internal const string pbShowMover = "pbShowMover";

		// Token: 0x0400011A RID: 282
		[Obsolete("Use Pref<T> or Settings class directly.")]
		internal const string pbShowCollider = "pbShowCollider";

		// Token: 0x0400011B RID: 283
		[Obsolete("Use Pref<T> or Settings class directly.")]
		internal const string pbShowTrigger = "pbShowTrigger";

		// Token: 0x0400011C RID: 284
		[Obsolete("Use Pref<T> or Settings class directly.")]
		internal const string pbShowNoDraw = "pbShowNoDraw";

		// Token: 0x0400011D RID: 285
		[Obsolete("Use Pref<T> or Settings class directly.")]
		internal const string defaultUnwrapParameters = "pbDefaultUnwrapParameters";
	}
}
