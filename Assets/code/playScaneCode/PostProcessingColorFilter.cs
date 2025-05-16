using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessingColorFilter : MonoBehaviour
{
    private PostProcessVolume postProcessVolume;
    private ColorGrading colorGrading;  // для изменения цвета

    private settingsToPlay settingsToPlay;

    void Start()
    {
        settingsToPlay=FindObjectOfType<settingsToPlay>();
        //Debug.Log(f.filter);

        GameObject volumeObject = GameObject.Find("Post-process Volume");

        if (volumeObject != null)
        {
            postProcessVolume = volumeObject.GetComponent<PostProcessVolume>();

            if (postProcessVolume != null && postProcessVolume.profile != null)
            {
                postProcessVolume.profile.TryGetSettings(out colorGrading);

                if (colorGrading != null)
                {
                 //   if(settingsToPlay!=null){
                        colorGrading.hueShift.value = settingsToPlay.Instance.filter;
                        //Debug.Log("Hue Shift переустановлен");
                  //  }
                  //  else{
                   //     colorGrading.hueShift.value = 0f;
                   // }
                }
                else
                {
                    Debug.LogError("ColorGrading не найден в профиле.");
                }
            }
            else
            {
                Debug.LogError("PostProcessVolume или его профиль не найден.");
            }
        }
        else
        {
            Debug.LogError("Post-process Volume не найден.");
        }
    }

    void Update(){
       /* if (Input.GetKeyDown(KeyCode.Z))
        {
           colorGrading.hueShift.value = -180f;
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            colorGrading.hueShift.value = -150f;
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            colorGrading.hueShift.value = -120f;
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            colorGrading.hueShift.value = -90f;
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            colorGrading.hueShift.value = -60f;
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            colorGrading.hueShift.value = -30f;
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            colorGrading.hueShift.value = 0f;
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            colorGrading.hueShift.value = 30f;
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            colorGrading.hueShift.value = 60f;
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            colorGrading.hueShift.value = 90f;
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            colorGrading.hueShift.value = 120f;
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
           colorGrading.hueShift.value = 150f;
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
           colorGrading.hueShift.value = 106f;
        }*/
    }


}
