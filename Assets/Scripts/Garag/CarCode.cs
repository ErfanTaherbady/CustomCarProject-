using System.Collections.Generic;
using UnityEngine;
namespace ErfanDeveloper
{
    public class CarCode : MonoBehaviour
    {
        [Header("Matrial Customize")] public Color[] colorUpgradeList;
        public Color defultColor;
        public Material bodyMat;
        public GameObject matCamera;

        [Space(5)] [Header("Hight Customize")] [HideInInspector]
        public List<WheelCollider> wheelsColider;

        [Space(5)] [Header("Spoiler &.... Customize")]
        public ObjectsCustomize[] objectsCustomizes;
        //0 Spoiler 1 TiresHight 2....

        public void RefrensToWheelColider()
        {
           objectsCustomizes[1].RefrensCustomizeHight();
           wheelsColider.Add(objectsCustomizes[1].Objects[0].GetComponent<WheelCollider>());
           wheelsColider.Add(objectsCustomizes[1].Objects[1].GetComponent<WheelCollider>());
           wheelsColider.Add(objectsCustomizes[1].Objects[2].GetComponent<WheelCollider>());
           wheelsColider.Add(objectsCustomizes[1].Objects[3].GetComponent<WheelCollider>());
        }

        public void ActiveSpoiler(int index)
        {
            int num = objectsCustomizes[0].Objects.Count;
            if (index < 0)
            {
                for (int i = 0; i < num; i++)
                {
                    objectsCustomizes[0].Objects[i].SetActive(false);
                }
            }
            else
            {
                for (int i = 0; i < num; i++)
                {
                    if (index == i)
                    {
                        objectsCustomizes[0].Objects[i].SetActive(true);
                    }
                    else
                    {
                        objectsCustomizes[0].Objects[i].SetActive(false);
                    }
                }
            }
        }

        public void ActiveWheel(int index)
        {
            int num = objectsCustomizes[2].RRl.Count;
            if(index < 0)
            {
                //Defult Select Code
            }
            else
            {
                for (int i = 0; i < num; i++)
                {
                    if (index == i)
                    {
                        objectsCustomizes[2].RRl[i].SetActive(true);
                        objectsCustomizes[2].RLl[i].SetActive(true);
                        objectsCustomizes[2].FRl[i].SetActive(true);
                        objectsCustomizes[2].FLl[i].SetActive(true);
                    }
                    else
                    {
                        objectsCustomizes[2].RRl[i].SetActive(false);
                        objectsCustomizes[2].RLl[i].SetActive(false);
                        objectsCustomizes[2].FRl[i].SetActive(false);
                        objectsCustomizes[2].FLl[i].SetActive(false);
                    }
                }
            }
        }
    }

    [System.Serializable]
    public class ObjectsCustomize
    {
        public string nameCustomize;
        public Transform parentObjects;
        public GameObject ObjectCamera;
        public List<GameObject> Objects;

        [Header("Hight")] public Transform parentAxelF;
        public Transform parentAxelR;
        //for customize hight
        [Header("Wheel")] 
        public Transform RR;
        public Transform RL;
        public Transform FR;
        public Transform FL;
        public List<GameObject> RRl;
        public List<GameObject> RLl;
        public List<GameObject> FRl;
        public List<GameObject> FLl;
        public void RefrensCustomizeObject()
        {
            if (Objects.Count == 0)
            {
                for (int i = 0; i < parentObjects.childCount; i++)
                {
                    Objects.Add(parentObjects.GetChild(i).gameObject);
                }
            }
        }

        public void RefrensCustomizeHight()
        {
            for (int i = 0; i < parentAxelF.childCount; i++)
            {
                Objects.Add(parentAxelF.GetChild(i).gameObject);
            }

            for (int i = 0; i < parentAxelR.childCount; i++)
            {
                Objects.Add(parentAxelR.GetChild(i).gameObject);
            }
            //Fr FL Rr Rl
        }

        public void RefrensCustomizeWheel()
        {
            if (RRl.Count == 0)
            {
                for (int i = 0; i < RR.childCount; i++)
                {
                    RRl.Add(RR.GetChild(i).gameObject);
                }
                for (int j = 0; j < RL.childCount; j++)
                {
                    RLl.Add(RR.GetChild(j).gameObject);
                }
                for (int p = 0; p < FR.childCount; p++)
                {
                    FRl.Add(RR.GetChild(p).gameObject);
                }
                for (int o = 0; o < FL.childCount; o++)
                {
                    FLl.Add(RR.GetChild(o).gameObject);
                }
            }
            //Wheel Refrens
        }
        //Object Customize
    }
}