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
using System.Diagnostics;
using System.IO;

namespace NWandSW
{
    public partial class Form1 : Form
    {
        char[] seq1;
        char[] seq2;
        int seql1;
        int seql2;
        int snum = 4;
        int[,] F;
        int windowwidth;
        int windowheight;
        int blocksizewidth;
        int blocksizeheight;
        Bitmap bmp;
        public int costd=2,match=2,mismatch=-1;
        List <char>  seqcmp1= new List<char>();
        List <char>  seqcmp2= new List<char>();
        //List<charstack> charstacklist = new List<charstack>();
        Random rnd = new Random();
        List<int> Time;
        Stopwatch swch = new Stopwatch();
        int swmaxi;
        int swmaxj;
        int swmaxF=0;
        int swmaxM=0;
        int affcostd = 2,affe=1,infinitcost=-60;
        List<int>[,] MIxIyij;
        
        List<afinerout>[,] evallist;

        //struct charstack {
        //    public int index;
        //    public char _char;
        //}

        struct rout {
          public  int routX;
          public  int routY;
          public  int score;
        }

        List<rout>[,] routlist;
      
        struct afinerout {
            public int Ixrout;
            public int Iyrout;
            public int Mrout ;
        }
      

        enum maxf{
        Splus,
        costdx
        ,costdy
        ,none
        }

        enum afingap { 
            MtoIx,
            MtoIy,
            MtoM,
            IxtoIx,
            IxtoM,
            IytoIy,
            IytoM,
            None
        }
        enum afintype{
            Ix,
            Iy,
            M,
        }
        

        public Form1()
        {
            InitializeComponent();
            selectrange.SelectedIndex = 0;
            sequenceproductcb.SelectedIndex = 0;
            windowwidth = pictureBox1.Width;
            windowheight = pictureBox1.Height;

            bmp = new Bitmap(windowwidth, windowheight);
            pictureBox1.Image = bmp;
            

        }


        private void seqproduct(int output, ref char[] seq)
        {

            for (int i = 0; i < output; i++)
            {
                switch (rnd.Next(4))
                {
                    case 0:
                        seq[i] = 'C';
                        break;
                    case 1:
                        seq[i] = 'G';
                        break;
                    case 2:
                        seq[i] = 'A';
                        break;
                    case 3:
                        seq[i] = 'T';
                        break;
                }
            }
        } //いじる必要なし
   
        private void horizontalprocess(int i,int j) {
            for (int jr = j; jr < seql2;jr++) {
                compareopt(F, i, jr);
            }
        }//いじる必要なし

        private void verticalprocess(int i, int j)
        {
            for (int ir = i; ir < seql1; ir++)
            {
                compareopt(F, ir, j);
            }
        }//いじる必要なし
        private void swhorizontalprocess(int i, int j)//いじる必要なし
        {
            for (int jr = j; jr < seql2; jr++)
            {
                SWcompareopt(F, i, jr);
            }
        }

        private void swverticalprocess(int i, int j)//いじる必要なし
        {
            for (int ir = i; ir < seql1; ir++)
            {
                SWcompareopt(F, ir, j);
            }
        }

        private void afswverticalprocess(int i, int j)
        {
            for (int ir = i; ir < seql1; ir++)
            {
                affswcompareopt(ir, j);
            }

        }
        private void afnwverticalprocess(int i,int j)
        {
            for (int ir = i; ir < seql1; ir++)
            {
                affnwcompareopt(ir, j);
            }
           
        }

