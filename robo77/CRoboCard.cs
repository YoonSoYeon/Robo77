using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace robo77
{
    class CRoboCard
    {
        public enum EType
        {
            ET_Number00,
            ET_Number02,
            ET_Number03,
            ET_Number04,
            ET_Number05,
            ET_Number06,
            ET_Number07,
            ET_Number08,
            ET_Number09,
            ET_Number10,
            ET_Number11,
            ET_Number22,
            ET_Number33,
            ET_Number44,
            ET_Number55
        }

        public int theX;
        public int theY;
        public int theSizeX;
        public int theSizeY;

        public Image theImage;

        public int theNumber;
 
        EType theType;

        public CRoboCard( EType aType, Image aImage, int aX, int aY, int aSizeX, int aSizeY )
        {
            theType = aType;
            theImage = aImage;

            theX = aX;
            theY = aY;
            theSizeX = aSizeX;
            theSizeY = aSizeY;
            switch( aType)
            {
           
                case EType.ET_Number00:                    theNumber = 0;                    break;
                case EType.ET_Number02:                    theNumber = 2;                    break;
                case EType.ET_Number03:                    theNumber = 3;                    break;
                case EType.ET_Number04:                    theNumber = 4;                    break;
                case EType.ET_Number05:
 theNumber = 5;
                    break;
                case EType.ET_Number06:
                    theNumber = 6;
                    break;
                case EType.ET_Number07:
                    theNumber = 7;
                    break;
                case EType.ET_Number08:
                    theNumber = 8;
                    break;
                case EType.ET_Number09:
                    theNumber = 9;
                    break;
                case EType.ET_Number10:
                    theNumber = 10;
                    break;

                case EType.ET_Number11:                    theNumber = 11;                    break;
                case EType.ET_Number22:                    theNumber = 22;                    break;
                    case EType.ET_Number33:                    theNumber = 33;                    break;
                    case EType.ET_Number44:                    theNumber = 44;                    break;
                case EType.ET_Number55:                    theNumber = 55;                    break;


            }
            
        }


    }
}
