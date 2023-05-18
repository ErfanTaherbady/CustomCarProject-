using UnityEngine;

namespace ErfanDeveloper
{
    [CreateAssetMenu(fileName = "GameDitales", menuName = "GameDitales")]
    public class GameDitales : ScriptableObject
    {
        public int basicCoinValues;
    }
    
    
    [CreateAssetMenu(fileName = "CarsData", menuName = "CarsData")]
    public class CarsData : ScriptableObject
    {
        public CarDitales[] carDitales;
    }

    [System.Serializable]
    public class CarDitales
    {
        public string carName;
        public int priceCar;
        public bool isFree;
        public UpgradeDitales[] upgradeDitales;
    }

    [System.Serializable]
    public class UpgradeDitales
    {
        public UpgradeType upgradeType;
        public int[] upgradePrice;
    }

    public enum UpgradeType
    {
        Color,
        Spoiler,
        Hight
    }
    
}