        private void compareopt(int[,] F, int i, int j)
        {
            //int counter = 0;

            int[] score = new int[3];

            if (seq1[i - 1] == seq2[j - 1])
            {
                score[(int)maxf.Splus] = F[i - 1, j - 1] + match;
            }
            else { score[(int)maxf.Splus] = F[i - 1, j - 1] + mismatch; }

            score[(int)maxf.costdx] = F[i - 1, j] - costd;
            score[(int)maxf.costdy] = F[i, j - 1] - costd;

            F[i, j] = score.Max();

            routlist[i, j] = new List<rout>();

            for (int n = 0; n < score.Length; n++)
            {
                if (score[n] == score.Max())
                {
                    rout pos;
                    pos.routX = 0;
                    pos.routY = 0;
                    pos.score = score.Max();
                    switch (n)
                    {
                        case (int)maxf.Splus:
                            pos.routX = i - 1;
                            pos.routY = j - 1;
                            break;
                        case (int)maxf.costdx:

                            pos.routX = i - 1;
                            pos.routY = j;

                            break;
                        case (int)maxf.costdy:
                            pos.routX = i;
                            pos.routY = j - 1;
                            break;
                    }

                    //counter++;
                    routlist[i, j].Add(pos);
                    //if (counter == 0) { oneroutlist.Add(pos); }

                    //    if (counter!=0) {
                    //        if (listrout[counter] == null)
                    //        {
                    //            listrout.Add(oneroutlist);
                    //        }
                    //}

                    using (Graphics g = Graphics.FromImage(bmp))
                    {
                        g.DrawString(F[i, j].ToString(), Font, Brushes.Black, (j + 1) * blocksizewidth, (i + 1) * blocksizeheight);
                    }
                }
            }
        }//いじる必要なし
        private void SWcompareopt(int[,] F, int i, int j)
        {
            //int counter = 0;

            int[] score = new int[snum];
            if (seq1[i - 1] == seq2[j - 1])
            {
                score[(int)maxf.Splus] = F[i - 1, j - 1] + match;
            }
            else { score[(int)maxf.Splus] = F[i - 1, j - 1] + mismatch; }

            score[(int)maxf.costdx] = F[i - 1, j] - costd;
            score[(int)maxf.costdy] = F[i, j - 1] - costd;
            score[(int)maxf.none] = 0;
            F[i, j] = score.Max();
            if (F[i, j] > swmaxF)
            {
                swmaxi = i;
                swmaxj = j;

                swmaxF = F[i, j];
            }

            routlist[i, j] = new List<rout>();

            for (int n = 0; n < score.Length; n++)
            {
                if (score[n] == score.Max())
                {
                    rout pos;
                    pos.routX = 0;
                    pos.routY = 0;
                    pos.score = 0;
                    switch (n)
                    {
                        case (int)maxf.Splus:
                            pos.routX = i - 1;
                            pos.routY = j - 1;
                            break;
                        case (int)maxf.costdx:

                            pos.routX = i - 1;
                            pos.routY = j;

                            break;
                        case (int)maxf.costdy:
                            pos.routX = i;
                            pos.routY = j - 1;
                            break;
                        case (int)maxf.none:

                            break;
                    }

                    //counter++;
                    if (F[i, j] != 0)
                    {
                        routlist[i, j].Add(pos);
                    }
                    //if (counter == 0) { oneroutlist.Add(pos); }

                    //if (counter != 0)
                    //{
                    //    if (listrout[counter] == null)
                    //    {
                    //        listrout.Add(oneroutlist);
                    //    }
                    //}

                    using (Graphics g = Graphics.FromImage(bmp))
                    {
                        g.DrawString(F[i, j].ToString(), Font, Brushes.Black, (j + 1) * blocksizewidth, (i + 1) * blocksizeheight);
                    }
                }
            }
        }//いじる必要なし

        private void affswcompareopt(int i,int j){
      
            int co;

            MIxIyij[i, j] = new List<int>();
            evallist[i, j] = new List<afinerout>();

            if (seq1[i - 1] == seq2[j - 1])
            {
                co = match;
            }
            else { co = mismatch; }

            List<int>[] ev = new List<int>[3];
            for (int a = 0; a < 3; a++)
            {
                ev[a] = new List<int>();
            }

            ev[(int)afintype.Ix].Add(MIxIyij[i - 1, j][(int)afintype.Ix] - affe);
            ev[(int)afintype.Ix].Add(MIxIyij[i - 1, j][(int)afintype.M] - affcostd);
            ev[(int)afintype.Ix].Add(0);
            ev[(int)afintype.Iy].Add(MIxIyij[i, j - 1][(int)afintype.Iy] - affe);
            ev[(int)afintype.Iy].Add(MIxIyij[i, j - 1][(int)afintype.M] - affcostd);
            ev[(int)afintype.Iy].Add(0);
            ev[(int)afintype.M].Add(MIxIyij[i - 1, j - 1][(int)afintype.Ix] + co);
            ev[(int)afintype.M].Add(MIxIyij[i - 1, j - 1][(int)afintype.Iy] + co);
            ev[(int)afintype.M].Add(MIxIyij[i - 1, j - 1][(int)afintype.M] + co);
            ev[(int)afintype.M].Add(0);


            MIxIyij[i, j].Add( ev[(int)afintype.Ix].Max());
            MIxIyij[i, j].Add(ev[(int)afintype.Iy].Max());
            MIxIyij[i, j].Add( ev[(int)afintype.M].Max());

            if (swmaxM < ev[(int)afintype.M].Max())
            {
                swmaxM = ev[(int)afintype.M].Max();
                swmaxi = i;
                swmaxj = j;
            }

            afinerout tracepoint = new afinerout();

            tracepoint.Ixrout = (int)afingap.None;
            tracepoint.Iyrout = (int)afingap.None;
            tracepoint.Mrout = (int)afingap.None;
            switch (ev[(int)afintype.Ix].FindIndex(p => p == ev[(int)afintype.Ix].Max()))
            {
                //default: break;
                case 0:

                    tracepoint.Ixrout = (int)afingap.IxtoIx;

                    break;
                case 1:
                    tracepoint.Ixrout = (int)afingap.MtoIx;

                    break;

            }

            switch (ev[(int)afintype.Iy].FindIndex(p => p == ev[(int)afintype.Iy].Max()))
            {
                //default: break;
                case 0:
                    tracepoint.Iyrout = (int)afingap.IytoIy;
                    break;

                case 1:
                    tracepoint.Iyrout = (int)afingap.MtoIy;
                    break;
            }

            switch (ev[(int)afintype.M].FindIndex(p => p == ev[(int)afintype.M].Max()))
            {
                //default: break;
                case 0:
                    tracepoint.Iyrout = (int)afingap.IxtoM;
                    break;
                case 1:
                    tracepoint.Iyrout = (int)afingap.IytoM;
                    break;
                case 2:
                    tracepoint.Iyrout = (int)afingap.MtoM;
                    break;
            }

            evallist[i, j].Add(tracepoint);
        
        }
            
