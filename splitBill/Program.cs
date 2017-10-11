using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace splitBill
{
    struct Participants
    {
        public decimal expenses;
        public decimal pending;
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter Expenses File Name ( eg: Expenses.txt ): ");
            string fileName = Console.ReadLine();

            try {

                var arrExpenses = readExpenseFile(fileName);
                if (arrExpenses.Length < 1) { throw new Exception("File is empty."); }
                
                int currlineIndex = 0;                
                int partCount = 0;
                List<Participants[]> lstParticipants = new List<Participants[]>();
                partCount = Convert.ToInt32(arrExpenses[currlineIndex]);
                while (partCount != 0 && currlineIndex < arrExpenses.Length)
                {
                    Participants[] participants = getExepnses(arrExpenses, ref currlineIndex, partCount);
                    calculateShare(ref participants);
                    lstParticipants.Add(participants);                  

                    currlineIndex++;
                    partCount = Convert.ToInt32(arrExpenses[currlineIndex]);
                }

                writeOutput(lstParticipants, fileName);
            }
            catch(FileNotFoundException filenotfound)
            { 
                Console.WriteLine("File not found !");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadKey();            
        }

        private static string[] readExpenseFile(string fileName)
        {
            var arrExpenses = File.ReadAllLines(Environment.CurrentDirectory + "/" + fileName);
            arrExpenses = arrExpenses.Where(exp => !string.IsNullOrEmpty(exp)).ToArray();
            return arrExpenses;
        }

        private static void writeOutput(List<Participants[]> participants, string fileName)
        {
            StringBuilder strOut = new StringBuilder();
            foreach (Participants[] part in participants)
            { 
                for (int i = 0; i < part.Count(); i++)
                {
                    if (part[i].pending < 0)
                        strOut.Append("($" + Math.Abs(part[i].pending) + ")" + "\n\r");
                    else
                        strOut.Append("$" + part[i].pending + "\n\r");
                }
                strOut.Append("\n\r");
            }
            //Console.Write(strOut);
            using (StreamWriter outputFile = new StreamWriter(Environment.CurrentDirectory + "/" + fileName + ".out"))
            {
                outputFile.WriteLine(strOut);
            }
             
        }

        private static void calculateShare(ref Participants[] participants)
        {
            decimal totalExpenseForTrip = 0;
            decimal sharePerPerson = 0;

            for (int i = 0; i < participants.Count(); i++)
            {
                totalExpenseForTrip = totalExpenseForTrip + participants[i].expenses;
            }
            sharePerPerson = totalExpenseForTrip / participants.Count();
            for (int i = 0; i < participants.Count(); i++)
            {
                participants[i].pending = Math.Round((sharePerPerson - participants[i].expenses), 2);
            }
        }

        private static Participants[] getExepnses(string[] arrExpenses,ref int curreLineIndex, int partCount)
        {
            int expCount = 0;
            decimal totalExp = 0;
            Participants[] part = new Participants[partCount];
            for(int i = 0; i<partCount; i++)
            {
                curreLineIndex++;
                expCount = Convert.ToInt32(arrExpenses[curreLineIndex]);
                for(int j = 0; j<expCount; j++)
                {
                    curreLineIndex++;
                    totalExp = totalExp + Convert.ToDecimal(arrExpenses[curreLineIndex]);
                }
                part[i].expenses = totalExp;
                totalExp = 0;
            }

            return part;
        }
    }   
}
