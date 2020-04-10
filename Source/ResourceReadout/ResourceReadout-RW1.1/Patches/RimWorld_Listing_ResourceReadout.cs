using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using Verse;
using RimWorld;
using UnityEngine;
using System.Reflection.Emit;

namespace ResourceReadout.Patches
{
	[HarmonyPatch(typeof(Listing_ResourceReadout))]
	public static class RimWorld_Listing_ResourceReadout
	{

//#if DEBUG
		[HarmonyPatch("DoThingDef")]
		[HarmonyTranspiler]
		public static IEnumerable<CodeInstruction> DoThingDef_Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator ilGenerator)
		{
#if DEBUG
			Log.Message("Listing_ResourceReadout.DoCategory_Transpiler - start");
#endif
			var mi_GUI_DrawTexture = AccessTools.Method(typeof(GUI), nameof(GUI.DrawTexture), parameters: new[] { typeof(Rect), typeof(Texture) });
			var mi_Widgets_ButtonInvisible = AccessTools.Method(typeof(Widgets), nameof(Widgets.ButtonInvisible));
			var fi_TreeNode_ThingCategory_catDef = AccessTools.Field(typeof(TreeNode_ThingCategory), nameof(TreeNode_ThingCategory.catDef));
			var mi_Patch_SelectAllOnMap = AccessTools.Method(typeof(Loader), nameof(Loader.SelectAllOnMap));

			var ldarg_0_last = instructions.Where(x => x.IsLdarg(0)).Last();

			foreach (var instruction in instructions)
			{
				if (instruction == ldarg_0_last)
				{
					// if (Widgets.ButtonInvisible(rect, false))
					// {
					//     SelectAllOnMap(thingDef)
					// }
					var jumpToEnd = ilGenerator.DefineLabel();

					yield return new CodeInstruction(OpCodes.Ldloc_2);
					yield return new CodeInstruction(OpCodes.Ldc_I4_0);
					yield return new CodeInstruction(OpCodes.Call, mi_Widgets_ButtonInvisible);

					yield return new CodeInstruction(OpCodes.Brfalse_S, jumpToEnd);

					yield return new CodeInstruction(OpCodes.Ldarg_1);
					yield return new CodeInstruction(OpCodes.Call, mi_Patch_SelectAllOnMap);

					yield return new CodeInstruction(OpCodes.Nop) { labels = new List<Label>() { jumpToEnd } };
				}
				yield return instruction;
			}
#if DEBUG
			Log.Message("Listing_ResourceReadout.DoCategory_Transpiler - end");
#endif
		}
		//#endif



		[HarmonyPatch(nameof(Listing_ResourceReadout.DoCategory))]
		[HarmonyPostfix]
		public static void DoCategory_Postfix(TreeNode_ThingCategory node, int openMask)
		{
//#if DEBUG
//			Log.Message("RimWorld.Listing_ResourceReadout_Postfix (start)");
//#endif

			if (Loader.NodeOpenTracker == null)
			{
				Loader.NodeOpenTracker = new NodeOpenTracker();
			}

			Loader.NodeOpenTracker.SetNode(node, openMask);

//#if DEBUG
//			Log.Message("RimWorld.Listing_ResourceReadout_Postfix (finish)");
//#endif
		}
	}
}