        //kore
        private void affnwcompareopt(int i,int j)
        {
            int co;

            MIxIyij[i, j] = new List<int>();
            evallist[i, j] = new List<afinerout>();

            if (seq1[i - 1] == seq2[j - 1])
            {
                co = match;
            }
            else { co = mismatch; }

            List<int>[] ev = new List<int>[3];
            for (int a = 0; a < 3; a++)
            {
                ev[a] = new List<int>();
            }

            ev[(int)afintype.Ix].Add(MIxIyij[i - 1, j][(int)afintype.Ix] - affe);
            ev[(int)afintype.Ix].Add(MIxIyij[i - 1, j][(int)afintype.M] - affcostd);
            ev[(int)afintype.Iy].Add(MIxIyij[i, j - 1][(int)afintype.Iy] - affe);
            ev[(int)afintype.Iy].Add(MIxIyij[i, j - 1][(int)afintype.M] - affcostd);
            ev[(int)afintype.M].Add(MIxIyij[i - 1, j - 1][(int)afintype.Ix] + co);
            ev[(int)afintype.M].Add(MIxIyij[i - 1, j - 1][(int)afintype.Iy] + co);
            ev[(int)afintype.M].Add(MIxIyij[i - 1, j - 1][(int)afintype.M] + co);



            MIxIyij[i, j].Add( ev[(int)afintype.Ix].Max());
            MIxIyij[i, j].Add(ev[(int)afintype.Iy].Max());
            MIxIyij[i, j].Add( ev[(int)afintype.M].Max());

            afinerout tracepoint = new afinerout();

            tracepoint.Ixrout = (int)afingap.None;
            tracepoint.Iyrout = (int)afingap.None;
            tracepoint.Mrout = (int)afingap.None;
            switch (ev[(int)afintype.Ix].FindIndex(p => p == ev[(int)afintype.Ix].Max()))
            {
                case 0:

                    tracepoint.Ixrout = (int)afingap.IxtoIx;

                    break;
                case 1:
                    tracepoint.Ixrout = (int)afingap.MtoIx;

                    break;

            }

            switch (ev[(int)afintype.Iy].FindIndex(p => p == ev[(int)afintype.Iy].Max()))
            {
                case 0:
                    tracepoint.Iyrout = (int)afingap.IytoIy;
                    break;

                case 1:
                    tracepoint.Iyrout = (int)afingap.MtoIy;
                    break;
            }

            switch (ev[(int)afintype.M].FindIndex(p => p == ev[(int)afintype.M].Max()))
            {
                case 0:
                    tracepoint.Iyrout = (int)afingap.IxtoM;
                    break;
                case 1:
                    tracepoint.Iyrout = (int)afingap.IytoM;
                    break;
                case 2:
                    tracepoint.Iyrout = (int)afingap.MtoM;
                    break;
            }

            evallist[i, j].Add(tracepoint);
        } //kore
        
        private void selectrange_SelectedIndexChanged(object sender, EventArgs e)
        {
          
        }//いじる必要なし


