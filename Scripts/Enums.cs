using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enums
{
    public enum CharacterType { Melee, Range }

    public enum ExpeditionState { Idle, Run, Attack, Death }

    public enum ItemType { Weapon, Potion, Material,Recipe }

    public enum EnemyState { Idle, Walk, Attack, Skill, Buff }

    public enum SkillType { Player, Monster}

    public enum CharacterGrade { S, A, B, C }
    
    public enum BGM { Start, Story, Forge, Battle , Stage1, Stage2, Stage3, Stage4, Stage5, Stage6, Stage7, Stage8, Stage9, Stage10, Stage11, Stage12, Stage13, Stage14, Stage15 }

    public enum SFX { Button, PlayerMelee, PlayerRange, EnemyMelee, EnemyRange, PlayerSkill, EnemySkill, Hammering, Blacksmith_Fire,HammeringFail,
        Etc_Aghhhhhhhhh,
        Etc_ImSleepSugo,
        Etc_IsItReallll,
        Etc_KingAaa,
        Etc_Mmmmm,
        Npc_Hello,
        Npc_WhoAreYou,
        Npc_YesYes,
        OrderComplete_KingaKinga,
        OrderComplete_ThankYouKinga,
        OrderComplete_YesGoodbye,
        OrderComplete_YesILoveYou,
        OrderComplete_YesSeeYou,
        OrderReject_UpSeo,
        OrderReject_UpSeoQuiet,
        OrderReject_YourFace
    }
}
