using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace ToDoListForms
{

    internal class myTodoForm : Form
    {
        private static TableLayoutPanel table;
        private static TableLayoutPanel todoListTable;
        private static List<ToDo> todoList = new List<ToDo>();
        private static TextBox todoInput;
        private static string editGuid;
        private static string filename = "todoSave.txt";
        private static IEnumerable<ToDo> sortList;

        public myTodoForm()
        {
            if (File.Exists(filename))
            {
                todoList = JsonSerializer.Deserialize<List<ToDo>>(File.ReadAllText(filename));
                sortList = todoList;
            }

            // Befehl an Fenster
            MinimumSize = new Size(600, 500);

            // layout table in Speicher generieren
            table = new TableLayoutPanel
            {
                BackColor = Color.DarkSlateGray,
                Dock = DockStyle.Fill,

            };

            table.RowStyles.Add(new RowStyle(SizeType.Absolute, 100));
            table.RowStyles.Add(new RowStyle(SizeType.AutoSize));

            // Befehl an Fenster table in Fenster anzeigen
            Controls.Add(table);

            // Überschrift - Befehl an table die Überschrift anzuzeigen
            table.Controls.Add(new Label
            {
                Text = "Aufgaben:",
                TextAlign = ContentAlignment.TopCenter,
                Font = new Font("consolas", 50),
                ForeColor = Color.CornflowerBlue,
                Dock = DockStyle.Fill,
            });

            //ButtonsPanel
            Panel sortButtonsPanel = new Panel
            {
                Width = 450,
                Height = 50,
                Anchor = AnchorStyles.Top
            };
            table.Controls.Add(sortButtonsPanel);

            Button allButton = new Button
            {
                Font = new Font("consolas", 10),
                BackColor = Color.DimGray,
                ForeColor = Color.CornflowerBlue,
                Text = "All",
                Height = 40,
                Width = 100,
                Location = new Point(0,0),
            };
            sortButtonsPanel.Controls.Add(allButton);
            allButton.Click += SortButtonEventhandler;

            Button activeButton = new Button
            {
                Font = new Font("consolas", 10),
                BackColor = Color.DimGray,
                ForeColor = Color.CornflowerBlue,
                Text = "Active",
                Height = 40,
                Width = 100,
                Location = new Point(110, 0),
            };
            sortButtonsPanel.Controls.Add(activeButton);
            activeButton.Click += SortButtonEventhandler;

            Button doneButton = new Button
            {
                Font = new Font("consolas", 10),
                BackColor = Color.DimGray,
                ForeColor = Color.CornflowerBlue,
                Text = "Done",
                Height = 40,
                Width = 100,
                Location = new Point(220, 0),
            };
            sortButtonsPanel.Controls.Add(doneButton);
            doneButton.Click += SortButtonEventhandler;

            Button clearDoneButton = new Button
            {
                Font = new Font("consolas", 10),
                BackColor = Color.DimGray,
                ForeColor = Color.DarkRed,
                Text = "Clear Done",
                Height = 40,
                Width = 120,
                Location = new Point(330, 0),
            };
            sortButtonsPanel.Controls.Add(clearDoneButton);
            clearDoneButton.Click += ClearDoneButtonEventhandler;

            // eingabefeld
            todoInput = new TextBox
            {
                Font = new Font("consolas", 20),
                BackColor = Color.DimGray,
                BorderStyle = BorderStyle.Fixed3D,
                ForeColor = Color.CornflowerBlue,
                Width = 450,
                Height = 50,
                Dock = DockStyle.Fill,
                Anchor = AnchorStyles.Top
            };

            table.Controls.Add(todoInput);
            table.AutoScroll = true;
            todoListTable = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.DarkSlateGray,
                Width = 450,
                Anchor = AnchorStyles.Top,
                AutoSize = true,
            };
            todoListTable.RowStyles.Add(new RowStyle(SizeType.AutoSize));

            table.Controls.Add(todoListTable);

            // events Wenn knopf gedrückt
            todoInput.KeyDown += TodoInputEventEnter; //Wenn Knopf gedrückt Check ob Enter
            todoInput.KeyDown += TodoInputEventEscape; //Wenn Knopg gedrückt Check ob Escape

            CreateTodoListDisplay();
        }

        private static void CreateTodoListDisplay()
        {
            todoListTable.Controls.Clear();

            foreach (ToDo todo in sortList)
            {
                Panel todoPanel = new Panel
                {
                    Width = 450,
                    Height = 40,
                };

                CheckBox checkBox = new CheckBox
                {
                    Checked = todo.IsDone,
                    ForeColor = Color.Blue,
                    BackColor = Color.SlateGray,
                    Width = 30,
                    Height = 40,
                    Location = new Point(0, 0),
                    Tag = todo.Id,
                };
                todoPanel.Controls.Add(checkBox);
                checkBox.Click += CheckBoxButtonEventhandler;

                Label todoText = new Label
                {
                    Text = todo.Text,
                    TextAlign = ContentAlignment.MiddleLeft,
                    ForeColor = Color.Blue,
                    BackColor = Color.SlateGray,
                    Font = new Font("consolas", 20),
                    Width = 360,
                    Height = 40,
                    Location = new Point(30, 0),
                };
                if (todo.IsDone)
                {
                    todoText.Font = new Font("consolas", 20, FontStyle.Strikeout);
                    todoText.ForeColor = Color.DarkGray;
                }
                todoPanel.Controls.Add(todoText);

                Button editButton = new Button
                {
                    Text = "E",
                    ForeColor = Color.Orange,
                    BackColor = Color.SlateGray,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Font = new Font("consolas", 16),
                    FlatStyle = FlatStyle.Flat,
                    Width = 30,
                    Height = 40,
                    Location = new Point(390, 0),
                    Tag = todo.Id,
                };
                editButton.FlatAppearance.BorderSize = 0;
                todoPanel.Controls.Add(editButton);
                editButton.Click += EditButtonEventhandler;

                Button removeButton = new Button
                {
                    Text = "X",
                    ForeColor = Color.Red,
                    BackColor = Color.SlateGray,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Font = new Font("consolas", 16),
                    FlatStyle = FlatStyle.Flat,
                    Width = 30,
                    Height = 40,
                    Location = new Point(420, 0),
                    Tag = todo.Id,
                };
                removeButton.FlatAppearance.BorderSize = 0;
                todoPanel.Controls.Add(removeButton);
                removeButton.Click += RemoveButtonEventhandler;

                todoListTable.Controls.Add(todoPanel);
            }

        }


        private static void CheckBoxButtonEventhandler(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            string todoId = (string)checkBox.Tag;
            ToDo todoObjekt = todoList.First(o => o.Id == todoId);
            todoObjekt.IsDone = todoObjekt.IsDone == true ? false : true; //besser als If und Else bei beschaltung einer variable
            CreateTodoListDisplay();
            Save();
        }

        private static void EditButtonEventhandler(object sender, EventArgs e)
        {
            Button edit = (Button)sender;
            string todoId =(string)edit.Tag;
            ToDo todoObjekt = todoList.First(o => o.Id == todoId);
            todoInput.Text = todoObjekt.Text;
            editGuid = todoObjekt.Id;
            CreateTodoListDisplay();
        }

        private static void RemoveButtonEventhandler(object sender, EventArgs e)
        {
            Button remove = (Button)sender;
            string todoId = (String)remove.Tag;
            ToDo todoObjekt = todoList.First(o => o.Id == todoId);
            todoList.Remove(todoObjekt);
            CreateTodoListDisplay();
            Save();
        }

        private static void SortButtonEventhandler(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            if (button.Text == "All")
            {
                sortList = todoList;
            }

            if (button.Text == "Active")
            {
                sortList = todoList.Where(o => o.IsDone == false);
            }

            if (button.Text == "Done")
            {
                sortList = todoList.Where(o => o.IsDone == true);
            }
            CreateTodoListDisplay();
        }

        private static void ClearDoneButtonEventhandler(object sender, EventArgs e)
        {
            todoList.RemoveAll(o => o.IsDone == true);
            CreateTodoListDisplay();
            Save();
        }

        private static void TodoInputEventEnter(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TextBox textBox = (TextBox)sender;
                ToDo todoAlreadyExists = todoList.FirstOrDefault(o => o.Text == textBox.Text);
                if (todoAlreadyExists != null)
                {
                    MessageBox.Show("Schon Vorhanden !");
                }
                
                if (editGuid == null && textBox.Text != "" && todoAlreadyExists == null)
                {
                    todoList.Add(new ToDo
                    {
                        Id = Guid.NewGuid().ToString(),
                        Text = textBox.Text
                    });
                    textBox.Clear();
                    CreateTodoListDisplay();
                    Save();
                }

                if (editGuid != null && textBox.Text != "" && todoAlreadyExists == null)
                {
                    ToDo todoObjekt = todoList.First(o => o.Id == editGuid);
                    todoObjekt.Text = textBox.Text;
                    textBox.Clear();
                    CreateTodoListDisplay();
                    editGuid = null;
                    Save();
                }
            }
        }

        private static void TodoInputEventEscape(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                TextBox textBox = (TextBox)sender;
                textBox.Clear();
                editGuid = null;
            }
        }

        private static void Save()
        {
            File.WriteAllText(filename, JsonSerializer.Serialize(todoList));
        }


    } //Ende Main

} //Ende NameSpace