        private void pathsearch(int i, int j)
        {
            if (i == seq1.Length && j==seq2.Length) {
                seqcmp1.Add(seq1[i - 1]);
                seqcmp2.Add(seq2[j - 1]);
                pathsearch(routlist[i, j][0].routX, routlist[i, j][0].routY);
                return;
            }

            if (i == 0 && j == 0 || routlist[i, j] == null || routlist[i, j].Count == 0) return;

            if (i == 0)
            {
                seqcmp1.Add('-');
                seqcmp2.Add(seq2[j - 1]);

                while (j > 1)
                {
                    j--;
                    seqcmp1.Add('-');
                    seqcmp2.Add(seq2[j - 1]);
                    
                }
                return;
            }

            if ( j == 0)
            {
                seqcmp1.Add(seq1[i - 1]);
                seqcmp2.Add('-');
                while (i > 1)
                {
                    i--;
                    seqcmp1.Add(seq1[i - 1]);
                    seqcmp2.Add('-');
                }
                return;
            }


            if (routlist[i, j][0].routX == i && routlist[i, j][0].routY != j)
             {
                 seqcmp1.Add('-');
                 seqcmp2.Add(seq2[j - 1]);
             }
            else if (routlist[i, j][0].routX != i && routlist[i, j][0].routY == j)
             {
                 seqcmp2.Add('-');
                 seqcmp1.Add(seq1[i - 1]);

             }
            else
             {
                 seqcmp1.Add(seq1[i - 1]);
                 seqcmp2.Add(seq2[j - 1]);
             }

             pathsearch(routlist[i, j][0].routX, routlist[i, j][0].routY);
            ////if (i == 1 || j == 1)
            ////{


            ////    seqcmp1.Add(seq1[i]);
            ////    seqcmp2.Add(seq2[j]);



            ////    if (i == 1)
            ////    {
            ////        while (j > 1)
            ////        {
            ////            j--;
            ////            seqcmp1.Add('-');
            ////            seqcmp2.Add(seq2[j - 1]);
            ////        }
            ////    }

            ////    if (j == 1)
            ////    {
            ////        while (i > 1)
            ////        {
            ////            i--;
            ////            seqcmp1.Add(seq1[i - 1]);
            ////            seqcmp2.Add('-');
            ////        }

            ////    }

            ////    return;
            ////}
            ////routlist[i, j].Count;
            ////for (int n = 0; n < routlist[i, j].Count; n++)
            ////{
            ////    if (routlist[i, j][n].routX == i && n == 0)
            ////    {
            ////        seqcmp1.Add('-');
            ////        seqcmp2.Add(seq2[j - 1]);
            ////    }
            ////    else if (routlist[i, j][n].routY == j && n == 0)
            ////    {
            ////        seqcmp2.Add('-');
            ////        seqcmp1.Add(seq1[i - 1]);

            ////    }
            ////    else if (n == 0)
            ////    {
            ////        seqcmp1.Add(seq1[i - 1]);
            ////        seqcmp2.Add(seq2[j - 1]);
            ////    }
            ////    else
            ////    {



            ////    }

            ////    if (n == 0) pathsearch(routlist[i, j][n].routX, routlist[i, j][n].routY);
            ////}
        }//完成

        private void affnwpathsearch(int routp, int i, int j)
        {
            if (i == 0) { 
            //while(j>1){
            //    seqcmp1.Add('-');
            //    seqcmp2.Add(seq2[j-1]);
            //    j--;
            //}
            return;
            }

            if (j == 0)
            {
                //while (i > 1)
                //{
                //    seqcmp1.Add(seq1[i-1]);
                //    seqcmp2.Add('-');
                //    i--;
                //}
                return;
            }

            switch(routp){

                case (int)afintype.Ix: 
                    
                        seqcmp1.Add(seq1[i-1]);
                        seqcmp2.Add('-');
                        if (evallist[i, j][0].Ixrout == (int)afingap.IxtoIx)
                        {
                            affnwpathsearch((int)afintype.Ix, i - 1, j);
                        }
                        else {
                            affnwpathsearch((int)afintype.M, i - 1, j);                        
                        }                        
                 
                    break;
                case (int)afintype.Iy: 
                        seqcmp1.Add('-');
                        seqcmp2.Add(seq2[j-1]);
                        if (evallist[i, j][0].Iyrout == (int)afingap.IytoIy)
                        {
                            affnwpathsearch((int)afintype.Iy, i, j-1);
                        }
                        else {
                            affnwpathsearch((int)afintype.M, i, j-1);                        
                        }
                    break;
                case (int)afintype.M:
                        seqcmp1.Add(seq1[i-1]);
                        seqcmp2.Add(seq2[j-1]);
                        if (evallist[i, j][0].Iyrout == (int)afingap.IxtoM)
                        {
                            affnwpathsearch((int)afintype.Ix, i-1, j-1);
                        }else{
                            if (evallist[i, j][0].Iyrout == (int)afingap.IytoM) {
                                affnwpathsearch((int)afintype.Iy, i - 1, j - 1);
                            }
                            else
                            {
                                affnwpathsearch((int)afintype.M, i - 1, j - 1);
                            }
                           }
                    break;
            }
            

        }

