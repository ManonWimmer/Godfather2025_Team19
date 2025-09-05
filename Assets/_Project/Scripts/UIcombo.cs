using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIcombo : MonoBehaviour
{
    [SerializeField] private List<Image> fond = new List<Image>();
    [SerializeField] private List<Image> un = new List<Image>();
    [SerializeField] private List<Image> deux = new List<Image>();
    [SerializeField] private List<Image> trois = new List<Image>();
    [SerializeField] private Image fleche;



    private void Desactive()
    {
        for (int i = 0; i < un.Count; i++)
        {
            fond[i].gameObject.SetActive(false);
            un[i].gameObject.SetActive(false);
            deux[i].gameObject.SetActive(false);
            trois[i].gameObject.SetActive(false);
        }
        fond[2].gameObject.SetActive(false);
    }



    public void UpdateCombo()
    {
        if (vanManager.jauge == 1f)
        {
            Debug.Log(fleche.rectTransform.rotation);
            //Debug.Log("Jauge 1");
            Desactive();
            fond[0].gameObject.SetActive(true);
            un[1].gameObject.SetActive(true);
            deux[0].gameObject.SetActive(true);
            trois[0].gameObject.SetActive(true);
            fleche.rectTransform.rotation = Quaternion.Euler(0, 0, 93);

        }
        else if (vanManager.jauge == 2f)
        {
            Debug.Log(fleche.rectTransform.rotation);
            //Debug.Log("Jauge 2");
            Desactive();
            fond[1].gameObject.SetActive(true);
            un[0].gameObject.SetActive(true);
            deux[1].gameObject.SetActive(true);
            trois[0].gameObject.SetActive(true);
            fleche.rectTransform.rotation = Quaternion.Euler(0, 0, 1);
        }
        else if (vanManager.jauge == 3f)
        {
            Debug.Log(fleche.rectTransform.rotation);
            //Debug.Log("Jauge 3");
            Desactive();
            fond[2].gameObject.SetActive(true);
            un[0].gameObject.SetActive(true);
            deux[0].gameObject.SetActive(true);
            trois[1].gameObject.SetActive(true);
            fleche.rectTransform.rotation = Quaternion.Euler(0, 0, 267);

        }
    }


}
