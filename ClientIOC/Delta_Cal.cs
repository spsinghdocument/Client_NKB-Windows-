using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Client
{
    public sealed class  Delta_Cal
    {
 
        private static readonly Delta_Cal instance = new Delta_Cal();
        public static Delta_Cal Instance
        {
            get
            {
                return instance;
            }


        }


        public double GetNetDelta(double NetDelta1, double NetDelta2, double NetDelta3)
        {
            double RetVal = 0;
            RetVal = NetDelta1 + NetDelta2 + NetDelta3;
            return RetVal;
        }
        public double Get_NetDelta(double Delta, string Symbo,double ratio,string BS)
        {
             double RetVal = 0;
             
             
            if (Symbo =="FUTIVX" || Symbo =="FUTIDX" || Symbo =="FUTSTK" )
           {
               RetVal = BS == "Buy" ? Convert.ToDouble(-ratio * 1) : Convert.ToDouble(ratio * 1);
             
             }

           else
           {

               RetVal = BS == "Buy" ? Convert.ToDouble(-ratio * Delta) : Convert.ToDouble(ratio * Delta);
             
             }

             return RetVal;
        }

       

        #region Dtelta Functions
        //==========================================================================================================================================
        public double dTwo(double UnderlyingPrice, double ExercisePrice, double Time, double Interest, double Volatility, double Dividend)
        {
            return dOne(UnderlyingPrice, ExercisePrice, Time, Interest, Volatility, Dividend) - Volatility * Math.Sqrt(Time);
        }
        //=====================================================================================================================================================================


        //===================================================================================================================================

        public double PutOption(double UnderlyingPrice, double ExercisePrice, double Time, double Interest, double Volatility, double Dividend)
        {
            return ExercisePrice * Math.Exp(-Interest * Time) * NormSDist(-dTwo(UnderlyingPrice, ExercisePrice, Time, Interest, Volatility, Dividend)) - Math.Exp(-Dividend * Time) * UnderlyingPrice * NormSDist(-dOne(UnderlyingPrice, ExercisePrice, Time, Interest, Volatility, Dividend));
        }
        //===========================================================================================================

        public double ImpliedCallVolatility(double UnderlyingPrice, double ExercisePrice, double Time, double Interest, double Target, double Dividend)
        {
            double High = 5;
            double Low = 0;
            while ((High - Low) > 0.0001)
            {
                if (CallOption(UnderlyingPrice, ExercisePrice, Time, Interest, (High + Low) / 2, Dividend) > Target)
                {
                    High = (High + Low) / 2;
                }
                else
                {
                    Low = (High + Low) / 2;
                }
            }
            return (High + Low) / 2;
        }

        public double CallOption(double UnderlyingPrice, double ExercisePrice, double Time, double Interest, double Volatility, double Dividend)
        {
            return Math.Exp(-Dividend * Time) * UnderlyingPrice * NormSDist(dOne(UnderlyingPrice, ExercisePrice, Time, Interest, Volatility, Dividend)) - ExercisePrice * Math.Exp(-Interest * Time) * NormSDist(dOne(UnderlyingPrice, ExercisePrice, Time, Interest, Volatility, Dividend) - Volatility * Math.Sqrt(Time));
        }
        //================================================================================================
        public double ImpliedPutVolatility(double UnderlyingPrice, double ExercisePrice, double Time, double Interest, double Target, double Dividend)
        {
            double High = 5;
            double Low = 0;
            while ((High - Low) > 0.0001)
            {
                if (PutOption(UnderlyingPrice, ExercisePrice, Time, Interest, (High + Low) / 2, Dividend) > Target)
                {
                    High = (High + Low) / 2;
                }
                else Low = (High + Low) / 2;


            }
            return (High + Low) / 2;
        }
        //==================================================================================================================================
        public double CallDelta(double UnderlyingPrice, double ExercisePrice, double Time, double Interest, double Volatility, double Dividend)
        {
            return NormSDist(dOne(UnderlyingPrice, ExercisePrice, Time, Interest, Volatility, Dividend));
            //'CallDelta = Application.NormSDist((Log(UnderlyingPrice / ExercisePrice) + (Interest - Dividend) * Time) / (Volatility * Sqr(Time)) + 0.5 * Volatility * Sqr(Time))
        }

        public double PutDelta(double UnderlyingPrice, double ExercisePrice, double Time, double Interest, double Volatility, double Dividend)
        {
            return NormSDist(dOne(UnderlyingPrice, ExercisePrice, Time, Interest, Volatility, Dividend)) - 1;
            //'PutDelta = Application.NormSDist((Log(UnderlyingPrice / ExercisePrice) + (Interest - Dividend) * Time) / (Volatility * Sqr(Time)) + 0.5 * Volatility * Sqr(Time)) - 1
        }
        //========================================================================================================================================
        //================================================================================================
        public double NormSDist(double d)
        {
            double L = 0.0;
            double K = 0.0;
            double dCND = 0.0;
            const double a1 = 0.31938153;
            const double a2 = -0.356563782;
            const double a3 = 1.781477937;
            const double a4 = -1.821255978;
            const double a5 = 1.330274429;
            L = Math.Abs(d);
            K = 1.0 / (1.0 + 0.2316419 * L);

            dCND = 1.0 - 1.0 / Math.Sqrt(2 * Convert.ToDouble(Math.PI)) * Math.Exp(-L * L / 2.0) * (a1 * K + a2 * K * K + a3 * Math.Pow(K, 3.0) + a4 * Math.Pow(K, 4.0) + a5 * Math.Pow(K, 5.0));

            if (d < 0)
            {
                return 1.0 - dCND;
            }
            else
            {
                return dCND;
            }
        }

        //=========================================================================================================================================================
        public double dOne(double UnderlyingPrice, double ExercisePrice, double Time, double Interest, double Volatility, double Dividend)
        {
            // string on = ConvertFromTimestamp(Convert.to).ToShortDateString();
            // string on = DateTime.Now.AddMinutes(Time).ToString("HH:mm:ss"); 
            double on = (Volatility * Math.Sqrt(Time));
            double a = (Math.Log(UnderlyingPrice / ExercisePrice) + Math.Pow(Interest - Dividend + 0.5 * Volatility, 2) * (Time)) / on;
            //return (Math.Log(UnderlyingPrice / ExercisePrice) + Math.Pow(Interest - Dividend + 0.5 * Volatility, 2) * Time) / (Volatility * (Math.Sqrt(Time)));   // main

            return a;

        }
        //==================================================================================================================================
        #endregion
    }
}
