using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Морской_бой
{
    public partial class Form_game : Form
    {
        #region Создание переменных
        Label[,] Field_my = new Label[10, 10];
        Label[,] Field_enemy = new Label[10, 10];

        /// <summary>
        /// 0 - пустая клетка Color.Aqua
        /// -10 - промах Color.LightGray
        /// 1 - однопалубник Color.Green
        /// -1 - уничтоженный однопалубник Color.Black
        /// 2 - двупалубник Color.Green
        /// -2 - подбитый двухпалубник Color.Red
        /// -20 - уничтоженный двухпалубник Color.Black
        /// 3 - трёхпалубник Color.Green
        /// -3 - подбитый трёпалубник Color.Red
        /// -30 - уничтоженный трёхпалубник Color.Black
        /// 4 - четырёхпалубник Color.Green
        /// -4 - подбитый четырёхпалубник Color.Red
        /// -40 - уничтоженный четырёхпалубник Color.Black
        /// </summary>
        int[,] _Field_enemy = new int[10, 10];

        /// <summary>
        /// 0 - пустая клетка Color.Aqua
        /// -10 - промах Color.LightGray
        /// 1 - однопалубник Color.Green
        /// -1 - уничтоженный однопалубник Color.Black
        /// 2 - двупалубник Color.Green
        /// -2 - подбитый двухпалубник Color.Red
        /// -20 - уничтоженный двухпалубник Color.Black
        /// 3 - трёхпалубник Color.Green
        /// -3 - подбитый трёпалубник Color.Red
        /// -30 - уничтоженный трёхпалубник Color.Black
        /// 4 - четырёхпалубник Color.Green
        /// -4 - подбитый четырёхпалубник Color.Red
        /// -40 - уничтоженный четырёхпалубник Color.Black
        /// </summary>
        int[,] _Field_my = new int[10, 10];

        public struct Tern_of_loc_ships
        {
            public string name;
            public int i;
            public int j;
            public bool allow;
        }

        public void set_allows(bool al)
        {
            for (int i = 0; i <= 3; i++)
                if (al)
                    terns[i].allow = true;
                else
                    terns[i].allow = false;
        }

        Tern_of_loc_ships[] terns = new Tern_of_loc_ships[4];

        bool Start = false;

        Ships Ships_my = new Ships();
        Ships Ships_enemy = new Ships();

        int _i_click, _j_click, _i_LightGray, _j_LightGray;

        #region Для стрельбы компа
        int kol_palub_kor = 0;
        int i_rand, j_rand, n_palub = 0, tern, i_first, j_first;
        bool tern_left = true, tern_up = true, tern_right = true, tern_down = true;
        bool first_hit = true, more_two_hit = false;
        #endregion

        public void Default_tern()
        {
            tern_left = true;
            tern_up = true;
            tern_right = true;
            tern_down = true;
        }

        string str;
        #endregion

        public Form_game()
        {
            InitializeComponent();
            Field_paint(Field_my, new Point(50, 50));
            Field_paint(Field_enemy, new Point(500, 50));
            for (int i = 0; i <= 9; i++)
                for (int j = 0; j <= 9; j++)
                    _Field_enemy[i, j] = 0;

            terns[0].name = "left";
            terns[1].name = "up";
            terns[2].name = "right";
            terns[3].name = "down";
            set_allows(false);
        }

        /// <summary>
        /// Отрисовка боевых полей
        /// </summary>
        /// <param name="Field"></param>
        /// <param name="Coords"></param>
        private void Field_paint(Label[,] Field, Point Coords)
        {
            for (int i = 0; i < 10; i++)
                for (int j = 0; j < 10; j++)
                {
                    Field[i, j] = new Label();
                    Field[i, j].Size = new Size(20, 20);
                    Field[i, j].Location = new Point(Coords.X + j * 21, Coords.Y + i * 21);
                    Field[i, j].BackColor = Color.Aqua;
                    Field[i, j].Parent = this;

                    if (Field == Field_my)
                        Field[i, j].Click += Form_game_Click_my;
                    else
                        Field[i, j].Click += Form_game_Click_enemy;
                }
        }

        /// <summary>
        /// Обработчик клика по своему полю
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form_game_Click_my(object sender, EventArgs e)
        {
            if (!Start)
            {
                _j_click = (((Label)sender).Location.X - 50) / 21;
                _i_click = (((Label)sender).Location.Y - 50) / 21;

                if (((Label)sender).BackColor == Color.Aqua)
                {
                    if (Check_locate_desk(_i_click, _j_click) && (Ships_my._ALL_desk > 0))
                    {
                        ((Label)sender).BackColor = Color.LightGray;
                        _i_LightGray = _i_click;
                        _j_LightGray = _j_click;

                        Field_my_enabled(false);

                        if (Ships_my._four_desk > 0)
                            if ((_j_click > 2) && Check_locate_Ship("left", 4) || (_i_click > 2) && Check_locate_Ship("up", 4) || (_j_click < 7) && Check_locate_Ship("right", 4) || (_i_click < 7) && Check_locate_Ship("down", 4))
                                but_Four_desk.Enabled = true;

                        if (Ships_my._three_desk > 0)
                            if ((_j_click > 1) && Check_locate_Ship("left", 3) || (_i_click > 1) && Check_locate_Ship("up", 3) || (_j_click < 8) && Check_locate_Ship("right", 3) || (_i_click < 8) && Check_locate_Ship("down", 3))
                                but_Three_desk.Enabled = true;

                        if (Ships_my._two_desk > 0)
                            if ((_j_click > 0) && Check_locate_Ship("left", 2) || (_i_click > 0) && Check_locate_Ship("up", 2) || (_j_click < 9) && Check_locate_Ship("right", 2) || (_i_click < 9) && Check_locate_Ship("down", 2))
                                but_Two_desk.Enabled = true;

                        if (Ships_my._one_desk > 0)
                            but_One_desk.Enabled = true;
                        but_reset_locships.Enabled = true;
                    }
                }

                if (((Label)sender).BackColor == Color.Gray)
                {
                    if (_i_click < _i_LightGray)
                        for (int i = _i_click; i <= _i_LightGray; i++)
                        {
                            Field_my[i, _j_click].BackColor = Color.Green;
                            _Field_my[i, _j_click] = (_i_LightGray - _i_click) + 1;
                        }

                    if (_i_click > _i_LightGray)
                        for (int i = _i_click; i >= _i_LightGray; i--)
                        {
                            Field_my[i, _j_click].BackColor = Color.Green;
                            _Field_my[i, _j_click] = (_i_click - _i_LightGray) + 1;
                        }

                    if (_j_click < _j_LightGray)
                        for (int j = _j_click; j <= _j_LightGray; j++)
                        {
                            Field_my[_i_click, j].BackColor = Color.Green;
                            _Field_my[_i_click, j] = (_j_LightGray - _j_click) + 1;
                        }

                    if (_j_click > _j_LightGray)
                        for (int j = _j_click; j >= _j_LightGray; j--)
                        {
                            Field_my[_i_click, j].BackColor = Color.Green;
                            _Field_my[_i_click, j] = (_j_click - _j_LightGray) + 1;
                        }

                    for (int i = 0; i < 10; i++)
                        for (int j = 0; j < 10; j++)
                            if (Field_my[i, j].BackColor == Color.Gray)
                                Field_my[i, j].BackColor = Color.Aqua;

                    but_Four_desk.Enabled = false;
                    but_Three_desk.Enabled = false;
                    but_Two_desk.Enabled = false;
                    but_One_desk.Enabled = false;

                    if (Ships_my._ALL_desk == 0)
                    {
                        but_Start.Enabled = true;
                        Field_my_enabled(false);
                    }
                    else
                        Field_my_enabled(true);
                }

            }
        }

        /// <summary>
        /// Обработчик клика по вражескому полю
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form_game_Click_enemy(object sender, EventArgs e)
        {
            if (Start)
            {
                if ((Ships_my._ALL_desk != 0) && (Ships_enemy._ALL_desk != 0))
                {
                    _j_click = (((Label)sender).Location.X - 500) / 21;
                    _i_click = (((Label)sender).Location.Y - 50) / 21;

                    Field_enemy_enabled(false);

                    switch (_Field_enemy[_i_click, _j_click])
                    {
                        case 0:
                            _Field_enemy[_i_click, _j_click] = -10;
                            Field_enemy[_i_click, _j_click].BackColor = Color.LightGray;
                            Shooting_Computer();
                            break;
                        case 1:
                            _Field_enemy[_i_click, _j_click] = -1;
                            Ships_enemy._ALL_desk--;
                            Ships_enemy._one_desk--;
                            Field_enemy[_i_click, _j_click].BackColor = Color.Black;
                            Paint_around_dead_player(_i_click, _j_click, 1, '1');
                            break;
                        case 2:
                            _Field_enemy[_i_click, _j_click] = -2;
                            Field_enemy[_i_click, _j_click].BackColor = Color.Red;
                            Ships_enemy._ALL_desk--;
                            if (Check_paint_desk(2))
                                Ships_enemy._two_desk--;
                            break;
                        case 3:
                            _Field_enemy[_i_click, _j_click] = -3;
                            Field_enemy[_i_click, _j_click].BackColor = Color.Red;
                            Ships_enemy._ALL_desk--;
                            if (Check_paint_desk(3))
                                Ships_enemy._three_desk--;
                            break;
                        case 4:
                            _Field_enemy[_i_click, _j_click] = -4;
                            Field_enemy[_i_click, _j_click].BackColor = Color.Red;
                            Ships_enemy._ALL_desk--;
                            if (Check_paint_desk(4))
                                Ships_enemy._four_desk--;
                            break;
                    }

                    
                }

                if ((Ships_my._ALL_desk != 0) && (Ships_enemy._ALL_desk != 0))
                    Field_enemy_enabled(true);
                else
                {
                    if (Ships_my._ALL_desk == 0) /// Пользователь проиграл
                    {
                        Field_enemy_enabled(false);
                        str = "Все ваши корабли уничтожены. Вы проиграли";
                    }
                    else
                    if (Ships_enemy._ALL_desk == 0) /// Компьютер проиграл
                    {
                        Field_enemy_enabled(false);
                        str = "Все корабли потивника уничтожены. Вы победили!";
                    }

                    Form_end_game ng = new Form_end_game(str);
                    ng.ShowDialog();

                    Close();
                }
            }
        }

        /// <summary>
        /// Обработчик клика сброса расположения кораблей
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void but_reset_locships_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 10; i++)
                for (int j = 0; j < 10; j++)
                {
                    Field_my[i, j].BackColor = Color.Aqua;
                    _Field_my[i, j] = 0;
                }

            Ships_my.Default();

            but_Four_desk.Enabled = false;
            but_Three_desk.Enabled = false;
            but_Two_desk.Enabled = false;
            but_One_desk.Enabled = false;
            but_reset_locships.Enabled = false;

            Field_my_enabled(true);
        }

        /// <summary>
        /// Возможность разместить палубу корабля
        /// </summary>
        /// <param name="Field"></param>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        public bool Check_locate_desk(int i, int j)
        {
            bool Check = true;

            if (Field_my[i, j].BackColor == Color.Green) return Check = false; // Сама клетка
            else
                if ((i == 0) && (j == 0)) // Верхний левый угол
            {
                if (Field_my[i, j + 1].BackColor == Color.Green) return Check = false;
                if (Field_my[i + 1, j + 1].BackColor == Color.Green) return Check = false;
                if (Field_my[i + 1, j].BackColor == Color.Green) return Check = false;
            }
            else
                if ((i == 9) && (j == 0)) // Нижний левый угол
            {
                if (Field_my[i - 1, j].BackColor == Color.Green) return Check = false;
                if (Field_my[i - 1, j + 1].BackColor == Color.Green) return Check = false;
                if (Field_my[i, j + 1].BackColor == Color.Green) return Check = false;
            }
            else
                if ((i == 0) && (j == 9)) // Правый верхний угол
            {
                if (Field_my[i + 1, j].BackColor == Color.Green) return Check = false;
                if (Field_my[i + 1, j - 1].BackColor == Color.Green) return Check = false;
                if (Field_my[i, j - 1].BackColor == Color.Green) return Check = false;
            }
            else
                if ((i == 9) && (j == 9)) // Нижний правый угол
            {
                if (Field_my[i, j - 1].BackColor == Color.Green) return Check = false;
                if (Field_my[i - 1, j - 1].BackColor == Color.Green) return Check = false;
                if (Field_my[i - 1, j].BackColor == Color.Green) return Check = false;
            }
            else
                if ((i == 0) && (j > 0) && (j < 9)) // Верхняя (нулевая) строка (без углов)
            {
                if (Field_my[i, j + 1].BackColor == Color.Green) return Check = false;
                if (Field_my[i + 1, j + 1].BackColor == Color.Green) return Check = false;
                if (Field_my[i + 1, j].BackColor == Color.Green) return Check = false;
                if (Field_my[i + 1, j - 1].BackColor == Color.Green) return Check = false;
                if (Field_my[i, j - 1].BackColor == Color.Green) return Check = false;
            }
            else
                if ((i > 0) && (i < 9) && (j == 9)) // Правый (девятый) столбец (без углов)
            {
                if (Field_my[i + 1, j].BackColor == Color.Green) return Check = false;
                if (Field_my[i + 1, j - 1].BackColor == Color.Green) return Check = false;
                if (Field_my[i, j - 1].BackColor == Color.Green) return Check = false;
                if (Field_my[i - 1, j - 1].BackColor == Color.Green) return Check = false;
                if (Field_my[i - 1, j].BackColor == Color.Green) return Check = false;
            }
            else
                if ((i == 9) && (j > 0) && (j < 9)) // Нижняя (девятая) строка (без углов)
            {
                if (Field_my[i, j - 1].BackColor == Color.Green) return Check = false;
                if (Field_my[i - 1, j - 1].BackColor == Color.Green) return Check = false;
                if (Field_my[i - 1, j].BackColor == Color.Green) return Check = false;
                if (Field_my[i - 1, j + 1].BackColor == Color.Green) return Check = false;
                if (Field_my[i, j + 1].BackColor == Color.Green) return Check = false;
            }
            else
                if ((i > 0) && (i < 9) && (j == 0)) // Левый (нулевой) столбец (без углов)
            {
                if (Field_my[i - 1, j].BackColor == Color.Green) return Check = false;
                if (Field_my[i - 1, j + 1].BackColor == Color.Green) return Check = false;
                if (Field_my[i, j + 1].BackColor == Color.Green) return Check = false;
                if (Field_my[i + 1, j + 1].BackColor == Color.Green) return Check = false;
                if (Field_my[i + 1, j].BackColor == Color.Green) return Check = false;
            }
            else
            { // Не рамка (внутренняя часть поля)
                if (Field_my[i - 1, j - 1].BackColor == Color.Green) return Check = false;
                if (Field_my[i - 1, j].BackColor == Color.Green) return Check = false;
                if (Field_my[i - 1, j + 1].BackColor == Color.Green) return Check = false;
                if (Field_my[i, j + 1].BackColor == Color.Green) return Check = false;
                if (Field_my[i + 1, j + 1].BackColor == Color.Green) return Check = false;
                if (Field_my[i + 1, j].BackColor == Color.Green) return Check = false;
                if (Field_my[i + 1, j - 1].BackColor == Color.Green) return Check = false;
                if (Field_my[i, j - 1].BackColor == Color.Green) return Check = false;
            }

            return Check;
        }

        /// <summary>
        /// Обработчик клика по кнопке старта
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void but_Start_Click(object sender, EventArgs e)
        {
            but_Four_desk.Parent = null;
            but_Three_desk.Parent = null;
            but_Two_desk.Parent = null;
            but_One_desk.Parent = null;
            but_reset_locships.Parent = null;
            but_Start.Parent = null;

            
            set_enemy_ships();

            //_Fields_enemy_equel();

            Ships_my.Default();
            Ships_enemy.Default();
            Field_enemy_enabled(true);

            Start = true;
        }

        /// <summary>
        /// Обработчик клика расположения в выбранной клетке однопалубного корабля
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void but_One_desk_Click(object sender, EventArgs e)
        {
            but_Four_desk.Enabled = false;
            but_Three_desk.Enabled = false;
            but_Two_desk.Enabled = false;
            but_One_desk.Enabled = false;

            Field_my[_i_click, _j_click].BackColor = Color.Green;
            _Field_my[_i_click, _j_click] = 1;

            Ships_my._one_desk--;
            Ships_my._ALL_desk--;

            if (Ships_my._ALL_desk == 0)
            {
                but_Start.Enabled = true;
                Field_my_enabled(false);
            }
            else
                Field_my_enabled(true);
        }

        /// <summary>
        /// Обработчик клика расположения в выбранной клетке 2-х палубного корабля
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void but_Two_desk_Click(object sender, EventArgs e)
        {
            but_Four_desk.Enabled = false;
            but_Three_desk.Enabled = false;
            but_Two_desk.Enabled = false;
            but_One_desk.Enabled = false;

            bool allow = false;

            if (_j_click > 0)
                if (Check_locate_Ship("left", 2))
                {
                    Field_my[_i_click, _j_click - 1].BackColor = Color.Gray;
                    Field_my[_i_click, _j_click - 1].Enabled = true;
                    allow = true;
                }

            if (_i_click > 0)
                if (Check_locate_Ship("up", 2))
                {
                    Field_my[_i_click - 1, _j_click].BackColor = Color.Gray;
                    Field_my[_i_click - 1, _j_click].Enabled = true;
                    allow = true;
                }

            if (_j_click < 9)
                if (Check_locate_Ship("right", 2))
                {
                    Field_my[_i_click, _j_click + 1].BackColor = Color.Gray;
                    Field_my[_i_click, _j_click + 1].Enabled = true;
                    allow = true;
                }

            if (_i_click < 9)
                if (Check_locate_Ship("down", 2))
                {
                    Field_my[_i_click + 1, _j_click].BackColor = Color.Gray;
                    Field_my[_i_click + 1, _j_click].Enabled = true;
                    allow = true;
                }

            if (allow)
            {
                Ships_my._two_desk--;
                Ships_my._ALL_desk -= 2;
            }

        }

        /// <summary>
        /// Кнопка выхода из игры (в главное меню)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void but_end_game_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Обработчик клика расположения в выбранной клетке 3-х палубного корабля
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void but_Three_desk_Click(object sender, EventArgs e)
        {
            but_Four_desk.Enabled = false;
            but_Three_desk.Enabled = false;
            but_Two_desk.Enabled = false;
            but_One_desk.Enabled = false;

            bool allow = false;

            if (_j_click > 1)
                if (Check_locate_Ship("left", 3))
                {
                    Field_my[_i_click, _j_click - 2].BackColor = Color.Gray;
                    Field_my[_i_click, _j_click - 2].Enabled = true;
                    allow = true;
                }

            if (_i_click > 1)
                if (Check_locate_Ship("up", 3))
                {
                    Field_my[_i_click - 2, _j_click].BackColor = Color.Gray;
                    Field_my[_i_click - 2, _j_click].Enabled = true;
                    allow = true;
                }

            if (_j_click < 8)
                if (Check_locate_Ship("right", 3))
                {
                    Field_my[_i_click, _j_click + 2].BackColor = Color.Gray;
                    Field_my[_i_click, _j_click + 2].Enabled = true;
                    allow = true;
                }

            if (_i_click < 8)
                if (Check_locate_Ship("down", 3))
                {
                    Field_my[_i_click + 2, _j_click].BackColor = Color.Gray;
                    Field_my[_i_click + 2, _j_click].Enabled = true;
                    allow = true;
                }

            if (allow)
            {
                Ships_my._three_desk--;
                Ships_my._ALL_desk -= 3;
            }
        }

        /// <summary>
        /// Обработчик клика расположения в выбранной клетке 4-х палубного корабля
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void but_Four_desk_Click(object sender, EventArgs e)
        {
            but_Four_desk.Enabled = false;
            but_Three_desk.Enabled = false;
            but_Two_desk.Enabled = false;
            but_One_desk.Enabled = false;

            bool allow = false;

            if (_j_click > 2)
                if (Check_locate_Ship("left", 4))
                {
                    Field_my[_i_click, _j_click - 3].BackColor = Color.Gray;
                    Field_my[_i_click, _j_click - 3].Enabled = true;
                    allow = true;
                }

            if (_i_click > 2)
                if (Check_locate_Ship("up", 4))
                {
                    Field_my[_i_click - 3, _j_click].BackColor = Color.Gray;
                    Field_my[_i_click - 3, _j_click].Enabled = true;
                    allow = true;
                }

            if (_j_click < 7)
                if (Check_locate_Ship("right", 4))
                {
                    Field_my[_i_click, _j_click + 3].BackColor = Color.Gray;
                    Field_my[_i_click, _j_click + 3].Enabled = true;
                    allow = true;
                }

            if (_i_click < 7)
                if (Check_locate_Ship("down", 4))
                {
                    Field_my[_i_click + 3, _j_click].BackColor = Color.Gray;
                    Field_my[_i_click + 3, _j_click].Enabled = true;
                    allow = true;
                }

            if (allow)
            {
                Ships_my._four_desk--;
                Ships_my._ALL_desk -= 4;
            }
        }

        /// <summary>
        /// Метод вкл/выкл лейблов (клеток)
        /// </summary>
        /// <param name="enabled"></param>
        public void Field_my_enabled(bool enabled)
        {
            for (int i = 0; i < 10; i++)
                for (int j = 0; j < 10; j++)
                    if (enabled)
                        Field_my[i, j].Enabled = true;
                    else
                        Field_my[i, j].Enabled = false;
        }

        /// <summary>
        /// Mетод вкл/выкл лейблов (клеток компа)
        /// </summary>
        /// <param name="enabled"></param>
        public void Field_enemy_enabled(bool enabled)
        {
            for (int i = 0; i < 10; i++)
                for (int j = 0; j < 10; j++)
                    if (enabled)
                        Field_enemy[i, j].Enabled = true;
                    else
                        Field_enemy[i, j].Enabled = false;
        }

        /// <summary>
        /// Возможность разместить остальную часть корабля (игрок)
        /// </summary>
        /// <param name="Field"></param>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        public bool Check_locate_Ship(string turn, int Number_of_desk)
        {
            bool Chek = true;

            switch (turn)
            {
                case "left":
                    for (int j = _j_LightGray; j >= _j_LightGray - (Number_of_desk - 1); j--)
                        if (!Check_locate_desk(_i_click, j))
                            Chek = false;
                    break;

                case "up":
                    for (int i = _i_LightGray; i >= _i_LightGray - (Number_of_desk - 1); i--)
                        if (!Check_locate_desk(i, _j_click))
                            Chek = false;
                    break;

                case "right":
                    for (int j = _j_LightGray; j <= _j_LightGray + (Number_of_desk - 1); j++)
                        if (!Check_locate_desk(_i_click, j))
                            Chek = false;
                    break;

                case "down":
                    for (int i = _i_LightGray; i <= _i_LightGray + (Number_of_desk - 1); i++)
                        if (!Check_locate_desk(i, _j_click))
                            Chek = false;
                    break;
            }
            return Chek;
        }

        /// <summary>
        /// Установка вражеских кораблей
        /// </summary>
        private void set_enemy_ships()
        {
            Random rnd = new Random();
            while (Ships_enemy._ALL_desk != 0)
            {
                // Установка 4-хпалубника
                while ((Ships_enemy._four_desk > 0) && (Ships_enemy._ALL_desk > 0))
                {
                    _i_LightGray = rnd.Next(0, 10);
                    _j_LightGray = rnd.Next(0, 10);

                    while (!_Check_locate_desk(_i_LightGray, _j_LightGray))
                    {
                        _i_LightGray = rnd.Next(0, 10);
                        _j_LightGray = rnd.Next(0, 10);
                    }

                    if (_Check_locate_Ship(4))
                    {
                        _Set_locate_ship(4);
                        Ships_enemy._four_desk--;
                        Ships_enemy._ALL_desk -= 4;
                    }
                }

                // Установка 3-хпалубника
                while ((Ships_enemy._three_desk > 0) && (Ships_enemy._ALL_desk > 0))
                {
                    _i_LightGray = rnd.Next(0, 10);
                    _j_LightGray = rnd.Next(0, 10);

                    while (!_Check_locate_desk(_i_LightGray, _j_LightGray))
                    {
                        _i_LightGray = rnd.Next(0, 10);
                        _j_LightGray = rnd.Next(0, 10);
                    }

                    if (_Check_locate_Ship(3))
                    {
                        _Set_locate_ship(3);
                        Ships_enemy._three_desk--;
                        Ships_enemy._ALL_desk -= 3;

                    }
                }

                // Установка 2-хпалубника
                while ((Ships_enemy._two_desk > 0) && (Ships_enemy._ALL_desk > 0))
                {
                    _i_LightGray = rnd.Next(0, 10);
                    _j_LightGray = rnd.Next(0, 10);

                    while (!_Check_locate_desk(_i_LightGray, _j_LightGray))
                    {
                        _i_LightGray = rnd.Next(0, 10);
                        _j_LightGray = rnd.Next(0, 10);
                    }

                    if (_Check_locate_Ship(2))
                    {
                        _Set_locate_ship(2);
                        Ships_enemy._two_desk--;
                        Ships_enemy._ALL_desk -= 2;
                    }
                }

                // Установка однопалубника
                while ((Ships_enemy._one_desk > 0) && (Ships_enemy._ALL_desk > 0))
                {
                    _i_LightGray = rnd.Next(0, 10);
                    _j_LightGray = rnd.Next(0, 10);

                    while (!_Check_locate_desk(_i_LightGray, _j_LightGray))
                    {
                        _i_LightGray = rnd.Next(0, 10);
                        _j_LightGray = rnd.Next(0, 10);
                    }

                    _Field_enemy[_i_LightGray, _j_LightGray] = 1;
                    Ships_enemy._one_desk--;
                    Ships_enemy._ALL_desk--;

                }

            }
        }

        /// <summary>
        /// Возможность разместить палубу корабля (комп)
        /// </summary>
        /// <param name="Field"></param>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        public bool _Check_locate_desk(int i, int j)
        {
            bool Check = true;

            if ((_Field_enemy[i, j] >= 1) && (_Field_enemy[i, j] <= 4)) return Check = false; // Сама клетка
            else
                if ((i == 0) && (j == 0)) // Верхний левый угол
            {
                if ((_Field_enemy[i, j + 1] >= 1) && (_Field_enemy[i, j + 1] <= 4)) return Check = false;
                if ((_Field_enemy[i + 1, j + 1] >= 1) && (_Field_enemy[i + 1, j + 1] <= 4)) return Check = false;
                if ((_Field_enemy[i + 1, j] >= 1) && (_Field_enemy[i + 1, j] >= 1)) return Check = false;
            }
            else
                if ((i == 9) && (j == 0)) // Нижний левый угол
            {
                if ((_Field_enemy[i - 1, j] >= 1) && (_Field_enemy[i - 1, j] <= 4)) return Check = false;
                if ((_Field_enemy[i - 1, j + 1] >= 1) && (_Field_enemy[i - 1, j + 1] <= 4)) return Check = false;
                if ((_Field_enemy[i, j + 1] >= 1) && (_Field_enemy[i, j + 1] <= 4)) return Check = false;
            }
            else
                if ((i == 0) && (j == 9)) // Правый верхний угол
            {
                if ((_Field_enemy[i + 1, j] >= 1) && (_Field_enemy[i + 1, j] <= 4)) return Check = false;
                if ((_Field_enemy[i + 1, j - 1] >= 1) && (_Field_enemy[i + 1, j - 1] <= 4)) return Check = false;
                if ((_Field_enemy[i, j - 1] >= 1) && (_Field_enemy[i, j - 1] <= 4)) return Check = false;
            }
            else
                if ((i == 9) && (j == 9)) // Нижний правый угол
            {
                if ((_Field_enemy[i, j - 1] >= 1) && (_Field_enemy[i, j - 1] <= 4)) return Check = false;
                if ((_Field_enemy[i - 1, j - 1] >= 1) && (_Field_enemy[i - 1, j - 1] <= 4)) return Check = false;
                if ((_Field_enemy[i - 1, j] >= 1) && (_Field_enemy[i - 1, j] <= 4)) return Check = false;
            }
            else
                if ((i == 0) && (j > 0) && (j < 9)) // Верхняя (нулевая) строка (без углов)
            {
                if ((_Field_enemy[i, j + 1] >= 1) && (_Field_enemy[i, j + 1] <= 4)) return Check = false;
                if ((_Field_enemy[i + 1, j + 1] >= 1) && (_Field_enemy[i + 1, j + 1] <= 4)) return Check = false;
                if ((_Field_enemy[i + 1, j] >= 1) && (_Field_enemy[i + 1, j] <= 4)) return Check = false;
                if ((_Field_enemy[i + 1, j - 1] >= 1) && (_Field_enemy[i + 1, j - 1] <= 4)) return Check = false;
                if ((_Field_enemy[i, j - 1] >= 1) && (_Field_enemy[i, j - 1] <= 4)) return Check = false;
            }
            else
                if ((i > 0) && (i < 9) && (j == 9)) // Правый (девятый) столбец (без углов)
            {
                if ((_Field_enemy[i + 1, j] >= 1) && (_Field_enemy[i + 1, j] <= 4)) return Check = false;
                if ((_Field_enemy[i + 1, j - 1] >= 1) && (_Field_enemy[i + 1, j - 1] <= 4)) return Check = false;
                if ((_Field_enemy[i, j - 1] >= 1) && (_Field_enemy[i, j - 1] <= 4)) return Check = false;
                if ((_Field_enemy[i - 1, j - 1] >= 1) && (_Field_enemy[i - 1, j - 1] <= 4)) return Check = false;
                if ((_Field_enemy[i - 1, j] >= 1) && (_Field_enemy[i - 1, j] <= 4)) return Check = false;
            }
            else
                if ((i == 9) && (j > 0) && (j < 9)) // Нижняя (девятая) строка (без углов)
            {
                if ((_Field_enemy[i, j - 1] >= 1) && (_Field_enemy[i, j - 1] <= 4)) return Check = false;
                if ((_Field_enemy[i - 1, j - 1] >= 1) && (_Field_enemy[i - 1, j - 1] <= 4)) return Check = false;
                if ((_Field_enemy[i - 1, j] >= 1) && (_Field_enemy[i - 1, j] <= 4)) return Check = false;
                if ((_Field_enemy[i - 1, j + 1] >= 1) && (_Field_enemy[i - 1, j + 1] <= 4)) return Check = false;
                if ((_Field_enemy[i, j + 1] >= 1) && (_Field_enemy[i, j + 1] <= 4)) return Check = false;
            }
            else
                if ((i > 0) && (i < 9) && (j == 0)) // Левый (нулевой) столбец (без углов)
            {
                if ((_Field_enemy[i - 1, j] >= 1) && (_Field_enemy[i - 1, j] <= 4)) return Check = false;
                if ((_Field_enemy[i - 1, j + 1] >= 1) && (_Field_enemy[i - 1, j + 1] <= 4)) return Check = false;
                if ((_Field_enemy[i, j + 1] >= 1) && (_Field_enemy[i, j + 1] <= 4)) return Check = false;
                if ((_Field_enemy[i + 1, j + 1] >= 1) && (_Field_enemy[i + 1, j + 1] <= 4)) return Check = false;
                if ((_Field_enemy[i + 1, j] >= 1) && (_Field_enemy[i + 1, j] <= 4)) return Check = false;
            }
            else
            { // Не рамка (внутренняя часть поля)
                if ((_Field_enemy[i - 1, j - 1] >= 1) && (_Field_enemy[i - 1, j - 1] <= 4)) return Check = false;
                if ((_Field_enemy[i - 1, j] >= 1) && (_Field_enemy[i - 1, j] <= 4)) return Check = false;
                if ((_Field_enemy[i - 1, j + 1] >= 1) && (_Field_enemy[i - 1, j + 1] <= 4)) return Check = false;
                if ((_Field_enemy[i, j + 1] >= 1) && (_Field_enemy[i, j + 1] <= 4)) return Check = false;
                if ((_Field_enemy[i + 1, j + 1] >= 1) && (_Field_enemy[i + 1, j + 1] <= 4)) return Check = false;
                if ((_Field_enemy[i + 1, j] >= 1) && (_Field_enemy[i + 1, j] <= 4)) return Check = false;
                if ((_Field_enemy[i + 1, j - 1] >= 1) && (_Field_enemy[i + 1, j - 1] <= 4)) return Check = false;
                if ((_Field_enemy[i, j - 1] >= 1) && (_Field_enemy[i, j - 1] <= 4)) return Check = false;
            }

            return Check;
        }

        /// <summary>
        /// Возможность разместить остальную часть корабля (комп)
        /// </summary>
        /// <param name="Field"></param>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        public bool _Check_locate_Ship(int Number_of_desk)
        {
            bool Check = false;

            bool Check_left = true, Check_up = true, Check_right = true, Check_down = true;

            if ((_j_LightGray - (Number_of_desk - 1)) >= 0)
            {
                for (int j = _j_LightGray; j >= _j_LightGray - (Number_of_desk - 1); j--) /// LEFT
                    if (!_Check_locate_desk(_i_LightGray, j))
                    {
                        Check_left = false;
                        break;
                    }

                if (Check_left)
                {
                    Check = true;
                    terns[0].allow = true;
                    terns[0].i = _i_LightGray;
                    terns[0].j = _j_LightGray - (Number_of_desk - 1);
                }
            }

            if ((_i_LightGray - (Number_of_desk - 1)) >= 0)
            {
                for (int i = _i_LightGray; i >= _i_LightGray - (Number_of_desk - 1); i--) /// UP
                    if (!_Check_locate_desk(i, _j_LightGray))
                    {
                        Check_up = false;
                        break;
                    }

                if (Check_up)
                {
                    Check = true;
                    terns[1].allow = true;
                    terns[1].i = _i_LightGray - (Number_of_desk - 1);
                    terns[1].j = _j_LightGray;
                }
            }

            if ((_j_LightGray + (Number_of_desk - 1)) <= 9)
            {
                for (int j = _j_LightGray; j <= _j_LightGray + (Number_of_desk - 1); j++) /// RIGHT
                    if (!_Check_locate_desk(_i_LightGray, j))
                    {
                        Check_right = false;
                        break;
                    }

                if (Check_right)
                {
                    Check = true;
                    terns[2].allow = true;
                    terns[2].i = _i_LightGray;
                    terns[2].j = _j_LightGray + (Number_of_desk - 1);
                }
            }

            if ((_i_LightGray + (Number_of_desk - 1)) <= 9)
            {
                for (int i = _i_LightGray; i <= _i_LightGray + (Number_of_desk - 1); i++) /// DOWN
                    if (!_Check_locate_desk(i, _j_LightGray))
                    {
                        Check_down = false;
                        break;
                    }

                if (Check_down)
                {
                    Check = true;
                    terns[3].allow = true;
                    terns[3].i = _i_LightGray + (Number_of_desk - 1);
                    terns[3].j = _j_LightGray;
                }
            }

            return Check;
        }

        /// <summary>
        /// Установка n-палубника (комп)
        /// </summary>
        /// <param name="n"></param>
        public void _Set_locate_ship(int n)
        {
            for (int i = 0; i <= 3; i++)
                if (terns[i].allow)
                {
                    if (i == 0)
                        for (int k = _j_LightGray; k >= terns[i].j; k--)
                            _Field_enemy[_i_LightGray, k] = n;

                    if (i == 1)
                        for (int k = _i_LightGray; k >= terns[i].i; k--)
                            _Field_enemy[k, _j_LightGray] = n;

                    if (i == 2)
                        for (int k = _j_LightGray; k <= terns[i].j; k++)
                            _Field_enemy[_i_LightGray, k] = n;

                    if (i == 3)
                        for (int k = _i_LightGray; k <= terns[i].i; k++)
                            _Field_enemy[k, _j_LightGray] = n;
                    break;
                }
            set_allows(false);
        }

        /// <summary>
        /// Перенос состояния числового массива поля противника (компа) на лейблы
        /// </summary>
        public void _Fields_enemy_equel()
        {
            for (int i = 0; i < 10; i++)
                for (int j = 0; j < 10; j++)
                {
                    if (_Field_enemy[i, j] == 0)
                        Field_enemy[i, j].BackColor = Color.Aqua;
                    else
                        Field_enemy[i, j].BackColor = Color.Green;
                }
        }

        /// <summary>
        /// Проверка возможности закраски и сама закраска (стреляет игрок)
        /// </summary>
        /// <param name="Field"></param>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        public bool Check_paint_desk(int number_of_desk)
        {
            bool Check = false;

            int Red = number_of_desk;

            for (int j = _j_click - (number_of_desk - 1); j <= _j_click + (number_of_desk - 1); j++) // Проверка горизонтали
                if ((j >= 0) && (j <= 9))
                {
                    if (_Field_enemy[_i_click, j] == ((-1) * number_of_desk))
                        Red--;
                }

            if (Red == 0)// Покраска горизонтали
            {
                Check = true;
                int j_last = 0;

                for (int j = _j_click - (number_of_desk - 1); j <= _j_click + (number_of_desk - 1); j++)
                    if ((j >= 0) && (j <= 9))
                    {
                        if (Field_enemy[_i_click, j].BackColor == Color.Red)
                        {
                            j_last = j;
                            Field_enemy[_i_click, j].BackColor = Color.Black;
                            _Field_enemy[_i_click, j] = number_of_desk * (-10);
                        }
                    }
                Paint_around_dead_player(_i_click, j_last, number_of_desk, 'r');
            }

            Red = number_of_desk;

            for (int i = _i_click - (number_of_desk - 1); i <= _i_click + (number_of_desk - 1); i++) // Проверка вертикали
                if ((i >= 0) && (i <= 9))
                {
                    if (_Field_enemy[i, _j_click] == ( (-1) * number_of_desk ) )
                        Red--;
                }

            if (Red == 0) // Покраска вертикали
            {
                Check = true;
                int i_last = 0;

                for (int i = _i_click - (number_of_desk - 1); i <= _i_click + (number_of_desk - 1); i++)
                    if ((i >= 0) && (i <= 9))
                    {
                        if (Field_enemy[i, _j_click].BackColor == Color.Red)
                        {
                            i_last = i;
                            Field_enemy[i, _j_click].BackColor = Color.Black;
                            _Field_enemy[i, _j_click] = number_of_desk * (-10);
                        }
                    }
                Paint_around_dead_player(i_last, _j_click, number_of_desk, 'd');
            }

            return Check;
        }

        /// <summary>
        /// Стреляет комп
        /// </summary>
        public void Shooting_Computer()
        {
            Random rnd = new Random();

            Random_coords:
            if(first_hit)
            {
                i_rand = rnd.Next(0, 10);
                j_rand = rnd.Next(0, 10);
            }

            if (Field_my[i_rand, j_rand].BackColor == Color.Aqua)
            {
                Field_my[i_rand, j_rand].BackColor = Color.LightGray;
                _Field_my[i_rand, j_rand] = -10;
            }
            else
                if (Field_my[i_rand, j_rand].BackColor == Color.LightGray)
                    goto Random_coords;
            else
                if (Field_my[i_rand, j_rand].BackColor == Color.Black)
                    goto Random_coords;
            else
                if ((Field_my[i_rand, j_rand].BackColor == Color.Green) || (Field_my[i_rand, j_rand].BackColor == Color.Red))
            {
                if(_Field_my[i_rand, j_rand] == 1) // Если попал в однопалубник
                {
                    _Field_my[i_rand, j_rand] = -1;
                    Field_my[i_rand, j_rand].BackColor = Color.Black;
                    Paint_around_dead(i_rand, j_rand, 1, '1');
                    Ships_my._ALL_desk--;
                    goto Random_coords;
                }
                else
                {
                    if (Field_my[i_rand, j_rand].BackColor == Color.Green)  //если было 1 попадание
                    {
                        i_first = i_rand;
                        j_first = j_rand;
                        n_palub = _Field_my[i_rand, j_rand] - 1;
                        kol_palub_kor = _Field_my[i_rand, j_rand];
                        _Field_my[i_rand, j_rand] *= -1;
                        Field_my[i_rand, j_rand].BackColor = Color.Red;
                        Ships_my._ALL_desk--;
                        first_hit = false;
                    }

                    //после первого попадания
                    if(!more_two_hit)
                        tern = rnd.Next(0, 4);  //Генерируем направление

                    #region Блокировка направлений
                    if(j_rand == 0) //Если у левой гранцы, то заблокируй и иди вправо
                    {
                        tern_left = false;
                        tern = 2;
                    }
                    if(j_rand == 9) //Если у правой гранцы, то заблокируй и иди влево
                    {
                        tern_right = false;
                        tern = 0;
                    }
                    if(i_rand == 0) //Если у верхней гранцы, то заблокируй и иди вниз
                    {
                        tern_up = false;
                        tern = 3;
                    }
                    if(i_rand == 9) //Если у нижней гранцы, то заблокируй и иди вверх
                    {
                        tern_down = false;
                        tern = 1;
                    }

                    if((j_rand != 0) && (Field_my[i_rand, j_rand - 1].BackColor == Color.LightGray))   //Если слева не граница и там был промах
                    {
                        tern_left = false;
                        tern = 2;
                    }
                    if ((i_rand != 0) && (Field_my[i_rand - 1, j_rand].BackColor == Color.LightGray))   //Если вверху не границы и там был промах
                    {
                        tern_up = false;
                        tern = 3;
                    }
                    if((j_rand != 9) && (Field_my[i_rand, j_rand + 1].BackColor == Color.LightGray)) //Если справа не граница и там был промах
                    {
                        tern_right = false;
                        tern = 0;
                    }
                    if ((i_rand != 9) && (Field_my[i_rand + 1, j_rand].BackColor == Color.LightGray)) //Если снизу не граница и там был промах
                    {
                        tern_down = false;
                        tern = 1;
                    }

                    if((!tern_left) && (!tern_right) && (i_rand != 0) && (Field_my[i_rand - 1, j_rand].BackColor == Color.Red))
                    {
                        //если слева и справа промахи, а сверху попадание, заблокируй верх и иди вниз
                        tern_up = false;
                        tern = 3;
                    }
                    if((!tern_left) && (!tern_right) && (i_rand != 9) && (Field_my[i_rand + 1, j_rand].BackColor == Color.Red))
                    {
                        //если слева и справа промахи, а снизу попадание, заблокируй низ и иди в верх
                        tern_down = false;
                        tern = 1;
                    }
                    if((!tern_up) && (!tern_down) && (j_rand != 0) && (Field_my[i_rand, j_rand - 1].BackColor == Color.Red))
                    {
                        //если сверху и снизу промах, а слева попадание, заблокируй лево и иди вправо
                        tern_left = false;
                        tern = 2;
                    }
                    if((!tern_up) && (!tern_down) && (j_rand != 9) && (Field_my[i_rand, j_rand + 1].BackColor == Color.Red))
                    {
                        //если сверху и снизу промах, а справа попадание, заблокируй справа и иди влево
                        tern_right = false;
                        tern = 0;
                    }

                    if ((!tern_left) && (!tern_up) && (!tern_right))    //Если снизу свободно
                        tern = 3;
                    else
                    if ((!tern_up) && (!tern_right) && (!tern_down))    //Если слева свободно
                        tern = 0;
                    else
                    if ((!tern_right) && (!tern_down) && (!tern_left))  //Если сверху свободно
                        tern = 1;
                    else
                    if ((!tern_down) && (!tern_left) && (!tern_up))      //Если справа свободно
                        tern = 2;
                    else
                    if ((!tern_left) && (!tern_right))                  //Если нельзя влево и вправо
                    {
                        if (tern_up)    //Если сверху свободно
                            tern = 1;
                        else
                            tern = 3;
                    }
                    else
                    if ((!tern_up) && (!tern_down))                     //Если нельзя вверх и вниз
                    {
                        if (tern_left)  //Если слева свободно
                            tern = 0;
                        else
                            tern = 2;
                    }
                    #endregion

                    switch (tern)
                    {
                        #region Left
                        case 0:
                            j_rand = j_rand - 1;    //Сдвигаемся влево
                            if(Field_my[i_rand, j_rand].BackColor == Color.Aqua)    //если промах, ставь его и блоч
                            {
                                _Field_my[i_rand, j_rand] = -10;
                                Field_my[i_rand, j_rand].BackColor = Color.LightGray;
                                tern_left = false;
                                j_rand = j_first;
                            }
                            else
                                if(Field_my[i_rand, j_rand].BackColor == Color.Green)   //если попал
                            {
                                _Field_my[i_rand, j_rand] *= -1;
                                Field_my[i_rand, j_rand].BackColor = Color.Red; //отметь попадание
                                n_palub--;  //уменьш счтчик оставшихся подбитых палуб
                                Ships_my._ALL_desk--;   //уменьш общее кол. палуб
                                more_two_hit = true;    //второе попадание было совершено +

                                if(n_palub == 0)    //если корабль добит
                                {
                                    _i_click = i_rand;
                                    _j_click = j_rand;
                                    Check_paint_desk_comp(kol_palub_kor);   //закрась в черный и закрась обводку
                                    Default_tern();     //открыть все направления для след кор.
                                    first_hit = true;   //открой доступ к генерации новых кораблей
                                    more_two_hit = false;   //второго попадания не было т.к. кор. убит +
                                    goto Random_coords;
                                }
                                if ((j_rand == 0) && (n_palub != 0))    //если слева стена и кор. не добит иди вправо
                                    goto case 2;

                                goto case 0;
                            }
                            else
                                if(Field_my[i_rand, j_rand].BackColor == Color.Red) //после промоха координаты устанавливаются на первую подбитую клетку (при этом было как минимум 2 попадания)
                            {
                                j_rand = j_first;   //при 2-ух пападаний и более последовал промах, координаты установились на координаты 1-го попадания,
                                                    //направление не меняется. при заход в case направление остается прежним, но в первой строчке мы сдвигаемся влево,
                                                    //поэтому мы должны убрать это изменение и относительно первого попадания должны стрелять в др. сторону
                                if ((!tern_left) && (n_palub != 0))     //если промах был слева от корабля, то иди вправо
                                    goto case 2;
                            }
                            else
                                if(Field_my[i_rand, j_rand].BackColor == Color.LightGray)
                            {
                                tern_left = false;
                                j_rand = j_first;
                                goto case 2;
                            }                        
                            break;
                        #endregion

                        #region Up
                        case 1:
                            i_rand = i_rand - 1;
                            if (Field_my[i_rand, j_rand].BackColor == Color.Aqua)
                            {
                                _Field_my[i_rand, j_rand] = -10;
                                Field_my[i_rand, j_rand].BackColor = Color.LightGray;
                                tern_up = false;
                                i_rand = i_first;
                            }
                            else
                                 if (Field_my[i_rand, j_rand].BackColor == Color.Green)
                            {
                                _Field_my[i_rand, j_rand] *= -1;
                                Field_my[i_rand, j_rand].BackColor = Color.Red;
                                n_palub--;
                                Ships_my._ALL_desk--;
                                more_two_hit = true;

                                if (n_palub == 0)
                                {
                                    _i_click = i_rand;
                                    _j_click = j_rand;
                                    Check_paint_desk_comp(kol_palub_kor);
                                    Default_tern();
                                    first_hit = true;
                                    more_two_hit = false;
                                    goto Random_coords;
                                }
                                if ((i_rand == 0) && (n_palub != 0))
                                    goto case 3;

                                goto case 1;
                            }
                            else
                                if (Field_my[i_rand, j_rand].BackColor == Color.Red)
                            {
                                i_rand = i_first;
                                if ((!tern_up) && (n_palub != 0))
                                    goto case 3;
                            }
                            else 
                                if(Field_my[i_rand, j_rand].BackColor == Color.LightGray)
                            {
                                tern_up = false;
                                i_rand = i_first;
                                goto case 3;
                            }
                            break;
                        #endregion

                        #region Right
                        case 2:
                            j_rand = j_rand + 1;
                            if(Field_my[i_rand, j_rand].BackColor == Color.Aqua)
                            {
                                _Field_my[i_rand, j_rand] = -10;
                                Field_my[i_rand, j_rand].BackColor = Color.LightGray;
                                tern_right = false;
                                j_rand = j_first;
                            }
                            else
                            if (Field_my[i_rand, j_rand].BackColor == Color.Green)
                            {
                                _Field_my[i_rand, j_rand] *= -1;
                                Field_my[i_rand, j_rand].BackColor = Color.Red;
                                n_palub--;
                                Ships_my._ALL_desk--;
                                more_two_hit = true;

                                if (n_palub == 0)
                                {
                                    _i_click = i_rand;
                                    _j_click = j_rand;
                                    Check_paint_desk_comp(kol_palub_kor);
                                    Default_tern();
                                    first_hit = true;
                                    more_two_hit = false;
                                    goto Random_coords;
                                }
                                if ((j_rand == 9) && (n_palub != 0))
                                    goto case 0;

                                goto case 2;
                            }
                            else
                                if (Field_my[i_rand, j_rand].BackColor == Color.Red)
                            {
                                j_rand = j_first;
                                if ((!tern_right) && (n_palub != 0))
                                    goto case 0;
                            }
                            else
                                if(Field_my[i_rand, j_rand].BackColor == Color.LightGray)
                            {
                                tern_right = false;
                                j_rand = j_first;
                                goto case 0;
                            }
                            break;
                        #endregion

                        #region Down
                        case 3:
                            i_rand = i_rand + 1;
                            if(Field_my[i_rand, j_rand].BackColor == Color.Aqua)
                            {
                                _Field_my[i_rand, j_rand] = -10;
                                Field_my[i_rand, j_rand].BackColor = Color.LightGray;
                                tern_down = false;
                                i_rand = i_first;
                            }
                            else
                                if(Field_my[i_rand, j_rand].BackColor == Color.Green)
                            {
                                _Field_my[i_rand, j_rand] *= -1;
                                Field_my[i_rand, j_rand].BackColor = Color.Red;
                                n_palub--;
                                Ships_my._ALL_desk--;
                                more_two_hit = true;

                                if (n_palub == 0)
                                {
                                    _i_click = i_rand;
                                    _j_click = j_rand;
                                    Check_paint_desk_comp(kol_palub_kor);
                                    Default_tern();
                                    first_hit = true;
                                    more_two_hit = false;
                                    goto Random_coords;
                                }

                                if ((i_rand == 9) && (n_palub != 0))
                                    goto case 1;

                                goto case 3;
                            }
                            else
                                if(Field_my[i_rand, j_rand].BackColor == Color.Red)
                            {
                                i_rand = i_first;
                                if ((!tern_down) && (n_palub != 0))
                                    goto case 1;
                            }
                            else
                                if(Field_my[i_rand, j_rand].BackColor == Color.LightGray)
                            {
                                tern_down = false;
                                i_rand = i_first;
                                goto case 1;
                            }
                            break;
                            #endregion
                    }
                }


            }
            // Стрельба компа
        }

        /// <summary>
        /// Проверка возможности закраски и сама закраска (стреляет комп)
        /// </summary>
        /// <param name="Field"></param>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        public bool Check_paint_desk_comp(int number_of_desk)
        {
            bool Check = false;

            int Red_gor = number_of_desk, Red_ver = number_of_desk;

            for (int j = _j_click - (number_of_desk - 1); j <= _j_click + (number_of_desk - 1); j++) // Проверка горизонтали
                if ((j >= 0) && (j <= 9))
                {
                    if (_Field_my[_i_click, j] == ( (-1) * number_of_desk ))
                        Red_gor--;
                }

            if (Red_gor == 0)// Покраска горизонтали
            {
                Check = true;

                int j_lst = 0;

                for (int j = _j_click - (number_of_desk - 1); j <= _j_click + (number_of_desk - 1); j++)
                    if ((j >= 0) && (j <= 9))
                    {
                        if (Field_my[_i_click, j].BackColor == Color.Red)
                        {
                            j_lst = j;
                            Field_my[_i_click, j].BackColor = Color.Black;
                            _Field_my[_i_click, j] = number_of_desk * (-10);
                        }
                    }

                Paint_around_dead(_i_click, j_lst, number_of_desk, 'r');
            }
            else
            {
                for (int i = _i_click - (number_of_desk - 1); i <= _i_click + (number_of_desk - 1); i++) // Проверка вертикали
                    if ((i >= 0) && (i <= 9))
                    {
                        if (_Field_my[i, _j_click] == ( (-1) * number_of_desk ))
                            Red_ver--;
                    }

                if (Red_ver == 0) // Покраска вертикали
                {
                    Check = true;

                    int i_lst = 0;

                    for (int i = _i_click - (number_of_desk - 1); i <= _i_click + (number_of_desk - 1); i++)
                        if ((i >= 0) && (i <= 9))
                        {
                            if (Field_my[i, _j_click].BackColor == Color.Red)
                            {
                                i_lst = i;
                                Field_my[i, _j_click].BackColor = Color.Black;
                                _Field_my[i, _j_click] = number_of_desk * (-10);
                            }
                        }

                    Paint_around_dead(i_lst, _j_click, number_of_desk, 'd');
                }
            }
                

            return Check;
        }

        /// <summary>
        /// Закраска клеток вокруг уничтоженного корабля
        /// </summary>
        /// <param name="i_last"></param>
        /// <param name="j_last"></param>
        /// <param name="number_of_desk"></param>
        /// <param name="pos"></param>
        public void Paint_around_dead(int i_last, int j_last, int number_of_desk, char pos)
        {
            switch (pos)
            {
                case 'r':
                    for (int i = i_last - 1; i <= i_last + 1; i++)
                        for (int j = j_last - number_of_desk; j <= j_last + 1; j++)
                            if ( (i >= 0) && (i <= 9) && (j >= 0) && (j <= 9) )
                                if (Field_my[i, j].BackColor == Color.Aqua)
                                {
                                    Field_my[i, j].BackColor = Color.LightGray;
                                    _Field_my[i, j] = -10;
                                }
                    break;

                case 'd':
                    for (int i = i_last - number_of_desk; i <= i_last + 1; i++)
                        for (int j = j_last - 1; j <= j_last + 1; j++)
                            if ((i >= 0) && (i <= 9) && (j >= 0) && (j <= 9))
                                if (Field_my[i, j].BackColor == Color.Aqua)
                                {
                                    Field_my[i, j].BackColor = Color.LightGray;
                                    _Field_my[i, j] = -10;
                                }
                    break;

                case '1':
                    for (int i = i_last - 1; i <= i_last + 1; i++)
                        for (int j = j_last - 1; j <= j_last + 1; j++)
                            if ((i >= 0) && (i <= 9) && (j >= 0) && (j <= 9))
                                if (Field_my[i, j].BackColor == Color.Aqua)
                                {
                                    Field_my[i, j].BackColor = Color.LightGray;
                                    _Field_my[i, j] = -10;
                                }
                    break;
            }
        }

        public void Paint_around_dead_player(int i_last, int j_last, int number_of_desk, char pos)
        {
            switch (pos)
            {
                case 'r':
                    for (int i = i_last - 1; i <= i_last + 1; i++)
                        for (int j = j_last - number_of_desk; j <= j_last + 1; j++)
                            if ((i >= 0) && (i <= 9) && (j >= 0) && (j <= 9))
                                if (Field_enemy[i, j].BackColor == Color.Aqua)
                                {
                                    Field_enemy[i, j].BackColor = Color.LightGray;
                                    _Field_enemy[i, j] = -10;
                                }
                    break;

                case 'd':
                    for (int i = i_last - number_of_desk; i <= i_last + 1; i++)
                        for (int j = j_last - 1; j <= j_last + 1; j++)
                            if ((i >= 0) && (i <= 9) && (j >= 0) && (j <= 9))
                                if (Field_enemy[i, j].BackColor == Color.Aqua)
                                {
                                    Field_enemy[i, j].BackColor = Color.LightGray;
                                    _Field_enemy[i, j] = -10;
                                }
                    break;

                case '1':
                    for (int i = i_last - 1; i <= i_last + 1; i++)
                        for (int j = j_last - 1; j <= j_last + 1; j++)
                            if ((i >= 0) && (i <= 9) && (j >= 0) && (j <= 9))
                                if (Field_enemy[i, j].BackColor == Color.Aqua)
                                {
                                    Field_enemy[i, j].BackColor = Color.LightGray;
                                    _Field_enemy[i, j] = -10;
                                }
                    break;
            }
            }
    }
}