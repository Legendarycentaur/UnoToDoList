﻿// See https://aka.ms/new-console-template for more information
using Microsoft.VisualBasic;
using System.Data;
using System.Diagnostics.Metrics;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;





///använda en struct för att sätta ihop variablern för utgångs datum vad det är för uppgift, 
///om den är färdig, 
///och de attrubuten som ska finnas i en to do list. 
///VI kommer använda uml för att modellera verkligheten i vår för att öva på att bryta ner större problem i delr som går att beräkna. 
///EN struct måste vara under själva koden där den används.
/// använda en struct för att sätta ihop variablern för utgångs datum vad det är för uppgift, 
///

bool programRuns = true;
int switchMenu = 4;
string filename = "ToDolist.csv";

WriteMenu();

while (programRuns) //Kör programmet
{
    Console.Write("Choose something from the action menu: ");
    switchMenu = menuAction();

    switch (switchMenu)
    {
        case 1: addData(); break;
        case 2: markData(); break;
        case 3: newShowData();  break;
        case 4: endProgram();  break;
    }
    //showData();
}
void WriteMenu() //menyn
{
    Console.ForegroundColor = ConsoleColor.White;
    Console.WriteLine("To Do List\nChoose feature by:\n1. Add new task.\n2. Mark as done.\n3. Show tasks.\n4. Close application.");
}
void addData() // metid för att lägg ain data i filen
{
    Console.Clear();
    WriteMenu();
    Console.ForegroundColor = ConsoleColor.White;
    ToDoItem toDoItem = new ToDoItem();

    Console.WriteLine("What is the task?");
    toDoItem.task = Console.ReadLine();

    toDoItem.deadline = addTime();

    //toDoItem.estimatedHours = hours;

    toDoItem.isCompleted = false;

    string[] input = {toDoItem.task + ";" + toDoItem.deadline + ";" + toDoItem.estimatedHours.ToString() + ";" + toDoItem.isCompleted.ToString()};
    File.AppendAllLines(filename, input);
    Console.WriteLine("That is now added as a new Task!");
    System.Threading.Thread.Sleep(1000);

}
void markData() //Använda för att markera data
{
    Console.Clear();
    WriteMenu();
    newShowData();
    Console.Write("Mark as Done: ");
    bool tryNumber = false;
    int alternative = 0;
    while (tryNumber == false)
    {
        tryNumber = int.TryParse(Console.ReadLine(), out alternative); //indata för att välja vilken task som ska markeras som klar. 
    }
    
    string[] file = File.ReadAllLines(filename);
    string[] dateSortedFile = new string[file.Length];
   //string[] dateData = new string[file.Length];
    int counter = 0;
    foreach (string dataInFile in file)
    {
        string[] dataFromFile = dataInFile.Split(";");
        dateSortedFile[counter] = $"{dataFromFile[1]};{dataFromFile[2]};{dataFromFile[0]};{dataFromFile[3]}";
        counter++;
    }
    Array.Sort(dateSortedFile);

    string[] splitUp = dateSortedFile[alternative].Split(";");
    bool done = bool.Parse(splitUp[3]);//tar ut så variebln done är true eller false värdet från den markerade raden. 
    if (done == false)
    {
        done = true;
        Console.Clear();
        WriteMenu();
        newShowData();
        //Console.WriteLine("It is now marked as done");
    }
    else
    {
        Console.WriteLine("That one is already done! Do you want to undo it? ([y]es / [n]o): ");
        tryNumber = false;
        char undoMaybe = 'n';
        while (tryNumber == false)
        {
            tryNumber = char.TryParse(Console.ReadLine(), out undoMaybe);
        }
        if (undoMaybe == 'y')
        {
            done = false;
        }
        else 
        {
            Console.WriteLine("No changes will be made!");
        }
    }

    dateSortedFile[alternative] = $"{splitUp[2]};{splitUp[0]};{splitUp[1]};{done}"; //tillbaka omvandlad string. 
    for(int i=0; i < file.Length; i++)
    {
        Console.WriteLine(file[i]);
        /*foreach (string dataInFile in file)
        {
            string[] dataFromFile = dataInFile.Split(";");
            //dateSortedFile[alternative] = $"{dataFromFile[1]};{dataFromFile[2]};{dataFromFile[0]};{dataFromFile[3]}";

            if ($"{splitUp[2]};{splitUp[0]};{splitUp[1]};".Equals($"{dataFromFile[0]};{dataFromFile[1]};{dataFromFile[2]};"))
            {
                file[i] = $"{splitUp[2]};{splitUp[0]};{splitUp[1]};{done}";
            }
        }*/
        
    }

    File.WriteAllLines(filename, file);
    Console.Clear();
    WriteMenu();
    newShowData();
    Console.WriteLine("Alternative: " + alternative + " Is now marked as " + maybeDone(done));

    
    

}

