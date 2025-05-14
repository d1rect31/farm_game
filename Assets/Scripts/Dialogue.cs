using System.Collections;
using System.Collections.Generic;
using System.Data; // Для работы с DataTable
using System.IO; // Для работы с файлами
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
    // Глобальный флаг для проверки активности диалога
    public static bool IsDialogueActive = false;

    // Список для хранения отключенных кнопок
    private List<Button> disabledButtons = new List<Button>();

    // Пример DataTable (заполняется из CSV)
    private DataTable dialogueTable;

    private void Awake()
    {
        
    }

    void Start()
    {
        // Подписка на событие клика кнопки
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

        // Получаем путь к файлу в папке StreamingAssets
        string filePath = Path.Combine(Application.streamingAssetsPath, "dialogues.csv");

        // Инициализация DataTable из CSV
        LoadDataTableFromCSV(filePath);

        // Загружаем строки диалога по ключевому слову
        LoadDialogueLines(DialogueKey);

        // Проверяем, есть ли строки в массиве lines
        if (lines == null || lines.Length == 0)
        {
            Debug.LogError($"Диалог с ключом \"{DialogueKey}\" не содержит строк. Проверьте CSV-файл или ключ.");
            Panel.SetActive(false); // Скрываем панель, так как диалог не может быть запущен
            return;
        }

        textComponent.text = string.Empty;

        // Устанавливаем флаг активности диалога
        IsDialogueActive = true;

        // Отключаем все кнопки, кроме NextButton
        void LoadDataTableFromCSV(string filePath)
        {
            dialogueTable = new DataTable();
            dialogueTable.Columns.Add("Keyword", typeof(string));
            dialogueTable.Columns.Add("DialogueLines", typeof(string));

            if (!File.Exists(filePath))
            {
                Debug.LogError($"Файл {filePath} не найден!");
                return;
            }

            string[] lines = File.ReadAllLines(filePath);

            // Проверка: файл пустой
            if (lines.Length == 0)
            {
                Debug.LogError($"Файл {filePath} пустой!");
                return;
            }

            foreach (string line in lines)
            {
                // Пропускаем пустые строки
                if (string.IsNullOrWhiteSpace(line))
                {
                    Debug.LogWarning("Обнаружена пустая строка в CSV-файле. Пропускаем...");
                    continue;
                }

                // Используем регулярное выражение для корректного разделения строк с кавычками
                string[] values = ParseCSVLine(line);

                // Проверка: строка имеет неправильное количество столбцов
                if (values.Length != 2)
                {
                    Debug.LogWarning($"Неправильный формат строки: \"{line}\". Ожидается 2 столбца, найдено {values.Length}. Пропускаем...");
                    continue;
                }

                // Проверка: ключевое слово или строки диалога пустые
                if (string.IsNullOrWhiteSpace(values[0]) || string.IsNullOrWhiteSpace(values[1]))
                {
                    Debug.LogWarning($"Обнаружена строка с пустым ключевым словом или диалогом: \"{line}\". Пропускаем...");
                    continue;
                }

                // Добавляем строку в DataTable
                dialogueTable.Rows.Add(values[0].Trim(), values[1].Trim());
            }

            // Проверка: DataTable осталась пустой после обработки
            if (dialogueTable.Rows.Count == 0)
            {
                Debug.LogError($"Файл {filePath} не содержит корректных данных!");
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
                    inQuotes = true; // Начало текста в кавычках
                }
                else if (c == '"' && inQuotes)
                {
                    inQuotes = false; // Конец текста в кавычках
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
        // Ищем строки в DataTable по ключевому слову
        List<string> loadedLines = new();

        foreach (DataRow row in dialogueTable.Rows)
        {
            if (row["Keyword"].ToString() == keyword)
            {
                string[] dialogueLines = row["DialogueLines"].ToString().Split('|'); // Разделитель строк
                loadedLines.AddRange(dialogueLines);
            }
        }

        // Заполняем массив lines
        lines = loadedLines.ToArray();
    }

    void LoadDataTableFromCSV(string filePath)
    {
        dialogueTable = new DataTable();
        dialogueTable.Columns.Add("Keyword", typeof(string));
        dialogueTable.Columns.Add("DialogueLines", typeof(string));

        if (!File.Exists(filePath))
        {
            Debug.LogError($"Файл {filePath} не найден!");
            return;
        }

        string[] lines = File.ReadAllLines(filePath);

        // Проверка: файл пустой
        if (lines.Length == 0)
        {
            Debug.LogError($"Файл {filePath} пустой!");
            return;
        }

        foreach (string line in lines)
        {
            // Пропускаем пустые строки
            if (string.IsNullOrWhiteSpace(line))
            {
                Debug.LogWarning("Обнаружена пустая строка в CSV-файле. Пропускаем...");
                continue;
            }

            string[] values = line.Split(',');
            // Проверка: ключевое слово или строки диалога пустые
            if (string.IsNullOrWhiteSpace(values[0]) || string.IsNullOrWhiteSpace(values[1]))
            {
                Debug.LogWarning($"Обнаружена строка с пустым ключевым словом или диалогом: \"{line}\". Пропускаем...");
                continue;
            }

            // Добавляем строку в DataTable
            dialogueTable.Rows.Add(values[0].Trim(), values[1].Trim());
        }

        // Проверка: DataTable осталась пустой после обработки
        if (dialogueTable.Rows.Count == 0)
        {
            Debug.LogError($"Файл {filePath} не содержит корректных данных!");
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
        // Сбрасываем флаг активности диалога
        IsDialogueActive = false;

        // Включаем все ранее отключенные кнопки
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
        // Создаем временный список для хранения кнопок, которые все еще существуют
        List<Button> validButtons = new List<Button>();

        foreach (Button button in disabledButtons)
        {
            if (button != null) // Проверяем, существует ли кнопка
            {
                button.interactable = true;
                validButtons.Add(button); // Добавляем только существующие кнопки
            }
            else
            {
                Debug.LogWarning("Обнаружена удаленная кнопка в списке отключенных кнопок.");
            }
        }

        // Обновляем список отключенных кнопок, оставляя только существующие
        disabledButtons = validButtons;
    }
}
