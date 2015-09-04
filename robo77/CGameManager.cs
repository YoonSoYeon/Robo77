using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace robo77
{
    class CGameManager
    {
        public List<int> theDesktopCards = new List<int>();

        public Queue<int> randomRoboCard = new Queue<int>();
        bool is_first = true;

        public SortedList<int, CRoboCard> theRoboCards;
        int index = 0;
        int cnt = 0;

        public List<int> thePlayerList;
        public List<int> theEnemyList1;

        public CGameManager( List<Image> aImage )
        {
            thePlayerList = new List<int>();
            theEnemyList1 = new List<int>();

            theRoboCards = new SortedList<int, CRoboCard>();
            int i;

            for( cnt=0 ; cnt < 4 ; cnt++, index++ )    //카드 0  총 4장
            {
                theRoboCards.Add( index, new CRoboCard(  CRoboCard.EType.ET_Number00, aImage[ 0 ], 0, 0, 0, 0 ) );
            }
            for( i=2 ; i < 10 ; i++ )    //카드 2~9  각 3장씩
            {
                for( cnt = 0 ; cnt < 3 ; cnt++, index++ )
                {
                    theRoboCards.Add( index, new CRoboCard( CRoboCard.EType.ET_Number00 + i, aImage[ i ], 0, 0, 0, 0 ) );
                }
            }
            for( cnt = 0 ; cnt < 8 ; cnt++, index++ )    //카드 9  총 8장
            {
                theRoboCards.Add( index, new CRoboCard( CRoboCard.EType.ET_Number10, aImage[ 9 ], 0, 0, 0, 0 ) );
            }
            //카드 10~14 각 1장씩
            theRoboCards.Add( index++, new CRoboCard( CRoboCard.EType.ET_Number11, aImage[ 10 ], 0, 0, 0, 0 ) );
            theRoboCards.Add( index++, new CRoboCard( CRoboCard.EType.ET_Number22, aImage[ 11 ], 0, 0, 0, 0 ) );
            theRoboCards.Add( index++, new CRoboCard( CRoboCard.EType.ET_Number33, aImage[ 12 ], 0, 0, 0, 0 ) );
            theRoboCards.Add( index++, new CRoboCard( CRoboCard.EType.ET_Number44, aImage[ 13 ], 0, 0, 0, 0 ) );
            theRoboCards.Add( index++, new CRoboCard( CRoboCard.EType.ET_Number55, aImage[ 14 ], 0, 0, 0, 0 ) );

            if( is_first == true )
            {
                swapCards_f();
            }
            //TODO 클릭할때 들어가야될거같은.. 그런.. 기능........
            if (randomRoboCard == null)
            {
                swapCards_s();
            }

            //플레이어 카드 5장 설정
            for( i=0; i<5; i++ )
            {
                int cardNumber = randomRoboCard.Dequeue();
                thePlayerList.Add( cardNumber );
                CRoboCard tmpRC = theRoboCards[ cardNumber ];
                tmpRC.theX = 405 + i * (tmpRC.theImage.Width + 10 );
                tmpRC.theY = 500;
                tmpRC.theSizeX = tmpRC.theImage.Width;
                tmpRC.theSizeY = tmpRC.theImage.Height; 
            }
            //적1
            int xxx = 180;
            for( int j = 0 ; j < 3 ; j++ )
            {
                for( i = 0 ; i < 5 ; i++ )
                {
                    int cardNumber = randomRoboCard.Dequeue();
                    theEnemyList1.Add( cardNumber );
                    CRoboCard tmpRC = theRoboCards[ cardNumber ];
                    tmpRC.theX = 60 + i * ((tmpRC.theImage.Width / 2) - 50);
                    tmpRC.theY = xxx;
                    tmpRC.theSizeX = tmpRC.theImage.Width / 2;
                    tmpRC.theSizeY = tmpRC.theImage.Height / 2;
                }
                xxx = xxx + 215;
            }

            totalNumber = 0;
        }

        public int totalNumber;

        public void RemoveUserCard( int selectedIndex )
        {

 //           int removeIndex = thePlayerList[ selectedIndex ];
            totalNumber += theRoboCards[ selectedIndex ].theNumber;

            thePlayerList.Remove( selectedIndex );
            int tmpIndex = randomRoboCard.Dequeue();
            thePlayerList.Add( tmpIndex );

            theDesktopCards.Add( selectedIndex );

            theRoboCards[ selectedIndex ].theX = 650 + theDesktopCards.Count * 5;
            theRoboCards[ selectedIndex ].theY = 100;

            int i;
            for( i = 0 ; i < thePlayerList.Count ; i++ )
            {
                CRoboCard tmpRC = theRoboCards[ thePlayerList[ i ] ];
                tmpRC.theX = 405 + i * (tmpRC.theImage.Width + 10);
//                tmpRC.theX = 405 + i * ( 10);
                tmpRC.theY = 500;

                tmpRC.theSizeX = tmpRC.theImage.Width;
                tmpRC.theSizeY = tmpRC.theImage.Height;

            }

        }

        public void swapCards_f () 
        {
            Random tmpR = new Random();
            int[] theViewIndices = new int[ theRoboCards.Count ];
            for(int i = 0 ; i < theRoboCards.Count ; i++ )
            {
                theViewIndices[ i ] = i;
            }
            for(int i = 0 ; i < 1000 ; i++ )
            {
                int tmpSwap;
                int tmpRand1 = tmpR.Next( theRoboCards.Count );
                int tmpRand2 = tmpR.Next( theRoboCards.Count );
                tmpSwap = theViewIndices[ tmpRand1 ];
                theViewIndices[ tmpRand1 ] = theViewIndices[ tmpRand2 ];
                theViewIndices[ tmpRand2 ] = tmpSwap;
            }

            //큐로 랜덤수 옮기기
            for(int i = 0 ; i < theRoboCards.Count ; i++ )
            {
                randomRoboCard.Enqueue( theViewIndices[ i ] );
            }

            is_first = false;
        }

        public void swapCards_s()
        {
            Random tmpR = new Random();
            int[] theViewIndices = new int[ theRoboCards.Count-20 ];
            for( int i = 0 ; i < (theRoboCards.Count-20) ; i++ )
            {
                for( int k = 0 ; k < 5 ; k++ )
                {
                    if( thePlayerList[ k ] == k ) { continue; }
                    if( theEnemyList1[ k ] == k ) { continue; }
                }
                    theViewIndices[ i ] = i;
            }
            for( int i = 0 ; i < 1000 ; i++ )
            {
                int tmpSwap;
                int tmpRand1 = tmpR.Next( theRoboCards.Count );
                int tmpRand2 = tmpR.Next( theRoboCards.Count );
                tmpSwap = theViewIndices[ tmpRand1 ];
                theViewIndices[ tmpRand1 ] = theViewIndices[ tmpRand2 ];
                theViewIndices[ tmpRand2 ] = tmpSwap;
            }

            //큐로 랜덤수 옮기기
            for( int i = 0 ; i < theRoboCards.Count-20 ; i++ )
            {
                randomRoboCard.Enqueue( theViewIndices[ i ] );
            }
        }


    }
}
