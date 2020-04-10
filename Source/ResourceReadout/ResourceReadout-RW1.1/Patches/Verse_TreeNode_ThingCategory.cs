using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using HarmonyLib;

namespace ResourceReadout.Patches
{
#if DEBUG
	[HarmonyPatch(typeof(TreeNode_ThingCategory))]
	public static class Verse_TreeNode_ThingCategory
	{
		[HarmonyPatch(MethodType.Constructor, typeof(ThingCategoryDef))]
		[HarmonyPostfix]
		public static void Postfix(int ___openBits)
		{
			Log.Message("TreeNode_ThingCategory_Postfix");
			Log.Message($"Value of open bits after constructor call: {Convert.ToString(___openBits, 2)}");
		}
	}
#endif
}
