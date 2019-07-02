using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace _2048
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        static PictureBox[,] pbArray = new PictureBox[4, 4];


        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Image bg = Resource.bg1;

            foreach (PictureBox pb in panel1.Controls)
            {
                pb.Image = bg;
                pb.SizeMode = PictureBoxSizeMode.StretchImage;
                
                pbArray[pb.Location.X / 100, pb.Location.Y / 100] = pb;

            }


            
            
        
        }


        public bool achieve ;
        public void pipei(PictureBox p1,PictureBox p2)          
        {

            Label lab1 = p1.Controls.OfType<Label>().ElementAt<Label>(0);
            Label lab2 = p2.Controls.OfType<Label>().ElementAt<Label>(0);


            int i =Convert.ToInt32(lab1.Text);
            int j =Convert.ToInt32(lab2.Text);
            if (i==j)
            {
                j += i;
                p1.Controls.Remove(lab1);
                lab2.Text = j.ToString();
                achieve = true;

            }
           
          
           
           
        
        
        }


        public void shengcheng()
        {

            Random local = new Random();
            int x = local.Next(0, 4);
            int y = local.Next(0, 4);

            int text = local.Next() % 3;

            while (pbArray[x, y].Controls.OfType<Label>().Any())
            {
                x = local.Next(0, 4);
                y = local.Next(0, 4);
            }


            string a = x.ToString() + y.ToString();
            label2.Text = label2.Text + a;



            Label l = new Label();
            l.BackColor = Color.Red;
            l.Size = new System.Drawing.Size(100, 100);
            l.TextAlign = ContentAlignment.MiddleCenter;
            if (text<=1)
            {
                l.Text = "2";
            }
            else
            {
                l.Text = "4";
            }
            
            l.Font = new System.Drawing.Font("宋体", 25, FontStyle.Bold);
            l.Parent = pbArray[x, y];





        }







       

        public void move(PictureBox pb, int dirction, int x,int y) 
        {


            Label lab = pb.Controls.OfType<Label>().ElementAt<Label>(0);
            switch (dirction)
            {

                case 1:



                    lab.Parent = pbArray[x-1, y ];    
                    
                    while((x-1)!=0&&!pbArray[x-2,y].Controls.OfType<Label>().Any())
                    {
                        x = x - 1;
                        lab.Parent = pbArray[x - 1, y];
                    }
                    if ((x - 2) >= 0)
                    {
                        pipei(pbArray[x-1, y], pbArray[x-2, y]);
                    }
                  
                   
                    break;
                case 2:
                    lab.Parent = pbArray[x+1, y ];

                    while ((x + 1) != 3 && !pbArray[x + 2, y].Controls.OfType<Label>().Any())
                    {
                        x = x + 1;
                        lab.Parent = pbArray[x + 1, y];
                    }
                    if ((x + 2) <= 3)
                    {
                        pipei(pbArray[x + 1, y], pbArray[x + 2, y]);
                    }
                    break;
                case 3:

                    lab.Parent = pbArray[x, y - 1];                  
                    while ((y - 1) != 0 && !pbArray[x , y-2].Controls.OfType<Label>().Any())
                    {
                        y = y - 1;
                        lab.Parent = pbArray[x , y-1];
                    } 
                  
                   
                    if ((y-2)>=0) 
                    {
                        pipei(pbArray[x, y-1], pbArray[x, y - 2]);
                    }
                    
                    break;
                case 4:
                    lab.Parent = pbArray[x, y +1];

                    while ((y + 1) != 3 && !pbArray[x, y + 2].Controls.OfType<Label>().Any())
                    {
                        y = y + 1;
                        lab.Parent = pbArray[x, y + 1];
                    }

                    if ((y + 2) <= 3)
                    {
                        pipei(pbArray[x, y +1], pbArray[x, y + 2]);
                    }
                    break;
           
            }
                                   


        }




        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {


            achieve = false;

            switch (e.KeyCode)
            {
                

                case Keys.Up:

                    for (int x = 0; x < 4; x++)
                    {
                        for (int y= 1; y < 4; y++)
                        {
                           

                            if (pbArray[x, y].Controls.OfType<Label>().Any()&&!pbArray[x, y - 1].Controls.OfType<Label>().Any())
                                
                            {

                                move(pbArray[x, y], 3, x, y);
                                achieve = true;
                              
                                
                            }
                            if (pbArray[x, y - 1].Controls.OfType<Label>().Any() && pbArray[x, y ].Controls.OfType<Label>().Any())//上面有,而且自己也要有
                              
                           {
                               pipei(pbArray[x, y], pbArray[x, y - 1]);
                             
                              
                           }

                        }


                    }


                    if (achieve)
                    {
                        shengcheng();
                    }
                    
                    break;
                case Keys.Down:
                    for (int x = 0; x < 4; x++) 
                    {
                        for (int y = 2; y >-1; y--)
                        {
                            if (pbArray[x, y].Controls.OfType<Label>().Any() &&! pbArray[x, y +1].Controls.OfType<Label>().Any())
                            
                            {

                                move(pbArray[x, y], 4, x, y);
                                achieve = true;
                               
                                
                            }
                            if (pbArray[x, y + 1].Controls.OfType<Label>().Any()&&pbArray[x, y ].Controls.OfType<Label>().Any())//下面有
                            {
                                pipei(pbArray[x, y], pbArray[x, y + 1]);
                              
                                
                            }


                        }


                    }

                    if (achieve)
                    {
                        shengcheng();
                    }
                    
                    break;
                case Keys.Right:
                    for (int x = 2; x >-1; x--) 
                    {
                        for (int y = 0; y <4; y++) 
                        {
                            if (pbArray[x, y].Controls.OfType<Label>().Any() && !pbArray[x+1, y ].Controls.OfType<Label>().Any())
                            
                            {

                                move(pbArray[x, y], 2, x, y);
                                achieve = true;
                                

                                

                            }
                            if (pbArray[x + 1, y].Controls.OfType<Label>().Any() && pbArray[x, y].Controls.OfType<Label>().Any())//右面有
                            {
                                pipei(pbArray[x, y], pbArray[x + 1, y]);
                               
                               
                            }

                        }


                    }

                    if (achieve)
                    {
                        shengcheng();
                    }
                    
                    break;
                case Keys.Left:
                    
                    for (int x = 1; x <4; x++)  
                    {
                        for (int y = 0; y < 4; y++)
                        {
                            if (pbArray[x, y].Controls.OfType<Label>().Any() && !pbArray[x-1, y ].Controls.OfType<Label>().Any())
                       
                            {

                                move(pbArray[x, y], 1, x, y);
                                achieve = true;
                               
                                
                            }
                            if (pbArray[x - 1, y].Controls.OfType<Label>().Any() && pbArray[x, y].Controls.OfType<Label>().Any())//左面有
                            {
                                pipei(pbArray[x, y], pbArray[x - 1, y]);
                               
            
                            }

                        }


                    }

                    if (achieve)
                    {
                        shengcheng();
                    }
                    
                    
                    break;
                default: break;

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            shengcheng();
            shengcheng();
            this.button1.Enabled = false;
        }
    }
}

