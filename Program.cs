using System;
using static System.Linq.Enumerable;

namespace CRUDapp
{
  class Program
  {

    // Methods for use in rest of program
    static void PrintMenu()
    {
        Console.WriteLine("The following list outlines your options. You will need to input which option you would like to perform.");
        Console.WriteLine("L: Load a file from storage into an array \nS: Store the array back into a file");
        Console.WriteLine("C: Create a new record within the array \nR: Read the entire list from the array");
        Console.WriteLine("U: Update a record within the array \nD: Delete a record from the array");
        Console.WriteLine("Q: Quit");
    } // End PrintMenu method

    // Method to gather user input; will call the validator method to verify input is acceptable
    static char GatherInput()
    {
        char op;
        char[] arrChar;
        char[] validInputsStart = {'L', 'l', 'S', 's', 'C', 'c', 'R', 'r', 'U', 'u', 'D', 'd', 'Q', 'q'};
        Console.Write("Please input which operation you would like to perform: ");
        string? input = Console.ReadLine();
        while (input == null)
        {
            Console.WriteLine("Input must not be null. Please input the desired operation: ");
            input = Console.ReadLine();
        } // End while block

        arrChar = input.ToCharArray();
        op = StringInputValidator(arrChar, validInputsStart) == true ? arrChar[0] : GatherInput(); // If inputValidator returns true, set op to arrChar[0], otherwise call GatherInput again
        return op;
        
    } // End GatherInput method

    // Input validator method
    static bool StringInputValidator(char[] arrCharUnvalidated, char[] validInput)
    {
        bool flag = Array.Exists(validInput, i => i == arrCharUnvalidated[0]); // If, within the acceptedInput, there exists a char equivalent to the first index of arr, return true
        if(flag == true)
        {
            return true;
        } // End if statement
        else
        {
            return false;
        } // End else statement
    } // End StringInputValidator method

    // NextMove method for determining what the user wants to do after their first step
    static void NextMove(string[] nextMoveStr)
    {
        Main(nextMoveStr);
    } // End NextMove method


    // Method for engaging desired operation based on operation variable
    // Could create switch statement that calls different methods depending on desired action
    // 9/21/2022: want to focus on Load, Read, and Quit operations first

    // Switch statment method for selecting desired operation
    static void OperationSelection(char letter, string[] opSelectStr)
    {
        switch(letter)
        {
            case 'L':
            case 'l':
            opSelectStr = LoadFile();
            NextMove(opSelectStr);
            goto default;

            case 'S':
            case 's':
            StoreArray(opSelectStr);
            NextMove(opSelectStr);
            goto default;

            case 'C':
            case 'c':
            opSelectStr = CreateArrayRecord(opSelectStr);
            NextMove(opSelectStr);
            goto default;

            case 'R':
            case 'r':
            ReadArray(opSelectStr);
            NextMove(opSelectStr);
            goto default;

            case 'U':
            case 'u':
            opSelectStr = UpdateArrayRecord(opSelectStr);
            NextMove(opSelectStr);
            goto default;

            case 'D':
            case 'd':
            opSelectStr = DeleteArrayRecord(opSelectStr);
            NextMove(opSelectStr);
            goto default;

            case 'Q':
            case 'q':
            QuitProgram(opSelectStr);
            break;

            default:
            break;
        } // End switch statement 
    } // End OperationSelection method


    // Load method for loading file (secondary storage) into array (memory)
    // TODO: refactor to accept file name
    static string[] LoadFile()
    {
        string readInText = File.ReadAllText("names.txt");
        string[] textArray = readInText.Split('\n');

        return textArray;
    } // End LoadFile method

    // Store method that updates the file with the current array
    // TODO: refactor to accept file name
    static void StoreArray(string[] textToStore)
    {
        string stringTextToStore = String.Join('\n', textToStore);
        File.WriteAllText("names.txt", stringTextToStore);
        Console.WriteLine("The file has been updated.");
    } // End StoreArray method

    // Create method that creates a new record and adds it to the end of the passed-in array
    static string[] CreateArrayRecord(string[] stringCreateRecord)
    {
        string? newRecord;
        Console.Write("Please input the new record you would like to create: ");
        newRecord = Console.ReadLine();

        while (newRecord == null) // While statement to verify that the input is not null; all other input will be accepted
        {
            Console.Write("Input must not be null. Please enter a new record to create: ");
            newRecord = Console.ReadLine();
        } // End while statement

        string[] stringNewRecord = new string[stringCreateRecord.Length + 1]; // Creates new array one element longer than passed-in array
        stringCreateRecord.CopyTo(stringNewRecord, 0); // Copies the passed-in array into the newly created array starting at index 0
        stringNewRecord[stringCreateRecord.Length] = newRecord; // Sets the final index of the new array equal to the user's new record

        return stringNewRecord;
    } // End CreateArrayRecord method


