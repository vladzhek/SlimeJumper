using System.Collections.Generic;
using UnityEngine;

namespace Script.Gameplay.Data
{
    [CreateAssetMenu(fileName = "MaterialsData", menuName = "Data/MaterialsData")]
    public class MaterialsData : ScriptableObject
    {
        public Material PlayerMaterial;
        public Material PlayerColorsMaterial;
        public List<MaterialComponent> Materials;
    }
}