        private void affswpathsearch(int routp, int i, int j)
        {
           

            if (i == 0)
            {
                //while(j>1){
                //    seqcmp1.Add('-');
                //    seqcmp2.Add(seq2[j-1]);
                //    j--;
                //}
                return;
            }

            if (j == 0)
            {
                //while (i > 1)
                //{
                //    seqcmp1.Add(seq1[i-1]);
                //    seqcmp2.Add('-');
                //    i--;
                //}
                return;
            }
           //if( (evallist[i, j][0].Ixrout == (int)afingap.None && routp==(int)afintype.Ix)
           // || (evallist[i, j][0].Iyrout == (int)afingap.None && routp == (int)afintype.Iy)
           // || (evallist[i, j][0].Mrout == (int)afingap.None && routp == (int)afintype.M)
           //    )
            if ((evallist[i, j][0].Ixrout == (int)afingap.None )
             && (evallist[i, j][0].Iyrout == (int)afingap.None )
             && (evallist[i, j][0].Mrout == (int)afingap.None )
                )
            {
                seqcmp1.Add(seq1[i - 1]);
                seqcmp2.Add(seq2[j - 1]);
                return;
            }

            switch (routp)
            {

                case (int)afintype.Ix:

                    seqcmp1.Add(seq1[i - 1]);
                    seqcmp2.Add('-');
                    if (evallist[i, j][0].Ixrout == (int)afingap.IxtoIx)
                    {
                        affswpathsearch((int)afintype.Ix, i - 1, j);
                    }
                    else
                    {
                        affswpathsearch((int)afintype.M, i - 1, j);
                    }

                    break;
                case (int)afintype.Iy:
                    seqcmp1.Add('-');
                    seqcmp2.Add(seq2[j - 1]);
                    if (evallist[i, j][0].Iyrout == (int)afingap.IytoIy)
                    {
                        affswpathsearch((int)afintype.Iy, i, j - 1);
                    }
                    else
                    {
                        affswpathsearch((int)afintype.M, i, j - 1);
                    }
                    break;
                case (int)afintype.M:
                    seqcmp1.Add(seq1[i - 1]);
                    seqcmp2.Add(seq2[j - 1]);
                    if (evallist[i, j][0].Iyrout == (int)afingap.IxtoM)
                    {
                        affswpathsearch((int)afintype.Ix, i - 1, j - 1);
                    }
                    else
                    {
                        if (evallist[i, j][0].Iyrout == (int)afingap.IytoM)
                        {
                            affswpathsearch((int)afintype.Iy, i - 1, j - 1);
                        }
                        else
                        {
                            affswpathsearch((int)afintype.M, i - 1, j - 1);
                        }
                    }
                    break;

            }


        }



        void SWprocess() //完成
        {

            seqcmp1 = new List<char>();
            seqcmp2 = new List<char>();


            if (sequenceproductcb.SelectedIndex == 0)
            {
                seq1 = sequencelength1.Text.ToCharArray();
                seq2 = sequencelength2.Text.ToCharArray();
            }

            if (sequenceproductcb.SelectedIndex == 1)
            {

                int output1;
                int output2;
                if (int.TryParse(sequencelength1.Text, out output1) && int.TryParse(sequencelength2.Text, out output2))
                {
                    seq1 = new char[output1];
                    seq2 = new char[output2];

                    seqproduct(output1, ref seq1);
                    seqproduct(output2, ref seq2);
                }
                else { return; }

            }



            seql1 = seq1.Length + 1;
            seql2 = seq2.Length + 1;


            blocksizeheight = (windowheight - 1) / (seql1 + 1);
            blocksizewidth = (windowwidth - 1) / (seql2 + 1);

            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.FillRectangle(Brushes.White, 0, 0, windowwidth, windowheight);
                for (int i = 0; i < seql2 + 1; i++)
                {
                    for (int j = 0; j < seql1 + 1; j++)
                    {

                        g.DrawRectangle(Pens.Black, i * blocksizewidth, j * blocksizeheight, blocksizewidth, blocksizeheight);
                    }
                }

                //Font fnt = new Font("MS 明朝",Math.Min(blocksizewidth,blocksizeheight)/2); 
                for (int i = 2; i < seql1 + 1; i++)
                {
                    g.DrawString(seq1[i - 2].ToString(), Font, Brushes.Black, blocksizewidth * 1 / 3, i * blocksizeheight);
                }
                for (int j = 2; j < seql2 + 1; j++)
                {
                    g.DrawString(seq2[j - 2].ToString(), Font, Brushes.Black, j * blocksizewidth + blocksizewidth * 1 / 3, 0);
                }
                //fnt.Dispose();
                g.Dispose();
                pictureBox1.Refresh();
            }

            F = new int[seql1, seql2];
            routlist = new List<rout>[seql1, seql2];
            //swch.Reset();
            //swch.Start();

            for (int i = 0; i < seql1; i++)
            {
                F[i, 0] = 0;
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.DrawString(F[i, 0].ToString(), Font, Brushes.Black, blocksizewidth, (i + 1) * blocksizeheight);
                }
            }

