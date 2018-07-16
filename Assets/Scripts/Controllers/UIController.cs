﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TinyTeam.UI;
using UnityEngine;

class UIController:SingletonMonobehavior<UIController>
{
    public Sprite border1, border2;
    private void OnEnable()
    {
        BeginBattleSystem.BeginBattleEvent += ShowBattleUI;
        EndBattleSystem.EndBattleEvent += CloseBattleUI;

    }

    private void Start()
    {
        TTUIPage.ShowPage<UI_PokemonBagIcon>();
        TTUIPage.ShowPage<UI_BagIcon>();
    }
    private void ShowBattleUI()
    {
        TTUIPage.ShowPage<UIBattle_Buttons>();
        TTUIPage.ShowPage<UIBattle_EnemyInfo>();
        TTUIPage.ShowPage<UIBattle_PokemonInfos>();

        TTUIPage.ClosePage<UI_PokemonBagIcon>();
        TTUIPage.ClosePage<UI_BagIcon>();
    }

    private void CloseBattleUI()
    {
        GameContext context = Contexts.sharedInstance.game;
        GameEntity[] entities = context.GetGroup(GameMatcher.BattlePokemonData).GetEntities();
        

        TTUIPage.ClosePage<UIBattle_Buttons>();
        TTUIPage.ClosePage<UIBattle_EnemyInfo>();
        TTUIPage.ClosePage<UIBattle_PokemonInfos>();
        TTUIPage.ClosePage<UIBattle_Skills>();
        TTUIPage.ClosePage<UIBattle_Pokemons>();
        TTUIPage.ShowPage<UI_PokemonBagIcon>();
        TTUIPage.ShowPage<UI_BagIcon>();

        
    }
}
