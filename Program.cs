using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }

    public class Data
    {
        public static string[] lists =
        {
            "1",
            "2"
        };

        public static int[] meat = {3, 7, 24, 26, 27, 28, 29, 30, 35};

        public static int[] pub =
        {
            1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 31, 29, 32, 34
        };

        public static int[] soup = {22, 23};
        
        public static string[] filters =
        {
            "전체선택",
            "전체해제",
            "고기집",
            "치킨집",
            "찌게집",
            "술집"
        };
    }

    public class Shop
    {
        public static int PUB = 1;
        public static int MEAT = 2;
        public static int CHICKEN = 3;
        public static int SOUP = 4;
        string name;
        int[] type;

        public Shop(int[] type, string name)
        {
            this.type = type;
            this.name = name;
        }
    }
}