            for (int j = 0; j < seql2; j++)
            {
                F[0, j] = 0;
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.DrawString(F[0, j].ToString(), Font, Brushes.Black, (j + 1) * blocksizewidth, blocksizeheight);
                }
            }
            for(int j=1;j<seql2;j++){
                swverticalprocess(1, j);
            }


            //if (swmaxi >= seq1.Length) { swmaxi--; }
            //if (swmaxj >= seq2.Length) { swmaxj--; }
            pathsearch(swmaxi, swmaxj);

            seqcmp1.Reverse();
            seqcmp2.Reverse();
            alignmentX.Text = "アライメント1:";
            alignmentY.Text = "アライメント2:";
            for (int i = 0; i < seqcmp1.Count; i++)
            {

                alignmentX.Text += seqcmp1[i].ToString();
            }
            for (int i = 0; i < seqcmp2.Count; i++)
            {

                alignmentY.Text += seqcmp2[i].ToString();
            }
            //swch.Stop();
            optscore.Text = "最適スコア:";
            optscore.Text += swmaxF;



            //Time.Add((int)swch.ElapsedMilliseconds);

            pictureBox1.Refresh();

        }
        private void mainprocess() {

        
            seqcmp1 = new List<char>();
            seqcmp2 = new List<char>();

            if (sequenceproductcb.SelectedIndex == 0)
            {
                seq1 = sequencelength1.Text.ToCharArray();
                seq2 = sequencelength2.Text.ToCharArray();
            }

            if (sequenceproductcb.SelectedIndex == 1)
            {

                int output1;
                int output2;
                if (int.TryParse(sequencelength1.Text, out output1) && int.TryParse(sequencelength2.Text, out output2))
                {
                    seq1 = new char[output1];
                    seq2 = new char[output2];

                    seqproduct(output1, ref seq1);
                    seqproduct(output2, ref seq2);
                }
                else { return; }

                }
                
        
            

            
               
            

            seql1 = seq1.Length + 1;
            seql2 = seq2.Length + 1;


            blocksizeheight = (windowheight - 1) / (seql1 + 1);
            blocksizewidth = (windowwidth - 1) / (seql2 + 1);

            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.FillRectangle(Brushes.White, 0, 0, windowwidth, windowheight);
                for (int i = 0; i < seql2 + 1; i++)
                {
                    for (int j = 0; j < seql1 + 1; j++)
                    {

                        g.DrawRectangle(Pens.Black, i * blocksizewidth, j * blocksizeheight, blocksizewidth, blocksizeheight);
                    }
                }

                //Font fnt = new Font("MS 明朝",Math.Min(blocksizewidth,blocksizeheight)/2); 
                for (int i = 2; i < seql1 + 1; i++)
                {
                    g.DrawString(seq1[i - 2].ToString(), Font, Brushes.Black, blocksizewidth * 1 / 3, i * blocksizeheight);
                }
                for (int j = 2; j < seql2 + 1; j++)
                {
                    g.DrawString(seq2[j - 2].ToString(), Font, Brushes.Black, j * blocksizewidth + blocksizewidth * 1 / 3, 0);
                }
                //fnt.Dispose();
                g.Dispose();
                pictureBox1.Refresh();
            }

            F = new int[seql1, seql2];
            routlist = new List<rout>[seql1, seql2];
            swch.Reset();
            swch.Start();
            
            for (int i = 0; i < seql1; i++)
            {
                F[i, 0] = -i * costd;
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.DrawString(F[i, 0].ToString(), Font, Brushes.Black, blocksizewidth, (i + 1) * blocksizeheight);
                }
            }

            for (int j = 0; j < seql2; j++)
            {
                F[0, j] = -j * costd;
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.DrawString(F[0, j].ToString(), Font, Brushes.Black, (j + 1) * blocksizewidth, blocksizeheight);
                }
            }

            for (int j = 1; j < seql2; j++)
            {
                verticalprocess(1, j);
            }
            //Thread hrzprocess;
            pathsearch(seq1.Length , seq2.Length );

            seqcmp1.Reverse();
            seqcmp2.Reverse();
            alignmentX.Text = "アライメント1:";
            alignmentY.Text = "アライメント2:";
            for (int i = 0; i < seqcmp1.Count; i++)
            {

                alignmentX.Text += seqcmp1[i].ToString();
            }
            for (int i = 0; i < seqcmp2.Count; i++)
            {

                alignmentY.Text += seqcmp2[i].ToString();
            }
            swch.Stop();
            optscore.Text = "最適スコア:";
            optscore.Text += F[seq1.Length, seq2.Length];



            Time.Add((int)swch.ElapsedMilliseconds);

            pictureBox1.Refresh();
           

        }//完成

        void afSWprocess()
        {

            seqcmp1 = new List<char>();
            seqcmp2 = new List<char>();
           

            if (sequenceproductcb.SelectedIndex == 0)
            {
                seq1 = sequencelength1.Text.ToCharArray();
                seq2 = sequencelength2.Text.ToCharArray();
            }

            if (sequenceproductcb.SelectedIndex == 1)
            {

                int output1;
                int output2;
                if (int.TryParse(sequencelength1.Text, out output1) && int.TryParse(sequencelength2.Text, out output2))
                {
                    seq1 = new char[output1];
                    seq2 = new char[output2];

                    seqproduct(output1, ref seq1);
                    seqproduct(output2, ref seq2);
                }
                else { return; }

            }



            seql1 = seq1.Length + 1;
            seql2 = seq2.Length + 1;


            blocksizeheight = (windowheight - 1) / (seql1 + 1);
            blocksizewidth = (windowwidth - 1) / (seql2 + 1);

            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.FillRectangle(Brushes.White, 0, 0, windowwidth, windowheight);
                for (int i = 0; i < seql2 + 1; i++)
                {
                    for (int j = 0; j < seql1 + 1; j++)
                    {

                        g.DrawRectangle(Pens.Black, i * blocksizewidth, j * blocksizeheight, blocksizewidth, blocksizeheight);
                    }
                }

                //Font fnt = new Font("MS 明朝",Math.Min(blocksizewidth,blocksizeheight)/2); 
                for (int i = 2; i < seql1 + 1; i++)
                {
                    g.DrawString(seq1[i - 2].ToString(), Font, Brushes.Black, blocksizewidth * 1 / 3, i * blocksizeheight);
                }
                for (int j = 2; j < seql2 + 1; j++)
                {
                    g.DrawString(seq2[j - 2].ToString(), Font, Brushes.Black, j * blocksizewidth + blocksizewidth * 1 / 3, 0);
                }
                //fnt.Dispose();
                g.Dispose();
                pictureBox1.Refresh();
            }

            MIxIyij = new List<int>[seql1, seql2];

            evallist = new List<afinerout>[seql1, seql2];
            //swch.Reset();
            //swch.Start();

            for (int i = 0; i < seql1; i++)
            {
                MIxIyij[i, 0] = new List<int>();
                for (int j=0;j< 3; j++)
                {
                    MIxIyij[i,0].Add(0);
                }
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.DrawString(MIxIyij[i, 0][0].ToString(), Font, Brushes.Black, blocksizewidth, (i + 1) * blocksizeheight);
                }
            }

            for (int j = 0; j < seql2; j++)
            {
                MIxIyij[0,j] = new List<int>();
                for (int i = 0; i < 3; i++)
                {
                    MIxIyij[0, j].Add(0);
                }
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.DrawString(MIxIyij[0, j][0].ToString(), Font, Brushes.Black, (j + 1) * blocksizewidth, blocksizeheight);
                }
            }
            for (int j = 1; j < seql2; j++)
            {
                afswverticalprocess(1, j);
            }


            affswpathsearch((int)afintype.M,swmaxi, swmaxj);

            seqcmp1.Reverse();
            seqcmp2.Reverse();
            alignmentX.Text = "アライメント1:";
            alignmentY.Text = "アライメント2:";
            for (int i = 0; i < seqcmp1.Count; i++)
            {

                alignmentX.Text += seqcmp1[i].ToString();
            }
            for (int i = 0; i < seqcmp2.Count; i++)
            {

                alignmentY.Text += seqcmp2[i].ToString();
            }
            //swch.Stop();
            optscore.Text = "最適スコア:";
            optscore.Text += swmaxM;



            //Time.Add((int)swch.ElapsedMilliseconds);

            pictureBox1.Refresh();

        } //kore


        void afNWprocess()
        {

            seqcmp1 = new List<char>();
            seqcmp2 = new List<char>();


            if (sequenceproductcb.SelectedIndex == 0)
            {
                seq1 = sequencelength1.Text.ToCharArray();
                seq2 = sequencelength2.Text.ToCharArray();
            }

            if (sequenceproductcb.SelectedIndex == 1)
            {

                int output1;
                int output2;
                if (int.TryParse(sequencelength1.Text, out output1) && int.TryParse(sequencelength2.Text, out output2))
                {
                    seq1 = new char[output1];
                    seq2 = new char[output2];

                    seqproduct(output1, ref seq1);
                    seqproduct(output2, ref seq2);
                }
                else { return; }

            }



            seql1 = seq1.Length + 1;
            seql2 = seq2.Length + 1;


            blocksizeheight = (windowheight - 1) / (seql1 + 1);
            blocksizewidth = (windowwidth - 1) / (seql2 + 1);

            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.FillRectangle(Brushes.White, 0, 0, windowwidth, windowheight);
                for (int i = 0; i < seql2 + 1; i++)
                {
                    for (int j = 0; j < seql1 + 1; j++)
                    {

                        g.DrawRectangle(Pens.Black, i * blocksizewidth, j * blocksizeheight, blocksizewidth, blocksizeheight);
                    }
                }

                //Font fnt = new Font("MS 明朝",Math.Min(blocksizewidth,blocksizeheight)/2); 
                for (int i = 2; i < seql1 + 1; i++)
                {
                    g.DrawString(seq1[i - 2].ToString(), Font, Brushes.Black, blocksizewidth * 1 / 3, i * blocksizeheight);
                }
                for (int j = 2; j < seql2 + 1; j++)
                {
                    g.DrawString(seq2[j - 2].ToString(), Font, Brushes.Black, j * blocksizewidth + blocksizewidth * 1 / 3, 0);
                }
                //fnt.Dispose();
                g.Dispose();
                pictureBox1.Refresh();
            }

            MIxIyij = new List<int>[seql1, seql2];

            evallist = new List<afinerout>[seql1, seql2];
            //swch.Reset();
            //swch.Start();

            for (int i = 0; i < seql1; i++)
            {
                MIxIyij[i, 0] = new List<int>();
                for (int j = 0; j < 3; j++)
                { 
                    MIxIyij[i, 0].Add(infinitcost);
                }
                using (Graphics g = Graphics.FromImage(bmp))
                {foreach(int n in MIxIyij[i,0]){
                    g.DrawString(n.ToString(), Font, Brushes.Black, blocksizewidth, (i + 1) * blocksizeheight);
                }
                }
            }

            for (int j = 0; j < seql2; j++)
            {
                MIxIyij[0, j] = new List<int>(); 
                for (int i = 0; i < 3; i++)
                {
                    
                    MIxIyij[0, j].Add(infinitcost);
                }
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    foreach (int n in MIxIyij[0,j])
                    {
                        g.DrawString(n.ToString(), Font, Brushes.Black, (j + 1) * blocksizewidth, blocksizeheight);
                    }
                }
            }

            for (int j = 1; j < seql2; j++)
            {
                afnwverticalprocess(1, j);
            }