string maybeDone(bool isDone)
{
    string markedAsDone;
    if (isDone == false)
    {
        markedAsDone = "Not done!";
    }
    else
    {
        markedAsDone = "Done!";
    }
    return markedAsDone;
}

int menuAction() //används för att välja alternativ i menyn.
{ 
    bool tryNumber = false;
    while (tryNumber == false)
    {
        tryNumber = int.TryParse(Console.ReadLine(), out switchMenu);
    }
    
    return switchMenu;
}

string addTime()
{
    string deadlineDate = "h";
    string dateFormat = "yyyyMMdd";
    try
    {
        while (true)
        {
            Console.WriteLine("Add deadline | YYYYMMDD |:");
            string date = Console.ReadLine();
            string result = DateTime.ParseExact(date, dateFormat, null).ToString("yyyy-MM-dd");
            deadlineDate = result;
            break;
        }


    }

    catch (FormatException error)
    {
        Console.WriteLine("Fel inmatning");
    }
    return deadlineDate;
}


/*void showData()
{
    Console.Clear();
    WriteMenu();
    int counter = 0;
    string[] file = File.ReadAllLines(filename);
    Console.ForegroundColor = ConsoleColor.White;
    Console.WriteLine("ID       Deadline        Tid        Vad");
    foreach (string dataInFile in file) 
    {
        
        ToDoItem readData = new ToDoItem();
        string[] dataFromFile = dataInFile.Split(";");
        readData.task = dataFromFile[0];
        readData.deadline = dataFromFile[1];
        readData.estimatedHours = double.Parse(dataFromFile[2]);
        readData.isCompleted = bool.Parse(dataFromFile[3]);
        DateTime now = DateTime.Now;
        int timeNowYear = now.Year;
        int timeNowMonth = now.Month;
        int timeNowDay = now.Day;
        string[] deadline = readData.deadline.Split("-");
        int deadlineYear = int.Parse(deadline[0]);
        int deadlineMonth = int.Parse(deadline[1]);
        int deadlineDay = int.Parse(deadline[2]);

        if (readData.isCompleted == true)
        {
            Console.ForegroundColor
            = ConsoleColor.Gray;
        }
        else if (readData.isCompleted != true && comparedays(ref timeNowDay,ref deadlineDay)==true)
        {
            if ( compareDeadlineDays(ref timeNowDay,ref deadlineDay)==true)
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
            }
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.White;
        }
        Console.WriteLine($"{counter+".",-9}{readData.deadline,-16}{readData.estimatedHours+"h",-11}{readData.task}");

        counter++;
    } 
    Console.ForegroundColor= ConsoleColor.White;
    //Console.Write("Please choose a new alternative: ");
    
}*/

