﻿using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using System;
using System.IO;
using System.Text;
using PokemonBattelePokemon;

public class GetCanUsePokemonJson 
{
    private const string path = "Assets/Resources/Config/AllPokemons.json";

    [MenuItem("Pokemon/获得加入的精灵列表")]
    public static void Get()
    {
        var list = SearchPokemon()
            .Where(x => "" != x)
            .ToList()
            .ConvertAll(x => Convert.ToInt32(x));
        var str = JsonConvert.SerializeObject(list);
        var SkillPools = SearchGameObject.SearchGameObjectList<SkillPool>("Assets/Skill/SkillPoolAsset")
            .ConvertAll(x=>x.PokemonID);
        foreach(int id in list)
        {
            if (SkillPools.Contains(id)) continue;
            SkillPool skillPool = ScriptableObject.CreateInstance<SkillPool>();
            skillPool.PokemonID = id;
            AssetDatabase.CreateAsset(skillPool, "Assets/Skill/SkillPoolAsset/" + id + ".asset");
        }
        File.WriteAllText(path, str, Encoding.UTF8);
    }

    private static List<string> SearchPokemon(string path = "Assets/Resources/PokemonPrefab")
    {
        string[] guids = AssetDatabase.FindAssets("t:GameObject", new string[] { path });
        //从GUID获得资源所在路径
        List<string> paths = new List<string>();
        guids.ToList().ForEach(m =>
        paths.Add(
             Regex.Replace(AssetDatabase.GUIDToAssetPath(m), @"[^\d]*", "")));

        return paths;

    }
}