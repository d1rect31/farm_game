using System.Collections;
using System.Collections.Generic;
using System.Data; // ��� ������ � DataTable
using System.IO; // ��� ������ � �������
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed;
    public GameObject Panel;
    public GameObject NextButton;
    private int index;
    public string dialogueKey;
    // ���������� ���� ��� �������� ���������� �������
    public static bool IsDialogueActive = false;

    // ������ ��� �������� ����������� ������
    private List<Button> disabledButtons = new List<Button>();

    // ������ DataTable (����������� �� CSV)
    private DataTable dialogueTable;

    private void Awake()
    {
        
    }

    void Start()
    {
        // �������� �� ������� ����� ������
        if (NextButton != null)
        {
            Button button = NextButton.GetComponent<Button>();
            if (button != null)
            {
                button.onClick.AddListener(OnNextButtonClick);
            }
        }
    }

    public void StartDialogue(string DialogueKey)
    {
        Panel.SetActive(true);

        // �������� ���� � ����� � ����� StreamingAssets
        string filePath = Path.Combine(Application.streamingAssetsPath, "dialogues.csv");

        // ������������� DataTable �� CSV
        LoadDataTableFromCSV(filePath);

        // ��������� ������ ������� �� ��������� �����
        LoadDialogueLines(DialogueKey);

        // ���������, ���� �� ������ � ������� lines
        if (lines == null || lines.Length == 0)
        {
            Debug.LogError($"������ � ������ \"{DialogueKey}\" �� �������� �����. ��������� CSV-���� ��� ����.");
            Panel.SetActive(false); // �������� ������, ��� ��� ������ �� ����� ���� �������
            return;
        }

        textComponent.text = string.Empty;

        // ������������� ���� ���������� �������
        IsDialogueActive = true;

        // ��������� ��� ������, ����� NextButton
        void LoadDataTableFromCSV(string filePath)
        {
            dialogueTable = new DataTable();
            dialogueTable.Columns.Add("Keyword", typeof(string));
            dialogueTable.Columns.Add("DialogueLines", typeof(string));

            if (!File.Exists(filePath))
            {
                Debug.LogError($"���� {filePath} �� ������!");
                return;
            }

            string[] lines = File.ReadAllLines(filePath);

            // ��������: ���� ������
            if (lines.Length == 0)
            {
                Debug.LogError($"���� {filePath} ������!");
                return;
            }

            foreach (string line in lines)
            {
                // ���������� ������ ������
                if (string.IsNullOrWhiteSpace(line))
                {
                    Debug.LogWarning("���������� ������ ������ � CSV-�����. ����������...");
                    continue;
                }

                // ���������� ���������� ��������� ��� ����������� ���������� ����� � ���������
                string[] values = ParseCSVLine(line);

                // ��������: ������ ����� ������������ ���������� ��������
                if (values.Length != 2)
                {
                    Debug.LogWarning($"������������ ������ ������: \"{line}\". ��������� 2 �������, ������� {values.Length}. ����������...");
                    continue;
                }

                // ��������: �������� ����� ��� ������ ������� ������
                if (string.IsNullOrWhiteSpace(values[0]) || string.IsNullOrWhiteSpace(values[1]))
                {
                    Debug.LogWarning($"���������� ������ � ������ �������� ������ ��� ��������: \"{line}\". ����������...");
                    continue;
                }

                // ��������� ������ � DataTable
                dialogueTable.Rows.Add(values[0].Trim(), values[1].Trim());
            }

            // ��������: DataTable �������� ������ ����� ���������
            if (dialogueTable.Rows.Count == 0)
            {
                Debug.LogError($"���� {filePath} �� �������� ���������� ������!");
            }
        }

        string[] ParseCSVLine(string line)
        {
            List<string> result = new List<string>();
            bool inQuotes = false;
            string currentField = "";

            foreach (char c in line)
            {
                if (c == '"' && !inQuotes)
                {
                    inQuotes = true; // ������ ������ � ��������
                }
                else if (c == '"' && inQuotes)
                {
                    inQuotes = false; // ����� ������ � ��������
                }
                else if (c == ',' && !inQuotes)
                {
                    result.Add(currentField);
                    currentField = "";
                }
                else
                {
                    currentField += c;
                }
            }

            if (!string.IsNullOrEmpty(currentField))
            {
                result.Add(currentField);
            }

            return result.ToArray();
        }
        DisableAllButtonsExceptNext();

        index = 0;
        StartCoroutine(TypeLine());
    }

    void LoadDialogueLines(string keyword)
    {
        // ���� ������ � DataTable �� ��������� �����
        List<string> loadedLines = new();

        foreach (DataRow row in dialogueTable.Rows)
        {
            if (row["Keyword"].ToString() == keyword)
            {
                string[] dialogueLines = row["DialogueLines"].ToString().Split('|'); // ����������� �����
                loadedLines.AddRange(dialogueLines);
            }
        }

        // ��������� ������ lines
        lines = loadedLines.ToArray();
    }

    void LoadDataTableFromCSV(string filePath)
    {
        dialogueTable = new DataTable();
        dialogueTable.Columns.Add("Keyword", typeof(string));
        dialogueTable.Columns.Add("DialogueLines", typeof(string));

        if (!File.Exists(filePath))
        {
            Debug.LogError($"���� {filePath} �� ������!");
            return;
        }

        string[] lines = File.ReadAllLines(filePath);

        // ��������: ���� ������
        if (lines.Length == 0)
        {
            Debug.LogError($"���� {filePath} ������!");
            return;
        }

        foreach (string line in lines)
        {
            // ���������� ������ ������
            if (string.IsNullOrWhiteSpace(line))
            {
                Debug.LogWarning("���������� ������ ������ � CSV-�����. ����������...");
                continue;
            }

            string[] values = line.Split(',');
            // ��������: �������� ����� ��� ������ ������� ������
            if (string.IsNullOrWhiteSpace(values[0]) || string.IsNullOrWhiteSpace(values[1]))
            {
                Debug.LogWarning($"���������� ������ � ������ �������� ������ ��� ��������: \"{line}\". ����������...");
                continue;
            }

            // ��������� ������ � DataTable
            dialogueTable.Rows.Add(values[0].Trim(), values[1].Trim());
        }

        // ��������: DataTable �������� ������ ����� ���������
        if (dialogueTable.Rows.Count == 0)
        {
            Debug.LogError($"���� {filePath} �� �������� ���������� ������!");
        }
    }

    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void OnNextButtonClick()
    {
        if (textComponent.text == lines[index])
        {
            NextLine();
        }
        else
        {
            StopAllCoroutines();
            textComponent.text = lines[index];
        }
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            EndDialogue();
        }
    }

    void EndDialogue()
    {
        // ���������� ���� ���������� �������
        IsDialogueActive = false;

        // �������� ��� ����� ����������� ������
        EnableAllButtons();

        Panel.SetActive(false);
    }

    void DisableAllButtonsExceptNext()
    {
        Button[] buttons = FindObjectsByType<Button>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        foreach (Button button in buttons)
        {
            if (button.gameObject != NextButton)
            {
                if (button.interactable)
                {
                    button.interactable = false;
                    disabledButtons.Add(button);
                }
            }
        }
    }

    void EnableAllButtons()
    {
        // ������� ��������� ������ ��� �������� ������, ������� ��� ��� ����������
        List<Button> validButtons = new List<Button>();

        foreach (Button button in disabledButtons)
        {
            if (button != null) // ���������, ���������� �� ������
            {
                button.interactable = true;
                validButtons.Add(button); // ��������� ������ ������������ ������
            }
            else
            {
                Debug.LogWarning("���������� ��������� ������ � ������ ����������� ������.");
            }
        }

        // ��������� ������ ����������� ������, �������� ������ ������������
        disabledButtons = validButtons;
    }
}
