using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public Centipede centipede;
    public Boss boss;
    public GameObject gameOverScreen;
    public GameObject winScreen;
    public GameObject bg;
    public GameObject restart;
    public GameObject healthbar;
    private void Update() {
        if (boss.AIFSM.m_currentState == boss.AIFSM.m_states["laserBarrage"])
        {
            healthbar.SetActive(true);
        }
        if (!centipede.isDead && boss.isDead)
        {
            bg.SetActive(true);
            restart.SetActive(true);
            winScreen.SetActive(true);
            healthbar.SetActive(false);
            Cursor.visible = true;
        }
        else if (centipede.isDead)
        {
            bg.SetActive(true);
            restart.SetActive(true);
            gameOverScreen.SetActive(true);
            Cursor.visible = true;
            healthbar.SetActive(false);
        }
    }
}
