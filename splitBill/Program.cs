using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace splitBill
{     
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter Expenses File Name ( eg: Expenses.txt ): ");
            string fileName = Console.ReadLine();

            try {

                SplitBillBL oSplitBillBL = new SplitBillBL();

                //Read input file
                var arrExpenses = oSplitBillBL.readExpenseFile(fileName);                
                oSplitBillBL.splitAllBills(arrExpenses, fileName);
                Console.WriteLine("Output file generated.");

            }
            catch(FileNotFoundException filenotfound)
            { 
                Console.WriteLine("Error: File not found !");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

           Console.ReadKey();            
        }

       
       
    }   
}
