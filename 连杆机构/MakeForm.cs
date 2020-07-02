using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 连杆机构
{
    public class MakeForm
    //用于形成一个好用的二维可变数组
    //索引（m，n）
    {
        public int M_Max { get; private set; }
        public int N_Max { get; private set; }
        public int Size
        {
            get
            {
                return M_Max * N_Max;
            }
        }


        double[,] Form;

        public MakeForm()
        {
            
            this.M_Max = 100;
            this.N_Max = 5;
            Form = new double[M_Max,N_Max];
        }

        public MakeForm(int m_Max,int n_Max)
        {
            
            this.M_Max = m_Max < 1 ? 10 : m_Max;
            this.N_Max = n_Max < 1 ? 100 : n_Max;
            Form = new double[M_Max, N_Max];
        }

        public double this[int m,int n]
        //做一个简单的索引
        {
            get
            {
                return Form[m, n];
            }
            set
            {
                SetValue(value, m, n);
            }

        }
       


        public void SetValue(double value,int m,int n)
        {
            if(m>=M_Max&n<N_Max)
            {
                ExpendM();
                Form[m, n] = value;
                return;
            }
            if(m<M_Max&n>=N_Max)
            {
                ExpendN();
                Form[m, n] = value;
                return;
            }
            if(m>=M_Max&n>=N_Max)
            {
                ExpendMxN();
                Form[m, n] = value;
                return;
            }
            Form[m, n] = value;
            return;
            
        }

        private void ExpendM()
        {
            double[,] temp = new double[2 * M_Max, N_Max];
            for(int i=0;i<M_Max;i++)
            {
                for(int j=0;j<N_Max;j++)
                {
                    temp[i, j] = Form[i, j];
                }
            }
            M_Max *= 2;
            Form = temp;
        }//用于内部扩展M

        private void ExpendN()
        {
            double[,] temp = new double[M_Max,2* N_Max];
            for (int i = 0; i < M_Max; i++)
            {
                for (int j = 0; j < N_Max; j++)
                {
                    temp[i, j] = Form[i, j];
                }
            }
            N_Max *= 2;
            Form = temp;
        }//用于内部扩展N

        private void ExpendMxN()
        {
            double[,] temp = new double[2*M_Max, 2 * N_Max];
            for (int i = 0; i < M_Max; i++)
            {
                for (int j = 0; j < N_Max; j++)
                {
                    temp[i, j] = Form[i, j];
                }
            }
            M_Max *= 2;
            N_Max *= 2;
            Form = temp;
        }//同时扩展MN

    }
}