    // Read method which displays the entire loaded array; possibly displays upper bound on array (20)
    static void ReadArray(string[] stringToRead)
    {
        Console.WriteLine("Here are the contents of your array: ");
        for (int i = 0; i < stringToRead.Length; i++)
        {
            int readCount = i + 1;
            Console.WriteLine("#" + readCount + ": " + stringToRead[i]);
        } // End for loop
    } // End ReadArray method

    // Update method that updates an existing record within the array
    static string[] UpdateArrayRecord(string[] stringToUpdate)
    {
        int elementToUpdate = 0;
        Console.Write("Which record would you like to update? Please input a number between 1 and " + stringToUpdate.Length + ": ");
        elementToUpdate = Convert.ToInt32(Console.ReadLine());

        while (elementToUpdate < 1 || elementToUpdate > stringToUpdate.Length)
        {
            Console.Write("Number must be between 1 and " + stringToUpdate.Length + ". Please input a number in this range: ");
            elementToUpdate = Convert.ToInt32(Console.ReadLine());
        } // End while statement

        string? updatedRecord;
        Console.Write("What would you like to update record #" + elementToUpdate + " to say: ");
        updatedRecord = Console.ReadLine();

        while (updatedRecord == null)
        {
            Console.WriteLine("The record cannot be null. Please enter a valid input: ");
            updatedRecord = Console.ReadLine();
        } // End while statement

        stringToUpdate[elementToUpdate - 1] = updatedRecord;

        return stringToUpdate;
    } // End UpdateArrayRecord method

    // Delete method that deletes a record from the array
    static string[] DeleteArrayRecord(string[] stringToDelete)
    {
        int elementToDelete = 0;
        Console.Write("Which record would you like to delete? Please input a number between 1 and " + stringToDelete.Length + ": ");
        elementToDelete = Convert.ToInt32(Console.ReadLine());

        while (elementToDelete < 1 || elementToDelete > stringToDelete.Length)
        {
            Console.Write("Number must be between 1 and " + stringToDelete.Length + ". Please input a number in this range: ");
            elementToDelete = Convert.ToInt32(Console.ReadLine());
        } // End while statement

        string[] stringDeletedRecord = new string[stringToDelete.Length - 1];
        int iNewArr = 0;
        for (int i = 0; i < stringToDelete.Length; i++)
        {
            if (i == elementToDelete - 1) // Logic to handle when the for loop reaches the element selected for deletion
            {
                stringDeletedRecord[iNewArr] = stringToDelete[i + 1];
                continue;
            } // End if statement

            stringDeletedRecord[iNewArr] = stringToDelete[i];
            iNewArr++;
        } // End for loop

        return stringDeletedRecord;
    } // End DeleteArrayRecord method

    
    // Quit method which will close the program; calls the Store method in order to save changes before quitting if user wants
    static void QuitProgram(string[] quitText)
    {
        Console.WriteLine("Are you certain you want to exit? Any unsaved work will be lost.");
        Console.WriteLine("Input 'Q' to quit without saving. Input 'S' to save and then quit.");
        Console.Write("Your input: ");
        string? confirmation = Console.ReadLine();

        while (confirmation == null)
        {
            Console.WriteLine("Input must not be null. Please enter another input: ");
            confirmation = Console.ReadLine();
        } // End while block

        char[] confirmationCharUnverified = confirmation.ToCharArray();
        char[] validInputsQuit = {'Q', 'q', 'S', 's'};

        while (StringInputValidator(confirmationCharUnverified, validInputsQuit) == false || confirmation == null)
        {
            Console.WriteLine("Input must be either 'Q' or 'S'. Please enter another input: ");
            confirmation = Console.ReadLine();
        } // End while block

        char[] confirmationCharVerified = confirmation.ToCharArray();

        if (confirmationCharVerified[0] == 'Q' || confirmationCharVerified[0] == 'q')
        {
            Console.WriteLine("See you later!");
        } // End if statement
        else
        {
            StoreArray(quitText);
        } // End else statement
        
    } // End QuitProgram method



    static void Main(string[] args)
    {            
            // PrintMenu method will get called first; ask user for input
            PrintMenu();

            // GatherInput method calls StringInputValidator method to verify that user input is acceptable
            char operation = GatherInput();
            
            string[] text = args;
            OperationSelection(operation, text);           
 
    } // End Main method
  } // End class
} // End namespace
