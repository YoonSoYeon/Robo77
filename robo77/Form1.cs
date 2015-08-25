using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace robo77
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            //이미지 불러오기 - 15개
            imgList = new List<Image>();
            for( int i = 0 ; i < 15 ; i++ )
            {
                String tmpName = String.Format( "Card_{0:D2}.png", i );
                Image tmpl = Image.FromFile( tmpName );
                imgList.Add( tmpl );
            }

            //카드 관리
            cGM = new CGameManager( imgList );

            InitializeComponent();
            tmpBack = Image.FromFile( "layout.png" );
            tmpChip = Image.FromFile( "chip.png" );
         

            bPressed = 0;
            cardIndex = -1;
        }
        //변수선언
        List<Image> imgList;

        CGameManager cGM;

        int bPressed;
        int cardIndex;
        Image tmpChip;
        Image tmpBack;
        TextBox tb_Name;

        //카드중심축
        int card0OffX;
        int card0OffY;

        int card1OffX;
        int card1OffY;

        private void Form1_Load( object sender, EventArgs e )
        {
            timer1.Start();
        }

        private void Form1_Paint( object sender, PaintEventArgs e )
        {
            e.Graphics.DrawImage( tmpBack, 0, 0, tmpBack.Size.Width, tmpBack.Size.Height );
            e.Graphics.DrawImage( tmpChip, 280, 180, tmpChip.Size.Width, tmpChip.Size.Height );
            e.Graphics.DrawImage( tmpChip, 280, 220, tmpChip.Size.Width, tmpChip.Size.Height );
            e.Graphics.DrawImage( tmpChip, 280, 260, tmpChip.Size.Width, tmpChip.Size.Height );

            e.Graphics.DrawImage( tmpChip, 280, 400, tmpChip.Size.Width, tmpChip.Size.Height );
            e.Graphics.DrawImage( tmpChip, 280, 440, tmpChip.Size.Width, tmpChip.Size.Height );
            e.Graphics.DrawImage( tmpChip, 280, 480, tmpChip.Size.Width, tmpChip.Size.Height );

            e.Graphics.DrawImage( tmpChip, 280, 610, tmpChip.Size.Width, tmpChip.Size.Height );
            e.Graphics.DrawImage( tmpChip, 280, 650, tmpChip.Size.Width, tmpChip.Size.Height );
            e.Graphics.DrawImage( tmpChip, 280, 690, tmpChip.Size.Width, tmpChip.Size.Height );

            DrawStringRectangleFFormat( e, "player1 윤소연", 90, 135, 150, 30, 14, Color.Gray );
            DrawStringRectangleFFormat( e, "player2 문예빈", 90, 355, 150, 30, 14, Color.Gray );
            DrawStringRectangleFFormat( e, "player3 김자은", 90, 570, 150, 30, 14, Color.Gray );
            DrawStringRectangleFFormat( e, "player2 차례", 380, 450, 200, 70, 20 , Color.White);

            foreach( int playerIndex in cGM.thePlayerList )
            {
                CRoboCard tmpRC = cGM.theRoboCards[ playerIndex ];
                e.Graphics.DrawImage( tmpRC.theImage, tmpRC.theX, tmpRC.theY, tmpRC.theSizeX, tmpRC.theSizeY );
            }
            foreach( int playerIndex in cGM.theEnemyList1 )
            {
                CRoboCard tmpRC = cGM.theRoboCards[ playerIndex ];
                e.Graphics.DrawImage( tmpRC.theImage, tmpRC.theX, tmpRC.theY, tmpRC.theSizeX, tmpRC.theSizeY );
            }
            foreach( int playerIndex in cGM.theDesktopCards )
            {
                CRoboCard tmpRC = cGM.theRoboCards[ playerIndex ];
                e.Graphics.DrawImage( tmpRC.theImage, tmpRC.theX, tmpRC.theY, tmpRC.theSizeX, tmpRC.theSizeY );
            }
        }

        private void Form1_MouseDown( object sender, MouseEventArgs e )
        {
            bPressed = 1;

            cardIndex = -1;

            int selectedIndex = -1;

            foreach( int iter in cGM.thePlayerList )
            {
                CRoboCard tmpRC = cGM.theRoboCards[ iter ];
                if( tmpRC.theX <= e.X && e.X <= tmpRC.theX + tmpRC.theSizeX )
                {
                    if( tmpRC.theY <= e.Y && e.Y <= tmpRC.theY + tmpRC.theSizeY )
                    {
                        selectedIndex = iter;
                    }
                }
            
            }

            if( selectedIndex != -1 )
            {
                cGM.RemoveUserCard( selectedIndex );
                Invalidate();
             }

            

            /*
                        if( card0PosX  <= e.X && e.X < card0PosX + card0SizeX )
                        {
                            if( card0PosY  <= e.Y && e.Y < card0PosY + card0SizeY )
                            {
                                cardIndex = 0;
                                card0OffX = e.X - card0PosX;
                                card0OffY = e.Y - card0PosY;
                            }
                        }
            */
        }

        private void Form1_MouseMove( object sender, MouseEventArgs e )
        {
            /*           if( bPressed == 1 )
                       {
                           if( cardIndex == 0 )
                           {
                               card0PosX = e.X - card0OffX;
                               card0PosY = e.Y - card0OffY;
            //                   Invalidate();

                           }
                       }
           */
        }

        private void Form1_MouseUp( object sender, MouseEventArgs e )
        {
            bPressed = 0;
        }

        private void timer1_Tick( object sender, EventArgs e )
        {
            Invalidate();
        }

        public void DrawStringRectangleFFormat( PaintEventArgs e, string str, float x, float y, float w, float h, int f_size, Color f_color )
        {
            // Create font and brush.
            Font drawFont = new Font( "Arial", f_size );
            SolidBrush drawBrush = new SolidBrush( f_color );

            // Create rectangle for drawing.
            RectangleF drawRect = new RectangleF( x, y, w, h );

            // Draw rectangle to screen.
            Pen blackPen = new Pen( Color.Transparent );
            e.Graphics.DrawRectangle( blackPen, x, y, w, h );

            // Set format of string.
            StringFormat drawFormat = new StringFormat();
            drawFormat.Alignment = StringAlignment.Center;

            // Draw string to screen.
            e.Graphics.DrawString( str, drawFont, drawBrush, drawRect, drawFormat );
        }
    }
}
