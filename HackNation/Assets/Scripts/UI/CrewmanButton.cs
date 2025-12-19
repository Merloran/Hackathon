using UnityEngine;
using UnityEngine.UI;


public class CrewmanButton : MonoBehaviour
{
    public CrewmanData data;
    private Button button;
    private Crew crew;

    void Start()
    {
        crew = FindFirstObjectByType<Crew>();
        button = GetComponent<Button>();
    }

    void Update()
    {
        for (int i = 0; i < BossSceneData.SelectedCrew.Count; ++i)
        {
            if (BossSceneData.SelectedCrew[i] == data)
            {
                var colors = button.colors;
                colors.normalColor = new Color(0f, 1.0f, 0.0f);
                colors.highlightedColor = new Color(0f, 1.0f, 0.0f);
                colors.selectedColor = new Color(0f, 1.0f, 0.0f);
                button.colors = colors;
                return;
            }
        }

        {
            var colors = button.colors;
            colors.normalColor = new Color(0.8f, 0.8f, 0.8f);
            colors.highlightedColor = new Color(0.8f, 0.8f, 0.8f);
            colors.selectedColor = new Color(0.8f, 0.8f, 0.8f);
            button.colors = colors;
        }
    }

    public void OnSelect()
    {
        for (int i = 0; i < BossSceneData.SelectedCrew.Count; ++i)
        {
            if (BossSceneData.SelectedCrew[i] == data)
            {
                return;
            }
        }
        crew.SwapCrew(BossSceneData.crewmanToChange, data);
    }

    public void SetCurrentCrewmate(int index)
    {
        BossSceneData.crewmanToChange = BossSceneData.SelectedCrew[index];
    }
}
