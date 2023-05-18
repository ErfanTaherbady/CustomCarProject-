using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ErfanDeveloper
{
    public class GaragManager : MonoBehaviour
    {
        [Header("Spoiler")] [SerializeField] private List<Image> spoilerImages;
        [SerializeField] private Image sampelSpoilerImage;
        [SerializeField] private Transform parentSpoilerImage;
        [SerializeField] private GameObject panelUpgradeSpoiler;
        [SerializeField] private GameObject selectBtnSpoiler;
        [SerializeField] private GameObject buyBtnSpoiler;
        [SerializeField] private GameObject isSpoilerSelected;
        [SerializeField] private Text priceSpoilerToBuy;

        [Header("Ui Refrens")] [SerializeField]
        private GameObject buyBtn;

        [SerializeField] private GameObject panelSelectUpgrade;
        [SerializeField] private GameObject buyBtnColor;
        [SerializeField] private GameObject isColorToBuyedBtn;
        [SerializeField] private Image sampelcolorPicker;
        [SerializeField] private Transform parentColorPicker;
        [SerializeField] private Text priceColorToBuy;
        [SerializeField] private GameObject selectBtn;
        [SerializeField] private GameObject selectedBtn;
        [SerializeField] private Text priceCarText;
        [SerializeField] private Text currentCoinText;

        [Header("Object Refrens")] [SerializeField]
        private Transform createdCarParent;
        [SerializeField] private HightCustomize hightCustomizeCode;
        [HideInInspector] public GameObject lastCreatedCar;
        [HideInInspector] public int indexCurrentCarSwich;
        private CarsData carsData;
        public List<Image> colorPickerImage;
        private GameDitales gameData;
        
        private void Awake()
        {
            PlayerHQ.LoadData();
            Refrenses();
        }

        private void Start()
        {
            indexCurrentCarSwich = PlayerPrefs.GetInt("currentSelectedCarForGameIndex");
            UpdateText();
            SetPlayerBasicValues();
            CreateNextCar();
        }

        private void Refrenses()
        {
            carsData = Resources.Load("ScriptLabelObject/CarsData") as CarsData;
            gameData = Resources.Load("ScriptLabelObject/GameDitales") as GameDitales;
        }

        private void SetPlayerBasicValues()
        {
            if (PlayerPrefs.GetInt("BasicValuesGive") == 0)
            {
                PlayerHQ.CoinTransactions(true, gameData.basicCoinValues);
                UpdateText();
                PlayerPrefs.SetInt("currentSelectedCarForGameIndex", 0);
                PlayerPrefs.SetInt("BasicValuesGive", 1);
            }
        }

        private void CreateNextCar()
        {
            if (lastCreatedCar)
                Destroy(lastCreatedCar);

            GameObject carToLoad =
                Resources.Load("Garag Cars/" + indexCurrentCarSwich + "/" + "Prfab/" + indexCurrentCarSwich) as
                    GameObject;
            GameObject thisCarObject = Instantiate(carToLoad, createdCarParent);
            lastCreatedCar = thisCarObject;
            CheckCarValid();
        }

        private void CheckCarValid()
        {
            bool isCarFree = carsData.carDitales[indexCurrentCarSwich].isFree;
            bool isCarNeedToBuy = PlayerPrefs.GetInt("CarBught" + indexCurrentCarSwich) == 0;
            if (!isCarFree && isCarNeedToBuy)
            {
                buyBtn.SetActive(true);
                selectedBtn.SetActive(false);
                selectBtn.SetActive(false);
                priceCarText.text = carsData.carDitales[indexCurrentCarSwich].priceCar.ToString();
            }
            else if (PlayerPrefs.GetInt("currentSelectedCarForGameIndex") == indexCurrentCarSwich)
            {
                buyBtn.SetActive(false);
                selectedBtn.SetActive(true);
                selectBtn.SetActive(false);
                CheckUpgrade();
            }
            else
            {
                buyBtn.SetActive(false);
                selectedBtn.SetActive(false);
                selectBtn.SetActive(true);
                CheckUpgrade();
            }
        }

        private void CheckUpgrade()
        {
            int selectedColorForThisCar = PlayerPrefs.GetInt("SelectedCarColor" + indexCurrentCarSwich, -1);
            CarCode cC = lastCreatedCar.GetComponent<CarCode>();
            if (selectedColorForThisCar < 0)
            {
                lastCreatedCar.GetComponent<CarCode>().bodyMat.color = cC.defultColor;
            }
            else
            {
                lastCreatedCar.GetComponent<CarCode>().bodyMat.color = cC.colorUpgradeList[selectedColorForThisCar];
            }

            int selectedSpoilerForThisCar = PlayerPrefs.GetInt("SelectedCarSpoiler" + indexCurrentCarSwich, -1);

            lastCreatedCar.GetComponent<CarCode>().ActiveSpoiler(selectedSpoilerForThisCar);
        }

        public void UpdateText()
        {
            currentCoinText.text = PlayerHQ.coinNum.ToString();
        }

        #region CodeByBtn

        public void SelectCarToLoad(int index)
        {
            indexCurrentCarSwich = index;
            CreateNextCar();
        }

        public void BuyCar()
        {
            Debug.Log("Buy");
            int buyCoinNeed = carsData.carDitales[indexCurrentCarSwich].priceCar;
            if (PlayerHQ.coinNum >= buyCoinNeed)
            {
                PlayerPrefs.SetInt("CarBught" + indexCurrentCarSwich, 1);
                PlayerHQ.CoinTransactions(false, buyCoinNeed);
                CheckCarValid();
                UpdateText();
            }
            else
            {
                print("We Cant Buy This Car");
            }
        }

        public void SelectCurrentCarForGame()
        {
            PlayerPrefs.SetInt("currentSelectedCarForGameIndex", indexCurrentCarSwich);
            CheckCarValid();
        }

        #endregion

        #region StartCustomize

        public void StartOurEndColorCustomize(bool isStart)
        {
            if (colorPickerImage != null)
            {
                for (int i = 0; i < colorPickerImage.Count; i++)
                {
                    Destroy(colorPickerImage[i].gameObject);
                }

                colorPickerImage.Clear();
            }

            lastCreatedCar.GetComponent<CarCode>().matCamera.SetActive(isStart);
            CarCode c = lastCreatedCar.GetComponent<CarCode>();

            if (isStart)
            {
                for (int i = 0; i < c.colorUpgradeList.Length; i++)
                {
                    Image o = Instantiate(sampelcolorPicker, parentColorPicker);
                    o.gameObject.SetActive(true);
                    o.color = c.colorUpgradeList[i];
                    o.transform.name = i.ToString();
                    colorPickerImage.Add(o);
                }
            }
            else
            {
                CheckUpgrade();
            }
        }


        public void StartOurEndSpoilerCustumize(bool isStart)
        {
            isSpoilerSelected.SetActive(false);
            buyBtnSpoiler.SetActive(false);
            if (spoilerImages.Count != 0)
            {
                for (int i = 0; i < spoilerImages.Count; i++)
                {
                    Destroy(spoilerImages[i].gameObject);
                }

                spoilerImages.Clear();
            }

            lastCreatedCar.GetComponent<CarCode>().objectsCustomizes[0].RefrensCustomizeObject();
            lastCreatedCar.GetComponent<CarCode>().objectsCustomizes[0].ObjectCamera.SetActive(isStart);
            CarCode c = lastCreatedCar.GetComponent<CarCode>();
            if (isStart)
            {
                for (int i = 0; i < c.objectsCustomizes[0].Objects.Count; i++)
                {
                    Image imageSpoiler = Instantiate(sampelSpoilerImage, parentSpoilerImage);
                    spoilerImages.Add(imageSpoiler);
                    imageSpoiler.gameObject.SetActive(true);
                    Sprite teaxer =
                        Resources.Load<Sprite>("Garag Cars/" + indexCurrentCarSwich + "/" + "SpoilerUpgrade/" + i);
                    imageSpoiler.sprite = teaxer;
                    imageSpoiler.transform.name = i.ToString();
                }
            }
            else
            {
                CheckUpgrade();
            }
        }

        public void StartOurEndHightCustomize(bool isStart)
        {
            lastCreatedCar.GetComponent<CarCode>().objectsCustomizes[1].ObjectCamera.SetActive(isStart);
            lastCreatedCar.GetComponent<CarCode>().RefrensToWheelColider();
            hightCustomizeCode.Refrenses();
            hightCustomizeCode.SetHight();
        }

        private int selectedColorToBuy;
        private int priceColor;

        public void ChangeColorPeriview(Transform btn)
        {
            selectedColorToBuy = Convert.ToInt32(btn.name);
            CheckToActiveColorBtn();
            CarCode c = lastCreatedCar.GetComponent<CarCode>();
            lastCreatedCar.GetComponent<CarCode>().bodyMat.color = c.colorUpgradeList[selectedColorToBuy];
        }

        private void CheckToActiveColorBtn()
        {
            int checkToSaveColor = PlayerPrefs.GetInt("SelectedCarColor" + indexCurrentCarSwich, -1);

            if (checkToSaveColor < 0)
            {
                isSpoilerSelected.SetActive(false);
                buyBtnSpoiler.SetActive(true);
            }

            if (checkToSaveColor == selectedColorToBuy)
            {
                isColorToBuyedBtn.SetActive(true);
                buyBtnColor.SetActive(false);
            }
            else
            {
                isColorToBuyedBtn.SetActive(false);
                buyBtnColor.SetActive(true);
                priceColor = carsData.carDitales[indexCurrentCarSwich].upgradeDitales[0].upgradePrice[0];
                priceColorToBuy.text = priceColor.ToString();
            }
        }

        private int priceSpoiler;
        private int indexSelectedSpoiler;

        public void ChangeSpoilerPeriview(Transform btn)
        {
            indexSelectedSpoiler = Convert.ToInt32(btn.name);
            CheckToActiveSpoilerBtn();
            lastCreatedCar.GetComponent<CarCode>().ActiveSpoiler(indexSelectedSpoiler);
        }

        private void CheckToActiveSpoilerBtn()
        {
            bool isBought =
                PlayerPrefs.GetInt("CarSpoiler" + indexCurrentCarSwich + "SpoilerBought" + indexSelectedSpoiler) == 1;

            bool isSelected = PlayerPrefs.GetInt("SelectedCarSpoiler" + indexCurrentCarSwich) == indexSelectedSpoiler;

            priceSpoiler = carsData.carDitales[indexCurrentCarSwich].upgradeDitales[1]
                .upgradePrice[indexSelectedSpoiler];
            if (isBought && !isSelected)
            {
                selectBtnSpoiler.SetActive(true);
                buyBtnSpoiler.SetActive(false);
                isSpoilerSelected.SetActive(false);
            }
            else if (!isBought)
            {
                selectBtnSpoiler.SetActive(false);
                buyBtnSpoiler.SetActive(true);
                priceSpoilerToBuy.text = priceSpoiler.ToString();
                isSpoilerSelected.SetActive(false);
            }
            else if (isSelected)
            {
                selectBtnSpoiler.SetActive(false);
                buyBtnSpoiler.SetActive(false);
                isSpoilerSelected.SetActive(true);
            }
        }

        public void BuyColorForThisCar()
        {
            PlayerHQ.LoadData();
            int myCoin = PlayerHQ.coinNum;
            if (myCoin >= priceColor)
            {
                PlayerHQ.CoinTransactions(false, priceColor);
                PlayerPrefs.SetInt("SelectedCarColor" + indexCurrentCarSwich, selectedColorToBuy);
                lastCreatedCar.GetComponent<CarCode>().matCamera.SetActive(false);
                UpdateText();
            }
            else
            {
                print("We Cant Buy This Color");
            }
        }

        public void BuySpoilerForThisCar()
        {
            PlayerHQ.LoadData();
            int myCoin = PlayerHQ.coinNum;
            if (myCoin >= priceSpoiler)
            {
                PlayerHQ.CoinTransactions(false, priceSpoiler);
                PlayerPrefs.SetInt("SelectedCarSpoiler" + indexCurrentCarSwich, indexSelectedSpoiler);
                PlayerPrefs.SetInt("CarSpoiler" + indexCurrentCarSwich + "SpoilerBought" + indexSelectedSpoiler, 1);
                CheckToActiveSpoilerBtn();
                UpdateText();
            }
            else
            {
                print("We Cant Buy This Spoiler");
            }
        }

        public void SelectSpoiler()
        {
            PlayerPrefs.SetInt("SelectedCarSpoiler" + indexCurrentCarSwich, indexSelectedSpoiler);
            CheckToActiveSpoilerBtn();
            CheckUpgrade();
        }

        public void SelectDefuldToSpoiler()
        {
            PlayerPrefs.SetInt("SelectedCarSpoiler" + indexCurrentCarSwich, -1);
            panelSelectUpgrade.SetActive(true);
            panelUpgradeSpoiler.SetActive(false);
            StartOurEndSpoilerCustumize(false);
        }
    }

    #endregion
}