int routtype= MIxIyij[seql1-1, seql2-1].FindIndex(p => p == MIxIyij[seql1-1, seql2-1].Max());


            //if (swmaxi >= seq1.Length) { swmaxi--; }
            //if (swmaxj >= seq2.Length) { swmaxj--; }
            affnwpathsearch(routtype,seql1-1,seql2-1);

            seqcmp1.Reverse();
            seqcmp2.Reverse();
            alignmentX.Text = "アライメント1:";
            alignmentY.Text = "アライメント2:";
            for (int i = 0; i < seqcmp1.Count; i++)
            {

                alignmentX.Text += seqcmp1[i].ToString();
            }
            for (int i = 0; i < seqcmp2.Count; i++)
            {

                alignmentY.Text += seqcmp2[i].ToString();
            }
            //swch.Stop();
            optscore.Text = "最適スコア:";
            optscore.Text += MIxIyij[seql1-1, seql2-1].Max();



            //Time.Add((int)swch.ElapsedMilliseconds);

            pictureBox1.Refresh();

        }


        private void start_Click(object sender, EventArgs e)
        {
            if (sequenceproductcb.SelectedIndex == 0)
            {
       if(sequencelength1.TextLength==0||sequencelength2.TextLength==0)  return;
  
            }

            Time = new List<int>();
            if (sequenceproductcb.SelectedIndex == 2)
            {
                List<int> TimeAve = new List<int>();
                List<List<int>> Dataset =new List<List<int>>();
                
                sequenceproductcb.SelectedIndex = 1;
                for (int j = 0; j < 5; j++) {
                    Time = new List<int>();
                    for (int i = 1; i < 1001; i++)
                    {
                        i += 100;

                        sequencelength1.Text = i.ToString();
                        sequencelength2.Text = i.ToString();


                        mainprocess();
                    }
                    Dataset.Add(Time);
            }
                for (int i = 0; i < Time.Count; i++) {
                    int a=0;
                    foreach (List<int> n in Dataset) {
                        a += n[i];
                    }
                    TimeAve.Add(a/Dataset.Count);
                }
                //int b;
                //StreamWriter sw = new StreamWriter(@"C:\Users\豪雄\takeo.txt", false, Encoding.UTF8);

                //foreach (int n in TimeAve)
                //{
                //    sw.WriteLine(n+",");
                //}
                //sw.Dispose();
            }
            else {

                if (selectrange.SelectedIndex == 1) {
                    swmaxi = 0;
                    swmaxj = 0;
                    swmaxF = 0;
                    SWprocess(); 
                }
                else
                {
                    if (selectrange.SelectedIndex == 2)
                    {
                        afNWprocess();
                    }
                    else
                    {
                        if (selectrange.SelectedIndex == 3) {
                            afSWprocess();
                        }
                        else
                        {
                            mainprocess();
                        }
                     }
                }            
            }
        }//逐後的に追加
    }
}
