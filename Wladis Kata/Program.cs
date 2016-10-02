﻿using System;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Events;

namespace Wladis_Kata
{
    internal class Loader
    {
        private static bool _lockedSpellcasts;

        private static bool LockedSpellCasts
        {
            get { return _lockedSpellcasts; }
            set
            {
                _lockedSpellcasts = value;
                if (value)
                {
                    _lockedTime = Core.GameTickCount;
                }
            }
        }

        private static int _lockedTime;

        private static void Main(string[] args)
        {
            Loading.OnLoadingComplete += Loading_OnLoadingComplete;
        }

        private static void Loading_OnLoadingComplete(EventArgs bla)
        {
            if (Player.Instance.Hero != Champion.Katarina) return;
            SpellsManager.InitializeSpells();
            Menus.CreateMenu();
            ModeManager.InitializeModes();
            DrawingsManager.InitializeDrawings();

            Obj_AI_Base.OnProcessSpellCast += delegate(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
            {
                if (sender.IsMe && (int) args.Slot == 3)
                {
                    LockedSpellCasts = true;
                }
            };

            Obj_AI_Base.OnSpellCast += delegate(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
            {
                if (sender.IsMe && (int) args.Slot == 3)
                {
                    LockedSpellCasts = false;
                }
            };

            Spellbook.OnCastSpell += delegate(Spellbook sender, SpellbookCastSpellEventArgs args)
            {
                if (sender.Owner.IsMe && (int) args.Slot == 3 && Player.GetSpell(args.Slot).IsReady)
                {
                    if (LockedSpellCasts)
                    {
                        args.Process = false;
                    }
                    else
                    {
                        LockedSpellCasts = true;
                    }

                }
            };

            Game.OnTick += delegate
            {
                if (_lockedTime > 0 && LockedSpellCasts && Core.GameTickCount - _lockedTime > 250)
                {
                    LockedSpellCasts = false;
                }
            };

            Chat.Print(""<font color='#04B404'>Wladis Kata Loaded </font>");
            Chat.Print("Credits to Tarakan and Hellsing");
        }
    }
}
