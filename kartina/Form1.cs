using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace kartina
{
        public partial class Form1 : Form
        {
            int x1, x2; // координаты ежиков
            bool ezh1 = false; // направление движения 1 ежика: вправо или влево
            bool increment1 = false; // пересчет координат 1 ежика: вправо или влево
            bool ezh2 = false; // направление движения 2 ежика: вправо или влево
            bool increment2 = false; // пересчет координат 2 ежика: вправо или влево
            int angle;
            Thread t1; // ежик1
            Thread t2; // ежик2
            Thread t3; // солнце
            Thread t4; // музыка

            public Form1()
        {
            InitializeComponent();
        }
            private void Form1_Load(object sender, EventArgs e)
            {
                this.MaximumSize = new Size(500, 600);
                this.MinimumSize = new Size(500, 600);
                // цвет фона
                this.BackColor = Color.AliceBlue;
                // убираем мерцание при перерисовке
                this.DoubleBuffered = true;
                // подключаем событие Paint
                // this.Paint += new PaintEventHandler(Form1_Paint);
                // начальная координата 1 и 2 ежика
                x1 = -100;
                x2 = -100;
                // 1 ежик
                t1 = new Thread(new ThreadStart(Run1));
                t1.Start();
                // 2 ежик
                t2 = new Thread(new ThreadStart(Run2));
                t2.Start();
                // солнце
                t3 = new Thread(new ThreadStart(Run3));
                t3.Start();
                // музыка
                t4 = new Thread(new ThreadStart(Run4));
                t4.Start();
            }



        void Form1_Paint(object sender, PaintEventArgs e)
        {
            // фон
            Image fon = Image.FromFile(@"C:\\fon_ezh-sun.png");
            e.Graphics.DrawImage(fon, 0, 0, this.Width, this.Height);
            Image sun = Image.FromFile(@"C:\\sun_ezh.png");
            // ежик: влево илли вправо
            Image img1 = Image.FromFile(@"C:\\ezh2.png");
            Image img2 = Image.FromFile(@"C:\\ezh2.png");
            // в зависимости от направления движения ежик: влево илли вправо
            if (ezh1 == false)
                e.Graphics.DrawImage(img1, x1, 420, 100, 100);
            else
                e.Graphics.DrawImage(img2, x1, 420, 100, 100);
            // в зависимости от направления движения ежик: влево илли вправо
            if (ezh2 == false)
                e.Graphics.DrawImage(img1, x2, 420, 100, 100);
            else
                e.Graphics.DrawImage(img2, x2, 420, 100, 100);
            // движение солнца
            e.Graphics.TranslateTransform(370, 100);
            // поворот
            e.Graphics.RotateTransform(angle);
            e.Graphics.DrawImage(sun, -60, -60, 120, 120);
        }
               
            public void Run1()
                {
                    // в зависимости от направления движения пересчет координат
                    while (true)
                    {
                        // for (int i = 0; i < 140; i++)
                        // {
                        if (increment1)
                            x1 += 5;
                        else
                            x1 -= 5;
                        if (x1 > 350)
                        {
                            increment1 = false;
                            ezh1 = true;
                        }
                        if (x1 < 50)
                        {
                            increment1 = true;
                            ezh1 = false;
                        }
                        // перерисовка формы
                        this.Invalidate();
                        Thread.Sleep(20);
                    }
                     }
              
                public void Run2()
                {
                    // в зависимости от направления движения пересчет координат
                    while (true)
                    {
                        // for (int i = 0; i < 140; i++)
                        // {
                        if (increment2)
                            x2 += 5;
                        else
                            x2 -= 5;
                        if (x2 > 350)
                        {
                            increment2 = false;
                            ezh2 = true;
                        }
                        if (x2 < 50)
                        {
                            increment2 = true;
                            ezh2 = false;
                        }
                        // перерисовка формы
                        this.Invalidate();
                        Thread.Sleep(50); // другая скорость ежика
                    }
                }
                public void Run3()
                {
                    // пересчет угла поворота
                    while (true)
                    {
                        angle++;
                        this.Invalidate();
                        Thread.Sleep(20);
                    }
                }
                public void Run4()
                {
                    WMPLib.WindowsMediaPlayer wplayer = new WMPLib.WindowsMediaPlayer();
                    wplayer.URL = "C:\\Ezhik-rezinovy.mp3";
                    wplayer.settings.setMode("Loop", true);
                    wplayer.controls.play();
                } 
        private void button1_Click(object sender, EventArgs e)
    {
        WMPLib.WindowsMediaPlayer wplayer = new WMPLib.WindowsMediaPlayer();
        wplayer.controls.stop();
    }

    private void button2_Click(object sender, EventArgs e)
    {
        WMPLib.WindowsMediaPlayer wplayer = new WMPLib.WindowsMediaPlayer();
        wplayer.URL = "C:\\Ezhik-rezinovy.mp3";
        wplayer.settings.playCount = 0;
        wplayer.controls.play();
    }
    private void Form1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
    {
        button2.PerformClick();

    }
    private void Form1_PreviewKeyDown1(object sender, PreviewKeyDownEventArgs e)
    {
        button1.PerformClick();
    }
    private void Form1_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Up)
        {
            Form1_PreviewKeyDown(null, null);
            button2.PerformClick();

        }
    }

       

        private void Form1_KeyDown1(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Down)
        {
            Form1_PreviewKeyDown1(null, null);
            button1.PerformClick();
    }
   
        }
    }
}
    

       
