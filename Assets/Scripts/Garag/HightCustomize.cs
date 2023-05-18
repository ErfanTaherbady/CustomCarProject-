
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ErfanDeveloper
{
    public class HightCustomize : MonoBehaviour
    {
        [Header("Code Refrens")] [SerializeField]
        private GaragManager garagCode;
        private CarCode carCode;
        private CarsData carsData;
        
        [Space(5)] [Header("Ui Refrens")] [SerializeField]
        private GameObject buyHightBtn;
        [SerializeField] private GameObject panelSelectionUpgrade;
        [SerializeField] private GameObject panelUpgradeWheel;
        [SerializeField] private Text priceHightText;
        [SerializeField] private Slider frontWheelsSlider;
        [SerializeField] private Slider backWheelsSlider;
        
        [SerializeField] private List<WheelCollider> wheelsColider;
        private int thisCarIndex;
        private float lastHightF,lastHightB;
        //fR fL bR bL
        private void Awake()
        {
            carsData = Resources.Load("ScriptLabelObject/CarsData") as CarsData;
        }

        public void Refrenses()
        {
            thisCarIndex = garagCode.indexCurrentCarSwich;
            lastHightF = PlayerPrefs.GetFloat("FrontHightWheel" + thisCarIndex);
            lastHightB = PlayerPrefs.GetFloat("BackHightWheel" + thisCarIndex);
            frontWheelsSlider.value = lastHightF;
            backWheelsSlider.value = lastHightB;
            if (wheelsColider.Count != 0)
            {
                wheelsColider.Clear();
            }

            carCode = garagCode.lastCreatedCar.GetComponent<CarCode>();
            
            for (int i = 0; i < carCode.wheelsColider.Count; i++)
            {
                wheelsColider.Add(carCode.wheelsColider[i]);
            }
        }

        private void FixedUpdate()
        {
            float hightF = frontWheelsSlider.value;
            float hightB = backWheelsSlider.value;
            wheelsColider[0].suspensionDistance = hightF;
            wheelsColider[1].suspensionDistance = hightF;
            wheelsColider[2].suspensionDistance = hightB;
            wheelsColider[3].suspensionDistance = hightB;
            CheckActiveUi();
        }

        private int priceUpgradeHight;
        private void CheckActiveUi()
        {
            
            priceUpgradeHight = carsData.carDitales[thisCarIndex].upgradeDitales[2].upgradePrice[0];
            if (frontWheelsSlider.value != lastHightF || backWheelsSlider.value != lastHightB)
            {
                buyHightBtn.SetActive(true);
                priceHightText.text = priceUpgradeHight.ToString();
            }
            else
            {
                priceHightText.text = "You Buyed It Hight"; 
            }
        }

        public void BuyThisHight()
        {
            PlayerHQ.LoadData();
            bool canIBuy1 = frontWheelsSlider.value != lastHightF;
            bool canIBuy2 = backWheelsSlider.value != lastHightB;
            if (PlayerHQ.coinNum >= priceUpgradeHight)
            {
                if (canIBuy1 || canIBuy2)
                {
                    panelSelectionUpgrade.SetActive(true);
                    panelUpgradeWheel.SetActive(false);
                    PlayerHQ.CoinTransactions(false,priceUpgradeHight);
                    PlayerPrefs.SetFloat("FrontHightWheel" + thisCarIndex, frontWheelsSlider.value);
                    PlayerPrefs.SetFloat("BackHightWheel" + thisCarIndex, backWheelsSlider.value);
                    garagCode.StartOurEndHightCustomize(false);
                    garagCode.UpdateText();
                }
            }
            else
            {
                print("We Cant Buy This Hight");
            }
        }

        public void SetHight()
        {
            wheelsColider[0].suspensionDistance = PlayerPrefs.GetFloat("FrontHightWheel");
            wheelsColider[1].suspensionDistance = PlayerPrefs.GetFloat("FrontHightWheel");
            wheelsColider[2].suspensionDistance = PlayerPrefs.GetFloat("BackHightWheel");
            wheelsColider[2].suspensionDistance = PlayerPrefs.GetFloat("BackHightWheel");
        }
    }
}