using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public List<Texture2D> skins;
    public List<Texture2D> buyedSkins;

    public int moneys;
    // Start is called before the first frame update
    void Start()
    {
        GetBuySkins();

        moneys = GameUI.game.PlayerHasKey("Moneys", moneys);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void GetBuySkins() {
        foreach(Texture2D skin in skins) {
            if(PlayerPrefs.HasKey(skin.name)) buyedSkins.Add(skin);
        }
    }

    public void Buy(string indexPrice) {
        moneys = GameUI.game.PlayerHasKey("Moneys", moneys);

        string[] split = indexPrice.Split(","[0]);
        int ID = int.Parse(split[0]);
        int price = int.Parse(split[1]);

        if(!PlayerPrefs.HasKey(skins.ToArray()[ID].name) && moneys >= price) {
            PlayerPrefs.SetString(skins.ToArray()[ID].name, skins.ToArray()[ID].name);
            buyedSkins.Add(skins.ToArray()[ID]);
            moneys -= price;

            PlayerPrefs.SetInt("Moneys", moneys);
            GameUI.game.Color(ID);
            GameUI.game.moneys = GameUI.game.PlayerHasKey("Moneys", moneys);
            print("Buy");
        }
        else if(moneys < price && !PlayerPrefs.HasKey(skins.ToArray()[ID].name)) print("No Buy, No Use");
        else if(PlayerPrefs.HasKey(skins.ToArray()[ID].name)) {
            GameUI.game.Color(ID);
            print("Use");
        }
    }
}