void newShowData()
{
    Console.Clear();
    WriteMenu();
    int counter = 0;
    string[] file = File.ReadAllLines(filename);
    string[] dateSortedFile = new string[file.Length];
    string[] dateData = new string[file.Length];
    Console.ForegroundColor = ConsoleColor.White;
    Console.WriteLine("ID       Deadline        Tid        Vad");
    foreach(string dataInFile in file)
    {
        string[] dataFromFile = dataInFile.Split(";");
        dateSortedFile[counter] = $"{dataFromFile[1]};{dataFromFile[2]};{dataFromFile[0]};{dataFromFile[3]}";
        counter++;
    }
    Array.Sort(dateSortedFile);
    counter = 0;
    foreach (string dataInFile in dateSortedFile)
    {

        ToDoItem readData = new ToDoItem();
        string[] dataFromFile = dataInFile.Split(";");
        readData.task = dataFromFile[2];
        readData.deadline = dataFromFile[0];
        readData.estimatedHours = double.Parse(dataFromFile[1]);
        readData.isCompleted = bool.Parse(dataFromFile[3]);
        DateTime now = DateTime.Now.Date;
        int timeNowYear = now.Year;
        int timeNowMonth = now.Month;
        int timeNowDay = now.Day;
        string[] deadline = readData.deadline.Split("-");
        int deadlineYear = int.Parse(deadline[0]);
        int deadlineMonth = int.Parse(deadline[1]);
        int deadlineDay = int.Parse(deadline[2]);
        

        //Console.WriteLine(comparedays(now, DateTime.Parse(dataFromFile[0])));
        dateData[counter] = readData.deadline;
        if (readData.isCompleted == true)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
        }
        else if (readData.isCompleted != true && (comparedays(now, DateTime.Parse(dataFromFile[0])) == -1))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            
        }
        else if (readData.isCompleted != true && comparedays(now, DateTime.Parse(dataFromFile[0])) <= 3 && comparedays(now, DateTime.Parse(dataFromFile[0])) >= 0)
        {
            
             Console.ForegroundColor = ConsoleColor.Yellow;
        }
        else
        {
            /*if (daysAway(now, DateTime.Parse(dataFromFile[0]))))
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
            }*/
            Console.ForegroundColor = ConsoleColor.White;
        }
        
        Console.WriteLine($"{counter + ".",-9}{readData.deadline,-16}{readData.estimatedHours + "h",-11}{readData.task}");

        //dateData = dateData.OrderBy(now => readData.deadline).ToArray();
        //dateData = dateData.OrderDescending().ToArray();
        //Array.Sort(dateData);
        
        counter++;
    }
    
    
    
    //Console.WriteLine(dateData);
    Console.ForegroundColor = ConsoleColor.White;
    //Console.Write("Please choose a new alternative: ");

}

int comparedays( DateTime timeNowDay, DateTime deadlineDay)
{
    int comparedTime = DateTime.Compare(DateTime.Parse(timeNowDay.Date.ToString("yyyy-MM-dd")), deadlineDay);
    
    if (comparedTime == 1)
    {
        return -1;
    }
    else if ((deadlineDay.Day-timeNowDay.Day <= 3 && deadlineDay.Day - timeNowDay.Day >= 0))
    { 
        return deadlineDay.Day-timeNowDay.Day; 
    }
    else
    {
        return -2;
    }
}

string daysAway(DateTime timeNowDay, DateTime deadlineDay)
{
    return (timeNowDay - deadlineDay).ToString();

}
bool compareMonth(ref int timeNowMonth, ref int deadlineMonth)
{
    if (deadlineMonth > timeNowMonth)
    {
        return true;
    }
    else
    {
        return false;
    }

}
bool compareYear(ref int timeNowYear, ref int deadlineYear)
{
    if (deadlineYear >= timeNowYear)
    {
        return true;
    }
    else
    {
        return false;
    }

}

void endProgram()
{
    Console.WriteLine("Hejdå!");
    programRuns = false;
}


public struct ToDoItem 
{
    public string task;
    public string deadline;
    public double estimatedHours;
    public bool isCompleted;
}




