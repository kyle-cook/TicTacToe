using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    private bool turn;
    private bool gameover;

    private string row1;
    private string row2;
    private string row3;
    private string col1;
    private string col2;
    private string col3;
    private string forw;
    private string back;

    public void QuitGame()
    {
        SceneManager.LoadScene("Scenes/MainMenu");
    }

    public void NewGame()
    {
        Debug.Log("Resetting the game board.");

        turn = Random.Range(1, 10) > 5;

        gameover = false;

        row1 = "";
        row2 = "";
        row3 = "";
        col1 = "";
        col2 = "";
        col3 = "";
        forw = "";
        back = "";
        
        var go = GameObject.FindObjectsOfType<Button>();
        if (go != null)
        {
            foreach(Button b in go)
            {
                if(b.name.Length == 2)
                {
                    TMPro.TMP_Text text = b.GetComponentInChildren<TMPro.TMP_Text>();
                    text.SetText(" ");
                }
            }
        }
    }

    public void Start()
    {
        Debug.Log("Starting Game.");
        NewGame();
    }

    private bool CheckWinner()
    {
        if (
            row1 == "XXX" || row1 == "OOO" ||
            row2 == "XXX" || row2 == "OOO" ||
            row3 == "XXX" || row3 == "OOO" ||
            col1 == "XXX" || col1 == "OOO" ||
            col2 == "XXX" || col2 == "OOO" ||
            col3 == "XXX" || col3 == "OOO" ||
            forw == "XXX" || forw == "OOO" ||
            back == "XXX" || back == "OOO"
            )
            return true;
        return false;
    }

    private string WhoWon()
    {

        if (
            row1 == "XXX" ||
            row2 == "XXX" ||
            row3 == "XXX" ||
            col1 == "XXX" ||
            col2 == "XXX" ||
            col3 == "XXX" ||
            forw == "XXX" ||
            back == "XXX"
            )
            return "X";
        if (
            row1 == "OOO" ||
            row2 == "OOO" ||
            row3 == "OOO" ||
            col1 == "OOO" ||
            col2 == "OOO" ||
            col3 == "OOO" ||
            forw == "OOO" ||
            back == "OOO"
            )
            return "O";

        return "Nil";
    }

    /* BUTTON GRID
     * B1 B2 B3
     * B4 B5 B6
     * B7 B8 B9 */

    public void SquareClicked()
    {
        if (gameover)
            return;

        var go = EventSystem.current.currentSelectedGameObject;
        if (go != null)
        {
            Debug.Log("Clicked on : " + go.name);
            TMPro.TMP_Text text = go.GetComponentInChildren<TMPro.TMP_Text>();

            if(text.text != "X" && text.text != "O")
            {
                Debug.Log("Need to move for player.");

                var setIt = "";
                if (turn)
                    setIt = "X";
                else
                    setIt = "O";

                text.SetText(setIt);

                Debug.Log($"It is '{setIt}' turn.");

                switch (go.name)
                {
                    case "B1":
                        row1 += setIt;
                        col1 += setIt;
                        back += setIt;
                        break;
                    case "B2":
                        row1 += setIt;
                        col2 += setIt;
                        break;
                    case "B3":
                        row1 += setIt;
                        col3 += setIt;
                        forw += setIt;
                        break;
                    case "B4":
                        row2 += setIt;
                        col1 += setIt;
                        break;
                    case "B5":
                        row2 += setIt;
                        col2 += setIt;
                        forw += setIt;
                        back += setIt;
                        break;
                    case "B6":
                        row2 += setIt;
                        col3 += setIt;
                        break;
                    case "B7":
                        row3 += setIt;
                        col1 += setIt;
                        forw += setIt;
                        break;
                    case "B8":
                        row3 += setIt;
                        col2 += setIt;
                        break;
                    case "B9":
                        row3 += setIt;
                        col3 += setIt;
                        back += setIt;
                        break;
                }

                turn = !turn;

                Debug.Log("Checking Winner...");

                gameover = CheckWinner();

                if (gameover)
                {
                    string winner = WhoWon();
                    Debug.Log($"Player \"{winner}\" won the game.");
                }
            }
        }
        else
            Debug.Log("currentSelectedGameObject is null");
    }
}
