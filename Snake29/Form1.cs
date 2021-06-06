using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tao.OpenGl;
using Tao.FreeGlut;
using Tao.Platform.Windows;

namespace Snake29
{
    public partial class Form1 : Form
    {
        Point lastPoint;


        const double left_b = -0.999;
        const double down_b = -0.995;
        const double right_b = 0.993;
        const double upper_b = 0.999;

        bool first_draw;
        Field field;
        Point f_size;
        PointF cell_size;
        //----------------textures--------------------
        public int fruitTextureID;
        public uint fruitTexture;


        //----------------game params--------------------
        int fruits_count = 5;
        enum _move_param
        {
            up,
            down,
            left,
            right,
            stop
        }


        Point head;
        Point perv_head;
        PointF head_size;
        List<Point> tail;
        PointF tail_size;
        PointF draw_position;
        _move_param move_param;
        _move_param prev_move;
        bool gameOver;

        List<Point> fruits;
        //------------------------------------------------

        List<Line> vert_lines;
        List<Line> hor_lines;
        public Form1()
        {
            InitializeComponent();
            SGC.InitializeContexts();

            fruitTexture = 0;
            fruitTextureID = 0;

            vert_lines = new List<Line>();
            hor_lines = new List<Line>();
            field = new Field(new PointF((float)right_b, (float)down_b), new PointF((float)left_b, (float)upper_b));
            f_size = new Point(15, 15);
            draw_position = new PointF(0, 0);
            first_draw = true;
            determine_cell_size();
            create_lines();
            //draw_lines();


        }
        private void draw_lines()
        {
            Gl.glViewport(0, 0, SGC.Width, SGC.Height);
            Gl.glLineWidth(2f);
            Gl.glColor3f(0.9373f, 0.8f, 0.5647f);

            for (int i = 0; i < vert_lines.Count; i++)
            {
                Gl.glBegin(Gl.GL_LINE_STRIP);
                Gl.glVertex2d(vert_lines[i].p1.X, vert_lines[i].p1.Y);
                Gl.glVertex2d(vert_lines[i].p2.X, vert_lines[i].p2.Y);
                Gl.glEnd();
            }

            for (int i = 0; i < hor_lines.Count; i++)
            {
                Gl.glBegin(Gl.GL_LINE_STRIP);
                Gl.glVertex2d(hor_lines[i].p1.X, hor_lines[i].p1.Y);
                Gl.glVertex2d(hor_lines[i].p2.X, hor_lines[i].p2.Y);
                Gl.glEnd();
            }

            Gl.glViewport(0, 0, SGC.Width, SGC.Height);
            Gl.glLineWidth(2f);
            Gl.glColor3f(1.0f, 0, 0);
            Gl.glBegin(Gl.GL_LINE_STRIP);


            Gl.glVertex2d(left_b, upper_b);
            Gl.glVertex2d(right_b, upper_b);
            Gl.glVertex2d(right_b, down_b);
            Gl.glVertex2d(left_b, down_b);
            Gl.glVertex2d(left_b, upper_b);
            Gl.glEnd();
        }
        private void create_lines()
        {
            float x_pos;
            for (int i = 0; i < f_size.X - 1; i++)
            {
                x_pos = field.l_up.X + cell_size.X + i * cell_size.X;
                vert_lines.Add(new Line(new PointF(x_pos, field.l_up.Y), new PointF(x_pos, field.r_down.Y)));
            }

            float y_pos;
            for (int i = 0; i < f_size.Y; i++)
            {
                y_pos = field.r_down.Y + cell_size.Y + i * cell_size.Y;
                hor_lines.Add(new Line(new PointF(field.l_up.X, y_pos), new PointF(field.r_down.X, y_pos)));
            }
        }
        private void determine_cell_size()
        {
            float width = field.r_down.X - field.l_up.X;
            float height = field.l_up.Y - field.r_down.Y;

            cell_size.X = width / f_size.X;
            cell_size.Y = height / f_size.Y;

            cx.Text += Convert.ToString(Math.Round(cell_size.X, 3));
            cy.Text += Convert.ToString(Math.Round(cell_size.Y, 3));
        }
        private void Ex_but_MouseHover(object sender, EventArgs e)
        {
            Ex_but.ForeColor = Color.Red;
        }
        private void Ex_but_MouseLeave(object sender, EventArgs e)
        {
            Ex_but.ForeColor = Color.Black;
        }
        private void Ex_but_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void upper_Panel_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = e.Location;
        }
        private void upper_Panel_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;
            }
        }
        private void Start_but_Click(object sender, EventArgs e)
        {
            start_init_game();

            t1.Interval = 100;
            t1.Enabled = true;

            textBox1.Enabled = false;
            if (textBox1.Text != "")
            {
                fruits_count = Convert.ToInt32(textBox1.Text);
            }

            label2.Text = "t1 enabled";
            Stop_but.Enabled = true;
            Stop_but.Visible = true;
            Start_but.Visible = false;
            Start_but.Enabled = false;

        }
        private void Stop_but_Click(object sender, EventArgs e)
        {
            t1.Enabled = false;
            label2.Text = "t1 disabled";

            textBox1.Enabled = true;

            Start_but.Enabled = true;
            Start_but.Visible = true;
            Stop_but.Visible = false;
            Stop_but.Enabled = false;


        }
        private void Form1_Load(object sender, EventArgs e)
        {
            // инициализация библиотеки glut 
            Glut.glutInit();
            // инициализация режима экрана 
            Glut.glutInitDisplayMode(Glut.GLUT_RGB | Glut.GLUT_DOUBLE);

            // установка цвета очистки экрана (RGBA) 
            Gl.glClearColor(0, 0, 0, 1);

            // установка порта вывода 

            Gl.glViewport(0, 0, SGC.Width, SGC.Height);
            Gl.glLineWidth(2f);
            Gl.glColor3f(1.0f, 0, 0);
            Gl.glBegin(Gl.GL_LINE_STRIP);


            Gl.glVertex2d(left_b, upper_b);
            Gl.glVertex2d(right_b, upper_b);
            Gl.glVertex2d(right_b, down_b);
            Gl.glVertex2d(left_b, down_b);
            Gl.glVertex2d(left_b, upper_b);
            Gl.glEnd();

        }
        private void SGC_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W)
            {
                if (tail.Count > 0)
                {
                    if (move_param != _move_param.down)
                    {

                        prev_move = move_param;
                        move_param = _move_param.up;
                        return;
                    }

                }
                else
                {
                    prev_move = move_param;
                    move_param = _move_param.up;
                    return;
                }
            }
            if (e.KeyCode == Keys.A)
            {

                if (tail.Count > 0)
                {
                    if (move_param != _move_param.right)
                    {
                        prev_move = move_param;
                        move_param = _move_param.left;
                        return;
                    }

                }
                else
                {
                    prev_move = move_param;
                    move_param = _move_param.left;
                    return;
                }

            }
            if (e.KeyCode == Keys.S)
            {
                if (tail.Count > 0)
                {
                    if (move_param != _move_param.up)
                    {
                        prev_move = move_param;
                        move_param = _move_param.down;
                        return;
                    }

                }
                else
                {
                    prev_move = move_param;
                    move_param = _move_param.down;
                    return;
                }

            }
            if (e.KeyCode == Keys.D)
            {
                if (tail.Count > 0)
                {
                    if (move_param != _move_param.left)
                    {
                        prev_move = move_param;
                        move_param = _move_param.right;
                        return;
                    }

                }
                else
                {
                    prev_move = move_param;
                    move_param = _move_param.right;
                    return;
                }

            }

            if (e.KeyCode == Keys.P)
            {
                move_param = _move_param.stop;
                prev_move = move_param;
            }

        }
        //Начало игры
        private void t1_Tick(object sender, EventArgs e)
        {
            if (move_param != _move_param.stop)
            {
                if (gameOver == false)
                {
                    Draw();
                }
                Logic();
            }
        }
        private void start_init_game()
        {
            gameOver = false;
            move_param = _move_param.stop;


            head = new Point(f_size.X / 2, f_size.Y / 2);
            head_size = new PointF(cell_size.X * 4 / 5, cell_size.Y * 4 / 5);
            tail_size = new PointF(cell_size.X * 2 / 5, cell_size.Y * 2 / 5);

            tail = new List<Point>();
            //tail.Add(new Point(head.X - 1, head.Y));
            //tail.Add(new Point(head.X - 2, head.Y));
            //tail.Add(new Point(head.X - 3, head.Y));
            Gl.glRotated(80, 1, 0, 0);

            fruits = new List<Point>();
            Draw();
            first_draw = false;

        }
        private void Logic()
        {
            int indx = -1;

            if (fruits.Count == 0)
            {
                generate_new_fruits();
            }
            else
            {
                if (fruits.Count < fruits_count)
                {
                    generate_fruit();
                }
            }

            move();

            for (int i = 0; i < fruits.Count; i++)
            {
                if (head == fruits[i])
                {
                    indx = i;
                    add_tail_el();
                    break;
                }
            }
            remove_fruit(indx);

            if (is_intersect() == true)
            {
                gameOver = true;
            }


            if ((head.X > f_size.X - 1) || (head.X < 0))
            {
                gameOver = true;
            }

            if ((head.Y > f_size.Y - 1) || (head.Y < 0))
            {
                gameOver = true;
            }

            //условие должно быть внизу
            if (gameOver == true)
            {
                t1.Enabled = false;
            }
        }
        private bool is_intersect()
        {
            for (int i = 0; i < tail.Count; i++)
            {
                if (head == tail[i])
                {
                    return true;
                }
            }
            return false;
        }
        private void generate_new_fruits()
        {
            for (int i = 0; i < fruits_count; i++)
            {
                generate_fruit();
            }
        }
        private void generate_fruit()
        {
            Point tmp;
            Random r;
            r = new Random();
            bool check = false;

            tmp = new Point(r.Next(0, f_size.X), r.Next(0, f_size.Y));
            while (check != true)
            {
                check = true;

                if (tmp == head)
                {
                    check = false;
                }
                else
                {
                    for (int j = 0; j < tail.Count; j++)
                    {
                        if (tmp == tail[j])
                        {
                            check = false;
                            break;
                        }
                    }

                    for (int j = 0; j < fruits.Count; j++)
                    {
                        if (tmp == fruits[j])
                        {
                            check = false;
                            break;
                        }
                    }

                }

                if (check == false)
                {
                    tmp = new Point(r.Next(0, f_size.X), r.Next(0, f_size.Y));
                }

            }

            fruits.Add(tmp);

        }
        private void add_tail_el()
        {
            if (tail.Count != 0)
            {
                tail.Add(new Point(tail[tail.Count - 1].X, tail[tail.Count - 1].Y));
            }
            else
            {
                tail.Add(perv_head);
            }
        }
        private void remove_fruit(int indx)
        {
            if (indx != -1)
            {
                List<Point> new_fruits = new List<Point>();
                for (int i = 0; i < fruits.Count; i++)
                {
                    if (i != indx)
                    {
                        new_fruits.Add(fruits[i]);
                    }
                }
                fruits = new_fruits;
            }
        }
        private void _move(Point new_head)
        {
            if (tail.Count != 0)
            {
                if (new_head != tail[0])
                {
                    if (tail.Count > 1)
                    {
                        List<Point> new_tail = new List<Point>();
                        new_tail.Add(head);
                        //perv_head = head;
                        head = new_head;
                        for (int i = 1; i < tail.Count; i++)
                        {
                            new_tail.Add(tail[i - 1]);
                        }

                        tail = new_tail;
                    }
                    else
                    {
                        tail = new List<Point>();
                        tail.Add(head);
                        perv_head = head;
                        head = new_head;
                    }
                }
                else
                {
                    move_param = prev_move;
                    move();
                }
            }
            else
            {
                perv_head = head;
                head = new_head;
            }

        }
        private void move()
        {
            switch (move_param)
            {

                case _move_param.right:
                    _move(new Point(head.X + 1, head.Y));
                    break;
                case _move_param.left:
                    _move(new Point(head.X - 1, head.Y));
                    break;
                case _move_param.up:
                    _move(new Point(head.X, head.Y - 1));
                    break;
                case _move_param.down:
                    _move(new Point(head.X, head.Y + 1));
                    break;
                case _move_param.stop:
                    break;



            }

        }
        private void Draw()
        {
            Gl.glClearColor(0, 0, 0, 1);
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);
            dfield();

            //draw_lines();
            //dhead();
            //dtail();
            //dfruits();
            SGC.Refresh();
        }
        private void dfield()
        {
            set_to_begin();
            label4.Text = "pp:\t" + Convert.ToString(cell_size.X * f_size.X);
            if (!first_draw)
            {
                Gl.glTranslated(-0.9272, 0.9311, 0);
            }/**/

            for (int i = 0; i < f_size.Y; i++)
            {
                if (i > 0)
                {
                    translated(-cell_size.X * f_size.X, -cell_size.Y);
                }
                for (int j = 0; j < f_size.X; j++)
                {
                    Glut.glutWireCube(cell_size.X);//Отрисовка Ячейки поля
                    
                    if (is_fruit_intersect(j, i))
                    {
                        //нада поменять цвет
                        drawFruit();
                    }
                    //отрисовка Гадюки
                    Gl.glColor3d(1, 1, 0);
                    if ((head.X == j) && (head.Y == i))
                    {
                        drawHead();
                    }
                    int tID = is_tail_intersect(j, i);
                    if (tID != -1)
                    {
                        if (tID < tail.Count - 3)
                        {
                            drawTail(cell_size.X * 11 / 24);
                        }
                        else
                        {
                            if (tID == tail.Count - 3)
                            {
                                drawTail(cell_size.X * 9 / 24);
                            }
                            if (tID == tail.Count - 2)
                            {
                                drawTail(cell_size.X * 7 / 24);
                            }
                            if (tID == tail.Count - 1)
                            {
                                drawTail(cell_size.X * 5 / 24);
                            }
                        }

                    }
                    Gl.glColor3d(1, 0, 0);
                    translated(cell_size.X);

                }
            }
        }
        private void drawTail(double tail_size)
        {
            Gl.glTranslated(0, 0, -cell_size.X);
            Glut.glutWireSphere(tail_size, 10, 10);
            Gl.glTranslated(0, 0, +cell_size.X);
        }
        private bool is_fruit_intersect(int x, int y)
        {
            for (int i = 0; i < fruits.Count; i++)
            {
                if ((fruits[i].X == x) && (fruits[i].Y == y))
                {
                    return true;
                }
            }
            return false;
        }
        private void drawFruit()
        {
            Gl.glColor3d(0, 1, 0);
            Gl.glTranslated(0, 0, -cell_size.X);
            Glut.glutWireSphere(cell_size.X * 5 / 12, 10, 10);
            Gl.glTranslated(0, 0, cell_size.X);
            Gl.glColor3d(1, 0, 0);
        }
        private void drawHead()
        {
            Gl.glTranslated(0, 0, -cell_size.X);
            Glut.glutWireCube(cell_size.X * 9 / 12);
            
            switch (move_param)
            {
                case _move_param.up:
                    Gl.glTranslated(cell_size.X / 4, cell_size.X*11/24, 0);
                    Glut.glutWireCube(cell_size.X / 6);
                    Gl.glTranslated(-cell_size.X / 2, 0, 0);
                    Glut.glutWireCube(cell_size.X / 6);
                    Gl.glTranslated(cell_size.X / 4, -cell_size.X * 11/ 24, 0);
                    break;
                case _move_param.down:
                    Gl.glTranslated(cell_size.X / 4, -cell_size.X * 11 / 24, 0);
                    Glut.glutWireCube(cell_size.X / 6);
                    Gl.glTranslated(-cell_size.X / 2, 0, 0);
                    Glut.glutWireCube(cell_size.X / 6);
                    Gl.glTranslated(cell_size.X / 4, cell_size.X * 11 / 24, 0);
                    break;
                case _move_param.left:
                    Gl.glTranslated(-cell_size.X * 11 / 24, cell_size.X / 4, 0);
                    Glut.glutWireCube(cell_size.X / 6);
                    Gl.glTranslated(0, -cell_size.X / 2, 0);
                    Glut.glutWireCube(cell_size.X / 6);
                    Gl.glTranslated(cell_size.X * 11 / 24, cell_size.X / 4, 0);
                    break;
                case _move_param.right:
                    Gl.glTranslated(cell_size.X * 11 / 24, cell_size.X / 4, 0);
                    Glut.glutWireCube(cell_size.X / 6);
                    Gl.glTranslated(0, -cell_size.X / 2, 0);
                    Glut.glutWireCube(cell_size.X / 6);
                    Gl.glTranslated(-cell_size.X * 11 / 24, cell_size.X / 4, 0);
                    break;
            }
            Gl.glTranslated(0, 0, cell_size.X);
        }
        private int is_tail_intersect(int x, int y)
        {
            for (int i = 0; i < tail.Count; i++)
            {
                if ((tail[i].X == x) && (tail[i].Y == y))
                {
                    return i;
                }
            }
            return -1;
        }
        private void set_to_begin()//установить отрисовку в начало поля;
        {
            translated(-draw_position.X - 0.932f, -draw_position.Y + 0.93f);
        }
        private void translated(float x = 0, float y = 0)
        {
            Gl.glTranslated(x, y, 0);
            draw_position = new PointF(x, y);
        }
        #region 2DDraw
        /* private void dhead()
         {
             Gl.glViewport(0, 0, SGC.Width, SGC.Height);
             Gl.glLineWidth(2f);
             Gl.glColor3f(0.9373f, 0.8f, 0.5647f);

             float x_pos = field.l_up.X + head.X * cell_size.X;
             float y_pos = field.r_down.Y + cell_size.Y + head.Y * cell_size.Y;

             PointF l_up = new PointF(x_pos, y_pos);
             PointF r_d = new PointF(l_up.X + cell_size.X, l_up.Y - cell_size.Y);

             Gl.glBegin(Gl.GL_LINE_STRIP);

             Gl.glVertex2d(l_up.X, l_up.Y - cell_size.Y / 2);
             Gl.glVertex2d(l_up.X + cell_size.X / 2, l_up.Y);
             Gl.glVertex2d(l_up.X + cell_size.X, l_up.Y - cell_size.Y / 2);
             Gl.glVertex2d(l_up.X + cell_size.X / 2, l_up.Y - cell_size.Y);
             Gl.glVertex2d(l_up.X, l_up.Y - cell_size.Y / 2);
             Gl.glEnd();

             double begin = 30;//начальная координата глаз в ячейке
             double end = 50;//конечная координата в ячейке (begin - end = длина глаз)
             double den = 100;//модификатор begin < den && end < den

             switch (move_param)
             {
                 case _move_param.up:
                     Gl.glBegin(Gl.GL_LINE_STRIP);
                     Gl.glVertex2d(l_up.X + cell_size.X / 4, l_up.Y - cell_size.Y * begin / den);
                     Gl.glVertex2d(l_up.X + cell_size.X / 4, l_up.Y - cell_size.Y * end / den);
                     Gl.glEnd();
                     Gl.glBegin(Gl.GL_LINE_STRIP);
                     Gl.glVertex2d(r_d.X - cell_size.X / 4, l_up.Y - cell_size.Y * begin / den);
                     Gl.glVertex2d(r_d.X - cell_size.X / 4, l_up.Y - cell_size.Y * end / den);
                     Gl.glEnd();
                     break;
                 case _move_param.down:
                     Gl.glBegin(Gl.GL_LINE_STRIP);
                     Gl.glVertex2d(l_up.X + cell_size.X / 4, r_d.Y + cell_size.Y * begin / den);
                     Gl.glVertex2d(l_up.X + cell_size.X / 4, r_d.Y + cell_size.Y * end / den);
                     Gl.glEnd();
                     Gl.glBegin(Gl.GL_LINE_STRIP);
                     Gl.glVertex2d(r_d.X - cell_size.X / 4, r_d.Y + cell_size.Y * begin / den);
                     Gl.glVertex2d(r_d.X - cell_size.X / 4, r_d.Y + cell_size.Y * end / den);
                     Gl.glEnd();
                     break;
                 case _move_param.left:
                     Gl.glBegin(Gl.GL_LINE_STRIP);
                     Gl.glVertex2d(l_up.X + cell_size.X * begin / den, l_up.Y - cell_size.Y / 4);
                     Gl.glVertex2d(l_up.X + cell_size.X * end / den, l_up.Y - cell_size.Y / 4);
                     Gl.glEnd();
                     Gl.glBegin(Gl.GL_LINE_STRIP);
                     Gl.glVertex2d(l_up.X + cell_size.X * begin / den, r_d.Y + cell_size.Y / 4);
                     Gl.glVertex2d(l_up.X + cell_size.X * end / den, r_d.Y + cell_size.Y / 4);
                     Gl.glEnd();
                     break;
                 case _move_param.right:
                     Gl.glBegin(Gl.GL_LINE_STRIP);
                     Gl.glVertex2d(r_d.X - cell_size.X * begin / den, l_up.Y - cell_size.Y / 4);
                     Gl.glVertex2d(r_d.X - cell_size.X * end / den, l_up.Y - cell_size.Y / 4);
                     Gl.glEnd();
                     Gl.glBegin(Gl.GL_LINE_STRIP);
                     Gl.glVertex2d(r_d.X - cell_size.X * begin / den, r_d.Y + cell_size.Y / 4);
                     Gl.glVertex2d(r_d.X - cell_size.X * end / den, r_d.Y + cell_size.Y / 4);
                     Gl.glEnd();
                     break;
                 case _move_param.stop:
                     break;
                 default:
                     break;
             }

         }
         private void dtail()
         {
             Gl.glViewport(0, 0, SGC.Width, SGC.Height);
             Gl.glLineWidth(2f);
             Gl.glColor3f(0.9373f, 0.8f, 0.5647f);

             for (int i = 0; i < tail.Count - 1; i++)
             {
                 float x_pos = field.l_up.X + tail[i].X * cell_size.X;
                 float y_pos = field.r_down.Y + cell_size.Y + tail[i].Y * cell_size.Y;

                 float free_x = (cell_size.X - tail_size.X) / 2;
                 float free_y = (cell_size.Y - tail_size.Y) / 2;

                 PointF l_up = new PointF(x_pos + free_x, y_pos - free_y);
                 PointF r_d = new PointF(l_up.X + tail_size.X, l_up.Y - tail_size.Y);

                 Gl.glBegin(Gl.GL_LINE_STRIP);

                 Gl.glVertex2d(l_up.X, l_up.Y);
                 Gl.glVertex2d(r_d.X, l_up.Y);
                 Gl.glVertex2d(r_d.X, r_d.Y);
                 Gl.glVertex2d(l_up.X, r_d.Y);
                 Gl.glVertex2d(l_up.X, l_up.Y);

                 Gl.glEnd();
             }

             if (tail.Count > 1)
             {
                 float x_pos = field.l_up.X + tail[tail.Count - 1].X * cell_size.X;
                 float y_pos = field.r_down.Y + cell_size.Y + tail[tail.Count - 1].Y * cell_size.Y;

                 float free_x = (cell_size.X - tail_size.X) / 2;
                 float free_y = (cell_size.Y - tail_size.Y) / 2;

                 PointF l_up = new PointF(x_pos + free_x, y_pos - free_y);
                 PointF r_d = new PointF(l_up.X + tail_size.X, l_up.Y - tail_size.Y);

                 if ((tail[tail.Count - 1].X + 1) == tail[tail.Count - 2].X) // right mooving
                 {
                     Gl.glBegin(Gl.GL_LINE_STRIP);
                     Gl.glVertex2d(l_up.X, l_up.Y - (l_up.Y - r_d.Y) / 2);
                     Gl.glVertex2d(r_d.X, l_up.Y);
                     Gl.glVertex2d(r_d.X, r_d.Y);
                     Gl.glVertex2d(l_up.X, l_up.Y - (l_up.Y - r_d.Y) / 2);
                     Gl.glEnd();

                 }
                 if ((tail[tail.Count - 1].X - 1) == tail[tail.Count - 2].X)//left mooving
                 {
                     Gl.glBegin(Gl.GL_LINE_STRIP);
                     Gl.glVertex2d(r_d.X, l_up.Y - (l_up.Y - r_d.Y) / 2);
                     Gl.glVertex2d(l_up.X, r_d.Y);
                     Gl.glVertex2d(l_up.X, l_up.Y);
                     Gl.glVertex2d(r_d.X, l_up.Y - (l_up.Y - r_d.Y) / 2);
                     Gl.glEnd();
                 }
                 if ((tail[tail.Count - 1].Y + 1) == tail[tail.Count - 2].Y)//up mooving
                 {
                     Gl.glBegin(Gl.GL_LINE_STRIP);
                     Gl.glVertex2d(r_d.X - (r_d.X - l_up.X) / 2, r_d.Y);
                     Gl.glVertex2d(l_up.X, l_up.Y);
                     Gl.glVertex2d(r_d.X, l_up.Y);
                     Gl.glVertex2d(r_d.X - (r_d.X - l_up.X) / 2, r_d.Y);
                     Gl.glEnd();

                 }
                 if ((tail[tail.Count - 1].Y - 1) == tail[tail.Count - 2].Y)//down mooving
                 {
                     Gl.glBegin(Gl.GL_LINE_STRIP);
                     Gl.glVertex2d(r_d.X - (r_d.X - l_up.X) / 2, l_up.Y);
                     Gl.glVertex2d(l_up.X, r_d.Y);
                     Gl.glVertex2d(r_d.X, r_d.Y);
                     Gl.glVertex2d(r_d.X - (r_d.X - l_up.X) / 2, l_up.Y);
                     Gl.glEnd();
                 }
             }
             else
             {
                 if (tail.Count == 1)
                 {

                     float x_pos = field.l_up.X + tail[0].X * cell_size.X;
                     float y_pos = field.r_down.Y + cell_size.Y + tail[0].Y * cell_size.Y;

                     float free_x = (cell_size.X - tail_size.X) / 2;
                     float free_y = (cell_size.Y - tail_size.Y) / 2;

                     PointF l_up = new PointF(x_pos + free_x, y_pos - free_y);
                     PointF r_d = new PointF(l_up.X + tail_size.X, l_up.Y - tail_size.Y);

                     if ((tail[tail.Count - 1].X + 1) == head.X) // right mooving
                     {
                         Gl.glBegin(Gl.GL_LINE_STRIP);
                         Gl.glVertex2d(l_up.X, l_up.Y - (l_up.Y - r_d.Y) / 2);
                         Gl.glVertex2d(r_d.X, l_up.Y);
                         Gl.glVertex2d(r_d.X, r_d.Y);
                         Gl.glVertex2d(l_up.X, l_up.Y - (l_up.Y - r_d.Y) / 2);
                         Gl.glEnd();

                     }
                     if ((tail[tail.Count - 1].X - 1) == head.X)//left mooving
                     {
                         Gl.glBegin(Gl.GL_LINE_STRIP);
                         Gl.glVertex2d(r_d.X, l_up.Y - (l_up.Y - r_d.Y) / 2);
                         Gl.glVertex2d(l_up.X, r_d.Y);
                         Gl.glVertex2d(l_up.X, l_up.Y);
                         Gl.glVertex2d(r_d.X, l_up.Y - (l_up.Y - r_d.Y) / 2);
                         Gl.glEnd();
                     }
                     if ((tail[tail.Count - 1].Y + 1) == head.Y)//up mooving
                     {
                         Gl.glBegin(Gl.GL_LINE_STRIP);
                         Gl.glVertex2d(r_d.X - (r_d.X - l_up.X) / 2, r_d.Y);
                         Gl.glVertex2d(l_up.X, l_up.Y);
                         Gl.glVertex2d(r_d.X, l_up.Y);
                         Gl.glVertex2d(r_d.X - (r_d.X - l_up.X) / 2, r_d.Y);
                         Gl.glEnd();

                     }
                     if ((tail[tail.Count - 1].Y - 1) == head.Y)//down mooving
                     {
                         Gl.glBegin(Gl.GL_LINE_STRIP);
                         Gl.glVertex2d(r_d.X - (r_d.X - l_up.X) / 2, l_up.Y);
                         Gl.glVertex2d(l_up.X, r_d.Y);
                         Gl.glVertex2d(r_d.X, r_d.Y);
                         Gl.glVertex2d(r_d.X - (r_d.X - l_up.X) / 2, l_up.Y);
                         Gl.glEnd();
                     }
                 }
             }

         }
         private void dfruits()
         {
             Gl.glViewport(0, 0, SGC.Width, SGC.Height);
             Gl.glLineWidth(2f);
             Gl.glColor3f(0.9373f, 0.8f, 0.5647f);

             for (int i = 0; i < fruits.Count; i++)
             {
                 float x_pos = field.l_up.X + fruits[i].X * cell_size.X;
                 float y_pos = field.r_down.Y + cell_size.Y + fruits[i].Y * cell_size.Y;
                 float free_x = (cell_size.X - tail_size.X) / 2;
                 float free_y = (cell_size.Y - tail_size.Y) / 2;

                 PointF l_up = new PointF(x_pos + free_x, y_pos - free_y);
                 PointF r_d = new PointF(l_up.X + tail_size.X, l_up.Y - tail_size.Y);

                 Gl.glBegin(Gl.GL_LINE_STRIP);
                 Gl.glVertex2d(l_up.X, l_up.Y - (l_up.Y - r_d.Y) / 2);
                 Gl.glVertex2d(l_up.X + (r_d.X - l_up.X) / 2, l_up.Y);
                 Gl.glVertex2d(r_d.X, l_up.Y - (l_up.Y - r_d.Y) / 2);
                 Gl.glVertex2d(l_up.X + (r_d.X - l_up.X) / 2, r_d.Y);
                 Gl.glVertex2d(l_up.X, l_up.Y - (l_up.Y - r_d.Y) / 2);
                 Gl.glEnd();

             }
         }/**/
        #endregion
    }
    class Line
    {

        public Line(PointF _p1, PointF _p2)
        {
            p1 = _p1;
            p2 = _p2;
        }

        public PointF p1;
        public PointF p2;
    }
    class Field
    {

        public Field()
        {

        }

        public Field(PointF rd, PointF lup)
        {
            r_down = rd;
            l_up = lup;
        }

        public PointF r_down;
        public PointF l_up;
    }
}
