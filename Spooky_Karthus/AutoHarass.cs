﻿using EloBuddy;
using EloBuddy.SDK;
using T2IN1_Lib;

namespace Spooky_Karthus
{
    class Autoharass
    {
        public static void Execute7()
        {
            var qtarget = TargetSelector.GetTarget(SpellsManager.Q.Range, DamageType.Magical);

            if ((qtarget == null) || qtarget.IsInvulnerable)
                return;
            //Cast Q
            if (qtarget.IsValidTarget(SpellsManager.Q.Range+20) && SpellsManager.Q.IsReady())
                SpellsManager.Q.GetPrediction(qtarget);
            SpellsManager.Q.TryToCast(qtarget, Menus.HarassMenu);

        }

        public static void Execute8()
        {
            var etarget = TargetSelector.GetTarget(SpellsManager.E.Range, DamageType.Magical);

            if ((etarget == null) || etarget.IsInvulnerable)
                return;
            //Cast E
            if (etarget.IsValidTarget(SpellsManager.E.Range) && SpellsManager.E.IsReady())
                SpellsManager.E.TryToCast(etarget, Menus.HarassMenu);
        }

        public static void Execute12()
        {
            var q1target = TargetSelector.GetTarget(SpellsManager.Q1.Range, DamageType.Magical);

            if ((q1target == null) || q1target.IsInvulnerable)
                return;
            //Cast Q
            if (q1target.IsValidTarget(SpellsManager.Q1.Range + 20) && SpellsManager.Q1.IsReady())
                SpellsManager.Q1.GetPrediction(q1target);
            SpellsManager.Q1.TryToCast(q1target, Menus.HarassMenu);
        }
    }
}