using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Foliage;
using Sirenix.OdinInspector;

namespace StudioNeeco
{
    [CreateAssetMenu(fileName="EditorData", menuName="NeverAlone/EditorData")]
    public class NeverAloneEditorData : ScriptableObject
    {
        [ShowInInspector]

        [PreviewField(75)]
        public Texture texture;
        
        [ShowInInspector]

        [PreviewField(75)]
        public Material material;
        public GameObject objectFromMaterialPrefab;

        private bool NoTextureAssigned() {
            return this.texture == null;
        }
        private bool NoMaterialAssigned() {
            return this.material == null;
        }

        [HideIf("NoTextureAssigned")]
        [Button("Create Material")]
        private void CreateNewMaterial()
        {
            Debug.Log("Create New Material");
            Material material = new Material (Shader.Find("Unlit/Transparent"));
            material.SetTexture(this.texture.name, this.texture);
            material.mainTexture = this.texture;
            AssetDatabase.CreateAsset(material, "Assets/NeverAlone/Materials/" + this.texture.name + ".mat");
            this.material = material;
        }
        [HideIf("NoMaterialAssigned")]
        [Button("Create Object")]
        private void CreateObjectFromMaterial()
        {
            Debug.Log("CreateObjectFromMaterial");
            GameObject newObject = Instantiate(this.objectFromMaterialPrefab);
            newObject.name = this.material.name;
            newObject.GetComponent<MeshRenderer>().sharedMaterial = this.material;
            newObject.GetComponent<Foliage2D>().RebuildMesh();
        }
        // public float foliageMultipliers = 1f;
        // [Button("Set Foliage Multipliers")]
        // public void SetFoliageMultipliers() {
        //     Foliage2D_Animation[] foliageObjects = Object.FindObjectsOfType<Foliage2D_Animation>();
        //     foreach (Foliage2D_Animation foliageObject in foliageObjects) {
        //         foliageObject.offsetMultiplier = this.foliageMultipliers;
        //     }

        // }
        // public float foliageSpeed = 1f;
        // [Button("Set Foliage Animation Speed")]
        // public void SetFoliageSpeed() {
        //     Foliage2D_Animation[] foliageObjects = Object.FindObjectsOfType<Foliage2D_Animation>();
        //     foreach (Foliage2D_Animation foliageObject in foliageObjects) {
        //         foliageObject.animationSpeed = this.foliageSpeed;
        //     }
        // }
        // public float foliageMaxWait = 0f;
        // [Button("Set Foliage Max Wait")]
        // public void SetFoliageMaxWait() {
        //     Foliage2D_Animation[] foliageObjects = Object.FindObjectsOfType<Foliage2D_Animation>();
        //     foreach (Foliage2D_Animation foliageObject in foliageObjects) {
        //         foliageObject.maxSeconds = this.foliageMaxWait;
        //     }
        // }
        
        // [Button("Update Foliage")]
        // public void UpdateFoliage() {
        //     this.SetFoliageMaxSpeed();
        //     this.SetFoliageMinSpeed();
        //     this.SetFoliageMaxWait();
        //     this.SetFoliageMinWait();
        // }
    }
}