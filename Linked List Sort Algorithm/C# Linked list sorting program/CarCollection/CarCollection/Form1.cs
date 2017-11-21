using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//Name: Ali Albayrak 
//Student ID - P304320 
//  Portfolio Activity 1.3
// 08/09/13
/* In the previous portfolio activity AT1.1 you added a find button and error trapping, now you will
need to add a sort button.There is no sort option for Linked Lists, so you will need to research a
suitable sort algorithm. Once you have selected a sort algorithm you will need to complete the
following tasks;
 Create a document to save the following items,
o Pseudo code for the sort algorithm,
o References to this code,
o A discuss on the benefits and pit falls of the algorithm,
o The computational complexity of the algorithm.
 Add a new button to Sort the items in the Linked List(ascending).
 Test the sort button on your UI form using more than 10 items.
 Record your testing session with a series of screen captures.
 Add comments to all the code (not XML)*/
//                                                                 Portfolio Activity 1.3 : Sorting

namespace CarCollection
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        LinkedList<string> myCarCollection = new LinkedList<string>();


        //Show Method : Clears the input fields and displays number of nodes and  every car in the myCarCollection linkedlist 
        private void showLinkedList()
        {
            // clear input boxes and list box
            listBoxOutput.Items.Clear();
            textBoxCarName.Clear();
            textBoxNodePosition.Clear();
            // display number of list nodes
            textBoxNumOfNodes.Text = numberOfNodes().ToString();
            // display linked list
            foreach (string car in myCarCollection)
                listBoxOutput.Items.Add(car);
        }

        //returns the number of nodes
        private int numberOfNodes()
        {
            return myCarCollection.Count;
        }



        // Add first button : adds the car name (taken from textBoxCarName text box) into first place of collection
        private void buttonAdd_First_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBoxCarName.Text))
            {
                myCarCollection.AddFirst(textBoxCarName.Text);
                showLinkedList();
            }
            else
                MessageBox.Show("Please enter a Car name");

        }

        //add last button : adds the car name (taken from textBoxCarName text box) into last place of collection
        private void buttonAdd_Last_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBoxCarName.Text))
            {
                myCarCollection.AddLast(textBoxCarName.Text);
                showLinkedList();
            }
            else
                MessageBox.Show("Please enter a Car name");

        }

        //Add before button : adds the car name (taken from textBoxCarName text box) into collection before the node position
        private void buttonAdd_Before_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBoxCarName.Text) &&
                        (!string.IsNullOrEmpty(textBoxNodePosition.Text)))

            {
                try
                {
                    int nodePos = Convert.ToInt32(textBoxNodePosition.Text);
                    string x = myCarCollection.ElementAt(nodePos);
                    LinkedListNode<string> current = myCarCollection.Find(x);
                    myCarCollection.AddBefore(current, textBoxCarName.Text);
                    showLinkedList();
                }
                catch (Exception)
                {
                    MessageBox.Show("Please enter valid node number (Node Position cannot be greater than " + ((myCarCollection.Count) - 1) + " )");  // List index starts with 0 that is why "-1"

                }
            }
            else
                MessageBox.Show("Please enter a Car name and Node position");

        }

        //Add after button : adds the car name (taken from textBoxCarName text box) into collection after the node position
        private void buttonAdd_After_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBoxCarName.Text) &&
                    (!string.IsNullOrEmpty(textBoxNodePosition.Text)))
            {
                try
                {
                    int nodePos = Convert.ToInt32(textBoxNodePosition.Text);
                    string x = myCarCollection.ElementAt(nodePos);
                    LinkedListNode<string> current = myCarCollection.Find(x);
                    myCarCollection.AddAfter(current, textBoxCarName.Text);
                    showLinkedList();
                }
                catch(Exception)
                {
                    MessageBox.Show("Please enter valid node number (Node Position cannot be greater than " + ((myCarCollection.Count)-1) + " )");          // List index starts with 0 that is why "-1"

                }
                
            }
            else
                MessageBox.Show("Please enter a Car name and Node position");

        }

        //Remove button : Removes the car that matches with textboxCarName.text from the collection
        private void buttonRemove_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBoxCarName.Text))
            {
                myCarCollection.Remove(textBoxCarName.Text);
                showLinkedList();
            }
            else
                MessageBox.Show("Please enter a Car name");

        }


        //Clear Button : Clears the collection 
        private void buttonClear_Click(object sender, EventArgs e)
        {
            myCarCollection.Clear();
            showLinkedList();

        }

        //Search button : Searchs for the text of the textBoxCarName in the myCarCollection
        private void buttonSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string current = myCarCollection.Find((textBoxCarName.Text)).Value;         
                int a = listBoxOutput.Items.IndexOf(current);
                listBoxOutput.SetSelected(a, true);                                         // Finds the element and sets it selected on the listbox
            }
            catch (Exception)
            {
                MessageBox.Show("Car could not found.");

            }
        }


        //Sorts the myCarCollection linked list alphabetically by using Insertion sort algorithm.
        private void buttonSort_Click(object sender, EventArgs e)               
        {
            listBoxOutput.Items.Clear();
            string temp;
            int j;
            int i = 1;
            while (i < myCarCollection.Count)
            {
                j = i;
                while(j>0 && string.Compare(myCarCollection.ElementAt(j-1), myCarCollection.ElementAt(j)) > 0)
                {
                    LinkedListNode<string> current = myCarCollection.Find(myCarCollection.ElementAt(j-1));
                    temp = current.Next.Value;
                    myCarCollection.Find(myCarCollection.ElementAt(j)).Value = myCarCollection.Find(myCarCollection.ElementAt(j-1)).Value;
                    myCarCollection.Find(myCarCollection.ElementAt(j-1)).Value = temp;
                    j = j - 1;
                }
                i = i + 1;
            }            
            foreach (string car in myCarCollection)
                listBoxOutput.Items.Add(car);         
        }


        // Load button to load cars from cars.txt file by calling the readTxtFile method
        private void buttonLoad_Click(object sender, EventArgs e)
        {
            try
            {
                readTextFile();
                showLinkedList();
            }
            catch
            {
                MessageBox.Show("Cars text cannot found. Please check your txt file.");
            }
        }


        // Method to open a stream reader and lines of
        // text into the "cars.txt" file from c:\temp\ path
        private void readTextFile()
        {
            string line = "";
            StreamReader file = new StreamReader(@"c:\temp\cars.txt");
            while ((line = file.ReadLine()) != null)
            {
                myCarCollection.AddFirst(line);
            }
            file.Close();
        }
    }
}
