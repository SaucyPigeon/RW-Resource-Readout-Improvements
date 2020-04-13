using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RimWorld;
using HarmonyLib;
using Verse;
using System.Reflection.Emit;
using UnityEngine;

namespace ResourceReadout.Patches
{
	[HarmonyPatch(typeof(RimWorld.ResourceReadout))]
	public static class RimWorld_ResourceReadout
	{
		/*
		Prevent vanilla bug that makes "Foods" category open by default
		*/
		[HarmonyPatch(MethodType.Constructor)]
		[HarmonyPostfix]
		public static void Constructor_Postfix(List<ThingCategoryDef> ___RootThingCategories)
		{
			foreach (var def in ___RootThingCategories)
			{
				def.treeNode.SetOpen(TreeOpenMasks.ResourceReadout, false);
			}
		}

		/*
		Add invisible button to simple view's icons for selecting items.
		*/
		[HarmonyPatch("DrawIcon")]
		[HarmonyTranspiler]
		public static IEnumerable<CodeInstruction> DrawIcon_Transpiler(IEnumerable<CodeInstruction> codeInstructions, ILGenerator ilGenerator)
		{
			var mi_Widgets_ThingIcon = AccessTools.Method(typeof(Widgets), nameof(Widgets.ThingIcon), parameters: new[] { typeof(Rect), typeof(ThingDef), typeof(ThingDef), typeof(float) });
			var mi_Widgets_ButtonInvisible = AccessTools.Method(typeof(Widgets), nameof(Widgets.ButtonInvisible));

			var mi_Patch_SelectAllOnMap = AccessTools.Method(typeof(Loader), nameof(Loader.SelectAllOnMap));

			foreach (var instruction in codeInstructions)
			{
				yield return instruction;

				if (instruction.Calls(mi_Widgets_ThingIcon))
				{
					//if (Widgets.ButtonInvisible(rect, false)
					//{
					//	SelectAllOnMap(thingDef)
					//}
					
					var jumpToEnd = ilGenerator.DefineLabel();

					yield return new CodeInstruction(OpCodes.Ldloc_0);
					yield return new CodeInstruction(OpCodes.Ldc_I4_0);
					yield return new CodeInstruction(OpCodes.Call, mi_Widgets_ButtonInvisible);

					yield return new CodeInstruction(OpCodes.Brfalse_S, jumpToEnd);

					yield return new CodeInstruction(OpCodes.Ldarg_3);
					yield return new CodeInstruction(OpCodes.Call, mi_Patch_SelectAllOnMap);

					yield return new CodeInstruction(OpCodes.Nop) { labels = new List<Label>() { jumpToEnd } };
				}
			}
		}
	}
}
