using System.Collections.Generic;
using UnityEngine;

namespace Nocturne
{
    public class CustomRenderUpdater : MonoBehaviour
    {

      [SerializeField] 
      private CustomRenderTexture customRenderTexture;

      [SerializeField] 
      private Material corruptionMaterial;

      [SerializeField] 
      private Material customRenderMaterial;
      
      private List<CustomRenderTextureUpdateZone> zones = new List<CustomRenderTextureUpdateZone>();

      [SerializeField]
      private float corruptX = 10;
      [SerializeField]
      private float corruptY = 10;
      
      private float targetCorruptX = 10;
      private float targetCorruptY = 10; 
      
      private static readonly int GlitchX = Shader.PropertyToID("_GlitchX");
      private static readonly int GlitchY = Shader.PropertyToID("_GlitchY");
      private static readonly int Method = Shader.PropertyToID("_Method");

      private float velocityX = 0;
      private float velocityY = 0;

      [SerializeField]
      private float smoothTime = 0.1f;

      public int corruptMethod = 1;
      public int fixMethod = 3;
      
      public void RenderFrame()
      {
        customRenderTexture.Update();
      }
      
      public void InitializeTexture()
      {
        customRenderTexture.Initialize();
      }

      public void Corrupt()
      {
        customRenderMaterial.SetInt(Method, corruptMethod);
        targetCorruptY = corruptY;
        targetCorruptX = corruptX;
      }

      public void Fix()
      {
        customRenderMaterial.SetInt(Method, fixMethod);
        targetCorruptY = 0;
        targetCorruptX = 0;
      }

      private void Update()
      {
        var currentY = corruptionMaterial.GetFloat(GlitchY);
        currentY = Mathf.SmoothDamp(currentY, targetCorruptY, ref velocityY, smoothTime);// Lerp(current, corruptAmount, 0.01f);
        corruptionMaterial.SetFloat(GlitchY,currentY);
        
        var currentX = corruptionMaterial.GetFloat(GlitchX);
        currentX = Mathf.SmoothDamp(currentX, targetCorruptX, ref velocityX, smoothTime);// Lerp(current, corruptAmount, 0.01f);
        corruptionMaterial.SetFloat(GlitchX,currentX);
      }

    }
}
