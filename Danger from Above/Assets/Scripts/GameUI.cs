using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour
{
    public static GameUI game;
    [Header("Numbers")]
    public int moneys;
    public int score;
    public int highScore;
    public float playerHP;
    [Header("Game Objects")]
    public GameObject shop;
    public GameObject start;
    public GameObject defeat;
    [Header("Text")]
    public Text moneytext;
    public Text scoreText;
    public Text highScoreText;
    public Text playerHPText;
    [Header("Textures")]
    public Texture2D red;
    public Texture2D blue;
    public Texture2D orange;
    public Texture2D green;
    public Texture2D all;
    private GameObject player;
    [Header("Sky and Ground")]
    public Sprite[] skyColors;
    public Texture2D[] groundTextures;
    public Image background;
    public Material groundMaterial;

    

    private void Start() {
        SetColor();

        Time.timeScale = 0;
        game = this;

        moneys = PlayerHasKey("Moneys", moneys);
        highScore = PlayerHasKey("HighScore", score);

        if(PlayerPrefs.HasKey("ID")) Color(PlayerPrefs.GetInt("ID"));
        else Color(0);
    }

    private void Update() {
        moneytext.text = moneys.ToString();
        scoreText.text = score.ToString();
        highScoreText.text = highScore.ToString();
        playerHPText.text = playerHP.ToString();

        if(Input.GetKey(KeyCode.Space)) {
            PlayerPrefs.DeleteAll();
        }
    }

    public void Color(int index) {
        player = GameObject.FindObjectOfType<Player>().gameObject;
        Material material = player.GetComponent<Renderer>().material;
        switch(index) {
            case 0:
                material.mainTexture = red;
                PlayerPrefs.SetInt("ID", 0);
                break;
            case 1:
                material.mainTexture = blue;
                PlayerPrefs.SetInt("ID", 1);
                break;
            case 2:
                material.mainTexture = orange;
                PlayerPrefs.SetInt("ID", 2);
                break;
            case 3:
                material.mainTexture = green;
                PlayerPrefs.SetInt("ID", 3);
                break;
            case 4:
                material.mainTexture = all;
                PlayerPrefs.SetInt("ID", 4);
                break;
        }
    }

    public void OpenShop() {
        shop.SetActive(true);
    }
    public void CloseShop() {
        shop.SetActive(false);
    }

    public void GameOver() {
        Time.timeScale = 0;
        if(score > highScore) {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
        }
        Reward.Instance.LoadAd();
        Interstitial.Instance.ShowAd();
        
        defeat.SetActive(true);
    }

    public void RestartGame() {
        SceneManager.LoadScene(0);
        SetColor();
    }

    public void StartGame() {
        Time.timeScale = 1;
        start.SetActive(false);
    }

    public void RewardAD() {
        Player player = GameObject.FindObjectOfType<Player>();
        player.HP = 1;

        Reward.Instance._showAdButton.interactable = false;

        defeat.SetActive(false);
        Time.timeScale = 1;
    }

    public void SetColor() {
        int random = Random.Range(0, skyColors.Length);

        groundMaterial.mainTexture = groundTextures[random];
        background.sprite = skyColors[random];
    }

    public int PlayerHasKey(string key, int property) {
        if(PlayerPrefs.HasKey(key)) property = PlayerPrefs.GetInt(key);
        else property = 0;

        return property;
    }
}